using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using AutoMapper;
using DevExpress.Data.ODataLinq.Helpers;
using Sw.Hospital.HealthExaminationSystem.Application.Questionnaire.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Application.Questionnaire
{
    /// <inheritdoc cref="IQuestionnaireAppService" />
    [AbpAuthorize]
    public class QuestionnaireAppService : MyProjectAppServiceBase, IQuestionnaireAppService
    {
        /// <summary>
        /// 问卷记录仓储
        /// </summary>
        private readonly IRepository<TjlCusQuestion, Guid> _cusQuestionRepository;

        /// <summary>
        /// 问卷试题记录仓储
        /// </summary>
        private readonly IRepository<TjlQuestionBom, Guid> _questionBomRepository;

        /// <summary>
        /// 问卷试题选项记录仓储
        /// </summary>
        private readonly IRepository<TjlQuestiontem, Guid> _questiontemRepository;

        /// <inheritdoc cref="IQuestionnaireAppService" />
        public QuestionnaireAppService(IRepository<TjlCusQuestion, Guid> cusQuestionRepository, IRepository<TjlQuestionBom, Guid> questionBomRepository, IRepository<TjlQuestiontem, Guid> questiontemRepository)
        {
            _cusQuestionRepository = cusQuestionRepository;
            _questionBomRepository = questionBomRepository;
            _questiontemRepository = questiontemRepository;
        }

        /// <inheritdoc />
        public List<QuestionnaireDto> QueryQuestionnaireRecord(QuestionnaireSearchInput input)
        {
            var query = _cusQuestionRepository.GetAll().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(input.checkNo))
            {
                query = query.Where(r => r.CustomerReg.CustomerBM == input.checkNo);
            }

            if (!string.IsNullOrWhiteSpace(input.tempPersonCheckOrderno))
            {
                query = query.Where(r => r.tempPersonCheckOrderno == input.tempPersonCheckOrderno);
            }

            if (input.lastTimeStart.HasValue)
            {
                var start = input.lastTimeStart.Value.Date.ToString("yyyy-MM-dd HH:mm:ss");
                query = query.Where(r =>
                    string.Compare(r.lastTime, start) >= 0);
            }

            if (input.lastTimeEnd.HasValue)
            {
                var end = input.lastTimeEnd.Value.AddDays(1).Date.ToString("yyyy-MM-dd HH:mm:ss");
                query = query.Where(r => string.Compare(r.lastTime,
                    end) < 0);
            }

            return query.ProjectToList<QuestionnaireDto>(GetConfigurationProvider<QuestionnaireDto>());
        }

        /// <inheritdoc />
        public List<QuestionBomDto> QueryQuestionBomRecordByQuestionnaireId(EntityDto<Guid> input)
        {
            var query = _questionBomRepository.GetAllIncluding(r => r.itemList).AsNoTracking();
            query = query.Where(r => r.CusQuestionId == input.Id);
            query = query.OrderBy(r => r.bomItemType);
            return query.ProjectToList<QuestionBomDto>(GetConfigurationProvider<QuestionBomDto>());
        }
        /// <summary>
        /// 报告问卷 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<QuestiontemReportDto> QueryQuestionBomRecordByRegID(EntityDto<Guid> input)
        {
            var query = _questiontemRepository.GetAll().Where(p => p.CustomerRegId == input.Id).Select(
                p => new QuestiontemReportDto
                {
                    bomItemName = p.QuestionBom.bomItemName,
                    answerContent = p.QuestionBom.answerContent,
                    bomItemType = p.QuestionBom.bomItemType,
                    isSelected = p.isSelected == 1 ? "√" : "",
                    itemName = p.itemName,
                    Sig="⭕",
                     OrderNum=p.QuestionBom.OrderNum,
                      ItemOrderNum=p.OrderNum,
                    Title = p.QuestionBom.Title
                }).OrderBy(p=>p.OrderNum).ThenBy(p=>p.ItemOrderNum).ToList();

            var quelist = _questionBomRepository.GetAll().Where(p => p.CustomerRegId == input.Id && p.bomItemType==1).Select(p =>
                   new QuestiontemReportDto
                   {
                       bomItemName = p.bomItemName,
                       answerContent ="",                      
                       bomItemType = p.bomItemType,
                       isSelected = "",
                       itemName = p.answerContent,
                       Sig = "",
                        OrderNum=p.OrderNum,
                         ItemOrderNum=p.OrderNum,
                          Title=p.Title
                   }).OrderBy(p=>p.OrderNum).ToList();
            query.AddRange(quelist);
            return query.OrderBy(p=>p.OrderNum).ThenBy(p=>p.ItemOrderNum).ToList();
        }
        /// <inheritdoc />
        public bool UpdateQuestionBomRecord(List<QuestionBomDto> input)
        {
            if (input.Count == 0)
            {
                return false;
            }
            var cusQuestionId = input.First().CusQuestionId;
            if (!cusQuestionId.HasValue)
            {
                return false;
            }

            var query = _questionBomRepository.GetAllIncluding(r => r.itemList)
                .Where(r => r.CusQuestionId == cusQuestionId);
            var data = query.ToList();
            foreach (var row in input)
            {
                var rowData = data.Find(r => r.Id == row.Id);
                if (rowData == null)
                {
                    return false;
                }
                switch (row.bomItemType)
                {
                    case 1:
                        {
                            if (row.answerContent != rowData.answerContent)
                            {
                                rowData.answerContent = row.answerContent;
                                _questionBomRepository.Update(rowData);
                            }
                            break;
                        }
                    case 2:
                    case 3:
                        {
                            if (row.itemList != null && row.itemList.Count != 0)
                            {
                                foreach (var dto in row.itemList)
                                {
                                    var dtoData = rowData.itemList.Find(r => r.Id == dto.Id);
                                    if (dtoData == null)
                                    {
                                        return false;
                                    }

                                    if (dtoData.isSelected != dto.isSelected)
                                    {
                                        dtoData.isSelected = dto.isSelected;
                                        _questiontemRepository.Update(dtoData);
                                    }
                                }
                            }

                            break;
                        }
                    default:
                        break;
                }
            }

            return true;
        }
    }
}