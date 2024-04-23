using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice;
using Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Roster
{
    public partial class FrmInspectionTotalShow : UserBaseForm
    {
        //医生站
        private readonly IDoctorStationAppService _iDoctorStationAppService;
        //建议字典
        private readonly ISummarizeAdviceAppService _iSummarizeAdviceAppService;
        //总检
        private readonly IInspectionTotalAppService _inspectionTotalService;
        public FrmInspectionTotalShow(CustomerRegRosterDto CustomerRegDto)
        {
            customerRegDto = CustomerRegDto;
            InitializeComponent();
            _iDoctorStationAppService = new DoctorStationAppService();
            _iSummarizeAdviceAppService = new SummarizeAdviceAppService();
            _inspectionTotalService = new InspectionTotalAppService();
        }

        private void FrmInspectionTotal_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        #region 全局对象

        //体检信息对象//列表页传进来//用于初始化绑定
        private CustomerRegRosterDto customerRegDto;

        //体检人所有信息
        private ATjlCustomerRegDto _ATjlCustomerRegDto;

        /// <summary>
        /// 体检人科室小结
        /// </summary>
        private List<ATjlCustomerDepSummaryDto> _aTjlCustomerDepSummaryDto;

        //保存匹配到的科室建议
        List<FullSummarizeAdviceDto> MatchingList = new List<FullSummarizeAdviceDto>();
        private List<SummarizeAdviceDto> _matchingList = new List<SummarizeAdviceDto>();

        /// <summary>
        /// 查询建议字典--初始化读取，后续生成总检使用
        /// </summary>
        private List<SummarizeAdviceDto> _summarizeAdviceFull = new List<SummarizeAdviceDto>();

        /// <summary>
        /// 总检后的总结结论
        /// </summary>
        private TjlCustomerSummarizeDto _customerSummarizeDto = new TjlCustomerSummarizeDto();

        //诊断子窗体
        //  TotalDiagnosis _TotalDiagnosis = null;

        #endregion

        #region 方法

        /// <summary>
        /// 界面初始化
        /// </summary>
        private void InitForm()
        {
            gridView1.Columns[gridColumnZhuangTai.FieldName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            gridView1.Columns[gridColumnZhuangTai.FieldName].DisplayFormat.Format = new CustomFormatter(CheckSateHelper.ProjectIStateFormatter);
            labelControlId.Text = customerRegDto.CustomerBM;
            labelControlYBKH.Text = customerRegDto.Customer.MedicalCard;
            labelControlName.Text = customerRegDto.Customer.Name;
            if (customerRegDto.Customer.Sex != 0)
            {
                labelControlSex.Text = customerRegDto.Customer.Sex == 1 ? "男" : "女";
                if (customerRegDto.Customer.Sex == 9)
                {
                    labelControlSex.Text = "未说明性别";
                }
            }
            else
            {
                labelControlSex.Text = "未说明性别";
            }
            labelControlAge.Text = customerRegDto.Customer.Age.ToString();
            labelControlTime.Text = customerRegDto.BookingDate == null ? null : customerRegDto.BookingDate.Value.ToString("yyyy-MM-dd");
            if (customerRegDto.CheckSate == 1)
                labelControlState.Text = "未体检";
            if (customerRegDto.CheckSate == 2)
                labelControlState.Text = "体检中";
            if (customerRegDto.CheckSate == 3)
                labelControlState.Text = "体检完成";
            if (customerRegDto.ClientReg != null)
            {
                labelControlCom.Text = customerRegDto.ClientReg.ClientInfo.ClientName;
            }

            LoadGridData();
            LoadDataState();
            //拼接总结
            //LoadStr();
        }

        /// <summary>
        /// 加载表格数据
        /// </summary>
        private void LoadGridData()
        {
            try
            {
                _aTjlCustomerDepSummaryDto = _inspectionTotalService.GetCustomerDepSummaryList(new EntityDto<Guid>() { Id = customerRegDto.Id });
                ////splashScreenManager.SetWaitFormDescription("加载建议字典...");
                ////查询建议字典--初始化读取，后续生成总检使用
                //var idlist = _aTjlCustomerDepSummaryDto.Select(r => r.DepartmentBMId).ToList();
                //_summarizeAdviceFull = _iSummarizeAdviceAppService.GetSummAll(new InputSearchSumm() { Age = customerRegDto.Customer.Age, SexState = customerRegDto.Customer.Sex, DepartmentId = idlist });

                var _CustomerReg = _iDoctorStationAppService.GetCustomerRegList(new Application.DoctorStation.Dto.QueryClass { CustomerBM = customerRegDto.CustomerBM });
                _ATjlCustomerRegDto = _CustomerReg?[0];

                var _CustomerRegItem = _inspectionTotalService.GetCustomerRegItemList(new Application.DoctorStation.Dto.QueryClass { CustomerRegId = customerRegDto.Id, CustomerBM = customerRegDto.CustomerBM });
                gridControl1.DataSource = _CustomerRegItem.OrderBy(n => n.DepartmentBM.OrderNum).ThenBy(n => n.CustomerItemGroupBM.ItemGroupOrder).ThenBy(n => n.ItemOrder).ToList();

            }
            catch (UserFriendlyException e)
            {
                ShowMessageBox(e);
            }
        }



        /// <summary>
        /// 匹配建议算法
        /// </summary>
        private void MatchingAdvice()
        {
            var StrContent = memoEditHuiZong.Text;

            //清空已存在的建议
            _matchingList = new List<SummarizeAdviceDto>();

            //存储建议Id集合
            List<Guid> IdList = new List<Guid>();
            //遍历科室建议 
            foreach (var Ditem in _summarizeAdviceFull.OrderByDescending(n => n.AdviceName.Length).ToList())
                if (!string.IsNullOrWhiteSpace(Ditem.AdviceName))
                    if (!string.IsNullOrWhiteSpace(StrContent))
                        if (StrContent.Contains(Ditem.AdviceName))
                        {
                            IdList.Add(Ditem.Id);
                            //_matchingList.Add(Ditem);
                            StrContent = StrContent.Replace(Ditem.AdviceName, "");
                        }
            _matchingList = _iSummarizeAdviceAppService.GetSummForGuidList(IdList);
        }


        //根据状态判断加载数据
        private void LoadDataState()
        {
            if (customerRegDto.SummSate == (int)SummSate.HasInitialReview || customerRegDto.SummSate == (int)SummSate.Audited)
            {

                //加载总检数据
                _customerSummarizeDto = _inspectionTotalService.GetSummarize(new TjlCustomerQuery
                { CustomerRegID = customerRegDto.Id });
                var summarizeBm = _inspectionTotalService.GetSummarizeBM(new TjlCustomerQuery
                { CustomerRegID = customerRegDto.Id });
                if (_customerSummarizeDto != null && _customerSummarizeDto.CharacterSummary.Trim() == string.Empty)
                {
                    memoEditHuiZong.Text = "* 您在本次体检中所做的项目，经检查未见异常。";
                }
                else
                {
                    memoEditHuiZong.Text = _customerSummarizeDto?.CharacterSummary;
                }
                //foreach (var item in summarizeBm)
                //    _matchingList.Add(item.SummarizeAdvice);
                gridColumnXuhao.FieldName = "SummarizeOrderNum";
                gridColumnMingCheng.FieldName = "SummarizeName";
                gridColumnNeiRong.FieldName = "Advice";
                if (summarizeBm.Count == 0)
                {
                    TjlCustomerSummarizeBMDto defaultData = new TjlCustomerSummarizeBMDto
                    {
                        SummarizeOrderNum = 1,
                        SummarizeName = "正常",
                        Advice = "经检查未见异常，希望您能保持良好的生活习惯，定期体检，使身体状况处于最佳健康状态。"
                    };
                    summarizeBm.Add(defaultData);
                }
                gridControl3.DataSource = summarizeBm.OrderBy(l => l.SummarizeOrderNum).ToList();
            }
        }
        #endregion

        #region 系统事件
        /// <summary>
        /// 主表格选中行改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var colName = gridView1.FocusedColumn.Caption;
            if (colName == "科室名称")
            {
                var dto = gridControl1.GetFocusedRowDto<TjlCustomerRegItemDto>();
                if (dto != null)
                {
                    labelKeShi.Text = dto.DepartmentBM.Name;
                    labelJianChaYiSheng.Text = dto.InspectEmployeeBM?.Name;
                    labelJieLunYiSheng.Text = dto.CheckEmployeeBM?.Name;
                    layoutControlItem18.Text = "科室小结：";
                    memoEditXiaoJie.Text =
                        _aTjlCustomerDepSummaryDto.FirstOrDefault(n => n.DepartmentBMId == dto.DepartmentBM.Id) == null ? "未填写内容"
                            : _aTjlCustomerDepSummaryDto.FirstOrDefault(n => n.DepartmentBMId == dto.DepartmentBM.Id).CharacterSummary;
                }
            }
            else if (colName == "组合名称")
            {
                var dto = gridControl1.GetFocusedRowDto<TjlCustomerRegItemDto>();
                labelKeShi.Text = dto.DepartmentBM.Name;
                labelJianChaYiSheng.Text = dto.InspectEmployeeBM?.Name;
                labelJieLunYiSheng.Text = dto.CheckEmployeeBM?.Name;
                layoutControlItem18.Text = "组合小结：";
                memoEditXiaoJie.Text = dto.CustomerItemGroupBM.ItemGroupSum;
            }
            else if (colName == "项目名称")
            {
                var dto = gridControl1.GetFocusedRowDto<TjlCustomerRegItemDto>();
                labelKeShi.Text = dto.DepartmentBM.Name;
                labelJianChaYiSheng.Text = dto.InspectEmployeeBM?.Name;
                labelJieLunYiSheng.Text = dto.CheckEmployeeBM?.Name;
                layoutControlItem18.Text = "项目小结：";
                memoEditXiaoJie.Text = dto.ItemSum;
            }
        }
        private void gridView1_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            var colName = gridView1.FocusedColumn.Caption;
            if (colName == "科室名称")
            {
                var dto = gridControl1.GetFocusedRowDto<TjlCustomerRegItemDto>();
                if (dto != null)
                {
                    labelKeShi.Text = dto.DepartmentBM.Name;
                    labelJianChaYiSheng.Text = dto.InspectEmployeeBM?.Name;
                    labelJieLunYiSheng.Text = dto.CheckEmployeeBM?.Name;
                    layoutControlItem18.Text = "科室小结：";
                    //memoEditXiaoJie.Text =
                    //    _customerRegDto.CustomerDepSummary.FirstOrDefault(n =>
                    //        n.DepartmentBMId == dto.DepartmentBM.Id) == null
                    //        ? "未填写内容"
                    //        : _customerRegDto.CustomerDepSummary
                    //            .FirstOrDefault(n => n.DepartmentBMId == dto.DepartmentBM.Id).CharacterSummary;

                    memoEditXiaoJie.Text =
                        _aTjlCustomerDepSummaryDto.FirstOrDefault(n => n.DepartmentBMId == dto.DepartmentBM.Id) == null ? "未填写内容"
                            : _aTjlCustomerDepSummaryDto.FirstOrDefault(n => n.DepartmentBMId == dto.DepartmentBM.Id).CharacterSummary;
                }
            }
            else if (colName == "组合名称")
            {
                var dto = gridControl1.GetFocusedRowDto<TjlCustomerRegItemDto>();
                if (dto == null)
                    return;
                labelKeShi.Text = dto.DepartmentBM.Name;
                labelJianChaYiSheng.Text = dto.InspectEmployeeBM?.Name;
                labelJieLunYiSheng.Text = dto.CheckEmployeeBM?.Name;
                layoutControlItem18.Text = "组合小结：";
                memoEditXiaoJie.Text = dto.CustomerItemGroupBM.ItemGroupSum;
            }
            else if (colName == "项目名称")
            {
                var dto = gridControl1.GetFocusedRowDto<TjlCustomerRegItemDto>();
                labelKeShi.Text = dto.DepartmentBM.Name;
                labelJianChaYiSheng.Text = dto.InspectEmployeeBM?.Name;
                labelJieLunYiSheng.Text = dto.CheckEmployeeBM?.Name;
                layoutControlItem18.Text = "项目小结：";
                memoEditXiaoJie.Text = dto.ItemSum;
            }
        }
        //主表格样式设置
        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                TjlCustomerRegItemDto data = (TjlCustomerRegItemDto)gridView1.GetRow(e.RowHandle);
                //H偏高 HH超高L偏低 LL 超低M正常P异常
                //switch (data.Symbol)
                //{
                //    case "H":
                //        e.Appearance.ForeColor = Color.Red;// 改变行字体颜色
                //        break;
                //    case "HH":
                //        e.Appearance.ForeColor = Color.Red;// 改变行字体颜色
                //        break;
                //    case "L":
                //        e.Appearance.ForeColor = Color.Red;// 改变行字体颜色
                //        break;
                //    case "LL":
                //        e.Appearance.ForeColor = Color.Red;// 改变行字体颜色
                //        break;
                //    case "P":
                //        e.Appearance.ForeColor = Color.Red;// 改变行字体颜色
                //        break;
                //    case "↑":
                //        e.Appearance.ForeColor = Color.Red;// 改变行字体颜色
                //        break;
                //    default:
                //        break;
                //}
                //加项项目标红
                if (data.CustomerItemGroupBM.IsAddMinus == (int)AddMinusType.Add)
                    e.Appearance.BackColor = Color.FromArgb(255, 192, 192); //BackColor //ForeColor
                if (data.CustomerItemGroupBM.IsAddMinus == (int)AddMinusType.Minus)
                    e.Appearance.BackColor = Color.FromArgb(192, 255, 192); //BackColor //ForeColor
                if (data.CustomerItemGroupBM.CheckState == (int)ProjectIState.Stay)
                    e.Appearance.BackColor = Color.FromArgb(192, 255, 255); //BackColor //ForeColor
                if (data.CustomerItemGroupBM.CheckState == (int)ProjectIState.GiveUp)
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 192); //BackColor //ForeColor
            }
        }
        #endregion

        #region 公用方法

        /// <summary>
        /// 克隆对象/引用类型对象复制出一个新的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Clone<T>(T obj)
        {
            T ret = default(T);
            if (obj != null)
            {
                XmlSerializer cloner = new XmlSerializer(typeof(T));
                MemoryStream stream = new MemoryStream();
                cloner.Serialize(stream, obj);
                stream.Seek(0, SeekOrigin.Begin);
                ret = (T)cloner.Deserialize(stream);
            }
            return ret;
        }

        #endregion

        private void gridView1_CellMerge(object sender, CellMergeEventArgs e)
        {
            GridView view = sender as GridView;
            string firstColumnFieldName = "gridColumn6", secondColumnFieldName = "gridColumn6";

            if (e.Column.FieldName == secondColumnFieldName)
            {
                string valueFirstColumn1 = Convert.ToString(view.GetRowCellValue(e.RowHandle1, view.Columns[firstColumnFieldName]));
                string valueFirstColumn2 = Convert.ToString(view.GetRowCellValue(e.RowHandle2, view.Columns[firstColumnFieldName]));
                string valueSecondColumn1 = Convert.ToString(view.GetRowCellValue(e.RowHandle1, view.Columns[secondColumnFieldName]));
                string valueSecondColumn2 = Convert.ToString(view.GetRowCellValue(e.RowHandle2, view.Columns[secondColumnFieldName]));

                e.Merge = valueFirstColumn1 == valueFirstColumn2 && valueSecondColumn1 == valueSecondColumn2;
                e.Handled = true;
            }

        }
    }
}
