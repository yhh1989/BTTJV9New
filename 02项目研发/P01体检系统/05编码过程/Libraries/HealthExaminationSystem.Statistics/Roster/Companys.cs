using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Roster
{
    public partial class Companys : UserBaseForm
    {
        private readonly IClientInfoesAppService _clientInfoesAppService; //单位接口
        private readonly IClientRegAppService _clientRegAppService;//单位预约
        public List<ClientRegDto> Dto { get; set; }
        public List<Guid> Company { get; set; }  //选择单位Id
        public int[] Num { get; set; }
        public Companys(List<ClientRegDto> Dtos, int[] Nums)
        {
            InitializeComponent();
            _clientInfoesAppService = new ClientInfoesAppService();
            _clientRegAppService = new ClientRegAppService();
            if (Dtos != null)
                Dto = Dtos;
            if (Nums != null)
                Num = Nums;
        }

        private void Company_Load(object sender, EventArgs e)
        {
            gridView1.GroupPanelText = "所有单位";
            gridView2.GroupPanelText = "单位预约信息";
            gridView2.OptionsSelection.MultiSelect = true;
            var clientinfo = _clientInfoesAppService.Query(new ClientInfoesListInput { });
            gvAll.DataSource = clientinfo;
            if (Dto != null)
            {
                gridPart.DataSource = Dto;
            }
            if (Num != null)
            {
                foreach (var item in Num)
                {
                    //this.gridView2.SelectRow(0);
                    gridView2.SelectRow(item);
                }
            }
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo hint = gridView1.CalcHitInfo(e.X, e.Y);
            if (gridView1.RowCount == 0 || /*记录数大于0*/
                    !hint.InRowCell /*有效的单元格*/||
                e.Button != MouseButtons.Left /*鼠标左键*/||
                    e.Clicks != 1  /*单击*/
                )
            {
                return;
            }

        }

        private void sblookup_Click(object sender, EventArgs e)
        {
            ClientInfoesListInput client = new ClientInfoesListInput();
            if (!string.IsNullOrWhiteSpace(teCompany.Text.Trim()))
                client.ClientName = teCompany.Text.Trim();
            var clientinfo = _clientInfoesAppService.Query(client);
            gvAll.DataSource = clientinfo;
        }


        private void simpleButtonOk_Click(object sender, EventArgs e)
        {
            List<Guid> guidlist = new List<Guid>();
            int[] selectRows = gridView2.GetSelectedRows();
            if (gridView2.FocusedRowHandle >= 0)
            {
                foreach (int num in selectRows)
                {
                    guidlist.Add(Guid.Parse(gridView2.GetRowCellValue(num, "Id").ToString()));
                    //Dto.Add((ClientInfosViewDto)gridView2.GetRow(num));
                }
            }
            Company = guidlist;
            Num = selectRows;
            if (selectRows.Length == 0)
                Dto = null;
            else
                Dto = gridPart.GetDtoListDataSource<ClientRegDto>().ToList();
            DialogResult = DialogResult.OK;
        }

        private void teCompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClientInfoesListInput client = new ClientInfoesListInput();
            if (!string.IsNullOrWhiteSpace(teCompany.Text.Trim()))
                client.ClientName = teCompany.Text.Trim();
            var clientinfo = _clientInfoesAppService.Query(client);
            gvAll.DataSource = clientinfo;
        }
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var id = Guid.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColsyId).ToString());

            var newlist = _clientRegAppService.GetAll(new EntityDto<Guid> { Id = id });
            gridPart.DataSource = null;
            gridPart.DataSource = newlist;
        }
    }
}