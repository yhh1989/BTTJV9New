using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
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

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.LogSet
{
    public partial class FrmLogs : UserBaseForm
    {
        private readonly ICommonAppService _commonAppService;
        public FrmLogs()
        {
            _commonAppService = new CommonAppService();
            InitializeComponent();
            LookUpEditType.DataSource = LogsTypesHelper.GetJobStatusModels();
        }

        private void FrmLogs_Load(object sender, EventArgs e)
        {
            conType.Properties.DataSource= LogsTypesHelper.GetJobStatusModels();
            txtClientDegree.Properties.DataSource = DefinedCacheHelper.GetComboUsers();


            dtStar.DateTime = _commonAppService.GetDateTimeNow().Now.Date;
            dtEnd.DateTime = _commonAppService.GetDateTimeNow().Now.Date;
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            SearchOpLogDto searchOpLogDto = new SearchOpLogDto();
            if (!string.IsNullOrEmpty(txtBM.EditValue?.ToString()))
            {
                searchOpLogDto.LogBM = txtBM.EditValue?.ToString();
            }
            if (!string.IsNullOrEmpty(txtName.EditValue?.ToString()))
            {
                searchOpLogDto.LogName = txtName.EditValue?.ToString();
            }
            if (!string.IsNullOrEmpty(txtIP.EditValue?.ToString()))
            {
                searchOpLogDto.IPAddress = txtIP.EditValue?.ToString();
            }
            if (!string.IsNullOrEmpty(txtCont.ToString()))
            {
                searchOpLogDto.LogText = txtCont.EditValue?.ToString();
            }
            if (dtStar.EditValue != null && dtEnd.EditValue != null)
            {
                searchOpLogDto.StarTime = DateTime.Parse(dtStar.EditValue.ToString());
                searchOpLogDto.EndTime = DateTime.Parse(dtEnd.EditValue.ToString());
            }
            if (conType.EditValue !=null)
            {
                searchOpLogDto.LogType = (int)conType.EditValue;
            }
            if (txtClientDegree.EditValue != null && txtClientDegree.EditValue!="")
            {
                searchOpLogDto.UseId = (long)txtClientDegree.EditValue;
            }
            AutoLoading(() =>
            {
                var input = new PageInputDto<SearchOpLogDto>
                {
                    TotalPages = TotalPages,
                    CurentPage = CurrentPage,
                    Input = searchOpLogDto

                };
                var output = _commonAppService.SeachOpLog(input);
                TotalPages = output.TotalPages;
                CurrentPage = output.CurrentPage;
                InitialNavigator(dataNavigator);
                gridLogs.DataSource = output.Result;
            });
           
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ExcelHelper.ExportToExcel("人员列表", gridLogs);
        }

        private void dataNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {
            DataNavigatorButtonClick(sender, e);
            btSearch.PerformClick();
        }
    }
}
