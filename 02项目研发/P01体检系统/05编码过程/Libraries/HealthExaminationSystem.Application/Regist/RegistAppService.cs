using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.Regist.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Regist
{
    [AbpAuthorize]
    public   class RegistAppService : MyProjectAppServiceBase, IRegistAppService
    {
        private readonly IRepository<TbmRegsit, Guid> _TbmRegsit;
        public RegistAppService(IRepository<TbmRegsit, Guid> TbmRegsit)
        {
            _TbmRegsit = TbmRegsit;
        }
        /// <summary>
        /// 保存注册信息
        /// </summary>
        /// <param name="input"></param>
        public void SaveRegsit(TbmRegsitDto input)
        {
            if (input.Id != Guid.Empty)
            {
                var tbmreg = _TbmRegsit.Get(input.Id);
                if (tbmreg != null)
                {
                    _TbmRegsit.Delete(tbmreg);
                }
                
            }
            TbmRegsit tbmRegsit = new TbmRegsit();
            tbmRegsit.Id = Guid.NewGuid();          
            tbmRegsit.MachineCode = Encrypt(input.MachineCode, "BTV9BTV9");
            tbmRegsit.RegistCode =input.RegistCode;
            tbmRegsit.ClientName = Encrypt(input.ClientName, "BTV9BTV9");
            _TbmRegsit.Insert(tbmRegsit);

        }
        /// <summary>
        /// 查询注册信息
        /// </summary>
        /// <param name="input"></param>
        public List<TbmRegsitDto> SearchRegsit()
        {
            List<TbmRegsitDto> tbmRegsitDtos = new List<TbmRegsitDto>();
            var search = _TbmRegsit.GetAllList();
            if (search.Count > 0)
            {
                foreach (var sear in search)
                {
                    TbmRegsitDto tbmRegsitDto = new TbmRegsitDto();
                    tbmRegsitDto.ClientName = Decrypt(sear.ClientName, "BTV9BTV9");
                    tbmRegsitDto.Id = sear.Id;
                    tbmRegsitDto.MachineCode = Decrypt(sear.MachineCode, "BTV9BTV9");
                    tbmRegsitDto.RegistCode = sear.RegistCode;
                    tbmRegsitDtos.Add(tbmRegsitDto);
                }
            }
            return tbmRegsitDtos;

        }

        
        #region ========加密数据========
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

#endregion

        #region ========解密========

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Decrypt(string Text)
        {
            return Decrypt(Text, "DTcms");
        }
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Decrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion
    }

}
