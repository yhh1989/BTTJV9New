using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.MemberShipCard;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit;
using Sw.Hospital.HealthExaminationSystem.Application.ItemSuit.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.MemberShipCard;
using Sw.Hospital.HealthExaminationSystem.Application.MemberShipCard.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.MemberShipCard
{
    public partial class ItemSuitLists : UserBaseForm
    {
        private readonly IMemberShipCardAppService service = new MemberShipCardAppService();
        public TbmItemSuitDto occpast = new TbmItemSuitDto();
        public ItemSuitLists()
        {
            InitializeComponent();

            gridView1.Columns[gridColumn3.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[gridColumn3.FieldName].DisplayFormat.Format = new CustomFormatter(SexHelper.CustomSexFormatter);
        }

        private void ItemSuitList_Load(object sender, EventArgs e)
        {
            ItemSuitsDto itemSuitsDto = new ItemSuitsDto();
            var data = service.GetItemSuits(itemSuitsDto);
            gridControl1.DataSource = data;
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ItemSuitsDto itemSuitsDto = new ItemSuitsDto();
            if (!string.IsNullOrWhiteSpace(Convert.ToString(textEdit1.EditValue)))
            {
                itemSuitsDto.ItemSuitName = textEdit1.Text;
            }
            var data = service.GetItemSuits(itemSuitsDto);
            gridControl1.DataSource = data;
        }

        private void repositoryItemButtonEdit2_Click(object sender, EventArgs e)
        {
            occpast = new TbmItemSuitDto();
            //待处理
            occpast.OrderNum = 0;
            occpast.ItemSuitName = gridView1.GetFocusedRowCellDisplayText(gridColumn2);
            occpast.Price = Convert.ToDecimal(gridView1.GetFocusedRowCellDisplayText(gridColumn4));
            occpast.Id=new Guid(gridView1.GetFocusedRowCellDisplayText(gridColumn7));
            DialogResult = System.Windows.Forms.DialogResult.OK;    
            this.Close();                    
        }
    }
}
