using DevExpress.XtraEditors.Controls;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.FrmMakeCollect;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.FrmMakeCollect;
using Sw.Hospital.HealthExaminationSystem.Application.FrmMakeCollect.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.PersonnelCategorys;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.CusRegStatis
{
    public partial class frmCusRegStatiscs : UserBaseForm
    {
        public IFrmMakeCollectAppService frmMakeCollectAppService = new FrmMakeCollectAppService();
        private readonly ICommonAppService _commonAppService;
        private DateTime stardate = new DateTime();

        private IPersonnelCategoryAppService _personnelCategoryAppService;
        public DateTime? olde = new DateTime?();

        public InIdDto inIdDto = new InIdDto();
        public frmCusRegStatiscs()
        {
            _personnelCategoryAppService = new PersonnelCategoryAppService();
            _commonAppService = new CommonAppService();
            InitializeComponent();
        }

        private void frmCusRegStatiscs_Load(object sender, EventArgs e)
        {
            var clienttype = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == (BasicDictionaryType.ClientSource).ToString())?.ToList();
            lookRegType.Properties.DataSource = clienttype;

          dateDate.DateTime=DateTime.Parse( _commonAppService.GetDateTimeNow().Now.ToShortDateString());

            repositoryItemLookUpEdit2.DataSource = clienttype;
            lookClient.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();
            //人员类别
            repositoryItemLookUpEdit1.DataSource = _personnelCategoryAppService.QueryCategoryList(new Application.PersonnelCategorys.Dto.PersonnelCategoryViewDto()).Where(o => o.IsActive == true)?.ToList();
            if (olde.HasValue)
            { dateDate.EditValue = olde.Value;
                simpleButton3.PerformClick();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

            ShowMakeCollectDto show1 = new ShowMakeCollectDto();           
                show1.StartDataTime = (DateTime)dateDate.EditValue;
            show1.EndDataTime = (DateTime)dateDate.EditValue;
            stardate = (DateTime)dateDate.EditValue;
            var list = frmMakeCollectAppService.GetShowMakeCollects(show1);
            if (list.Count > 0)
            {
                laballCount.Text = list[0].allcout.ToString();
                labClientCout.Text = list[0].dw.ToString();
                labPCCount.Text = list[0].xx.ToString();

                labOnLineCount.Text = list[0].xs.ToString();
                labPer.Text = list[0].gr.ToString();
                simpleLabelItem1.Text = "预约时间：" + list[0].BookingDate;

                labDepatCount.Text = string.Join("     ", list[0].departlist.Select(o => o.departname + "("+ o.count+"人)"));

                InIdDto InseachCusReg = new InIdDto();
                if (dateDate.DateTime != null)
                {
                    InseachCusReg.BookingDate = (DateTime)dateDate.EditValue;
                }
                if (comTime.SelectedIndex > 0)
                {
                    InseachCusReg.TimeSoft = comTime.SelectedIndex;
                }
                if (lookUpState.SelectedIndex > 0)
                {
                    InseachCusReg.RegState = lookUpState.SelectedIndex;
                }
                if (lookRegType.EditValue != null)
                {
                    InseachCusReg.RegType = (int)lookRegType.EditValue;
                }
                if (!string.IsNullOrEmpty(textName.Text))
                { InseachCusReg.SearchName = textName.Text; }
                var departlist = frmMakeCollectAppService.getDepartCount(InseachCusReg);
                gridControl1.DataSource = departlist;

                //人员
                inIdDto = InseachCusReg;
                var cuslist = frmMakeCollectAppService.getRegCusLis(InseachCusReg);
                gridControl2.DataSource = cuslist;

            }
            else
            {
                List<OutDepartCusDto> outDepartCusDtos = new List<OutDepartCusDto>(); 
                gridControl1.DataSource = outDepartCusDtos;
                List<OutRegCusListDto> outRegCusListDtos = new List<OutRegCusListDto>();
                gridControl2.DataSource = outRegCusListDtos;
            }
            
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            

            var colName = gridView1.FocusedColumn.Caption;
                if (gridView1.IsGroupRow(gridView1.FocusedRowHandle))
                {

                colName = "科室名称";
                }
                if (colName == "科室名称")
                {
                var dto = gridControl2.GetFocusedRowDto<OutDepartCusDto>();
                if (dto !=null)
                    {
                        InIdDto inIdDtols = new InIdDto();
                    inIdDtols = inIdDto;
                    inIdDtols.DepartId = dto.DepartId;
                   var list= frmMakeCollectAppService.getRegCusLis(inIdDtols);
                        gridControl2.DataSource = list;
                    }
                }
                else
                {
                    if (gridView1.GetFocusedRowCellValue(conGroupId) is Guid id)
                    {
                       

                    InIdDto inIdDtols = new InIdDto();
                    inIdDtols = inIdDto;
                    inIdDtols.GroupId = id;
                    var list = frmMakeCollectAppService.getRegCusLis(inIdDto);
                        gridControl2.DataSource = list;
                    }
                }
           
        }

        private void gridView1_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            //var colName = gridView1.FocusedColumn.Caption;
            //if (gridView1.IsGroupRow(gridView1.FocusedRowHandle))
            //{
            //    var childRowHandle = gridView1.GetChildRowHandle(gridView1.FocusedRowHandle, 0);
            //    colName = gridView1.IsGroupRow(childRowHandle) ? gridColumn1.Caption : gridColumn2.Caption;
            //}
            //if (colName == "科室名称")
            //{
            //    if (gridView1.GetFocusedRowCellValue(conDepartId) is Guid id)
            //    {
            //        InIdDto inIdDto = new InIdDto();
            //        inIdDto.DepartId = id;
            //        inIdDto.BookingDate = stardate;
            //        var list = frmMakeCollectAppService.getRegCusLis(inIdDto);
            //        gridControl2.DataSource = list;
            //    }
            //}
            //else
            //{
            //    if (gridView1.GetFocusedRowCellValue(conGroupId) is Guid id)
            //    {
            //        InIdDto inIdDto = new InIdDto();
            //        inIdDto.GroupId = id;
            //        inIdDto.BookingDate = stardate;
            //        var list = frmMakeCollectAppService.getRegCusLis(inIdDto);
            //        gridControl2.DataSource = list;
            //    }
            //}
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            dateDate.DateTime = DateTime.Parse(dateDate.DateTime.AddDays(-1).ToShortDateString());
            simpleButton3.PerformClick();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            dateDate.DateTime = DateTime.Parse(dateDate.DateTime.AddDays(1).ToShortDateString());
            simpleButton3.PerformClick();
        }

        private void lookRegType_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                lookRegType.EditValue = null;
            }
        }
    }
}
