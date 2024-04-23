using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto
{
    public class CrossTableViewDto
    {
        /// <summary>
        /// 预约记录
        /// </summary>
        public PageResultDto<CustomerRegForCrossTableViewDto> CustomerReg { get; set; }
        /// <summary>
        /// 已登记总数
        /// </summary>
        public int? SumRegister { get; set; }
        /// <summary>
        /// 已交表总数
        /// </summary>
        public int? SumSendToConfirm { get; set; }
    }
}
