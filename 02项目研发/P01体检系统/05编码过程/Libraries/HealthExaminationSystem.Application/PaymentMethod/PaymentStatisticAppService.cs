using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;

namespace Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod
{
    [AbpAuthorize]
    public class PaymentStatisticAppService : MyProjectAppServiceBase, IPaymentStatisticAppService
    {
        private readonly IRepository<TjlMReceiptInfo, Guid> _receiptInfoRepository;
        private readonly IRepository<TjlMInvoiceRecord, Guid> _invoiceRecordRepository;
        private readonly IRepository<TjlMReceiptDetails, Guid> _receiptDetailsRepository;
        private readonly IRepository<TjlMPayment, Guid> _paymentRepository;

        public PaymentStatisticAppService(IRepository<TjlMReceiptInfo, Guid> receiptInfoRepository
            ,IRepository<TjlMInvoiceRecord, Guid> invoiceRecordRepository
            ,IRepository<TjlMReceiptDetails, Guid> receiptDetailsRepository
            ,IRepository<TjlMPayment, Guid> paymentRepository)
        {
            _receiptInfoRepository = receiptInfoRepository;
            _invoiceRecordRepository = invoiceRecordRepository;
            _receiptDetailsRepository = receiptDetailsRepository;
            _paymentRepository = paymentRepository;
        }
        /// <summary>
        /// 日报统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ViewDailyReportDto GetDailyReport(SearchReceiptInfoDto input)
        { 
            input.strChargeDate = input.strChargeDate.Date;
            input.ChargeDate = input.ChargeDate.Date.AddDays(1);
            int receoptState = (int)InvoiceStatus.Valid;
            int rctzfState = (int)InvoiceStatus.Cancellation;

            var receipts = _receiptInfoRepository.GetAllIncluding(r=>r.MInvoiceRecord).
                Where(r => r.ChargeDate < input.ChargeDate && r.ChargeDate>= input.strChargeDate && r.SettlementSate == input.ReceiptState);
            if (input.UserId.HasValue)
            {
                receipts = receipts.Where(p=>p.UserId== input.UserId);
            }
                // 团体体检
                var groupInvoices = receipts.Where(r => r.TJType == input.GroupCharge );
            var groupInvoiceCount = groupInvoices.Count();
            var groupInvoiceMoney = groupInvoices.ToList().Sum(n =>n.Actualmoney);
            // 个人体检
            var personalInvoices = receipts.Where(r => r.TJType == input.PersonalCharge );
            var personalInvoiceCount = personalInvoices.Count();
            var personalInvoiceMoney = personalInvoices.Where(p=>p.Remarks!= "套餐卡支付").ToList().Sum(n => n.Actualmoney);
            // 实收金额
            // var actualMoney = receipts.Where(o => o.Remarks != "套餐卡支付" && o.ReceiptSate == receoptState).ToList().Sum(r => r.Actualmoney);
            var actualMoney = receipts.Where(o => o.Remarks != "套餐卡支付").ToList().Sum(r => r.Actualmoney);
            // 发票号段
            var invoiceNumSpan = receipts.Any() ? receipts
                .Join(_invoiceRecordRepository.GetAll(), r => r.Id, i => i.MReceiptInfoId, (r, i) => new { i.InvoiceNum })
                .Select(i => new { i.InvoiceNum }) : null;
            var minInvoiceNum = invoiceNumSpan?.Min(i => i.InvoiceNum);
            var maxInvoiceNum = invoiceNumSpan?.Max(i => i.InvoiceNum);

            // 收费方式
            //var chargeTypes = receipts.Any() ? receipts
            //     .Join(_receiptDetailsRepository.GetAll(), r => r.Id, i => i.MReceiptInfoID, (r, i) => new { i.Id })
            //     .Join(_paymentRepository.GetAll(), i => i.Id, p => p.Id, (i, p) => new { p.MChargeTypename, p.Actualmoney })
            //     .GroupBy(p => p.MChargeTypename)
            //     .Select(c => new { TypeName = c.FirstOrDefault().MChargeTypename, TypeTotal = c.Sum(m => m.Actualmoney) })
            //     .MapTo<List<ViewChargeTypeListDto>>() : new List<ViewChargeTypeListDto>();

            //var sum12 = receipts
            //  .Join(_paymentRepository.GetAll(), i => i.Id, p => p.MReceiptInfoId, (i, p) => new { p.MChargeTypename, p.MReceiptInfo.ReceiptSate, p.Actualmoney })
            //  .GroupBy(p => p.MChargeTypename).ToList();

            //var sum1 = receipts
            //    .Join(_paymentRepository.GetAll(), i => i.Id, p => p.MReceiptInfoId, (i, p) => new { p.MChargeTypename, p.MReceiptInfo.ReceiptSate, p.Actualmoney })
            //    .GroupBy(p => p.MChargeTypename)
            //    .Select(c => new { TypeName = c.FirstOrDefault().MChargeTypename, TypeTotal = c.Sum(m => m.Actualmoney), allMoney = c.Where(o => o.ReceiptSate != rctzfState).Sum(m => m.Actualmoney), ZFMoney = c.Where(o => o.ReceiptSate == rctzfState).Sum(m => m.Actualmoney) }).ToList();
            //var chargeTypes = new List<ViewChargeTypeListDto>();

            var chargelist = receipts
                .Join(_paymentRepository.GetAll(), i => i.Id, p => p.MReceiptInfoId, (i, p) => new { p.MChargeTypename, p.MReceiptInfo.ReceiptSate, p.Actualmoney ,i.UserId, Username= i.User.Name})
                .GroupBy(p => new  { p.MChargeTypename,p.UserId ,p.Username})
                .Select(c => new ViewChargeTypeListDto
                { 
                    UserName=c.Key.Username,
                    TypeName = c.Key.MChargeTypename,
                    TypeTotal = c.Sum(m => (decimal?)m.Actualmoney),
                    allMoney = c.Where(o => o.ReceiptSate != rctzfState).Sum(m => (decimal?)m.Actualmoney),
                    ZFMoney = c.Where(o => o.ReceiptSate == rctzfState).Sum(m => (decimal?)m.Actualmoney) });

            var chargeTypes = chargelist.OrderBy(p=>p.TypeName).ThenBy(p=>p.UserName).ToList();
            //var chargeTypes = receipts.Any() ? receipts
            //    .Join(_paymentRepository.GetAll(), i => i.Id, p => p.MReceiptInfoId, (i, p) => new { p.MChargeTypename, p.MReceiptInfo.ReceiptSate, p.Actualmoney })
            //    .GroupBy(p => p.MChargeTypename)
            //    .Select(c => new ViewChargeTypeListDto { TypeName = c.FirstOrDefault().MChargeTypename, TypeTotal = c.Sum(m => (decimal?)m.Actualmoney), allMoney = c.Where(o => o.ReceiptSate != rctzfState).Sum(m => (decimal?)m.Actualmoney), ZFMoney = c.Where(o => o.ReceiptSate == rctzfState).Sum(m => (decimal?)m.Actualmoney) })
            //    .MapTo<List<ViewChargeTypeListDto>>() : new List<ViewChargeTypeListDto>();

            // 作废发票
            var invalidInvoices = receipts.Any() ? receipts
                .Join(_invoiceRecordRepository.GetAll().Where(i => i.State == input.InvoiceStatus), r => r.Id, i => i.MReceiptInfo.Id, (r, i) => new { i.InvoiceNum, i.InvoiceMoney })
                .Select(i => new ViewInvalidInvoiceListDto { InvoiceNum= i.InvoiceNum, InvoiceMoney= i.InvoiceMoney })
                .MapTo<List<ViewInvalidInvoiceListDto>>() : new List<ViewInvalidInvoiceListDto>();

            return new ViewDailyReportDto
            {
                MinInvoiceNum = minInvoiceNum,
                MaxInvoiceNum = maxInvoiceNum,
                GroupInvoiceCount = groupInvoiceCount,
                IndividualInvoiceCount = personalInvoiceCount,
                GroupInvoiceMoney = groupInvoiceMoney,
                IndividualInvoiceMoney = personalInvoiceMoney,
                ActualMoney = actualMoney,
                ChargeTypes = chargeTypes,
                InvalidInvoices = invalidInvoices
            };
        }
    }
}