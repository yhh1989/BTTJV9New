using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
   public  class UsedStateHelper
    {
        private static readonly List<UsedStateModel> UsedStateList = new List<UsedStateModel>();
        /// <summary>
        /// 获取系统性别列表
        /// </summary>
        /// <returns></returns>
        public static List<UsedStateModel> GetUsedStateList()
        {
            var model = new List<UsedStateModel>();
            var work = new UsedStateModel()
            {
                Id = (int)UsedState.NoUser,
                Name = UsedState.NoUser.ToString(),
                Display = "未使用"
            };
            model.Add(work);
            var workshop = new UsedStateModel()
            {
                Id = (int)UsedState.Userd,
                Name = UsedState.Userd.ToString(),
                Display = "已使用"
            };
            model.Add(workshop);
            var industry = new UsedStateModel()
            {
                Id = (int)UsedState.All,
                Name = UsedState.All.ToString(),
                Display = "全部"
            };
            model.Add(industry);
            return model;
        }
    }
}
