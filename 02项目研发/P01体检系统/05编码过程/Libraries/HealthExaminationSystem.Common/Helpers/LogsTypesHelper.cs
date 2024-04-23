using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
   public static class LogsTypesHelper
    {
        /// <summary>
        /// 获取系统职位状态列表
        /// </summary>
        /// <returns></returns>
        public static List<LogsTypesModel> GetJobStatusModels()
        {
            var result = new List<LogsTypesModel>();
            var ClientId = new LogsTypesModel
            {
                Id = (int)LogsTypes.ClientId,
                Name = LogsTypes.ClientId.ToString(),
                Display = "单位相关"
            };
            result.Add(ClientId);
            var ResId = new LogsTypesModel
            {
                Id = (int)LogsTypes.ResId,
                Name = LogsTypes.ResId.ToString(),
                Display = "登记相关"
            };
            result.Add(ResId);
            var CheckId = new LogsTypesModel
            {
                Id = (int)LogsTypes.CheckId,
                Name = LogsTypes.CheckId.ToString(),
                Display = "检查相关"
            };
            result.Add(CheckId);
            var ChargId = new LogsTypesModel
            {
                Id = (int)LogsTypes.ChargId,
                Name = LogsTypes.ChargId.ToString(),
                Display = "收费相关"
            };
            result.Add(ChargId);
            var SumId = new LogsTypesModel
            {
                Id = (int)LogsTypes.SumId,
                Name = LogsTypes.SumId.ToString(),
                Display = "总检相关"
            };
            result.Add(SumId);
            var PrintId = new LogsTypesModel
            {
                Id = (int)LogsTypes.PrintId,
                Name = LogsTypes.PrintId.ToString(),
                Display = "打印相关"
            };
            result.Add(PrintId);
            var InterId = new LogsTypesModel
            {
                Id = (int)LogsTypes.InterId,
                Name = LogsTypes.InterId.ToString(),
                Display = "接口相关"
            };
            result.Add(InterId);
            var BisId = new LogsTypesModel
            {
                Id = (int)LogsTypes.BisId,
                Name = LogsTypes.BisId.ToString(),
                Display = "基础资料"
            };
            result.Add(BisId);
            var Other = new LogsTypesModel
            {
                Id = (int)LogsTypes.Other,
                Name = LogsTypes.Other.ToString(),
                Display = "其他相关"
            };
            result.Add(Other);
            var OtherWZ = new LogsTypesModel
            {
                Id = (int)LogsTypes.WZ,
                Name = LogsTypes.WZ.ToString(),
                Display = "问诊相关"
            };
            result.Add(Other);
            return result;
        }
    }
}
