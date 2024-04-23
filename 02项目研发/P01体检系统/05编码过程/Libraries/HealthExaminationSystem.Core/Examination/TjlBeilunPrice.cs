using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
   public class TjlBeilunPrice : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        //编号
        [StringLength(50)]
        public string BH { get; set; }
        //名称
        [StringLength(500)]
        public string MC { get; set; }

        [StringLength(500)]
        public string FYTJBH { get; set; }
        //助记码
        [StringLength(500)]
        public string SRM1 { get; set; }
        //单价
        public decimal DJ { get; set; }
        //单位
        [StringLength(500)]
        public string DW { get; set; }
        [StringLength(500)]
        public string YJHD { get; set; }
        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
