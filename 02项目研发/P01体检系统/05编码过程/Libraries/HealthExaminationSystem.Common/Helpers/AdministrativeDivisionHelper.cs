using System;
using System.Collections.Generic;
using System.Linq;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 行政区划帮助器
    /// </summary>
    public class AdministrativeDivisionHelper
    {
        public static List<string> _numbers;

        static AdministrativeDivisionHelper()
        {
            _numbers = new List<string>(99);
            for (int i = 1; i < 100; i++)
            {
                _numbers.Add(i.ToString("D2"));
            }
        }
        /// <summary>
        /// 获取省级行政区划
        /// </summary>
        /// <returns></returns>
        public static List<AdministrativeDivisionDto> GetProvince()
        {
            GC.Collect();
            var result = DefinedCacheHelper.GetAdministrativeDivision();
            return result.Where(r => r.Code.EndsWith("0000")).ToList();
        }

        /// <summary>
        /// 获取市级行政区划
        /// </summary>
        /// <param name="province">隶属省级</param>
        /// <returns></returns>
        public static List<AdministrativeDivisionDto> GetCity(AdministrativeDivisionDto province)
        {
            //GC.Collect();
            //var prefix = province.Code.Substring(0, 2);
            //var result = DefinedCacheHelper.GetAdministrativeDivision();
            //var cities = result.Where(r => r.Code.StartsWith(prefix) && r.Code.EndsWith("00") && !r.Code.EndsWith("000")).ToList();
            //if (cities.Count == 0)
            //{
            //    return result.Where(r => r.Code.StartsWith(prefix) && !r.Code.EndsWith("000")).ToList();
            //}

            //return cities;

            GC.Collect();
            var prefix = province.Code.Substring(0, 2);
            var result = DefinedCacheHelper.GetAdministrativeDivision();
            var cities = result.Where(r => r.Code.StartsWith(prefix) && r.Code.Length<8 && _numbers.Contains(r.Code.Substring(2, 2)) && r.Code.EndsWith("00")).ToList();
            if (cities.Count == 0)
            {
                return result.Where(r => r.Code.StartsWith(prefix) && !r.Code.EndsWith("0000")).ToList();
            }

            return cities;
        }

        /// <summary>
        /// 获取县级行政区划
        /// </summary>
        /// <param name="city">隶属市级</param>
        /// <returns></returns>
        public static List<AdministrativeDivisionDto> GetCounty(AdministrativeDivisionDto city)
        {
            //GC.Collect();
            //var prefix = city.Code.Substring(0, 4);
            //var result = DefinedCacheHelper.GetAdministrativeDivision();
            //if (!city.Code.EndsWith("00"))
            //{
            //    return result.Where(r => r.Code == city.Code).ToList();
            //}
            //return result.Where(r => r.Code.StartsWith(prefix) && !r.Code.EndsWith("00") &&  r.Code.Length==6).ToList();

            GC.Collect();
            var prefix = city.Code.Substring(0, 4);
            var result = DefinedCacheHelper.GetAdministrativeDivision();
            if (!city.Code.EndsWith("00"))
            {
                return result.Where(r => r.Code == city.Code).ToList();
            }
            return result.Where(r => r.Code.StartsWith(prefix) && r.Code.Length < 8  && _numbers.Contains(r.Code.Substring(4, 2))).ToList();
        }
        /// <summary>
        /// 获取乡镇
        /// </summary>
        /// <param name="city">隶属市级</param>
        /// <returns></returns>
        public static List<AdministrativeDivisionDto> GetTownship(AdministrativeDivisionDto city)
        {
            GC.Collect();
            var prefix = city.Code.Substring(0, 6);
            var result = DefinedCacheHelper.GetAdministrativeDivision();
           
            return result.Where(r => r.Code.StartsWith(prefix) && r.Code.Length != 6).ToList();
        }
    }
}