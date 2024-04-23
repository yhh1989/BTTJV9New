using DevExpress.Utils;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.DoctorStation
{
    public partial class UpdateCustomerChecked : UserBaseForm
    {
        /// <summary>
        /// 医生站
        /// </summary>
        private readonly IDoctorStationAppService _doctorStation;
        private readonly ICommonAppService _commonAppService;
        public UpdateCustomerChecked()
        {
            InitializeComponent();
            _doctorStation = new DoctorStationAppService();
            _commonAppService = new CommonAppService();
        }


        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSearch_Click(object sender, EventArgs e)
        {
            
                QueryClass query = new QueryClass();
                query.CustomerBM = textBoxCustomerBM.Text;
                if(!string.IsNullOrEmpty(lookUpEditSumStatus.EditValue?.ToString()) 
                && lookUpEditSumStatus.EditValue?.ToString() !="0")
                {
                    query.SummSate = int.Parse(lookUpEditSumStatus.EditValue?.ToString());
                }
                if (!string.IsNullOrEmpty(cbo_xmzh.EditValue?.ToString()))
                {
                    query.CustomerItemGroupId = Guid.Parse(cbo_xmzh.EditValue?.ToString());
                }
                if (!string.IsNullOrEmpty(dateEditStart.EditValue?.ToString()))
                {
                    query.LastModificationTimeBign = dateEditStart.DateTime;
                }
                if (!string.IsNullOrEmpty(dateEditEnd.EditValue?.ToString()))
                {
                    query.LastModificationTimeEnd = dateEditEnd.DateTime.AddDays(1);
                }
                var Customer = _doctorStation.GetCustomerRegList(query).OrderBy(P=>P.CustomerBM).ToList();
                gridControl1.DataSource = Customer;
                //if (Customer!=null)
                //{
                //    //体检号
                //    labelControlCustomerBM.Text = Customer.CustomerBM;
                //    //姓名
                //    labelControlCustomerName.Text = Customer.Customer.Name;
                //    //性别
                //    if (int.TryParse(Customer.Customer.Sex.ToString(), out var resultSex))
                //    {
                //        labelControlCustomerSex.Text = EnumHelper.GetEnumDesc((Sex)resultSex);
                //    }
                //    else
                //    {
                //        labelControlCustomerSex.Text = string.Empty;
                //    }
                //    //年龄
                //    labelControlCustomerAge.Text = Customer.Customer.Age?.ToString();
                //    //单位
                //    if (Customer.ClientInfo != null)
                //    {
                //        labelControlCustomerClient.Text = Customer.ClientInfo.ClientName;
                //    }
                //    //交表状态
                //    if (int.TryParse(Customer.SendToConfirm.ToString(), out var resultSendToConfirm))
                //    {
                //        labelControlSendToConfirm.Text = EnumHelper.GetEnumDesc((SendToConfirm)resultSendToConfirm);
                //    }
                //    else
                //    {
                //        labelControlSendToConfirm.Text = string.Empty;
                //    }
                //    //体检状态
                //    if(int.TryParse(Customer.CheckSate.ToString(), out var resultCheckSate))
                //    {
                //        labelControlCheckSate.Text = EnumHelper.GetEnumDesc((PhysicalEState)resultCheckSate);
                //    }
                //    else
                //    {
                //        labelControlCheckSate.Text = string.Empty;
                //    }
                //    //总检状态
                //    if (int.TryParse(Customer.SummSate.ToString(), out var resultSummSate))
                //    {
                //        labelControlSummSate.Text = EnumHelper.GetEnumDesc((SummSate)resultSummSate);
                //    }
                //    else
                //    {
                //        labelControlSummSate.Text = string.Empty;
                //    }
                //}
                //else
                //{
                //    alertControl.Show(this, "温馨提示", "没有找到这个人哦,检查一下体检号对不对!");
                //}
         
        }
        /// <summary>
        /// 修改患者总检和体检状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonReset_Click(object sender, EventArgs e)
        {
            var selectIndexes = gridView1.GetSelectedRows();
            if (selectIndexes.Length != 0)
            {
                //string err = "";
                foreach (var index in selectIndexes)
                {
                    var customerBM = gridView1.GetRowCellValue(index, conCustomerBM)?.ToString();
                    var cus = gridView1.GetRow(index) as ATjlCustomerRegDto;
                    //var strName = gridView1.GetRowCellValue(index, conName)?.ToString();
                    QueryClass query = new QueryClass();
                    query.CustomerBM = cus.CustomerBM;
                    var Is = _doctorStation.ResetCustomerChecked(query);
                    if (Is)
                    {

                        //日志                  
                        CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                        createOpLogDto.LogBM = customerBM;
                        createOpLogDto.LogName = cus.Customer.Name;
                        createOpLogDto.LogText = "重置状态";
                        createOpLogDto.LogDetail = $"重置前：交表状态：{cus.SummSate},体检状态：{cus.CheckSate}，总检状态：{cus.SummSate}";
                        createOpLogDto.LogType = (int)LogsTypes.SumId;
                        _commonAppService.SaveOpLog(createOpLogDto);

                    }

                }
                MessageBox.Show("重置状态成功！");
                simpleButtonSearch.PerformClick();
            }
        }

        private void UpdateCustomerChecked_Load(object sender, EventArgs e)
        {
            //SummSate
            lookUpEditSumStatus.Properties.DataSource = SummSateHelper.GetSelectList();
            cbo_xmzh.Properties.DataSource = DefinedCacheHelper.GetItemGroups();
            cbo_xmzh.Properties.ValueMember = "Id";
            cbo_xmzh.Properties.DisplayMember = "ItemGroupName";

            // 初始化时间框
            var date = _commonAppService.GetDateTimeNow().Now.ToShortDateString();
            dateEditEnd.DateTime =DateTime.Parse( date);
            dateEditStart.DateTime = DateTime.Parse(date);
            //体检状态
            gridView1.Columns[conCheckSate.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns[conCheckSate.FieldName].DisplayFormat.Format =
                new CustomFormatter(CheckSateHelper.PhysicalEStateFormatter);

            //总检状态
            gridView1.Columns[conSummSate.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns[conSummSate.FieldName].DisplayFormat.Format =
                new CustomFormatter(SummSateHelper.SummSateFormatter);

            //交表状态
            gridView1.Columns[conSendToConfirm.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns[conSendToConfirm.FieldName].DisplayFormat.Format =
                new CustomFormatter(SendToConfirmHelper.SendToConfirmFormatter);

            //性别状态            
            gridView1.Columns[conSex.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns[conSex.FieldName].DisplayFormat.Format =
                new CustomFormatter(SexHelper.CustomSexFormatter);
        }

        private void textBoxCustomerBM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(textBoxCustomerBM.Text))
            {

                AutoLoading(() =>
                {

                    QueryClass query = new QueryClass();
                    query.CustomerBM = textBoxCustomerBM.Text;                   
                    var Customer = _doctorStation.GetCustomerRegList(query).OrderBy(P => P.CustomerBM).ToList();
                   
                    if (checkAll.Checked == true)
                    {
                        var cusreg = gridControl1.DataSource as List<ATjlCustomerRegDto>;
                        if (cusreg != null)
                        {
                            cusreg.AddRange(Customer);
                            gridControl1.DataSource = cusreg;
                            gridView1.BestFitColumns();
                        }
                        else
                        {
                            gridControl1.DataSource = Customer;
                            gridView1.BestFitColumns();
                        }
                        textBoxCustomerBM.SelectAll();
                    }
                    else
                    {
                        gridControl1.DataSource = Customer;
                        gridView1.BestFitColumns();
                    }
                });
            }
        }
    }
}
