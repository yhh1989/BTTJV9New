using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
   public  class GMedicalInfoDto
    {
        /// <summary>
        /// 脉率 次/分钟
        /// </summary>
        public string PULSE_FREQUENCY { get; set; }

        /// <summary>
        /// 呼吸频率
        /// </summary>
        public string RESPIRATORY_RATE { get; set; }
        /// <summary>
        /// 皮肤1正常 2潮红3苍白 4发绀5黄染 6色素沉着 7 其他
        /// </summary>
        public string SKIN { get; set; }

        /// <summary>
        /// 皮肤其他
        /// </summary>
        public string SKIN_OTHERS { get; set; }

        /// <summary>
        /// 淋巴结 1未触及2锁骨上3腋窝 4其他
        /// </summary>
        public string LYMPH_NODE { get; set; }


        /// <summary>
        ///淋巴结其他
        /// </summary>
        public string LYMPH_NODE_OTHERS { get; set; }

        /// <summary>
        ///心脏-心律 
        /// </summary>
        public string HEART_RATE { get; set; }

        /// <summary>
        ///心脏-心律1 齐 2不齐 3绝对不齐
        /// </summary>
        public string CARDIAC_RHYTHM { get; set; }

        /// <summary>
        ///心脏-心律心脏-杂音0 无 1 有
        /// </summary>
        public string CARDIAC_SOUFFLE { get; set; }

        /// <summary>
        ///心脏-杂音有描述
        /// </summary>
        public string CARDIAC_SOUFFLE_OTHERS { get; set; }

        /// <summary>
        ///肺-桶状胸1 是 0否
        /// </summary>
        public string LUNG_BARREL_CHEST { get; set; }

        /// <summary>
        ///肺-呼吸音1正常 2异常
        /// </summary>
        public string LUNG_BREATH_SOUND { get; set; }

        /// <summary>
        ///肺-呼吸音异常
        /// </summary>
        public string LUNG_BREATH_SOUND_EXCEP { get; set; }

        /// <summary>
        ///肺-罗音1无 2干罗音 3湿罗音4其他
        /// </summary>
        public string LUNG_RHONCHUS { get; set; }
        /// <summary>
        ///肺-罗音其他
        /// </summary>
        public string LUNG_RHONCHUS_EXCEPTION { get; set; }

        /// <summary>
        ///腹部-压痛 0 无 1有
        /// </summary>
        public string ABDO_PRESS_PAIN { get; set; }

        /// <summary>
        ///腹部-压痛描述
        /// </summary>
        public string ABDO_PRESS_PAIN_OTH { get; set; }
        /// <summary>
        ///腹部-包块0 无 1有
        /// </summary>
        public string ABDO_MASSES { get; set; }
        /// <summary>
        ///腹部-包块描述
        /// </summary>
        public string ABDO_MASSES_OTHERS { get; set; }
        /// <summary>
        ///腹部-肝大0 无 1有
        /// </summary>
        public string ABDO_HEPATOMEGALY { get; set; }

        /// <summary>
        ///腹部-肝大描述
        /// </summary>
        public string ABDO_HEPATOMEGALY_OTH { get; set; }

        /// <summary>
        ///腹部-脾大 0 无 1有
        /// </summary>
        public string ABDO_SPLENOMEGALY { get; set; }

        /// <summary>
        ///腹部-脾大描述
        /// </summary>
        public string ABDO_SPLENOMEGALY_OTH { get; set; }
        /// <summary>
        ///腹部-移动性浊音0 无 1有
        /// </summary>
        public string ABDO_SHIFTING_DULL { get; set; }
        /// <summary>
        ///腹部-移动性浊音描述
        /// </summary>
        public string ABDO_SHIFTING_DULL_OTH { get; set; }
        /// <summary>
        ///口腔-口唇1红润 2苍白 3发绀4皲裂 5疱疹
        /// </summary>
        public string LIPS { get; set; }
        /// <summary>
        ///口腔-齿列正常1是 0否
        /// </summary>
        public string DENTITIONDENTURE { get; set; }
        /// <summary>
        ///口腔-齿列缺齿1是 0否
        /// </summary>
        public string MISS_TEETH { get; set; }

        /// <summary>
        ///口腔-齿列缺齿1
        /// </summary>
        public string MISS_TEETH_1 { get; set; }

        /// <summary>
        ///口腔-齿列缺齿2
        /// </summary>
        public string MISS_TEETH_2 { get; set; }

        /// <summary>
        ///口腔-齿列缺齿3
        /// </summary>
        public string MISS_TEETH_3 { get; set; }

        /// <summary>
        ///口腔-齿列缺齿4
        /// </summary>
        public string MISS_TEETH_4 { get; set; }


        /// <summary>
        ///口腔-齿列龋齿1是 0否
        /// </summary>
        public string DENTAL_CARY { get; set; }

        /// <summary>
        ///口腔-齿列龋齿1
        /// </summary>
        public string DENTAL_CARY_1 { get; set; }


        /// <summary>
        ///口腔-齿列龋齿2
        /// </summary>
        public string DENTAL_CARY_2 { get; set; }
        /// <summary>
        ///口腔-齿列龋齿3
        /// </summary>
        public string DENTAL_CARY_3 { get; set; }
        /// <summary>
        ///口腔-齿列龋齿4
        /// </summary>
        public string DENTAL_CARY_4 { get; set; }
        /// <summary>
        ///口腔-齿列义齿1是 0否
        /// </summary>
        public string DENTURE { get; set; }

        /// <summary>
        ///口腔-齿列义齿1
        /// </summary>
        public string DENTURE_1 { get; set; }

        /// <summary>
        ///口腔-齿列义齿2
        /// </summary>
        public string DENTURE_2 { get; set; }

        /// <summary>
        ///口腔-齿列义齿3
        /// </summary>
        public string DENTURE_3 { get; set; }

        /// <summary>
        ///口腔-齿列义齿4
        /// </summary>
        public string DENTURE_4 { get; set; }

        /// <summary>
        ///口腔-咽部1无充血 2充血3淋巴滤泡增生
        /// </summary>
        public string PHARYNGEALPORTION { get; set; }

        /// <summary>
        ///视力-左眼
        /// </summary>
        public string VISION_LEFT_EYE { get; set; }

        /// <summary>
        ///视力-右眼
        /// </summary>
        public string VISION_RIGHT_EYE { get; set; }

        /// <summary>
        ///矫正视力-左眼
        /// </summary>
        public string STRAIGHTEN_VISION_LEFT_EYE { get; set; }

        /// <summary>
        ///矫正视力-右眼
        /// </summary>
        public string STRAIGHTEN_VISION_RIGHT_EYE { get; set; }

        /// <summary>
        ///听力1听见 2听不清或无法听见
        /// </summary>
        public string AUDITION { get; set; }

        /// <summary>
        ///运动功能1可顺利完成 2无法独立完成其中任何一个动作
        /// </summary>
        public string MOTOR_FUNCTION { get; set; }

        /// <summary>
        ///腹部B超1 正常 2异常
        /// </summary>
        public string TYPE_B_ULTRASONIC { get; set; }


        /// <summary>
        ///腹部B超异常描述
        /// </summary>
        public string TYPE_B_ULTRASONIC_EXCEP { get; set; }

        /// <summary>
        ///其他B超1 正常 2异常
        /// </summary>
        public string TYPE_B_ULTRASONIC_QT { get; set; }

        /// <summary>
        ///其他B超异常描述
        /// </summary>
        public string TYPE_B_ULTRASONIC_EXCEP_QT { get; set; }

        /// <summary>
        ///心电1 正常 2异常
        /// </summary>
        public string ELECTROCARDIOGRAM { get; set; }
        /// <summary>
        ///心电图异常描述
        /// </summary>
        public string ELECTROCARDIOGRAM_EXCEP { get; set; }
    }
}
