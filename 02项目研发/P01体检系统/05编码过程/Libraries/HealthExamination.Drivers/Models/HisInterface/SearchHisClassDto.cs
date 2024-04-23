﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExamination.Drivers.Models.HisInterface
{

   public  class SearchHisClassDto
    {

        /// <summary>
        /// 身份证号
        /// </summary>
        public string  IDCardNo { get; set; }

        /// <summary>
        ///科室名称
        /// </summary>
        public string DepartName { get; set; }


        /// <summary>
        ///组合名称
        /// </summary>
        public string GroupName { get; set; }


        /// <summary>
        ///项目名称
        /// </summary>
        public string ItemName { get; set; }



        /// <summary>
        ///项目编码
        /// </summary>
        public string ItemBM { get; set; }

    }
}
