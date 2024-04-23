using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;

namespace Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod
{
    [AbpAuthorize]
    public class PaymentMethodAppService : MyProjectAppServiceBase, IPaymentMethodAppService
    {
        private readonly IRepository<TbmMChargeType, Guid> _paymentRepository;

        public PaymentMethodAppService(IRepository<TbmMChargeType, Guid> paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public MChargeTypeDto AddMCargeType(MChargeTypeDto input)
        {
            var allList = _paymentRepository.GetAll();
            var exist = allList.Where(p => p.ChargeCode == input.ChargeCode);
            if (exist.Any())
                throw new FieldVerifyException("编码重复","已存在编码代码的支付方式！");
            exist = allList.Where(p => p.ChargeName == input.ChargeName);
            if (exist.Any())
                throw new FieldVerifyException("名称重复", "已存在相同名称的支付方式！");
            var orderNum = allList.Max(a => a.OrderNum);
            input.Id = Guid.NewGuid();
            input.OrderNum = orderNum == null ? 0 : orderNum + 1;
            var result = _paymentRepository.Insert(input.MapTo<TbmMChargeType>());
            return result.MapTo<MChargeTypeDto>();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        public void DeleteMCargeType(MChargeTypeDto input)
        {
            if (input.Id == Guid.Empty)
                throw new UserFriendlyException("ID不能为空");
            _paymentRepository.Delete(input.MapTo<TbmMChargeType>());
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public MChargeTypeDto EditMCargeType(MChargeTypeDto input)
        {
            if (input.Id == Guid.Empty)
                throw new UserFriendlyException("ID不能为空");
            if (_paymentRepository.GetAll().Any(o => o.Id != input.Id && o.ChargeName == input.ChargeName))
            {
                throw new FieldVerifyException("名称重复", "已存在相同名称的支付方式！");
            }
            if (_paymentRepository.GetAll().Any(o => o.Id != input.Id && o.ChargeName == input.ChargeName))
            {
                throw new FieldVerifyException("编码重复", "已存在编码代码的支付方式！");
            }
            var oldData = _paymentRepository.Get(input.Id);
            var newData = input.MapTo(oldData);
            var result = _paymentRepository.Update(newData);
            return result.MapTo<MChargeTypeDto>();
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public List<MChargeTypeDto> GetMChargeType()
        {
            var result = _paymentRepository.GetAll();
            return result.MapTo<List<MChargeTypeDto>>();
        }
    }
}