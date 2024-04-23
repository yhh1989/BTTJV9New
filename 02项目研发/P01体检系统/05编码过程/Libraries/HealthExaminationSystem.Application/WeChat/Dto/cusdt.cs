using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public  class cusdt
    {
        /// <summary>
        /// 体检ID
        /// </summary>
        [StringLength(32)]
        public virtual string customerregid { get; set; }

        /// <summary>
        /// 档案号
        /// </summary>
        [StringLength(32)]
        public virtual string archivesnum { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(32)]
        public virtual string name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? sex { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public virtual DateTime? birthday { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? age { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [StringLength(24)]
        public virtual string idcardno { get; set; }
        /// <summary>
        /// 移动电话
        /// </summary>
        [StringLength(16)]
        public virtual string mobile { get; set; }
        /// <summary>
        /// 预约日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [StringLength(16)]
        public virtual string Telephone { get; set; }

        

        /// <summary>
        /// 婚姻状况 1未婚2结婚
        /// </summary>
        public virtual int? MarriageStatus { get; set; }


       
        /// <summary>
        /// 套餐信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>      
        public virtual Guid? ItemSuitBMId { get; set; }
       
    }
}
