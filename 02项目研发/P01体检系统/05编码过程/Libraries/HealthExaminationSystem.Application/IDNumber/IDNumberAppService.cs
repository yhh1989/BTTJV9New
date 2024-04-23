using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.IDNumber.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

namespace Sw.Hospital.HealthExaminationSystem.Application.IDNumber
{
    /// <summary>
    /// 编码生成应用服务
    /// </summary>
    [AbpAuthorize]
    public class IDNumberAppService : MyProjectAppServiceBase, IIDNumberAppService
    {
        private readonly IRepository<TdbIdNumber, Guid> _idNumberRepository;
        /// <summary>
        /// 用户表仓储
        /// </summary>
        private readonly IRepository<User, long> _userRepository;
        /// <summary>
        /// 字典
        /// </summary>
        private readonly IRepository<TbmBasicDictionary, Guid> _basicDictionary;

        /// <summary>
        /// 编码生成应用服务
        /// </summary>
        /// <param name="idNumberRepository"></param>
        public IDNumberAppService(IRepository<TdbIdNumber, Guid> idNumberRepository,
            IRepository<User, long> userRepository,
            IRepository<TbmBasicDictionary, Guid> basicDictionary)
        {
            _idNumberRepository = idNumberRepository;
            _basicDictionary = basicDictionary;
            _userRepository = userRepository;

        }

        /// <summary>
        /// 创建科室编码
        /// </summary>
        public string CreateDepartmentBM()
        {
            var code = CreateCurrencyBM(1);
            return code;
        }

        /// <summary>
        /// 创建单位编码
        /// </summary>
        public string CreateClientBM()
        {
            var code = CreateCurrencyBM(2);
            return code;
        }

        /// <summary>
        /// 创建组合编码
        /// </summary>
        public string CreateItemGroupBM()
        {
            var code = CreateCurrencyBM(3);
            return code;
        }

        /// <summary>
        /// 创建项目编码
        /// </summary>
        public string CreateItemBM()
        {
            var code = CreateCurrencyBM(41);
            return code;
        }

        /// <summary>
        /// 创建用户编码
        /// </summary>
        public string CreateEmployeeBM()
        {
            var code = CreateCurrencyBM(5);
            return code;
        }

        /// <summary>
        /// 创建套餐编码
        /// </summary>
        public string CreateItemSuitBM()
        {
            var code = CreateCurrencyBM(6);
            return code;
        }

        /// <summary>
        /// 创建单位预约编码
        /// </summary>
        public string CreateClientRegBM()
        {
            var code = CreateCurrencyBM(7);
            return code;
        }

        /// <summary>
        /// 创建体检人编码
        /// </summary>
        public string CreateCustomerBM()
        {
            var code = CreateCurrencyBM(8);
            return code;
        }

        /// <summary>
        /// 创建体检人预约编码
        /// </summary>
        public string CreateCustomerRegBM()
        {
            var code = CreateCurrencyBM(9);
            return code;
        }

        /// <summary>
        /// 创建体检号编码
        /// </summary>
        public string CreateArchivesNumBM()
        {
            var IdNumber = _idNumberRepository.FirstOrDefault(p => p.IDType == "10");
            var Dateprefix = IdNumber.Dateprefix.Split('-');
            if (IdNumber.Dateprefix.Contains("+"))
            {
                Dateprefix= IdNumber.Dateprefix.Split('+');
            }
          
            if (IdNumber.IDTime.Value.ToShortDateString() != DateTime.Now.ToShortDateString()&&!string.IsNullOrEmpty(IdNumber.Dateprefix))
                IdNumber.IDValue = 0;
            IdNumber.IDValue = IdNumber.IDValue + 1;
            IdNumber.IDTime = DateTime.Now;
            // _idNumberRepository.Update(IdNumber);
            try
            {
                _idNumberRepository.Update(IdNumber);
                CurrentUnitOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return CreateArchivesNumBM();
            }
            var strprefix = formatDate(IdNumber.prefix, Dateprefix[0]);
            if (Dateprefix.Length > 1)
            {
                if (IdNumber.Dateprefix.Contains("+"))
                {
                    strprefix  = IdNumber.IDValue.ToString().PadLeft(Convert.ToInt16(Dateprefix[1]), '0') + strprefix;

                }
                else
                {
                    strprefix += IdNumber.IDValue.ToString().PadLeft(Convert.ToInt16(Dateprefix[1]), '0');
                }
            }
            else
                strprefix += IdNumber.IDValue.ToString();
            var userBM = _userRepository.Get(AbpSession.UserId.Value);
            if (userBM != null && userBM.HospitalArea.HasValue && userBM.HospitalArea != 999)
            {
                var qz = _basicDictionary.FirstOrDefault(p=>p.Type== "HospitalArea"
                && p.Value== userBM.HospitalArea);
                if (qz != null && !string.IsNullOrEmpty(qz.Code))
                {
                    strprefix = qz.Code + strprefix;
                }
            }
            return strprefix;
        }

