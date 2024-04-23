using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;

namespace Sw.Hospital.HealthExaminationSystem.Permission
{
    public partial class PersonnelIndividuationConfigManager : UserBaseForm
    {
        private readonly IPersonnelIndividuationConfigAppService _individuationConfigAppService;

        public PersonnelIndividuationConfigManager()
        {
            InitializeComponent();

            _individuationConfigAppService = new PersonnelIndividuationConfigAppService();
        }

        private void BindingAllData()
        {
            AutoLoading(() =>
            {
                var users = _individuationConfigAppService.GetAllUsers();
                gridControlUserExtend.DataSource = users;
                gridViewUserExtend.BestFitColumns();
            });
        }

        private void simpleButtonQuery_Click(object sender, EventArgs e)
        {
            var userName = textEditAccount.Text.Trim();
            var name = textEditName.Text.Trim();
            if (string.IsNullOrWhiteSpace(userName + name))
            {
                BindingAllData();
            }
            else
            {
                AutoLoading(() =>
                {
                    var input = new SearchUserIndividuationConfigDto {UserName = userName, Name = name};
                    var users = _individuationConfigAppService.GetUsersByCondition(input);
                    gridControlUserExtend.DataSource = users;
                    gridViewUserExtend.BestFitColumns();
                });
            }
        }

        private void simpleButtonEdit_Click(object sender, EventArgs e)
        {
            if (gridViewUserExtend.GetFocusedRow() is UserIndividuationConfigsDto row)
            {
                using (var frm = new PersonnelIndividuationConfigEditor(row.Id))
                {
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        simpleButtonQuery.PerformClick();
                    }
                }
            }
            else
            {
                ShowMessageBoxWarning("请先选择一行数据！");
            }
        }

        private void gridViewUserExtend_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                simpleButtonEdit.PerformClick();
            }
        }
    }
}