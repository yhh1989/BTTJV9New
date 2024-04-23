using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Application.Dictionary;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Dictionary;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Dictionary.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Dictionary
{
    public partial class DictionaryEdit : UserBaseForm
    {
        private readonly Guid _ItemInfoid;
        private readonly Guid _Departentid;
        private Guid _id;

        private readonly IDictionaryAppService _dictionaryAppService;
        private readonly ICommonAppService _commonAppService;
        public string word;



        public DictionaryEdit(Guid ItemInfoid, Guid Departentid )
        {
            InitializeComponent();
            _ItemInfoid = ItemInfoid;
            _Departentid = Departentid;
            _dictionaryAppService = new DictionaryAppService();
            _commonAppService = new CommonAppService();


        }

        public DictionaryEdit(Guid ItemInfoid, Guid Departentid,string _word)
        {
            InitializeComponent();
            _ItemInfoid = ItemInfoid;
            _Departentid = Departentid;
            _dictionaryAppService = new DictionaryAppService();
            _commonAppService = new CommonAppService();
            word = _word;


        }
        private void DictionaryEdit_Load(object sender, EventArgs e)
        {

            LoadData();
            MustNot();
            if (!string.IsNullOrEmpty(word))
            {
                simpleButtonADD.PerformClick();
               
                memoEditcontent.Text = word;
                memoEditDiagnostic.Text = word;
                lookUpEditdisease.EditValue = (int)HealthExaminationSystem.Common.Enums.IllnessSate.Diagnosis;
                var result = _commonAppService.GetHansBrief(new ChineseDto { Hans = word });
                textEditremember.Text = result.Brief;
            }
        }
        //添加
        private void simpleButtonADD_Click(object sender, EventArgs e)
        {
            Add();
            Must();
        }
        //保存
        private void simpleButtonPreser_Click(object sender, EventArgs e)
        {
            Preser();
            
        }
        //助记码
        private void memoEditcontent_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            //if (!string.IsNullOrWhiteSpace(textEditremember.Text))
            //return;
            var name = memoEditcontent.Text.Trim();
            if (!string.IsNullOrWhiteSpace(name))
                try
                {
                    var result = _commonAppService.GetHansBrief(new ChineseDto { Hans = name });
                    textEditremember.Text = result.Brief;
                }
                catch (UserFriendlyException exception)
                {
                    Console.WriteLine(exception);
                }
            else
                textEditremember.Text = string.Empty;
        }
        //表格双击
        private void gridViewItemDictionary_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                edit();
                Must();

            }
            if (e.Clicks == 1)
            {
                edit();
                MustNot();
            }
        }
        //修改
        private void simpleButtonModify_Click(object sender, EventArgs e)
        {
            edit();
        }
        //退出
        private void simpleButtonSign_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        //删除
        private void simpleButtonDel_Click(object sender, EventArgs e)
        {
           
            try
            {
                _id = (Guid)gridViewItemDictionary.GetRowCellValue(gridViewItemDictionary.FocusedRowHandle, Ids);
                
            }
            catch
            {
                XtraMessageBox.Show("请正确的选择项目！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var name = gridViewItemDictionary.GetRowCellValue(gridViewItemDictionary.FocusedRowHandle, gridColumnWord);

            var question = XtraMessageBox.Show($"确定删除字典设置 {name}？", "询问",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
            if (question != DialogResult.Yes)
                return;

            _dictionaryAppService.DeleteItemDictionary(new EntityDto<Guid> { Id = _id });

            LoadData();
            MustNot();
        }

        private void LoadData()
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            splashScreenManager.SetWaitFormDescription(Variables.LoadingForCloud);

            try
            {
                DictionaryItemDictionaryDto Dictionary = new DictionaryItemDictionaryDto();

                Dictionary.iteminfoBMId = _ItemInfoid;
                Dictionary.DepartmentBMId = _Departentid;



                var output = _dictionaryAppService.GetById(Dictionary);

                if (output != null)
                {
                    gridControl1.DataSource = null;
                    gridControl1.DataSource = output.OrderBy(r => r.OrderNum).ToList();

                }

            }
            catch (UserFriendlyException e)
            {
                ShowMessageBox(e);
            }
            finally
            {
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
            }
        }


        private void Add()
        {
            textWGBM.Text = "";
            memoEditcontent.EditValue = null;
            memoEditDiagnostic.EditValue = null;
            //疾病状态
            lookUpEditdisease.Properties.DataSource = IllnessSateHelp.GetIfTypeModels();
            lookUpEditdisease.EditValue = null;
            textEditICD.EditValue = null;
            textEditremember.EditValue = null;
            checkEditsuggestions.Checked  = true;
            _id = new Guid();
            var list = gridControl1.GetDtoListDataSource<DictionaryItemDictionaryDto>();
            if (list.Count>0)
                spinEditOrder.EditValue = list.Max(a => a.OrderNum) + 1;
            else
                spinEditOrder.EditValue = 1;
        }
        private void edit()
        {
            try
            {
                _id = (Guid)gridViewItemDictionary.GetRowCellValue(gridViewItemDictionary.FocusedRowHandle, Ids);
            }
            catch
            {
                XtraMessageBox.Show("请正确的选择项目！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var output = _dictionaryAppService.GetByDictionaryId(new EntityDto<Guid> { Id = _id });

            if (output != null)
            {
                memoEditcontent.EditValue = output.Word;
                memoEditDiagnostic.EditValue = output.Period;
                //疾病状态
                lookUpEditdisease.Properties.DataSource = IllnessSateHelp.GetIfTypeModels();
                lookUpEditdisease.EditValue = output.IsSickness;

                textEditICD.EditValue = output.WBCode;
                textEditremember.EditValue = output.HelpChar;
                checkEditsuggestions.Checked = output.ApplySate == 1;
                spinEditOrder.Value = (decimal)output.OrderNum;
                if (!string.IsNullOrEmpty(output.WBCode))
                {
                    textWGBM.Text = output.WBCode;
                }
            }
            Must();

        }
        private void MustNot()
        {
            memoEditcontent.Enabled = false;
            memoEditDiagnostic.Enabled = false;
            lookUpEditdisease.Enabled = false;
            textEditICD.Enabled = false;
            textEditremember.Enabled = false;
            checkEditsuggestions.Enabled = false;
            spinEditOrder.Enabled = false;
            simpleButtonPreser.Enabled = false;
        }

        private void Must()
        {
            memoEditcontent.Enabled = true;
            memoEditDiagnostic.Enabled = true;
            lookUpEditdisease.Enabled = true;
            textEditICD.Enabled = true;
            textEditremember.Enabled = true;
            checkEditsuggestions.Enabled = true;
            simpleButtonPreser.Enabled = true;
            spinEditOrder.Enabled = true;
        }


        //保存
        private void Preser()
        {
            dxErrorProvider.ClearErrors();
            var Word = memoEditcontent.Text.Trim();
            if (string.IsNullOrWhiteSpace(Word))
            {
                dxErrorProvider.SetError(memoEditcontent, string.Format(Variables.MandatoryTips, "项目字典"));
                memoEditcontent.Focus();
                return;
            }

            var Period = memoEditDiagnostic.Text.Trim();
            if (string.IsNullOrWhiteSpace(Period))
            {
                dxErrorProvider.SetError(memoEditDiagnostic, string.Format(Variables.MandatoryTips, "疾病名称"));
                memoEditDiagnostic.Focus();
                return;
            }

            if (lookUpEditdisease.EditValue == null)
            {
                dxErrorProvider.SetError(lookUpEditdisease, string.Format(Variables.MandatoryTips, "疾病状态"));
                lookUpEditdisease.Focus();
                return;
            }
            if (spinEditOrder.EditValue == null)
            {
                dxErrorProvider.SetError(lookUpEditdisease, string.Format(Variables.MandatoryTips, "序号"));
                spinEditOrder.Focus();
                return;
            }

            int IsSickness = (int)lookUpEditdisease.EditValue;
            

            var WBCode = textEditICD.Text.Trim();
            var HelpChar = textEditremember.Text.Trim();

            var orderNum = spinEditOrder.EditValue;


            try
            {
                if (_id == Guid.Empty)
                {
                    var input = new DictionaryItemDictionaryDto
                    {
                        Word = Word,
                        Period = Period,
                        IsSickness = IsSickness,
                        ApplySate = checkEditsuggestions.Checked ? 1 : 2,
                        WBCode = WBCode,
                        HelpChar = HelpChar,
                        DepartmentBMId = _Departentid,
                        iteminfoBMId = _ItemInfoid,
                        OrderNum = Convert.ToInt32(orderNum),
                         GWBM= textWGBM.Text

                    };

                    _dictionaryAppService.InsertItemDictionary(input);
                }
                else
                {
                    var input = new DictionaryItemDictionaryDto
                    {
                        Id = _id,
                        Word = Word,
                        Period = Period,
                        IsSickness = IsSickness,
                        ApplySate = checkEditsuggestions.Checked ? 1 : 2,
                        WBCode = WBCode,
                        HelpChar = HelpChar,
                        DepartmentBMId = _Departentid,
                        iteminfoBMId = _ItemInfoid,
                        OrderNum = Convert.ToInt32(orderNum),
                         GWBM = textWGBM.Text
                    };
                    _dictionaryAppService.UpdateItemDictionary(input);
                }
                //DialogResult = DialogResult.OK;
            }
            catch (UserFriendlyException exception)
            {
                ShowMessageBox(exception);
            }
            LoadData();
            MustNot();

        }

        private void simpleButtonUp_Click(object sender, EventArgs e)
        {
            var list = gridControl1.GetDtoListDataSource<DictionaryItemDictionaryDto>();
            if (list == null)
                return;
            var currentItem = gridControl1.GetFocusedRowDto<DictionaryItemDictionaryDto>();
            if (currentItem == null)
                return;
            if (currentItem == list.FirstOrDefault())
                return;
            var currentIndex = list.IndexOf(currentItem);
            var currentOrder = currentItem.OrderNum;
            var changeOrder = list[currentIndex - 1].OrderNum;
            if (currentOrder == changeOrder)
                changeOrder = changeOrder - 1;
            list[currentIndex].OrderNum = changeOrder;
            list[currentIndex - 1].OrderNum = currentOrder;
            gridControl1.DataSource = list.OrderBy(l => l.OrderNum).ToList();
            gridControl1.RefreshDataSource();
            gridViewItemDictionary.FocusedRowHandle = currentIndex - 1;
        }

        private void simpleButtonDown_Click(object sender, EventArgs e)
        {
            var list = gridControl1.GetDtoListDataSource<DictionaryItemDictionaryDto>();
            if (list == null)
                return;
            var currentItem = gridControl1.GetFocusedRowDto<DictionaryItemDictionaryDto>();
            if (currentItem == null)
                return;
            if (currentItem == list.LastOrDefault())
                return;
            var currentIndex = list.IndexOf(currentItem);
            var currentOrder = currentItem.OrderNum;
            var changeOrder = list[currentIndex + 1].OrderNum;
            if (currentOrder == changeOrder)
                changeOrder = changeOrder + 1;
            list[currentIndex].OrderNum = changeOrder;
            list[currentIndex + 1].OrderNum = currentOrder;
            gridControl1.DataSource = list.OrderBy(l => l.OrderNum).ToList();
            gridControl1.RefreshDataSource();
            gridViewItemDictionary.FocusedRowHandle = currentIndex + 1;
        }

        private void simpleButtonSaveOrder_Click(object sender, EventArgs e)
        {
            DictionaryItemDictionaryDto Dictionary = new DictionaryItemDictionaryDto();

            Dictionary.iteminfoBMId = _ItemInfoid;
            Dictionary.DepartmentBMId = _Departentid;

            var output = _dictionaryAppService.GetById(Dictionary);
            
            var list = gridControl1.GetDtoListDataSource<DictionaryItemDictionaryDto>();
            foreach (var item in list)
            {
                var oldData = output.Where(a => a.Id == item.Id).FirstOrDefault();
                if (oldData.OrderNum != item.OrderNum)
                {
                    try
                    {
                        _dictionaryAppService.UpdateItemDictionary(item);
                    }
                    catch (UserFriendlyException ex)
                    {
                        ShowMessageBoxError(ex.ToString());
                    }
                }
            }
            ShowMessageSucceed("保存成功!");
        }
    }
}