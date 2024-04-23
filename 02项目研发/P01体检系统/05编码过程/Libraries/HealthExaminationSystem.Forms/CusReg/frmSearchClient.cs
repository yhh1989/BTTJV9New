using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    public partial class frmSearchClient : UserBaseForm
    {
        private readonly IClientInfoesAppService _clientInfoesAppService;
        public ClientInfosViewDto clientInfoesListInput =new ClientInfosViewDto();
        public string clientName = "";
        public frmSearchClient()
        {
            InitializeComponent();
            _clientInfoesAppService = new ClientInfoesAppService();

        }
        public frmSearchClient(string _clientName) :this()
        {
            clientName = _clientName;
        }

        private void frmSearchClient_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(clientName))
            {
                Reload();
            }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        public void Reload()
        {
            treClientInfo.DataSource = null;
            AutoLoading(() =>
            {
                //var page = new PageInputDto<ClientInfoesListInput> { TotalPages = TotalPages, CurentPage = CurentPage };

                var dto = new ClientInfoesListInput();

                

                //单位名称
                if (!string.IsNullOrWhiteSpace(txtClientName.Text))
                {
                    dto.ClientName = txtClientName.Text;
                }
                if (!string.IsNullOrEmpty(clientName))
                { dto.ClientName = clientName; }
               
                AutoLoading(() =>
                {
                    var output = _clientInfoesAppService.PageFulls(new PageInputDto<ClientInfoesListInput>
                    {
                        TotalPages = TotalPages,
                        CurentPage = CurrentPage,
                        Input = dto
                    });
                    TotalPages = output.TotalPages;
                    CurrentPage = output.CurrentPage;
                    InitialNavigator(dataNavigator);
                    treClientInfo.DataSource = output.Result;
                });
                // var output = _clientInfoesAppService.Query(dto);

                //page.Input = dto;
                //var output = ClientInfoService.PageFulls(page);
                //TotalPages = output.TotalPages;
                //CurentPage = output.CurentPage;
                //InitialNavigator(dataNav);

                //treClientInfo.DataSource = output;
            });
        }

        private void txtClientName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtClientName.Text))
            {
                Reload();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var curClient= treClientInfo.GetFocusedRow() as ClientInfosViewDto;
            if (curClient != null)
            {
                clientInfoesListInput = curClient;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("请选择合适得单位");
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treClientInfo_DoubleClick(object sender, EventArgs e)
        {
            var curClient = treClientInfo.GetFocusedRow() as ClientInfosViewDto;
            if (curClient != null)
            {
                clientInfoesListInput = curClient;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
