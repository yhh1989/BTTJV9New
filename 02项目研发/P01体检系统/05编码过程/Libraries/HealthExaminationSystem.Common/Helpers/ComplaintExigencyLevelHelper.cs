using System.Collections.Generic;
using System.Collections.ObjectModel;
using HealthExaminationSystem.Enumerations;
using Sw.Hospital.HealthExaminationSystem.Common.Models;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 投诉紧急级别帮助器
    /// </summary>
    public static class ComplaintExigencyLevelHelper
    {
        /// <summary>
        /// 投诉紧急级别列表
        /// </summary>
        public static ReadOnlyCollection<EnumModel<ComplaintExigencyLevel>> ComplaintExigencyLevelCollection { get; }

        /// <summary>
        /// 静态初始化投诉紧急级别帮助器
        /// </summary>
        static ComplaintExigencyLevelHelper()
        {
            ComplaintExigencyLevelCollection = new ReadOnlyCollection<EnumModel<ComplaintExigencyLevel>>(new List<EnumModel<ComplaintExigencyLevel>>
            {
                new EnumModel<ComplaintExigencyLevel>
                {
                    Id = (int)ComplaintExigencyLevel.First,
                    Name = ComplaintExigencyLevel.First.ToString("G"),
                    Display = "一级",
                    EnumFiled = ComplaintExigencyLevel.First
                },
                new EnumModel<ComplaintExigencyLevel>
                {
                    Id = (int)ComplaintExigencyLevel.Second,
                    Name = ComplaintExigencyLevel.Second.ToString("G"),
                    Display = "二级",
                    EnumFiled = ComplaintExigencyLevel.Second
                },
                new EnumModel<ComplaintExigencyLevel>
                {
                    Id = (int)ComplaintExigencyLevel.Third,
                    Name = ComplaintExigencyLevel.Third.ToString("G"),
                    Display = "三级",
                    EnumFiled = ComplaintExigencyLevel.Third
                },
                new EnumModel<ComplaintExigencyLevel>
                {
                    Id = (int)ComplaintExigencyLevel.Fourth,
                    Name = ComplaintExigencyLevel.Fourth.ToString("G"),
                    Display = "四级",
                    EnumFiled = ComplaintExigencyLevel.Fourth
                },
                new EnumModel<ComplaintExigencyLevel>
                {
                    Id = (int)ComplaintExigencyLevel.Fifth,
                    Name = ComplaintExigencyLevel.Fifth.ToString("G"),
                    Display = "五级",
                    EnumFiled = ComplaintExigencyLevel.Fifth
                }
            });
        }
    }
}