using DevExpress.Utils.Win;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraGrid.Editors;
using DevExpress.XtraLayout;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Crisis;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Crisis;
using Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations;

namespace Sw.Hospital.HealthExaminationSystem.Crisis
{
    public partial class CustomerServiceCrisis : UserBaseForm
    {
        private readonly ICrisisAppService _crisisAppService;
        private ICustomerAppService customerSvr;//体检预约
        public CustomerServiceCrisis()
        {
            InitializeComponent();
            _crisisAppService = new CrisisAppService();
            customerSvr = new CustomerAppService();
        }
        /// <summary>
        /// 检索
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var input = new SearchCrisisInfosDto();
            input.CustomerBM = txtCustomerBM.Text;
            if(!string.IsNullOrWhiteSpace(txtCallBackState.Text))
                input.CallBackState = (int)txtCallBackState.EditValue;
            if (!string.IsNullOrWhiteSpace(txtMsgState.Text))
                input.MsgState = (int)txtMsgState.EditValue;
            if(txtClientRegId.EditValue!=null)
                input.ClientRegId = txtClientRegId.EditValue as List<Guid>;
            if (dateStart.EditValue != null)
            {
                var date= dateStart.DateTime.AddDays(-1);
                input.StartCheckTime =new DateTime(date.Year,date.Month,date.Day,23,59,59);
            }
            if (dateEnd.EditValue != null)
            {
                var date = dateEnd.DateTime.AddDays(1);
                input.EndCheckTime = new DateTime(date.Year,date.Month,date.Day,0,0,0);
            }
            gcCrisis.DataSource= _crisisAppService.QueryCrisisList(input);
        }

        private void CustomerServiceCrisis_Load(object sender, EventArgs e)
        {
            txtClientRegId.Properties.DataSource = customerSvr.QuereyClientRegInfos(new FullClientRegDto() { });
            txtCallBackState.Properties.DataSource = new List<EnumModel>()
            {
                new EnumModel(){Id=0,Display="否"},
                new EnumModel(){Id=1,Display="是"}
            };
            txtMsgState.Properties.DataSource = CallBackEnumHelper.GetMsgState();
        }
        #region 单位搜索框改造
        private void txtClientRegId_Popup(object sender, EventArgs e)
        {
            //得到当前SearchLookUpEdit弹出窗体
            PopupSearchLookUpEditForm form = (sender as IPopupControl).PopupWindow as PopupSearchLookUpEditForm;
            SearchEditLookUpPopup popup = form.Controls.OfType<SearchEditLookUpPopup>().FirstOrDefault();
            LayoutControl layout = popup.Controls.OfType<LayoutControl>().FirstOrDefault();
            //如果窗体内空间没有确认按钮，则自定义确认simplebutton，取消simplebutton，选中结果label
            if (layout.Controls.OfType<Control>().Where(ct => ct.Name == "btOK").FirstOrDefault() == null)
            {
                //得到空的空间
                EmptySpaceItem a = layout.Items.Where(it => it.TypeName == "EmptySpaceItem").FirstOrDefault() as EmptySpaceItem;

                //得到取消按钮，重写点击事件
                Control clearBtn = layout.Controls.OfType<Control>().Where(ct => ct.Name == "btClear").FirstOrDefault();
                LayoutControlItem clearLCI = (LayoutControlItem)layout.GetItemByControl(clearBtn);
                clearBtn.Click += clearBtn_Click;

                //添加一个simplebutton控件(确认按钮)
                LayoutControlItem myLCI = (LayoutControlItem)clearLCI.Owner.CreateLayoutItem(clearLCI.Parent);
                myLCI.TextVisible = false;
                SimpleButton btOK = new SimpleButton() { Name = "btOK", Text = "确定" };
                btOK.Click += btOK_Click;
                myLCI.Control = btOK;
                myLCI.SizeConstraintsType = SizeConstraintsType.Custom;//控件的大小设置为自定义
                myLCI.MaxSize = clearLCI.MaxSize;
                myLCI.MinSize = clearLCI.MinSize;
                myLCI.Move(clearLCI, DevExpress.XtraLayout.Utils.InsertType.Left);

                //添加一个label控件（选中结果显示）
                LayoutControlItem msgLCI = (LayoutControlItem)clearLCI.Owner.CreateLayoutItem(a.Parent);
                msgLCI.TextVisible = false;
                msgLCI.Move(a, DevExpress.XtraLayout.Utils.InsertType.Left);
                msgLCI.BestFitWeight = 100;
            }
        }
        private void clearBtn_Click(object sender, EventArgs e)
        {
            //luValues.Clear();//将保存的数据清空
            searchLookUpEdit1View.ClearSelection();
            txtClientRegId.EditValue = null;
            txtClientRegId.ToolTip = "";
        }
        private void btOK_Click(object sender, EventArgs e)
        {
            txtClientRegId.ClosePopup();
        }

        private void txtClientRegId_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            if (txtClientRegId.EditValue != null)
                e.DisplayText = txtClientRegId.ToolTip;
            else
                e.DisplayText = "";
        }

        private void txtClientRegId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //清除
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                searchLookUpEdit1View.ClearSelection();
                //luValues.Clear();
                txtClientRegId.EditValue = null;
                txtClientRegId.ToolTip = "";
            }
        }

        private void txtClientRegId_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            var row= searchLookUpEdit1View.GetSelectedRows();
            if (row.Count() > 0)
            {
                var str = "";
                var values = new List<Guid>();
                foreach(var r in row)
                {
                    var data= searchLookUpEdit1View.GetRow(r) as Application.CusReg.Dto.ClientRegDto;
                    str += data.ClientInfo.ClientName+"，";
                    values.Add(data.Id);
                }
                //txtClientRegId.Text = str;
                txtClientRegId.ToolTip = str;
                txtClientRegId.EditValue = values;
            }
        }
        #endregion
        /// <summary>
        /// 双击显示详情，写回访记录
        /// </summary>
        private void gvCrisis_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks != 2)
                return;
            if (e.RowHandle < 0)
                return;
            OpenCallbackList();
        }
        /// <summary>
        /// 详情按钮
        /// </summary>
        private void btnDetail_Click(object sender, EventArgs e)
        {
            OpenCallbackList();
        }



        /// <summary>
        /// 加载详情窗体
        /// </summary>
        private void OpenCallbackList()
        {
            var id = gvCrisis.GetFocusedRowCellValue( nameof(CustomerServiceCrisisViewDto.Id));
            using (var frm = new EditCallBackList())
            {
                frm.CrisisSetId = (Guid)id;
                var dr = frm.ShowDialog();
                btnSearch.PerformClick();
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            var fileName = saveFileDialog.FileName;
            ExcelHelper.ExportToExcel(fileName,gcCrisis);
        }

        private void gvCrisis_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if(e.Column.FieldName==gConMsg.FieldName)
            {
                if (e.Value != null)
                    e.DisplayText = EnumHelper.GetEnumDesc((CrisisMsgStatecs)e.Value);
                else
                    e.DisplayText = EnumHelper.GetEnumDesc(CrisisMsgStatecs.Not);
            }
            if (e.Column.FieldName == gConState.FieldName)
            {
                e.DisplayText = "否";
                if (e.Value != null)
                    if ((int)e.Value == 1)
                        e.DisplayText = "是";
            }
            if (e.Column.FieldName == gConSex.FieldName)
            {
                if (e.Value != null)
                    e.DisplayText = EnumHelper.GetEnumDesc((Sex)e.Value);
            }
        }
    }
}