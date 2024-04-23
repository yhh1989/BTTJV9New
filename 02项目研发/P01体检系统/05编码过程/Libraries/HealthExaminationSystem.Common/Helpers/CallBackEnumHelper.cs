using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System.Collections.Generic;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public class CallBackEnumHelper
    {
        public static List<EnumModel> GetCallBackState()
        {
            var result = new List<EnumModel>();
            var complete = new EnumModel
            {
                Id = (int)CallBackState.Complete,
                Display = "完成"
            };
            var close = new EnumModel
            {
                Id = (int)CallBackState.Close,
                Display = "关闭"
            };
            result.Add(complete);
            result.Add(close);
            return result;
        }
        public static List<EnumModel> GetCallBackType()
        {
            var result = new List<EnumModel>();
            var wechat = new EnumModel
            {
                Id = (int)CallBackType.Wechat,
                Display = "微信"
            };
            result.Add(wechat);
            var phone = new EnumModel
            {
                Id = (int)CallBackType.Phone,
                Display = "电话"
            };
            result.Add(phone);
            var message = new EnumModel
            {
                Id = (int)CallBackType.Message,
                Display = "短信"
            };
            result.Add(message);
            var arrival = new EnumModel
            {
                Id = (int)CallBackType.Arrival,
                Display = "到店"
            };
            result.Add(arrival);
            return result;
        }
        public static List<EnumModel> GetMsgState()
        {
            var result = new List<EnumModel>();
            var not = new EnumModel
            {
                Id = (int)CrisisMsgStatecs.Not,
                Display = "未发送"
            };
            result.Add(not);
            var send = new EnumModel
            {
                Id = (int)CrisisMsgStatecs.Send,
                Display = "已发送"
            };
            result.Add(send);
            var sendfail = new EnumModel
            {
                Id = (int)CrisisMsgStatecs.SendFail,
                Display = "发送失败"
            };
            result.Add(sendfail);
            return result;
        }
    }
}
