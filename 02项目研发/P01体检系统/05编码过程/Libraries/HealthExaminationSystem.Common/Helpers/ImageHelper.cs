using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 图片帮助类
    /// </summary>
    public class ImageHelper
    {
        /// <summary>
        /// 默认图片的 Base64 表示形式
        /// </summary>
        /// <remarks>
        /// data:image/gif;base64,
        /// </remarks>
        public static string DefaultImageBase64 =>
            "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";

        /// <summary>
        /// 从网络位置下载图片流
        /// </summary>
        /// <param name="url">图片网络地址</param>
        /// <returns>读取到的流</returns>
        public static Stream DownloadUriImageStream(Uri url)
        {
            var webRequest = WebRequest.Create(url);
            var webResponse = webRequest.GetResponse();
            return webResponse.GetResponseStream();
        }

        /// <summary>
        /// 从网络位置获取图片流
        /// <para>如果未找到图片则使用默认图片替代</para>
        /// </summary>
        /// <param name="url">图片网络地址</param>
        /// <returns>获取到的图片流或默认图片流</returns>
        public static Stream GetUriImageStream(Uri url)
        {
            try
            {
                return DownloadUriImageStream(url);
            }
            catch (WebException e)
            {
                if (e.Status != WebExceptionStatus.ProtocolError)
                {
                    throw;
                }

                if (!(e.Response is HttpWebResponse httpWebResponse))
                {
                    throw;
                }

                if (httpWebResponse.StatusCode != HttpStatusCode.NotFound)
                {
                    throw;
                }

                return GetDefaultImageStream();
            }
        }

        /// <summary>
        /// 从网络位置获取图片
        /// <para>如果未找到图片则使用默认图片替代</para>
        /// </summary>
        /// <param name="url">图片网络地址</param>
        /// <returns>获取到的图片流或默认图片流</returns>
        public static Image GetUriImage(Uri url)
        {
            using (var stream = GetUriImageStream(url))
            {
                try
                {
                    return Image.FromStream(stream);
                }
                catch (ArgumentException)
                {
                    return GetDefaultImage();
                }
            }
        }

        /// <summary>
        /// 获取默认图片流
        /// </summary>
        /// <returns>默认图片流</returns>
        public static Stream GetDefaultImageStream()
        {
            var bytes = Convert.FromBase64String(DefaultImageBase64);
            return new MemoryStream(bytes);
        }

        /// <summary>
        /// 获取默认图片
        /// </summary>
        /// <returns>默认图片</returns>
        public static Image GetDefaultImage()
        {
            using (var stream = GetDefaultImageStream())
            {
                return Image.FromStream(stream);
            }
        }

        /// <summary>
        /// 从网络位置获取图片流
        /// <para>如果未找到图片则使用默认图片替代</para>
        /// </summary>
        /// <param name="url">图片网络地址</param>
        /// <param name="stream">获取到的图片流</param>
        /// <returns>如果是网络图片流则为 True，否则为 False</returns>
        public static bool TryGetUriImageStream(Uri url, out Stream stream)
        {
            try
            {
                stream = DownloadUriImageStream(url);
                return true;
            }
            catch (WebException e)
            {
                if (e.Status != WebExceptionStatus.ProtocolError)
                {
                    throw;
                }

                if (!(e.Response is HttpWebResponse httpWebResponse))
                {
                    throw;
                }

                if (httpWebResponse.StatusCode != HttpStatusCode.NotFound)
                {
                    throw;
                }

                stream = GetDefaultImageStream();
                return false;
            }
        }

        /// <summary>
        /// 从网络位置获取图片
        /// <para>如果未找到图片则使用默认图片替代</para>
        /// </summary>
        /// <param name="url">图片网络地址</param>
        /// <param name="image">获取到的图片</param>
        /// <returns>如果是网络图片则为 True，否则为 False</returns>
        public static bool TryGetUriImage(Uri url, out Image image)
        {
            if (TryGetUriImageStream(url, out var stream))
            {
                using (stream)
                {
                    try
                    {
                        image = Image.FromStream(stream);
                        return true;
                    }
                    catch (ArgumentException)
                    {
                        image = GetDefaultImage();
                        return false;
                    }
                }
            }

            using (stream)
            {
                image = Image.FromStream(stream);
                return false;
            }
        }

        /// <summary>
        /// 读取byte[]并转化为图片
        /// </summary>
        /// <param name="bytes">byte[]</param>
        /// <returns>Image</returns>
        public static Image GetImageByBytes(byte[] bytes)
        {
            Image photo = null;
            using (var ms = new MemoryStream(bytes))
            {
                //ms.Write(bytes, 0, bytes.Length);
                photo = Image.FromStream(ms, true);
            }

            return photo;
        }

        /// <summary>
        /// 将 Image 转化为二进制
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] GetByteImage(Image img)
        {
            if (img != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var bmp = new Bitmap(img))
                    {
                        bmp.Save(memoryStream, ImageFormat.Png);
                        return memoryStream.GetBuffer();
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 合并多张图片为一张
        /// </summary>
        /// <param name="width">新图片宽度</param>
        /// <param name="height">新图片高度</param>
        /// <param name="imageSize">每行上放置图片个数</param>
        /// <param name="images">要合并的图片列表</param>
        /// <returns>合并后的图片</returns>
        public static Image DrawImage(int width, int height, int imageSize = 2, params Image[] images)
        {
            var number = (int)Math.Ceiling(images.Length / (decimal)imageSize);
            var imageHeight = height / number;
            var imageWidth = width / imageSize;
            var bitmap = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                for (var row = 0; row < number; row++)
                {
                    for (var column = 0; column < imageSize; column++)
                    {
                        var index = row * imageSize + column;
                        if (index >= images.Length)
                        {
                            continue;
                        }

                        using (var bit = new Bitmap(images[index]))
                        {
                            var x = column * imageWidth;
                            var y = row * imageHeight;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                            graphics.DrawImage(bit, x, y, imageWidth, imageHeight);
                        }
                    }
                }
            }

            return bitmap;
        }
        /// <summary>
        /// 等比缩放图片
        /// </summary>
        /// <param name="pathImageFrom">图片地址（服务器绝对路径）</param>
        /// <param name="maxSize">宽和高最大值</param>
        public static Image ChangePicSize(Image imageFrom, int maxSize)
        {          
             
                 
                // 源图宽度及高度   
                int imageFromWidth = imageFrom.Width;
                int imageFromHeight = imageFrom.Height;
                // 生成的缩略图实际宽度及高度  
                int bitmapWidth = imageFromWidth;
                int bitmapHeight = imageFromHeight;
                if (imageFromWidth > maxSize || imageFromHeight > maxSize)
                {
                    if (imageFromWidth > imageFromHeight)
                    {
                        bitmapWidth = maxSize;
                        bitmapHeight = imageFromHeight * maxSize / imageFromWidth;
                    }
                    else
                    {
                        bitmapWidth = imageFromWidth * maxSize / imageFromHeight;
                        bitmapHeight = maxSize;
                    }
                }
                // 创建画布   
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(bitmapWidth, bitmapHeight);
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp);
                // 用白色清空   
                g.Clear(System.Drawing.Color.White);
                // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                // 指定高质量、低速度呈现。   
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                // 在指定位置并且按指定大小绘制指定的 Image 的指定部分。   
                g.DrawImage(imageFrom, new System.Drawing.Rectangle(0, 0, bitmapWidth, bitmapHeight), new System.Drawing.Rectangle(0, 0, imageFromWidth, imageFromHeight), System.Drawing.GraphicsUnit.Pixel);

                imageFrom.Dispose();
          
            //释放资源     
            //bmp.Dispose();
            //g.Dispose();
            return bmp;


        }
        /// <summary>
        /// 合并多张图片为一张
        /// </summary>
        /// <param name="width">新图片宽度</param>
        /// <param name="height">新图片高度</param>
        /// <param name="imageSize">每行上放置图片个数</param>
        /// <param name="images">要合并的图片列表</param>
        /// <returns>合并后的图片</returns>
        public static Image DrawImageReport(int width, int height,  params Image[] images)
        {
            int hbk = 0;
            int wbk = 0;
            int imageSize = 2;           
            if (images.Length > 4)
            {
                imageSize = 3;
            }
            else if (images.Length > 9)
            {
                imageSize = 4;
            }
           var number = (int)Math.Ceiling(images.Length / (decimal)imageSize);
            var imageHeight = height / number;
            var imageWidth = width / imageSize;
            var bitmap = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                for (var row = 0; row < number; row++)
                {
                    for (var column = 0; column < imageSize; column++)
                    {
                        var index = row * imageSize + column;
                        if (index >= images.Length)
                        {
                            continue;
                        }
                        using (var bit = new Bitmap(images[index]))
                        {

                            var x = column * imageWidth + wbk;
                            var y = row * imageHeight + hbk;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;                         
                            graphics.SmoothingMode = SmoothingMode.HighQuality;                           
                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                            graphics.DrawImage(bit, x, y, imageWidth, imageHeight);
                            //graphics.

                        }
                        wbk = wbk + 1;
                    }
                    hbk = hbk + 1;
                }
            }
            foreach (var iamgels in images)
            {
                if (iamgels != null)
                {
                    iamgels.Dispose();
                }
            }
            return bitmap;
        }
    }
}