using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.InvoiceManagement.Dto
{
    /// <summary>
    /// 添加发票信息数据
    /// </summary>
    public class AddReceiptManageDto
    {
        /// <summary>
        /// 发票信息
        /// </summary>
        public MReceiptManagerDto receiptManage { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public List<UserViewDto> users { get; set; }
    }
}