        /// <summary>
        /// 创建条码编码
        /// </summary>
        public string CreateBarBM()
        {
            var IdNumber = _idNumberRepository.FirstOrDefault(p => p.IDType == "11");
            var Dateprefix = IdNumber.Dateprefix.Split('-');
            if (IdNumber.IDTime.Value.ToShortDateString() != DateTime.Now.ToShortDateString() && !string.IsNullOrEmpty(IdNumber.Dateprefix))
                IdNumber.IDValue = 0;
            IdNumber.IDValue = IdNumber.IDValue + 1;
            IdNumber.IDTime = DateTime.Now;
            try
            {
                _idNumberRepository.Update(IdNumber);
                CurrentUnitOfWork.SaveChanges();
            }
            catch(DbUpdateConcurrencyException)
            {
                return CreateBarBM();
            }
            var strprefix = formatDate(IdNumber.prefix, Dateprefix[0]);
            if (Dateprefix.Length > 1)
                strprefix += IdNumber.IDValue.ToString().PadLeft(Convert.ToInt16(Dateprefix[1]), '0');
            else
                strprefix += IdNumber.IDValue.ToString();

            return strprefix;
        }
        /// <summary>
        /// 创建条码编码
        /// </summary>
        public string CreateCusBarBM(string CusRegBM)
        {
            var IdNumber = _idNumberRepository.FirstOrDefault(p => p.IDType == "11");
            var per = IdNumber.prefix;


            return "";
        }
        /// <summary>
        /// 创建分组编码
        /// </summary>
        /// <returns></returns>
        public string CreateTeamBM()
        {
            var code = CreateCurrencyBM(12);
            return code;
        }
        /// <summary>
        /// 创建建议编码
        /// </summary>
        /// <returns></returns>
        public string CreateAdviceBM()
        {
            // var code = CreateCurrencyBM(13);
            string IDType = "13";
            var IdNumber = _idNumberRepository.FirstOrDefault(p => p.IDType == IDType.ToString());
            string strprefix = "";
            if (IdNumber == null)
            {
                IdNumber = new TdbIdNumber();
                IdNumber.IDTime = DateTime.Now;
                IdNumber.Id = Guid.NewGuid();
                IdNumber.IDValue = 1;
                IdNumber.prefix = "";
                IdNumber.IDName = "adviceBM";
                IdNumber.IDType = "13";
                IdNumber.Dateprefix = "";
                IdNumber = _idNumberRepository.Insert(IdNumber);
                 strprefix = formatDate(IdNumber.prefix, IdNumber.Dateprefix);
                strprefix += IdNumber.IDValue.ToString();
            }
            else
            {
                IdNumber.IDTime = DateTime.Now;
                IdNumber.IDValue = IdNumber.IDValue + 1;
                _idNumberRepository.Update(IdNumber);
                 strprefix = formatDate(IdNumber.prefix, IdNumber.Dateprefix);
                strprefix += IdNumber.IDValue.ToString();
            }
            return strprefix;
          
        }

