using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList.Nodes;
using HealthExaminationSystem.Enumerations.Helpers;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Picture.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using DialogResult = System.Windows.Forms.DialogResult;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.Users
{
    public partial class UserEditor : UserBaseForm
    {
        private readonly ICommonAppService _commonAppService;

        private readonly IFormRoleAppService _formRoleAppService;

        private readonly IDepartmentAppService _departmentAppService;

        private readonly IUserAppService _userAppService;

        private  long _userId;
        private UserViewDto _user;
        public  UpdateUserFormDto _Modle;

        private PictureController _pictureController;

        /// <summary>
        /// 新增
        /// </summary>
        public UserEditor()
        {
            InitializeComponent();

            _userId = -1;

            _commonAppService = new CommonAppService();
            _formRoleAppService = new FormRoleAppService();
            _departmentAppService = new DepartmentAppService();
            _userAppService = new UserAppService();
            _pictureController = new PictureController();
        }

        public UserEditor(long id) : this()
        {
            _userId = id;
        }

        private static string PicAddress { get; set; }

        private static byte[] byt { get; set; }
        private Guid? singImage { get; set; }
        /// <summary>
        /// 初始化窗体
        /// </summary>
        private void InitForm()
        {
            var sexs = SexHelper.GetSexModels();
            lookUpEditSex.Properties.DataSource = sexs;
            lookUpEditSex.ItemIndex = 0;

            dateEditBrithDay.DateTime = DateTime.Now;

            var rolelist = _formRoleAppService.GetAll();
            checkedComboBoxFormRole.Properties.DataSource = rolelist;

            var jobStatus = JobStatusHelper.GetJobStatusModels();
            lookUpEditJobStatus.Properties.DataSource = jobStatus;
            lookUpEditJobStatus.EditValue = (int)JobStatus.OnGuard;

            var ifTypes = IfTypeHelper.GetIfTypeModels();
            lookUpEditToll.Properties.DataSource = ifTypes;

            // 员工级别处理

            lookUpEditEnabled.Properties.DataSource = ifTypes;
            lookUpEditEnabled.EditValue = (int)IfType.True;
            //单位审核权限
            lookUpEditClientZK.Properties.DataSource = ClientZKStateHelper.GetBloodStateModel();
            lookUpEditClientZK.EditValue = (int)ClientZKState.Normal;
            var HospitalArealist = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.HospitalArea);
            HospitalArealist.Add(new Application.BasicDictionary.Dto.BasicDictionaryDto {
                Value = 999,
                Text = "所有",
                Type = "HospitalArea"
            });
            lookUpEditHospitalArea.Properties.DataSource = HospitalArealist;

            try
            {
                var departments = _departmentAppService.GetAll();
                treeListDepartments.DataSource = departments.OrderBy(n => n.OrderNum).ToList();
            }
            catch (UserFriendlyException e)
            {
                ShowMessageBox(e);
            }
        }

        private void LoadData()
        {
            try
            {
                var user = _userAppService.GetUser(new EntityDto<long> { Id = _userId });
                _user = user;
                textEditUserName.Text = user.UserName;

                textEditJobNumber.Text = user.EmployeeNum;

                textEditName.Text = user.Name;

                textEditHelpChar.Text = user.HelpChar;
                lookUpEditSex.EditValue = user.Sex;
                if (user.Birthday != null)
                    dateEditBrithDay.DateTime = user.Birthday.Value;
                textEditPhone.Text = user.PhoneNumber;
                if (user.FormRoles != null)
                {
                    var roles = new List<Guid>();
                    foreach (var role in user.FormRoles)
                    {
                        roles.Add(role.Id);
                    }
                    checkedComboBoxFormRole.SetEditValue(roles);
                }

                textEditMail.Text = user.EmailAddress;
                if (user.TbmDepartments != null)
                {
                    var departments = new List<Guid>();
                    foreach (var departmentDto in user.TbmDepartments)
                    {
                        departments.Add(departmentDto.Id);
                    }

                    foreach (TreeListNode node in treeListDepartments.Nodes)
                    {
                        var id = (Guid)node.GetValue(treeListColumnDepartmentId);
                        if (departments.Contains(id))
                        {
                            treeListDepartments.SetNodeCheckState(node, System.Windows.Forms.CheckState.Checked);
                        }
                    }
                }
                if (user != null)
                {
                    textEditUserZKL.Text = user.Discount;
                }
                if (user.ClientZKS.HasValue)
                {
                    lookUpEditClientZK.EditValue = (int)user.ClientZKS;
                }
                if (user.HospitalArea.HasValue)
                {
                    lookUpEditHospitalArea.EditValue = (int)user.HospitalArea;
                    
                }
                lookUpEditJobStatus.EditValue = user.State;
                //var toll = (int)lookUpEditToll.EditValue;
                //textEditDomain.Text = user.DomainName;
                textEditFunction.Text = user.Duty;
                // 员工级别处理
                lookUpEditEnabled.EditValue = user.IsActive ? (int)IfType.True : (int)IfType.False;
                //memoEditAddress.Text = user.Address;
                // 签名处理
                //if (user.SignImage.HasValue)
                //{
                //    singImage =user.SignImage.Value;
                //    var result = _pictureController.GetUrl(new Guid(user.SignImage));
                //    using (var stream = ImageHelper.GetUriImageStream(new Uri(result.Thumbnail)))
                //    {
                //        pictureEditSign.Image = Image.FromStream(stream);
                //    }

                //    //pictureEditSign.Image = new Bitmap((new System.Net.WebClient()).OpenRead(url));
                //}

                // 签名处理
                if (user.SignImage.HasValue)
                {
                    singImage = user.SignImage.Value;
                    var result = _pictureController.GetUrlUser(user.SignImage.Value);
                    pictureEditSign.Image = ImageHelper.GetUriImage(new Uri(result.RelativePath));
                }
                //Image obj = Image.FromStream(System.Net.WebRequest.Create(imagePath).GetResponse().GetResponseStream());
                // pictureEdit1.Image = new Bitmap((new System.Net.WebClient()).OpenRead(url));

               
            }
            catch (UserFriendlyException e)
            {
                ShowMessageBox(e);
            }
        }

        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            dxErrorProvider.ClearErrors();

            var userName = textEditUserName.Text.Trim();
            if (string.IsNullOrWhiteSpace(userName))
            {
                dxErrorProvider.SetError(textEditUserName, string.Format(Variables.MandatoryTips, "工号"));
                textEditUserName.Focus();
                return;
            }
            textEditJobNumber.Text = userName;
            var jobNumber = textEditJobNumber.Text.Trim();
            //if (string.IsNullOrWhiteSpace(jobNumber))
            //{
            //    dxErrorProvider.SetError(textEditJobNumber, string.Format(Variables.MandatoryTips, "工号"));
            //    textEditJobNumber.Focus();
            //    return;
            //}
          

            var name = textEditName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                dxErrorProvider.SetError(textEditName, string.Format(Variables.MandatoryTips, "姓名"));
                textEditName.Focus();
                return;
            }

            var pwd = textEditPwd.Text;
            if (_userId == -1)
            {
                if (string.IsNullOrWhiteSpace(pwd))
                {
                    //dxErrorProvider.SetError(textEditPwd, string.Format(Variables.MandatoryTips, "密码"));
                    //textEditPwd.Focus();
                    //return;
                    pwd = "&&&&&&";
                }
            }
            var Eail = textEditMail.Text.Trim();
          
            if (string.IsNullOrWhiteSpace(Eail))
            {
                //textEditMail.Text = jobNumber + "@qq.com";
                Eail = jobNumber + "@qq.com";
                //dxErrorProvider.SetError(textEditMail, string.Format(Variables.MandatoryTips, "邮箱"));
                //textEditPwd.Focus();
                //return;
            }
            else
            {
                Regex re = new Regex(@"[\w!#$%&'*+/=?^_`{|}~-]+(?:\.[\w!#$%&'*+/=?^_`{|}~-]+)*@(?:[\w](?:[\w-]*[\w])?\.)+[\w](?:[\w-]*[\w])?");//实例化一个Regex对象

                if (!re.IsMatch(Eail))//验证数据是否匹配
                {
                    dxErrorProvider.SetError(textEditMail, string.Format(Variables.BoxFormat, "邮箱"));
                    textEditPwd.Focus();
                    return;
                }
                Eail = textEditMail.Text.Trim();
            }
            var ZKL = textEditUserZKL.EditValue?.ToString();
            if (!string.IsNullOrWhiteSpace(ZKL))
            {
                if (double.Parse(ZKL) > 1)
                {
                    dxErrorProvider.SetError(textEditUserZKL, string.Format(Variables.GreaterThanTips, "折扣率", "'100'"));
                    textEditPwd.Focus();
                    return;
                }
                if (double.Parse(ZKL) < 0)
                {
                    dxErrorProvider.SetError(textEditUserZKL, string.Format(Variables.Negative, "折扣率"));
                    textEditPwd.Focus();
                    return;
                }
            }
            else
            {
                ZKL = "1";
            }
            var helpChar = textEditHelpChar.Text.Trim();
            var sex = 1;
            if (lookUpEditSex.EditValue != null)
                sex = (int)lookUpEditSex.EditValue;
            var birthday = dateEditBrithDay.DateTime;
            var phone = textEditPhone.Text.Trim();
            var roles = new List<Guid>();
            var selectRoles = (List<object>)checkedComboBoxFormRole.EditValue;
            foreach (Guid roleId in selectRoles)
                roles.Add(roleId);
          
            var ClientZK = 1;
            if (lookUpEditClientZK.EditValue !=null)
             ClientZK = (int)lookUpEditClientZK.EditValue;
            var hospitalArea = 999;
            if (lookUpEditHospitalArea.EditValue != null)
                hospitalArea = (int)lookUpEditHospitalArea.EditValue;
            var departments = new List<Guid>();
            foreach (TreeListNode node in treeListDepartments.Nodes)
            {
                if (node.Checked)
                {
                    var id = (Guid)node.GetValue(treeListColumnDepartmentId);
                    departments.Add(id);
                }
            }
            var jobStatus = 1;
            if (lookUpEditJobStatus.EditValue != null)
                jobStatus = (int)lookUpEditJobStatus.EditValue;
            //var toll = (int)lookUpEditToll.EditValue;
            //var domainName = textEditDomain.Text.Trim();
            var function = textEditFunction.Text.Trim();
            // 员工级别处理
            var enabled = (int)lookUpEditEnabled.EditValue;
            //var address = memoEditAddress.Text.Trim();
            // 签名处理
            var user = _userAppService.GetUser(new EntityDto<long> { Id = CurrentUser.Id });
            if (user == null)
            {
                XtraMessageBox.Show("您已被管理员删除，无法进行此操作");
                return;
            }
            // 用户如果没有图片选择上传操作
            // 如果有图片则为更新操作
            // 删除用户需要包含删除操作
            // 写代码这么简单就放心的使用了，是自己脑子太简单了还是写代码的时候思想已经云游天外了？
            // 请补全所有操作在启用
            PictureDto result = null;
            //PictureDto pictureDto = null;
            //if (!string.IsNullOrWhiteSpace(PicAddress))
            //    result = _pictureController.Uploading(PicAddress, "Test"); PicAddress = null;
            try
            {
                if (_userId == -1)
                {
                    if (!string.IsNullOrWhiteSpace(PicAddress))
                    {
                        result = _pictureController.UploadingUser(PicAddress, "UserSign");
                        PicAddress = null;
                    }
                    var createUserFormDto = new CreateUserFormDto
                    {
                        UserName = userName,
                        EmployeeNum = jobNumber,
                        Name = name,
                        Password = pwd,
                        HelpChar = helpChar,
                        Sex = sex,
                        Birthday = birthday,
                        PhoneNumber = phone,
                        FormRoleIds = roles,
                        EmailAddress = Eail,
                        DepartmentIds = departments,
                        State = jobStatus,
                        //DomainName = domainName,
                        Duty = function,
                        IsActive = enabled == (int)IfType.True,
                        //Address = address,
                        Discount = ZKL,
                        ClientZKS = ClientZK,
                        HospitalArea = hospitalArea
                    };
                    if (result != null)
                    {
                        createUserFormDto.SignImage = result.Id;
                    }
                    _userAppService.CreateUser(createUserFormDto);
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(PicAddress))
                    {
                        if (_user.SignImage.HasValue)
                        {
                            result = _pictureController.UpdateUser(PicAddress, _user.SignImage.Value);
                            PicAddress = null;
                        }
                        else
                        {
                            result = _pictureController.UploadingUser(PicAddress, "UserSign");
                            PicAddress = null;
                        }
                    }
                    var updateUserFormDto = new UpdateUserFormDto
                    {
                        Id = _userId,
                        UserName = userName,
                        EmployeeNum = jobNumber,
                        Name = name,
                        HelpChar = helpChar,
                        Sex = sex,
                        Birthday = birthday,
                        PhoneNumber = phone,
                        FormRoleIds = roles,
                        EmailAddress = Eail,
                        DepartmentIds = departments,
                        State = jobStatus,
                        //DomainName = domainName,
                        Duty = function,
                        IsActive = enabled == (int)IfType.True,
                        //Address = address,
                        Discount = ZKL,
                        ClientZKS = ClientZK,
                         HospitalArea= hospitalArea

                    };
                    if (result != null)
                        updateUserFormDto.SignImage = result.Id;
                    if (singImage != null && result == null)
                        updateUserFormDto.SignImage = singImage;
                    _userAppService.UpdateUser(updateUserFormDto);
                    _Modle = updateUserFormDto;
                    if (CurrentUser.Id == _userId)
                    {
                        CurrentUser.Name = updateUserFormDto.Name;
                        CurrentUser.Discount = updateUserFormDto.Discount;
                    }
                }
                DialogResult = DialogResult.OK;
            }
            catch (UserFriendlyException ex)
            {
                // XtraMessageBox.Show(exception.Message.Replace("Name", "工号").Replace("is already taken", "重复"));
                var mess = ex.Description.Replace("Message","提示").Replace("Name", "工号").Replace("is already taken", "重复");
                XtraMessageBox.Show(this, mess, ex.Code.ToString(), ex.Buttons, ex.Icon);
            }
        }
   
        private void OperationSetting_Load(object sender, EventArgs e)
        {
            InitForm();
            if (_userId != -1)
            {
                LoadData();
                textEditPwd.Enabled = false;
            }
            if (_userId == -1)
            {
                simpleButtonReset.Enabled = false;
            }
          
        }

        private void pictureEditSign_Click(object sender, EventArgs e)
        {
            //ShowPic(pictureEditSign);
            openFileDialogSign.ShowDialog(this);
        }

        private void textEditName_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            CommonHelper.SetHelpChar(textEditHelpChar, textEditName);
        }

        /// <summary>
        /// 选择图片
        /// </summary>
        /// <param name="picEdit"></param>
        public static void ShowPic(PictureEdit picEdit)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\";
            ofd.Filter =
                "Image Files(*.JPG;*.PNG;*.jpeg;*.GIF;*.BMP)|*.JPG;*.PNG;*.GIF;*.BMP;*.jpeg|All files(*.*)|*.*";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                PicAddress = ofd.FileName;
                var imge = Image.FromFile(PicAddress);
                var bm = new Bitmap(imge, picEdit.Width, picEdit.Height);
                picEdit.Image = bm;
                byt = Bitmap2Byte(bm);
            }
        }

        /// <summary>
        /// bitmap转byte[]
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static byte[] Bitmap2Byte(Bitmap bitmap)
        {
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Jpeg);
                var data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return data;
            }
        }

        /// <summary>
        /// 转换为Byte[]
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static byte[] Getcontent(string filepath) //将指定路径下的文件转换成二进制代码，用于传输到数据库
        {
            var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            var bydata = new byte[fs.Length]; //新建用于保存文件流的字节数组
            fs.Read(bydata, 0, bydata.Length); //读取文件流
            fs.Close();
            return bydata;
        }

        /// <summary>
        /// 读取byte[]并转化为图片
        /// </summary>
        /// <param name="bytes">byte[]</param>
        /// <returns>Image</returns>
        public static Image GetImageByBytes(byte[] bytes)
        {
            Image photo;
            using (var ms = new MemoryStream(bytes))
            {
                ms.Write(bytes, 0, bytes.Length);
                photo = Image.FromStream(ms, true);
                ms.Dispose();
                ms.Close();
            }

            return photo;
        }
        //
        private void simpleButtonReset_Click(object sender, EventArgs e)
        {
            if (_userId == -1)
            {
                return;
            }
            var question = XtraMessageBox.Show("确定要重置密码？", "询问",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question,
                   MessageBoxDefaultButton.Button2);
            if (question != DialogResult.Yes)
                return;
            try
            {
                _userAppService.ResetPassword(new UpdatePwdDto { UserId = _userId, Password = "123qwe", NewPassword = "123qwe", ConfirmPassword = "123qwe" });
                XtraMessageBox.Show("重置成功,当前用户的新密码为：123qwe");
            }
            catch (UserFriendlyException exception)
            {
                ShowMessageBox(exception);
            }
        }

        private void openFileDialogSign_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PicAddress = openFileDialogSign.FileName;
            var img = Image.FromFile(PicAddress);
            if (img.Width > 100)
            {
                MessageBox.Show("图片的宽度不能大于100像素，高度不能大于50像素");
                return;
            }
            if (img.Height > 50)
            {
                MessageBox.Show("图片的宽度不能大于100像素，高度不能大于50像素");
                return;
            }
            pictureEditSign.Image = Image.FromFile(PicAddress);
            
        }
        
        private void UserEditor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        private void layoutControl2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }

        }

        private void simpleButtonNewUser_Click(object sender, EventArgs e)
        {
            this.Close();
            using (var frm = new UserEditor())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
            //_userId = -1;
            //try
            //{
                
            //    _user = new UserViewDto();
            //    textEditUserName.Text = "";

            //    textEditJobNumber.Text = "";

            //    textEditName.Text = "";

            //    textEditHelpChar.Text = "";
            //    lookUpEditSex.EditValue = null;
              
            //        dateEditBrithDay.EditValue = null;
            //    textEditPhone.Text ="";
             
            //    checkedComboBoxFormRole.EditValue = null;
            //    textEditMail.Text ="";
            //    if (user.TbmDepartments != null)
            //    {
            //        var departments = new List<Guid>();
            //        foreach (var departmentDto in user.TbmDepartments)
            //        {
            //            departments.Add(departmentDto.Id);
            //        }

            //        foreach (TreeListNode node in treeListDepartments.Nodes)
            //        {
            //            var id = (Guid)node.GetValue(treeListColumnDepartmentId);
            //            if (departments.Contains(id))
            //            {
            //                treeListDepartments.SetNodeCheckState(node, System.Windows.Forms.CheckState.Checked);
            //            }
            //        }
            //    }
            //    treeListDepartments.chec
            //    if (user != null)
            //    {
            //        textEditUserZKL.Text = user.Discount;
            //    }
            //    if (user.ClientZKS.HasValue)
            //    {
            //        lookUpEditClientZK.EditValue = (int)user.ClientZKS;
            //    }
            //    lookUpEditJobStatus.EditValue = user.State;
            //    //var toll = (int)lookUpEditToll.EditValue;
            //    //textEditDomain.Text = user.DomainName;
            //    textEditFunction.Text = user.Duty;
            //    // 员工级别处理
            //    lookUpEditEnabled.EditValue = user.IsActive ? (int)IfType.True : (int)IfType.False;
            //    //memoEditAddress.Text = user.Address;
            //    // 签名处理
            //    //if (user.SignImage.HasValue)
            //    //{
            //    //    singImage =user.SignImage.Value;
            //    //    var result = _pictureController.GetUrl(new Guid(user.SignImage));
            //    //    using (var stream = ImageHelper.GetUriImageStream(new Uri(result.Thumbnail)))
            //    //    {
            //    //        pictureEditSign.Image = Image.FromStream(stream);
            //    //    }

            //    //    //pictureEditSign.Image = new Bitmap((new System.Net.WebClient()).OpenRead(url));
            //    //}

            //    // 签名处理
            //    if (user.SignImage.HasValue)
            //    {
            //        singImage = user.SignImage.Value;
            //        var result = _pictureController.GetUrl(user.SignImage.Value);
            //        pictureEditSign.Image = ImageHelper.GetUriImage(new Uri(result.RelativePath));
            //    }
            //    //Image obj = Image.FromStream(System.Net.WebRequest.Create(imagePath).GetResponse().GetResponseStream());
            //    // pictureEdit1.Image = new Bitmap((new System.Net.WebClient()).OpenRead(url));


            //}
            //catch (UserFriendlyException e)
            //{
            //    ShowMessageBox(e);
            //}
            simpleButtonReset.Enabled = false;
             

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (_userId == -1)
            {
                return;
            }
            var question = XtraMessageBox.Show("密码清空成功？", "询问",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question,
                   MessageBoxDefaultButton.Button2);
            if (question != DialogResult.Yes)
                return;
            try
            {
                _userAppService.ResetPassword(new UpdatePwdDto { UserId = _userId, Password = "&&&&&&", NewPassword = "&&&&&&", ConfirmPassword = "&&&&&&" });
                XtraMessageBox.Show("密码清空成功！");
            }
            catch (UserFriendlyException exception)
            {
                ShowMessageBox(exception);
            }
        }
    }
}