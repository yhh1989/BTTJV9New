using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew;
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
    public partial class frmRiskSum : UserBaseForm
    {
        private TjlCustomerRegForInspectionTotalDto _tjlCustomerRegDto;
        public OccCustomerHazardSumDto _cusHazar;
        // 总检
        private readonly IInspectionTotalAppService _inspectionTotalService;
        public readonly IOccDisProposalNewAppService _IOccDisProposalNewAppService;
        private readonly string _riskName;
   
        public frmRiskSum()
        {
            InitializeComponent();
            _inspectionTotalService = new InspectionTotalAppService();
            _IOccDisProposalNewAppService = new OccDisProposalNewAppService();
        }
        public frmRiskSum(TjlCustomerRegForInspectionTotalDto custreg , OccCustomerHazardSumDto cusHazar,string riskName) :this()
        {
            _tjlCustomerRegDto = custreg;
            _cusHazar = cusHazar;
            _riskName = riskName;

        }
        private void frmRiskSum_Load(object sender, EventArgs e)
        {
            //职业病
            var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
            var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
            var tjlb = Clientcontract.FirstOrDefault(o => o.Value == _tjlCustomerRegDto.PhysicalType)?.Text;

            labelControl5.Text = string.Format("体检类型：{0} 危害因素：{1} 岗位： {2} 工种：{3}", _tjlCustomerRegDto.PostState,
                   _riskName, _tjlCustomerRegDto.WorkName, _tjlCustomerRegDto.TypeWork);
            EntityDto<Guid> entityDto = new EntityDto<Guid>();
            entityDto.Id = _tjlCustomerRegDto.Id;
            var Occ = _inspectionTotalService.GetOccSumByCusReg(entityDto);

            SearOccDis.Properties.DataSource = Occ.OccDiseases;
            SearOccDis.Properties.DisplayMember = "Text";
            SearOccDis.Properties.ValueMember = "Id";
            searJJZ.Properties.DataSource = Occ.Contraindications;
            searJJZ.Properties.DisplayMember = "Text";
            searJJZ.Properties.ValueMember = "Id";
            //处理意见
            ChargeBM chargeBM = new ChargeBM();
            chargeBM.Name = ZYBBasicDictionaryType.Opinions.ToString();
            var lis1 = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            //searOccAdvice.Properties.DataSource = lis1;
            searOccAdvice.Items.Clear();
            foreach (var yj in lis1)
            {
                searOccAdvice.Items.Add(yj.Text);
            }
            //职业病结论

            chargeBM.Name = ZYBBasicDictionaryType.Conclusion.ToString();
            var lis2 = _IOccDisProposalNewAppService.getOutOccDictionaryDto(chargeBM);
            txtZYBSum.Properties.DataSource = lis2;

            //tabNavigationPage3.Show();
            tabNavigationPage3.PageVisible = true;
            if (_cusHazar != null)
            {
                txtZYBSum.EditValue = _cusHazar.Conclusion;
                searOccAdvice.Text = _cusHazar.Advise;
                txtOccContent.EditValue = _cusHazar.Description;
                txtMedicalAdvice.EditValue = _cusHazar.MedicalAdvice;
                SearOccDis.EditValue = _cusHazar.OccCustomerOccDiseasesIds;
                searJJZ.EditValue = _cusHazar.OccDictionarysIDs;
            }

        }

        private void SearOccDis_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void searJJZ_EditValueChanged(object sender, EventArgs e)
        {
         
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
