using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Threading;
using AForge.Video.DirectShow;
using Sw.Hospital.HealthExaminationSystem.SoftFace.SDKUtil;
using Sw.Hospital.HealthExaminationSystem.SoftFace.SDKModels;
using Sw.Hospital.HealthExaminationSystem.SoftFace.Utils;
using Sw.Hospital.HealthExaminationSystem.SoftFace.Entity;

namespace Sw.Hospital.HealthExaminationSystem.SoftFace
{
    public partial class frmFace : Form
    {
        #region 参数定义
        /// <summary>
        /// 引擎Handle
        /// </summary>
        private IntPtr pImageEngine = IntPtr.Zero;

        /// <summary>
        /// 保存右侧图片路径
        /// </summary>
        private string image1Path;

        /// <summary>
        /// 图片最大大小
        /// </summary>
        private long maxSize = 1024 * 1024 * 2;

        /// <summary>
        /// 右侧图片人脸特征
        /// </summary>
        private IntPtr image1Feature;

        /// <summary>
        /// 保存对比图片的列表
        /// </summary>
        private List<string> imagePathList = new List<string>();

        /// <summary>
        /// 左侧图库人脸特征列表
        /// </summary>
        private List<IntPtr> imagesFeatureList = new List<IntPtr>();

        /// <summary>
        /// 相似度
        /// </summary>
        private float threshold = 0.8f;

        /// <summary>
        /// 用于标记是否需要清除比对结果
        /// </summary>
        private bool isCompare = false;

        /// <summary>
        /// 是否是双目摄像
        /// </summary>
        private bool isDoubleShot = false;

        /// <summary>
        /// 允许误差范围
        /// </summary>
        private int allowAbleErrorRange = 40;

        /// <summary>
        /// RGB 摄像头索引
        /// </summary>
        private int rgbCameraIndex = 0;
        /// <summary>
        /// IR 摄像头索引
        /// </summary>
        private int irCameraIndex = 0;

        #region 视频模式下相关
        /// <summary>
        /// 视频引擎Handle
        /// </summary>
        private IntPtr pVideoEngine = IntPtr.Zero;
        /// <summary>
        /// RGB视频引擎 FR Handle 处理   FR和图片引擎分开，减少强占引擎的问题
        /// </summary>
        private IntPtr pVideoRGBImageEngine = IntPtr.Zero;
        /// <summary>
        /// IR视频引擎 FR Handle 处理   FR和图片引擎分开，减少强占引擎的问题
        /// </summary>
        private IntPtr pVideoIRImageEngine = IntPtr.Zero;
        /// <summary>
        /// 视频输入设备信息
        /// </summary>
        private FilterInfoCollection filterInfoCollection;
        /// <summary>
        /// RGB摄像头设备
        /// </summary>
        private VideoCaptureDevice rgbDeviceVideo;
        /// <summary>
        /// IR摄像头设备
        /// </summary>
        private VideoCaptureDevice irDeviceVideo;
        #endregion

        #endregion

        #region 初始化
    

