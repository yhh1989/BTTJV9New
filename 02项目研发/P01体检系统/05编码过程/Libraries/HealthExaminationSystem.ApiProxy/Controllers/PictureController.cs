using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Sw.Hospital.HealthExaminationSystem.Application.Picture.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers
{
    public class PictureController : AppServiceApiProxyBase
    {
        public PictureController() : base("Url", "Controller")
        {

        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file">文件对象</param>
        /// <param name="belong">文件归属于</param>
        /// <returns></returns>
        public PictureDto Uploading(string file, string belong)
        {
            var value = new NameValueCollection();
            value.Set(nameof(belong), belong);
            return GetResult<PictureDto>(file, value, DynamicUriBuilder.GetAppSettingValue());
        }      
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file">文件对象</param>
        /// <param name="belong">文件归属于</param>
        /// <returns></returns>
        public PictureDto UploadingUser(string file, string belong)
        {
            var value = new NameValueCollection();
            value.Set(nameof(belong), belong);
            return GetResult<PictureDto>(file, value, DynamicUriBuilder.GetAppSettingValue());
        }

        /// <summary>
        /// 获取文件地址
        /// </summary> 
        /// <param name="id">文件标识</param>
        /// <returns></returns>
        public PictureDto GetUrl(Guid id)
        {
            var value = new NameValueCollection();
            value.Set(nameof(id), id.ToString());
            return GetResult<PictureDto>(value, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 获取文件地址
        /// </summary> 
        /// <param name="id">文件标识</param>
        /// <returns></returns> 
        public PictureDto GetUrlUser(Guid id)
        {
            var value = new NameValueCollection();
            value.Set(nameof(id), id.ToString());
            return GetResult<PictureDto>(value, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 获取文件地址集合
        /// </summary>
        /// <param name="id">文件标识</param>
        /// <returns></returns>
        public List< PictureDto> GetUrlls(List<Guid> idls)
        {
            List<PictureDto> pictureDtos = new List<PictureDto>();
            foreach (var id in idls)
            {
                var value = new NameValueCollection();
                value.Set(nameof(id), id.ToString());

               var pic= GetResult<PictureDto>(value, DynamicUriBuilder.GetAppSettingValue());
                pictureDtos.Add(pic);
            }
            return pictureDtos;
        }
        /// <summary>
        /// 更新文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="id">文件标识</param>
        /// <returns></returns>
        public PictureDto Update(string file, Guid id)
        {
            var value = new NameValueCollection();
            value.Set(nameof(id), id.ToString());
            return GetResult<PictureDto>(file, value, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 更新用户签名文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="id">文件标识</param>
        /// <returns></returns>
        public PictureDto UpdateUser(string file, Guid id)
        {
            var value = new NameValueCollection();
            value.Set(nameof(id), id.ToString());
            return GetResult<PictureDto>(file, value, DynamicUriBuilder.GetAppSettingValue());
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id">文件标识</param>
        /// <returns></returns>
        public void Delete(Guid id)
        {
            var value = new NameValueCollection();
            value.Set(nameof(id), id.ToString());
            GetResult(value, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}