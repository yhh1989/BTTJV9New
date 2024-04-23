using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.CommonFormat
{
  public  class ZYBDictionaryType
    {
       
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Key { get; set; }
        /// <summary>
        /// 展示名称
        /// </summary>
        public virtual string Value { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        public virtual string ParentId { get; set; }
    }

    public static class ZYBDictionaryTypels
    {
        //static List<ZYBDictionaryType> zybls;
        public static List<ZYBDictionaryType> getZYBDictionaryTypels()
        {
          var   zybls = new List<ZYBDictionaryType>();
           
            var inputls = EnumHelper.GetEnumDescs(typeof(ZYBBasicDictionaryType));
            foreach (var input in inputls)
            {
                ZYBDictionaryType zybbic = new ZYBDictionaryType();
                zybbic.Key = input.Key.ToString();
                zybbic.Value= input.Value.ToString();
                //行业小类
                //if (input.Key.ToString()== ZYBBasicDictionaryType.Industry.ToString())
                //{
                //    zybbic.ParentId = ZYBBasicDictionaryType.IndustryType.ToString();
                //}
                //病史小类
                  if (input.Key.ToString() == ZYBBasicDictionaryType.MedicalHistory.ToString())
                {
                    zybbic.ParentId = ZYBBasicDictionaryType.MedicalHistoryType.ToString();
                }
                //症状小类
                else if (input.Key.ToString() == ZYBBasicDictionaryType.Symptom.ToString())
                {
                    zybbic.ParentId = ZYBBasicDictionaryType.SymptomType.ToString();
                }
                zybls.Add(zybbic);
            }
            return zybls;

        }
       
    }
}
