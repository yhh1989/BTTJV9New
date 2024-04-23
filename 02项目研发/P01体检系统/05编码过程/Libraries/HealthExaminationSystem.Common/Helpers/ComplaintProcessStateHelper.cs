using System.Collections.Generic;
using System.Collections.ObjectModel;
using HealthExaminationSystem.Enumerations;
using Sw.Hospital.HealthExaminationSystem.Common.Models;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 投诉处理状态帮助器
    /// </summary>
    public static class ComplaintProcessStateHelper
    {
        /// <summary>
        /// 投诉紧急级别列表
        /// </summary>
        public static ReadOnlyCollection<EnumModel<ComplaintProcessState>> ComplaintProcessStateCollection { get; }

        /// <summary>
        /// 静态初始化投诉处理状态帮助器
        /// </summary>
        static ComplaintProcessStateHelper()
        {
            ComplaintProcessStateCollection = new ReadOnlyCollection<EnumModel<ComplaintProcessState>>(new List<EnumModel<ComplaintProcessState>>
            {
                new EnumModel<ComplaintProcessState>
                {
                    Id = (int)ComplaintProcessState.Undisposed,
                    Name = ComplaintProcessState.Undisposed.ToString("G"),
                    Display = "未处理",
                    EnumFiled = ComplaintProcessState.Undisposed
                },
                new EnumModel<ComplaintProcessState>
                {
                    Id = (int)ComplaintProcessState.Processed,
                    Name = ComplaintProcessState.Processed.ToString("G"),
                    Display = "已处理",
                    EnumFiled = ComplaintProcessState.Processed
                },
                new EnumModel<ComplaintProcessState>
                {
                    Id = (int)ComplaintProcessState.Ignored,
                    Name = ComplaintProcessState.Ignored.ToString("G"),
                    Display = "忽略",
                    EnumFiled = ComplaintProcessState.Ignored
                },
                new EnumModel<ComplaintProcessState>
                {
                    Id = (int)ComplaintProcessState.Reported,
                    Name = ComplaintProcessState.Reported.ToString("G"),
                    Display = "已上报",
                    EnumFiled = ComplaintProcessState.Reported
                }
            });
        }
    }
}