        /// <summary>
        /// 初始化引擎
        /// </summary>
        private void InitEngines()
        {
            //读取配置文件
            AppSettingsReader reader = new AppSettingsReader();
            string appId = (string)reader.GetValue("APP_ID", typeof(string));
            string sdkKey64 = (string)reader.GetValue("SDKKEY64", typeof(string));
            string sdkKey32 = (string)reader.GetValue("SDKKEY32", typeof(string));
            rgbCameraIndex = (int)reader.GetValue("RGB_CAMERA_INDEX", typeof(int));
            irCameraIndex = (int)reader.GetValue("IR_CAMERA_INDEX", typeof(int));
            //判断CPU位数
            var is64CPU = Environment.Is64BitProcess;
            if (string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(is64CPU ? sdkKey64 : sdkKey32))
            {
                //禁用相关功能按钮
                //ControlsEnable(false, chooseMultiImgBtn, matchBtn, btnClearFaceList, chooseImgBtn);
                MessageBox.Show(string.Format("请在App.config配置文件中先配置APP_ID和SDKKEY{0}!", is64CPU ? "64" : "32"));
                return;
            }

            //在线激活引擎    如出现错误，1.请先确认从官网下载的sdk库已放到对应的bin中，2.当前选择的CPU为x86或者x64
            int retCode = 0;
            try
            {
                retCode = ASFFunctions.ASFActivation(appId, is64CPU ? sdkKey64 : sdkKey32);
            }
            catch (Exception ex)
            {
                //禁用相关功能按钮
                //ControlsEnable(false, chooseMultiImgBtn, matchBtn, btnClearFaceList, chooseImgBtn);
                if (ex.Message.Contains("无法加载 DLL"))
                {
                    MessageBox.Show("请将sdk相关DLL放入bin对应的x86或x64下的文件夹中!");
                }
                else
                {
                    MessageBox.Show("激活引擎失败!");
                }
                return;
            }
            Console.WriteLine("Activate Result:" + retCode);

            //初始化引擎
            uint detectMode = DetectionMode.ASF_DETECT_MODE_IMAGE;
            //Video模式下检测脸部的角度优先值
            int videoDetectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_HIGHER_EXT;
            //Image模式下检测脸部的角度优先值
            int imageDetectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_ONLY;
            //人脸在图片中所占比例，如果需要调整检测人脸尺寸请修改此值，有效数值为2-32
            int detectFaceScaleVal = 16;
            //最大需要检测的人脸个数
            int detectFaceMaxNum = 5;
            //引擎初始化时需要初始化的检测功能组合
            int combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_AGE | FaceEngineMask.ASF_GENDER | FaceEngineMask.ASF_FACE3DANGLE;
            //初始化引擎，正常值为0，其他返回值请参考http://ai.arcsoft.com.cn/bbs/forum.php?mod=viewthread&tid=19&_dsign=dbad527e
            retCode = ASFFunctions.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pImageEngine);
            Console.WriteLine("InitEngine Result:" + retCode);
            AppendText((retCode == 0) ? "引擎初始化成功!\n" : string.Format("引擎初始化失败!错误码为:{0}\n", retCode));
            if (retCode != 0)
            {
                //禁用相关功能按钮
               // ControlsEnable(false, chooseMultiImgBtn, matchBtn, btnClearFaceList, chooseImgBtn);
            }

            //初始化视频模式下人脸检测引擎
            uint detectModeVideo = DetectionMode.ASF_DETECT_MODE_VIDEO;
            int combinedMaskVideo = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION;
            retCode = ASFFunctions.ASFInitEngine(detectModeVideo, videoDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMaskVideo, ref pVideoEngine);
            //RGB视频专用FR引擎
            detectFaceMaxNum = 1;
            combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_LIVENESS;
            retCode = ASFFunctions.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pVideoRGBImageEngine);

