using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Company
{
    public partial class FrmCopyTeamItem : UserBaseForm
    {
        private readonly IClientRegAppService _clientRegAppService; // 预约仓储
        private List<ClientTeamRegitemViewDto> lisTeamItem; //分组项目信息
        public Guid TeamId; //分组ID
        public Guid regId;//单位预约ID 

        public event Action<List<CreateClientTeamInfoesDto>, List<ClientTeamRegitemViewDto>> SaveTeamItem;
        public FrmCopyTeamItem()
        {
            InitializeComponent();
            _clientRegAppService = new ClientRegAppService();
        }

        /// <summary>
        /// 加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCopyTeamItem_Load(object sender, EventArgs e)
        {
            var clientReg = _clientRegAppService.getClientRegNameCom();
            //var clientReg = DefinedCacheHelper.GetClientRegNameComDto();
            schClient.Properties.DataSource = clientReg;
        }

        /// <summary>
        /// 查询分组项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var clireg_Id = schClient.EditValue;
            if (clireg_Id == null)
            {
                dxErrorProvider.SetError(schClient, string.Format(Variables.MandatoryTips, "单位名称"));
                schClient.Focus();
                return;
            }
            var crt = _clientRegAppService.QueryClientRegList(new CreateClientRegDto { Id = new Guid(clireg_Id.ToString()) });

            gridTeamInfo.DataSource = null;
            gridTeamInfo.DataSource = crt.ListClientTeam;

            lisTeamItem = crt.ListClientTeamItem;

            //gridItemInfo.DataSource = null;
            //gridItemInfo.DataSource = crt.ListClientTeamItem;
        }


        /// <summary>
        /// 点击分组显示项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvTeam_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1 && e.RowHandle >= 0)
            {
                var TeamId = gvTeam.GetRowCellValue(this.gvTeam.FocusedRowHandle, "Id").ToString();
                Guid tmId = new Guid(TeamId);
                gridItemInfo.DataSource = null;
                gridItemInfo.DataSource = lisTeamItem.Where(o => o.ClientTeamInfoId == tmId);
            }
        }

        /// <summary>
        /// 确定选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            var rowListTeam = gvTeam.GetSelectedRows();
            var rowListItem = gvItem.GetSelectedRows();
            if ((rowListTeam == null || rowListTeam.Count() == 0)&&(rowListItem==null||rowListItem.Count()==0))
            {
                ShowMessageBoxInformation("请选择要复制的分组项目信息！");
                return;
            }
            var listTeamInfo = new List<CreateClientTeamInfoesDto>();
            var listTeamItemInfo = new List<ClientTeamRegitemViewDto>();
            //选择分组信息返回分组信息与项目信息
            if (rowListTeam != null && rowListTeam.Count() > 0)
            {
                foreach (var item in rowListTeam)
                {
                    var row = gvTeam.GetRow(item) as CreateClientTeamInfoesDto;
                    listTeamInfo.Add(row);
                    var itemInfo = lisTeamItem.Where(o => o.ClientTeamInfoId == row.Id);
                    foreach (var info in itemInfo)
                    {
                        info.ClientRegId = regId;
                        info.PayerCatType= (int)PayerCatType.ClientCharge;
                    }
                    listTeamItemInfo.AddRange(itemInfo);
                }
            }
            //不选择分组信息返回项目信息
            else
            {
                if (TeamId == null || TeamId == Guid.Empty)
                {
                    ShowMessageBoxInformation("如果只选择项目信息，请从外侧选择相应的分组！");
                    return;
                }
                
                foreach (var item in rowListItem)
                {
                    var row = gvItem.GetRow(item) as ClientTeamRegitemViewDto;
                    row.Id = Guid.NewGuid();
                    row.ClientRegId = regId;
                    row.ClientTeamInfoId = TeamId;
                    row.ItemGroup = null;
                    row.PayerCatType = (int)PayerCatType.ClientCharge;
                    listTeamItemInfo.Add(row);
                }
            }
            SaveTeamItem?.Invoke(listTeamInfo,listTeamItemInfo);
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 查询单位下的分组项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void schClient_EditValueChanged(object sender, EventArgs e)
        {
            var clireg_Id = schClient.EditValue;
            if (clireg_Id == null)
            {
                dxErrorProvider.SetError(schClient, string.Format(Variables.MandatoryTips, "单位名称"));
                schClient.Focus();
                return;
            }
            var crt = _clientRegAppService.QueryClientRegList(new CreateClientRegDto { Id = new Guid(clireg_Id.ToString()) });

            gridTeamInfo.DataSource = null;
            gridTeamInfo.DataSource = crt.ListClientTeam.OrderBy(o=>o.TeamBM);

            lisTeamItem = crt.ListClientTeamItem;
        }
    }
}
