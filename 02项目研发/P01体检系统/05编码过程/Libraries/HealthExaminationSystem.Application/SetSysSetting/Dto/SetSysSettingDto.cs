
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.SetSysSetting.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmBaritem))]
#endif
    public class SetSysSettingDto : EntityDto<Guid>
    {
        /// <summary>
        /// 参数ID
        /// </summary>
        public virtual string KeyID { get; set; }

        /// <summary>
        /// 参数类别名称:只能一级类别，一级明细。
        /// </summary>
        [StringLength(64)]
        public virtual string KeyCategory { get; set; }

        /// <summary>
        /// 参数明细名称
        /// </summary>
        [StringLength(64)]
        public virtual string KeyName { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        [StringLength(64)]
        public virtual string KeyValue { get; set; }

        /// <summary>
        /// 参数说明:在参数设置页面会显示，不能输入内部信息。
        /// </summary>
        [StringLength(1024)]
        public virtual string KeyMemo { get; set; }

    }
}
