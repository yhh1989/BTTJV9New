using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;

namespace Sw.Hospital.HealthExaminationSystem.Permission
{
    public partial class PersonnelIndividuationConfigEditor : UserBaseForm
    {
        private readonly IPersonnelIndividuationConfigAppService _individuationConfigAppService;

        private readonly long _userId;

        private bool IsCreate;

        public PersonnelIndividuationConfigEditor(long id)
        {
            InitializeComponent();

            _individuationConfigAppService = new PersonnelIndividuationConfigAppService();
            _userId = id;
        }

        private void PersonnelIndividuationConfigEditor_Load(object sender, EventArgs e)
        {
            var startupMenuBars = _individuationConfigAppService.GetAllStartupMenuBars();
            checkedListBoxControlStartupMenuBars.DataSource = startupMenuBars;
            var user = _individuationConfigAppService.GetUserById(new EntityDto<long> { Id = _userId });
            if (user.IndividuationConfig != null)
            {
                IsCreate = false;
                var individuationConfig = user.IndividuationConfig;
                checkEditIsActive.Checked = individuationConfig.IsActive;
                checkEditAdvancedAlwaysCheck.Checked = individuationConfig.AdvancedAlwaysCheck;
                if (user.IndividuationConfig.StartupMenuBars != null)
                {
                    foreach (var bar in user.IndividuationConfig.StartupMenuBars)
                    {
                        var index = startupMenuBars.FindIndex(r => r.Id == bar.Id);
                        checkedListBoxControlStartupMenuBars.SetItemChecked(index, true);
                    }
                }
            }
            else
            {
                IsCreate = true;
            }

        }

        private void simpleButtonOk_Click(object sender, EventArgs e)
        {
            AutoLoading(() =>
            {
                var input = new CreateOrUpdatePersonnelIndividuationConfigDto
                {
                    UserId = _userId,
                    IsActive = checkEditIsActive.Checked,
                    AdvancedAlwaysCheck = checkEditAdvancedAlwaysCheck.Checked,
                    StartupMenuBarIds = new List<Guid>()
                };
                var checkedItems = checkedListBoxControlStartupMenuBars.CheckedItems;
                foreach (var checkedItem in checkedItems)
                {
                    if (checkedItem is StartupMenuBarDto startupMenuBar)
                    {
                        input.StartupMenuBarIds.Add(startupMenuBar.Id);
                    }
                }
                if (IsCreate)
                {
                    _individuationConfigAppService.CreatePersonnelIndividuationConfig(input);
                }
                else
                {
                    _individuationConfigAppService.UpdatePersonnelIndividuationConfig(input);
                }

                DialogResult = DialogResult.OK;
            });
        }
    }
}