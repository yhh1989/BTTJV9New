using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    //用户查询
    public class getAuthKeyGetDto
    {
        public doctordata doctor { get; set; }
     
    }
    public class doctordata
    {
        //医生姓名
        public string xm { get; set; }

        //医生所在机构
        public string jgdm { get; set; }
    }
    //个档查询
    public class getJkdaGrJbxxIGetDto
    {
        public doctorJkdaGrJbxxIGetDto doctor { get; set; }

        public PaientJkdaGrJbxxIGetDto patient { get; set; }


    }
    //添加
    public class getJkdaGrJbxxIGetAddDto
    {
        public doctorJkdaGrJbxxIGetDto doctor { get; set; }
        public PaientAdd patient { get; set; }


    }
    public class doctorJkdaGrJbxxIGetDto
    {
        //医生姓名
        public string loginId { get; set; }

        //医生所在机构
        public string token { get; set; }
        //身份证号
        public string jgdm { get; set; }
    }
    public class PaientJkdaGrJbxxIGetDto
    {
        //身份证号
        public string sfzh { get; set; }
    }
    //修改dto
    public class updateJkdaTjBg
    {
        public doctorJkdaGrJbxxIGetDto doctor { get; set; }
        public PaientInfor patient { get; set; }
    }
    #region PaientInfor
    public class PaientInfor
    {
        public string loginId { get; set; }
        public string jgdm { get; set; }
        public string token { get; set; }
        public string ytjid { get; set; }
        public string sfzh { get; set; }
        public string tjsj { get; set; }
        public string tjys { get; set; }
        public string yljgdm { get; set; }
        public string zz { get; set; }
        public string tw { get; set; }
        public string hxpl { get; set; }
        public string ml { get; set; }
        public string zcssy { get; set; }
        public string zcszy { get; set; }
        public string ycssy { get; set; }
        public string ycszy { get; set; }
        public string tz { get; set; }
        public string sg { get; set; }
        public string yw { get; set; }
        public string tzzs { get; set; }
        public string lnrjkztzwpg { get; set; }
        public string lnrshzlnlpg { get; set; }
        public string lnrrzgn { get; set; }
        public string lnrrzgnpf { get; set; }
        public string lnrqgztpg { get; set; }
        public string lnrqgztpf { get; set; }
        public string tydl_dlpl { get; set; }
        public string tydl_jcdlsj { get; set; }
        public string tydl_mcdlsj { get; set; }
        public string tydl_dlfs { get; set; }
        public string ysxg { get; set; }
        public string xyqk_xyzk { get; set; }
        public string xyqk_rxyl { get; set; }
        public string xyqk_ksxynl { get; set; }
        public string xyqk_jynl { get; set; }
        public string yjqk_yjpl { get; set; }
        public string yjqk_ryjl { get; set; }
        public string yjqk_sfzj { get; set; }
        public string yjqk_sfjj { get; set; }
        public string yjqk_yjzl { get; set; }
        public string yjqk_ksyjnl { get; set; }
        public string yjqk_jjnl { get; set; }
        public string zybwxys_yw { get; set; }
        public string zybwxys_gz { get; set; }
        public string zybwxys_cysj { get; set; }
        public string zybwxys_fc { get; set; }
        public string zybwxys_fcfhcs { get; set; }
        public string zybwxys_fcfhcsms { get; set; }
        public string zybwxys_fswz { get; set; }
        public string zybwxys_fswzfhcs { get; set; }
        public string zybwxys_fswzfhcsms { get; set; }
        public string zybwxys_wlys { get; set; }
        public string zybwxys_wlysfhcs { get; set; }
        public string zybwxys_wlysfhcsms { get; set; }
        public string zybwxys_hxwz { get; set; }
        public string zybwxys_hxwzfhcs { get; set; }
        public string zybwxys_hxwzfhcsms { get; set; }
        public string zybwxys_qt { get; set; }
        public string zybwxys_qtfhcs { get; set; }
        public string zybwxys_qtfhcsms { get; set; }
        public string kq_kcwglb { get; set; }
        public string kq_cllb { get; set; }
        public string kq_ybjcjg { get; set; }
        public string sl_zyly { get; set; }
        public string sl_yyly { get; set; }
        public string sl_zyjz { get; set; }
        public string sl_yyjz { get; set; }
        public string tljcjg { get; set; }
        public string ydgnzt { get; set; }
        public string yd { get; set; }
        public string ydycms { get; set; }
        public string pf { get; set; }
        public string pfqtms { get; set; }
        public string gm { get; set; }
        public string gmqtms { get; set; }
        public string lbjjc { get; set; }
        public string lbjjcqtms { get; set; }
        public string f_tzx { get; set; }
        public string f_hxy { get; set; }
        public string f_hxyyc { get; set; }
        public string f_ly { get; set; }
        public string f_lyqt { get; set; }
        public string xz_xl { get; set; }
        public string xz_xllb { get; set; }
        public string xz_zy { get; set; }
        public string xz_zyms { get; set; }
        public string fb_yt { get; set; }
        public string fb_ytms { get; set; }
        public string fb_bk { get; set; }
        public string fb_bkms { get; set; }
        public string fb_gd { get; set; }
        public string fb_gdms { get; set; }
        public string fb_pd { get; set; }
        public string fb_pdms { get; set; }
        public string fb_ydxzy { get; set; }
        public string fb_ydxzyms { get; set; }
        public string xzszjc { get; set; }
        public string gmzz { get; set; }
        public string gmzzqt { get; set; }
        public string zbdmbd { get; set; }
        public string rx { get; set; }
        public string rxqt { get; set; }
        public string fkjc_wy { get; set; }
        public string fkjc_wyyc { get; set; }
        public string fkjc_yd { get; set; }
        public string fkjc_ydyc { get; set; }
        public string fkjc_gj { get; set; }
        public string fkjc_gjyc { get; set; }
        public string fkjc_gt { get; set; }
        public string fkjc_gtyc { get; set; }
        public string fkjc_zgfj { get; set; }
        public string fkjc_zgfjyc { get; set; }
        public string ct_qt { get; set; }
        public string xcg { get; set; }
        public string xcg_xhdb { get; set; }
        public string xcg_bxb { get; set; }
        public string xcg_xxb { get; set; }
        public string xcg_qt { get; set; }
        public string ncg { get; set; }
        public string ncg_ndb { get; set; }
        public string ncg_nt { get; set; }
        public string ncg_ntt { get; set; }
        public string ncg_nxx { get; set; }
        public string ncg_qt { get; set; }
        public string nwlbdb { get; set; }
        public string kfxt { get; set; }
        public string thxhdb { get; set; }
        public string dbqx { get; set; }
        public string ygbmky { get; set; }
        public string ggn { get; set; }
        public string ggn_xqgbzam { get; set; }
        public string ggn_xqgczam { get; set; }
        public string ggn_bdb { get; set; }
        public string ggn_zdhs { get; set; }
        public string ggn_jhdhs { get; set; }
        public string sgn { get; set; }
        public string sgn_xqjg { get; set; }
        public string sgn_xnsd { get; set; }
        public string sgn_xjnd { get; set; }
        public string sgn_xnnd { get; set; }
        public string xz { get; set; }
        public string xz_zdgcz { get; set; }
        public string xz_gysz { get; set; }
        public string xz_xqdmdzdb { get; set; }
        public string xz_xqgmdzdb { get; set; }
        public string xdt { get; set; }
        public string xdtyc { get; set; }
        public string xsx { get; set; }
        public string xsxyc { get; set; }
        public string bchao { get; set; }
        public string bchaoyc { get; set; }
        public string gjtp { get; set; }
        public string gjtpyc { get; set; }
        public string fzjc_qt { get; set; }
        public string nxgjb { get; set; }
        public string nxgjbqt { get; set; }
        public string szjb { get; set; }
        public string szjbqt { get; set; }
        public string xzjb { get; set; }
        public string xzjbqt { get; set; }
        public string xgjb { get; set; }
        public string xgjbqt { get; set; }
        public string ybjb { get; set; }
        public string ybjbqt { get; set; }
        public string sjxtjbqt { get; set; }
        public string sjxtjb { get; set; }
        public string qtxtjb { get; set; }
        public string qtxtjbqt { get; set; }
        public string jtywycbm { get; set; }
        public string yc { get; set; }
        public string jkzdbm { get; set; }
        public string wxyskzbm { get; set; }
        public string jytz { get; set; }
        public string jyjzym { get; set; }
        public string qtjy { get; set; }
        public string lnrShzlnlpgzb { get; set; }
        public string yyqk { get; set; }
        public string zyqk { get; set; }
        public string jtbsqk { get; set; }
        public string fmyqk { get; set; }

    }
    #endregion


    //添加dto
    public class addJkdaTjBgjlInfo
    {
        public getJkdaGrJbxxIGetAddDto Doctor { get; set; }
        public PaientAdd patient { get; set; }
        ////用药情况
        //public Drugjson drugjson { get; set; }
        ////个人住院情况
        //public Personjson Personjson { get; set; }
        ////家庭病床史情况
        //public Familyjson familyjson { get; set; }
        ////非免疫规划预防
        //public immunityjson immunity { get; set; }
        ////老年人的自理能力
        //public Oldjson oldjson { get; set; }
    }
    public class Oldjson
    {
        public string q1 { get; set; }
        public string q2 { get; set; }
        public string q3 { get; set; }
        public string q4 { get; set; }
        public string q5 { get; set; }
    }
    public class immunityjson
    {
        public string jzmc { get; set; }
        public string jzsj { get; set; }
        public string jzjg { get; set; }
    }
    public class Familyjson
    {
        public string jcsj { get; set; }
        public string ccsj { get; set; }
        public string zyyy { get; set; }
        public string zyyljgmc { get; set; }
        public string bah { get; set; }
    }
    public class Personjson
    {
        public string jcsj { get; set; }
        public string ccsj { get; set; }
        public string zyyy { get; set; }
        public string zyyljgmc { get; set; }
        public string bah { get; set; }
    }
    public class Drugjson
    {
        public string ywzwmc { get; set; }
        public string ywyf { get; set; }
        public string ywyl { get; set; }
        public string yysj { get; set; }
        public string fyycx { get; set; }
    }
    public class PaientAdd
    {
        public string ytjid { get; set; }
        public string sfzh { get; set; }
        public string tjsj { get; set; }
        public string tjys { get; set; }
        public string yljgdm { get; set; }
        public string zz { get; set; }
        public string tw { get; set; }
        public string ml { get; set; }
        public string hxpl { get; set; }
        public string zcssy { get; set; }
        public string zcszy { get; set; }
        public string ycssy { get; set; }
        public string ycszy { get; set; }
        public string tz { get; set; }
        public string sg { get; set; }
        public string yw { get; set; }
        public string tzzs { get; set; }
        public string lnrjkztzwpg { get; set; }
        public string lnrshzlnlpg { get; set; }
        public string lnrrzgn { get; set; }
        public string lnrrzgnpf { get; set; }
        public string lnrqgztpg { get; set; }
        public string lnrqgztpf { get; set; }
        public string tydl_dlpl { get; set; }
        public string tydl_jcdlsj { get; set; }
        public string tydl_mcdlsj { get; set; }
        public string tydl_dlfs { get; set; }
        public string ysxg { get; set; }
        public string xyqk_xyzk { get; set; }
        public string xyqk_rxyl { get; set; }
        public string xyqk_ksxynl { get; set; }
        public string xyqk_jynl { get; set; }
        public string yjqk_yjpl { get; set; }
        public string yjqk_ryjl { get; set; }
        public string yjqk_sfzj { get; set; }
        public string yjqk_sfjj { get; set; }
        public string yjqk_yjzl { get; set; }
        public string yjqk_ksyjnl { get; set; }
        public string yjqk_jjnl { get; set; }
        public string zybwxys_yw { get; set; }
        public string zybwxys_gz { get; set; }
        public string zybwxys_cysj { get; set; }
        public string zybwxys_fc { get; set; }
        public string zybwxys_fcfhcs { get; set; }
        public string zybwxys_fcfhcsms { get; set; }
        public string zybwxys_fswz { get; set; }
        public string zybwxys_fswzfhcs { get; set; }
        public string zybwxys_fswzfhcsms { get; set; }
        public string zybwxys_wlys { get; set; }
        public string zybwxys_wlysfhcs { get; set; }
        public string zybwxys_wlysfhcsms { get; set; }
        public string zybwxys_hxwz { get; set; }
        public string zybwxys_hxwzfhcs { get; set; }
        public string zybwxys_hxwzfhcsms { get; set; }
        public string zybwxys_qt { get; set; }
        public string zybwxys_qtfhcs { get; set; }
        public string zybwxys_qtfhcsms { get; set; }
        public string kq_kcwglb { get; set; }
        public string kq_cllb { get; set; }
        public string kq_ybjcjg { get; set; }
        public string sl_zyly { get; set; }
        public string sl_yyly { get; set; }
        public string sl_zyjz { get; set; }
        public string sl_yyjz { get; set; }
        public string tljcjg { get; set; }
        public string ydgnzt { get; set; }
        public string yd { get; set; }
        public string ydycms { get; set; }
        public string pf { get; set; }
        public string pfqtms { get; set; }
        public string gm { get; set; }
        public string gmqtms { get; set; }
        public string lbjjc { get; set; }
        public string lbjjcqtms { get; set; }
        public string f_tzx { get; set; }
        public string f_hxy { get; set; }
        public string f_hxyyc { get; set; }
        public string f_ly { get; set; }
        public string f_lyqt { get; set; }
        public string xz_xl { get; set; }
        public string xz_xllb { get; set; }
        public string xz_zy { get; set; }
        public string xz_zyms { get; set; }
        public string fb_yt { get; set; }
        public string fb_ytms { get; set; }
        public string fb_bk { get; set; }
        public string fb_bkms { get; set; }
        public string fb_gd { get; set; }
        public string fb_gdms { get; set; }
        public string fb_pd { get; set; }
        public string fb_pdms { get; set; }
        public string fb_ydxzy { get; set; }
        public string fb_ydxzyms { get; set; }
        public string xzszjc { get; set; }
        public string gmzz { get; set; }
        public string gmzzqt { get; set; }
        public string zbdmbd { get; set; }
        public string rx { get; set; }
        public string rxqt { get; set; }
        public string fkjc_wy { get; set; }
        public string fkjc_wyyc { get; set; }
        public string fkjc_yd { get; set; }
        public string fkjc_ydyc { get; set; }
        public string fkjc_gj { get; set; }
        public string fkjc_gjyc { get; set; }
        public string fkjc_gt { get; set; }
        public string fkjc_gtyc { get; set; }
        public string fkjc_zgfj { get; set; }
        public string fkjc_zgfjyc { get; set; }
        public string ct_qt { get; set; }
        public string xcg { get; set; }
        public string xcg_xhdb { get; set; }
        public string xcg_bxb { get; set; }
        public string xcg_xxb { get; set; }
        public string xcg_qt { get; set; }
        public string ncg { get; set; }
        public string ncg_ndb { get; set; }
        public string ncg_nt { get; set; }
        public string ncg_ntt { get; set; }
        public string ncg_nxx { get; set; }
        public string ncg_qt { get; set; }
        public string nwlbdb { get; set; }
        public string kfxt { get; set; }
        public string thxhdb { get; set; }
        public string dbqx { get; set; }
        public string ygbmky { get; set; }
        public string ggn { get; set; }
        public string ggn_xqgbzam { get; set; }
        public string ggn_xqgczam { get; set; }
        public string ggn_bdb { get; set; }
        public string ggn_zdhs { get; set; }
        public string ggn_jhdhs { get; set; }
        public string sgn { get; set; }
        public string sgn_xqjg { get; set; }
        public string sgn_xnsd { get; set; }
        public string sgn_xjnd { get; set; }
        public string sgn_xnnd { get; set; }
        public string xz { get; set; }
        public string xz_zdgcz { get; set; }
        public string xz_gysz { get; set; }
        public string xz_xqdmdzdb { get; set; }
        public string xz_xqgmdzdb { get; set; }
        public string xdt { get; set; }
        public string xdtyc { get; set; }
        public string xsx { get; set; }
        public string xsxyc { get; set; }
        public string bchao { get; set; }
        public string bchaoyc { get; set; }
        public string gjtp { get; set; }
        public string gjtpyc { get; set; }
        public string fzjc_qt { get; set; }
        public string nxgjb { get; set; }
        public string nxgjbqt { get; set; }
        public string szjb { get; set; }
        public string szjbqt { get; set; }
        public string xzjb { get; set; }
        public string xzjbqt { get; set; }
        public string xgjb { get; set; }
        public string xgjbqt { get; set; }
        public string ybjb { get; set; }
        public string ybjbqt { get; set; }
        public string sjxtjb { get; set; }
        public string sjxtjbqt { get; set; }
        public string qtxtjb { get; set; }
        public string qtxtjbqt { get; set; }
        public string jtywycbm { get; set; }
        public string yc { get; set; }
        public string jkzdbm { get; set; }
        public string wxyskzbm { get; set; }
        public string jytz { get; set; }
        public string jyjzym { get; set; }
        public string qtjy { get; set; }
        public Oldjson lnrShzlnlpgzb { get; set; }
        public Drugjson yyqk { get; set; }
        public Personjson zyqk { get; set; }
        public Familyjson jtbsqk { get; set; }
        public immunityjson fmyqk { get; set; }
    }


}
