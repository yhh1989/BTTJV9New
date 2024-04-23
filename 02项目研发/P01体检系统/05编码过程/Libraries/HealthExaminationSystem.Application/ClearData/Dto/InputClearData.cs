using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.ClearData.Dto
{
    public class InputClearData
    {

 
        /// <summary>
        /// 用户
        /// </summary>
        public virtual bool? isUser { get; set; }
        /// <summary>
        /// 接口
        /// </summary>
        public virtual bool? isInterfase { get; set; }
        /// <summary>
        /// 套餐
        /// </summary>
        public virtual bool? isSuit { get; set; }
        /// <summary>
        /// 系统日志
        /// </summary>
        public virtual bool? isAbpLog { get; set; }
        /// <summary>
        /// 操作日志
        /// </summary>
        public virtual bool? isLog { get; set; }
        /// <summary>
        /// 业务数据
        /// </summary>
        public virtual bool? isTjl { get; set; }
        /// <summary>
        /// 建议数据
        /// </summary>
        public virtual bool? isAdVice { get; set; }

        /// <summary>
        /// 条码数据
        /// </summary>
        public virtual bool? isBar { get; set; }

    }
}
