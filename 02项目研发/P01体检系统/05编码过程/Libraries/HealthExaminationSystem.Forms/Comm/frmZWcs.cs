using natrotech;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Comm
{
    public partial class frmZWcs : UserBaseForm
    {
        private int emptyBM = -1;
        public frmZWcs()
        {
            InitializeComponent();
        }

        private void frmZWcs_Load(object sender, EventArgs e)
        {
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            startEnroll();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            startIdentify();
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static void clearAllFeature()
        {

            int nRet = FpSdk.Init();
            if (nRet != FpSdk.RTC_SUCCESS)
            {
                Console.WriteLine("指纹仪连接失败");
                return;
            }

            nRet = FpSdk.ClearAllFeature();
            if (nRet == FpSdk.RTC_SUCCESS)
            {
                Console.WriteLine("所有指纹清除成功");
            }
            else
            {
                Console.WriteLine("指纹清除失败,错误码：{0}", nRet);
            }
        }

        public  void startEnroll()
        {



            if (FpSdk.Init() == FpSdk.RTC_SUCCESS)
            {
                int emptyId =3;
                //int v = FpSdk.GetEmptyId(out emptyId);

                int nRet = FpSdk.ChangeFeature(emptyId, (nCode, nData) =>
                {
                    switch (nCode)
                    {
                        case FpSdk.RTC_NEED_FIRST_SWEEP:
                            //Console.WriteLine("第一次登记，请按手指");
                          
                            labInfo.Text = "第一次登记，请按手指";
                            labInfo.Refresh();
                            
                            break;
                        case FpSdk.RTC_NEED_SECOND_SWEEP:
                           // Console.WriteLine("第二次登记，请按手指");
                            labInfo.Text = "第二次登记，请按手指";
                            labInfo.Refresh();
                            break;
                        case FpSdk.RTC_NEED_THIRD_SWEEP:
                            //Console.WriteLine("第三次登记，请按手指");
                            
                            labInfo.Text = "第三次登记，请按手指";
                            labInfo.Refresh();
                          
                            break;
                        case FpSdk.RTC_NEED_RELEASE_FINGER:
                            //Console.WriteLine("请离开手指");
                           
                            labInfo.Text = "请离开手指";
                            labInfo.Refresh();
                           
                            break;
                        case FpSdk.RTC_BAD_QUALITY:
                            // Console.WriteLine("指纹质量差,请重试");
                           
                            labInfo.Text = "指纹质量差,请重试";
                            labInfo.Refresh();
                            
                            break;
                        case FpSdk.RTC_DUPLICATION_ID:
                            //Console.WriteLine("指纹重复,该指纹已登记,登记位置ID为:{0}", nData);
                          
                            labInfo.Text = string.Format("指纹重复,该指纹已登记,登记位置ID为:{0}", nData);
                            labInfo.Refresh();
                            
                            break;
                        default:
                            //Console.WriteLine("UNKNOW");
                           
                                labInfo.Text = "UNKNOW";
                            labInfo.Refresh();
                            
                            break;
                    }
                });


                if (nRet == FpSdk.RTC_SUCCESS)
                {
                    emptyBM = emptyId;
                  
                        labInfo.Text =string.Format("登记指纹成功 id为:{0}", emptyId);
                    labInfo.Refresh();
                  
                    //Console.WriteLine("登记指纹成功 id为:{0}", emptyId);
                }
                else if (nRet == FpSdk.RTC_DEVICE_NOT_FOUND)
                {
                   
                        labInfo.Text = string.Format("找不到指纹仪");
                   
                //Console.WriteLine("找不到指纹仪");
            }
                else
                {
                    
                        labInfo.Text = string.Format("登记指纹失败，错误码：{0}  \n", nRet);
                   
                    //Console.WriteLine("登记指纹失败，错误码：{0}  \n", nRet);
                }
            }
            else
            {
                
                    labInfo.Text = string.Format("找不到指纹仪");
               
                // Console.WriteLine("找不到指纹仪");
            }
        }
        public void startEnroll1()
        {



            if (FpSdk.Init() == FpSdk.RTC_SUCCESS)
            {
                int emptyId = 3;
                //int v = FpSdk.GetEmptyId(out emptyId);

                int nRet = FpSdk.ChangeFeature(emptyId, (nCode, nData) =>
                {
                    switch (nCode)
                    {
                        case FpSdk.RTC_NEED_FIRST_SWEEP:
                            //Console.WriteLine("第一次登记，请按手指");
                            BeginInvoke(new Action(() =>
                            {
                                labInfo.Text = "第一次登记，请按手指";
                                labInfo.Refresh();
                            }));
                            break;
                        case FpSdk.RTC_NEED_SECOND_SWEEP:
                            // Console.WriteLine("第二次登记，请按手指");
                            labInfo.Text = "第二次登记，请按手指";
                            labInfo.Refresh();
                            break;
                        case FpSdk.RTC_NEED_THIRD_SWEEP:
                            //Console.WriteLine("第三次登记，请按手指");
                            BeginInvoke(new Action(() =>
                            {
                                labInfo.Text = "第三次登记，请按手指";
                                labInfo.Refresh();
                            }));
                            break;
                        case FpSdk.RTC_NEED_RELEASE_FINGER:
                            //Console.WriteLine("请离开手指");
                            BeginInvoke(new Action(() =>
                            {
                                labInfo.Text = "请离开手指";
                                labInfo.Refresh();
                            }));
                            break;
                        case FpSdk.RTC_BAD_QUALITY:
                            // Console.WriteLine("指纹质量差,请重试");
                            BeginInvoke(new Action(() =>
                            {
                                labInfo.Text = "指纹质量差,请重试";
                                labInfo.Refresh();
                            }));
                            break;
                        case FpSdk.RTC_DUPLICATION_ID:
                            //Console.WriteLine("指纹重复,该指纹已登记,登记位置ID为:{0}", nData);
                            BeginInvoke(new Action(() =>
                            {
                                labInfo.Text = string.Format("指纹重复,该指纹已登记,登记位置ID为:{0}", nData);
                                labInfo.Refresh();
                            }));
                            break;
                        default:
                            //Console.WriteLine("UNKNOW");
                            BeginInvoke(new Action(() =>
                            {
                                labInfo.Text = "UNKNOW";
                                labInfo.Refresh();
                            }));
                            break;
                    }
                });


                if (nRet == FpSdk.RTC_SUCCESS)
                {
                    emptyBM = emptyId;
                    BeginInvoke(new Action(() =>
                    {
                        labInfo.Text = string.Format("登记指纹成功 id为:{0}", emptyId);
                        labInfo.Refresh();
                    }));
                    //Console.WriteLine("登记指纹成功 id为:{0}", emptyId);
                }
                else if (nRet == FpSdk.RTC_DEVICE_NOT_FOUND)
                {
                    BeginInvoke(new Action(() =>
                    {
                        labInfo.Text = string.Format("找不到指纹仪");
                    }));
                    //Console.WriteLine("找不到指纹仪");
                }
                else
                {
                    BeginInvoke(new Action(() =>
                    {
                        labInfo.Text = string.Format("登记指纹失败，错误码：{0}  \n", nRet);
                    }));
                    //Console.WriteLine("登记指纹失败，错误码：{0}  \n", nRet);
                }
            }
            else
            {
                BeginInvoke(new Action(() =>
                {
                    labInfo.Text = string.Format("找不到指纹仪");
                }));
                // Console.WriteLine("找不到指纹仪");
            }
        }
        public static void GetBitmapOnePress()
        {
            byte[] bitmapBuffer = new byte[FpSdk.MAX_BITMAP_BASE64_SIZE + 1];
            int readByteCount = 0;

            if (FpSdk.Init() == FpSdk.RTC_SUCCESS)
            {
                Console.WriteLine("请按压指纹仪，以获取指纹图像");
                int nRet = -1;
                nRet = FpSdk.GetBitmapOnePress(bitmapBuffer, out readByteCount);
                if (nRet == FpSdk.RTC_SUCCESS)
                {
                    string bitmapBase64 = System.Text.Encoding.Default.GetString(bitmapBuffer, 0, readByteCount);
                    // 指纹特征和指纹设备的数据
                    Console.WriteLine("获取指纹图像成功,指纹数据(base64)：" + bitmapBase64);

                    helper_saveBitmap(bitmapBase64);
                    Console.WriteLine("\n@@位图图像已经保存到fingerprint.bmp");
                }
                else if (nRet == FpSdk.RTC_BAD_QUALITY)
                {
                    Console.WriteLine("指纹质量差");
                }
                else if (nRet == FpSdk.RTC_DEVICE_NOT_FOUND)
                {
                    Console.WriteLine("找不到指纹仪");
                }
                else
                {
                    Console.WriteLine("获取指纹数据失败, 错误码：{0}", nRet);
                }
            }
            else
            {
                Console.WriteLine("找不到指纹仪");
            }
        }

        public   void startIdentify()
        {
             

            if (FpSdk.Init() == FpSdk.RTC_SUCCESS)
            {

                //Console.WriteLine("请按压指纹仪，以识别指纹");
                labInfo.Text = "请按压指纹仪，以识别指纹";
                labInfo.Refresh();
                int featureId = -1;
                int nRet = FpSdk.Identify(out featureId);
                if (nRet == FpSdk.RTC_SUCCESS)
                {
                    // Console.WriteLine("指纹识别成功 id为:{0}", featureId);
                    labInfo.Text = string.Format("指纹识别成功 id为:{0}", featureId);
                    labInfo.Refresh();
                }
                else if (nRet == FpSdk.RTC_BAD_QUALITY)
                {
                    //Console.WriteLine("指纹质量差");
                    labInfo.Text = "指纹质量差";
                    labInfo.Refresh();
                }
                else if (nRet == FpSdk.RTC_DEVICE_NOT_FOUND)
                {
                    //Console.WriteLine("找不到指纹仪");
                    labInfo.Text = "找不到指纹仪";
                    labInfo.Refresh();
                }
                else
                {
                    //Console.WriteLine("指纹识别失败，错误码：{0}", nRet);
                    labInfo.Text =string.Format("指纹识别失败，错误码：{0}", nRet);
                    labInfo.Refresh();
                }

            }
            else
            {
                labInfo.Text = string.Format("找不到指纹仪");
                labInfo.Refresh();
                // Console.WriteLine("找不到指纹仪");
            }

            FpSdk.UnInit();
        }

        public static void GetFeatureOnePress()
        {


            byte[] featureBuffer = new byte[FpSdk.FP_FEATURE_SIZE + 1];
            int readByteCount = FpSdk.FP_FEATURE_SIZE + 1;

            if (FpSdk.Init() == FpSdk.RTC_SUCCESS)
            {
                Console.WriteLine("请按压指纹仪，以获取指纹特征");
                int nRet = -1;
                nRet = FpSdk.GetFeatureOnePress(featureBuffer, out readByteCount);
                if (nRet == FpSdk.RTC_SUCCESS)
                {
                    string featureBase64 = Convert.ToBase64String(featureBuffer);
                    // 指纹特征和指纹设备的数据
                    Console.WriteLine("获取指纹数据成功,指纹数据(base64)：" + featureBase64);
                }
                else if (nRet == FpSdk.RTC_BAD_QUALITY)
                {
                    Console.WriteLine("指纹质量差");
                }
                else if (nRet == FpSdk.RTC_DEVICE_NOT_FOUND)
                {
                    Console.WriteLine("找不到指纹仪");
                }
                else
                {
                    Console.WriteLine("获取指纹数据失败, 错误码：{0}", nRet);
                }
            }
            else
            {
                Console.WriteLine("找不到指纹仪");
            }

        }

        public static void getEnrollCount()
        {


            int nRet = FpSdk.Init();
            if (nRet != FpSdk.RTC_SUCCESS)
            {
                Console.WriteLine("指纹仪连接失败");
                return;
            }

            int featureCount = -1;

            nRet = FpSdk.GetFeatureCount(out featureCount);

            if (nRet == FpSdk.RTC_SUCCESS)
            {
                Console.WriteLine("登记指纹数量：" + featureCount);
            }
            else
            {
                Console.WriteLine("登记数量查询失败, 错误码：{0}", nRet);
            }

            FpSdk.UnInit();
        }

        public static void clearOneFeature()
        {


            int nRet = FpSdk.Init();
            if (nRet != FpSdk.RTC_SUCCESS)
            {
                Console.WriteLine("指纹仪连接失败");
                return;
            }

            int idToDelete = helper_readInt("请输入要删除的指纹ID:");

            nRet = FpSdk.ClearFeature(idToDelete);
            if (nRet == FpSdk.RTC_SUCCESS)
            {
                Console.WriteLine("指纹清除成功, 已删除ID为: " + idToDelete);
            }
            else
            {
                Console.WriteLine("指纹清除失败, 错误码: {0} ", nRet);
            }

            FpSdk.UnInit();


        }

        public static int helper_readInt(string info)
        {
 
            if (info.Length > 0)
            {
                Console.Write(info);
            }

            try
            {
                string inputContent = Console.ReadLine();
                return Convert.ToInt32(inputContent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return -1;
        }


        static int helper_saveBitmap(string bitmapBase64)
        {
            FileStream fs = new FileStream("fingerprint.bmp", FileMode.Create);

            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(Convert.FromBase64String(bitmapBase64));
            bw.Flush();
            bw.Close();

            return 0;

        }

        private void frmZWcs_Shown(object sender, EventArgs e)
        {
            //startEnroll();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            startEnroll();


            ///backgroundWorker1.RunWorkerAsync();
         

        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            if (FpSdk.Init() == FpSdk.RTC_SUCCESS)
            {
                FpSdk.UnInit();
            }
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            saveZW(  );


        }
        private void saveZW()
        {
            if (FpSdk.Init() == FpSdk.RTC_SUCCESS)
            {


                int featureId = -1;
                byte[] redata=null;
                int nRet = FpSdk.ReadFeature(emptyBM,   redata, out featureId);
                if (nRet == FpSdk.RTC_SUCCESS)
                {
                    // Console.WriteLine("指纹识别成功 id为:{0}", featureId);
                    labInfo.Text = string.Format("指纹识别成功 id为:{0}", featureId);
                    labInfo.Refresh();
                }
                else if (nRet == FpSdk.RTC_DEVICE_NOT_FOUND)
                {
                    
                    labInfo.Text = "找不到指纹仪";
                    labInfo.Refresh();
                }
                else if (nRet == FpSdk.RTC_INVALID_TMPL_NO)
                {
                    //Console.WriteLine("找不到指纹仪");
                    labInfo.Text = "id 无效";
                    labInfo.Refresh();
                }
                else if (nRet == FpSdk.RTC_FP_CANCEL)
                {
                    //Console.WriteLine("找不到指纹仪");
                    labInfo.Text = "该操作已被取消";
                    labInfo.Refresh();
                }
                else if (nRet == FpSdk.RTC_FAIL)
                {
                    //Console.WriteLine("找不到指纹仪");
                    labInfo.Text = "发生异常";
                    labInfo.Refresh();
                }
                else
                {
                    //Console.WriteLine("指纹识别失败，错误码：{0}", nRet);
                    labInfo.Text = string.Format("指纹识别失败，错误码：{0}", nRet);
                    labInfo.Refresh();
                }

            }
            else
            {
                labInfo.Text = string.Format("找不到指纹仪");
                labInfo.Refresh();
                // Console.WriteLine("找不到指纹仪");
            }

            FpSdk.UnInit();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            startEnroll();
        }
    }
}
