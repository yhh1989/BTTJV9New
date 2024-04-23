using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
   public  class ClientZKStateHelper
    {
        public static List<ClientZKStateModel> GetBloodStateModel()
        {
            var result = new List<ClientZKStateModel>();
            var Normal = new ClientZKStateModel
            {
                Id = (int)ClientZKState.Normal,
                Name = ClientZKState.Normal.ToString(),
                Display = "无权限"
            };
            result.Add(Normal);
            var Scatter = new ClientZKStateModel
            {
                Id = (int)ClientZKState.Scatter,
                Name = ClientZKState.Scatter.ToString(),
                Display = "有权限"
            };
            result.Add(Scatter);           
            return result;
        }
    }
}
