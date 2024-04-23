using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{

    public  class SellTypeStateHelper
    {
        private static readonly List<SellTypeStateModel> SellTypeList = new List<SellTypeStateModel>();
        /// <summary>
        /// 获取系统性别列表
        /// </summary>
        /// <returns></returns>
        public static List<SellTypeStateModel> GetSellTypeStates()
        {
            var model = new List<SellTypeStateModel>();
            var work = new SellTypeStateModel()
            {
                Id = (int)SellTypeState.Begin,
                Name = SellTypeState.Begin.ToString(),
                Display = "先返款"
            };
            model.Add(work);
            var workshop = new SellTypeStateModel()
            {
                Id = (int)SellTypeState.After,
                Name = SellTypeState.After.ToString(),
                Display = "后返款"
            };
            model.Add(workshop);
            var industry = new SellTypeStateModel()
            {
                Id = (int)SellTypeState.All,
                Name = SellTypeState.All.ToString(),
                Display = "全部"
            };
            model.Add(industry);
            return model;
        }
    }
}
