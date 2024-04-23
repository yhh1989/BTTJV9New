using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using Sw.Hospital.HealthExaminationSystem.Application.InvoiceManagement.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;

namespace Sw.Hospital.HealthExaminationSystem.Application.InvoiceManagement
{
    [AbpAuthorize]
    public class InvoiceManagementAppService : MyProjectAppServiceBase, IInvoiceManagementAppService
    {
        private readonly IRepository<TbmMReceiptManager, Guid> _receiptRepository;
        private readonly IRepository<User, long> _userRepository;

        public InvoiceManagementAppService(IRepository<User, long> userRepository,
            IRepository<TbmMReceiptManager, Guid> receiptRepository)
        {
            _userRepository = userRepository;
            _receiptRepository = receiptRepository;
        }

        public List<MReceiptManagerDto> AddReceiptManage(AddReceiptManageDto input)
        {
            var allData = _receiptRepository.GetAll();
            var exist = allData.Where(a => a.SerialNumber == input.receiptManage.SerialNumber);
            if (exist.Any())
                throw new FieldVerifyException("提示","已存在相同编号的发票号段!");
            exist = allData.Where(a=>
            ((a.StratCard>=input.receiptManage.StratCard&&a.StratCard<=input.receiptManage.EndCard)
            ||(a.EndCard>=input.receiptManage.StratCard&&a.EndCard<=input.receiptManage.EndCard))
            &&a.State==1);
            if (exist.Any())
                throw new FieldVerifyException("提示","已存在此发票号段!");
            var result = new List<MReceiptManagerDto>();
            foreach (var user in input.users)
            {
                var newData = input.receiptManage;
                newData.Id = Guid.NewGuid();

                var entity = newData.MapTo<TbmMReceiptManager>();
                var userEntity = _userRepository.Get(user.Id);
                entity.User = userEntity;
                var addResult = _receiptRepository.Insert(entity);
                result.Add(addResult.MapTo<MReceiptManagerDto>());
            }

            return result;
        }

        public void DeleteReceiptManage(MReceiptManagerDto input)
        {
            if (input.Id != Guid.Empty)
                _receiptRepository.Delete(input.MapTo<TbmMReceiptManager>());
            if (input.SerialNumber != null)
            {
                var dataList = _receiptRepository.GetAll().Where(r => r.SerialNumber == input.SerialNumber);
                foreach (var data in dataList)
                    _receiptRepository.Delete(data);
            }
        }

        public MReceiptManagerDto EditReceiptManage(AddReceiptManageDto input)
        {
            if (input.receiptManage == null)
                throw new FieldVerifyException("提示","未获取发票信息!");
            if (input.receiptManage.SerialNumber == null)
                throw new FieldVerifyException("提示","无法验证的发票编号!");
            if(input.receiptManage.Id==Guid.Empty)
                throw new FieldVerifyException("提示","无法验证的发票ID!");
            if (_receiptRepository.GetAll().Any(a => 
                ((a.StratCard >= input.receiptManage.StratCard && a.StratCard <= input.receiptManage.EndCard)
                || (a.EndCard >= input.receiptManage.StratCard && a.EndCard <= input.receiptManage.EndCard))
                && a.State == 1
                &&a.SerialNumber!=input.receiptManage.SerialNumber))
                throw new FieldVerifyException("提示","已存在此发票号段!");
            var currentSNumber = _receiptRepository.Get(input.receiptManage.Id).SerialNumber;
            var invoiceGroup = _receiptRepository.GetAllIncluding(r=>r.User).Where(a => a.SerialNumber == currentSNumber);
            var result = new List<MReceiptManagerDto>();
            foreach (var data in invoiceGroup)
            { 
                //判断是否存在原始用户不存在则删除
                var exist = input.users.Where(u => u.Id == data.User.Id);
                if (!exist.Any())
                {
                    _receiptRepository.Delete(data);
                    continue;
                }

                var oldData = _receiptRepository.Get(data.Id);
                var user = oldData.User;
                input.receiptManage.Id = oldData.Id;
                input.receiptManage.MapTo(oldData);
                oldData.UserId = user.Id;
                oldData.User = user;
                var newData = _receiptRepository.Update(oldData);
                result.Add(newData.MapTo<MReceiptManagerDto>());
            }
            //新用户不存在则增加
            foreach (var user in input.users)
            {
                if (!invoiceGroup.Any(g=>g.UserId==user.Id))
                {
                    var newData = input.receiptManage;
                    newData.Id = Guid.NewGuid();

                    var entity = newData.MapTo<TbmMReceiptManager>();
                    var userEntity = _userRepository.Get(user.Id);
                    entity.User = userEntity;
                    var addResult = _receiptRepository.Insert(entity);
                    result.Add(addResult.MapTo<MReceiptManagerDto>());
                }
            }
            return result.FirstOrDefault();
        }

        public List<UserViewDto> GetUser()
        {
            var users = _userRepository.GetAll();
            return users.MapTo<List<UserViewDto>>();
        }

        public List<MReceiptManagerDto> QueryReceiptManage(MReceiptManagerDto input)
        {
            var allData = _receiptRepository.GetAll();
            if(input.State!=null)
                allData = allData.Where(a => a.State == input.State);
            if (input.SerialNumber != null)
                allData = allData.Where(a => a.SerialNumber == input.SerialNumber);
            return allData.MapTo<List<MReceiptManagerDto>>();
        }
        /// <summary>
        /// 获取当前用户的发票
        /// </summary>
        public List<MReceiptManagerDto> GetUserInvoice()
        {
            var userId = AbpSession.UserId;
            if(userId==null)
                throw new FieldVerifyException("提示","获取登陆用户失败!");
            var invoiceList = _receiptRepository.GetAll().Where(r=>r.UserId==userId);
            return invoiceList.MapTo<List<MReceiptManagerDto>>();
        }
    }
}