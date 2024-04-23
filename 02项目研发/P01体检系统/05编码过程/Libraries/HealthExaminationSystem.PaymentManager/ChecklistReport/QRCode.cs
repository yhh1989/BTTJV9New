using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.QrCode;

namespace Sw.Hospital.HealthExaminationSystem.PaymentManager.ChecklistReport
{
   public class QRCode
    {
        public string er(string name)
        {
          
           //1先设置二维码的规格
           QrCodeEncodingOptions qr = new QrCodeEncodingOptions();
            qr.CharacterSet = "UTF-8";//设置编码格式,否则会乱码
            qr.Height = 150;
            qr.Width = 150;
            qr.Margin = 1;//设置二维码图片周围空白边距

            //2生成条形码图片保存
            BarcodeWriter wr = new BarcodeWriter();
            wr.Format = BarcodeFormat.QR_CODE;//二维码
            wr.Options = qr;//指定格式
            Bitmap bitmap = wr.Write(name);//存放二维码
                                           //设置图片的路径
            var url = AppDomain.CurrentDomain.BaseDirectory + @"QRCode\" + Guid.NewGuid().ToString() + ".jpg";
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"QRCode\"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"QRCode\");
            }
            //进行保存
            
            string qq = DateTime.Now.ToString("yyyyMMddHHmmss");
            bitmap.Save(url, ImageFormat.Jpeg);
            return url;
        }
 
 
    }
}
