using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.CusReg
{
    public static class PDCCInterface
    {
        #region 获取用户信息到排队
        /// <summary>
        /// 获取用户信息到排队
        /// </summary>
        /// <param name="idnum"></param>
        /// <param name="strtjcode"></param>
        public static Btpd.Model.UserInfo getuserinfo(string idnum, string strtjcode)
        {
            Btpd.BLL.Binding binddal = new Btpd.BLL.Binding(Btpd_base.PublicTool.SqlConnection);
            Btpd.BLL.NowTeam BLLNowTeam = new Btpd.BLL.NowTeam(Btpd_base.PublicTool.SqlConnection);
            Btpd.BLL.UserInfo userinfo = new Btpd.BLL.UserInfo(Btpd_base.PublicTool.SqlConnection);
            Btpd.Model.UserInfo EnteruserinfoGd = new Btpd.Model.UserInfo();
            var dxid = new StringBuilder(" Externa='");
            List<Btpd.Model.UserInfo> lsuserinfo = userinfo.GetModelList(" tjcode='" + strtjcode + "'");
            if (lsuserinfo.Count > 0)
            { 
                EnteruserinfoGd = lsuserinfo[0];
            }
            var strsql = string.Empty;
            if (idnum != string.Empty)
            {
                strsql = "select SELFBH,RFID,YYRXM,SEX,AGE,VIP,DXID,TeamOrder,Picture," +
                 " ItemSuitIDName,jcfrom from tj_requireinfo where IDCardNo='" + idnum + "' order by TJRQ desc";
            }
            if (strtjcode != string.Empty)
            {
                strsql = "select SELFBH,RFID,YYRXM,SEX,AGE,VIP,DXID,TeamOrder,Picture," +
                    " ItemSuitIDName,jcfrom from tj_requireinfo where SelfBH='" + strtjcode + "' order by TJRQ desc";
            }
            var tjcon = new SqlConnection(Btpd_base.PublicTool.SqlTj);
            tjcon.Open();
            var tjcom = new SqlCommand(strsql, tjcon);
            var tjread = tjcom.ExecuteReader();
            var coutn = 0;
            var first = 0;
            if (!tjread.HasRows)
            {
                return null;
            }
            var tjcode = string.Empty;
            while (tjread.Read())
            {
                if (first == 0)
                {
                    tjcode = tjread["SELFBH"].ToString();
                    EnteruserinfoGd.Name = tjread["YYRXM"].ToString();
                    EnteruserinfoGd.Sex = tjread["SEX"].ToString();
                    try
                    {
                        EnteruserinfoGd.Age = Convert.ToInt16(tjread["AGE"].ToString());
                    }
                    catch (Exception)
                    {
                        EnteruserinfoGd.Age = 1;
                    }

                    EnteruserinfoGd.tjCode = tjread["SELFBH"].ToString();
                    if (EnteruserinfoGd == null || EnteruserinfoGd.TeamOrder == null)
                    {
                        string strcount = BLLNowTeam.SelectSql("select (COUNT(id)+1) from UserInfo where DATEDIFF(day,getdate(),Time)=0");
                        EnteruserinfoGd.TeamOrder = Convert.ToInt16(strcount);
                    }
                    if (tjread["Picture"] != null && tjread["Picture"].ToString() != string.Empty)
                    {
                        EnteruserinfoGd.Picture = (tjread["Picture"] == Convert.DBNull ? null : (byte[])(tjread["Picture"]));
                    }
                    EnteruserinfoGd.jcfrom = tjread["jcfrom"].ToString();
                    EnteruserinfoGd.Vip = Convert.ToInt16(tjread["VIP"].ToString());
                    EnteruserinfoGd.ItemSuitIDName = tjread["ItemSuitIDName"].ToString();
                    if (tjread["DXID"].ToString().Contains("+"))
                    {
                        dxid.Append("1=1' ");
                        first++;
                        coutn++;
                        for (var i = 0; i < tjread["DXID"].ToString().Split('+').Length; i++)
                        {
                            dxid.Append("or Externa='" + tjread["DXID"].ToString().Split('+')[i] + "'");
                        }
                    }
                    else
                    {
                        dxid.Append(tjread["DXID"].ToString() + "'");
                        first++;
                        coutn++;
                    }
                }
                else
                {
                    if (tjcode == tjread["SELFBH"].ToString())
                    {
                        if (tjread["DXID"].ToString().Contains("+"))
                        {
                            dxid.Append("or Externa='" + tjread["DXID"].ToString() + "'");
                        }
                        else
                        {
                            dxid.Append("or Externa='" + tjread["DXID"].ToString() + "'");
                        }
                    }

                    coutn++;
                }
            }
            dxid.Append("or Externa='ABV10001'");
            BLLNowTeam.UpdateSQL("delete from user_item where tjcode='" + EnteruserinfoGd.tjCode + "' ");
            var binding = binddal.GetList(dxid.ToString());
            BLLNowTeam.insertuser_item(EnteruserinfoGd.tjCode, dxid.ToString(), "1001", "0");
            EnteruserinfoGd.NeedDepart = string.Empty;
            EnteruserinfoGd.Time = BLLNowTeam.GetNowTime();
            foreach (Btpd.Model.Binding item in binding)
            {
                if (!EnteruserinfoGd.NeedDepart.Contains(item.DepartmentsID))
                {
                    EnteruserinfoGd.NeedDepart += item.DepartmentsID + ",";
                }
            }
            EnteruserinfoGd.needDepart2 = string.Empty;
            EnteruserinfoGd.WdCode = string.Empty;
            EnteruserinfoGd.Remark = string.Empty;
            EnteruserinfoGd.ItemSuitIDName = string.Empty;

            string strnum = "SELECT TOP 1 TeamOrder FROM UserInfo where  DATEDIFF(day,Time,GETDATE())=0 ORDER BY Time DESC ";
            strnum = BLLNowTeam.SelectSql(strnum);
            if (strnum == "")
            {
                strnum = "1";
            }
            else
            {
                strnum = (Convert.ToInt16(strnum) + 1).ToString();
            }

            EnteruserinfoGd.TeamOrder = Convert.ToInt16(strnum);

            if (EnteruserinfoGd != null && EnteruserinfoGd.tjCode != string.Empty)
            {
                var user = userinfo.GetList("tjCode='" + EnteruserinfoGd.tjCode.ToString().Trim() + "'");
                if (user.Count == 0)
                {
                    EnteruserinfoGd.UserID = System.Guid.NewGuid().ToString();
                    userinfo.Add(EnteruserinfoGd);
                }
                else
                {
                    EnteruserinfoGd.UserID = user[0].UserID;
                    userinfo.Update(EnteruserinfoGd);
                }
            }

            tjread.Close();
            tjcon.Close();
            if (EnteruserinfoGd != null)
            {
                //通过user_item表插入公共池表
                strsql = " insert into tb_SelfCusitemReg select Tjcode" +
               ",'" + EnteruserinfoGd.UserID + "',getdate(),DepartmentId,itemid" +
               ",2,1,1,'" + EnteruserinfoGd.Sex + "','" + EnteruserinfoGd.Vip + "','" + EnteruserinfoGd.Age + "' ,0 " +
               "from user_item where tjcode='" + EnteruserinfoGd.tjCode + "' and " +
               " itemid NOT in (SELECT ItemGroupID FROM tb_SelfCusitemReg where tb_SelfCusitemReg.userid='" + EnteruserinfoGd.UserID + "' )";
                BLLNowTeam.UpdateSQL(strsql);
                strsql = "update tb_SelfCusitemReg set Checkdate=GETDATE() where tjcode='" + EnteruserinfoGd.tjCode + "'";
                BLLNowTeam.UpdateSQL(strsql);
            }
            return EnteruserinfoGd;
        }
        #endregion
    }
}
