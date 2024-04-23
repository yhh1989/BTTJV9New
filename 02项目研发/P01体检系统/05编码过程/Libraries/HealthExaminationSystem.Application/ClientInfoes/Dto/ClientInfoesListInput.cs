using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlClientInfo))]
#endif


    public class ClientInfoesListInput : FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 单位编码
        /// </summary>
        public string ClientBM { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        public string HelpCode { get; set; }

        /// <summary>
        /// 五笔码
        /// </summary>
        public string WubiCode { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string ClientSource { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkMan { get; set; }

        /// <summary>
        /// 登记，需要相应的员工登记才可以查看，默认值1
        /// </summary>
        public int? ClientDegree { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 来源数组
        /// </summary>
        public List<int> lisClientSource { get; set; }

        /// <summary>
        /// 客服人员
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 18位社会信用代码
        /// </summary>
    
        public string SocialCredit { get; set; }
    }
}
