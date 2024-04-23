using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Application.Common
{
    /// <summary>
    /// 公共应用服务
    /// </summary>
    [AbpAuthorize]
    public class CommonAppService : MyProjectAppServiceBase, ICommonAppService
    {
        private readonly IRepository<AdministrativeDivision, Guid> _administrativeDivisionRepository;
        private readonly IRepository<TjlOperationLog, Guid> _tbmOperationLogcs;
        

        /// <summary>
        /// 公共应用服务
        /// </summary>
        /// <param name="administrativeDivisionRepository">行政区划仓储</param>
        public CommonAppService(IRepository<AdministrativeDivision, Guid> administrativeDivisionRepository,
            IRepository<TjlOperationLog, Guid> TbmOperationLogcs)
        {
            _administrativeDivisionRepository = administrativeDivisionRepository;
            _tbmOperationLogcs = TbmOperationLogcs;
        }

        /// <inheritdoc />
        public TimeDto GetDateTimeNow()
        {
            var result = new TimeDto
            {
                Now = DateTime.Now
            };
            return result;
        }

        /// <inheritdoc />
        public ChineseDto GetHansBrief(ChineseDto input)
        {
            input.Brief = ChineseHelper.GetBriefCode(input.Hans);
            return input;
        }

        /// <inheritdoc />
        public async Task<List<AdministrativeDivisionDto>> GetAdministrativeDivisions()
        {
            var result =  _administrativeDivisionRepository.GetAll().OrderBy(r => r.Code);
            return await result.ProjectToListAsync<AdministrativeDivisionDto>(GetConfigurationProvider<Core.AppSystem.AdministrativeDivision, AdministrativeDivisionDto>());
           // return await result.MapTo<List<AdministrativeDivisionDto>>();
        }
        #region 日志相关
        /// <summary>
        /// 插入日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CreateOpLogDto SaveOpLog(CreateOpLogDto input)
        {
            var oplog = new TjlOperationLog();
            input.MapTo(oplog);
            oplog.Id = Guid.NewGuid();
            oplog.UseId = AbpSession.UserId;
            oplog.IPAddress = GetCurrentUserIp();
            oplog = _tbmOperationLogcs.Insert(oplog);
            return oplog.MapTo<CreateOpLogDto>();
        }        
        /// <summary>
        /// 查询日志
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PageResultDto<ShowOpLogDto> SeachOpLog(PageInputDto<SearchOpLogDto>  pinput)
        {
            var input = pinput.Input;
            var que = _tbmOperationLogcs.GetAll().AsNoTracking();
            if (!string.IsNullOrEmpty(input.LogBM))
            {
                que = que.Where(o=>o.LogBM==input.LogBM);
            }
            if (!string.IsNullOrEmpty(input.LogName))
            {
                que = que.Where(o => o.LogName.Contains(input.LogName));
            }
            if (!string.IsNullOrEmpty(input.LogText))
            {
                que = que.Where(o => o.LogText.Contains(input.LogText));
            }
            if (!string.IsNullOrEmpty(input.IPAddress))
            {
                que = que.Where(o => o.IPAddress.Contains(input.IPAddress));
            }
            if (input.UseId.HasValue)
            {
                que = que.Where(o => o.UseId== input.UseId);
            }
            if (input.StarTime.HasValue)
            {
                que = que.Where(o => o.CreationTime >= input.StarTime);
            }
            if (input.EndTime.HasValue)
            {
                var endtime = input.EndTime.Value.AddDays(1);
                que = que.Where(o => o.CreationTime <= endtime);
            }
            if (input.LogType.HasValue)
            {                
                que = que.Where(o => o.LogType== input.LogType);
            }
            var list = que.Select(o => new ShowOpLogDto
            {
                CreationTime = o.CreationTime,
                IPAddress = o.IPAddress,
                LogBM = o.LogBM,
                LogName = o.LogName,
                LogText = o.LogText,
                LogType = o.LogType,
                UserName = o.UseInfo.Name,
                UserNo=o.UseInfo.EmployeeNum,
                 LogDetail =o.LogDetail
            }).OrderBy(r => r.CreationTime);
            var result = new PageResultDto<ShowOpLogDto> { CurrentPage = pinput.CurentPage };
            result.Calculate(list.Count());
            var   list1 = list.Skip((result.CurrentPage - 1) * result.PageSize).Take(result.PageSize);
            var result1 = list1.ToList();
            result.Result = result1;
            return result;
        }
        ///// <summary>
        ///// 获取当前用户客户端ip
        ///// </summary>
        ///// <returns></returns>
        ////private string GetCurrentUserIp()
        ////{
        ////    string userIP;
        ////    userIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] == null ? System.Web.HttpContext.Current.Request.UserHostAddress : System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        ////    return userIP;
        ////}

        /// <summary>
        /// 获取客户端IP地址（无视代理）
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        public static string GetCurrentUserIp()
        {
            string userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(userHostAddress))
            {
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                    userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
            }
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.UserHostAddress;
            }
            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
            {
                return userHostAddress;
            }
            return "127.0.0.1";
        }
        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        #endregion
    }
}