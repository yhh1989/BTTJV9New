using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReSultStatus;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemInfo;
using Sw.Hospital.HealthExaminationSystem.Application.CusReSultStatus;
using Sw.Hospital.HealthExaminationSystem.Application.CusReSultStatus.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.CusReSultStatus
{
    public partial class ReSultSet : UserBaseForm
    {
        ICusReSultStatusAppService _cusReSultStatusApp = new CusReSultStatusAppService();
        IDepartmentAppService service = new DepartmentAppService();
        IItemGroupAppService itemGroup = new ItemGroupAppService();
        IItemInfoAppService ItemInfo = new ItemInfoAppService();
        ReSultSetDto GetReSultDepart = new ReSultSetDto();
        public ReSultSetDto _Model { get; private set; }
        public List<Guid> GetGuidsDepart;
        public List<Guid> GetGuidsItemGroup;
        public List<Guid> GetGuidsItemInfo;

        public ReSultSet()
        {
            if (_Model == null) _Model = new ReSultSetDto();
            GetGuidsDepart = new List<Guid>();
            GetGuidsItemGroup = new List<Guid>();
            GetGuidsItemInfo = new List<Guid>();
            InitializeComponent();
        }

        private void ReSultStatus_Load(object sender, EventArgs e)
        {
            try
            {
                var results = _cusReSultStatusApp.GetReSultDepart().ToList();

                if (results != null)
                {
                    foreach (var re in results)
                    {
                        _Model.Id = re.Id;
                        if (re.isAdVice != null)
                        {
                            checkEdit2.Checked = re.isAdVice.Value;
                        }
                        if (re.isOccupational != null)
                        {
                            checkEdit3.Checked = re.isOccupational.Value;
                        }
                        foreach (var de in re.Items)
                        {

                            var result = DefinedCacheHelper.GetItemInfos().FirstOrDefault(o => o.Id == de.ItemInfoId);
                            textEdit1.Text += result.Name + ",";
                            GetGuidsItemInfo.Add(de.ItemInfoId);

                        }
                        foreach (var gr in re.Groups)
                        {
                            var result = DefinedCacheHelper.GetItemGroups().FirstOrDefault(o => o.Id == gr.ItemGroupId);
                            textEdit2.Text += result.ItemGroupName + ",";
                            GetGuidsItemGroup.Add(gr.ItemGroupId);

                        }
                        foreach (var it in re.Departs)
                        {
                            
                            var result = DefinedCacheHelper.GetDepartments().FirstOrDefault(o => o.Id == it.DepartmentId);
                            if (result != null)
                            {
                                textEdit3.Text += result.Name + ",";

                                GetGuidsDepart.Add(it.DepartmentId);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmItemInfoList())
            {
                frm.DepartNames = string.Join(",",GetGuidsItemInfo);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    textEdit1.EditValue = "";
                    GetGuidsItemInfo = frm.GetGuids;
                    foreach (var itemname in frm.GetGuids)
                    {
                                             
                        Guid guids = new Guid(itemname.ToString());
                        var result = DefinedCacheHelper.GetItemInfos().FirstOrDefault(o => o.Id == guids);
                        if (textEdit1.Text.Contains(result.Name.ToString()))
                        {
                            continue;
                        }
                        else
                        {
                            textEdit1.EditValue += result.Name + ",";
                        }
                    }
                    textEdit1.Refresh();
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (Ok())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private bool Ok()
        {
            AddResultSetDto dto = new AddResultSetDto();
            dto.isAdVice = checkEdit2.Checked;
            dto.isOccupational = checkEdit3.Checked;
            List<Guid> dep = new List<Guid>();
            List<Guid> itemgroup = new List<Guid>();
            List<Guid> iteminfo = new List<Guid>();
            if (_Model.Id != null)
            {
                dto.Id = _Model.Id;
            }
            if (GetGuidsDepart != null)
            {
                 dep = GetGuidsDepart.ToList();
            }
            if (GetGuidsItemGroup != null)
            {
                itemgroup = GetGuidsItemGroup.ToList();
            }
            if (GetGuidsItemInfo != null)
            {
                iteminfo = GetGuidsItemInfo.ToList();
            }
            
            bool res = false;
            ReSultSetDto dtos = null;
            AutoLoading(() =>
            {
                FullReSultSetDto input = new FullReSultSetDto()
                {
                    ReSultSets= dto,
                    Departs = dep,
                    Groups = itemgroup,
                    Items = iteminfo
                };

                dtos = _cusReSultStatusApp.Add(input);

                _Model = dtos;
                res = true;
            });
            return res;
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            using (var frm = new frmItemGroupList())
            {
                frm.ItemGroupNames = string.Join(",", GetGuidsItemGroup);
                if (frm.ShowDialog() == DialogResult.OK)
                {

                    textEdit2.EditValue = "";
                    GetGuidsItemGroup = frm.GetGuids;
                    foreach (var ItemGroup in frm.GetGuids)
                    {
                                                
                        Guid guids = new Guid(ItemGroup.ToString());
                        var result = DefinedCacheHelper.GetItemGroups().FirstOrDefault(o => o.Id == guids);
                        if (textEdit2.Text.Contains(result.ItemGroupName.ToString()))
                        {
                            continue;
                        }
                        else
                        {
                            textEdit2.EditValue += result.ItemGroupName + ",";
                        }
                    }
                    textEdit2.Refresh();
                }
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            using (var frm = new frmDepartList())
            {
                frm.DepartNames = string.Join(",", GetGuidsDepart);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    textEdit3.EditValue = "";
                    GetGuidsDepart = frm.GetGuids;
                    foreach (var dept in frm.GetGuids)
                    {
                         
                           
                      
                        Guid guids = new Guid(dept.ToString());
                        var result = DefinedCacheHelper.GetDepartments().FirstOrDefault(o => o.Id == guids);
                        if (textEdit3.Text.Contains(result.Name.ToString()))
                        {
                            continue;
                        }
                        else
                        {
                            textEdit3.EditValue += result.Name + ",";
                        }
                    }
                    textEdit3.Refresh();
                }
            }
        }
    }
}
