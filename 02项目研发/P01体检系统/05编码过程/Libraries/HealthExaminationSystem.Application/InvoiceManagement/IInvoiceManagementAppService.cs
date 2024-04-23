using System.Collections.Generic;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.InvoiceManagement.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.InvoiceManagement
{
    /// <summary>
    /// 发票管理
    /// </summary>
    public interface IInvoiceManagementAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        List<UserViewDto> GetUser();

        /// <summary>
        /// 添加发票信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<MReceiptManagerDto> AddReceiptManage(AddReceiptManageDto input);

        /// <summary>
        /// 修改发票信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        MReceiptManagerDto EditReceiptManage(AddReceiptManageDto input);

        /// <summary>
        /// 删除发票信息
        /// </summary>
        /// <param name="input"></param>
        void DeleteReceiptManage(MReceiptManagerDto input);

        /// <summary>
        /// 查询发票信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<MReceiptManagerDto> QueryReceiptManage(MReceiptManagerDto input);
        /// <summary>
        /// 获取当前登陆用户发票
        /// </summary>
        /// <returns></returns>
        List<MReceiptManagerDto> GetUserInvoice();
    }
}