using DevExpress.XtraEditors;
using NPOI.SS.Formula.Functions;
using SelfControl;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.CommonTools
{
   public static  class BindCustomGridLookUpEdit<T> where T : new()
    {

        #region 绑定单位搜索框
        /// <summary>
        /// 绑定单位搜索框
        /// </summary>
        /// <param name="combo">CustomGridLookUpEdit控件</param>
        /// <param name="dt">填充数据</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="valueName">值内容</param>
        /// <param name="pinyin">拼音码</param>
        /// <param name="IndicatorWidth">行宽度</param>
        /// <param name="NotdisplayName">隐藏列</param>
        public static bool BindGridLookUpEdit(CustomGridLookUpEdit combo, List<T> dt, string displayName, 
            string valueName, string pinyin, int IndicatorWidth,string NotdisplayName)
        {
            bool bres = false;
            if (dt!=null)
            {
                combo.Properties.ValueMember = valueName;
                combo.Properties.DisplayMember = displayName;
                combo.Properties.DataSource = dt;
                combo.Properties.PopulateViewColumns();
                if (!string.IsNullOrEmpty(NotdisplayName))
                {
                    combo.Properties.View.Columns[NotdisplayName].Visible = false;
                }
                combo.Properties.View.Columns[displayName].Width = 300;
                combo.Properties.View.Columns[pinyin].Width = 200;
                combo.Properties.PopupFormMinSize = new System.Drawing.Size(600, 0);
                combo.Properties.View.IndicatorWidth = IndicatorWidth;
            }
            return bres;
        } //

        public static void BindGridLookUpEdit(SearchLookUpEdit txtClientDegree, List<UserFormDto> userList, string v1, string v2, string v3, int v4, string v5)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
