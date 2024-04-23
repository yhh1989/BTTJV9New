using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public static class LookUpEditHelper
    {
        /// <summary>
        /// 若有添加其它按钮，很可以引起冲突
        /// </summary>
        /// <param name="lue"></param>
        public static void SetClearButton(this LookUpEdit lue, ButtonPredefines kind = ButtonPredefines.Delete)
        {
            if (lue.Properties.Buttons.Any(b => b.Kind == kind)) throw new ArgumentException($"{kind}已存在");
            lue.Properties.Buttons.Add(new EditorButton(kind));
            lue.ButtonClick += (s, e) => { if (e.Button.Kind == kind) { lue.EditValue = null; } };
        }

        /// <summary>
        /// 若有添加其它按钮，很可以引起冲突
        /// </summary>
        /// <param name="lue"></param>
        public static void SetClearButton(this SearchLookUpEdit lue, ButtonPredefines kind = ButtonPredefines.Delete)
        {
            if (lue.Properties.Buttons.Any(b => b.Kind == kind)) throw new ArgumentException($"{kind}已存在");
            lue.Properties.Buttons.Add(new EditorButton(kind));
            lue.ButtonClick += (s, e) => { if (e.Button.Kind == kind) { lue.EditValue = null; } };
        }

    }
}
