using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.HisInterface;
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
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.ItemGroup
{
    public partial class PriceSynchronize : UserBaseForm
    {
        private readonly ICustomerAppService _CustomerAppService;
        public PriceSynchronize()
        {
            InitializeComponent();
            _CustomerAppService = new CustomerAppService();
        }
        
        private void PriceSynchronize_Load(object sender, EventArgs e)
        {
            var GetHis = new InCarNumPriceDto();
            var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;
            GetHis.HISName = HISName;
            var result = _CustomerAppService.GetYXHis(GetHis);
            gridControl1.DataSource = result.ToList();

        }
        

        //获取更新
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var GetHis = new InCarNumPriceDto();
            var HISName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisInterface, 2).Remarks;
            GetHis.HISName = HISName;
            var result = _CustomerAppService.GetYXHis(GetHis);
            gridControl1.DataSource = result.ToList();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //水平进度条
            progressBarControl1.Properties.Minimum = 0;
            var HisPrice = new HisPriceDtos();

            var result = gridControl1.DataSource as List<HisPriceDtos>;          
            if (result != null && result.Count > 0)
            {

                progressBarControl1.Properties.Maximum = result.Count;
                progressBarControl1.Properties.Step = 1;
                progressBarControl1.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
                progressBarControl1.Position = 0;
                progressBarControl1.Properties.ShowTitle = true;
                progressBarControl1.Properties.PercentView = true;
                progressBarControl1.Properties.ProgressKind = DevExpress.XtraEditors.Controls.ProgressKind.Horizontal;
                System.Windows.Forms.Application.DoEvents();
                var currREsult = result.ToList();
                var len = result.Count();
                bool isOK = true;
                while (isOK)
                {

                    if (currREsult.Count > 5000)
                    {
                        var r1 = currREsult.Take(5000).ToList();
                        var results = _CustomerAppService.InsertYXHis(r1);
                        currREsult = currREsult.Skip(5000).Take(len - 5000).ToList();
                        progressBarControl1.Properties.Step = 5000;
                    }
                    else
                    { var results = _CustomerAppService.InsertYXHis(currREsult);
                        isOK = false;
                        progressBarControl1.Properties.Step = currREsult.Count;
                    }
                    progressBarControl1.PerformStep();
                    //处理当前消息队列中的所有windows消息,不然进度条会不同步
                    System.Windows.Forms.Application.DoEvents();
                    //分两次调用避免太长报错
                    //var r2 = result.Skip(5000).Take(len - 5000).ToList();
                    //if (r2.Count > 0)
                    //{
                    //    results = _CustomerAppService.InsertYXHis(r2);
                    //}
                }
                MessageBox.Show("同步成功！");
            }
            else
            {
                MessageBox.Show("没有可以同步的项目！");
            }
        }
    }
}