        /// <summary>
        /// 创建申请单编码
        /// </summary>
        /// <returns></returns>
        public string CreateApplicationBM()
        {
            // var code = CreateCurrencyBM(13);
            string IDType = "14";
            var IdNumber = _idNumberRepository.FirstOrDefault(p => p.IDType == IDType.ToString());
            string strprefix = "";
            if (IdNumber == null)
            {
                IdNumber = new TdbIdNumber();
                IdNumber.IDTime = DateTime.Now;
                IdNumber.Id = Guid.NewGuid();
                IdNumber.IDValue = 100001;
                IdNumber.prefix = "";
                IdNumber.IDName = "ApplicatioBM";
                IdNumber.IDType = "14";
                IdNumber.Dateprefix = "";
                IdNumber = _idNumberRepository.Insert(IdNumber);
                strprefix = formatDate(IdNumber.prefix, IdNumber.Dateprefix);
                strprefix += IdNumber.IDValue.ToString();
            }
            else
            {
                IdNumber.IDTime = DateTime.Now;
                IdNumber.IDValue = IdNumber.IDValue + 1;
                //_idNumberRepository.Update(IdNumber);
                try
                {
                    _idNumberRepository.Update(IdNumber);
                    CurrentUnitOfWork.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return CreateApplicationBM();
                }
                strprefix = formatDate(IdNumber.prefix, IdNumber.Dateprefix);
                strprefix += IdNumber.IDValue.ToString();
            }
            return strprefix;

        }

        /// <summary>
        /// 创建健康证编码
        /// </summary>
        /// <returns></returns>
        public string CreateJKZBM()
        {
            // var code = CreateCurrencyBM(13);
            string IDType = "15";
            var IdNumber = _idNumberRepository.FirstOrDefault(p => p.IDType == IDType.ToString());
            string strprefix = "";
            if (IdNumber == null)
            {
                IdNumber = new TdbIdNumber();
                IdNumber.IDTime = DateTime.Now;
                IdNumber.Id = Guid.NewGuid();
                IdNumber.IDValue =0;
                IdNumber.prefix = "";
                IdNumber.IDName = "JKZBM";
                IdNumber.IDType = "15";
                IdNumber.Dateprefix = "";
                IdNumber = _idNumberRepository.Insert(IdNumber);
                strprefix = formatDate(IdNumber.prefix, IdNumber.Dateprefix);
                strprefix += IdNumber.IDValue.ToString();
            }
            else
            {
                var Dateprefix = IdNumber.Dateprefix.Split('-');

                IdNumber.IDTime = DateTime.Now;
                IdNumber.IDValue = IdNumber.IDValue + 1;
                //_idNumberRepository.Update(IdNumber);
                try
                {
                    _idNumberRepository.Update(IdNumber);
                    CurrentUnitOfWork.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return CreateJKZBM();
                }
                strprefix = formatDate(IdNumber.prefix, Dateprefix[0]);
                if (Dateprefix.Length > 1)
                    strprefix += IdNumber.IDValue.ToString().PadLeft(Convert.ToInt16(Dateprefix[1]), '0');
                else
                    strprefix += IdNumber.IDValue.ToString();
               // strprefix += IdNumber.IDValue.ToString();
            }
            return strprefix;

        }