            //IR视频专用FR引擎
            combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_IR_LIVENESS;
            retCode = ASFFunctions.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pVideoIRImageEngine);

            Console.WriteLine("InitVideoEngine Result:" + retCode);


            initVideo();
        }

        /// <summary>
        /// 摄像头初始化
        /// </summary>
        private void initVideo()
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            //如果没有可用摄像头，“启用摄像头”按钮禁用，否则使可用
            if (filterInfoCollection.Count == 0)
            {
                //btnStartVideo.Enabled = false;
            }
            else
            {
                //btnStartVideo.Enabled = true;
            }
        }

        #endregion
        #region 公用方法
        /// <summary>
        /// 恢复使用/禁用控件列表控件
        /// </summary>
        /// <param name="isEnable"></param>
        /// <param name="controls">控件列表</param>
        private void ControlsEnable(bool isEnable, params Control[] controls)
        {
            if (controls == null || controls.Length <= 0)
            {
                return;
            }
            foreach (Control control in controls)
            {
                control.Enabled = isEnable;
            }
        }

        /// <summary>
        /// 校验图片
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        private bool checkImage(string imagePath)
        {
            if (imagePath == null)
            {
                AppendText("图片不存在，请确认后再导入\r\n");
                return false;
            }
            try
            {
                //判断图片是否正常，如将其他文件把后缀改为.jpg，这样就会报错
                Image image = ImageUtil.readFromFile(imagePath);
                if (image == null)
                {
                    throw new Exception();
                }
                else
                {
                    image.Dispose();
                }
            }
            catch
            {
                AppendText(string.Format("{0} 图片格式有问题，请确认后再导入\r\n", imagePath));
                return false;
            }
            FileInfo fileCheck = new FileInfo(imagePath);
            if (fileCheck.Exists == false)
            {
                AppendText(string.Format("{0} 不存在\r\n", fileCheck.Name));
                return false;
            }
            else if (fileCheck.Length > maxSize)
            {
                AppendText(string.Format("{0} 图片大小超过2M，请压缩后再导入\r\n", fileCheck.Name));
                return false;
            }
            else if (fileCheck.Length < 2)
            {
                AppendText(string.Format("{0} 图像质量太小，请重新选择\r\n", fileCheck.Name));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 追加公用方法
        /// </summary>
        /// <param name="message"></param>
        private void AppendText(string message)
        {
            logBox.AppendText(message);
        }

        /// <summary>
        /// 判断参数0与参数1是否在误差允许范围内
        /// </summary>
        /// <param name="arg0">参数0</param>
        /// <param name="arg1">参数1</param>
        /// <returns></returns>
        private bool isInAllowErrorRange(float arg0, float arg1)
        {
            bool rel = false;
            if (arg0 > arg1 - allowAbleErrorRange && arg0 < arg1 + allowAbleErrorRange)
            {
                rel = true;
            }
            return rel;
        }
        #endregion
        public frmFace()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            //初始化引擎
            InitEngines();
            //隐藏摄像头图像窗口
            rgbVideoSource.Hide();
            irVideoSource.Hide();
            //阈值控件不可用
            txtThreshold.Enabled = false;
        }

        private void frmFace_Load(object sender, EventArgs e)
        {

        }

        private void btnStartVideo_Click(object sender, EventArgs e)
        {

        }

        #region 视频检测相关(<摄像头按钮点击事件、摄像头Paint事件、特征比对、摄像头播放完成事件>)

        /// <summary>
        /// 摄像头按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartVideo()
        {
            //在点击开始的时候再坐下初始化检测，防止程序启动时有摄像头，在点击摄像头按钮之前将摄像头拔掉的情况
            initVideo();
            //必须保证有可用摄像头
            if (filterInfoCollection.Count == 0)
            {
                MessageBox.Show("未检测到摄像头，请确保已安装摄像头或驱动!");
                return;
            }
            if (rgbVideoSource.IsRunning || irVideoSource.IsRunning)
            {
                //btnStartVideo.Text = "启用摄像头";
                //关闭摄像头
                if (irVideoSource.IsRunning)
                {
                    irVideoSource.SignalToStop();
                    irVideoSource.Hide();
                }
                if (rgbVideoSource.IsRunning)
                {
                    rgbVideoSource.SignalToStop();
                    rgbVideoSource.Hide();
                }
                //“选择识别图”、“开始匹配”按钮可用，阈值控件禁用
                //chooseImgBtn.Enabled = true;
                //matchBtn.Enabled = true;
                txtThreshold.Enabled = false;
            }
            else
            {
                if (isCompare)
                {
                    //比对结果清除
                    for (int i = 0; i < imagesFeatureList.Count; i++)
                    {
                        imageList.Items[i].Text = string.Format("{0}号", i);
                    }
                    //lblCompareInfo.Text = "";
                    isCompare = false;
                }
                //“选择识别图”、“开始匹配”按钮禁用，阈值控件可用，显示摄像头控件
                txtThreshold.Enabled = true;
                rgbVideoSource.Show();
                irVideoSource.Show();
                //chooseImgBtn.Enabled = false;
                //matchBtn.Enabled = false;
                //btnStartVideo.Text = "关闭摄像头";
                //获取filterInfoCollection的总数
                int maxCameraCount = filterInfoCollection.Count;
                //如果配置了两个不同的摄像头索引
                if (rgbCameraIndex != irCameraIndex && maxCameraCount >= 2)
                {
                    //RGB摄像头加载
                    rgbDeviceVideo = new VideoCaptureDevice(filterInfoCollection[rgbCameraIndex < maxCameraCount ? rgbCameraIndex : 0].MonikerString);
                    rgbDeviceVideo.VideoResolution = rgbDeviceVideo.VideoCapabilities[0];
                    rgbVideoSource.VideoSource = rgbDeviceVideo;
                    rgbVideoSource.Start();

                    //IR摄像头
                    irDeviceVideo = new VideoCaptureDevice(filterInfoCollection[irCameraIndex < maxCameraCount ? irCameraIndex : 0].MonikerString);
                    irDeviceVideo.VideoResolution = irDeviceVideo.VideoCapabilities[0];
                    irVideoSource.VideoSource = irDeviceVideo;
                    irVideoSource.Start();
                    //双摄标志设为true
                    isDoubleShot = true;
                }
                else
                {
                    //仅打开RGB摄像头，IR摄像头控件隐藏
                    rgbDeviceVideo = new VideoCaptureDevice(filterInfoCollection[rgbCameraIndex <= maxCameraCount ? rgbCameraIndex : 0].MonikerString);
                    rgbDeviceVideo.VideoResolution = rgbDeviceVideo.VideoCapabilities[0];
                    rgbVideoSource.VideoSource = rgbDeviceVideo;
                    rgbVideoSource.Start();
                    irVideoSource.Hide();
                }
            }
        }

        private FaceTrackUnit trackRGBUnit = new FaceTrackUnit();
        private FaceTrackUnit trackIRUnit = new FaceTrackUnit();
        private Font font = new Font(FontFamily.GenericSerif, 10f, FontStyle.Bold);
        private SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        private SolidBrush blueBrush = new SolidBrush(Color.Blue);
        private bool isRGBLock = false;
        private bool isIRLock = false;
        private MRECT allRect = new MRECT();
        private object rectLock = new object();

        /// <summary>
        /// RGB摄像头Paint事件，图像显示到窗体上，得到每一帧图像，并进行处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void videoSource_Paint(object sender, PaintEventArgs e)
        {
            if (rgbVideoSource.IsRunning)
            {
                //得到当前RGB摄像头下的图片
                Bitmap bitmap = rgbVideoSource.GetCurrentVideoFrame();
                if (bitmap == null)
                {
                    return;
                }
                //检测人脸，得到Rect框
                ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(pVideoEngine, bitmap);
                //得到最大人脸
                ASF_SingleFaceInfo maxFace = FaceUtil.GetMaxFace(multiFaceInfo);
                //得到Rect
                MRECT rect = maxFace.faceRect;
                //检测RGB摄像头下最大人脸
                Graphics g = e.Graphics;
                float offsetX = rgbVideoSource.Width * 1f / bitmap.Width;
                float offsetY = rgbVideoSource.Height * 1f / bitmap.Height;
                float x = rect.left * offsetX;
                float width = rect.right * offsetX - x;
                float y = rect.top * offsetY;
                float height = rect.bottom * offsetY - y;
                //根据Rect进行画框
                g.DrawRectangle(Pens.Red, x, y, width, height);
                if (trackRGBUnit.message != "" && x > 0 && y > 0)
                {
                    //将上一帧检测结果显示到页面上
                    g.DrawString(trackRGBUnit.message, font, trackRGBUnit.message.Contains("真人") ? blueBrush : yellowBrush, x, y - 15);
                }

                //保证只检测一帧，防止页面卡顿以及出现其他内存被占用情况
                if (isRGBLock == false)
                {
                    isRGBLock = true;
                    //异步处理提取特征值和比对，不然页面会比较卡
                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                    {
                        if (rect.left != 0 && rect.right != 0 && rect.top != 0 && rect.bottom != 0)
                        {
                            try
                            {
                                lock (rectLock)
                                {
                                    allRect.left = (int)(rect.left * offsetX);
                                    allRect.top = (int)(rect.top * offsetY);
                                    allRect.right = (int)(rect.right * offsetX);
                                    allRect.bottom = (int)(rect.bottom * offsetY);
                                }

                                bool isLiveness = false;

                                //调整图片数据，非常重要
                                ImageInfo imageInfo = ImageUtil.ReadBMP(bitmap);
                                if (imageInfo == null)
                                {
                                    return;
                                }
                                int retCode_Liveness = -1;
                                //RGB真人检测
                                ASF_LivenessInfo liveInfo = FaceUtil.LivenessInfo_RGB(pVideoRGBImageEngine, imageInfo, multiFaceInfo, out retCode_Liveness);
                                //判断检测结果
                                if (retCode_Liveness == 0 && liveInfo.num > 0)
                                {
                                    int isLive = MemoryUtil.PtrToStructure<int>(liveInfo.isLive);
                                    isLiveness = (isLive == 1) ? true : false;
                                }
                                if (imageInfo != null)
                                {
                                    MemoryUtil.Free(imageInfo.imgData);
                                }
                                if (isLiveness)
                                {
                                    //提取人脸特征
                                    IntPtr feature = FaceUtil.ExtractFeature(pVideoRGBImageEngine, bitmap, maxFace);
                                    float similarity = 0f;
                                    //得到比对结果
                                    int result = compareFeature(feature, out similarity);
                                    MemoryUtil.Free(feature);
                                    if (result > -1)
                                    {
                                        //将比对结果放到显示消息中，用于最新显示
                                        trackRGBUnit.message = string.Format(" {0}号 {1},{2}", result, similarity, string.Format("RGB{0}", isLiveness ? "真人" : "非真人"));
                                    }
                                    else
                                    {
                                        //显示消息
                                        trackRGBUnit.message = string.Format("RGB{0}", isLiveness ? "真人" : "非真人");
                                    }
                                }
                                else
                                {
                                    //显示消息
                                    trackRGBUnit.message = string.Format("RGB{0}", isLiveness ? "真人" : "非真人");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            finally
                            {
                                if (bitmap != null)
                                {
                                    bitmap.Dispose();
                                }
                                isRGBLock = false;
                            }
                        }
                        else
                        {
                            lock (rectLock)
                            {
                                allRect.left = 0;
                                allRect.top = 0;
                                allRect.right = 0;
                                allRect.bottom = 0;
                            }
                        }
                        isRGBLock = false;
                    }));
                }
            }
        }

        /// <summary>
        /// RGB摄像头Paint事件,同步RGB人脸框，对比人脸框后进行IR真人检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void irVideoSource_Paint(object sender, PaintEventArgs e)
        {
            if (isDoubleShot && irVideoSource.IsRunning)
            {
                //如果双摄，且IR摄像头工作，获取IR摄像头图片
                Bitmap irBitmap = irVideoSource.GetCurrentVideoFrame();
                if (irBitmap == null)
                {
                    return;
                }
                //得到Rect
                MRECT rect = new MRECT();
                lock (rectLock)
                {
                    rect = allRect;
                }
                float irOffsetX = irVideoSource.Width * 1f / irBitmap.Width;
                float irOffsetY = irVideoSource.Height * 1f / irBitmap.Height;
                float offsetX = irVideoSource.Width * 1f / rgbVideoSource.Width;
                float offsetY = irVideoSource.Height * 1f / rgbVideoSource.Height;
                //检测IR摄像头下最大人脸
                Graphics g = e.Graphics;

                float x = rect.left * offsetX;
                float width = rect.right * offsetX - x;
                float y = rect.top * offsetY;
                float height = rect.bottom * offsetY - y;
                //根据Rect进行画框
                g.DrawRectangle(Pens.Red, x, y, width, height);
                if (trackIRUnit.message != "" && x > 0 && y > 0)
                {
                    //将上一帧检测结果显示到页面上
                    g.DrawString(trackIRUnit.message, font, trackIRUnit.message.Contains("真人") ? blueBrush : yellowBrush, x, y - 15);
                }

                //保证只检测一帧，防止页面卡顿以及出现其他内存被占用情况
                if (isIRLock == false)
                {
                    isIRLock = true;
                    //异步处理提取特征值和比对，不然页面会比较卡
                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                    {
                        if (rect.left != 0 && rect.right != 0 && rect.top != 0 && rect.bottom != 0)
                        {
                            bool isLiveness = false;
                            try
                            {
                                //得到当前摄像头下的图片
                                if (irBitmap != null)
                                {
                                    //检测人脸，得到Rect框
                                    ASF_MultiFaceInfo irMultiFaceInfo = FaceUtil.DetectFace(pVideoIRImageEngine, irBitmap);
                                    if (irMultiFaceInfo.faceNum <= 0)
                                    {
                                        return;
                                    }
                                    //得到最大人脸
                                    ASF_SingleFaceInfo irMaxFace = FaceUtil.GetMaxFace(irMultiFaceInfo);
                                    //得到Rect
                                    MRECT irRect = irMaxFace.faceRect;
                                    //判断RGB图片检测的人脸框与IR摄像头检测的人脸框偏移量是否在误差允许范围内
                                    if (isInAllowErrorRange(rect.left * offsetX / irOffsetX, irRect.left) && isInAllowErrorRange(rect.right * offsetX / irOffsetX, irRect.right)
                                        && isInAllowErrorRange(rect.top * offsetY / irOffsetY, irRect.top) && isInAllowErrorRange(rect.bottom * offsetY / irOffsetY, irRect.bottom))
                                    {
                                        int retCode_Liveness = -1;
                                        //将图片进行灰度转换，然后获取图片数据
                                        ImageInfo irImageInfo = ImageUtil.ReadBMP_IR(irBitmap);
                                        if (irImageInfo == null)
                                        {
                                            return;
                                        }
                                        //IR真人检测
                                        ASF_LivenessInfo liveInfo = FaceUtil.LivenessInfo_IR(pVideoIRImageEngine, irImageInfo, irMultiFaceInfo, out retCode_Liveness);
                                        //判断检测结果
                                        if (retCode_Liveness == 0 && liveInfo.num > 0)
                                        {
                                            int isLive = MemoryUtil.PtrToStructure<int>(liveInfo.isLive);
                                            isLiveness = (isLive == 1) ? true : false;
                                        }
                                        if (irImageInfo != null)
                                        {
                                            MemoryUtil.Free(irImageInfo.imgData);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            finally
                            {
                                trackIRUnit.message = string.Format("IR{0}", isLiveness ? "真人" : "非真人");
                                if (irBitmap != null)
                                {
                                    irBitmap.Dispose();
                                }
                                isIRLock = false;
                            }
                        }
                        else
                        {
                            trackIRUnit.message = string.Empty;
                        }
                        isIRLock = false;
                    }));
                }
            }
        }


        /// <summary>
        /// 得到feature比较结果
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        private int compareFeature(IntPtr feature, out float similarity)
        {
            int result = -1;
            similarity = 0f;
            //如果人脸库不为空，则进行人脸匹配
            if (imagesFeatureList != null && imagesFeatureList.Count > 0)
            {
                for (int i = 0; i < imagesFeatureList.Count; i++)
                {
                    //调用人脸匹配方法，进行匹配
                    ASFFunctions.ASFFaceFeatureCompare(pVideoRGBImageEngine, feature, imagesFeatureList[i], ref similarity);
                    if (similarity >= threshold)
                    {
                        result = i;
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 摄像头播放完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="reason"></param>
        private void videoSource_PlayingFinished(object sender, AForge.Video.ReasonToFinishPlaying reason)
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;
               // chooseImgBtn.Enabled = true;
               // matchBtn.Enabled = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        private void frmFace_Shown(object sender, EventArgs e)
        {
            StartVideo();
        }

        private void chooseMultiImgBtn_Click(object sender, EventArgs e)
        {
           // ChooseMultiImg();
            ChooseImg();
        }
        #region 注册人脸按钮事件

        private object locker = new object();
        /// <summary>
        /// 人脸库图片选择按钮事件
        /// </summary>
        private void ChooseMultiImg()
        {
            lock (locker)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "选择图片";
                openFileDialog.Filter = "图片文件|*.bmp;*.jpg;*.jpeg;*.png";
                openFileDialog.Multiselect = true;
                openFileDialog.FileName = string.Empty;
                imageList.Refresh();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    List<string> imagePathListTemp = new List<string>();
                    var numStart = imagePathList.Count;
                    int isGoodImage = 0;

                    //保存图片路径并显示
                    string[] fileNames = openFileDialog.FileNames;
                    for (int i = 0; i < fileNames.Length; i++)
                    {
                        //图片格式判断
                        if (checkImage(fileNames[i]))
                        {
                            imagePathListTemp.Add(fileNames[i]);
                        }
                    }

                    //人脸检测以及提取人脸特征
                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                    {
                        //禁止点击按钮
                        Invoke(new Action(delegate
                        {
                            chooseMultiImgBtn.Enabled = false;
                           // matchBtn.Enabled = false;
                            //btnClearFaceList.Enabled = false;
                           // chooseImgBtn.Enabled = false;
                            //btnStartVideo.Enabled = false;
                        }));

                        //人脸检测和剪裁
                        for (int i = 0; i < imagePathListTemp.Count; i++)
                        {
                            Image image = ImageUtil.readFromFile(imagePathListTemp[i]);
                            if (image == null)
                            {
                                continue;
                            }
                            if (image.Width > 1536 || image.Height > 1536)
                            {
                                image = ImageUtil.ScaleImage(image, 1536, 1536);
                            }
                            if (image == null)
                            {
                                continue;
                            }
                            if (image.Width % 4 != 0)
                            {
                                image = ImageUtil.ScaleImage(image, image.Width - (image.Width % 4), image.Height);
                            }
                            //人脸检测
                            ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(pImageEngine, image);
                            //判断检测结果
                            if (multiFaceInfo.faceNum > 0)
                            {
                                imagePathList.Add(imagePathListTemp[i]);
                                MRECT rect = MemoryUtil.PtrToStructure<MRECT>(multiFaceInfo.faceRects);
                                image = ImageUtil.CutImage(image, rect.left, rect.top, rect.right, rect.bottom);
                            }
                            else
                            {
                                if (image != null)
                                {
                                    image.Dispose();
                                }
                                continue;
                            }
                            //显示人脸
                            this.Invoke(new Action(delegate
                            {
                                if (image == null)
                                {
                                    image = ImageUtil.readFromFile(imagePathListTemp[i]);

                                    if (image.Width > 1536 || image.Height > 1536)
                                    {
                                        image = ImageUtil.ScaleImage(image, 1536, 1536);
                                    }
                                }
                                imageLists.Images.Add(imagePathListTemp[i], image);
                                imageList.Items.Add((numStart + isGoodImage) + "号", imagePathListTemp[i]);
                                imageList.Refresh();
                                isGoodImage += 1;
                                if (image != null)
                                {
                                    image.Dispose();
                                }
                            }));
                        }

                        //提取人脸特征
                        for (int i = numStart; i < imagePathList.Count; i++)
                        {
                            ASF_SingleFaceInfo singleFaceInfo = new ASF_SingleFaceInfo();
                            Image image = ImageUtil.readFromFile(imagePathList[i]);
                            if (image == null)
                            {
                                continue;
                            }
                            IntPtr feature = FaceUtil.ExtractFeature(pImageEngine, image, out singleFaceInfo);
                            this.Invoke(new Action(delegate
                            {
                                if (singleFaceInfo.faceRect.left == 0 && singleFaceInfo.faceRect.right == 0)
                                {
                                    AppendText(string.Format("{0}号未检测到人脸\r\n", i));
                                }
                                else
                                {
                                    AppendText(string.Format("已提取{0}号人脸特征值，[left:{1},right:{2},top:{3},bottom:{4},orient:{5}]\r\n", i, singleFaceInfo.faceRect.left, singleFaceInfo.faceRect.right, singleFaceInfo.faceRect.top, singleFaceInfo.faceRect.bottom, singleFaceInfo.faceOrient));
                                    imagesFeatureList.Add(feature);
                                }
                            }));
                            if (image != null)
                            {
                                image.Dispose();
                            }
                        }
                        //允许点击按钮
                        Invoke(new Action(delegate
                        {
                            chooseMultiImgBtn.Enabled = true;
                            //btnClearFaceList.Enabled = true;
                            //btnStartVideo.Enabled = true;

                            //if (btnStartVideo.Text == "启用摄像头")
                            //{
                            //   // chooseImgBtn.Enabled = true;
                            //    //matchBtn.Enabled = true;
                            //}
                            //else
                            //{
                            //    //chooseImgBtn.Enabled = false;
                            //   // matchBtn.Enabled = false;
                            //}
                        }));
                    }));

                }
            }
        }
        /// <summary>
        /// 人脸库图片选择按钮事件
        /// </summary>
        private void ChooseImg()
        {
            lock (locker)
            {
              

                    List<string> imagePathListTemp = new List<string>();
                    var numStart = 0;
                    int isGoodImage = 0;
                
               
                Bitmap bitmap = (Bitmap)rgbVideoSource.GetCurrentVideoFrame().Clone();
                pictureBox1.Image = bitmap;
                var iamgeNmae = System.IO.Directory.GetCurrentDirectory() + "\\image\\" + System.DateTime.Now.ToString("yyMMddss");
                pictureBox1.Image.Save(iamgeNmae);
                imagePathListTemp.Add(iamgeNmae);
                //保存图片路径并显示
                //string[] fileNames = openFileDialog.FileNames;
                //    for (int i = 0; i < fileNames.Length; i++)
                //    {
                //        //图片格式判断
                //        if (checkImage(fileNames[i]))
                //        {
                //            imagePathListTemp.Add(fileNames[i]);
                //        }
                //    }


                    //人脸检测以及提取人脸特征
                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                    {
                        //禁止点击按钮
                        Invoke(new Action(delegate
                        {
                            chooseMultiImgBtn.Enabled = false;
                            // matchBtn.Enabled = false;
                            //btnClearFaceList.Enabled = false;
                            // chooseImgBtn.Enabled = false;
                            //btnStartVideo.Enabled = false;
                        }));

                        //人脸检测和剪裁
                        for (int i = 0; i < imagePathListTemp.Count; i++)
                        {
                            Image image = ImageUtil.readFromFile(imagePathListTemp[i]);
                            if (image == null)
                            {
                                continue;
                            }
                            if (image.Width > 1536 || image.Height > 1536)
                            {
                                image = ImageUtil.ScaleImage(image, 1536, 1536);
                            }
                            if (image == null)
                            {
                                continue;
                            }
                            if (image.Width % 4 != 0)
                            {
                                image = ImageUtil.ScaleImage(image, image.Width - (image.Width % 4), image.Height);
                            }
                            //人脸检测
                            ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(pImageEngine, image);
                            //判断检测结果
                            if (multiFaceInfo.faceNum > 0)
                            {
                                imagePathList.Add(imagePathListTemp[i]);
                                MRECT rect = MemoryUtil.PtrToStructure<MRECT>(multiFaceInfo.faceRects);
                                image = ImageUtil.CutImage(image, rect.left, rect.top, rect.right, rect.bottom);
                            }
                            else
                            {
                                if (image != null)
                                {
                                    image.Dispose();
                                }
                                continue;
                            }
                            //显示人脸
                            this.Invoke(new Action(delegate
                            {
                                if (image == null)
                                {
                                    image = ImageUtil.readFromFile(imagePathListTemp[i]);

                                    if (image.Width > 1536 || image.Height > 1536)
                                    {
                                        image = ImageUtil.ScaleImage(image, 1536, 1536);
                                    }
                                }
                                imageLists.Images.Add(imagePathListTemp[i], image);
                                imageList.Items.Add((numStart + isGoodImage) + "号", imagePathListTemp[i]);
                                imageList.Refresh();
                                isGoodImage += 1;
                                if (image != null)
                                {
                                    image.Dispose();
                                }
                            }));
                        }

                        //提取人脸特征
                        for (int i = numStart; i < imagePathList.Count; i++)
                        {
                            ASF_SingleFaceInfo singleFaceInfo = new ASF_SingleFaceInfo();
                            Image image = ImageUtil.readFromFile(imagePathList[i]);
                            if (image == null)
                            {
                                continue;
                            }
                            IntPtr feature = FaceUtil.ExtractFeature(pImageEngine, image, out singleFaceInfo);
                            this.Invoke(new Action(delegate
                            {
                                if (singleFaceInfo.faceRect.left == 0 && singleFaceInfo.faceRect.right == 0)
                                {
                                    AppendText(string.Format("{0}号未检测到人脸\r\n", i));
                                }
                                else
                                {
                                    AppendText(string.Format("已提取{0}号人脸特征值，[left:{1},right:{2},top:{3},bottom:{4},orient:{5}]\r\n", i, singleFaceInfo.faceRect.left, singleFaceInfo.faceRect.right, singleFaceInfo.faceRect.top, singleFaceInfo.faceRect.bottom, singleFaceInfo.faceOrient));
                                    imagesFeatureList.Add(feature);
                                }
                            }));
                            if (image != null)
                            {
                                image.Dispose();
                            }
                        }
                        //允许点击按钮
                        Invoke(new Action(delegate
                        {
                            chooseMultiImgBtn.Enabled = true;
                            //btnClearFaceList.Enabled = true;
                            //btnStartVideo.Enabled = true;

                            //if (btnStartVideo.Text == "启用摄像头")
                            //{
                            //    // chooseImgBtn.Enabled = true;
                            //    //matchBtn.Enabled = true;
                            //}
                            //else
                            //{
                            //    //chooseImgBtn.Enabled = false;
                            //    // matchBtn.Enabled = false;
                            //}
                        }));
                    }));

                
            }
        }
        #endregion

        private void frmFace_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                
                    // btnStartVideo_Click(sender, e); //关闭摄像头


                    //关闭摄像头
                    if (irVideoSource.IsRunning)
                    {
                        irVideoSource.SignalToStop();
                        irVideoSource.Hide();
                    }
                    if (rgbVideoSource.IsRunning)
                    {
                        rgbVideoSource.SignalToStop();
                        rgbVideoSource.Hide();
                    }
                    //“选择识别图”、“开始匹配”按钮可用，阈值控件禁用

                    txtThreshold.Enabled = false;
              

                //销毁引擎
                int retCode = ASFFunctions.ASFUninitEngine(pImageEngine);
                Console.WriteLine("UninitEngine pImageEngine Result:" + retCode);
                //销毁引擎
                retCode = ASFFunctions.ASFUninitEngine(pVideoEngine);
                Console.WriteLine("UninitEngine pVideoEngine Result:" + retCode);

                //销毁引擎
                retCode = ASFFunctions.ASFUninitEngine(pVideoRGBImageEngine);
                Console.WriteLine("UninitEngine pVideoImageEngine Result:" + retCode);

                //销毁引擎
                retCode = ASFFunctions.ASFUninitEngine(pVideoIRImageEngine);
                Console.WriteLine("UninitEngine pVideoIRImageEngine Result:" + retCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UninitEngine pImageEngine Error:" + ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
