using System;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy
{
    /// <summary>
    /// 应用程序编程接口响应结果
    /// </summary>
    /// <typeparam name="T">响应结果的类型</typeparam>
    public class ApiResponse<T> : ApiResponse
    {
        /// <summary>
        /// 初始化应用程序编程接口响应结果
        /// </summary>
        public ApiResponse()
        {
        }

        /// <summary>
        /// 初始化应用程序编程接口响应结果
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        /// <param name="errorMessage">错误信息</param>
        public ApiResponse(int errorCode, string errorMessage):base(errorCode,errorMessage)
        {
            
        }

        /// <summary>
        /// 结果
        /// </summary>
        [XmlElement("result")]
        [JsonProperty("result")]
        public T Result { get; set; }
    }

    /// <summary>
    /// 应用程序编程接口响应结果
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// 初始化应用程序编程接口响应结果
        /// </summary>
        public ApiResponse()
        {
        }

        /// <summary>
        /// 初始化应用程序编程接口响应结果
        /// </summary>
        /// <param name="errorCode">错误代码</param>
        /// <param name="errorMessage">错误信息</param>
        public ApiResponse(int errorCode, string errorMessage)
        {
            Success = false;
            Error = new ApiError
            {
                Code = errorCode,
                Message = errorMessage
            };
        }

        /// <summary>
        /// 目标 Url
        /// </summary>
        [XmlElement("targetUrl")]
        [JsonProperty("targetUrl")]
        public string TargetUrl { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        [XmlElement("success")]
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [XmlElement("error")]
        [JsonProperty("error")]
        public ApiError Error { get; set; }

        /// <summary>
        /// 是否是未经授权的请求
        /// </summary>
        [XmlElement("unAuthorizedRequest")]
        [JsonProperty("unAuthorizedRequest")]
        public bool UnAuthorizedRequest { get; set; }

        /// <summary>
        /// 是否 Abp
        /// </summary>
        [XmlElement("__abp")]
        [JsonProperty("__abp")]
        public bool Abp { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        [XmlElement("exception")]
        [JsonProperty("exception")]
        public Exception Exception { get; set; }
    }
}