        /// <summary>
        /// 创建合格证编码
        /// </summary>
        /// <returns></returns>
        public string CreatHGZBM()
        {
            // var code = CreateCurrencyBM(13);
            string IDType = "19";
            var IdNumber = _idNumberRepository.FirstOrDefault(p => p.IDType == IDType.ToString());
            string strprefix = "";
            if (IdNumber == null)
            {
                IdNumber = new TdbIdNumber();
                IdNumber.IDTime = DateTime.Now;
                IdNumber.Id = Guid.NewGuid();
                IdNumber.IDValue = 1;
                IdNumber.prefix = "0";
                IdNumber.IDName = "HGZBM";
                IdNumber.IDType = "19";
                IdNumber.Dateprefix = "";
                IdNumber = _idNumberRepository.Insert(IdNumber);
                //strprefix = formatDate(IdNumber.prefix, IdNumber.Dateprefix);
                //strprefix += IdNumber.IDValue.ToString();
                var Dateprefix = IdNumber.Dateprefix.Split('-');
                if (Dateprefix.Length > 1)
                    strprefix += IdNumber.IDValue.ToString().PadLeft(Convert.ToInt16(Dateprefix[1]), '0');
                else
                    strprefix += IdNumber.IDValue.ToString();
            }
            else
            {
                var Dateprefix = IdNumber.Dateprefix.Split('-');

                IdNumber.IDTime = DateTime.Now;
                IdNumber.IDValue = IdNumber.IDValue + 1;
                // _idNumberRepository.Update(IdNumber);
                try
                {
                    _idNumberRepository.Update(IdNumber);
                    CurrentUnitOfWork.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return CreatHGZBM();
                }
                strprefix = formatDate(IdNumber.prefix, Dateprefix[0]);
                if (Dateprefix.Length > 1)
                    strprefix += IdNumber.IDValue.ToString().PadLeft(Convert.ToInt16(Dateprefix[1]), '0');
                else
                    strprefix += IdNumber.IDValue.ToString();
                // strprefix += IdNumber.IDValue.ToString();
            }
            return strprefix;

        }

        /// <summary>
        /// 创建登记序号 
        /// </summary>
        /// <returns></returns>
        public int CreateRegNum()
        {
            // var code = CreateCurrencyBM(13);
            string IDType = "18";
            var IdNumber = _idNumberRepository.FirstOrDefault(p => p.IDType == IDType.ToString());
            int strprefix = 0;
            if (IdNumber == null)
            {
                IdNumber = new TdbIdNumber();
                IdNumber.IDTime = DateTime.Now;
                IdNumber.Id = Guid.NewGuid();
                IdNumber.IDValue = 1;
                IdNumber.prefix = "";
                IdNumber.IDName = "RegNum";
                IdNumber.IDType = "18";
                IdNumber.Dateprefix = "";
                IdNumber = _idNumberRepository.Insert(IdNumber);                
                strprefix = IdNumber.IDValue;
            }
            else
            {
                //var d1 = IdNumber.IDTime.Value.ToString("yyyyMMdd");
                //var d2= System.DateTime.Now.ToString("yyyyMMdd");
                //当天累加
                if (IdNumber.IDTime.HasValue && IdNumber.IDTime.Value.ToString("yyyyMMdd")
                    == System.DateTime.Now.ToString("yyyyMMdd"))
                {
                    IdNumber.IDTime = DateTime.Now;
                    IdNumber.IDValue = IdNumber.IDValue + 1;
                   // _idNumberRepository.Update(IdNumber);
                }//其他日期重新生成
                else
                {
                    IdNumber.IDTime = DateTime.Now;
                    IdNumber.IDValue = 1;
                    //_idNumberRepository.Update(IdNumber);

                }

                try
                {
                    _idNumberRepository.Update(IdNumber);
                    CurrentUnitOfWork.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return CreateRegNum();
                }

                strprefix = IdNumber.IDValue;
            }
            return strprefix;

        }

