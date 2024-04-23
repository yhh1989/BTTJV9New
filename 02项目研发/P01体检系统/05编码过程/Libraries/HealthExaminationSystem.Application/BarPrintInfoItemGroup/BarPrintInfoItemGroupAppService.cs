using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.BarPrintInfoItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using System;
using System.Collections.Generic;
using System.Data;
using Abp.Authorization;

namespace Sw.Hospital.HealthExaminationSystem.Application.BarPrintInfoItemGroup
{
    [AbpAuthorize]
    public class BarPrintInfoItemGroupAppService : MyProjectAppServiceBase, IBarPrintInfoItemGroupAppService
    {
        #region 申明变量
        private readonly IRepository<TjlCustomerBarPrintInfoItemGroup, Guid> _CustomerBarPrintInfoItemGroup;
        private readonly IRepository<TjlCustomerBarPrintInfo, Guid> _CustomerBarPrintInfo;
        private readonly IRepository<TbmItemGroup, Guid> _ItemGroup;
        private readonly IRepository<TjlCustomerItemGroup, Guid> _TjlCustomerItemGroup;
        #endregion 申明变量

        #region 构造函数
        public BarPrintInfoItemGroupAppService(IRepository<TjlCustomerBarPrintInfoItemGroup, Guid> CustomerBarPrintInfoItemGroup, 
            IRepository<TjlCustomerBarPrintInfo, Guid> CustomerBarPrintInfo, IRepository<TbmItemGroup, Guid> ItemGroup,
            IRepository<TjlCustomerItemGroup, Guid> TjlCustomerItemGroup)
        {
            _CustomerBarPrintInfoItemGroup = CustomerBarPrintInfoItemGroup;
            _CustomerBarPrintInfo = CustomerBarPrintInfo;
            _ItemGroup = ItemGroup;
            _TjlCustomerItemGroup = TjlCustomerItemGroup;
        }
        #endregion 构造函数
        /// <summary>
        /// 增加
        /// </summary>
        public bool AddBarPrintInfoItemGroupApp(CreateBarPrintInfoItemGroupDto CustomerBarPrintInfoDto)
        {
            var entity = CustomerBarPrintInfoDto.MapTo<TjlCustomerBarPrintInfoItemGroup>();
            entity.Id = Guid.NewGuid();
            entity.BarPrintInfo = _CustomerBarPrintInfo.Get(CustomerBarPrintInfoDto.BarPrintInfoid);
            entity.ItemGroup = _ItemGroup.Get(CustomerBarPrintInfoDto.ItemGroupid);
            var entityResult = _CustomerBarPrintInfoItemGroup.Insert(entity);
           var cusgroup= _TjlCustomerItemGroup.FirstOrDefault(o=>o.CustomerRegBMId== entityResult.BarPrintInfo.CustomerReg_Id && o.ItemGroupBM_Id== entityResult.ItemGroup.Id);
            cusgroup.BarState = 2;
            _TjlCustomerItemGroup.Update(cusgroup);
            var dto = entityResult.MapTo<BarPrintInfoItemGroupDto>();
            return true;
        }

        /// <summary>
        /// 批量增加
        /// </summary>
        public bool AddBarPrintInfoItemGroupApp(List<CreateBarPrintInfoItemGroupDto> CustomerBarPrintInfoDto)
        {
            foreach (var item in CustomerBarPrintInfoDto)
            {
                var entity = item.MapTo<TjlCustomerBarPrintInfoItemGroup>();
                entity.Id = Guid.NewGuid();
                entity.BarPrintInfo = _CustomerBarPrintInfo.Get(item.BarPrintInfoid);
                entity.ItemGroup = _ItemGroup.Get(item.ItemGroupid);
                var entityResult = _CustomerBarPrintInfoItemGroup.Insert(entity);
                var cusgroup = _TjlCustomerItemGroup.FirstOrDefault(o => o.CustomerRegBMId == entityResult.BarPrintInfo.CustomerReg_Id && o.ItemGroupBM_Id == entityResult.ItemGroup.Id);
                cusgroup.BarState = 2;
                _TjlCustomerItemGroup.Update(cusgroup);
                var dto = entityResult.MapTo<BarPrintInfoItemGroupDto>();
            }
            return true;
        }

        public bool DeleteBarPrintInfoItemGroupApp(CreateBarPrintInfoItemGroupDto CustomerBarPrintInfoDto)
        {
            var pals = _CustomerBarPrintInfoItemGroup.GetAllList(o => o.BarPrintInfoId == CustomerBarPrintInfoDto.BarPrintInfoid);
            if (pals != null && pals.Count > 0)
            {
                foreach (var pa in pals)
                {
                    var cusgroup = _TjlCustomerItemGroup.FirstOrDefault(o => o.CustomerRegBMId == pa.BarPrintInfo.CustomerReg_Id && o.ItemGroupBM_Id == pa.ItemGroup.Id);
                    if (cusgroup != null)
                    {
                        cusgroup.BarState = 1;
                        _TjlCustomerItemGroup.Update(cusgroup);
                        _CustomerBarPrintInfoItemGroup.Delete(pa);
                    }
                }
            }
            return true;
        }





        /// <summary>
        /// 查询
        /// </summary>
        public List<BarPrintInfoItemGroupQueryDto> GetLstBarPrintInfoItemGroupApp(CusNameInput cusNameInput)
        {
            var LstBarPrintInfoItemGroupApp = _CustomerBarPrintInfoItemGroup.GetAllList(p => p.BarPrintInfo.CustomerReg_Id == cusNameInput.Id);
            return LstBarPrintInfoItemGroupApp.MapTo<List<BarPrintInfoItemGroupQueryDto>>();
        }

    }
}
