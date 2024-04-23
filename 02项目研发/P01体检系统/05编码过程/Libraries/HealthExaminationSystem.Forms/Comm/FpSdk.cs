using System.Runtime.InteropServices;

namespace natrotech
{
    public class FpSdk
    {
        // global defines -------------------------------------------------------
        public const int FP_FEATURE_SIZE = (570);


        // return codes ---------------------------------------------------------
        public const int RTC_SUCCESS = (0);
        public const int RTC_LOAD_LIB_FAIL = (-1);
        public const int RTC_LOAD_FUNC_FAIL = (-2);
        public const int RTC_JNI_ERROR = (-10);
        public const int RTC_LIB_NOT_LOAD = (-20);
        public const int RTC_MEMORY_ERROR = (-30);

        // return values -----------------------------------------------------------
        public const int RTC_FAIL = 0x01;
        public const int RTC_NOT_SUPPORTED = 0x03;
        public const int RTC_DEVICE_NOT_OPENED = 0x04;
        public const int RTC_DEVICE_NOT_CLOSED = 0x05;
        public const int RTC_DEVICE_NOT_FOUND = 0x06;
        public const int RTC_DEVICE_BUSY = 0x07;
        public const int RTC_COMMUNICATION_FAIL = 0x08;

        public const int RTC_VERIFY = 0x11;
        public const int RTC_IDENTIFY = 0x12;
        public const int RTC_TMPL_EMPTY = 0x13;
        public const int RTC_TMPL_NOT_EMPTY = 0x14;
        public const int RTC_ALL_TMPL_EMPTY = 0x15;
        public const int RTC_EMPTY_ID_NOEXIST = 0x16;
        public const int RTC_BROKEN_ID_NOEXIST = 0x17;
        public const int RTC_INVALID_TMPL_DATA = 0x18;
        public const int RTC_DUPLICATION_ID = 0x19;
        public const int RTC_TOO_FAST = 0x20;
        public const int RTC_BAD_QUALITY = 0x21;
        public const int RTC_SMALL_LINES = 0x22;
        public const int RTC_TIME_OUT = 0x23;
        public const int RTC_NOT_AUTHORIZED = 0x24;
        public const int RTC_GENERALIZE = 0x30;
        public const int RTC_COM_TIMEOUT = 0x40;
        public const int RTC_FP_CANCEL = 0x41;
        public const int RTC_INTERNAL = 0x50;
        public const int RTC_MEMORY = 0x51;
        public const int RTC_EXCEPTION = 0x52;
        public const int RTC_INVALID_TMPL_NO = 0x60;
        public const int RTC_INVALID_PARAM = 0x70;
        public const int RTC_NO_RELEASE = 0x71;
        public const int RTC_INVALID_OPERATION_MODE = 0x72;
        public const int RTC_NOT_SET_PWD = 0x74;
        public const int RTC_FP_NOT_DETECTED = 0x75;
        public const int RTC_ADJUST_SENSOR = 0x76;
        // 代码强制超时
        public const int RTC_FORCE_TIMEOUT = 0x77;
        // 指纹设备被占用
        public const int RTC_DEVICE_TOKEN = 0x78;
        // 找不到手指
        public const int RTC_NOT_FOUND_FINGER = 0x79;

        public const int RTC_NEED_FIRST_SWEEP = 0xFFF1;
        public const int RTC_NEED_SECOND_SWEEP = 0xFFF2;
        public const int RTC_NEED_THIRD_SWEEP = 0xFFF3;
        public const int RTC_NEED_RELEASE_FINGER = 0xFFF4;

        // MT Return Values
        public const int RTC_NEED_EXPAND_ENROLL = 0xFFF9;
        public const int RTC_NEED_MORE_EXPAND_ENROLL = 0xFFFA;
        public const int RTC_CONTINUE_INPUT_FINGER = 0xFFFB;
        public const int RTC_NEED_CORRECT_FP = 0xFFFC;
        public const int RTC_NEED_MOVE_FP = 0xFFFD;

        public const int RTC_TEMPLATE_NOT_EMPTY = 0x01;
        public const int RTC_TEMPLATE_EMPTY = 0x00;

        public const int RTC_DETECT_FINGER = 0x01;
        public const int RTC_NO_DETECT_FINGER = 0x00;

        public const int RTC_DOWNLOAD_SUCCESS = 0xA1;


        // parameter defines -------------------------------------------------------
        public const int MAX_RECORD_COUNT = 5000;
        public const int MAX_RECORD_SIZE = 2048;
        public const int MAX_IMAGE_SIZE = 75000;
        public const int MAX_BITMAP_BASE64_SIZE = 101438;
        public const int SZ_TEMPLATE_SIZE = 570;
        public const int SZ_UID_SIZE = 8;


