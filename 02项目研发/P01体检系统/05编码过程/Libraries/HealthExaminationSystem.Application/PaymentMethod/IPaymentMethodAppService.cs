using System.Collections.Generic;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod
{
    /// <summary>
    /// 支付方式接口
    /// </summary>
    public interface IPaymentMethodAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 获取所有支付方式
        /// </summary>
        /// <returns></returns>
        List<MChargeTypeDto> GetMChargeType();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        MChargeTypeDto AddMCargeType(MChargeTypeDto input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        MChargeTypeDto EditMCargeType(MChargeTypeDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        void DeleteMCargeType(MChargeTypeDto input);
    }
}