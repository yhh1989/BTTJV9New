using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public static class ProjectIStateHelper
    {
        /// <summary>
        /// 获取项目检查状态选项列表
        /// </summary>
        /// <returns></returns>
        public static List<ProjectIStateModel> GetProjectIStateModels()
        {
            var result = new List<ProjectIStateModel>();
            var Not = new ProjectIStateModel
            {
                Id = (int)ProjectIState.Not,
                Name = ProjectIState.Not.ToString(),
                Display = "未体检"
            };
            result.Add(Not);
            var Complete = new ProjectIStateModel
            {
                Id = (int)ProjectIState.Complete,
                Name = ProjectIState.Complete.ToString(),
                Display = "已检查"
            };
            result.Add(Complete);
            var Part = new ProjectIStateModel
            {
                Id = (int)ProjectIState.Part,
                Name = ProjectIState.Part.ToString(),
                Display = "部分检查"
            };
            result.Add(Part);
            var GiveUp = new ProjectIStateModel
            {
                Id = (int)ProjectIState.GiveUp,
                Name = ProjectIState.GiveUp.ToString(),
                Display = "放弃"
            };
            result.Add(GiveUp);
            var Stay = new ProjectIStateModel
            {
                Id = (int)ProjectIState.Stay,
                Name = ProjectIState.Stay.ToString(),
                Display = "待查"
            };
            result.Add(Stay);
            var Temporary = new ProjectIStateModel
            {
                Id = (int)ProjectIState.Temporary,
                Name = ProjectIState.Temporary.ToString(),
                Display = "暂存"
            };
            result.Add(Temporary);
            return result;
        }
        /// <summary>
        /// 获取查询组合项目检查状态选项列表
        /// </summary>
        /// <returns></returns>
        public static List<ProjectIStateModel> GetProjectModels()
        {
            var result = new List<ProjectIStateModel>();
            var whole = new ProjectIStateModel
            {
                Id = 0,
                Name = "Whole",
                Display = "全部"
            };
            result.Add(whole);
            var Not = new ProjectIStateModel
            {
                Id = (int)ProjectIState.Not,
                Name = ProjectIState.Not.ToString(),
                Display = "未检"
            };
            result.Add(Not);
            var Complete = new ProjectIStateModel
            {
                Id = (int)ProjectIState.Complete,
                Name = ProjectIState.Complete.ToString(),
                Display = "已检"
            };
            result.Add(Complete); 
            var GiveUp = new ProjectIStateModel
            {
                Id = (int)ProjectIState.GiveUp,
                Name = ProjectIState.GiveUp.ToString(),
                Display = "放弃"
            };
            result.Add(GiveUp);
            var Stay = new ProjectIStateModel
            {
                Id = (int)ProjectIState.Stay,
                Name = ProjectIState.Stay.ToString(),
                Display = "待查"
            };
            result.Add(Stay);            
            return result;
        }
        /// <summary>
        /// 项目检查状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ProjectIStateFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(ProjectIState), obj))
            {
                return EnumHelper.GetEnumDesc((ProjectIState)obj);
            }
            return obj.ToString();
        }
    }
}
