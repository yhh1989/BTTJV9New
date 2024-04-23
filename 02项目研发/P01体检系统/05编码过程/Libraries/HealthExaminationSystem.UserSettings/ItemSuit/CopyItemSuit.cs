using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.MemberShipCard.Dto;
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

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.ItemSuit
{
    public partial class CopyItemSuit : UserBaseForm
    {
        public ItemSuitItemGroupContrastFullDto occpast = new ItemSuitItemGroupContrastFullDto();
        private readonly IItemSuitAppService service = new ItemSuitAppService();
        private List<SimpleItemSuitDto> groupls = new List<SimpleItemSuitDto>();
        public CopyItemSuit()
        {
            InitializeComponent();
        }

        private void CopyItemSuit_Load(object sender, EventArgs e)
        {
            var result = service.QuerySimplesCache();
            gridControl1.DataSource = result;
            groupls = DefinedCacheHelper.GetItemSuit().ToList();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            occpast = new ItemSuitItemGroupContrastFullDto();
            var id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridColumn2);
            ItemSuitEditor f2 = new ItemSuitEditor();
            occpast.Id = (Guid)id;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void searchControl1_TextChanged(object sender, EventArgs e)
        {
            BindgrdOptionalItemGroup();
        }
        public void BindgrdOptionalItemGroup()
        {
            if (searchControl1.Text != "")
            {
                var strup = searchControl1.Text.ToUpper();
                var output = new List<SimpleItemSuitDto>();
                output = groupls.Where(o => o.ItemSuitName.Contains(searchControl1.Text) || o.HelpChar.Contains(strup)).ToList();
                gridControl1.DataSource = output;
            }
            else
            {
                gridControl1.DataSource = groupls;
            }

        }
    }
}