        /// <summary>
        /// 创建单位审批编码
        /// </summary>
        /// <returns></returns>
        public string CreateClientZKBM()
        {
            // var code = CreateCurrencyBM(13);
            string IDType = "16";
            var IdNumber = _idNumberRepository.FirstOrDefault(p => p.IDType == IDType.ToString());
            string strprefix = "";
            if (IdNumber == null)
            {
                IdNumber = new TdbIdNumber();
                IdNumber.IDTime = DateTime.Now;
                IdNumber.Id = Guid.NewGuid();
                IdNumber.IDValue = 1;
                IdNumber.prefix = "";
                IdNumber.IDName = "ClientZKBM";
                IdNumber.IDType = "16";
                IdNumber.Dateprefix = "yyMMdd-2";
                IdNumber = _idNumberRepository.Insert(IdNumber);
                var Dateprefix = IdNumber.Dateprefix.Split('-');
                strprefix = formatDate(IdNumber.prefix, Dateprefix[0]);
                if (Dateprefix.Length > 1)
                    strprefix += IdNumber.IDValue.ToString().PadLeft(Convert.ToInt16(Dateprefix[1]), '0');
                else
                    strprefix += IdNumber.IDValue.ToString();
            }
            else
            {
                var Dateprefix = IdNumber.Dateprefix.Split('-');
                IdNumber.IDTime = DateTime.Now;
                IdNumber.IDValue = IdNumber.IDValue + 1;
                //_idNumberRepository.Update(IdNumber);
                try
                {
                    _idNumberRepository.Update(IdNumber);
                    CurrentUnitOfWork.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return CreateClientZKBM();
                }
                strprefix = formatDate(IdNumber.prefix, Dateprefix[0]);               
                if (Dateprefix.Length > 1)
                    strprefix += IdNumber.IDValue.ToString().PadLeft(Convert.ToInt16(Dateprefix[1]), '0');
                else
                    strprefix += IdNumber.IDValue.ToString();
            }
            return strprefix;

        }
        public List<IDNumberDto> GetAllList()
        {
            var result = _idNumberRepository.GetAllList();
            var orderResult = result.OrderBy(r => Convert.ToInt32(r.IDType)).ToList();
            return orderResult.MapTo<List<IDNumberDto>>();
        }

        public void Create(CreateIdNumberDto input)
        {
            var entity = _idNumberRepository.FirstOrDefault(r => r.IDName == input.IDName);
            if (entity == null)
            {
                entity = input.MapTo<TdbIdNumber>();
                entity.IDTime = DateTime.Now;
                entity.Id = Guid.NewGuid();
                _idNumberRepository.Insert(entity);
            }
            else
            {
                input.MapTo(entity);
                _idNumberRepository.Update(entity);
            }
        }

        public IDNumberDto GetByName(NameDto input)
        {
            var entity = _idNumberRepository.FirstOrDefault(r => r.IDName == input.IDName);
            if (entity != null)
            {
                return entity.MapTo<IDNumberDto>();
            }

            return null;
        }

        /// <summary>
        /// 获取当前类型编码，并更新
        /// </summary>
        /// <param name="IDType">类别id</param>
        /// <returns></returns>
        public string CreateCurrencyBM(int IDType)
        {
            var IdNumber = _idNumberRepository.FirstOrDefault(p => p.IDType == IDType.ToString());
           

            if (IDType == 11&&IdNumber.Dateprefix== "yyMMdd")
            {
                if (IdNumber.IDTime.Value.ToShortDateString() != DateTime.Now.ToShortDateString())
                {

                }
            }
            IdNumber.IDTime = DateTime.Now;
            IdNumber.IDValue = IdNumber.IDValue + 1;
            //_idNumberRepository.Update(IdNumber);
            try
            {
                _idNumberRepository.Update(IdNumber);
                CurrentUnitOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return CreateCurrencyBM(IDType);
            }
            var strprefix = formatDate(IdNumber.prefix, IdNumber.Dateprefix);
            strprefix += IdNumber.IDValue.ToString();
            return strprefix;
        }

        private string formatDate(string prefix, string Dateprefix)
        {
            var strprefix = "";
            if (!string.IsNullOrEmpty(prefix.Trim()))
                strprefix = prefix;
            if (!string.IsNullOrEmpty(Dateprefix.Trim()))
            {
                strprefix += Dateprefix;
                strprefix = strprefix.Replace("yyyy", DateTime.Now.ToString("yyyy"));
                strprefix = strprefix.Replace("yy", DateTime.Now.ToString("yy"));
                strprefix = strprefix.Replace("MM", DateTime.Now.ToString("MM"));
                strprefix = strprefix.Replace("dd", DateTime.Now.ToString("dd"));
                strprefix = strprefix.Replace("HH", DateTime.Now.ToString("HH"));
                strprefix = strprefix.Replace("mm", DateTime.Now.ToString("mm"));
                strprefix = strprefix.Replace("ss", DateTime.Now.ToString("ss"));
            }

            return strprefix;
        }

        
    }
}