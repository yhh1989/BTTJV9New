using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.DoctorStation
{
    public partial class PersonnelDetailsControl : UserBaseControl
    {

        public PersonnelDetailsControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 当前体检人
        /// </summary>
        public ATjlCustomerRegDto CurrentInputSys
        { 
            set
            {
                if (value != null && value.Customer != null)
                {
                    //身份证号
                    textEditShenFenZhengHao.Text = value.Customer.IDCardNo;
                    //移动电话
                    textEditYiDongDianHua.Text = value.Customer.Mobile;
                    //出生日期
                    if (value.Customer.Birthday != null)
                    {
                        textEditChuShengRiQi.Text = value.Customer.Birthday.ToString();
                    }
                    //体检日期
                    if (value.LoginDate != null)
                    {
                        textEditTiJianRiQi.Text = value.LoginDate.ToString();
                    }
                    //预约日期
                    if (value.BookingDate != null)
                    {
                        textEditYuYueRiQi.Text = value.BookingDate.ToString();
                    }
                    //通信地址
                    textEditTongXunDiZhi.Text = value.Customer.Address;
                    //固定电话
                    textEditGuDingDianHua.Text = value.Customer.Telephone;
                    //客户类别
                    if (value.Customer.CustomerType != null)
                    {
                        textEditKeHuLeiBie.Text = CustomerTypeHelper.CustomerTypeFormatter(value.Customer.CustomerType);
                    }
                    else
                    {
                        textEditKeHuLeiBie.Text = CustomerTypeHelper.CustomerTypeFormatter((int)CustomerType.ordinary);
                    }
                    //保密级别
                    //textEditBaoMiJiBie.Text = value.Customer.IDCardNo;
                    //医保卡号
                    textEditYiBaoKaHao.Text = value.Customer.MedicalCard;
                    //邮箱/QQ
                    textEditYouXiangQQ.Text = value.Customer.Qq;
                }
            }
        }
        private void PersonnelDetailsControl_Load(object sender, EventArgs e)
        {
            
        }
    }
}
