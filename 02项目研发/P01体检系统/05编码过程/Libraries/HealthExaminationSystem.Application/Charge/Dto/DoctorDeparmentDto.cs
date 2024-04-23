using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto;
#if !Proxy
using Abp.AutoMapper;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerReg))]
#endif
    public class DoctorDeparmentDto
    {
        public virtual List<MReceiptInfoDto> MReceiptInfo { get; set; }

        public virtual List<showCustomerItemGroupsDto> CustomerItemGroup { get; set; }
#if Application
        [IgnoreMap]
#endif
        public virtual string CustomerItemGroups
        {
            get
            {
                if (CustomerItemGroup == null)
                {
                    return "";
                }
                else
                {
                    var GetDisease = CustomerItemGroup.Select(a => a.ItemGroupName).FirstOrDefault();
                    return GetDisease.ToString();
                }



            }
        }
        public virtual string CustomerItemGroupPrice
        {
            get
            {
                if (CustomerItemGroup == null)
                {
                    return "";
                }
                else
                {
                    var GetDisease = CustomerItemGroup.Select(o => o.PriceAfterDis).FirstOrDefault();
                    return string.Join(",", GetDisease);
                }



            }
        }
        public virtual string CustomerItemGroupPriceCount
        {
            get
            {
                if (CustomerItemGroup == null)
                {
                    return "";
                }
                else
                {
                    var GetDisease = CustomerItemGroup.Select(o => o.PriceAfterDis).Sum().ToString();
                    return string.Join(",", GetDisease);
                }
            }
        }
        public virtual string CustomerItemGroupName
        {
            get
            {
                if (CustomerItemGroup == null)
                {
                    return "";
                }
                else
                {
                    var GetDisease = CustomerItemGroup.Select(o => o.ItemGroupName).Count();
                    return string.Join(",", GetDisease);
                }
            }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }       
        /// <summary>
        /// 单位
        /// </summary>
        public virtual ClientInfoSimpDto ClientInfo { get; set; }
       
        public string ItemGroupName { get; set; }

        public virtual string CustomerItemGroupTime
        {
            get
            {
                if (CustomerItemGroup == null)
                {
                    return "";
                }
                else
                {
                    var GetDisease = CustomerItemGroup.Select(o => o.CreationTime).FirstOrDefault();
                    return GetDisease.ToString();
                }
            }
        }
    }
}
