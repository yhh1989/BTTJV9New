using System;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy
{
    public class UserFriendlyException : ApplicationException
    {
        public int Code { get; set; }

        public string Description { get; set; }

        public MessageBoxButtons Buttons { get; set; }

        public MessageBoxIcon Icon { get; set; }
    }
}