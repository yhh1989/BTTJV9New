using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.Comm
{
    public partial class FrmIntroduce : UserBaseForm
    {
        public IItemGroupAppService itemGroupAppService;
        private SimpleItemGroupDto SimpleItemGroupDto { get; set; }
        public FrmIntroduce()
        {
            InitializeComponent();
            itemGroupAppService = new ItemGroupAppService();
            // SaveDataComplateForGroup?.Invoke(OutputsimpleItemSuitDto, TTOutputlstclientTeamRegitemViewDtos);
            // DialogResult = DialogResult.OK;
        }
        public FrmIntroduce(SimpleItemGroupDto dto) : this()
        {
            SimpleItemGroupDto = dto;
        }

        private void FrmIntroduce_Load(object sender, System.EventArgs e)
        {
            if (SimpleItemGroupDto != null)
            {
                labelText.Text = " " + SimpleItemGroupDto.ItemGroupName + "     年龄：" + SimpleItemGroupDto.MinAge + "-" + SimpleItemGroupDto.MaxAge + "     性别：" + SexHelper.CustomSexFormatter(SimpleItemGroupDto.Sex) + "      价格：" + SimpleItemGroupDto.Price.ToString() + "";
                memoEditInt.Text = SimpleItemGroupDto.ItemGroupExplain;
                var itemgroup = itemGroupAppService.Get(new SearchItemGroupDto { Id = SimpleItemGroupDto.Id });
                gridControlItemGroup.DataSource = itemgroup.ItemInfos;
            }
        }
    }
}
