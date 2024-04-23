using Abp.Application.Services.Dto;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Company
{
    public partial class FrmTjlClientTeamAdd : UserBaseForm
    {
        private readonly IClientRegAppService _clientRegAppService;
        public List<CreateClientTeamInfoesDto> TeamInfoesList;
        public ClientTeamRegDto cteDto;
        public CreateClientRegDto ClientReg;
        public List<Guid> Risk = new List<Guid>();
        private IChargeAppService _chargeAppService { get; set; }
        public FrmTjlClientTeamAdd()
        {
            InitializeComponent();
            _clientRegAppService = new ClientRegAppService();
            _chargeAppService = new ChargeAppService();
            //bandedGridView1.Columns[11].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //bandedGridView1.Columns[11].DisplayFormat.Format = new CustomFormatter(SumTJRS);//体检人数
            //bandedGridView1.Columns[12].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //bandedGridView1.Columns[12].DisplayFormat.Format = new CustomFormatter(SumSJRS);//已检人数
            //bandedGridView1.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            //bandedGridView1.Columns[5].DisplayFormat.Format = new CustomFormatter(SumWJRS);//未检人数
        }

        #region 系统事件
        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (getFZState())
            {
                ShowMessageBoxInformation("单位已封账,不能执行该操作！");
                return;
            }
            cteDto = new ClientTeamRegDto();
            cteDto = _clientRegAppService.getClientRegList(new EntityDto<Guid> { Id = ClientReg.Id });;
            int teamCode = cteDto.ListClientTeam.Count + 1;
            using (FrmClientTeamAdd frmclientTeamAdd = new FrmClientTeamAdd(ClientReg.Id, null) { EditMode = (int)EditModeType.Add, teamCode = teamCode })
            {
                frmclientTeamAdd.TeamInfoesList = TeamInfoesList;
                frmclientTeamAdd.SaveDataComplate += (TeamInfoes) =>
                {
                    TeamInfoesList.Add(TeamInfoes);
                };

                if (frmclientTeamAdd.ShowDialog() == DialogResult.OK)
                {
                    gridTeamInfoes.DataSource = null;
                    reload();
                }
            }

        }
        #endregion

        #region 人员管理
        private void btnUserList_Click(object sender, EventArgs e)
        {
            FrmClientRegCustomerList frmcuslist = new FrmClientRegCustomerList();
            frmcuslist.Show();
        }
        #endregion 人员管理

        private void FrmTjlClientTeamAdd_Load(object sender, EventArgs e)
        {
            //行号宽度
            bandedGridView1.IndicatorWidth = 30;
            reload();
        }
        public void reload()
        {
            if (ClientReg != null)
                if (ClientReg.StartCheckDate != null && ClientReg.EndCheckDate != null)
                    foreach (var item in TeamInfoesList)
                    {
                        item.StartDate = ClientReg.StartCheckDate;
                        item.EndDate = ClientReg.EndCheckDate;
                    }

            gridTeamInfoes.DataSource = TeamInfoesList.OrderBy(o=>o.TeamBM)?.ToList();
            bandedGridView1.Columns[CostType.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            bandedGridView1.Columns[CostType.FieldName].DisplayFormat.Format = new CustomFormatter(CostStateHelper.CostStateFormatter);
            bandedGridView1.Columns[Type.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            bandedGridView1.Columns[Type.FieldName].DisplayFormat.Format = new CustomFormatter(CheckSateHelper.ExaminationFormatter);
        }
        private void simpleButton6_Click(object sender, EventArgs e)
        {

        }


        private void bandedGridView1_DoubleClick(object sender, EventArgs e)
        {
            int selectRow = bandedGridView1.GetSelectedRows()[0];
            string id = this.bandedGridView1.GetRowCellValue(selectRow, "Id").ToString();
        }
        /// <summary>
        /// 回传数据方法
        /// </summary>
        public event Action<List<CreateClientTeamInfoesDto>> SaveDataTeamInfoes;

        private void gridTeamInfoes_Click(object sender, EventArgs e)
        {

        }

        #region 方法
        //FormatCheckSate


        private string FormatClienCheck(object arg)
        {
            if (arg.ToString() == "1")
            {
                return "完成";
            }
            else
            {
                return "未完成";
            }
        }
        private string FormatCheckSate(object arg)
        {
            if (arg.ToString() == "1")
            {
                return "未到检";
            }
            else
            {
                return "已到检";
            }
        }
        //private string FormatSexs(object arg)
        //{
        //    try
        //    {

        //        return _sexModels.Find(r => r.Id == (int)arg).Display;
        //    }
        //    catch
        //    {
        //        return _sexModels.Find(r => r.Id == (int)Sw.Hospital.HealthExaminationSystem.Common.Enums.Sex.GenderNotSpecified).Display;
        //    }
        //}
        private string SumTJRS(object arg)
        {

            string TJRS = "";
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            TJRS = ChargeCus.Count.ToString();
            return TJRS;

        }
        //SumSJ
        private string SumSJRS(object arg)
        {
            string TJRS = "";
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            TJRS = ChargeCus.Where(r => r.CheckSate != 1).Count().ToString();
            return TJRS;
        }

        private string SumWJRS(object arg)
        {
            string TJRS = "";
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            TJRS = ChargeCus.Where(r => r.CheckSate == 1).Count().ToString();
            return TJRS;
        }
        private string SumSJMoney(object arg)
        {

            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            var McusPayMoneys = ChargeCus.Where(r => r.CheckSate != 1).Select(r => r.McusPayMoney);
            var sumMcusPayMoneys = McusPayMoneys?.Sum(r => r?.ClientMoney);
            return sumMcusPayMoneys.Value.ToString();
        }
        //SumJX
        private string SumAddMoney(object arg)
        {
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            var McusPayMoneys = ChargeCus.Select(r => r.McusPayMoney);
            var sumMcusPayMoneys = McusPayMoneys?.Sum(r => r?.ClientAddMoney);
            return sumMcusPayMoneys.Value.ToString();
        }
        private string SumYSMoney(object arg)
        {
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            var McusPayMoneys = ChargeCus.Select(r => r.McusPayMoney);
            var sumMcusPayMoneys = McusPayMoneys?.Sum(r => r?.ClientAddMoney);
            return sumMcusPayMoneys.Value.ToString();
        }
        private string SumJQX(object arg)
        {
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            var McusPayMoneys = ChargeCus.Select(r => r.McusPayMoney);
            var sumMcusPayMoneys = McusPayMoneys?.Sum(r => r?.ClientMoney);
            return sumMcusPayMoneys.Value.ToString();
        }
        private string SumTXAddMoney(object arg)
        {
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            var McusPayMoneys = ChargeCus.Select(r => r.McusPayMoney);
            var sumMcusPayMoneys = McusPayMoneys?.Sum(r => r?.ClientAdjustAddMoney);
            return sumMcusPayMoneys.Value.ToString();
        }
        private string SumTXJXMoney(object arg)
        {
            ICollection<ChargeCusStateDto> ChargeCus = (ICollection<ChargeCusStateDto>)arg;
            var McusPayMoneys = ChargeCus.Select(r => r.McusPayMoney);
            var sumMcusPayMoneys = McusPayMoneys?.Sum(r => r?.ClientAdjustMinusMoney);
            string ss = sumMcusPayMoneys.Value.ToString();
            return sumMcusPayMoneys.Value.ToString();
        }


        #endregion


        private void butClose_Click(object sender, EventArgs e)
        {
            SaveDataTeamInfoes?.Invoke(TeamInfoesList);
            this.DialogResult = DialogResult.OK;
        }

        private void butUpdate_Click(object sender, EventArgs e)
        {
            if (getFZState())
            {
                ShowMessageBoxInformation("单位已封账,不能执行该操作！");
                return;
            }
            var TeamId = bandedGridView1.GetRowCellValue(this.bandedGridView1.FocusedRowHandle, "Id").ToString();
            if (TeamId != null && TeamId != "")
            {
                var teamGuid = new Guid(TeamId);
                var regTeam = TeamInfoesList.FirstOrDefault(o => o.Id == teamGuid);
                using (FrmClientTeamAdd frmTeamAdd = new FrmClientTeamAdd() { EditMode = (int)EditModeType.Edit, TeamInfoes = regTeam })
                {
                    frmTeamAdd.SaveDataComplate += (TeamInfoes) =>
                    {


                    };
                    if (frmTeamAdd.ShowDialog() == DialogResult.OK)
                    {
                        gridTeamInfoes.DataSource = null;
                        reload();
                    }
                }

            }
            else
            {
                ShowMessageSucceed("请选择一行数据");
            }
        }

        private void butDel_Click(object sender, EventArgs e)
        {
            if (getFZState())
            {
                ShowMessageBoxInformation("单位已封账,不能执行该操作！");
                return;
            }
            var TeamId = bandedGridView1.GetRowCellValue(this.bandedGridView1.FocusedRowHandle, "Id").ToString();
            var teamGuid = new Guid(TeamId);
            var regTeam = TeamInfoesList.FirstOrDefault(o => o.Id == teamGuid);
            TeamInfoesList.Remove(regTeam);
        }

        private void bandedGridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                butUpdate_Click(sender, e);
            }
        }

        private void bandedGridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
        private bool getFZState()
        {
            bool isFZState = false;
            if (ClientReg != null && ClientReg.Id !=Guid.Empty)
            {
                var FZSt = _chargeAppService.GetZFState(new EntityDto<Guid> { Id = ClientReg.Id });
                if (FZSt == 1)
                {
                    return true;
                }
            }
            return isFZState;

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (getFZState())
            {
                ShowMessageBoxInformation("单位已封账,不能执行该操作！");
                return;
            }
        }
    }
}

