using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
   public  class InSearchCusDto
    {

        /// <summary>
        /// 体检人名字
        /// </summary>     
        public virtual string CusNameBM { get; set; }
        /// <summary>
        /// 体检人名字
        /// </summary>     
        public virtual string Code { get; set; }
        /// <summary>
        /// 体检开始时间
        /// </summary>     
        public virtual DateTime? LoginStar { get; set; }
        /// <summary>
        /// 体检结束时间
        /// </summary>     
        public virtual DateTime? LoginEnd { get; set; }
        /// <summary>
        /// 总检开始时间
        /// </summary>     
        public virtual DateTime? SumStar { get; set; }
        /// <summary>
        /// 体检结束时间
        /// </summary>     
        public virtual DateTime? SumEnd { get; set; }
        /// <summary>
        /// 天数
        /// </summary>
        public virtual int? Day { get; set; }
        /// <summary>
        /// 体检类别
        /// </summary>
        public virtual int? CheckType { get; set; }
      
        /// <summary>
        /// 体检状态
        /// </summary>
        public virtual int? Sate { get; set; }
        /// <summary>
        /// 总检状态
        /// </summary>
        public virtual int? SumSate { get; set; }
        /// <summary>
        ///单位预约ID
        /// </summary>
        public virtual Guid? ClientRegId { get; set; }

        /// <summary>
        /// 是否合格
        /// </summary>     
        public virtual string Qualified { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>  
        public virtual string FPNo { get; set; }
        /// <summary>
        /// 查询时间类型1体检日期，其他登记日期
        /// </summary>
        public virtual int? DateType { get; set; }

        /// <summary>
        /// 总检医生
        /// </summary>
        public virtual long[] arrEmployeeName_Id { get; set; }
        /// <summary>
        /// 总检医生类别1审核医生0其他
        /// </summary>
        public virtual int? EmployeeNameType { get; set; }
    }
}
