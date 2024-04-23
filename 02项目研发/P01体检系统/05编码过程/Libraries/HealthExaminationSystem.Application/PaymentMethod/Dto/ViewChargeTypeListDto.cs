namespace Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod.Dto
{
    public class ViewChargeTypeListDto
    {
        /// <summary>
        /// 收费类型名称
        /// </summary>
        public virtual string TypeName { get; set; }
        /// <summary>
        /// 收费员
        /// </summary>
        public virtual string UserName { get; set; }
        /// <summary>
        /// 收费类型合计
        /// </summary>
        public virtual decimal? TypeTotal { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public virtual decimal? allMoney { get; set; }


        /// <summary>
        /// 作废金额
        /// </summary>
        public virtual decimal? ZFMoney { get; set; }
    }
}