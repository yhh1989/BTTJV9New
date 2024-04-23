using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    //用户查询接口返回值
   public class getAuthKeyReturnDto
    {
        //0成功  1失败
        public string Code { get; set; }

        public getAuthKeyData data { get; set; }

        public string msg { get; set; }
        ////是否成功true false
        //public string success { get; set; }

        ////异常信息
        //public string message { get; set; }
    }
    
    public class getAuthKeyData
    {
        public string loginId { get; set; }

        public string token { get; set; }

    }

    //个档查询接口返回值
    public  class GetkdaGrJbxxIReturnData
    {
        public AllData data { get; set; }
        public string msg { get; set; }
        //是否成功true false
        public string success { get; set; }
        //异常信息
        public string message { get; set; }
    }
    public class AllData
    {
      
        public string jlbh { get; set; }
        public string jbxxXm { get; set; }
        public string grbh { get; set; }
        public string jbxxXb { get; set; }
        public string csrq { get; set; }
        public string jbxxSfzh { get; set; }
        public string jbxxGzdw { get; set; }
        public string jbxxBrdh { get; set; }
        public string jbxxLxrxm { get; set; }
        public string jbxxLxrdh { get; set; }
        public string jbxxCzlx { get; set; }
        public string jbxxHjxxdz { get; set; }
        public string jbxxJzdzxx { get; set; }
        public string jbxxMz { get; set; }
        public string jbxxXx { get; set; }
        public string jbxxRhyx { get; set; }
        public string jbxxWhcd { get; set; }
        public string jbxxZy { get; set; }
        public string jbxxHyzk { get; set; }
        public string jbxxYlfyzffs { get; set; }
        public string jbxxYlfyzffsQt { get; set; }
        public string jbxxYwgms { get; set; }
        public string jbxxYwgmsMs { get; set; }
        public string jbxxBls { get; set; }
        public string jbxxBlsMs { get; set; }
        public string jbsYwjbs { get; set; }
        public string jwsJbss { get; set; }
        public string sssYwsss { get; set; }
        public string jwsSsss { get; set; }
        public string wssYwws { get; set; }
        public string jwsWsss { get; set; }
        public string sxsYwsxs { get; set; }
        public string jwsSxss { get; set; }
        public string jzsYwjb { get; set; }
        public string jwsJzss { get; set; }
        public string ycbsYwycb { get; set; }
        public string jwsYcbss { get; set; }
        public string cjqkYwcj { get; set; }
        public string cjqkCjqk { get; set; }
        public string shhjPfss { get; set; }
        public string shhjRllx { get; set; }
        public string shhjYs { get; set; }
        public string shhjCs { get; set; }
        public string shhjQcl { get; set; }
        public string gxyZb { get; set; }
        public string tnbZb { get; set; }
        public string zlbZb { get; set; }
        public string nzzZb { get; set; }
        public string gxbZb { get; set; }
        public string lnr { get; set; }
        public string cjr { get; set; }
        public string jsb { get; set; }
        public string jhbzt { get; set; }
        public string pkrkzt { get; set; }
        public string jbxxSfqy { get; set; }
        public string qysj { get; set; }
        public string isxq { get; set; }
        public string jyqysxrq { get; set; }
        public string jyqydqrq { get; set; }
        public string jbxxJdjg { get; set; }
        public string jdrq { get; set; }
        public string xgrq { get; set; }
        public string jdys { get; set; }
        public string zrys { get; set; }
        public string homeProvince { get; set; }
        public string homeCity { get; set; }
        public string homeDistrict { get; set; }
        public string homeTownship { get; set; }
        public string homeVillage { get; set; }
        public string wgdm { get; set; }
    }

    public class updateJkdaTjBgReturn
    {
        public string code { get; set; }
        public Uptdata data { get; set; }
        public string msg { get; set; }
        //是否成功true false
        public string success { get; set; }

        //异常信息
        public string message { get; set; }
    }
    public class Uptdata
    {
        public string tjId { get; set; }
    }

    public class AddJkdaTjBgReturn
    {
        public string code { get; set; }
        public Adds data { get; set; }
        public string msg { get; set; }
        //是否成功true false
        public string success { get; set; }
        //异常信息
        public string message { get; set; }
    }
    public class Adds
    {
        public string tjId { get; set; }
    }


}
