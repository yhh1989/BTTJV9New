using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;

namespace Sw.Hospital.HealthExaminationSystem.Market
{
    /// <summary>
    /// 重要事项查看器
    /// </summary>
    public partial class ImportantMatterViewer : UserBaseForm
    {
        /// <summary>
        /// 重要事项查看器
        /// </summary>
        public ImportantMatterViewer(string content)
        {
            InitializeComponent();
            memoEdit1.Text = content;
        }
    }
}