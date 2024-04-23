using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Web.Mvc.Authorization;
using Sw.Hospital.HealthExaminationSystem.Application.Picture;
using Sw.Hospital.HealthExaminationSystem.Application.Picture.Dto;
using Sw.Hospital.HealthExaminationSystem.UserException.Others;

namespace Sw.Hospital.HealthExaminationSystem.Web.Controllers
{
    [AbpMvcAuthorize]
    public class PictureController : MyProjectControllerBase
    {
        private readonly IPictureAppService _pictureAppService;

        public PictureController(IPictureAppService pictureAppService)
        {
            _pictureAppService = pictureAppService;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file">文件对象</param>
        /// <param name="belong">文件归属于</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Uploading(HttpPostedFileWrapper file, string belong, Guid? id = null)
        {
            //HttpPostedFile
            //HttpPostedFileBase
            //HttpPostedFileWrapper
            //Directory
            var baseDirectory = Server.MapPath("~/");
            var fileDirectory = Path.Combine(baseDirectory, "Upload");
            var pictureDirectory = Path.Combine(fileDirectory, "Picture");
            var belongDirectory = Path.Combine(pictureDirectory, belong);
            var date = DateTime.Now.ToString("yyyyMM");
            var dateDirectory = Path.Combine(belongDirectory, date);
            if (!Directory.Exists(dateDirectory))
                Directory.CreateDirectory(dateDirectory);
            var createPictureDto = new CreateOrUpdatePictureDto
            {
                Id = Guid.NewGuid(),
                Belong = belong
            };
            if (id != null && id != Guid.Empty)
            {
                createPictureDto.Id = id.Value;
            }
            var extension = Path.GetExtension(file.FileName);
            var fileName = Path.Combine(dateDirectory, $"{createPictureDto.Id}{extension}");
            file.SaveAs(fileName);
            GC.Collect();
            createPictureDto.RelativePath = fileName.Replace(baseDirectory, "");
            var thumbnail = Path.Combine(dateDirectory, "Thumbnail");
            if (!Directory.Exists(thumbnail))
                Directory.CreateDirectory(thumbnail);
            var thumbnailFileName = Path.Combine(thumbnail, $"{createPictureDto.Id}{extension}");
            using (var img = Image.FromFile(fileName))
            {
                if (img.Height <= 300 && img.Width <= 300)
                {
                    img.Save(thumbnailFileName);
                    createPictureDto.Thumbnail = thumbnailFileName.Replace(baseDirectory, "");
                }
                else
                {
                    var width = 300;
                    var height = 300;
                    if (img.Height > img.Width)
                        width = (int)(300d / img.Height * img.Width);
                    else
                        height = (int)(300d / img.Width * img.Height);

                    using (var img1 = img.GetThumbnailImage(width, height, () => false, IntPtr.Zero))
                    {
                        img1.Save(thumbnailFileName);
                        createPictureDto.Thumbnail = thumbnailFileName.Replace(baseDirectory, "");
                    }
                }
            } 

            try
            {
                var result = _pictureAppService.Create(createPictureDto);
                return Json(result);
            }
            catch (Exception)
            {
                System.IO.File.Delete(fileName);
                System.IO.File.Delete(thumbnailFileName);
            }

            return new EmptyResult();
        }

        /// <summary>
        /// 获取文件地址
        /// </summary>
        /// <param name="id">文件标识</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult GetUrl(Guid? id)
        {
            if (id.HasValue)
            {
                var result = _pictureAppService.GetById(new EntityDto<Guid> { Id = id.Value });
                if (result.RelativePath.StartsWith("http://"))
                {
                    return Json(result);
                }
                var uriBuilder = new UriBuilder(Request.Url ?? throw new InvalidOperationException())
                {
                    Path = result.RelativePath,
                    Query = string.Empty
                };
                result.RelativePath = uriBuilder.ToString();
                uriBuilder.Path = result.Thumbnail;
                result.Thumbnail = uriBuilder.ToString();
                return Json(result);
            }

            return new EmptyResult();
        }

        /// <summary>
        /// 更新文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="id">文件标识</param>
        /// <returns></returns>
        public ActionResult Update(HttpPostedFileWrapper file, Guid? id)
        {
            if (id.HasValue)
            {
                PictureDto result = null;
                try
                {
                    result = _pictureAppService.GetById(new EntityDto<Guid> { Id = id.Value });
                }
                catch (IdNotFoundExecption)
                {
                    return Uploading(file, "Lost", id);
                    //var value = new RouteValueDictionary {{"file", file}, {"belong", "Lost"}};
                    //return RedirectToAction("Uploading", "Picture", value);
                }
                var baseDirectory = Server.MapPath("~/");
                var fileNameOld = Path.Combine(baseDirectory, result.RelativePath);
                //var extensionOld = Path.GetExtension(fileNameOld);
                //var extension = Path.GetExtension(file.FileName);
                //var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileNameOld);
                //var fileDirectory = Path.GetDirectoryName(fileNameOld);
                //Debug.Assert(fileDirectory != null, nameof(fileDirectory) + " != null");
                //if (!Directory.Exists(fileDirectory))
                //{
                //    Directory.CreateDirectory(fileDirectory);
                //}

                //var fileName = Path.Combine(fileDirectory, $"{fileNameWithoutExtension}{extension}");
                //if (System.IO.File.Exists(fileName))
                //{
                //    System.IO.File.Delete(fileName);
                //}
                //var baseDirectory = Server.MapPath("~/");
                var fileDirectory = Path.Combine(baseDirectory, "Upload");
                var pictureDirectory = Path.Combine(fileDirectory, "Picture");
                var belongDirectory = Path.Combine(pictureDirectory, "CusPhotoBm");
                var date = DateTime.Now.ToString("yyyyMM");
                var dateDirectory = Path.Combine(belongDirectory, date);
                if (!Directory.Exists(dateDirectory))
                    Directory.CreateDirectory(dateDirectory);
                var extension = Path.GetExtension(file.FileName);
                var fileName = Path.Combine(dateDirectory, $"{id}{extension}");

                file.SaveAs(fileName);

                GC.Collect();
                var thumbnailFileNameOld = Path.Combine(baseDirectory, result.Thumbnail);
                //var thumbnailFileNameWithoutExtension = Path.GetFileNameWithoutExtension(thumbnailFileNameOld);
                //var thumbnailFileDirectory = Path.GetDirectoryName(thumbnailFileNameOld);
                //Debug.Assert(thumbnailFileDirectory != null, nameof(thumbnailFileDirectory) + " != null");
                //if (!Directory.Exists(thumbnailFileDirectory))
                //    Directory.CreateDirectory(thumbnailFileDirectory);

                //var thumbnailFileName = Path.Combine(thumbnailFileDirectory,
                //    $"{thumbnailFileNameWithoutExtension}{extension}");

                 
                var thumbnail = Path.Combine(dateDirectory, "Thumbnail");
                if (!Directory.Exists(thumbnail))
                    Directory.CreateDirectory(thumbnail);
                var thumbnailFileName = Path.Combine(thumbnail, $"{id}{extension}");

                //if (System.IO.File.Exists(thumbnailFileName))
                //{
                //    System.IO.File.Delete(thumbnailFileName);
                //}
                using (var img = Image.FromFile(fileName))
                {
                    if (img.Height <=300 && img.Width <= 300)
                    {
                        img.Save(thumbnailFileName);
                    }
                    else
                    {
                        //var width = 300;
                        //var height = 300;
                        //if (img.Height > img.Width)
                        //    width = (int)(300 / img.Height * img.Width);
                        //else
                        //    height = (int)(300 / img.Width * img.Height);
                        //using (var img1 = img.GetThumbnailImage(width, height, () => false, IntPtr.Zero))
                        //{
                        //    img1.Save(thumbnailFileName);
                        //}


                        var width = 300;
                        var height = 300;
                        if (img.Height > img.Width)
                            width = (int)(300d / img.Height * img.Width);
                        else
                            height = (int)(300d / img.Width * img.Height);

                        using (var img1 = img.GetThumbnailImage(width, height, () => false, IntPtr.Zero))
                        {
                            img1.Save(thumbnailFileName);
                            //createPictureDto.Thumbnail = thumbnailFileName.Replace(baseDirectory, "");
                        }

                    }
                }

                if (fileNameOld != fileName)
                {
                    var updatePictureDto = result.MapTo<CreateOrUpdatePictureDto>();
                    updatePictureDto.RelativePath = fileName.Replace(baseDirectory, "");
                    updatePictureDto.Thumbnail = thumbnailFileName.Replace(baseDirectory, "");
                    var newResult = _pictureAppService.Update(updatePictureDto);
                    try
                    {
                        if (System.IO.File.Exists(fileNameOld))
                            System.IO.File.Delete(fileNameOld);

                        if (System.IO.File.Exists(thumbnailFileNameOld))
                            System.IO.File.Delete(thumbnailFileNameOld);
                    }
                    catch (Exception ex)
                    {
 
                    }
                    return Json(newResult);
                }

                return Json(result);
            }

            return new EmptyResult();
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id">文件标识</param>
        /// <returns></returns>
        public ActionResult Delete(Guid? id)
        {
            if (id.HasValue)
            {
                var result = _pictureAppService.GetById(new EntityDto<Guid> { Id = id.Value });
                var baseDirectory = Server.MapPath("~/");
                var fileName = Path.Combine(baseDirectory, result.RelativePath);
                var thumbnailFileName = Path.Combine(baseDirectory, result.Thumbnail);
                System.IO.File.Delete(fileName);
                System.IO.File.Delete(thumbnailFileName);
                _pictureAppService.Delete(new EntityDto<Guid> { Id = id.Value });
                return new EmptyResult();
            }

            return new EmptyResult();
        }

        #region 用户签名
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file">文件对象</param>
        /// <param name="belong">文件归属于</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UploadingUser(HttpPostedFileWrapper file, string belong, Guid? id = null)
        {
            //HttpPostedFile
            //HttpPostedFileBase
            //HttpPostedFileWrapper
            //Directory
            var baseDirectory = Server.MapPath("~/");
            var fileDirectory = Path.Combine(baseDirectory, "Upload");
            var pictureDirectory = Path.Combine(fileDirectory, "Picture");
            var belongDirectory = Path.Combine(pictureDirectory, belong);
            var date = DateTime.Now.ToString("yyyyMM");
            var dateDirectory = Path.Combine(belongDirectory, date);
            if (!Directory.Exists(dateDirectory))
                Directory.CreateDirectory(dateDirectory);
            var createPictureDto = new CreateOrUpdatePictureDto
            {
                Id = Guid.NewGuid(),
                Belong = belong
            };
            if (id != null && id != Guid.Empty)
            {
                createPictureDto.Id = id.Value;
            }
            var extension = Path.GetExtension(file.FileName);
            var fileName = Path.Combine(dateDirectory, $"{createPictureDto.Id}{extension}");
            file.SaveAs(fileName);
            GC.Collect();
            createPictureDto.RelativePath = fileName.Replace(baseDirectory, "");
            var thumbnail = Path.Combine(dateDirectory, "Thumbnail");
            if (!Directory.Exists(thumbnail))
                Directory.CreateDirectory(thumbnail);
            var thumbnailFileName = Path.Combine(thumbnail, $"{createPictureDto.Id}{extension}");
            using (var img = Image.FromFile(fileName))
            {
                if (img.Height <= 300 && img.Width <= 300)
                {
                    img.Save(thumbnailFileName);
                    createPictureDto.Thumbnail = thumbnailFileName.Replace(baseDirectory, "");
                }
                else
                {
                    var width = 300;
                    var height = 300;
                    if (img.Height > img.Width)
                        width = (int)(300d / img.Height * img.Width);
                    else
                        height = (int)(300d / img.Width * img.Height);

                    using (var img1 = img.GetThumbnailImage(width, height, () => false, IntPtr.Zero))
                    {
                        img1.Save(thumbnailFileName);
                        createPictureDto.Thumbnail = thumbnailFileName.Replace(baseDirectory, "");
                    }
                }
            }

            try
            {
                var result = _pictureAppService.CreateUser(createPictureDto);
                return Json(result);
            }
            catch (Exception)
            {
                System.IO.File.Delete(fileName);
                System.IO.File.Delete(thumbnailFileName);
            }

            return new EmptyResult();
        }

        /// <summary>
        /// 获取文件地址
        /// </summary>
        /// <param name="id">文件标识</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult GetUrlUser(Guid? id)
        {
            if (id.HasValue)
            {
                var result = _pictureAppService.GetByIdUser(new EntityDto<Guid> { Id = id.Value });
                if (result.RelativePath.StartsWith("http://"))
                {
                    return Json(result);
                }
                var uriBuilder = new UriBuilder(Request.Url ?? throw new InvalidOperationException())
                {
                    Path = result.RelativePath,
                    Query = string.Empty
                };
                result.RelativePath = uriBuilder.ToString();
                uriBuilder.Path = result.Thumbnail;
                result.Thumbnail = uriBuilder.ToString();
                return Json(result);
            }

            return new EmptyResult();
        }

        /// <summary>
        /// 更新文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="id">文件标识</param>
        /// <returns></returns>
        public ActionResult UpdateUser(HttpPostedFileWrapper file, Guid? id)
        {
            if (id.HasValue)
            {
                PictureDto result = null;
                try
                {
                    result = _pictureAppService.GetByIdUser(new EntityDto<Guid> { Id = id.Value });
                }
                catch (IdNotFoundExecption)
                {
                    return Uploading(file, "Lost", id);
                    //var value = new RouteValueDictionary {{"file", file}, {"belong", "Lost"}};
                    //return RedirectToAction("Uploading", "Picture", value);
                }
                var baseDirectory = Server.MapPath("~/");
                 var fileNameOld = Path.Combine(baseDirectory, result.RelativePath);
                //var extensionOld = Path.GetExtension(fileNameOld);
                //var extension = Path.GetExtension(file.FileName);
                //var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileNameOld);
                //var fileDirectory = Path.GetDirectoryName(fileNameOld);
                //Debug.Assert(fileDirectory != null, nameof(fileDirectory) + " != null");
                //if (!Directory.Exists(fileDirectory))
                //{
                //    Directory.CreateDirectory(fileDirectory);
                //}

                //var fileName = Path.Combine(fileDirectory, $"{fileNameWithoutExtension}{extension}");

                var fileDirectory = Path.Combine(baseDirectory, "Upload");
                var pictureDirectory = Path.Combine(fileDirectory, "Picture");
                var belongDirectory = Path.Combine(pictureDirectory, "CusPhotoBm");
                var date = DateTime.Now.ToString("yyyyMM");
                var dateDirectory = Path.Combine(belongDirectory, date);
                if (!Directory.Exists(dateDirectory))
                    Directory.CreateDirectory(dateDirectory);
                var extension = Path.GetExtension(file.FileName);
                var fileName = Path.Combine(dateDirectory, $"{id}{extension}");

                //if (System.IO.File.Exists(fileName))
                //{
                //    System.IO.File.Delete(fileName);
                //}
                file.SaveAs(fileName);

                GC.Collect();
                var thumbnailFileNameOld = Path.Combine(baseDirectory, result.Thumbnail);
                //var thumbnailFileNameWithoutExtension = Path.GetFileNameWithoutExtension(thumbnailFileNameOld);
                //var thumbnailFileDirectory = Path.GetDirectoryName(thumbnailFileNameOld);
                //Debug.Assert(thumbnailFileDirectory != null, nameof(thumbnailFileDirectory) + " != null");
                //if (!Directory.Exists(thumbnailFileDirectory))
                //    Directory.CreateDirectory(thumbnailFileDirectory);
                //var thumbnailFileName = Path.Combine(thumbnailFileDirectory,
                //    $"{thumbnailFileNameWithoutExtension}{extension}");

                //if (System.IO.File.Exists(thumbnailFileName))
                //{
                //    System.IO.File.Delete(thumbnailFileName);
                //}
                var thumbnail = Path.Combine(dateDirectory, "Thumbnail");
                if (!Directory.Exists(thumbnail))
                    Directory.CreateDirectory(thumbnail);
                var thumbnailFileName = Path.Combine(thumbnail, $"{id}{extension}");
                using (var img = Image.FromFile(fileName))
                {
                    if (img.Height <= 300 && img.Width <= 300)
                    {
                        img.Save(thumbnailFileName);
                    }
                    else
                    {
                        //var width = 300;
                        //var height = 300;
                        //if (img.Height > img.Width)
                        //    width = (int)(300 / img.Height * img.Width);
                        //else
                        //    height = (int)(300 / img.Width * img.Height);
                        //using (var img1 = img.GetThumbnailImage(width, height, () => false, IntPtr.Zero))
                        //{
                        //    img1.Save(thumbnailFileName);
                        //}


                        var width = 300;
                        var height = 300;
                        if (img.Height > img.Width)
                            width = (int)(300d / img.Height * img.Width);
                        else
                            height = (int)(300d / img.Width * img.Height);

                        using (var img1 = img.GetThumbnailImage(width, height, () => false, IntPtr.Zero))
                        {
                            img1.Save(thumbnailFileName);
                            //createPictureDto.Thumbnail = thumbnailFileName.Replace(baseDirectory, "");
                        }

                    }
                }

                if (fileNameOld != fileName)
                {
                    var updatePictureDto = result.MapTo<CreateOrUpdatePictureDto>();
                    updatePictureDto.RelativePath = fileName.Replace(baseDirectory, "");
                    updatePictureDto.Thumbnail = thumbnailFileName.Replace(baseDirectory, "");
                    var newResult = _pictureAppService.UpdateUser(updatePictureDto);
                    try
                    {
                        if (System.IO.File.Exists(fileNameOld))
                            System.IO.File.Delete(fileNameOld);

                        if (System.IO.File.Exists(thumbnailFileNameOld))
                            System.IO.File.Delete(thumbnailFileNameOld);
                    }
                    catch (Exception)
                    {

                       // throw;
                    }
                    return Json(newResult);
                }

                return Json(result);
            }

            return new EmptyResult();
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id">文件标识</param>
        /// <returns></returns>
        public ActionResult DeleteUser(Guid? id)
        {
            if (id.HasValue)
            {
                var result = _pictureAppService.GetByIdUser(new EntityDto<Guid> { Id = id.Value });
                var baseDirectory = Server.MapPath("~/");
                var fileName = Path.Combine(baseDirectory, result.RelativePath);
                var thumbnailFileName = Path.Combine(baseDirectory, result.Thumbnail);
                System.IO.File.Delete(fileName);
                System.IO.File.Delete(thumbnailFileName);
                _pictureAppService.DeleteUser(new EntityDto<Guid> { Id = id.Value });
                return new EmptyResult();
            }

            return new EmptyResult();
        }
        #endregion
        /// <summary>
        /// 为图片生成缩略图
        /// </summary>
        /// <param name="image">原图片的路径</param>
        /// <param name="width">缩略图宽</param>
        /// <param name="height">缩略图高</param>
        /// <returns></returns>
        private Image GetThumbnail(Image image, int width, int height)
        {
            using (image)
            {
                var bmp = new Bitmap(width, height);
                //从Bitmap创建一个System.Drawing.Graphics
                var gr = Graphics.FromImage(bmp);
                //设置 
                gr.SmoothingMode = SmoothingMode.HighQuality;
                //下面这个也设成高质量
                gr.CompositingQuality = CompositingQuality.HighQuality;
                //下面这个设成High
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //把原始图像绘制成上面所设置宽高的缩小图
                var rectDestination = new Rectangle(0, 0, width, height);

                gr.DrawImage(image, rectDestination, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                return bmp;
            }
        }

        ///// <summary>
        ///// PASS上传文件
        ///// </summary>
        ///// <param name="file">文件对象</param>
        ///// <returns></returns>
        //[AbpAllowAnonymous]
        //public ActionResult PassUploading(HttpPostedFileWrapper file)
        //{
        //    //HttpPostedFile
        //    //HttpPostedFileBase
        //    //HttpPostedFileWrapper
        //    //Directory
        //    var baseDirectory = Server.MapPath("~/");
        //    var fileDirectory = Path.Combine(baseDirectory, "Upload");
        //    var pictureDirectory = Path.Combine(fileDirectory, "Picture");
        //    var belongDirectory = Path.Combine(pictureDirectory, "PASS-AAE6AA99-8E7C-48E5-918F-7408D08A1CDB");
        //    var date = DateTime.Now.ToString("yyyyMM");
        //    var dateDirectory = Path.Combine(belongDirectory, date);
        //    if (!Directory.Exists(dateDirectory))
        //        Directory.CreateDirectory(dateDirectory);

        //    var extension = Path.GetFileName(file.FileName);
        //    Debug.Assert(extension != null, nameof(extension) + " != null");
        //    var fileName = Path.Combine(dateDirectory, extension);
        //    file.SaveAs(fileName);
        //    GC.Collect();

        //    return new EmptyResult();
        //}
    }
}