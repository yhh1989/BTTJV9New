using System.Windows.Forms;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.CodeManager
{
    public partial class CodeConfig : UserBaseForm
    {
        private readonly IDNumberAppService _idNumberAppService;

        public CodeConfig()
        {
            InitializeComponent();

            _idNumberAppService = new IDNumberAppService();
        }

        private void CodeConfig_Load(object sender, System.EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            AutoLoading(() =>
            {
                var result = _idNumberAppService.GetAllList();
                gridControl.DataSource = result;
            });
        }

        private void simpleButtonCodeCreate_Click(object sender, System.EventArgs e)
        {
            var name = gridViewCode.GetRowCellValue(gridViewCode.FocusedRowHandle, gridColumnCodeName);
            if (name != null)
            {
                using(var frm = new CodeEditor(name.ToString()))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
            else
            {
                using (var frm = new CodeEditor())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
        }

        private void gridViewCode_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                simpleButtonCodeCreate.PerformClick();
            }
        }
    }
}