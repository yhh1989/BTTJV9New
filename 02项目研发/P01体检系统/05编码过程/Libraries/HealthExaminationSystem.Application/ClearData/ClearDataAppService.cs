using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Sw.Hospital.HealthExaminationSystem.Application.ClearData.Dto;
using Sw.Hospital.HealthExaminationSystem.Core;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;

namespace Sw.Hospital.HealthExaminationSystem.Application.ClearData
{
    /// <summary>
    /// 清空数据
    /// </summary>
    public class ClearDataAppService : MyProjectAppServiceBase, IClearDataAppService
    {
        private readonly ISqlExecutor _sqlExecutor;
        public List<string> delTables = new List<string>();
        public string SCName = "";
        bool isOk = false;
        public string strSqlDel = "";
        /// <summary>
        /// 预约表仓储
        /// </summary>
        private readonly IRepository<TjlCustomerReg, Guid> _customerRegRepository;

        /// <summary>
        /// 体检人表仓储
        /// </summary>
        private readonly IRepository<TjlCustomer, Guid> _customerRepository;
        public ClearDataAppService(ISqlExecutor sqlExecutor,
              IRepository<TjlCustomer, Guid> customerRepository,
            IRepository<TjlCustomerReg, Guid> customerRegRepository)
        {
            _sqlExecutor = sqlExecutor;
            _customerRepository = customerRepository;
            _customerRegRepository = customerRegRepository;
        }
        /// <summary>
        /// 时间段   删除数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public DataBaseDto TimeDeleteData(ClearDataDto input)
        {
            DataBaseDto dto = new DataBaseDto();
            var result = 0;

            try
            {
                SqlParameter[] parameter = {
                         new SqlParameter("@CreationTime",input.StartTime),
                         new SqlParameter("@EndTime", input.EndTime)
                };
                string strsql = "delete from Pictures where CreationTime between @CreationTime  and @EndTime ;" +
                "delete from Pictures where CreationTime between @CreationTime  and  @EndTime;" +
                "delete from TjlCustomerItemPics where TjlCustomerItemPics.ItemBMID in (select id from TjlCustomerRegItems where CreationTime between @CreationTime and  @EndTime);" +
                "delete from TjlCustomerSummarizeBMs where CreationTime  between @CreationTime and  @EndTime;" +
                "delete from TjlCustomerSummarizes where CreationTime between @CreationTime  and  @EndTime;" +
                "delete from TjlCustomerSummaries where CreationTime between @CreationTime  and  @EndTime;" +
                "delete from TjlCustomerDepSummaries where CreationTime between @CreationTime  and  @EndTime;" +
                "delete from TjlCustomerServiceCallBacks  where CreationTime between @CreationTime  and  @EndTime;" +
                "delete from TjlCrisisSets  where CreationTime between @CreationTime  and  @EndTime ;" +
                "delete from TjlCrisisSets  where TjlCrisisSets.TjlCustomerRegItemId in (select id from TjlCustomerRegItems where CreationTime between @CreationTime and  @EndTime);" +
                "delete from TjlApplicationForms where CreationTime between @CreationTime and @EndTime;" +
                "delete from TjlCustomerRegItems  where CreationTime between @CreationTime  and @EndTime;" +
                "delete from TjlCustomerBarPrintInfoItemGroups  where CreationTime  between @CreationTime  and @EndTime; " +
                "delete from TjlCustomerBloodNums where CreationTime  between @CreationTime  and  @EndTime ;" +
                "delete from TjlCustomerItemGroups  where CreationTime between @CreationTime  and  @EndTime;" +
                "delete from TjlMPayments  where CreationTime between @CreationTime  and  @EndTime ; " +
                "delete from TjlMReceiptDetails where CreationTime between @CreationTime  and @EndTime ;" +
                "delete from TjlMReceiptInfoes where CreationTime between @CreationTime  and @EndTime ; " +
                "delete from TjlMcusPayMoneys  where CreationTime between @CreationTime  and @EndTime; " +
                "delete from TjlCustomerBarPrintInfoes  where CreationTime between @CreationTime  and @EndTime;" +
                "delete from TjlCustomerDepSummaries   where CreationTime between @CreationTime  and @EndTime;" +
                "delete from TjlCustomerDepSummaries  where CustomerRegId in (select id from TjlCustomerRegs  where CreationTime between @CreationTime  and @EndTime);" +
                "delete from TjlCustomerItemGroups  where CustomerRegBMId in(select Id from TjlCustomerRegs where CreationTime between @CreationTime  and @EndTime);" +
                "delete from TjlCustomerSummaries  where CustomerRegBMId in (select id from TjlCustomerRegs  where CreationTime between @CreationTime  and @EndTime);" +
                "delete from TjlCustomerRegs  where CreationTime between @CreationTime  and @EndTime;" +
                "delete from TjlCustomers where CreationTime  between @CreationTime  and  @EndTime;" +
                "delete from TjlCustomerBarPrintInfoes where CreationTime between @CreationTime  and @EndTime;" +
                "delete from TjlCustomerReportPrintInfoes where CreationTime between @CreationTime  and @EndTime;" +
                "delete from TjlClientTeamRegitems  where CreationTime between @CreationTime  and @EndTime;" +
                "delete from TjlClientTeamRegitems  where ClientTeamInfoId in(select Id from TjlClientTeamInfoes where CreationTime between @CreationTime  and @EndTime);" +
                "delete from TjlClientTeamInfoes where CreationTime between @CreationTime  and  @EndTime;" +
                "delete from TjlApplicationForms where ClientRegId in (select id from TjlClientRegs where CreationTime between @CreationTime  and  @EndTime);" +
                "delete from TjlClientRegs where CreationTime between @CreationTime  and @EndTime;" +
                "delete from TjlClientInfoes  where CreationTime between @CreationTime  and @EndTime; " +
                "delete from TjlCustomerRegItems where CreationTime between @CreationTime  and @EndTime;" +
                "";

                result = _sqlExecutor.Execute(strsql, parameter);
            }
            catch (Exception ex)
            {
                throw;
            }
            dto.Leixing = result;
            return dto;
        }

        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <returns></returns>
        public DataBaseDto AllDeleteData(InputClearData input)
        {
            DataBaseDto dto = new DataBaseDto();
            var result = 0;
            try 
            {
                #region 已删除
                //string strsql = "truncate table   Pictures;" +
                //    "truncate table TjlCustomerSummarizeBMs;" +
                //    "truncate table TjlCustomerSummarizes;" +
                //    "truncate table TjlCustomerSummaries;" +
                //    "truncate table TjlCustomerDepSummaries;" +
                //    "truncate table TjlCustomerServiceCallBacks;" +
                //    "delete from  TjlCrisisSets;" +
                //    "truncate table TjlApplicationForms;" +
                //    "delete from TjlCustomerRegItems;" +
                //    "truncate table TjlCustomerBarPrintInfoItemGroups;" +
                //    "truncate table TjlCustomerBloodNums;" +
                //    "delete from TjlCustomerItemGroups;" +
                //    "delete from TjlMPayments;" +
                //    "truncate table TjlMReceiptDetails;" +
                //    "delete from TjlMReceiptInfoes;" +
                //    "truncate table TjlMcusPayMoneys;" +
                //    "delete from TjlCustomerBarPrintInfoes;" +
                //    "truncate table  TjlCustomerReportPrintInfoes;" +
                //    "truncate table  TjlClientTeamRegitems;" +
                //    "  ";

                //result = _sqlExecutor.Execute(strsql); 
                #endregion
                #region 禁用索引
                string OpenSY = @"select 'alter table ['+

oSub.name +'] WITH NOCHECK ADD CONSTRAINT ['+

fk.name +'] FOREIGN KEY(['+

SubCol.name +']) REFERENCES [dbo].['+

oMain.name +'](['+

MainCol.name +']);' as 禁用约束 

from

sys.foreign_keys fk

JOIN sys.all_objects oSub

ON (fk.parent_object_id = oSub.object_id)

JOIN sys.all_objects oMain

ON (fk.referenced_object_id = oMain.object_id)

JOIN sys.foreign_key_columns fkCols

ON (fk.object_id = fkCols.constraint_object_id)

JOIN sys.columns SubCol

ON (oSub.object_id = SubCol.object_id

AND fkCols.parent_column_id = SubCol.column_id)

JOIN sys.columns MainCol

ON (oMain.object_id = MainCol.object_id AND fkCols.referenced_column_id = MainCol.column_id) 
";

                var dtOpenSY = _sqlExecutor.SqlQuery<SY>(OpenSY).ToList();

                string CloseSY = @"select 'alter table ['+oSub.name +'] drop constraint ['+fk.name +'];'  as 禁用约束 

from

sys.foreign_keys fk

JOIN sys.all_objects oSub

ON (fk.parent_object_id = oSub.object_id)

JOIN sys.all_objects oMain

ON (fk.referenced_object_id = oMain.object_id)

JOIN sys.foreign_key_columns fkCols

ON (fk.object_id = fkCols.constraint_object_id)

JOIN sys.columns SubCol

ON (oSub.object_id = SubCol.object_id

AND fkCols.parent_column_id = SubCol.column_id)

JOIN sys.columns MainCol

ON (oMain.object_id = MainCol.object_id AND fkCols.referenced_column_id = MainCol.column_id)

 
 ";


                var dtcloseSY = _sqlExecutor.SqlQuery<SY>(CloseSY).ToList();
                foreach (var sq in dtcloseSY)
                {
                    string strsql = sq.禁用约束;
                    _sqlExecutor.Execute(strsql);
                }
                #endregion
                #region 已删除
                //                string strdel = @"--体检报告相关
                //truncate table   Pictures 
                //truncate table TjlCustomerItemPics
                //truncate table TjlCustomerSummarizeBMs
                //truncate table TjlCustomerSummarizes

                //truncate table TjlCustomerDepSummaries
                //truncate table TjlCustomerRegItems
                //truncate table TjlCustomerItemGroups 
                // truncate table  TjlCustomerRegs
                // truncate table  TjlCustomers
                // --收费相关
                //truncate table TjlMPayments
                //truncate table TjlMReceiptDetails
                //truncate table TjlMReceiptInfoes
                //truncate table TjlMcusPayMoneys

                //--职业健康相关
                //truncate table  TjlOccCustomerOccDiseases
                //truncate table  TjlOccCustomerContraindications
                //truncate table  TjlOccCustomerSums
                //truncate table  TjlOccQuesCareerHistories
                //truncate table  TjlOccQuesFamilyHistories
                //truncate table  TjlOccQuesHisHazardFactors
                //truncate table  TjlOccQuesHisProtectives
                //truncate table TjlOccQuesPastHistories 
                //truncate table TjlOccQuesSymptoms
                //truncate table TjlOccQuestionnaires

                //--单位相关
                //truncate table  TjlClientTeamRegitems
                //truncate table TjlClientTeamInfoes
                //truncate table TjlClientRegs--单位预约
                //truncate table TjlClientInfoes--单位
                //truncate table TjlClientCustomItemSuits

                //--打印记录等
                //truncate table TjlCustomerBarPrintInfoes
                //truncate table TjlCustomerBarPrintInfoItemGroups
                //truncate table TjlCustomerReportPrintInfoes
                //truncate table TjlCustomerReviewFollows											
                //truncate table TjlCustomerServiceCallBacks
                //truncate table  TjlCrisisSets
                //truncate table TjlApplicationForms
                //truncate table TjlCustomerBloodNums
                //--日志表
                //truncate table [dbo].[AbpAuditLogs] 
                //truncate table [dbo].[TjlOperationLogs]";

                // _sqlExecutor.Execute(strdel); 
                #endregion
                //循环删除所有tjl开头的业务表
                if (input.isTjl.HasValue && input.isTjl.Value == true)
                {
                    #region 循环删除业务数据（Tjl开头数据）
                    string allTable = @"SELECT  'truncate table ['+TABLE_NAME +'] ;' as 禁用约束  FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME like 'tjl%'";
                    var dtallTable = _sqlExecutor.SqlQuery<SY>(allTable).ToList();
                    foreach (var sq in dtallTable)
                    {
                        string strsql = sq.禁用约束;
                        _sqlExecutor.Execute(strsql);
                    }
                    #endregion
                    string piclogsql = @"truncate table Pictures ;";
                    _sqlExecutor.Execute(piclogsql);
                }
                //清除用户
                if (input.isUser.HasValue && input.isUser.Value == true)
                {
                    string userlogsql = @"
truncate table  AbpUserLoginAttempts;
delete AbpUserRoles  where UserId not in (select Id from AbpUsers  where UserName !='admin');
delete  AbpUsers  where UserName !='admin';
truncate table  AbpUserAccounts;";
                    _sqlExecutor.Execute(userlogsql);
                }
                //清除接口
                if (input.isInterfase.HasValue && input.isInterfase.Value == true)
                {
                    string intlogsql = @"truncate table  TdbInterfaceEmployeeComparisons;
truncate table  TdbInterfaceItemComparisons; 
truncate table  TdbInterfaceItemGroupComparisons; ";
                    _sqlExecutor.Execute(intlogsql);
                }               
                //清除套餐
                if (input.isSuit.HasValue && input.isSuit.Value == true)
                {
                    string intlogsql = @"truncate table  TbmItemSuits; 
truncate table  TbmItemSuitItemGroupContrasts;";
                    _sqlExecutor.Execute(intlogsql);
                }
                //清除系统日志
                if (input.isAbpLog.HasValue && input.isAbpLog.Value == true)
                {
                    string abplogsql = @"truncate table [dbo].[AbpAuditLogs];";
                    abplogsql += @"truncate table [dbo].[AbpUserLoginAttempts];";
                    _sqlExecutor.Execute(abplogsql);
                }

                //清除操作日志
                if (input.isLog.HasValue && input.isLog.Value == true)
                {
                    string logsql = @"truncate table [dbo].[TjlOperationLogs];";
                    _sqlExecutor.Execute(logsql);
                }
                //清除建议日志
                if (input.isAdVice.HasValue && input.isAdVice.Value == true)
                {
                    string logsql = @"truncate table [dbo].[TbmSummarizeAdvices];";
                    _sqlExecutor.Execute(logsql);
                }
                //清除条码
                if (input.isBar.HasValue && input.isBar.Value == true)
                {
                    string logsql = @"truncate table [dbo].[TjlCustomerBarPrintInfoItemGroups];
truncate table [dbo].[TjlCustomerBarPrintInfoes];
truncate table [dbo].[TbmBarSettingsTbmGroupConsumables];
truncate table [dbo].[TbmBaritems];
truncate table [dbo].[TbmBarSettings];";
                    _sqlExecutor.Execute(logsql);

                }
                #region 开启约束
                foreach (var sq in dtOpenSY)
                {
                    string strsql = sq.禁用约束;
                    _sqlExecutor.Execute(strsql);
                } 
                #endregion
                // delDataAll();
                result = 1;
            }
            catch (Exception ex)
            {

                throw;
            }
            dto.Leixing = result;
            return dto;
        }
      

