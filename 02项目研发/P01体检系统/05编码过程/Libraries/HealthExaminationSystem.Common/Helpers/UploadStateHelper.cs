using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public class UploadStateHelper
    { 
        private static List<EnumModel> _UploadStateEnumModels = new List<EnumModel>();
        static UploadStateHelper()
        { 
            var notAlwaysCheck = new EnumModel
            {
                Id = (int)UploadState.No,
                Name = UploadState.No.ToString(),
                Display = "未上传"
            };
            _UploadStateEnumModels.Add(notAlwaysCheck);

            var yes = new EnumModel
            {
                Id = (int)UploadState.Yse,
                Name = UploadState.Yse.ToString(),
                Display = "已上传"
            };
            _UploadStateEnumModels.Add(yes);
            var onyes = new EnumModel
            {
                Id = (int)UploadState.OnYse,
                Name = UploadState.OnYse.ToString(),
                Display = "全部"
            };
            _UploadStateEnumModels.Add(onyes);

        }
        public static List<EnumModel> GetUploadStateModels()
        {
            //var notAlwaysCheck = new EnumModel
            //{
            //    Id = (int)UploadState.No,
            //    Name = UploadState.No.ToString(),
            //    Display = "未上传"
            //};
            //_UploadStateEnumModels.Add(notAlwaysCheck);

            //var yes = new EnumModel
            //{
            //    Id = (int)UploadState.Yse,
            //    Name = UploadState.Yse.ToString(),
            //    Display = "已上传"
            //};
            //_UploadStateEnumModels.Add(yes);
            //var onyes = new EnumModel
            //{
            //    Id = (int)UploadState.OnYse,
            //    Name = UploadState.OnYse.ToString(),
            //    Display = "可上传"
            //};
            //_UploadStateEnumModels.Add(onyes);
            return _UploadStateEnumModels;
        }
        /// <summary>
        /// 体检状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string UploadStateFormatter(object obj)
        {
            if (obj == null)
                return "未上传";
            if (obj == "")
                return "未上传";
            if (Enum.IsDefined(typeof(UploadState), obj))
            {
                return EnumHelper.GetEnumDesc((UploadState)obj);
            }
            return obj.ToString();
        }
    }
}
