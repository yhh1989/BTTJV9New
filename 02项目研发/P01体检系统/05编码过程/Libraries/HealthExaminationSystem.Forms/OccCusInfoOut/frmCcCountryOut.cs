using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccNationalDisease;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto;
using Sw.Hospital.HealthExaminationSystem.Comm;
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
using System.Xml;

namespace Sw.Hospital.HealthExaminationSystem.OccCusInfoOut
{
    public partial class frmCcCountryOut : UserBaseForm
    {
        private OccNationalDiseaseAppService occNationalDiseaseAppService = new OccNationalDiseaseAppService();
        private readonly ICommonAppService _commonAppService;
        private DataNodeDto dataNode  = new DataNodeDto();
        private XMLDataNodeDto XMLDataNode = new XMLDataNodeDto();
        public frmCcCountryOut()
        {
            _commonAppService = new CommonAppService();
            InitializeComponent();
        }

        private void frmCcCountryOut_Load(object sender, EventArgs e)
        {
            //dateStar.EditValue=
            var date = _commonAppService.GetDateTimeNow().Now;
            dateEnd.DateTime = date;
            dateStar.DateTime = date;
            searchLookClient.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var country = occNationalDiseaseAppService.GetCountry();
            if (country == null)
            {
                MessageBox.Show("请先进行用户配置！");
                return;
            }
            InSearchDto inSearchDto = new InSearchDto();
            if (!string.IsNullOrEmpty(textCustomerBM.Text))
            {
                inSearchDto.CustomerBM = textCustomerBM.Text;
            }
            if(!string.IsNullOrEmpty(dateStar.EditValue?.ToString()))
            {
                inSearchDto.StarDate = dateStar.DateTime;
            }
            if (!string.IsNullOrEmpty(dateEnd.EditValue?.ToString()))
            {
                inSearchDto.EndDate = dateEnd.DateTime;
            }
            if (!string.IsNullOrEmpty(searchLookClient.EditValue?.ToString()))
            {
                inSearchDto.ClientRegId = (Guid)searchLookClient.EditValue;
            }


            dataNode =  occNationalDiseaseAppService.GetEnterpriseInfo(inSearchDto);
            gridControlClient.DataSource = dataNode.EventBody.ENTERPRISE_INFO;


            XMLDataNode = occNationalDiseaseAppService.GetExamRecord(inSearchDto);
            gridControlCusReg.DataSource = XMLDataNode.EventBody.HEALTH_EXAM_RECORD;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            frmCountrySet frmCountrySet = new frmCountrySet();
            frmCountrySet.ShowDialog();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "国家疾控接口XML";
            saveFileDialog.Title = "导出xml";
            saveFileDialog.Filter = "XML文件(*.xml)|*.xml";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            var xml1=   XmlTool.Serialize(dataNode);
            var xml2 = XmlTool.Serialize(XMLDataNode);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml1);
            xmldoc.Save(saveFileDialog.FileName.Replace("国家疾控接口XML", "用人单位信息"));
            XmlDocument xmldoc2 = new XmlDocument();
            xmldoc2.LoadXml(xml2);
            xmldoc2.Save(saveFileDialog.FileName.Replace("国家疾控接口XML", "职业健康档案"));
        }
    }
}
