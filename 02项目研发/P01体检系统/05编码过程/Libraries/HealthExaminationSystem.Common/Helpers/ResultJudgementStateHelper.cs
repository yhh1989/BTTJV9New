using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 结果判断状态帮助
    /// </summary>
    public class ResultJudgementStateHelper
    {
        /// <summary>
        /// 获取结果判断状态
        /// </summary>
        /// <returns></returns>
        public static List<ResultJudgementStateModel> GetResultJudgementStateModels()
        {
            var result = new List<ResultJudgementStateModel>();
            var BigOrSmall = new ResultJudgementStateModel
            {
                Id = (int)ResultJudgementState.BigOrSmall,
                Name = ResultJudgementState.BigOrSmall.ToString(),
                Display = "大小区间"
            };
            result.Add(BigOrSmall);
            var ContainText = new ResultJudgementStateModel
            {
                Id = (int)ResultJudgementState.ContainText,
                Name = ResultJudgementState.ContainText.ToString(),
                Display = "包含文字"
            };
            result.Add(ContainText);
            var Text = new ResultJudgementStateModel
            {
                Id = (int)ResultJudgementState.Text,
                Name = ResultJudgementState.Text.ToString(),
                Display = "等于文字"
            };
            result.Add(Text);
            return result;
        }
    }
}