        public DataBaseDto delTableByTiem(ClearDataDto input)
        {

            //using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            //{
            //    var st = DateTime.Parse(input.StartTime);
            //    var ed = DateTime.Parse(input.EndTime);
            //    var cusIDlist = _customerRegRepository.GetAll().Where(p => p.LoginDate >= st && p.LoginDate < ed).Select(r => r.Id).ToList();
            //    foreach (var cusid in cusIDlist)
            //    {
            //        var cus = _customerRegRepository.Get(cusid);
            //        _customerRegRepository.HardDelete(cus);
            //        CurrentUnitOfWork.SaveChanges();
            //    }
            //}
            //return new DataBaseDto();
            DataBaseDto dto = new DataBaseDto();
            var result = 0;
            //删除个人预约表
            delTables.Clear();
            strSqlDel = string.Format(@"select Id from TjlCustomerRegs where  CreationTime between '{0}' and '{1}'",
input.StartTime, input.EndTime);
            SCName = "TjlCustomerRegs";
            delCusRegByTiem( input);
          
            isOk = false;
            delTables.Clear();
            strSqlDel = string.Format(@"select Id  from TjlClientRegs where id not in (select ClientRegId  from TjlCustomerRegs)
  and CreationTime between '{0}' and '{1}'", input.StartTime, input.EndTime);
            SCName = "TjlClientRegs";
            delCusRegByTiem(input);

            //删除没有预约的人员档案
            string delcus = string.Format(@"delete  from TjlCustomers where id not in 
(select CustomerId  from TjlCustomerRegs)   and CreationTime between '{0}' and '{1}'",
input.StartTime, input.EndTime);
            var resultcus = _sqlExecutor.Execute(delcus);
            //删除单位信息
            string delclient = string.Format(@"delete   from TjlClientInfoes where id not in (select ClientInfoId  from TjlClientRegs)
  and CreationTime between '{0}' and '{1}'", input.StartTime, input.EndTime);
            var resultclient = _sqlExecutor.Execute(delclient);
            //删除日志

            string dellog = "  truncate table [dbo].[AbpAuditLogs]";
            var resultlog = _sqlExecutor.Execute(dellog);
            result = 1;
            dto.Leixing = result;
            return dto;

        }        
        /// <summary>
        /// 删除个人业务相关表
        /// </summary>
        /// <param name="input"></param>
        public void delCusRegByTiem(ClearDataDto input)
        {
            if (isOk)
            {
                return;
            }            
            DelTable table = new DelTable();
            table.Ids = strSqlDel;
            table.PkId = "Id";
            table.TableName = SCName;
            table.PKSY = "主索引";
            table.EndTime = DateTime.Parse(input.EndTime);
            table.starTime = DateTime.Parse(input.StartTime);
           // table.PathName = "";
            DeleteDataByTable(table);
        }       
      
        private void DeleteDataByTable(DelTable table)
        {

            try
            {
                if (isOk)
                {
                    return;
                }
                string sql1 = string.Format(@"select
a.name as 约束名,
object_name(b.parent_object_id) as 外键表,
d.name as 外键列,
object_name(b.referenced_object_id) as 主健表,
c.name as 主键列
from sys.foreign_keys A
inner join sys.foreign_key_columns B on A.object_id=b.constraint_object_id
inner join sys.columns C on B.parent_object_id=C.object_id and B.parent_column_id=C.column_id 
inner join sys.columns D on B.referenced_object_id=d.object_id and B.referenced_column_id=D.column_id 
where object_name(B.referenced_object_id)='{0}' and object_name(b.parent_object_id)!='TjlCustomerRegs' ", table.TableName);               

                List<IDS> dtrws1 = _sqlExecutor.SqlQuery<IDS>
                    (sql1).ToList();
                List<IDS> dtrws = new List<IDS>();
                if (dtrws1.Count == 0)
                {
                    dtrws = dtrws1;
                }
                else
                {
                     dtrws = dtrws1.Where(o => !delTables.Contains( o.约束名)).ToList();
                }
                if (dtrws.Count > 0)
                {
                    foreach (var rw in dtrws)
                    {
                        try
                        {
                            if (!delTables.Contains(rw.约束名))
                            {

                                DelTable tablenew = new DelTable();
                                tablenew.TableName = rw.外键表;
                                tablenew.PkId = rw.主键列;
                                tablenew.PKSY = rw.约束名;
                                string ids = string.Format("select {3} from {0} where {1} in({2})",
                                    rw.外键表, rw.主键列, table.Ids, rw.外键列);
                                tablenew.Ids = ids;
                                tablenew.EndTime = table.EndTime;
                                tablenew.starTime = table.starTime;
                               // tablenew.PathName = table.PathName + rw.外键表;
                                DeleteDataByTable(tablenew);
                            }
                        }
                        catch (Exception ex)
                        {
                            string ss = ex.ToString();
                            throw new FieldVerifyException( ex.InnerException == null ? ex.Message : ex.InnerException.Message,"");
                        }

                    }
                }
                else
                {
                    try
                    {

                        if (!delTables.Contains( table.PKSY))
                        {
                            var sqldel = "delete " + table.Ids.Substring(table.Ids.IndexOf("from"), table.Ids.Length - table.Ids.IndexOf("from"));
                            //string sqldel = string.Format(@"delete {0} where {1} in({2})", table.TableName, table.PkId, table.Ids);
                            var result = _sqlExecutor.Execute(sqldel);
                            delTables.Add( table.PKSY);
                            if (table.TableName != SCName)
                            {
                                ClearDataDto input = new ClearDataDto();
                                input.EndTime = table.EndTime.ToString();
                                input.StartTime = table.starTime.ToString();
                                delCusRegByTiem(input);
                            }
                            else
                            {
                                delTables.Add("结束");
                                isOk = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string ss = ex.ToString();
                        throw new FieldVerifyException(ex.InnerException==null? ex.Message:ex.InnerException.Message,"");
                    }
                }
            }
            catch (Exception ex)
            {
                string ss = ex.ToString();
                //throw new UserFriendlyException("CannotDeleteAStaticRole");
                throw new FieldVerifyException( ex.InnerException == null ? ex.Message : ex.InnerException.Message,"");
             
            }

        }
      
        /// <summary>
        /// 删除数据表
        /// </summary>
        /// <param name="input"></param>
        public void delDataAll()
        {
            isOk = false;
            //删除个人数据
            delTables.Clear();
            SCName = "TjlCustomerRegs";
            DeleteDataByTableAll("TjlCustomerRegs");
            isOk = false;
            SCName = "TjlClientRegs";
            DeleteDataByTableAll("TjlClientRegs");

            string delcus= "  delete TjlCustomers";
            var resultcus = _sqlExecutor.Execute(delcus);

            string delclient = "  delete TjlClientInfoes";
            var resultclient = _sqlExecutor.Execute(delclient);
            //删除日志

            string dellog = "  truncate table [dbo].[AbpAuditLogs]";
            var resultlog = _sqlExecutor.Execute(dellog);
        }
       

        private void DeleteDataByTableAll(string tableName)
        {

            try
            {
                if (isOk)
                {
                    return;
                }
                string sqlswhere = "";
                if(SCName == "TjlClientRegs")
                {
                    sqlswhere = "  and object_name(b.parent_object_id)!='TjlCustomerRegs'  ";
                }
                string sql1 = string.Format(@"select
a.name as 约束名,
object_name(b.parent_object_id) as 外键表,
d.name as 外键列,
object_name(b.referenced_object_id) as 主健表,
c.name as 主键列
from sys.foreign_keys A
inner join sys.foreign_key_columns B on A.object_id=b.constraint_object_id
inner join sys.columns C on B.parent_object_id=C.object_id and B.parent_column_id=C.column_id 
inner join sys.columns D on B.referenced_object_id=d.object_id and B.referenced_column_id=D.column_id 
where object_name(B.referenced_object_id)='{0}' {1}", tableName, sqlswhere);               
                List<IDS> dtrws1 = new List<IDS>();
                try
                {
                    dtrws1 = _sqlExecutor.SqlQuery<IDS>
                      (sql1)?.ToList();
                }
                catch (Exception ex)
                {
                    string ss=ex.ToString();
                }
                var dtrws = dtrws1.Where(o => !delTables.Contains(o.外键表)).ToList();
                if (dtrws.Count > 0)
                {
                    foreach (var rw in dtrws)
                    {
                        if (!delTables.Contains(rw.外键表))
                        {
                            DeleteDataByTableAll(rw.外键表);
                        }
                        
                    }
                }
                else
                {
                    if (!delTables.Contains(tableName))
                    {
                        //string sqldel = string.Format(@" truncate table  {0} ", tableName);
                        //var result = _sqlExecutor.Execute(sqldel);
                        string sqldel1 = string.Format(@" delete  {0} ", tableName);
                        var result1 = _sqlExecutor.Execute(sqldel1);
                        delTables.Add(tableName);
                        if (!delTables.Contains(SCName))
                        {

                            DeleteDataByTableAll(SCName);
                        }
                        else
                        {
                            isOk = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
                throw;
            }

        }

    }
    public class IDS
    {
        [StringLength(640)]
        public virtual string 约束名 { get; set; }
        [StringLength(640)]
        public virtual string 外键表 { get; set; }
        [StringLength(640)]
        public virtual string 外键列 { get; set; }
        [StringLength(640)]
        public virtual string 主健表 { get; set; }
        [StringLength(640)]
        public virtual string 主键列 { get; set; }

    }
    public class DelTable
    {
        public virtual string TableName { get; set; }
        //public virtual string PathName { get; set; }
        public virtual string PKSY { get; set; }
        public virtual string PkId { get; set; }
        public virtual string Ids { get; set; }
        public virtual DateTime? starTime { get; set; }
        public virtual DateTime? EndTime { get; set; }
    }
    public class SY
    {
        public virtual string 禁用约束 { get; set; }
      
    }
}
