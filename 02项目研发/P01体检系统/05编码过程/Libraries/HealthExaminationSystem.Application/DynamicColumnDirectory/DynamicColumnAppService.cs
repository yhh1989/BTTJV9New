using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.DynamicColumnDirectory.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.DynamicColumnDirectory;

namespace Sw.Hospital.HealthExaminationSystem.Application.DynamicColumnDirectory
{
    /// <summary>
    /// 动态列应用服务
    /// </summary>
    [AbpAuthorize]
    public class DynamicColumnAppService : MyProjectAppServiceBase, IDynamicColumnAppService
    {
        /// <summary>
        /// “动态列配置”仓储
        /// </summary>
        private readonly IRepository<DynamicColumnConfiguration, Guid> _dynamicColumnConfigurationRepository;

        /// <summary>
        /// 初始化“动态列应用服务”
        /// </summary>
        public DynamicColumnAppService(IRepository<DynamicColumnConfiguration, Guid> dynamicColumnConfigurationRepository)
        {
            _dynamicColumnConfigurationRepository = dynamicColumnConfigurationRepository;
        }

        /// <inheritdoc />
        public async Task<bool> SaveDynamicColumnConfigurationList(List<DynamicColumnConfigurationDtoNo1> input)
        {
            using (var unitOfWork = UnitOfWorkManager.Begin())
            {
                foreach (var inputRow in input)
                {
                    var row = await _dynamicColumnConfigurationRepository.FirstOrDefaultAsync(r =>
                        r.GridViewId == inputRow.GridViewId && r.GridViewColumnName == inputRow.GridViewColumnName);

                    if (row == null)
                    {
                        row = new DynamicColumnConfiguration();
                        inputRow.Id = Guid.NewGuid();
                        ObjectMapper.Map(inputRow, row);
                        await _dynamicColumnConfigurationRepository.InsertAsync(row);
                    }
                    else
                    {
                        inputRow.Id = row.Id;
                        ObjectMapper.Map(inputRow, row);
                        await _dynamicColumnConfigurationRepository.UpdateAsync(row);
                    }
                }

                await CurrentUnitOfWork.SaveChangesAsync();

                var inputGroupByGridViewId = input.GroupBy(r => r.GridViewId);
                foreach (var inputGridViewIdGroup in inputGroupByGridViewId)
                {
                    var dbColumnNameList = await _dynamicColumnConfigurationRepository.GetAll()
                        .Where(r => r.GridViewId == inputGridViewIdGroup.Key).Select(r => r.GridViewColumnName)
                        .ToListAsync();
                    var inputColumnNameList = inputGridViewIdGroup.Select(r => r.GridViewColumnName).ToList();
                    var columnNameList = dbColumnNameList.Except(inputColumnNameList).ToList();
                    foreach (var columnName in columnNameList)
                    {
                        var row = await _dynamicColumnConfigurationRepository.FirstOrDefaultAsync(r =>
                            r.GridViewId == inputGridViewIdGroup.Key && r.GridViewColumnName == columnName);
                        await _dynamicColumnConfigurationRepository.HardDeleteAsync(row);
                    }
                }

                await CurrentUnitOfWork.SaveChangesAsync();
                await unitOfWork.CompleteAsync();
                return true;
            }
        }

        /// <inheritdoc />
        public async Task<List<DynamicColumnConfigurationDtoNo1>> QueryDynamicColumnConfigurationList(DynamicColumnConfigurationDtoNo2 input)
        {
            var query = _dynamicColumnConfigurationRepository.GetAll().AsNoTracking();
            query = query.Where(r => r.GridViewId == input.GridViewId);
            query = query.OrderBy(r => r.VisibleIndex);
            return await query.ProjectToListAsync<DynamicColumnConfigurationDtoNo1>(
                GetConfigurationProvider<DynamicColumnConfigurationDtoNo1>());
        }
    }
}