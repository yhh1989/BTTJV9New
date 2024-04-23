using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.CustomerReport
{
    public partial class CompanySelection : UserBaseForm
    {
        private readonly ICommonAppService _commonAppService;
        private readonly IClientInfoesAppService _clientInfoesAppService;
        private Queue<TreeListNode> queClients = new Queue<TreeListNode>();
        public List<ClientInfosViewDto> selections = new List<ClientInfosViewDto>();
        public CompanySelection()
        {
            _commonAppService = new CommonAppService();
            _clientInfoesAppService = new ClientInfoesAppService();
            InitializeComponent();
            InitDefaultData();
        }
        /// <summary>
        /// 数据初始化
        /// </summary>
        public void InitDefaultData()
        {
            dateStart.EditValue = _commonAppService.GetDateTimeNow().Now.Date;
            dateEnd.EditValue = _commonAppService.GetDateTimeNow().Now.Date;
        }
        /// <summary>
        /// 查询单位信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchList_Click(object sender, EventArgs e)
        {
            if (dateEnd.DateTime < dateStart.DateTime)
            {
                ShowMessageBoxInformation("请重新选择时间，开始时间不能大于结束时间");
                return;
            }
            var clients = _clientInfoesAppService.Query(new ClientInfoesListInput
            {
                StartTime = this.dateStart.DateTime,
                EndTime = this.dateEnd.DateTime
            });
            tlCompany.DataSource = CreateTable(clients);
            tlCompany.ExpandToLevel(0);
        }
        /// <summary>
        /// 建立单位查询数据
        /// </summary>
        /// <param name="clients"></param>
        /// <returns></returns>
        private DataTable CreateTable(List<ClientInfosViewDto> clients)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn(nameof(ClientInfosViewDto.Id)));
            dt.Columns.Add(new DataColumn(nameof(ClientInfosViewDto.Parent)));
            dt.Columns.Add(new DataColumn(nameof(ClientInfosViewDto.ClientName)));
            DataRow dr0 = dt.NewRow();
            dr0[nameof(ClientInfosViewDto.Id)] = new EntityDto<Guid>().Id;
            dr0[nameof(ClientInfosViewDto.Parent)] = Guid.Empty;
            dr0[nameof(ClientInfosViewDto.ClientName)] = "所有单位";
            dt.Rows.Add(dr0);
            foreach (var client in clients)
            {
                DataRow dr = dt.NewRow();
                dr[nameof(ClientInfosViewDto.Id)] = client.Id;
                dr[nameof(ClientInfosViewDto.Parent)] = client.Parent;
                dr[nameof(ClientInfosViewDto.ClientName)] = client.ClientName;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 单位选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlCompany_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (e.Node.Level > 0)
            {
                List<ClientInfosViewDto> focusedClients = new List<ClientInfosViewDto>();
                // add self node
                focusedClients.Add(new ClientInfosViewDto
                {
                    Id = Guid.Parse(e.Node.GetValue(nameof(ClientInfosViewDto.Id)).ToString()),
                    ClientName = e.Node.GetValue(nameof(ClientInfosViewDto.ClientName)).ToString(),
                    Parent = e.Node.GetValue(nameof(ClientInfosViewDto.Id)) as ClientInfosViewDto
                });
                // add children nodes
                foreach (TreeListNode node in e.Node.Nodes)
                {
                    focusedClients.Add(new ClientInfosViewDto
                    {
                        Id = Guid.Parse(node.GetValue(nameof(ClientInfosViewDto.Id)).ToString()),
                        ClientName = node.GetValue(nameof(ClientInfosViewDto.ClientName)).ToString(),
                        Parent = node.GetValue(nameof(ClientInfosViewDto.Id)) as ClientInfosViewDto
                    });
                }
                clbCompany.DataSource = focusedClients;
                clbCompany.CheckAll();
            }
            else
            {
                clbCompany.DataSource = null;
            }
        }
        /// <summary>
        /// 搜索单位操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSeachNode_Click(object sender, EventArgs e)
        {
            if (SearchClients())
            {
                tlCompany.SetFocusedNode(queClients.Dequeue());
                btnNext.Enabled = true;
            }
            else
            {
                XtraMessageBox.Show("没有找到单位！", "提示");
                btnNext.Enabled = false;
            }
        }
        /// <summary>
        /// 搜索单位信息
        /// </summary>
        /// <returns></returns>
        private bool SearchClients()
        {
            queClients.Clear();
            string condition = this.txtSearchNode.EditValue.ToString().Trim();
            tlCompany.FindNodes(n => n.GetValue(nameof(ClientInfosViewDto.ClientName)).ToString().Contains(condition) || n.GetValue(nameof(ClientInfosViewDto.HelpCode)).ToString().Contains(condition)).ToList().ForEach(n => queClients.Enqueue(n));
            return queClients.Any();
        }
        /// <summary>
        /// 查找下一个单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (queClients.Any())
            {
                tlCompany.SetFocusedNode(queClients.Dequeue());
            }
            else
            {
                XtraMessageBox.Show("已查询满足条件的所有单位！","提示");
                btnNext.Enabled = false;
            }
        }
        /// <summary>
        /// 全选单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            clbCompany.CheckAll();
        }
        /// <summary>
        /// 取消全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            clbCompany.UnCheckAll();
        }
        /// <summary>
        /// 确定选择单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            foreach (ClientInfosViewDto item in clbCompany.CheckedItems)
            {
                selections.Add(item);
            }
            DialogResult = DialogResult.OK;
        }
    }
}
