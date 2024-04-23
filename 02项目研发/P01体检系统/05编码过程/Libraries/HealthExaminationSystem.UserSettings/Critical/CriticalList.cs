using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Critical;
using Sw.Hospital.HealthExaminationSystem.Application.Critical;
using Sw.Hospital.HealthExaminationSystem.Application.Critical.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Critical
{
    public partial class CriticalList : UserBaseForm
    {
        private ICriticalAppService criticalAppService=new CriticalAppService();
        public CriticalList()
        {
            InitializeComponent();
        }

        private void CriticalList_Load(object sender, EventArgs e)
        {
            repositoryItemLookUpEdit1.DataSource = DefinedCacheHelper.GetDepartments();
            repositoryItemLookUpEdit2.DataSource = DefinedCacheHelper.GetItemInfos();
            repositoryItemLookUpEdit3.DataSource = CriticalTypeStateHelper.GetList();
            repositoryItemLookUpEdit4.DataSource = CalculationTypeStateHelper.GetList();
            repositoryItemLookUpEdit5.DataSource = OperatorStateHelper.GetList();

            searchLookUpItem.Properties.DataSource= DefinedCacheHelper.GetItemInfos();



        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            using (var frm = new CriticalSet())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    search();
                }
            }
        }
        private void search()
        {
            SearchCriticalDto searchCriticalDto = new SearchCriticalDto();
            if (!string.IsNullOrEmpty(searchLookUpItem.EditValue?.ToString()))
            {
                searchCriticalDto.ItemInfoId = (Guid)searchLookUpItem.EditValue;
            }
            var Critical = criticalAppService.getSearchCriticalDto(searchCriticalDto);
            gridControl1.DataSource = Critical;
        }

        private void butEdit_Click(object sender, EventArgs e)
        {
            var id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, conID);
            using (var frm = new CriticalSet((Guid)id))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    search(); 
                }
            }
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            var id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, conID);
            if (id == null)
            {
                ShowMessageBoxWarning("请选择要删除的危急值。");
                return;
            }
            else
            {
                DialogResult dr = XtraMessageBox.Show("是否删除该记录？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    SearchCriticalDto searchCriticalDto = new SearchCriticalDto();
                    searchCriticalDto.Id = (Guid)id;
                    criticalAppService.DelCritical(searchCriticalDto);
                    search();
                }
            }
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            search();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Regex r = new Regex(@"\d*\.\d*|0\.\d*[1-9]\d*$");           

            //string str = textBox1.Text;
            //Regex reg = new Regex(@"\d*\.\d*|0\.\d*[1-9]\d*$");
            //MatchCollection mc = reg.Matches(str);//设定要查找的字符串
            //List<string> strlist = new List<string>();
            //foreach (Match m in mc)
            //{
            //    string s = m.Groups[0].Value;
            //    strlist.Add(s);
            //    str=str.Replace(s,"");
            //}


            //Regex reg1 = new Regex(@"[0-9]+");//2秒后超时
            //MatchCollection mc1 = reg1.Matches(str);//设定要查找的字符串
            //foreach (Match m in mc1)
            //{
            //    string s = m.Groups[0].Value;
            //    strlist.Add(s);
            //}
            //MessageBox.Show(string.Join(" | ", strlist));
        }
    }
}