        private const string FpSdkDll = @"Dll\FpSdk.dll";

        /// <summary>
        /// 连接指纹仪，在其他操作前调用此函数连接指纹仪
        /// </summary>
        /// <returns></returns>
        [DllImport(FpSdkDll, EntryPoint = "FP_Init", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int Init();

        /// <summary>
        /// 释放指纹仪
        /// </summary>
        /// <returns></returns>
        [DllImport(FpSdkDll, EntryPoint = "FP_Uninit", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int UnInit();

        /// <summary>
        /// 取消当前命令
        /// </summary>
        /// <returns></returns>
        [DllImport(FpSdkDll, EntryPoint = "FP_Cancel", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int Cancel();

        /// <summary>
        /// 判断当前电脑是否有连接指纹仪
        /// </summary>
        /// <returns></returns>
        [DllImport(FpSdkDll, EntryPoint = "FP_HasDevice", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int HasDevice();



        /// <summary>
        /// 登记指纹
        /// </summary>
        /// <param name="id">Id: 指纹仪中存储指纹特征数据的位置编号。 1000枚容量的指纹仪，id取值范围从1到1000。</param>
        /// <param name="enrollCallback">callback：是回调函数，在登记过程中回调，方便操作界面给用户做出相应的提示。详解见下方的表格</param>
        /// <returns></returns>
        [DllImport(FpSdkDll, EntryPoint = "FP_EnrollFeature", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int EnrollFeature(int id, EnrollCallback enrollCallback);

        /// <summary>
        /// 先清除，再登记指纹
        /// </summary>
        /// <param name="id">Id: 指纹仪中存储指纹特征数据的位置编号。 1000枚容量的指纹仪，id取值范围从1到1000。</param>
        /// <param name="enrollCallback">callback：是回调函数，在登记过程中回调，方便操作界面给用户做出相应的提示。详解见下方的表格</param>
        /// <returns></returns>
        [DllImport(FpSdkDll, EntryPoint = "FP_ChangeFeature", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int ChangeFeature(int id, EnrollCallback enrollCallback);

        /// <summary>
        /// 从指纹仪读取指定id的指纹特征
        /// </summary>
        /// <param name="id">指纹仪中存储指纹特征数据的位置编号。1000枚容量的指纹仪，id取值范围从1到1000。</param>
        /// <param name="p_pTemplateBuf">接收指纹特征数据的内存块。</param>
        /// <param name="len">内存块p_pTemplateBuf的长度。</param>
        /// <returns>
        /// RTC_SUCCESS：操作成功
        /// RTC_DEVICE_NOT_FOUND：找不到指纹仪
        /// RTC_INVALID_TMPL_NO：id无效
        /// RTC_FP_CANCEL：该操作已被取消
        /// </returns>
        [DllImport(FpSdkDll, EntryPoint = "FP_ReadFeature", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int ReadFeature(int id, byte[] p_pTemplateBuf, out int len);


        /// <summary>
        /// 把指纹特征写入指纹仪
        /// </summary>
        /// <param name="id">指纹仪中存储指纹特征数据的位置编号。1000枚容量的指纹仪，id取值范围从1到1000。</param>
        /// <param name="featureBuf">存储指纹特征数据的数组。</param>
        /// <param name="len">数组featureBuf的长度。</param>
        /// <returns>
        /// RTC_SUCCESS：操作成功
        /// RTC_DEVICE_NOT_FOUND：找不到指纹仪
        /// RTC_INVALID_TMPL_NO：id无效
        /// RTC_FP_CANCEL：该操作已被取消
        /// </returns>
        [DllImport(FpSdkDll, EntryPoint = "FP_WriteFeature", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int WriteFeature(int id, byte[] featureBuf, int len);

        /// <summary>
        /// 清除指定指纹
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DllImport(FpSdkDll, EntryPoint = "FP_ClearFeature", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int ClearFeature(int id);

        /// <summary>
        /// 清除所有指纹
        /// </summary>
        /// <returns></returns>
        [DllImport(FpSdkDll, EntryPoint = "FP_ClearAllFeature", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int ClearAllFeature();

        /// <summary>
        /// 按一次指纹，获取指纹特征数据
        /// </summary>
        /// <param name="p_pTemplateBuf"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        [DllImport(FpSdkDll, EntryPoint = "FP_GetFeatureOnePress", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int GetFeatureOnePress(byte[] p_pTemplateBuf, out int len);


        /// <summary>
        /// 按一次指纹，获取指纹图像
        /// </summary>
        /// <param name="bitmap_buffer">位图图像，直接把bitmap_buffer经过base64解码后保存到文件，文件名后缀为bmp，即可得到位图文件。</param>
        /// <param name="image_size">位图大小</param>
        /// <returns></returns>
        [DllImport(FpSdkDll, EntryPoint = "FP_GetBitmapOnePress", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int GetBitmapOnePress(byte[] bitmap_buffer, out int image_size);


        /// <summary>
        /// RTC_SUCCESS：操作成功
        /// RTC_DEVICE_NOT_FOUND：找不到指纹仪
        /// RTC_INVALID_TMPL_NO：id无效
        /// RTC_FP_CANCEL：该操作已被取消
        /// RTC_TIME_OUT：等待超时
        /// RTC_DUPLICATION_ID：登记的指纹已经存在，不允许重复登记
        /// RTC_FAIL：发生异常
        /// </summary>
        /// <param name="nCode">
        /// RTC_NEED_FIRST_SWEEP：请一次采集，需要用户按压传感器
        /// RTC_NEED_SECOND_SWEEP：请二次采集，需要用户按压传感器
        /// RTC_NEED_THIRD_SWEEP：请三次采集，需要用户按压传感器
        /// RTC_NEED_RELEASE_FINGER：需要用户的手指离开传感器
        /// RTC_BAD_QUALITY：指纹质量低，需要用户重新采集
        /// RTC_DUPLICATION_ID：采集完指纹准备保存时，发现本次登记的指纹已经登记过，存在重复的指纹。
        /// nData：当nCode为RTC_DUPLICATION_ID时，返回与本次登记指纹重复的id
        /// </param>
        /// <param name="nData"></param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void EnrollCallback(int nCode, int nData);



        /// <summary>
        /// 识别指纹
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [DllImport(FpSdkDll, EntryPoint = "FP_Identify", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int Identify(out int id);



        /// <summary>
        /// 获取指纹仪的一个没有登记指纹的空id，取最小值
        /// </summary>
        /// <param name="id">p_nEmptyID：用于接收返回的空id</param>
        /// <returns>
        /// RTC_SUCCESS：操作成功
        /// RTC_DEVICE_NOT_FOUND：找不到指纹仪
        /// RTC_FP_CANCEL：该操作已被取消
        /// RTC_FAIL：发生异常
        /// </returns>
        [DllImport(FpSdkDll, EntryPoint = "FP_GetEmptyId", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int GetEmptyId(out int id);


        /// <summary>
        /// 获取所有已登记指纹
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [DllImport(FpSdkDll, EntryPoint = "FP_GetFeatureCount", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int GetFeatureCount(out int count);


        /// <summary>
        /// 获取指纹设备的唯一编号
        /// </summary>
        /// <param name="uidBuf"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        [DllImport(FpSdkDll, EntryPoint = "FP_GetDeviceUID", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int GetDeviceUID(byte[] uidBuf, out int len);


        /// <summary>
        /// 开启指纹仪的指示灯
        /// </summary>        
        /// <returns></returns>
        [DllImport(FpSdkDll, EntryPoint = "FP_LedOn", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int LedOn();

        /// <summary>
        /// 关闭指纹仪的指示灯
        /// </summary>        
        /// <returns></returns>
        [DllImport(FpSdkDll, EntryPoint = "FP_LedOff", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int LedOff();

        /// <summary>
        /// 获取指纹仪的超时时间，单位秒
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        [DllImport(FpSdkDll, EntryPoint = "FP_GetTimeout", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int GetTimeout(out int val);

        /// <summary>
        /// 设定指纹仪的超时时间，单位秒
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        [DllImport(FpSdkDll, EntryPoint = "FP_SetTimeout", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int SetTimeout(int val);

        [DllImport(FpSdkDll, EntryPoint = "FP_GetAutoLearn", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int GetAutoLearn(out int val);

        [DllImport(FpSdkDll, EntryPoint = "FP_SetAutoLearn", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int SetAutoLearn(int val);

        [DllImport(FpSdkDll, EntryPoint = "FP_GetSecLevel", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int GetSecLevel(out int val);

        [DllImport(FpSdkDll, EntryPoint = "FP_SetSecLevel", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int SetSecLevel(int val);

        [DllImport(FpSdkDll, EntryPoint = "FP_GetDuplicateCheck", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int GetDuplicateCheck(out int val);

        [DllImport(FpSdkDll, EntryPoint = "FP_SetDuplicateCheck", CallingConvention = CallingConvention.Cdecl, SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int SetDuplicateCheck(int val);


    }
}
