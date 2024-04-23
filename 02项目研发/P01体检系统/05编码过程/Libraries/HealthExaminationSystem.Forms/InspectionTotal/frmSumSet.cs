using DevExpress.XtraLayout.Utils;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary;
using Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.InspectionTotal
{
    public partial class frmSumSet : UserBaseForm
    {
        private readonly IBasicDictionaryAppService _basicDictionaryAppService;

        public frmSumSet()
        {
            _basicDictionaryAppService = new BasicDictionaryAppService();
            InitializeComponent();
        }

        private void frmSumSet_Load(object sender, EventArgs e)
        {
           
            var sumsets = DefinedCacheHelper.GetBasicDictionary().
                Where(p => p.Type == BasicDictionaryType.CusSumSet.ToString());
            var next = sumsets.FirstOrDefault(p=>p.Value== 131);
            if (next == null || next?.Remarks == "Y")
            {
                checkNext.Checked = true;
            }
            var auto = sumsets.FirstOrDefault(p => p.Value == 14);
            if (auto != null && auto?.Remarks == "Y")
            {
                checkAutoSum.Checked = true;
            }
            var NOYC = sumsets.FirstOrDefault(p => p.Value == 1);
            if (NOYC != null && NOYC?.Remarks == "1")
            {
                checkNOYC.Checked = true;
            }

            var SD = sumsets.FirstOrDefault(p => p.Value == 21);
            if (SD != null && SD?.Remarks == "Y")
            {
                checkSD.Checked = true;
            }

            var xmxj = sumsets.FirstOrDefault(p => p.Value == 16);
            if (xmxj != null && xmxj?.Remarks == "Y")
            {
                checkXM.Checked = true;
            }
            var kshz = sumsets.FirstOrDefault(p => p.Value == 3);
            if (!string.IsNullOrEmpty(kshz?.Remarks))
            {
                richKS.Text = kshz?.Remarks;
            }
            var xmhz = sumsets.FirstOrDefault(p => p.Value == 11);
            if (!string.IsNullOrEmpty(xmhz?.Remarks))
            {
                richXM.Text = xmhz?.Remarks;
            }
            if (checkXM.Checked == true)
            {
                layoutControlGroup1.Visibility = LayoutVisibility.Always;
                layoutControlGroup2.Visibility = LayoutVisibility.Never;
            }
            else
            {              

                layoutControlGroup1.Visibility = LayoutVisibility.Never;
                layoutControlGroup2.Visibility = LayoutVisibility.Always;
            }
           
          
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var sumsets = DefinedCacheHelper.GetBasicDictionary().
                Where(p => p.Type == BasicDictionaryType.CusSumSet.ToString());
            var next = sumsets.FirstOrDefault(p => p.Value == 131);
            if (next == null  )
            {
                next = new BasicDictionaryDto();
                next.Value = 131;
                next.Type = BasicDictionaryType.CusSumSet.ToString();
                next.Text = "总检提示下一位";
            }
            if (checkNext.Checked == true)
            {
                next.Remarks = "Y";
            }
            else
            { next.Remarks = "N"; }
            saveSumSet(next);
            var auto = sumsets.FirstOrDefault(p => p.Value == 14);
            if (auto == null)
            {
                auto = new BasicDictionaryDto();
                auto.Value = 14;
                auto.Text = "自动汇总";
            }
            if (checkAutoSum.Checked == true)
            {
                auto.Remarks = "Y";
            }
            else
            { auto.Remarks = "N"; }
            saveSumSet(auto);
            var NOYC = sumsets.FirstOrDefault(p => p.Value == 1);
            if (NOYC == null)
            {
                NOYC = new BasicDictionaryDto();
                NOYC.Type = BasicDictionaryType.CusSumSet.ToString();
                NOYC.Value = 1;
                NOYC.Text = "未见异常是否进总检";
            }
            if (checkNOYC.Checked == true)
            {
                NOYC.Remarks = "1";
            }
            else
            {
                NOYC.Remarks = "0";
            }
            saveSumSet(NOYC);

            var SD = sumsets.FirstOrDefault(p => p.Value == 21);
            if (SD == null)
            {
                SD = new BasicDictionaryDto();
                SD.Type = BasicDictionaryType.CusSumSet.ToString();
                SD.Value = 21;
                SD.Text = "总检锁定";
            }
            if (checkSD.Checked == true)
            {
                SD.Remarks = "Y";
            }
            else
            { SD.Remarks = "N"; }
            saveSumSet(SD);
            var xmxj = sumsets.FirstOrDefault(p => p.Value == 16);
            if (xmxj == null)
            {
                xmxj = new BasicDictionaryDto();
                xmxj.Type = BasicDictionaryType.CusSumSet.ToString();
                xmxj.Value = 16;
                xmxj.Text = "按项目小结生成汇总（重大异常结果放在最上面）";
            }
            if (checkXM.Checked == true)
            {
                xmxj.Remarks = "Y";
            }
            else
            { xmxj.Remarks = "N"; }
            saveSumSet(xmxj);
            if (layoutControlGroup2.Visibility == LayoutVisibility.Always)
            {
                var kshz = sumsets.FirstOrDefault(p => p.Value == 3);
                if (kshz == null)
                {
                    kshz = new BasicDictionaryDto();
                    kshz.Type = BasicDictionaryType.CusSumSet.ToString();
                    kshz.Value = 3;
                    kshz.Text = "科室小结总检格式";
                }

                kshz.Remarks = richKS.Text;
                saveSumSet(kshz);
            }
            if (layoutControlGroup1.Visibility == LayoutVisibility.Always)
            {
                var xmhz = sumsets.FirstOrDefault(p => p.Value == 11);
                if (xmxj == null)
                {
                    xmhz = new BasicDictionaryDto();
                    xmhz.Type = BasicDictionaryType.CusSumSet.ToString();
                    xmhz.Value = 11;
                    xmhz.Text = "项目汇总格式";
                }

                xmhz.Remarks = richXM.Text;
                saveSumSet(xmhz);
            }
            MessageBox.Show("保存成功");
           

            CreateLoginAsync();
        }
        public async Task CreateLoginAsync()
        {
            await Task.Run(() =>
            DefinedCacheHelper.UpdateBasicDictionary() 
           );
        }
        private void saveSumSet(BasicDictionaryDto Saveinput)
        {
            if (Saveinput.Id == Guid.Empty)
            {
                var input = new CreateBasicDictionaryDto
                {
                    Value = Saveinput.Value,
                    Text = Saveinput.Text,
                    Remarks = Saveinput.Remarks,
                    Type = Saveinput.Type,
                    Code = Saveinput.Code,
                    OrderNum = Saveinput.Value
                };
                _basicDictionaryAppService.Add(input);
                DialogResult = DialogResult.OK;
            }
            else
            {
                var input = new UpdateBasicDictionaryDto
                {
                    Id = Saveinput.Id,
                    Value = Saveinput.Value,
                    Text = Saveinput.Text,
                    Remarks = Saveinput.Remarks,
                    Type = Saveinput.Type,
                    Code = Saveinput.Code,
                    OrderNum = Saveinput.OrderNum

                };
                _basicDictionaryAppService.Edit(input);
                DialogResult = DialogResult.OK;
            }
        }

        private void checkXM_CheckedChanged(object sender, EventArgs e)
        {
            if (checkXM.Checked == true)
            {
                layoutControlGroup1.Visibility = LayoutVisibility.Always;
                layoutControlGroup2.Visibility = LayoutVisibility.Never;
            }
            else
            {
                layoutControlGroup1.Visibility = LayoutVisibility.Never;
                layoutControlGroup2.Visibility = LayoutVisibility.Always;
            }
          
        }
    }
}
