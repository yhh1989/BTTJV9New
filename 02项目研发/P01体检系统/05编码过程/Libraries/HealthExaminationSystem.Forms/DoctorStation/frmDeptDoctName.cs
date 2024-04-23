using DevExpress.XtraGrid.Columns;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.DoctorStation
{
    public partial class frmDeptDoctName : UserBaseForm
    {
        public frmDeptDoctName()
        {
            InitializeComponent();
        }

        private void frmDeptDoctName_Load(object sender, EventArgs e)
        {
           
            var Doct = DefinedCacheHelper.GetComboUsers().ToList();
            repositoryItemLookDoct.DataSource = Doct;
            var depart = DefinedCacheHelper.GetDepartments().ToList();
            repositoryItemLookDepart.DataSource = depart;
            List<DeptNameDto> obji = new List<DeptNameDto>();
            string fp = System.Windows.Forms.Application.StartupPath + "\\DepartNameinfo.json";
            if (File.Exists(fp))  // 判断是否已有相同文件 
            {
                obji = JsonConvert.DeserializeObject<List<DeptNameDto>>(File.ReadAllText(fp));

            }
            List<DeptNameDto> deptNameDtos = new List<DeptNameDto>();
            foreach (var depar in depart)
            {
                DeptNameDto DeptNameDto = new DeptNameDto();
                DeptNameDto.DepatId = depar.Id;
                if (obji != null )
                {
                    var deparName = obji.FirstOrDefault(o=>o.DepatId== depar.Id);
                    if (deparName != null)
                    {
                        var userid = Doct.FirstOrDefault(o=>o.Id== deparName.DoctId);
                        if (userid != null)
                        {
                            DeptNameDto.DoctId = userid.Id;
                        }
                    }

                }
                deptNameDtos.Add(DeptNameDto);
            }
            gridControl1.DataSource = deptNameDtos;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var departNamels = gridControl1.DataSource as List<DeptNameDto>;
            //departNamels = departNamels.Where(o=>o.DepatId !=null && o.DepatId !=Guid.Empty).ToList();
            // 获取当前程序所在路径，并将要创建的文件命名为info.json 
            string fp = System.Windows.Forms.Application.StartupPath + "\\DepartNameinfo.json";
            if (!File.Exists(fp))  // 判断是否已有相同文件 
            {
                FileStream fs1 = new FileStream(fp, FileMode.Create, FileAccess.ReadWrite);
                fs1.Close();
            }
            
            File.WriteAllText(fp, JsonConvert.SerializeObject(departNamels));
            this.DialogResult = DialogResult.OK;
        }
    }
    public class DeptNameDto
    {
        public Guid? DepatId { get; set; }
        public long? DoctId { get; set; }
    }
}
