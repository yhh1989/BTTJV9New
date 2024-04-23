using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    public partial class Form1 : Form
    {        /// <summary>
             /// 总检后的总结结论
             /// </summary>
        private TjlCustomerSummarizeDto _customerSummarizeDto = new TjlCustomerSummarizeDto();

        // 总检
        private readonly IInspectionTotalAppService _inspectionTotalService=new InspectionTotalAppService();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _customerSummarizeDto = _inspectionTotalService.GetSummarize(new TjlCustomerQuery
            { CustomerRegID = Guid.Parse("BE6CDA46-95C2-460F-8B79-8AED3944A5CA") });

            _customerSummarizeDto.CharacterSummary = richEditControl1.HtmlText;

            var result = _inspectionTotalService.CreateSummarize(_customerSummarizeDto);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            _customerSummarizeDto = _inspectionTotalService.GetSummarize(new TjlCustomerQuery
            { CustomerRegID = Guid.Parse("BE6CDA46-95C2-460F-8B79-8AED3944A5CA") });
            richEditControl1.HtmlText = _customerSummarizeDto.CharacterSummary;

        }
    }
}
