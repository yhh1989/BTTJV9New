using Abp.UI;

namespace Sw.Hospital.HealthExaminationSystem.UserException.Others
{
    public class IdNotFoundExecption : UserFriendlyException
    {
        public IdNotFoundExecption(string message, string details) : base(message, details)
        {
            Code = 90000;
        }
    }
}