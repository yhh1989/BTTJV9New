using Abp.UI;

namespace Sw.Hospital.HealthExaminationSystem.UserException.Verification
{
    public class FieldVerifyException : UserFriendlyException
    {
        public FieldVerifyException(string message, string details) : base(message, details)
        {
            Code = 10000;
        }
    }
}