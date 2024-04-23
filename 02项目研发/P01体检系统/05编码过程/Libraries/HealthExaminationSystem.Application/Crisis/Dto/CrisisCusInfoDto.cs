using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
#if !Proxy
using AutoMapper;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto
{
    public class CrisisCusInfoDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 预约ID
        /// </summary>
        [StringLength(32)]
        public virtual Guid cusRegId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(32)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }


        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        [StringLength(16)]
        public virtual string Mobile { get; set; }
        /// <summary>
        ///体检日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 单位信息标识
        /// </summary>      
        public virtual Guid? ClientInfoId { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public virtual DateTime? ExamineTime { get; set; }
        /// <summary>
        /// 审核人
        /// </summary> 
        public virtual long? ExamineUserId { get; set; }

        /// <summary>
        /// 通知日期
        /// </summary>
        public virtual DateTime? MessageTime { get; set; }
        /// <summary>
        /// 通知人
        /// </summary>
        public virtual long? MessageUserId { get; set; }

        /// <summary>
        /// 通知状态1未通知2已通知
        /// </summary>
        public virtual int? MessageState { get; set; }

        /// <summary>
        /// 危急值回访状态 0未上报1已上报2已取消3已审核
        /// </summary>
        public virtual int? CrisisVisitSate { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }


        /// <summary>
        /// 部门名称
        /// </summary>
        public virtual string department { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public virtual string IDCard { get; set; }
        /// <summary>
        /// 单位信息标识
        /// </summary>      
        public virtual List<CrisisItemDto> 重要异常明细 { get; set; }
        /// <summary>
        /// 格式化性别
        /// </summary>
#if Application
        [IgnoreMap]
#endif
        public virtual string FormatSex
        {
            get
            {
                var SexName = SexHelper.GetSexModelsForItemInfo().Where(o => o.Id == Sex).Select(o => o.Display).FirstOrDefault();
                return SexName;
            }

        }

#if Application
        [IgnoreMap]
#endif
        public virtual string strItemName
        {
            get
            {
                if (重要异常明细 != null && 重要异常明细.Count>0)
                {
                    var ItemName = 重要异常明细.Select(p => p.ItemName ).ToList();
                    return string.Join("\r\n", ItemName);
                     
                }
                else
                {
                    return "";
                }
            }

        }
#if Application
        [IgnoreMap]
#endif
        public virtual string strItemReSult
        {
            get
            {
                if (重要异常明细 != null && 重要异常明细.Count > 0)
                {
                    var ssyItemChar = 重要异常明细.FirstOrDefault(p => p.ItemName == "收缩压") ;
                    var szyItemChar = 重要异常明细.FirstOrDefault(p => p.ItemName == "舒张压") ;

                    string xy = "";
                    if (ssyItemChar != null && szyItemChar != null)
                    {
                        xy = "血压" + ssyItemChar?.ItemResultChar + "/" + szyItemChar?.ItemResultChar + szyItemChar?.Unit;
                    }
                    var ItemName = 重要异常明细.Where(p=>p.ItemName!= "收缩压" 
                    && p.ItemName != "舒张压").Select(p =>  p.ItemName + ":" +p.ItemResultChar ).ToList();
                    if (!string.IsNullOrEmpty(xy))
                    {
                        return xy + "\r\n" + string.Join("\r\n\r\n", ItemName);
                    }
                    else
                    { return   string.Join("\r\n\r\n", ItemName); }

                }
                else
                {
                    return "";
                }
            }

        }
#if Application
        [IgnoreMap]
#endif
        public virtual string strCrisisLever
        {
            get
            {
                if (重要异常明细 != null && 重要异常明细.Count > 0)
                {
                    List<string> ItemName = new List<string>();
                    foreach (var 重要异常 in 重要异常明细)
                    {
                        if (重要异常.CrisisLever.HasValue)
                        {
                            var sp = CriticalTypeStateHelper.CriticalTypeStateFormatter(重要异常.CrisisLever);
                            ItemName.Add(sp);
                        }
                    }
                    return string.Join("\r\n", ItemName);

                }
                else
                {
                    return "";
                }
            }

        }

#if Application
        [IgnoreMap]
#endif
        public virtual string strCrisiChar
        {
            get
            {
                if (重要异常明细 != null && 重要异常明细.Count > 0)
                {
                    //  var ItemName = 重要异常明细.Where(p=>p.CrisiChar!=null  && p.CrisiChar!="").Select(p => p.ItemName + ":" + p.CrisiChar).ToList();
                    var ssyItemChar = 重要异常明细.FirstOrDefault(p => p.ItemName == "收缩压");
                    var szyItemChar = 重要异常明细.FirstOrDefault(p => p.ItemName == "舒张压");

                    string xy = "";
                    if (ssyItemChar != null && szyItemChar != null)
                    {
                        xy = "血压" + ssyItemChar?.ItemResultChar + "/" + szyItemChar?.ItemResultChar + szyItemChar?.Unit;
                    }                    
                    var ItemNames = 重要异常明细.Where(p => p.ItemName != "收缩压"
                    && p.ItemName != "舒张压").Select(p => (p.CrisiChar != null && p.CrisiChar != "") ? (p.ItemName + ":" + p.CrisiChar.Replace("|",";")): (p.ItemName + ":" + p.ItemResultChar +p.Unit) ).ToList();
                    if (!string.IsNullOrEmpty(xy))
                    {
                        return xy + ";\r\n\r\n" + string.Join(";\r\n\r\n", ItemNames);
                    }
                    else
                    {

                        return string.Join(";\r\n\r\n", ItemNames);
                    }

                }
                else
                {
                    return "";
                }
            }

        }


#if Application
        [IgnoreMap] 
#endif
        public virtual string strCrisiContent
        {
            get
            {
                if (重要异常明细 != null && 重要异常明细.Count > 0)
                {
                     
                    var ItemNames = 重要异常明细.Where(p=>p.CrisiContent!=null &&
                    p.CrisiContent !="").Select(p=> p.ItemName + ":" + p.CrisiContent).ToList();
                    return string.Join(";\r\n\r\n", ItemNames);
                    

                }
                else
                {
                    return "";
                }
            }

        }
    }
}
