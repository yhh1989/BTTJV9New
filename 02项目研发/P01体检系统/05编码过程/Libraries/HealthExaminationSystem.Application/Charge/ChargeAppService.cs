using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using AutoMapper;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Sw.Hospital.HealthExamination.Drivers;
using Sw.Hospital.HealthExamination.Drivers.Models.HisInterface;
using Sw.Hospital.HealthExamination.Drivers.Models.NYKInterface;
using Sw.Hospital.HealthExamination.Drivers.Models.YKYInterface;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.HisInterface;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.HealthCard;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;
using Z.EntityFramework.Plus;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge
{
    [AbpAuthorize]
    public class ChargeAppService : MyProjectAppServiceBase, IChargeAppService
    {
        /// <summary>
        /// 支付方式
        /// </summary>
        private readonly IRepository<TbmMChargeType, Guid> _chargeTypeRepository;

        private readonly IRepository<TjlClientTeamInfo, Guid> _clientTeamInfoRepository;

        private readonly IRepository<TbmItemGroup, Guid> _itemGroupRepository;
        private readonly IRepository<TbmDepartment, Guid> _departmentRepository;
        /// <summary>
        /// 预约表
        /// </summary>
        private readonly IRepository<TjlCustomerReg, Guid> _customerRegRepository;

        private readonly IRepository<TjlMcusPayMoney, Guid> _mcusPayMoneyRepository;

        /// <summary>
        /// 结账表
        /// </summary>
        private readonly IRepository<TjlMDiurnalTable, Guid> _diurnalTableRepository;

        /// <summary>
        /// 发票记录
        /// </summary>
        private readonly IRepository<TjlMInvoiceRecord, Guid> _invoiceRecordRepository;

        /// <summary>
        /// 收费方式集合
        /// </summary>
        private readonly IRepository<TjlMPayment, Guid> _paymentRepository;

        /// <summary>
        /// 收费记录
        /// </summary>
        private readonly IRepository<TjlMReceiptInfo, Guid> _receiptInfoRepository;

        /// <summary>
        /// 收费项目详情
        /// </summary>
        private readonly IRepository<TjlMReceiptDetails, Guid> _receiptDetailsRepository;

        private readonly IRepository<TbmMReceiptManager, Guid> _receiptManagerRepository;

        private readonly IRepository<TjlClientReg, Guid> _clientRegRepository;

        private readonly IRepository<TjlCustomerItemGroup, Guid> _customerItemGroupRepository;

        /// <summary>
        /// 收费方式集合
        /// </summary>
        private readonly IRepository<TjlMRise, Guid> _riseRepository;

        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<TbmBasicDictionary, Guid> _BasicDictionaries;

        private readonly IRepository<TjlMInvoiceRecord, Guid> _TjlMInvoiceRecord;

        private readonly IRepository<TjlClientReg, Guid> _TjlClientReg;
        /// <summary>
        /// 申请单号
        /// </summary>
        private readonly IRepository<TjlApplicationForm, Guid> _TjlApplicationForm;

        private readonly ICommonAppService _commonAppService;
        private readonly IRepository<TbmCard, Guid> _TbmCard;
        private readonly IRepository<TjlWebCharge, Guid> _TjlWebCharge;
        public ChargeAppService(
            IRepository<TjlCustomerReg, Guid> customerRegRepository,
            IRepository<TjlMReceiptInfo, Guid> receiptInfoRepository,
            IRepository<TbmMChargeType, Guid> chargeTypeRepository,
            IRepository<TjlMInvoiceRecord, Guid> invoiceRecordRepository,
            IRepository<TjlMReceiptDetails, Guid> receiptDetailsRepository,
            IRepository<TjlMPayment, Guid> paymentRepository,
            IRepository<TjlMDiurnalTable, Guid> diurnalTableRepository,
            IRepository<User, long> userRepository,
            IRepository<TbmItemGroup, Guid> itemGroupRepository,
            IRepository<TjlCustomerItemGroup, Guid> customerItemGroupRepository,
            IRepository<TjlClientTeamInfo, Guid> clientTeamInfoRepository,
            IRepository<TjlClientReg, Guid> clientRegRepository,
            IRepository<TjlMcusPayMoney, Guid> mcusPayMoneyRepository,
            IRepository<TbmMReceiptManager, Guid> receiptManagerRepository,
            IRepository<TjlMRise, Guid> riseRepository,
            IRepository<TbmBasicDictionary, Guid> BasicDictionaries,
            IRepository<TjlMInvoiceRecord, Guid> TjlMInvoiceRecord,
            IRepository<TjlApplicationForm, Guid> TjlApplicationForm,
             ICommonAppService CommonAppService,
             IRepository<TbmCard, Guid> TbmCard,
             IRepository<TjlClientReg, Guid> TjlClientReg,
             IRepository<TjlWebCharge, Guid> TjlWebCharge,
             IRepository<TbmDepartment, Guid> departmentRepository
        )
        {
            _customerRegRepository = customerRegRepository;
            _receiptInfoRepository = receiptInfoRepository;
            _chargeTypeRepository = chargeTypeRepository;
            _invoiceRecordRepository = invoiceRecordRepository;
            _invoiceRecordRepository = invoiceRecordRepository;
            _receiptDetailsRepository = receiptDetailsRepository;
            _paymentRepository = paymentRepository;
            _diurnalTableRepository = diurnalTableRepository;
            _userRepository = userRepository;
            _itemGroupRepository = itemGroupRepository;
            _customerItemGroupRepository = customerItemGroupRepository;
            _clientTeamInfoRepository = clientTeamInfoRepository;
            _clientRegRepository = clientRegRepository;
            _mcusPayMoneyRepository = mcusPayMoneyRepository;
            _receiptManagerRepository = receiptManagerRepository;
            _riseRepository = riseRepository;
            _BasicDictionaries = BasicDictionaries;
            _TjlMInvoiceRecord = TjlMInvoiceRecord;
            _TjlApplicationForm = TjlApplicationForm;
            _commonAppService = CommonAppService;
            _TbmCard = TbmCard;
            _TjlClientReg = TjlClientReg;
            _TjlWebCharge = TjlWebCharge;
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        /// 获取个人收费记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ICollection<MReceiptInfoPerDto> GetReceipt(ChargeCusRegDto input)
        {
            var querey = _receiptInfoRepository.GetAll();
            var row = querey.Where(o => o.CustomerReg.Id == input.Id);
            return row.MapTo<ICollection<MReceiptInfoPerDto>>();
        }

        #region 农行接口相关
        /// <summary>
        /// 农行支付接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OutErrDto WebCharge(WebChargeInputDto input)
        {
            input.out_trade_no = input.out_trade_no.Replace("-", "");
            OutErrDto outErrDto = new OutErrDto();
            var basic = _BasicDictionaries.GetAll().Where(p => p.Type == "WebPay").ToList();
            //线上厂商名称 
            var WebType = basic.FirstOrDefault(p => p.Value == 2)?.Remarks;
            //商户号 
            var merchant_id = basic.FirstOrDefault(p => p.Value == 3)?.Remarks;
            //私钥
            var PrivateKey = basic.FirstOrDefault(p => p.Value == 4)?.Remarks;
            //地址
            var url = basic.FirstOrDefault(p => p.Value == 5)?.Remarks;
            //收银点编号
            var cashier_id = basic.FirstOrDefault(p => p.Value == 6)?.Remarks;
            if (WebType != null && WebType == "农行")
            {

                WebChargeDto webChargeDto = new WebChargeDto();
                using (var wc = new WebClient())
                {

                    webChargeDto.merchant_id = merchant_id;
                    webChargeDto.out_trade_no = input.out_trade_no;
                    webChargeDto.total_amount = input.total_amount;
                    webChargeDto.body = "体检费";
                    //webChargeDto.expire_time = dt;
                    webChargeDto.channel_type = "bmp";
                    webChargeDto.pay_type = "micro";
                    webChargeDto.auth_code = input.auth_code;
                    webChargeDto.timestamp = input.timestamp;

                    var entity = webChargeDto.MapTo<TjlWebCharge>();
                    entity.Id = Guid.NewGuid();
                    entity.ReceiptInfoID = Guid.Parse(input.out_trade_no);
                    entity.CustomerRegBMId = input.CustomerRegBMId;
                    entity.cashier_id = cashier_id;
                    var lWebChargeresult = _TjlWebCharge.Insert(entity);
                    CurrentUnitOfWork.SaveChanges();

                    Dictionary<string, string> dics = new Dictionary<string, string>();
                    dics.Add("merchant_id", merchant_id);
                    if (!string.IsNullOrEmpty(cashier_id))
                    {
                        dics.Add("cashier_id", cashier_id);
                    }
                    //dics.Add("cashier_id", "");
                    //dics.Add("oper_no", "");
                    dics.Add("out_trade_no", input.out_trade_no);
                    dics.Add("total_amount", input.total_amount);
                    dics.Add("body", "体检费");
                    //dics.Add("expire_time", dt);           
                    dics.Add("channel_type", "bmp");
                    dics.Add("pay_type", "micro");
                    dics.Add("auth_code", input.auth_code);
                    //dics.Add("appid", "");
                    //dics.Add("openid", "");
                    //dics.Add("spbill_create_ip", "");
                    //dics.Add("callback_url", "");
                    //dics.Add("notify_url", "");
                    //dics.Add("is_split_order", "");
                    //dics.Add("split_info", "");
                    //dics.Add("terminal_id", "");
                    //dics.Add("location", "");
                    dics.Add("timestamp", input.timestamp);

                    var sin = getParamSrc(dics);
                    //var sin = requestPay(webChargeDto);
                    var sign = Sha256WithRsa(sin, PrivateKey);
                    //webChargeDto.sign = sign;
                    dics.Add("sign", sign);
                    var json = JsonConvert.SerializeObject(dics);
                    var request = (HttpWebRequest)WebRequest.Create(url + "//create");
                    //var request = (HttpWebRequest)WebRequest.Create(url);
                    request.ContentType = "application/json";
                    request.Method = "POST";
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(json);
                    }

                    var response = (HttpWebResponse)request.GetResponse();
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var OutReSult = JsonConvert.DeserializeObject<WebChargeOutDto>(result);
                        if (OutReSult.code == "200")
                        {
                            outErrDto.code = "1";
                            outErrDto.merchant_id = OutReSult.merchant_id;
                            outErrDto.pay_order_id = OutReSult.pay_order_id;
                            return outErrDto;
                        }
                        else if (OutReSult.code == "201")
                        {
                            var nowTime = System.DateTime.Now;
                            //System.Threading.Thread.Sleep(6 * 1000);
                            //bool isOK = false;
                            while (System.DateTime.Now < nowTime.AddMinutes(1))
                            {
                                System.Threading.Thread.Sleep(10 * 1000);

                                Dictionary<string, string> seachdics = new Dictionary<string, string>();
                                seachdics.Add("merchant_id", merchant_id);
                                //dics.Add("pay_order_id", webChargeDto.out_trade_no);
                                seachdics.Add("out_trade_no", input.out_trade_no);
                                //dics.Add("transaction_id", webChargeDto.body);                            
                                seachdics.Add("timestamp", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                var seachesin = getParamSrc(seachdics);
                                var seachesign = Sha256WithRsa(seachesin, PrivateKey);
                                seachdics.Add("sign", seachesign);
                                var seachjson = JsonConvert.SerializeObject(seachdics);
                                var seachrequest = (HttpWebRequest)WebRequest.Create(url + "//query");
                                seachrequest.ContentType = "application/json";
                                seachrequest.Method = "POST";
                                using (var streamWriter = new StreamWriter(seachrequest.GetRequestStream()))
                                {
                                    streamWriter.Write(seachjson);
                                }

                                var seachresponse = (HttpWebResponse)seachrequest.GetResponse();
                                using (var seachstreamReader = new StreamReader(seachresponse.GetResponseStream()))
                                {
                                    var seachresult = seachstreamReader.ReadToEnd();
                                    var OutseachReSult = JsonConvert.DeserializeObject<WebChargeOutDto>(seachresult);
                                    if (OutseachReSult.code == "200")
                                    {
                                        outErrDto.code = "1";
                                        outErrDto.merchant_id = OutseachReSult.merchant_id;
                                        outErrDto.pay_order_id = OutseachReSult.pay_order_id;
                                        return outErrDto;
                                    }
                                    else if (OutseachReSult.code != "201")
                                    {
                                        outErrDto.code = "0";
                                        outErrDto.cardInfo = OutseachReSult.message;
                                        outErrDto.err = OutseachReSult.message;
                                        return outErrDto;

                                    }
                                }
                            }
                            outErrDto.code = "0";
                            outErrDto.cardInfo = "支付超时";

                            return outErrDto;
                        }
                        else
                        {
                            outErrDto.code = "0";
                            outErrDto.cardInfo = OutReSult.message;
                            outErrDto.err = OutReSult.message;
                            return outErrDto;
                        }

                    }


                }
            }
            else
            {
                outErrDto.code = "0";
                outErrDto.cardInfo = "无字典中设置的线上支付厂家";
                return outErrDto;
            }

        }

        /// <summary>
        /// 农行退款接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OutErrDto WebRefund(WebChargeInputDto input)
        {
            input.out_trade_no = input.out_trade_no.Replace("-", "");
            OutErrDto outErrDto = new OutErrDto();
            var basic = _BasicDictionaries.GetAll().Where(p => p.Type == "WebPay").ToList();
            //商户号 
            var WebType = basic.FirstOrDefault(p => p.Value == 2)?.Remarks;
            //商户号 
            var merchant_id = basic.FirstOrDefault(p => p.Value == 3)?.Remarks;
            //私钥
            var PrivateKey = basic.FirstOrDefault(p => p.Value == 4)?.Remarks;
            //地址
            var url = basic.FirstOrDefault(p => p.Value == 5)?.Remarks;

            //收银点编号
            var cashier_id = basic.FirstOrDefault(p => p.Value == 6)?.Remarks;

            if (WebType != null && WebType == "农行")
            {


                using (var wc = new WebClient())
                {

                    Dictionary<string, string> dics = new Dictionary<string, string>();
                    dics.Add("merchant_id", merchant_id);
                    dics.Add("cashier_id", cashier_id);

                    dics.Add("out_refund_no", input.out_refund_no.Replace("-", ""));
                    dics.Add("out_trade_no", input.out_trade_no.Replace("-", ""));
                    dics.Add("refund_amount", input.total_amount);
                    dics.Add("timestamp", input.timestamp);
                    var sin = getParamSrc(dics);
                    var sign = Sha256WithRsa(sin, PrivateKey);
                    dics.Add("sign", sign);
                    var json = JsonConvert.SerializeObject(dics);
                    var request = (HttpWebRequest)WebRequest.Create(url + "//refund");
                    request.ContentType = "application/json";
                    request.Method = "POST";
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(json);
                    }

                    var response = (HttpWebResponse)request.GetResponse();
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        var OutReSult = JsonConvert.DeserializeObject<dynamic>(result);
                        //var ss = Convert.ToString(OutReSult);
                        //File.AppendAllText(Path.Combine(Variables.LogDirectory, $"ApiTakesLog-{DateTime.Now:yyyyMMdd}.txt"), OutReSult);

                      
                        if (OutReSult.code == "200")
                        {
                            outErrDto.code = "1";
                            outErrDto.merchant_id = OutReSult.out_refund_no;
                            outErrDto.pay_order_id = OutReSult.plat_refund_id;
                            return outErrDto;
                        }
                        else
                        {
                            outErrDto.code = "0";
                            outErrDto.cardInfo = OutReSult.message;

                            return outErrDto;
                        }


                    }


                }
            }
            else
            {
                outErrDto.code = "0";
                outErrDto.cardInfo = "无字典中设置的线上支付厂家";
                return outErrDto;
            }

        }

        /// <summary>
        /// Sha256 签名
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Encoding Encoding { get; set; }
        public string Sha256WithRsa(string input, string PrivateKey)
        {
            var ss = Convert.FromBase64String(PrivateKey);
            if (PrivateKeyFactory.CreateKey(ss) is RsaPrivateCrtKeyParameters pvk)
            {
                var p = new RSAParameters
                {
                    Modulus = pvk.Modulus.ToByteArrayUnsigned(),
                    Exponent = pvk.PublicExponent.ToByteArrayUnsigned(),
                    D = pvk.Exponent.ToByteArrayUnsigned(),
                    DP = pvk.DP.ToByteArrayUnsigned(),
                    DQ = pvk.DQ.ToByteArrayUnsigned(),
                    P = pvk.P.ToByteArrayUnsigned(),
                    Q = pvk.Q.ToByteArrayUnsigned(),
                    InverseQ = pvk.QInv.ToByteArrayUnsigned()
                };

                using (var rsa = new RSACryptoServiceProvider())
                {
                    rsa.ImportParameters(p);
                    using (var sha256 = new SHA256CryptoServiceProvider())
                    {
                        var value = input;

                        if (Debugger.IsAttached)
                        {
                            Debugger.Break();
                        }
                        Encoding = new UTF8Encoding(false);
                        //value = @"<body><reportCardList><reportCard><userInfo><name>雷超庆</name><idcardType>1</idcardType><idcardCode>421081199007092998</idcardCode><sexCode>1</sexCode><birthday>19900709</birthday><telPhone>15927808013</telPhone><emergencyContactPerson>聂畑</emergencyContactPerson><emergencyContactPhone>15927808013</emergencyContactPhone><workshop/><jobCode/><otherJobName>机修工</otherJobName><radiationType/><maritalStatusCode>0</maritalStatusCode></userInfo><empInfo><creditCode>91421022MA48T4EY8W</creditCode><employerName>山鹰华中纸业有限公司</employerName><economicTypeCode>130</economicTypeCode><industryCategoryCode>2223</industryCategoryCode><enterpriseSizeCode>10000</enterpriseSizeCode><areaCode>421022000</areaCode><address>青吉工业园</address><addressZipCode>0</addressZipCode><contactPerson>聂畑</contactPerson><employerPhone>15927808013</employerPhone><areaName>湖北省荆州市公安县</areaName><jobNumber>操作车间</jobNumber></empInfo><empInfoEmployer><creditCodeEmployer>91421022MA48T4EY8W</creditCodeEmployer><employerNameEmployer>山鹰华中纸业有限公司</employerNameEmployer><economicTypeCodeEmployer>130</economicTypeCodeEmployer><industryCategoryCodeEmployer>2223</industryCategoryCodeEmployer><enterpriseSizeCodeEmployer>10000</enterpriseSizeCodeEmployer><areaCodeEmployer>421022000</areaCodeEmployer><areaNameEmployer>湖北省荆州市公安县</areaNameEmployer></empInfoEmployer><orgInfo><orgCode>330206999</orgCode><orgName>公安县疾病预防控制中心</orgName></orgInfo><cardInfo><code>210316039</code><type>101</type><seniorityYear>9</seniorityYear><seniorityMonth>0</seniorityMonth><exposureYear>0</exposureYear><exposureMonth>0</exposureMonth><checkType>11</checkType><bodyCheckType>1</bodyCheckType><previousCardId/><checkTime>20210316091157</checkTime><writePerson>万小峰</writePerson><writePersonTel>0716-5150394</writePersonTel><writeDate>20210316</writeDate><reportOrgName>公安县疾病预防控制中心</reportOrgName><reportTime>20210316094908</reportTime><checkResultCode>复查</checkResultCode><suggest>脱离噪声环境48小时后复查</suggest><checkDoctor>袁发萍</checkDoctor><monitorTypeCode>01</monitorTypeCode><reportUnit>公安县疾病预防控制中心</reportUnit><reportPerson>万小峰</reportPerson><reportPersonTel>0716-5150394</reportPersonTel><remark/></cardInfo><contactHazardFactorList><contactHazardFactor><hazardCode>13001</hazardCode><otherHazardName>噪声</otherHazardName></contactHazardFactor><contactHazardFactor><hazardCode>13002</hazardCode><otherHazardName>高温（高温作业）</otherHazardName></contactHazardFactor></contactHazardFactorList><hazardFactorList><hazardFactor><hazardCode>13001</hazardCode><otherHazardName>噪声</otherHazardName><hazardStartDate>20210316</hazardStartDate><hazardYear>0</hazardYear><hazardMonth>0</hazardMonth></hazardFactor><hazardFactor><hazardCode>13002</hazardCode><otherHazardName>高温（高温作业）</otherHazardName><hazardStartDate>20210316</hazardStartDate><hazardYear>0</hazardYear><hazardMonth>0</hazardMonth></hazardFactor></hazardFactorList><itemList><item><itemId>701009</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>38.00</result><type>2</type><unit>fL</unit><max>无</max><min>无</min><checkResult>38.00</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>500007</itemId><otherItemName/><itemGroupName>神经系统</itemGroupName><department>内外科</department><result>正常</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>正常</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>701002</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>5.48</result><type>2</type><unit>10^12/L</unit><max>无</max><min>无</min><checkResult>5.48</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>702016</itemId><otherItemName/><itemGroupName>尿常规</itemGroupName><department>检验科</department><result>-</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>-</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>805016</itemId><otherItemName/><itemGroupName>纯音听阈测试</itemGroupName><department>电测听</department><result>28</result><type>2</type><unit>无</unit><max>无</max><min>无</min><checkResult>语频听阈28</checkResult><mark>0</mark><checkDate>20210316</checkDate><checkDoctor>彭玲玲</checkDoctor></item><item><itemId>601019</itemId><otherItemName/><itemGroupName>五官检查</itemGroupName><department>内外科</department><result>正常</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>正常</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>805009</itemId><otherItemName/><itemGroupName>纯音听阈测试</itemGroupName><department>电测听</department><result>45</result><type>2</type><unit>无</unit><max>无</max><min>无</min><checkResult>左耳2000Hz45</checkResult><mark>0</mark><checkDate>20210316</checkDate><checkDoctor>彭玲玲</checkDoctor></item><item><itemId>702007</itemId><otherItemName/><itemGroupName>尿常规</itemGroupName><department>检验科</department><result>-</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>-</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>701026</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>0.38</result><type>2</type><unit>10^9/L</unit><max>无</max><min>无</min><checkResult>0.38</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>701011</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>0.154</result><type>2</type><unit>%</unit><max>无</max><min>无</min><checkResult>0.154</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>200010</itemId><otherItemName/><itemGroupName>内科常规</itemGroupName><department>内外科</department><result>130</result><type>2</type><unit>mmHg</unit><max>无</max><min>无</min><checkResult>130</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>702012</itemId><otherItemName/><itemGroupName>尿常规</itemGroupName><department>检验科</department><result>-</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>-</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>805010</itemId><otherItemName/><itemGroupName>纯音听阈测试</itemGroupName><department>电测听</department><result>40</result><type>2</type><unit>无</unit><max>无</max><min>无</min><checkResult>40</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>彭玲玲</checkDoctor></item><item><itemId>500022</itemId><otherItemName/><itemGroupName>神经系统</itemGroupName><department>内外科</department><result>正常</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>正常</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>606001</itemId><otherItemName/><itemGroupName>外科常规</itemGroupName><department>内外科</department><result>正常</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>正常</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>500008</itemId><otherItemName/><itemGroupName>神经系统</itemGroupName><department>内外科</department><result>正常</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>正常</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>701027</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>1.30</result><type>2</type><unit>%</unit><max>无</max><min>无</min><checkResult>1.30</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>500024</itemId><otherItemName/><itemGroupName>神经系统</itemGroupName><department>内外科</department><result>正常</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>正常</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>500023</itemId><otherItemName/><itemGroupName>神经系统</itemGroupName><department>内外科</department><result>正常</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>正常</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>805005</itemId><otherItemName/><itemGroupName>纯音听阈测试</itemGroupName><department>电测听</department><result>35</result><type>2</type><unit>无</unit><max>无</max><min>无</min><checkResult>35</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>彭玲玲</checkDoctor></item><item><itemId>703004</itemId><otherItemName/><itemGroupName>肝功能检查</itemGroupName><department>检验科</department><result>16.8</result><type>2</type><unit>U/L</unit><max>无</max><min>无</min><checkResult>16.8</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>606010</itemId><otherItemName/><itemGroupName>外科常规</itemGroupName><department>内外科</department><result>正常</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>正常</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>805015</itemId><otherItemName/><itemGroupName>纯音听阈测试</itemGroupName><department>电测听</department><result>35</result><type>2</type><unit>无</unit><max>无</max><min>无</min><checkResult>35</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>彭玲玲</checkDoctor></item><item><itemId>805002</itemId><otherItemName/><itemGroupName>纯音听阈测试</itemGroupName><department>电测听</department><result>25</result><type>2</type><unit>无</unit><max>无</max><min>无</min><checkResult>25</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>彭玲玲</checkDoctor></item><item><itemId>601004</itemId><otherItemName/><itemGroupName>五官检查</itemGroupName><department>内外科</department><result>正常</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>正常</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>701028</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>0.07</result><type>2</type><unit>10^9/L</unit><max>无</max><min>无</min><checkResult>0.07</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>702002</itemId><otherItemName/><itemGroupName>尿常规</itemGroupName><department>检验科</department><result>-</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>-</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>701030</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>0.02</result><type>2</type><unit>10^9/L</unit><max>无</max><min>无</min><checkResult>0.02</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>606002</itemId><otherItemName/><itemGroupName>外科常规</itemGroupName><department>内外科</department><result>正常</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>正常</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>805012</itemId><otherItemName/><itemGroupName>纯音听阈测试</itemGroupName><department>电测听</department><result>35</result><type>2</type><unit>无</unit><max>无</max><min>无</min><checkResult>35</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>彭玲玲</checkDoctor></item><item><itemId>705001</itemId><otherItemName/><itemGroupName>血糖</itemGroupName><department>检验科</department><result>4.87</result><type>2</type><unit>mmol/L</unit><max>无</max><min>无</min><checkResult>4.87</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>805001</itemId><otherItemName/><itemGroupName>纯音听阈测试</itemGroupName><department>电测听</department><result>25</result><type>2</type><unit>无</unit><max>无</max><min>无</min><checkResult>25</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>彭玲玲</checkDoctor></item><item><itemId>703007</itemId><otherItemName/><itemGroupName>肝功能检查</itemGroupName><department>检验科</department><result>78.5</result><type>2</type><unit>g/L</unit><max>无</max><min>无</min><checkResult>78.5</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>500005</itemId><otherItemName/><itemGroupName>神经系统</itemGroupName><department>内外科</department><result>正常</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>正常</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>703005</itemId><otherItemName/><itemGroupName>肝功能检查</itemGroupName><department>检验科</department><result>1.1</result><type>2</type><unit>无</unit><max>无</max><min>无</min><checkResult>1.1</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>701016</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>3.54</result><type>2</type><unit>10^9/L</unit><max>无</max><min>无</min><checkResult>3.54</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>701006</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>29.50</result><type>2</type><unit>pg</unit><max>无</max><min>无</min><checkResult>29.50</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>701029</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>0.30</result><type>2</type><unit>%</unit><max>无</max><min>无</min><checkResult>0.30</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>701010</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>188.00</result><type>2</type><unit>10^9/L</unit><max>无</max><min>无</min><checkResult>188.00</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>805006</itemId><otherItemName/><itemGroupName>纯音听阈测试</itemGroupName><department>电测听</department><result>40</result><type>2</type><unit>无</unit><max>无</max><min>无</min><checkResult>40</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>彭玲玲</checkDoctor></item><item><itemId>805004</itemId><otherItemName/><itemGroupName>纯音听阈测试</itemGroupName><department>电测听</department><result>30</result><type>2</type><unit>无</unit><max>无</max><min>无</min><checkResult>30</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>彭玲玲</checkDoctor></item><item><itemId>805013</itemId><otherItemName/><itemGroupName>纯音听阈测试</itemGroupName><department>电测听</department><result>26</result><type>2</type><unit>无</unit><max>无</max><min>无</min><checkResult>26</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>彭玲玲</checkDoctor></item><item><itemId>701001</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>5.43</result><type>2</type><unit>10^9/L</unit><max>无</max><min>无</min><checkResult>5.43</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>702014</itemId><otherItemName/><itemGroupName>尿常规</itemGroupName><department>检验科</department><result>-</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>-</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>400003</itemId><otherItemName/><itemGroupName>内科常规</itemGroupName><department>内外科</department><result>未触及</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>未触及</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>601002</itemId><otherItemName/><itemGroupName>五官检查</itemGroupName><department>内外科</department><result>正常</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>正常</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>500009</itemId><otherItemName/><itemGroupName>神经系统</itemGroupName><department>内外科</department><result>正常</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>正常</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>400005</itemId><otherItemName/><itemGroupName>内科常规</itemGroupName><department>内外科</department><result>正常</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>正常</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>802009</itemId><otherItemName/><itemGroupName>心电图</itemGroupName><department>心电图</department><result>诊断结果:窦性心律心电轴右偏</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>诊断结果:窦性心律心电轴右偏</checkResult><mark>0</mark><checkDate>20210316</checkDate><checkDoctor>魏娟</checkDoctor></item><item><itemId>702013</itemId><otherItemName/><itemGroupName>尿常规</itemGroupName><department>检验科</department><result>-</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>-</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>701007</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>330.00</result><type>2</type><unit>g/L</unit><max>无</max><min>无</min><checkResult>330.00</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>703002</itemId><otherItemName/><itemGroupName>肝功能检查</itemGroupName><department>检验科</department><result>12.6</result><type>2</type><unit>umol/L</unit><max>无</max><min>无</min><checkResult>12.6</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>701004</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>48.90</result><type>2</type><unit>%</unit><max>无</max><min>无</min><checkResult>48.90</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>701020</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>15.30</result><type>2</type><unit>%</unit><max>无</max><min>无</min><checkResult>15.30</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>703008</itemId><otherItemName/><itemGroupName>肝功能检查</itemGroupName><department>检验科</department><result>50.0</result><type>2</type><unit>g/L</unit><max>无</max><min>无</min><checkResult>50.0</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>805029</itemId><otherItemName/><itemGroupName>纯音听阈测试</itemGroupName><department>电测听</department><result>31</result><type>2</type><unit>无</unit><max>无</max><min>无</min><checkResult>31</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>彭玲玲</checkDoctor></item><item><itemId>703003</itemId><otherItemName/><itemGroupName>肝功能检查</itemGroupName><department>检验科</department><result>7.1</result><type>2</type><unit>umol/L</unit><max>无</max><min>无</min><checkResult>7.1</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>701025</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>7.00</result><type>2</type><unit>%</unit><max>无</max><min>无</min><checkResult>7.00</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>701015</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>11.90</result><type>2</type><unit>%</unit><max>无</max><min>无</min><checkResult>11.90</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>701014</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>8.20</result><type>2</type><unit>fL</unit><max>无</max><min>无</min><checkResult>8.20</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>701005</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>89.30</result><type>2</type><unit>fL</unit><max>无</max><min>无</min><checkResult>89.30</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>805007</itemId><otherItemName/><itemGroupName>纯音听阈测试</itemGroupName><department>电测听</department><result>25</result><type>2</type><unit>无</unit><max>无</max><min>无</min><checkResult>25</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>彭玲玲</checkDoctor></item><item><itemId>702001</itemId><otherItemName/><itemGroupName>尿常规</itemGroupName><department>检验科</department><result>-</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>-</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>200011</itemId><otherItemName/><itemGroupName>内科常规</itemGroupName><department>内外科</department><result>80</result><type>2</type><unit>mmHg</unit><max>无</max><min>无</min><checkResult>80</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>703001</itemId><otherItemName/><itemGroupName>肝功能检查</itemGroupName><department>检验科</department><result>5.6</result><type>2</type><unit>umol/L</unit><max>无</max><min>无</min><checkResult>5.6</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>702010</itemId><otherItemName/><itemGroupName>尿常规</itemGroupName><department>检验科</department><result>1.015</result><type>2</type><unit>无</unit><max>无</max><min>无</min><checkResult>1.015</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>603001</itemId><otherItemName/><itemGroupName>五官检查</itemGroupName><department>内外科</department><result>正常</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>正常</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>604001</itemId><otherItemName/><itemGroupName>五官检查</itemGroupName><department>内外科</department><result>正常</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>正常</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>702003</itemId><otherItemName/><itemGroupName>尿常规</itemGroupName><department>检验科</department><result>Normal</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>Normal</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>500006</itemId><otherItemName/><itemGroupName>神经系统</itemGroupName><department>内外科</department><result>正常</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>正常</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>602013</itemId><otherItemName/><itemGroupName>五官检查</itemGroupName><department>内外科</department><result>正常</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>正常</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>701017</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>65.30</result><type>2</type><unit>%</unit><max>无</max><min>无</min><checkResult>65.30</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>701003</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>161.00</result><type>2</type><unit>g/L</unit><max>无</max><min>无</min><checkResult>161.00</checkResult><mark>0</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>701013</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>16.10</result><type>2</type><unit>fL</unit><max>无</max><min>无</min><checkResult>16.10</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>805008</itemId><otherItemName/><itemGroupName>纯音听阈测试</itemGroupName><department>电测听</department><result>25</result><type>2</type><unit>无</unit><max>无</max><min>无</min><checkResult>25</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>彭玲玲</checkDoctor></item><item><itemId>805030</itemId><otherItemName/><itemGroupName>纯音听阈测试</itemGroupName><department>电测听</department><result>26</result><type>2</type><unit>无</unit><max>无</max><min>无</min><checkResult>26</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>彭玲玲</checkDoctor></item><item><itemId>701023</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>1.42</result><type>2</type><unit>10^9/L</unit><max>无</max><min>无</min><checkResult>1.42</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>805014</itemId><otherItemName/><itemGroupName>纯音听阈测试</itemGroupName><department>电测听</department><result>31</result><type>2</type><unit>无</unit><max>无</max><min>无</min><checkResult>31</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>彭玲玲</checkDoctor></item><item><itemId>604011</itemId><otherItemName/><itemGroupName>五官检查</itemGroupName><department>内外科</department><result>正常</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>正常</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>805003</itemId><otherItemName/><itemGroupName>纯音听阈测试</itemGroupName><department>电测听</department><result>30</result><type>2</type><unit>无</unit><max>无</max><min>无</min><checkResult>右耳2000Hz30</checkResult><mark>0</mark><checkDate>20210316</checkDate><checkDoctor>彭玲玲</checkDoctor></item><item><itemId>701018</itemId><otherItemName/><itemGroupName>血常规</itemGroupName><department>检验科</department><result>26.10</result><type>2</type><unit>%</unit><max>无</max><min>无</min><checkResult>26.10</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>400004</itemId><otherItemName/><itemGroupName>内科常规</itemGroupName><department>内外科</department><result>未触及</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>未触及</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>702008</itemId><otherItemName/><itemGroupName>尿常规</itemGroupName><department>检验科</department><result>7.5</result><type>2</type><unit>无</unit><max>无</max><min>无</min><checkResult>7.5</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>703006</itemId><otherItemName/><itemGroupName>肝功能检查</itemGroupName><department>检验科</department><result>19.3</result><type>2</type><unit>U/L</unit><max>无</max><min>无</min><checkResult>19.3</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>702009</itemId><otherItemName/><itemGroupName>尿常规</itemGroupName><department>检验科</department><result>-</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>-</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>蒋再安</checkDoctor></item><item><itemId>606005</itemId><otherItemName/><itemGroupName>外科常规</itemGroupName><department>内外科</department><result>正常</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>正常</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item><item><itemId>805011</itemId><otherItemName/><itemGroupName>纯音听阈测试</itemGroupName><department>电测听</department><result>45</result><type>2</type><unit>无</unit><max>无</max><min>无</min><checkResult>45</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>彭玲玲</checkDoctor></item><item><itemId>500014</itemId><otherItemName/><itemGroupName>神经系统</itemGroupName><department>内外科</department><result>正常</result><type>3</type><unit>无</unit><max>无</max><min>无</min><checkResult>正常</checkResult><mark>1</mark><checkDate>20210316</checkDate><checkDoctor>袁发萍</checkDoctor></item></itemList><diagnosisList><diagnosis><conclusion>2</conclusion><repeatItemList><repeatItem><repeatItemId>805016</repeatItemId><otherItemName/></repeatItem><repeatItem><repeatItemId>805009</repeatItemId><otherItemName/></repeatItem><repeatItem><repeatItemId>805010</repeatItemId><otherItemName/></repeatItem><repeatItem><repeatItemId>805005</repeatItemId><otherItemName/></repeatItem><repeatItem><repeatItemId>805015</repeatItemId><otherItemName/></repeatItem><repeatItem><repeatItemId>805002</repeatItemId><otherItemName/></repeatItem><repeatItem><repeatItemId>805012</repeatItemId><otherItemName/></repeatItem><repeatItem><repeatItemId>805001</repeatItemId><otherItemName/></repeatItem><repeatItem><repeatItemId>805006</repeatItemId><otherItemName/></repeatItem><repeatItem><repeatItemId>805004</repeatItemId><otherItemName/></repeatItem><repeatItem><repeatItemId>805013</repeatItemId><otherItemName/></repeatItem><repeatItem><repeatItemId>805029</repeatItemId><otherItemName/></repeatItem><repeatItem><repeatItemId>805007</repeatItemId><otherItemName/></repeatItem><repeatItem><repeatItemId>805008</repeatItemId><otherItemName/></repeatItem><repeatItem><repeatItemId>805030</repeatItemId><otherItemName/></repeatItem><repeatItem><repeatItemId>805014</repeatItemId><otherItemName/></repeatItem><repeatItem><repeatItemId>805003</repeatItemId><otherItemName/></repeatItem><repeatItem><repeatItemId>805011</repeatItemId><otherItemName/></repeatItem></repeatItemList></diagnosis></diagnosisList><auditInfo><auditStatus>1</auditStatus><auditDesc/><auditDate>20210316</auditDate><auditorName>袁发萍</auditorName></auditInfo><healthSurvey><questionnaire><id>210316039</id><surveyDate>20210316</surveyDate><smkSituation>1</smkSituation><smkNum>0</smkNum><smkYear>0</smkYear><drkSituation>1</drkSituation><drkNum>0</drkNum><drkYear>0</drkYear><childrenNum>0</childrenNum><abortionNum>0</abortionNum><stillbirthNum>0</stillbirthNum><prematureBirthNum>0</prematureBirthNum><abnormalFetalNum>0</abnormalFetalNum><menarcheAge>13</menarcheAge><period>28</period><cycle>28</cycle><menopauseAge>0</menopauseAge><occupationHistoryList/><diseaseHistoryList/><occupationDieaseHistoryList/><marriageHistoryList/></questionnaire></healthSurvey></reportCard></reportCardList><employerList><employer><creditCode>91421022MA48T4EY8W</creditCode><employerName>山鹰华中纸业有限公司</employerName><orgCode>330206999</orgCode><orgName>公安县疾病预防控制中心</orgName><areaCode>421022000</areaCode><economicTypeCode>130</economicTypeCode><industryCategoryCode>2223</industryCategoryCode><enterpriseSizeCode>10000</enterpriseSizeCode><address>青吉工业园</address><addressZipCode>0</addressZipCode><contactPerson>聂畑</contactPerson><contactPhone>15927808013</contactPhone><isSubsidiary>false</isSubsidiary><secondEmployerCode/><createAreaCode>421022000</createAreaCode><writeUnit>公安县疾病预防控制中心</writeUnit><writePerson>万小峰</writePerson><writePersonTel>0716-5150394</writePersonTel><writeDate>20210908</writeDate><reportUnit>公安县疾病预防控制中心</reportUnit><reportPerson>万小峰</reportPerson><reportPersonTel>0716-5150394</reportPersonTel><reportDate>20210908</reportDate><auditStatus>1</auditStatus><auditInfo/><auditTime>20210308</auditTime><auditorName/></employer></employerList></body>";
                        var valueBytes = Encoding.GetBytes(input);

                        var signData = rsa.SignData(valueBytes, sha256);
                        return Convert.ToBase64String(signData);
                    }
                }
            }

            return null;
        }

        public static String getParamSrc(Dictionary<string, string> paramsMap)
        {
            var vDic = (from objDic in paramsMap orderby objDic.Key ascending select objDic);
            StringBuilder str = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in vDic)
            {
                string pkey = kv.Key;
                string pvalue = kv.Value;
                str.Append(pkey + "=" + pvalue + "&");
            }
            String result = str.ToString().Substring(0, str.ToString().Length - 1);
            return result;
        }

        #endregion
        /// <summary>
        /// 线上支付查询
        /// </summary>
        /// <returns></returns>
        public List<WebChargeShow> getWebCharge(InSearchBusiCusDto input)
        {
            var qure = _TjlWebCharge.GetAll();
            if (input.StarDate.HasValue)
            {
                qure = qure.Where(p => p.CreationTime >= input.StarDate);
            }
            if (input.EndDate.HasValue)
            { qure = qure.Where(p => p.CreationTime < input.EndDate); }
            if (!string.IsNullOrEmpty(input.LinkName))
            {
                qure = qure.Where(p => p.CustomerRegBM != null &&
                p.CustomerRegBM.Customer != null && (
                p.CustomerRegBM.CustomerBM == input.LinkName ||
                 p.CustomerRegBM.Customer.Name.Contains(input.LinkName)));
            }
            var outList = qure.Select(p => new WebChargeShow
            {
                body = p.body,
                cashier_id = p.cashier_id,
                CustomerBM = p.CustomerRegBM.CustomerBM,
                Name = p.CustomerRegBM.Customer.Name,
                Sex = p.CustomerRegBM.Customer.Sex,
                merchant_id = p.merchant_id,
                oper_no = p.oper_no,
                out_trade_no = p.out_trade_no,
                total_amount = p.total_amount,
                MReceiptInfoID = p.ReceiptInfoID,
                createTime = p.CreationTime
            }).ToList();
            foreach (var outpay in outList)
            {
                var cusPay = _receiptInfoRepository.GetAll().FirstOrDefault(p => p.Id ==
               outpay.MReceiptInfoID);
                if (cusPay == null)
                { outpay.Mess = "收费异常"; }
                else
                { outpay.Mess = "收费正常"; }
            }
            return outList;

        }
        /// <summary>
        /// 获取收费员Id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ReceiptInfoPerViewDto GetReceiptDetailed(EntityDto<Guid> input)
        {
            var row = _receiptInfoRepository.FirstOrDefault(o => o.Id == input.Id);
            return row.MapTo<ReceiptInfoPerViewDto>();
        }

        /// <summary>
        /// 获取该用户最低价格
        /// </summary>
        /// <returns></returns>
        public decimal MinMoney(SeachChargrDto dto)
        {
            decimal mMinMoney = 0;
            foreach (var customerItemGroup in dto.ItemGroups)
            {
                decimal zkl = 1;
                if (!decimal.TryParse(dto.user.Discount, out var userZKL))
                {
                    userZKL = 0;
                }

                var groupZKL = customerItemGroup.MaxDiscount.ToString() == ""
                    ? 0
                    : decimal.Parse(customerItemGroup.MaxDiscount.ToString());
                if (userZKL >= groupZKL)
                {
                    zkl = userZKL;
                }
                else if (userZKL < groupZKL)
                {
                    zkl = groupZKL;
                }

                mMinMoney += customerItemGroup.ItemPrice * zkl;
            }

            return mMinMoney;
        }

        /// <summary>
        /// 计算项目价格
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public ICollection<GroupMoneyDto> MinCusGroupMoney(SeachChargrDto dto)
        {
            var groupMoney = dto.ItemGroups.Where(r => r.IsAddMinus >= 0).Sum(r => r.ItemPrice) - dto.minMoney;
            var ZKL = 0M;
            if (groupMoney > 0)
            {
                ZKL = (dto.PayMoney - dto.minDiscountMoney) / groupMoney;
            }

            decimal MinGroupZKMoney = 0;
            decimal MinGroupMoney = 0;
            ICollection<GroupMoneyDto> GroupMins = null;
            GroupMins = dto.ItemGroups
                .Where(r => r.MaxDiscount != null && r.IsAddMinus >= 0 && r.MaxDiscount > ZKL)
                .MapTo<ICollection<GroupMoneyDto>>();
            MinGroupZKMoney = GroupMins.Sum(r => r.ItemPrice * decimal.Parse(r.MaxDiscount?.ToString()));
            MinGroupMoney = GroupMins.Sum(r => r.ItemPrice);
            foreach (var customerItemGroup in dto.ItemGroups)
                if (customerItemGroup.MaxDiscount == null)
                {
                    if (ZKL == 1)
                    {
                        customerItemGroup.PriceAfterDis = customerItemGroup.ItemPrice * customerItemGroup.DiscountRate;
                    }

                    if (ZKL < 1)
                    {
                        customerItemGroup.DiscountRate = ZKL;
                        customerItemGroup.PriceAfterDis = customerItemGroup.ItemPrice * ZKL;
                    }
                }
                else
                {
                    var minGroupZKL = decimal.Parse(customerItemGroup.MaxDiscount.ToString());

                    if (minGroupZKL > ZKL)
                    {
                        customerItemGroup.DiscountRate = minGroupZKL;
                        customerItemGroup.PriceAfterDis = customerItemGroup.ItemPrice * minGroupZKL;
                    }
                    else
                    {
                        customerItemGroup.DiscountRate = ZKL;
                        customerItemGroup.PriceAfterDis = customerItemGroup.ItemPrice * ZKL;
                    }
                }

            if (MinGroupMoney - dto.minMoney > 0)
            {
                dto.ItemGroups = MinCusGroupMoney(new SeachChargrDto
                {
                    ItemGroups = dto.ItemGroups,
                    user = dto.user,
                    PayMoney = dto.PayMoney,
                    minMoney = MinGroupMoney,
                    minDiscountMoney = MinGroupZKMoney
                });
            }

            var mj = dto.ItemGroups.Sum(r => r.PriceAfterDis);
            return dto.ItemGroups;
        }

        /// <summary>
        /// 计算项目价格,处理换项等情况
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<GroupMoneyDto> CusGroupMoney(SeachChargrDto dto)
        {
            var groupMoney = dto.ItemGroups.Where(r => r.IsAddMinus != (int)AddMinusType.Minus).Sum(r => r.ItemPrice) - dto.minMoney;
            decimal ZKL = 1;
            if (dto.PayMoney != 0)
            {
                ZKL = (dto.PayMoney - dto.minDiscountMoney) / groupMoney;
            }


            decimal MinGroupZKMoney = 0;
            decimal MinGroupMoney = 0;
            ICollection<GroupMoneyDto> GroupMins = null;
            GroupMins = dto.ItemGroups
                .Where(r => r.MaxDiscount != null && r.IsAddMinus != (int)AddMinusType.Minus && r.MaxDiscount > ZKL)
                .MapTo<ICollection<GroupMoneyDto>>();
            MinGroupZKMoney = GroupMins.Sum(r => r.ItemPrice * decimal.Parse(r.MaxDiscount?.ToString()));
            MinGroupMoney = GroupMins.Sum(r => r.ItemPrice);
            foreach (var customerItemGroup in dto.ItemGroups.Where(r => r.IsAddMinus != (int)AddMinusType.Minus))
            {
                if (dto.PayMoney == 0)
                {
                    ZKL = customerItemGroup.DiscountRate;
                }

                var minGroupZKL = decimal.Parse(customerItemGroup.MaxDiscount.ToString());
                if (minGroupZKL > ZKL)
                {
                    customerItemGroup.DiscountRate = minGroupZKL;
                    customerItemGroup.PriceAfterDis = customerItemGroup.ItemPrice * minGroupZKL;
                    customerItemGroup.PriceAfterDis = Math.Round(customerItemGroup.PriceAfterDis, 2);
                }
                else
                {
                    customerItemGroup.DiscountRate = ZKL;
                    customerItemGroup.PriceAfterDis = customerItemGroup.ItemPrice * ZKL;
                    customerItemGroup.PriceAfterDis = Math.Round(customerItemGroup.PriceAfterDis, 2);
                }
            }

            if (MinGroupMoney - dto.minMoney > 0)
            {
                dto.ItemGroups = MinCusGroupMoney(new SeachChargrDto
                {
                    ItemGroups = dto.ItemGroups,
                    user = dto.user,
                    PayMoney = dto.PayMoney,
                    minMoney = MinGroupMoney,
                    minDiscountMoney = MinGroupZKMoney
                });
            }

            var mj = dto.ItemGroups.Sum(r => r.PriceAfterDis);
            return (List<GroupMoneyDto>)dto.ItemGroups;
        }

        /// <summary>
        /// 获取组合最低折扣率
        /// </summary>
        /// <returns></returns>
        public decimal MinGroupZKL(SeachChargrDto dto)
        {
            var userMaxDis = string.IsNullOrWhiteSpace(dto.user.Discount) ? 0 : decimal.Parse(dto.user.Discount);
            if (dto.ItemGroups == null)
            {
                if (dto.ZKL < userMaxDis)
                {
                    dto.ZKL = userMaxDis;
                }

                return dto.ZKL;
            }

            var GroupMaxDis = dto.ItemGroups.FirstOrDefault().MaxDiscount.Value;
            if (dto.ZKL < GroupMaxDis)
            {
                dto.ZKL = GroupMaxDis;
            }

            return dto.ZKL;
        }

        /// <summary>
        /// 输入折扣率,根据用户最大折扣率和组合最大折扣率计算价格
        /// </summary>
        /// <returns></returns>
        public List<GroupMoneyDto> MinlstGroupZKL(SeachChargrDto dto)
        {
            var lstgroupMoneyDtos = new List<GroupMoneyDto>();
            foreach (var item in dto.ItemGroups)
            {
                //获取最大折扣率
                var GroupMaxDis = item.MaxDiscount.Value;
                var userMaxDis = decimal.Parse(dto.user.Discount);
                if (dto.ZKL < GroupMaxDis)
                {
                    dto.ZKL = GroupMaxDis;
                }

                if (dto.ZKL < userMaxDis)
                {
                    dto.ZKL = userMaxDis;
                }

                if (item.IsAddMinus != 1 && item.IsAddMinus != 2)
                {
                    dto.ZKL = item.DiscountRate;
                }

                var groupMoneyDto = new GroupMoneyDto
                {
                    IsAddMinus = item.IsAddMinus,
                    ItemPrice = item.ItemPrice,
                    MaxDiscount = dto.ZKL,
                    PriceAfterDis = item.ItemPrice * dto.ZKL,
                    DiscountRate = dto.ZKL
                };
                lstgroupMoneyDtos.Add(groupMoneyDto);
            }

            return lstgroupMoneyDtos;
        }

        /// <summary>
        /// 获取个人收费列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public ICollection<ChargeCusInfoDto> ChargeCusInfo(ChargQueryCusDto input)
        {
            var query = _customerRegRepository.GetAllIncluding(m => m.Customer, m => m.McusPayMoney, m => m.ClientInfo);
            if (input != null)
            {
                //体检号
                if (!string.IsNullOrEmpty(input.CustomerBM))
                {
                    query = query.Where(m =>
                        m.CustomerBM == input.CustomerBM || m.Customer.Name == input.CustomerBM ||
                        m.Customer.IDCardNo == input.CustomerBM);
                }

                //体检日期
                if (input.NavigationStartTime != null && input.NavigationEndTime != null)
                {
                    input.NavigationStartTime = input.NavigationStartTime.Value.Date;
                    input.NavigationEndTime = input.NavigationEndTime.Value.Date.AddDays(1);
                    query = query.Where(o => (
                        o.LoginDate >= input.NavigationStartTime
                        && o.LoginDate < input.NavigationEndTime) ||
                        (o.BookingDate >= input.NavigationStartTime
                        && o.BookingDate < input.NavigationEndTime));
                }
                if (input.RegisterState.HasValue)
                {
                    query = query.Where(m => m.RegisterState == input.RegisterState.Value);
                }
            }

            var paycart = (int)PayerCatType.NoCharge;
            if (input.CostState == 1)
            {
                int minus = (int)AddMinusType.Minus;
                int adjustminus = (int)AddMinusType.AdjustMinus;
                query = query.Where(m =>
                    (m.McusPayMoney.PersonalMoney > m.McusPayMoney.PersonalPayMoney &&
                    m.PersonnelCategory.IsFree != true) || (m.PersonnelCategory.IsFree == true && m.CostState == 1) ||
                    m.CustomerItemGroup.Where(n => n.PayerCat == paycart && n.IsAddMinus != minus && n.IsAddMinus != adjustminus).Count() > 0);
            }
            else if (input.CostState == 2)
            {
                query = query.Where(m =>
                    (m.McusPayMoney.PersonalMoney <= m.McusPayMoney.PersonalPayMoney &&
                    m.PersonnelCategory.IsFree != true) || (m.PersonnelCategory.IsFree == true && m.CostState == 2));
            }
            var userBM = _userRepository.Get(AbpSession.UserId.Value);
            if (userBM != null && userBM.HospitalArea.HasValue && userBM.HospitalArea != 999)
            {
                query = query.Where(p => p.HospitalArea == userBM.HospitalArea
                || p.HospitalArea == null);
            }

            var paycartNo = (int)PayerCatType.ClientCharge;
            query = query.Where(r =>
                r.McusPayMoney.PersonalMoney > 0 ||
                r.CustomerItemGroup.Where(n => n.PayerCat != paycartNo).Count() > 0);
            var row = query.OrderByDescending(o => o.CreationTime);
            var chargeCusInfoList = row.MapTo<List<ChargeCusInfoDto>>();
            //chargeCusInfoList.Where
            return chargeCusInfoList;
        }

        /// <summary>
        /// 获取个人收费信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public ChargeInfoDto Get(ChargeBM input)
        {
            //GetAllIncluding(m => m.Customer, m => m.McusPayMoney, m => m.ClientInfo);
            // 加减状态 1为正常项目2为加项3为减项4调项减5调项加
            // var database = _BasicDictionaries.GetAll().Where(o => o.Type == "ChargeCategory").Select(o => new { o.OrderNum, o.Value });
            var chargedetaile = new ChargeInfoDto();
            var querey = _customerRegRepository.GetAllIncluding(m => m.MReceiptInfo, m => m.CustomerItemGroup);
            var row = querey.FirstOrDefault(o => o.CustomerBM == input.Name.ToString());
            var CustomerReg = row.MapTo<ChargeCusRegDto>();
            var ItemSuit = CustomerReg.ItemSuitBM;
            var CustomerItemGroup = CustomerReg.CustomerItemGroup.OrderBy(o => o.DepartmentName)
                .ThenBy(o => o.DepartmentOrder).ThenBy(o => o.ItemGroupOrder);
            var MReceiptInfo = CustomerReg.MReceiptInfo;
            var mGroupsMoney = CustomerItemGroup.Where(r => r.IsAddMinus != 3).Sum(r => r.GRmoney);
            var mAddGroupMoney = CustomerItemGroup.Where(r => r.IsAddMinus == 2).Sum(r => r.GRmoney);
            var mSubtractMoney = CustomerItemGroup.Where(r => r.IsAddMinus == 3).Sum(r => r.GRmoney);
            var mAdjustmentMoney = mAddGroupMoney - mSubtractMoney;
            var mItemSuitMoney = (decimal)0.00;
            if (ItemSuit != null && ItemSuit.CjPrice != null)
            {
                mItemSuitMoney = decimal.Parse(ItemSuit.CjPrice.ToString());
            }

            var mReceivableMoney = CustomerItemGroup.Where(r => r.IsAddMinus != 3).Sum(r => r.GRmoney);
            var mCollectedMoney = MReceiptInfo.Sum(r => r.Actualmoney);
            chargedetaile.Id = CustomerReg.Id;
            chargedetaile.PersonnelCategoryId = row.PersonnelCategoryId;
            chargedetaile.PersonnelCategory = row.PersonnelCategory.MapTo<PersonnelCategoryDto>();
            chargedetaile.GroupsMoney = mGroupsMoney;
            chargedetaile.AddGroupMoney = mAddGroupMoney;
            chargedetaile.SubtractMoney = mSubtractMoney;
            chargedetaile.AdjustmentMoney = mAdjustmentMoney;
            chargedetaile.ItemSuit = ItemSuit;
            chargedetaile.ReceivableMoney = mReceivableMoney;
            chargedetaile.CollectedMoney = mCollectedMoney;
            chargedetaile.SurplusMoney = mReceivableMoney - mCollectedMoney;
            chargedetaile.Discontmoney = MReceiptInfo.Sum(r => r.Discontmoney);
            var grouplist = CustomerItemGroup.MapTo<List<ChargeGroupsDto>>();

            for (int i = 0; i < grouplist.Count(); i++)
            {
                if (grouplist[i].SFType.HasValue)
                {
                    int num = grouplist[i].SFType.Value;
                    grouplist[i].SfTypeOrder = _BasicDictionaries.GetAll().FirstOrDefault(o => o.Type == "ChargeCategory" && o.Value == num)?.OrderNum ?? 0;
                }
            }
            chargedetaile.ChargeGroups = grouplist;
            return chargedetaile;
        }

        /// <summary>
        /// 获取个人收费组合信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public List<ChargeGroupsDto> GetCusGroups(ChargeBM input)
        {
            var chargedetaile = new List<ChargeGroupsDto>();
            var querey = _customerRegRepository.GetAllIncluding(m => m.CustomerItemGroup);
            var row = querey.FirstOrDefault(o => o.CustomerBM == input.Name.ToString());
            var CustomerReg = row.MapTo<ChargeCusRegDto>();
            var CustomerItemGroup = CustomerReg?.CustomerItemGroup.OrderBy(o => o.DepartmentName)
                .ThenBy(o => o.DepartmentOrder).ThenBy(o => o.ItemGroupOrder);
            chargedetaile = CustomerItemGroup.MapTo<List<ChargeGroupsDto>>();

            return chargedetaile;
        }

        /// <summary>
        /// 获取支付方式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ChargeTypeDto> ChargeType(ChargeBM input)
        {
            // return _MChargeType.GetAll().MapTo<List<ChargeTypeDto>>();
            var query = _chargeTypeRepository.GetAll().AsNoTracking();
            if (int.TryParse(input.Name, out var c))
            {
                query = query.Where(r => r.ChargeApply == c || r.ChargeApply == 3);
            }

            query = query.OrderBy(r => r.OrderNum);
            return query.MapTo<List<ChargeTypeDto>>();

            //var result = _chargeTypeRepository.GetAllList()
            //    .Where(r => r.ChargeApply == int.Parse(input.Name.ToString()) || r.ChargeApply == 3)
            //    .OrderBy(r => r.OrderNum);
            //return result.MapTo<List<ChargeTypeDto>>();
        }

        /// <summary>
        /// 根据ID获取支付方式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ChargeTypeDto ChargeTypeByID(EntityDto<Guid> input)
        {
            var result = _chargeTypeRepository.Get(input.Id);
            return result.MapTo<ChargeTypeDto>();
        }

        /// <summary>
        /// 插入收费记录相关
        /// </summary>
        /// <param name="input"></param>
        public Guid InsertReceiptInfoDto(CreateReceiptInfoDto input)
        {


            var user = _userRepository.Get(AbpSession.UserId.Value);
            var Receipt = input.MapTo<TjlMReceiptInfo>();
            Receipt.Id = Guid.NewGuid();
            Receipt.User = user;
            CreateOpLogDto createOpLogDto = new CreateOpLogDto();
            string sflt = "";
            if (input.CustomerRegid != null)
            {
                var tjlCustomerReg = _customerRegRepository.Get(input.CustomerRegid.Value);
                Receipt.CustomerRegId = input.CustomerRegid;
                Receipt.CustomerId = tjlCustomerReg.CustomerId;

                createOpLogDto.LogBM = tjlCustomerReg.CustomerBM;
                createOpLogDto.LogName = tjlCustomerReg.Customer.Name;
                sflt = "个人收费";

                // Receipt.CustomerReg = _jlCustomerReg.FirstOrDefault(r => r.Id == input.CustomerRegid).MapTo<TjlCustomerReg>();
            }
            else
            {
                var clientlsb = _clientRegRepository.Get(input.ClientRegid.Value);
                Receipt.ClientName = clientlsb.ClientInfo.ClientName;
                Receipt.ClientRegId = input.ClientRegid;

                createOpLogDto.LogBM = clientlsb.ClientInfo.ClientBM;
                createOpLogDto.LogName = clientlsb.ClientInfo.ClientName;
                sflt = "团体收费";
            }


            var MReceiptInfo = _receiptInfoRepository.Insert(Receipt);
            // CurrentUnitOfWork.SaveChanges();
            foreach (var Payment in input.MPaymentr)
            {
                var Payt = Payment.MapTo<TjlMPayment>();
                Payt.Id = Guid.NewGuid();
                var ChargeType = _chargeTypeRepository.FirstOrDefault(r => r.Id == Payment.MChargeTypeId)
                    .MapTo<TbmMChargeType>();
                Payt.MChargeType = ChargeType;
                Payt.MChargeTypename = ChargeType.ChargeName;
                Payt.MReceiptInfo = MReceiptInfo;
                var mPayment = _paymentRepository.Insert(Payt);
            }

            if (input.MReceiptInfoDetailedgr != null && input.MReceiptInfoDetailedgr.Count > 0)
            {
                foreach (var MReceiptInfoDetailed in input.MReceiptInfoDetailedgr)
                {
                    var ReceiptDetail = MReceiptInfoDetailed.MapTo<TjlMReceiptDetails>();
                    ReceiptDetail.Id = Guid.NewGuid();
                    var tjlCustomerItemGroup = _customerItemGroupRepository.Get(MReceiptInfoDetailed.ItemGroupId);
                    ReceiptDetail.ItemGroupId = tjlCustomerItemGroup.ItemGroupBM_Id.Value;
                    ReceiptDetail.ItemGroupName = tjlCustomerItemGroup.ItemGroupName;
                    ReceiptDetail.ItemGroupOrder = tjlCustomerItemGroup.ItemGroupOrder;
                    ReceiptDetail.ItemGroupCodeBM = tjlCustomerItemGroup.ItemGroupCodeBM;
                    ReceiptDetail.DepartmentId = tjlCustomerItemGroup.DepartmentId;
                    ReceiptDetail.DepartmentName = tjlCustomerItemGroup.DepartmentName;
                    ReceiptDetail.DepartmentOrder = tjlCustomerItemGroup.DepartmentOrder;
                    ReceiptDetail.DepartmentCodeBM = tjlCustomerItemGroup.DepartmentCodeBM;
                    ReceiptDetail.PayerCat = tjlCustomerItemGroup.PayerCat;
                    ReceiptDetail.IsAddMinus = tjlCustomerItemGroup.IsAddMinus;
                    ReceiptDetail.GroupsMoney = MReceiptInfoDetailed.GroupsMoney;
                    ReceiptDetail.Discount = MReceiptInfoDetailed.Discount;
                    ReceiptDetail.GroupsDiscountMoney = MReceiptInfoDetailed.GroupsDiscountMoney;
                    ReceiptDetail.MReceiptInfo = MReceiptInfo;
                    _receiptDetailsRepository.Insert(ReceiptDetail);
                }
            }
            //添加日志           

            createOpLogDto.LogText = sflt + "金额：" + input.Actualmoney;
            createOpLogDto.LogType = (int)LogsTypes.ChargId;
            var palist = input.MPaymentr.Select(o => o.MChargeTypename + "：" + o.Actualmoney).ToList();
            //  string xmmx = "";
            //if (input.MReceiptInfoDetailedgr != null)
            //{
            //    var groups = input.MReceiptInfoDetailedgr?.Select(o => o.ItemGroupName).ToList();
            //   // xmmx = "项目明细：" + string.Join(",", groups);
            //}

            createOpLogDto.LogDetail = string.Join(";", palist);
            _commonAppService.SaveOpLog(createOpLogDto);
            return Receipt.Id;
        }

        /// <summary>
        /// 插入收费记录相关完整版
        /// </summary>
        /// <param name="input"></param>
        public OutErrDto InsertReceiptState(CreateReceiptInfoDto input)
        {

            OutErrDto outErrDto = new OutErrDto();
            TjlCustomerReg tjlCustomerinfo = new TjlCustomerReg();
            var user = _userRepository.Get(AbpSession.UserId.Value);
            CreateOpLogDto createOpLogDto = new CreateOpLogDto();
            string sflb = "";
            var Receipt = input.MapTo<TjlMReceiptInfo>();
            Receipt.Id = Guid.NewGuid();
            Receipt.UserId = AbpSession.UserId.Value;
            Receipt.User = user;
            if (!string.IsNullOrEmpty(input.auth_code))
            {
                var basic = _BasicDictionaries.GetAll().Where(p => p.Type == "WebPay").ToList();
                var webOpen = basic.FirstOrDefault(p => p.Value == 1)?.Remarks;
                if (webOpen != null && webOpen == "1")
                {
                    WebChargeInputDto webInput = new WebChargeInputDto();
                    if (input.CustomerRegid.HasValue)
                    {
                        webInput.CustomerRegBMId = input.CustomerRegid.Value;
                    }
                    webInput.auth_code = input.auth_code;
                    webInput.timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    webInput.total_amount = input.Actualmoney.ToString("0.00");
                    webInput.out_trade_no = Receipt.Id.ToString();
                    var webErr = WebCharge(webInput);
                    if (webErr.code == "1")
                    {
                        Receipt.merchant_id = webErr.merchant_id;
                        Receipt.pay_order_id = webErr.pay_order_id;
                    }
                    else
                    {
                        outErrDto.code = "0";
                        outErrDto.cardInfo = webErr.cardInfo;
                        return outErrDto;
                    }
                }

            }
            if (input.CustomerRegid != null)
            {
                var tjlCustomerReg = _customerRegRepository.Get(input.CustomerRegid.Value);
                Receipt.CustomerRegId = input.CustomerRegid;
                Receipt.CustomerId = tjlCustomerReg.CustomerId;
                tjlCustomerinfo = tjlCustomerReg;
                createOpLogDto.LogBM = tjlCustomerReg.CustomerBM;
                createOpLogDto.LogName = tjlCustomerReg.Customer.Name;
                sflb = "个人收费";
                // Receipt.CustomerReg = _jlCustomerReg.FirstOrDefault(r => r.Id == input.CustomerRegid).MapTo<TjlCustomerReg>();
            }
            else
            {
                var clientls = _clientRegRepository.Get(input.ClientRegid.Value).ClientInfo;
                Receipt.ClientName = clientls.ClientName;
                Receipt.ClientRegId = input.ClientRegid;
                createOpLogDto.LogBM = clientls.ClientBM;
                createOpLogDto.LogName = clientls.ClientName;
                sflb = "团体收费";
            }
            #region 优康会员卡接口 
            var ykpayment = input.MPaymentr.FirstOrDefault(o => o.MChargeTypename == "会员卡");
            if (ykpayment != null)
            {
                decimal YFJE = input.Actualmoney;
                if (ykpayment.CardNum == "")
                {
                    outErrDto.code = "0";
                    outErrDto.err = "使用会员卡时，卡号不能为空！";
                    return outErrDto;
                }

                ChargeBM inCarNumDto = new ChargeBM();
                inCarNumDto.Name = ykpayment.CardNum;
                var strhtm = GetNYHCardByNum(inCarNumDto);
                if (strhtm.code == 0)
                {
                    outErrDto.code = "0";
                    outErrDto.err = strhtm.err;
                    outErrDto.cardInfo = "卡号：" + strhtm.CardNo + ",类别：" + strhtm.CategoryName + ",卡余额：" + strhtm.Amount + "";
                    return outErrDto;
                }
                try
                {


                    string ja1a = strhtm.ArchivesNum;
                    decimal hyzk = strhtm.Amount;

                    if (hyzk < YFJE)
                    {
                        // MessageBox.Show("会员卡余额不足，请充值！");
                        outErrDto.code = "0";
                        outErrDto.err = "会员卡余额不足，请充值！";
                        outErrDto.cardInfo = "卡号：" + strhtm.CardNo + ",类别：" + strhtm.CategoryName + ",卡余额：" + strhtm.Amount + "";
                        return outErrDto;
                    }
                    else
                    {


                        ChargCardDto cusinput = new ChargCardDto();
                        cusinput.Amount = ykpayment.Actualmoney;
                        cusinput.CardNo = ykpayment.CardNum;
                        cusinput.CheckItemCount = input.MReceiptInfoDetailedgr.Count;
                        if (input.CustomerRegid != null)
                        {
                            cusinput.ArchivesNum = tjlCustomerinfo.CustomerBM;

                            cusinput.SuitName = tjlCustomerinfo.ItemSuitName ?? "";

                        }
                        var ykstrh = NYHChargeCard(cusinput);
                        if (ykstrh.code == 0)
                        {
                            outErrDto.code = "0";
                            outErrDto.err = ykstrh.Mess;
                            outErrDto.cardInfo = "卡号：" + strhtm.CardNo + ",类别：" + strhtm.CategoryName + ",卡余额：" + strhtm.Amount + "";
                            return outErrDto;
                        }
                        ykpayment.Discount = 1;
                        outErrDto.cardInfo = "卡号：" + strhtm.CardNo + ",类别：" + strhtm.CategoryName + ",卡余额：" + ykstrh.Amount + "";

                    }
                }
                catch
                {
                    outErrDto.code = "0";
                    outErrDto.err = "该会员卡不存在，请核实！";
                    return outErrDto;
                }
            }
            #endregion

            #region 单位储值卡扣款 
            var jjkpayment = input.MPaymentr.FirstOrDefault(o => o.MChargeTypename == "单位储值卡");
            if (jjkpayment != null)
            {

                if (jjkpayment.CardNum == "")
                {
                    outErrDto.code = "0";
                    outErrDto.err = "使用单位储值卡时，卡号不能为空！";
                    return outErrDto;
                }


                try
                {
                    var _tbmCardinfo = _TbmCard.GetAll().FirstOrDefault(p => p.CardNo ==
                   jjkpayment.CardNum);
                    if (_tbmCardinfo == null)
                    {
                        outErrDto.code = "0";
                        outErrDto.err = "无此卡信息！";
                        return outErrDto;
                    }
                    if (!_tbmCardinfo.CardCategory.Contains("储值卡"))
                    {
                        outErrDto.code = "0";
                        outErrDto.err = "无此非储值卡，不能在收费使用！";
                        return outErrDto;
                    }


                    if (_tbmCardinfo.FaceValue < jjkpayment.Actualmoney)
                    {
                        // MessageBox.Show("会员卡余额不足，请充值！");
                        outErrDto.code = "0";
                        outErrDto.err = "单位储值卡余额不足，请充值！";
                        outErrDto.cardInfo = "卡号：" + _tbmCardinfo.CardNo + ",类别：" + _tbmCardinfo.CardCategory + ",卡余额：" + _tbmCardinfo.FaceValue + "";
                        return outErrDto;
                    }
                    else
                    {
                        _tbmCardinfo.FaceValue = _tbmCardinfo.FaceValue - jjkpayment.Actualmoney;
                        _tbmCardinfo.CustomerRegId = Receipt.CustomerRegId;
                        _tbmCardinfo.UseTime = System.DateTime.Now;

                        _TbmCard.Update(_tbmCardinfo);

                        outErrDto.cardInfo = "卡号：" + _tbmCardinfo.CardNo + ",类别：" + _tbmCardinfo.CardCategory + ",卡余额：" + _tbmCardinfo.FaceValue + "";

                    }
                }
                catch
                {
                    outErrDto.code = "0";
                    outErrDto.err = "该会员卡不存在，请核实！";
                    return outErrDto;
                }
            }
            #endregion

            var MReceiptInfo = _receiptInfoRepository.Insert(Receipt);
            // CurrentUnitOfWork.SaveChanges();
            foreach (var Payment in input.MPaymentr)
            {
                var Payt = Payment.MapTo<TjlMPayment>();
                Payt.Id = Guid.NewGuid();
                var ChargeType = _chargeTypeRepository.FirstOrDefault(r => r.Id == Payment.MChargeTypeId)
                    .MapTo<TbmMChargeType>();
                Payt.MChargeType = ChargeType;
                Payt.MChargeTypename = ChargeType.ChargeName;
                Payt.MReceiptInfo = MReceiptInfo;
                var mPayment = _paymentRepository.Insert(Payt);
            }

            if (input.MReceiptInfoDetailedgr != null && input.MReceiptInfoDetailedgr.Count > 0)
            {
                foreach (var MReceiptInfoDetailed in input.MReceiptInfoDetailedgr)
                {
                    var ReceiptDetail = MReceiptInfoDetailed.MapTo<TjlMReceiptDetails>();
                    ReceiptDetail.Id = Guid.NewGuid();
                    var tjlCustomerItemGroup = _customerItemGroupRepository.Get(MReceiptInfoDetailed.ItemGroupId);
                    ReceiptDetail.ItemGroupId = tjlCustomerItemGroup.ItemGroupBM_Id.Value;
                    ReceiptDetail.ItemGroupName = tjlCustomerItemGroup.ItemGroupName;
                    ReceiptDetail.ItemGroupOrder = tjlCustomerItemGroup.ItemGroupOrder;
                    ReceiptDetail.ItemGroupCodeBM = tjlCustomerItemGroup.ItemGroupCodeBM;
                    ReceiptDetail.DepartmentId = tjlCustomerItemGroup.DepartmentId;
                    ReceiptDetail.DepartmentName = tjlCustomerItemGroup.DepartmentName;
                    ReceiptDetail.DepartmentOrder = tjlCustomerItemGroup.DepartmentOrder;
                    ReceiptDetail.DepartmentCodeBM = tjlCustomerItemGroup.DepartmentCodeBM;
                    ReceiptDetail.PayerCat = tjlCustomerItemGroup.PayerCat;
                    ReceiptDetail.IsAddMinus = tjlCustomerItemGroup.IsAddMinus;
                    ReceiptDetail.GroupsMoney = MReceiptInfoDetailed.GroupsMoney;
                    ReceiptDetail.Discount = MReceiptInfoDetailed.Discount;
                    ReceiptDetail.GroupsDiscountMoney = MReceiptInfoDetailed.GroupsDiscountMoney;
                    ReceiptDetail.MReceiptInfo = MReceiptInfo;
                    _receiptDetailsRepository.Insert(ReceiptDetail);
                    //更新组合收费状态
                    var payercat = (int)PayerCatType.PersonalCharge;
                    var refundstate = (int)PayerCatType.NotRefund;
                    tjlCustomerItemGroup.MReceiptInfoPersonalId = MReceiptInfo.Id;
                    tjlCustomerItemGroup.MReceiptInfoPersonal = MReceiptInfo;
                    tjlCustomerItemGroup.PayerCat = payercat;
                    tjlCustomerItemGroup.RefundState = refundstate;
                    _customerItemGroupRepository.Update(tjlCustomerItemGroup);
                }
            }
            //更新体检状态
            if (tjlCustomerinfo != null && tjlCustomerinfo.Id != null)
            {
                var coststate = (int)PayerCatType.Charge;
                tjlCustomerinfo.CostState = coststate;
                _customerRegRepository.Update(tjlCustomerinfo);
            }
            //保存抹零项
            if (MReceiptInfo.Discontmoney != 0)
            {
                SearchMLGroupDto searchMLGroupDto = new SearchMLGroupDto();
                searchMLGroupDto.CustomerRegID = tjlCustomerinfo.Id;
                searchMLGroupDto.MReceiptInfoPersonalId = MReceiptInfo.Id;
                searchMLGroupDto.MLMoney = -MReceiptInfo.Discontmoney;
                InsertMLGroup(searchMLGroupDto);
            }

            SearchPayMoneyDto inputcs = new SearchPayMoneyDto();
            inputcs.Id = tjlCustomerinfo.Id;
            inputcs.PayMoney = MReceiptInfo.Actualmoney;
            inputcs.DistMoney = -MReceiptInfo.Discontmoney;
            UpCusMoney(inputcs);
            outErrDto.code = "1";
            outErrDto.Id = MReceiptInfo.Id;
            //CurrentUnitOfWork.SaveChanges();
            //添加日志            
            createOpLogDto.LogText = "个人收费，金额：" + input.Actualmoney;
            createOpLogDto.LogType = (int)LogsTypes.ChargId;
            var palist = input.MPaymentr.Select(o => o.MChargeTypename + "：" + o.Actualmoney).ToList();

            createOpLogDto.LogDetail = string.Join(";", palist);
            _commonAppService.SaveOpLog(createOpLogDto);
            return outErrDto;
        }
        /// <summary>
        /// 电子发票
        /// </summary>
        public void DZPF(DZFPInputDto input)
        {
            #region 电子发票接口
            var bis = _BasicDictionaries.GetAll().Where(p => p.Type == "DZFP").ToList();
            var fpisop = bis.FirstOrDefault(p => p.Value == 1)?.Remarks;
            if (fpisop == "1")
            {
                var fpComName = bis.FirstOrDefault(p => p.Value == 2)?.Remarks;
                if (!string.IsNullOrEmpty(fpComName) && fpComName == "航天信息")
                {
                    try
                    {

                        //正常开票：销方名称：常州九洲金东方医院有限公司，销方税号：91320412MA1MBTW87E，终端号：1，分机号1，发票种类：51（电子票），开票类型：0（蓝票），蓝票代码为空，蓝票号码为空，含税标志：0，税率：0
                        //销方名称  常州九洲金东方医院有限公司
                        var xfmc = bis.FirstOrDefault(p => p.Value == 3)?.Remarks;
                        //销方税号 91320412MA1MBTW87E
                        var xfsh = bis.FirstOrDefault(p => p.Value == 4)?.Remarks;
                        //销方开户行及银行账号  中国农业银行股份有限公司常州湖塘支行 10602301040017703
                        var xfyhzh = bis.FirstOrDefault(p => p.Value == 5)?.Remarks;
                        //销方地址电话 常州市武进区湖滨南路99号 0519-81089121
                        var xfdzdh = bis.FirstOrDefault(p => p.Value == 6)?.Remarks;
                        DZFPMain dZFPMain = new DZFPMain();
                        dZFPMain.djrq = input.ChargeDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        dZFPMain.bz = "";
                        var sfy = input.UserName;
                        dZFPMain.fhr = sfy;
                        dZFPMain.fjh = "1";
                        dZFPMain.fpzl = "51";
                        dZFPMain.gfdzdh = "";
                        dZFPMain.gfmc = input.Name;
                        dZFPMain.gfsh = "";
                        dZFPMain.gfsj = input.Mobile;
                        dZFPMain.gfyhzh = "";
                        dZFPMain.gfyx = "";
                        dZFPMain.hjje = input.Actualmoney.ToString("0.00");
                        dZFPMain.hjse = "0";
                        dZFPMain.jshj = input.Actualmoney.ToString("0.00");
                        dZFPMain.kplx = "0";
                        dZFPMain.kpr = sfy;
                        dZFPMain.lzfpdm = "";
                        dZFPMain.lzfphm = "";
                        dZFPMain.qdbz = "0";
                        dZFPMain.skr = sfy;
                        dZFPMain.xfdzdh = xfdzdh;
                        dZFPMain.xfmc = xfmc;
                        dZFPMain.xfsh = xfsh;
                        dZFPMain.xfyhzh = xfyhzh;
                        dZFPMain.xsdjbh = input.TjlMReceiptId.ToString();
                        dZFPMain.zdh = "1";
                        dZFPMain.listOrder = new List<DZFPDetail>();
                        DZFPDetail dZFPDetail = new DZFPDetail();
                        dZFPDetail.dj = "";
                        dZFPDetail.flbm = "3070202";
                        dZFPDetail.fphxz = "0";
                        dZFPDetail.ggxh = "";
                        dZFPDetail.hsjbz = "0";
                        dZFPDetail.je = input.Actualmoney.ToString("0.00");
                        dZFPDetail.jldw = "";
                        dZFPDetail.kce = "";
                        dZFPDetail.lslvbs = "1";
                        dZFPDetail.se = "0";
                        dZFPDetail.sl = "";
                        dZFPDetail.slv = "0";
                        dZFPDetail.spmc = "体检费";
                        dZFPDetail.yhbz = "1";
                        dZFPDetail.yhsm = "免税";
                        dZFPDetail.zxbm = "";
                        dZFPMain.listOrder.Add(dZFPDetail);
                        var json = JsonConvert.SerializeObject(dZFPMain);
                        DZFP.SalesWebService salesWebService = new DZFP.SalesWebService();
                        File.AppendAllText(Path.Combine(Variables.LogDirectory, $"DZFP-{DateTime.Now:yyyyMMdd}.txt"), json);
                        var outresult = salesWebService.invoiceKJ(json);
                        File.AppendAllText(Path.Combine(Variables.LogDirectory, $"OUTDZFP-{DateTime.Now:yyyyMMdd}.txt"), outresult);

                        var userViewDto = JsonConvert.DeserializeObject<DZFPDout>(outresult);
                        if (userViewDto != null)
                        {
                            if (userViewDto.returnCode == "0000")
                            {
                                //发票返回信息存入收费表
                                var reseipt = _receiptInfoRepository.Get(input.TjlMReceiptId.Value);
                                reseipt.returnCode = userViewDto.returnCode;
                                reseipt.returnMsg = userViewDto.returnMsg;
                                reseipt.xsdjbh = userViewDto.xsdjbh;
                                reseipt.hjje = userViewDto.hjje;
                                reseipt.hjse = userViewDto.hjse;
                                reseipt.kprq = userViewDto.kprq;
                                reseipt.ssyf = userViewDto.ssyf;
                                reseipt.fpdm = userViewDto.fpdm;
                                reseipt.fphm = userViewDto.fphm;
                                reseipt.qdbz = userViewDto.qdbz;
                                reseipt.mw = userViewDto.mw;
                                reseipt.jym = userViewDto.jym;
                                reseipt.qmz = userViewDto.qmz;
                                reseipt.ewm = userViewDto.ewm;
                                reseipt.jqbh = userViewDto.jqbh;
                                _receiptInfoRepository.Update(reseipt);
                            }
                            else
                            {
                                //发票返回信息存入收费表
                                var reseipt = _receiptInfoRepository.Get(input.TjlMReceiptId.Value);
                                reseipt.returnCode = userViewDto.returnCode;
                                reseipt.returnMsg = userViewDto.returnMsg;
                                _receiptInfoRepository.Update(reseipt);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            #endregion
        }
        /// <summary>
        /// 红冲电子发票
        /// </summary>
        public void HCDZPF(TjlMReceiptInfo input, Guid nowId)
        {

            #region 电子发票接口
            var bis = _BasicDictionaries.GetAll().Where(p => p.Type == "DZFP").ToList();
            var fpisop = bis.FirstOrDefault(p => p.Value == 1)?.Remarks;
            if (fpisop == "1")
            {
                var fpComName = bis.FirstOrDefault(p => p.Value == 2)?.Remarks;
                if (!string.IsNullOrEmpty(fpComName) && fpComName == "航天信息")
                {
                    try
                    {

                        //正常开票：销方名称：常州九洲金东方医院有限公司，销方税号：91320412MA1MBTW87E，终端号：1，分机号1，发票种类：51（电子票），开票类型：0（蓝票），蓝票代码为空，蓝票号码为空，含税标志：0，税率：0
                        //销方名称  常州九洲金东方医院有限公司
                        var xfmc = bis.FirstOrDefault(p => p.Value == 3)?.Remarks;
                        //销方税号 91320412MA1MBTW87E
                        var xfsh = bis.FirstOrDefault(p => p.Value == 4)?.Remarks;
                        //销方开户行及银行账号  中国农业银行股份有限公司常州湖塘支行 10602301040017703
                        var xfyhzh = bis.FirstOrDefault(p => p.Value == 5)?.Remarks;
                        //销方地址电话 常州市武进区湖滨南路99号 0519-81089121
                        var xfdzdh = bis.FirstOrDefault(p => p.Value == 6)?.Remarks;
                        DZFPMain dZFPMain = new DZFPMain();
                        dZFPMain.djrq = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        dZFPMain.bz = "";

                        var sfy = _userRepository.Get(input.UserId.Value)?.Name;
                        dZFPMain.fhr = sfy;
                        dZFPMain.fjh = "1";
                        dZFPMain.fpzl = "51";
                        dZFPMain.gfdzdh = "";
                        dZFPMain.gfmc = input.Customer?.Name;
                        dZFPMain.gfsh = "";
                        dZFPMain.gfsj = input.Customer?.Mobile;
                        dZFPMain.gfyhzh = "";
                        dZFPMain.gfyx = "";
                        dZFPMain.hjje = "-" + input.Actualmoney.ToString("0.00");
                        dZFPMain.hjse = "0";
                        dZFPMain.jshj = "-" + input.Actualmoney.ToString("0.00");
                        dZFPMain.kplx = "1";
                        dZFPMain.kpr = sfy;
                        dZFPMain.lzfpdm = input.fpdm;
                        dZFPMain.lzfphm = input.fphm;
                        dZFPMain.qdbz = "0";
                        dZFPMain.skr = sfy;
                        dZFPMain.xfdzdh = xfdzdh;
                        dZFPMain.xfmc = xfmc;
                        dZFPMain.xfsh = xfsh;
                        dZFPMain.xfyhzh = xfyhzh;
                        dZFPMain.xsdjbh = nowId.ToString();
                        dZFPMain.zdh = "1";
                        dZFPMain.listOrder = new List<DZFPDetail>();
                        DZFPDetail dZFPDetail = new DZFPDetail();
                        dZFPDetail.dj = "";
                        dZFPDetail.flbm = "3070202";
                        dZFPDetail.fphxz = "0";
                        dZFPDetail.ggxh = "";
                        dZFPDetail.hsjbz = "0";
                        dZFPDetail.je = "-" + input.Actualmoney.ToString("0.00");
                        dZFPDetail.jldw = "";
                        dZFPDetail.kce = "";
                        dZFPDetail.lslvbs = "1";
                        dZFPDetail.se = "0";
                        dZFPDetail.sl = "";
                        dZFPDetail.slv = "0";
                        dZFPDetail.spmc = "体检费";
                        dZFPDetail.yhbz = "1";
                        dZFPDetail.yhsm = "免税";
                        dZFPDetail.zxbm = "";
                        dZFPMain.listOrder.Add(dZFPDetail);
                        var json = JsonConvert.SerializeObject(dZFPMain);
                        DZFP.SalesWebService salesWebService = new DZFP.SalesWebService();
                        File.AppendAllText(Path.Combine(Variables.LogDirectory, $"DZFP-{DateTime.Now:yyyyMMdd}.txt"), json);
                        var outresult = salesWebService.invoiceKJ(json);
                        File.AppendAllText(Path.Combine(Variables.LogDirectory, $"OUTDZFP-{DateTime.Now:yyyyMMdd}.txt"), outresult);

                        var userViewDto = JsonConvert.DeserializeObject<DZFPDout>(outresult);
                        if (userViewDto != null)
                        {
                            if (userViewDto.returnCode == "0000")
                            {
                                //发票返回信息存入收费表
                                var reseipt = _receiptInfoRepository.Get(nowId);
                                reseipt.returnCode = userViewDto.returnCode;
                                reseipt.returnMsg = userViewDto.returnMsg;
                                reseipt.xsdjbh = userViewDto.xsdjbh;
                                reseipt.hjje = userViewDto.hjje;
                                reseipt.hjse = userViewDto.hjse;
                                reseipt.kprq = userViewDto.kprq;
                                reseipt.ssyf = userViewDto.ssyf;
                                reseipt.fpdm = userViewDto.fpdm;
                                reseipt.qdbz = userViewDto.qdbz;
                                reseipt.fphm = userViewDto.fphm;
                                reseipt.mw = userViewDto.mw;
                                reseipt.jym = userViewDto.jym;
                                reseipt.qmz = userViewDto.qmz;
                                reseipt.ewm = userViewDto.ewm;
                                reseipt.jqbh = userViewDto.jqbh;
                                _receiptInfoRepository.Update(reseipt);
                            }
                            else
                            {
                                //发票返回信息存入收费表
                                var reseipt = _receiptInfoRepository.Get(nowId);
                                reseipt.returnCode = userViewDto.returnCode;
                                reseipt.returnMsg = userViewDto.returnMsg;
                                _receiptInfoRepository.Update(reseipt);
                            }
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            #endregion
        }
        /// <summary>
        /// 插入团体收费记录相关
        /// </summary>
        /// <param name="input"></param>
        public void InsertClientReceiptInfoDto(CreateReceiptInfoDto input)
        {
            var user = _userRepository.Get(AbpSession.UserId.Value);
            var MReceiptInfo = _receiptInfoRepository.FirstOrDefault(r => r.ClientReg.Id == input.ClientRegid);
            if (MReceiptInfo == null)
            {
                var Receipt = input.MapTo<TjlMReceiptInfo>();
                Receipt.Id = Guid.NewGuid();
                Receipt.UserId = AbpSession.UserId.Value;
                var ClientReg = _clientRegRepository.FirstOrDefault(r => r.Id == input.ClientRegid).MapTo<TjlClientReg>();
                Receipt.ClientRegId = input.ClientRegid;
                Receipt.ClientName = ClientReg?.ClientInfo?.ClientName;
                Receipt.Actualmoney = decimal.Parse("0");
                Receipt.ChargeDate = DateTime.Now;
                Receipt.ChargeState = 1;
                Receipt.Discontmoney = 0;

                Receipt.DiscontReason = "";
                Receipt.Discount = 1;
                Receipt.ReceiptSate = 1; //和上面重复了ChargeState
                Receipt.Remarks = "";
                Receipt.SettlementSate = 2;
                Receipt.Shouldmoney = 0;
                Receipt.Summoney = 0;
                Receipt.TJType = 2;
                Receipt.CustomerRegId = null;
                MReceiptInfo = _receiptInfoRepository.Insert(Receipt);
            }

            foreach (var Payment in input.MPaymentr)
            {
                var Payt = Payment.MapTo<TjlMPayment>();
                Payt.Id = Guid.NewGuid();
                var ChargeType = _chargeTypeRepository.FirstOrDefault(r => r.Id == Payment.MChargeTypeId)
                    .MapTo<TbmMChargeType>();
                Payt.MChargeType = ChargeType;
                Payt.MChargeTypename = ChargeType.ChargeName;

                // Payt.MInvoiceRecordId = MInvoiceRecordinfo;
                var mPayment = _paymentRepository.Insert(Payt);
            }

            // CurrentUnitOfWork.SaveChanges();
        }

        /// <summary>
        /// 插入团体建账
        /// </summary>
        /// <param name="input"></param>
        public void InsertClientReceiptDto(CreateReceiptInfoDto input)
        {
            var user = _userRepository.Get(AbpSession.UserId.Value);
            var Receipt = input.MapTo<TjlMReceiptInfo>();
            Receipt.Id = Guid.NewGuid();
            Receipt.User = user;
            Receipt.UserId = AbpSession.UserId.Value;
            Receipt.ClientReg = _clientRegRepository.FirstOrDefault(r => r.Id == input.ClientRegid).MapTo<TjlClientReg>();
            Receipt.ClientRegId = input.ClientRegid;
            Receipt.ClientName = Receipt.ClientReg?.ClientInfo?.ClientName;
            var MReceiptInfo = _receiptInfoRepository.Insert(Receipt);
        }

        /// <summary>
        /// 获取单位收费人员列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public List<ClientPayCusLisViewDto> GetClientCusList(EntityDto<Guid> input)
        {
            var query = _customerRegRepository.GetAllIncluding(m => m.CustomerItemGroup, m => m.Customer, m => m.McusPayMoney, m => m.ClientTeamInfo).AsNoTracking();
            query = query.Where(r => r.ClientRegId == input.Id);
            query = query.OrderBy(r => r.ClientTeamInfo.TeamName);
            return query.MapTo<List<ClientPayCusLisViewDto>>();
        }

        /// <summary>
        /// 获取单位分组金额列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public List<ChargeTeamMoneyViewDto> GetClientTeamList(EntityDto<Guid> input)
        {
            var qure = _clientTeamInfoRepository.GetAllIncluding(m => m.CustomerReg);
            qure = qure.Where(r => r.ClientRegId == input.Id);
            qure = qure.OrderBy(r => r.TeamName);
            return qure.MapTo<List<ChargeTeamMoneyViewDto>>();
        }

        public MReceiptOrCustomerViewDto GetReceiptViewDto(EntityDto<Guid> input)
        {
            return _receiptInfoRepository.FirstOrDefault(o => o.Id == input.Id).MapTo<MReceiptOrCustomerViewDto>();
        }

        public List<MReceiptInfoPerViewDto> GetReceiptOrCustomer(EntityDto<Guid> input)
        {
            return _receiptInfoRepository.GetAll().Where(o => o.CustomerRegId == input.Id && o.ReceiptSate != 2)
                .MapTo<List<MReceiptInfoPerViewDto>>();
        }

        /// <summary>
        /// 获取单位已收费用
        /// </summary>
        /// <param name="bM"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public List<MReceiptClientDto> MInvoiceRecorView(EntityDto<Guid> bM)
        {
            var qure = _receiptInfoRepository.GetAllIncluding(r => r.MPayment, r => r.User, r => r.MInvoiceRecord);
            qure = qure.Where(r => r.ClientRegId == bM.Id);
            return qure.MapTo<List<MReceiptClientDto>>();
        }

        /// <summary>
        /// 获取收费发票信息
        /// </summary>
        /// <param name="searchInvoice"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public List<MReceiptInfoDto> GetInvalidReceipt(SearchInvoiceDto searchInvoice)
        {
            var query = _receiptInfoRepository.GetAllIncluding(r => r.MPayment, r => r.User, r => r.MInvoiceRecord,
                r => r.CustomerReg.Customer);
            if (!string.IsNullOrEmpty(searchInvoice.SearchName))
            {
                query = query.Where(r =>
                    r.ClientName == searchInvoice.SearchName || r.Customer.Name == searchInvoice.SearchName ||
                    r.CustomerReg.CustomerBM == searchInvoice.SearchName ||
                    r.MInvoiceRecord.Any(u => u.InvoiceNum == searchInvoice.SearchName));
            }

            if (searchInvoice.UserType == 1)
            {
                query = query.Where(r => r.UserId == AbpSession.UserId);
            }

            //if (searchInvoice.ReceiptSate != null && searchInvoice.ReceiptSate != 0)
            if (searchInvoice.ReceiptSate != 0)
            {
                query = query.Where(r => r.ReceiptSate == searchInvoice.ReceiptSate);
            }
            // else if (searchInvoice.ReceiptSate == 10)
            // query = query.Where(r => r.ReceiptSate != 1);

            //体检日期
            if (searchInvoice.StarDate != null && searchInvoice.EndDate != null)
            {
                searchInvoice.StarDate = searchInvoice.StarDate.Value.Date;
                searchInvoice.EndDate = searchInvoice.EndDate.Value.Date.AddDays(1);
                query = query.Where(o =>
                    o.CreationTime > searchInvoice.StarDate && o.CreationTime < searchInvoice.EndDate);
            }

            return query.MapTo<List<MReceiptInfoDto>>();
        }

        /// <summary>
        /// 获取收费类别明细
        /// </summary>
        /// <returns></returns>
        public List<MReceiptInfoDetailedViewDto> getReceiptInfoDetaileds(EntityDto<Guid> input)
        {
            var qure = _receiptDetailsRepository.GetAllIncluding(r => r.MReceiptInfo);
            qure = qure.Where(r => r.MReceiptInfo.Id == input.Id).OrderBy(o => o.DepartmentOrder)
                .ThenBy(o => o.ItemGroupOrder);
            var grouplst = qure.MapTo<List<MReceiptInfoDetailedViewDto>>();
            for (int i = 0; i < grouplst.Count; i++)
            {
                if (!string.IsNullOrEmpty(grouplst[i].ReceiptTypeName))
                {
                    string ss = grouplst[i].ReceiptTypeName.Trim();
                    grouplst[i].SfTypeOrder = _BasicDictionaries.GetAll().FirstOrDefault(o => o.Type == "ChargeCategory" && o.Text == ss).OrderNum ?? 0;
                }
            }

            return grouplst;
        }

        /// <summary>
        /// 获取收费类别明细
        /// </summary>
        /// <returns></returns>
        public List<ChargeGroupsDto> getReceiptInfoGroups(ChargeBM input)
        {
            //var qure = _MReceiptInfoDetailed.GetAllIncluding(r => r.MReceiptInfo);
            //qure = qure.Where(r => r.MReceiptInfo.Id == input.Id);
            //return qure.MapTo<List<MReceiptInfoDetailedViewDto>>();
            var qure = _customerItemGroupRepository.GetAll();
            if (input.Name == "单位")
            {
                qure = qure.Where(o => o.MReceiptInfoClientlId == input.Id).OrderBy(o => o.DepartmentId)
                    .OrderBy(o => o.DepartmentOrder).OrderBy(o => o.ItemGroupBM_Id).OrderBy(o => o.ItemGroupOrder);
            }
            else
            {
                qure = qure.Where(o => o.MReceiptInfoPersonalId == input.Id).OrderBy(o => o.DepartmentId)
                    .OrderBy(o => o.DepartmentOrder).OrderBy(o => o.ItemGroupBM_Id).OrderBy(o => o.ItemGroupOrder);
            }

            return qure.MapTo<List<ChargeGroupsDto>>();
        }

        /// <summary>
        /// 获取单位预约可结算信息
        /// </summary>
        public List<SettlementInfoViewDto> GetCompenyInfo(EntityDto<Guid> input)
        {
            var settlements = _receiptInfoRepository.GetAll().Where(r => r.Id == input.Id && r.ReceiptSate == 1);
            var result = new List<SettlementInfoViewDto>();
            foreach (var item in settlements)
            {
                var invoices = _invoiceRecordRepository.GetAllList().Where(r => r.MReceiptInfoId == item.Id);
                var total = item.Actualmoney;
                var already = invoices.Sum(i => i.InvoiceMoney);
                //if (total <= already)
                //    continue;

                var dto = new SettlementInfoViewDto { Total = total, Already = already, Surplus = total - already };
                result.Add(dto);
            }

            return result;
        }

        /// <summary>
        /// 发票打印
        /// </summary>
        /// <param name="input"></param>
        public MInvoiceRecordDto PrintInvoice(MInvoiceRecordDto input)
        {
            var invoiceList = _receiptManagerRepository.GetAllList(r => r.UserId == AbpSession.UserId && r.State == 1);
            if (!invoiceList.Any())
            {
                throw new FieldVerifyException("当前用户没用发票!", "当前用户没用发票!");
            }

            invoiceList = invoiceList.Where(i => i.StratCard < i.EndCard).OrderBy(r => r.CreationTime).ToList();
            if (!invoiceList.Any())
            {
                throw new FieldVerifyException("当前用户发票已用完!", "当前用户发票已用完!");
            }

            var currentNumber = invoiceList.First();

            input.Id = Guid.NewGuid();
            var entity = input.MapTo<TjlMInvoiceRecord>();
            entity.UserForMakeInvoiceId = AbpSession.UserId;
            entity.User = _userRepository.Get(AbpSession.UserId.Value);
            entity.MRise = _riseRepository.Get(input.MRise.Id);
            entity.InvoiceNum = currentNumber.NowCard.ToString();
            entity.MReceiptInfo = _receiptInfoRepository.Get(input.MReceiptInfo.Id);
            entity.MReceiptInfoId = input.MReceiptInfo.Id;
            var result = _invoiceRecordRepository.Insert(entity);
            currentNumber.NowCard = currentNumber.NowCard + 1;
            _receiptManagerRepository.Update(currentNumber);
            return result.MapTo<MInvoiceRecordDto>();
        }



        /// <summary>
        /// 指定体检人预约组合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ChargeGroupsDto> getChargeGroups(List<EntityDto<Guid>> input)
        {
            var qure = _customerItemGroupRepository.GetAll();
            var ids = input.Select(r => r.Id);
            qure = qure.Where(r => r.CustomerRegBMId.HasValue && ids.Contains(r.CustomerRegBMId.Value));
            return qure.MapTo<List<ChargeGroupsDto>>();
        }

        /// <summary>
        /// 更新预约及组合状态
        /// </summary>
        public void updateClientChargeState(UpdateChargeStateDto input)
        {
            // 待修改
            var payercat = (int)PayerCatType.PersonalCharge;
            var coststate = (int)PayerCatType.Charge;
            var refundstate = (int)PayerCatType.NotRefund;
            if (input.CusType == 1)
            {
                _customerItemGroupRepository.GetAll().Where(r => input.CusGroupids.Contains(r.Id)).Update(r =>
                    new TjlCustomerItemGroup
                    { MReceiptInfoPersonalId = input.ReceiptID, PayerCat = payercat, RefundState = refundstate });
                _customerRegRepository.GetAll().Where(r => input.CusRegids.Contains(r.Id))
                    .Update(r => new TjlCustomerReg { CostState = coststate });
            }
            else
            {
                _customerItemGroupRepository.GetAll().Where(r => input.CusGroupids.Contains(r.Id)).Update(r =>
                    new TjlCustomerItemGroup { MReceiptInfoClientlId = input.ReceiptID, RefundState = refundstate });
                _customerRegRepository.GetAll().Where(r => input.CusRegids.Contains(r.Id))
                    .Update(r => new TjlCustomerReg { CostState = coststate });
            }
        }

        /// <summary>
        /// 获取单位收费人员列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public List<ClientPayCusLisViewDto> GetTeamCusList(List<EntityDto<Guid>> input)
        {
            var qure = _customerRegRepository.GetAllIncluding(m => m.Customer, m => m.McusPayMoney, m => m.ClientInfo);
            var ids = input.Select(r => r.Id);
            qure = qure.Where(r => ids.Contains(r.ClientTeamInfoId.Value));
            return qure.MapTo<List<ClientPayCusLisViewDto>>();
        }

        /// <summary>
        /// 修改封帐状态
        /// </summary>
        /// <param name="input"></param>
        public ClientRegDto UpZFState(ChargeBM input)
        {
            var ClientReg = _clientRegRepository.Get(input.Id);
            ClientReg.FZState = int.Parse(input.Name);
            if (ClientReg.FZState == 1)
            { ClientReg.FZTime = System.DateTime.Now; }
            else
            { ClientReg.FZTime = null; }
            ClientReg = _clientRegRepository.Update(ClientReg);

            // TjlClientReg  ClientReg =  _TjlClientReg.GetAll().Where(r=>r.Id==input.Id).Update(r => new TjlClientReg { FZState = int.Parse(input.Name) });
            return ClientReg.MapTo<ClientRegDto>();
        }
        /// <summary>
        /// 查询封帐状态
        /// </summary>
        /// <param name="input"></param>
        public int GetZFState(EntityDto<Guid> input)
        {
            //var ClientReg = _clientRegRepository.Get(input.Id);

            var ClientReg = _clientRegRepository.FirstOrDefault(input.Id);
            if (ClientReg == null)
            {
                return 0;
            }
            else
            {
                return ClientReg.FZState;
            }
        }

        /// <summary>
        /// 更新个人收费费用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CusPayMoneyViewDto UpCusMoney(SearchPayMoneyDto input)
        {
            var tjlMcusPayMoney = _mcusPayMoneyRepository.GetAll().FirstOrDefault(r => r.CustomerReg.Id == input.Id);
            if (tjlMcusPayMoney == null)
            {
                return null;
            }

            tjlMcusPayMoney.PersonalPayMoney = tjlMcusPayMoney.PersonalPayMoney + input.PayMoney;
            tjlMcusPayMoney.PersonalMoney = tjlMcusPayMoney.PersonalMoney + input.DistMoney;
            tjlMcusPayMoney = _mcusPayMoneyRepository.Update(tjlMcusPayMoney);
            return tjlMcusPayMoney.MapTo<CusPayMoneyViewDto>();
        }
        /// <summary>
        ///获取个人收费费用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public CusPayMoneyViewDto GetCusMoney(EntityDto<Guid> input)
        {
            var tjlMcusPayMoney = _mcusPayMoneyRepository.GetAll().FirstOrDefault(r => r.CustomerReg.Id == input.Id);
            return tjlMcusPayMoney.MapTo<CusPayMoneyViewDto>();
        }
        /// <summary>
        /// 插入作废记录1
        /// </summary>
        /// <param name="input"></param>
        public MReceiptInfoDto InsertInvalidReceiptInfoDto(GuIdDto input)
        {
            var receiptInfoOld = _receiptInfoRepository.Get(input.Id);
            var receiptsate = (int)InvoiceStatus.HasCanceld;
            decimal mlMoney = receiptInfoOld.Discontmoney;
            receiptInfoOld.ReceiptSate = receiptsate;
            //添加作废记录
            var receiptInfoNew = new TjlMReceiptInfo();
            receiptInfoNew.Id = Guid.NewGuid();
            #region 线上退费
            var basic = _BasicDictionaries.GetAll().Where(p => p.Type == "WebPay").ToList();
            var webOpen = basic.FirstOrDefault(p => p.Value == 1)?.Remarks;
            if (webOpen != null && webOpen == "1" &&
               !string.IsNullOrEmpty(receiptInfoOld.merchant_id)
               && !string.IsNullOrEmpty(receiptInfoOld.pay_order_id)
               &&
               (!input.PaymentId.HasValue || receiptInfoOld.MPayment.Any(p => p.MChargeTypeId == input.PaymentId)
               ))
            {
                WebChargeInputDto webInput = new WebChargeInputDto();

                webInput.timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (input.Remoney.HasValue)
                {
                    webInput.total_amount = input.Remoney.Value.ToString("0.00");
                }
                else
                {
                    webInput.total_amount = receiptInfoOld.Actualmoney.ToString("0.00");
                }
                webInput.out_trade_no = receiptInfoOld.Id.ToString();
                if (!string.IsNullOrEmpty(receiptInfoOld.Remarks) &&
                  receiptInfoOld.Remarks.Length == receiptInfoOld.Id.ToString().Length)
                {
                    webInput.out_trade_no = receiptInfoOld.Remarks;
                }
                webInput.out_refund_no = receiptInfoNew.Id.ToString();
                var webErr = WebRefund(webInput);
                if (webErr.code == "1")
                {

                    receiptInfoNew.merchant_id = webErr.merchant_id;
                    receiptInfoNew.pay_order_id = webErr.pay_order_id;
                }
                else
                {
                  
                    MReceiptInfoDto retMReceiptInfoDto = new MReceiptInfoDto();
                    retMReceiptInfoDto.code = "0";
                    retMReceiptInfoDto.err = webErr.cardInfo;
                    #region MyRegion
                    //日志
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();

                    createOpLogDto.LogBM = receiptInfoOld.CustomerReg.CustomerBM;
                    createOpLogDto.LogName = receiptInfoOld.Customer.Name;


                    createOpLogDto.LogText = "线上退费返回";
                    createOpLogDto.LogDetail = Convert.ToString(webErr.cardInfo);
                    createOpLogDto.LogType = (int)LogsTypes.ChargId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                    #endregion
                    return retMReceiptInfoDto;
                }
            }
            #endregion
            //修改原收费状态
            receiptInfoOld = _receiptInfoRepository.Update(receiptInfoOld);
            receiptsate = (int)InvoiceStatus.Cancellation;
            receiptInfoNew.InvalidTjlMReceiptInfoId = receiptInfoOld.Id;



            //重新赋值外键
            receiptInfoNew.Actualmoney = -receiptInfoOld.Actualmoney;
            receiptInfoNew.Shouldmoney = -receiptInfoOld.Shouldmoney;
            receiptInfoNew.Summoney = -receiptInfoOld.Summoney;
            receiptInfoNew.ChargeDate = DateTime.Now;
            receiptInfoNew.ChargeState = receiptInfoOld.ChargeState;
            var settlementsate = (int)ReceiptState.UnSettled;
            receiptInfoNew.SettlementSate = settlementsate;
            receiptInfoNew.TJType = receiptInfoOld.TJType;
            receiptInfoNew.ClientName = receiptInfoOld.ClientName;
            receiptInfoNew.ClientReg = receiptInfoOld.ClientReg;
            receiptInfoNew.ClientRegId = receiptInfoOld.ClientRegId;
            receiptInfoNew.Customer = receiptInfoOld.Customer;
            receiptInfoNew.CustomerId = receiptInfoOld.CustomerId;
            receiptInfoNew.CustomerReg = receiptInfoOld.CustomerReg;
            receiptInfoNew.CustomerRegId = receiptInfoOld.CustomerRegId;
            receiptInfoNew.Discontmoney = receiptInfoOld.Discontmoney;
            receiptInfoNew.DiscontReason = "";
            receiptInfoNew.Discount = receiptInfoOld.Discount;

            receiptInfoNew.ReceiptSate = receiptsate;
            receiptInfoNew.Remarks = "";
            receiptInfoNew.User = null;
            receiptInfoNew.UserId = AbpSession.UserId.Value;

            var tjlMReceiptInfoNew = _receiptInfoRepository.Insert(receiptInfoNew);

            CurrentUnitOfWork.SaveChanges();

            //插入支付方式作废记录
            foreach (var payment in receiptInfoOld.MPayment)
            {
                var MPaymentNew = new TjlMPayment();
                MPaymentNew.Id = Guid.NewGuid();
                MPaymentNew.InvalidTjlMPaymentId = payment.Id;
                MPaymentNew.Actualmoney = -payment.Actualmoney;
                MPaymentNew.CardNum = payment.CardNum;
                MPaymentNew.Discount = payment.Discount;
                if (input.PaymentId.HasValue)
                {
                    var paymentInfo = _chargeTypeRepository.Get(input.PaymentId.Value);

                    MPaymentNew.MChargeTypename = paymentInfo.ChargeName;
                    MPaymentNew.MChargeType = paymentInfo;
                }
                else
                {
                    MPaymentNew.MChargeType = payment.MChargeType;
                    MPaymentNew.MChargeTypename = payment.MChargeTypename;
                }
                MPaymentNew.MReceiptInfo = null;
                MPaymentNew.MReceiptInfoId = tjlMReceiptInfoNew.Id;
                MPaymentNew.Shouldmoney = -payment.Shouldmoney;
                _paymentRepository.Insert(MPaymentNew);
                #region 优康会员卡接口 
                if (payment.MChargeTypename == "会员卡")
                {
                    try
                    {

                        ChargCardDto cusinput = new ChargCardDto();
                        cusinput.Amount = -payment.Actualmoney;
                        cusinput.CardNo = payment.CardNum;
                        cusinput.CheckItemCount = 0;
                        cusinput.ArchivesNum = "";
                        cusinput.SuitName = "";
                        var ykstrh = NYHChargeCard(cusinput);
                        if (ykstrh.code == 0)
                        {
                            throw new FieldVerifyException("会员卡退费失败", ykstrh.Mess);
                        }

                    }
                    catch (Exception ex)
                    {
                        ;
                        throw new FieldVerifyException("会员卡退费失败", ex.Message);
                    }
                }
                #endregion
                #region 单位储值卡
                if (payment.MChargeTypename == "单位储值卡")
                {
                    var carNO = payment.CardNum;
                    var cardinfo = _TbmCard.GetAll().FirstOrDefault(p => p.CardNo == carNO);
                    if (cardinfo != null)
                    {
                        cardinfo.FaceValue = cardinfo.FaceValue + payment.Actualmoney;
                    }
                    _TbmCard.Update(cardinfo);
                }
                #endregion
                //CurrentUnitOfWork.SaveChanges();
            }

            //插入发票作废记录
            foreach (var InvoiceRecord in receiptInfoOld.MInvoiceRecord)
            {
                var InvoiceRecordNew = new TjlMInvoiceRecord();
                InvoiceRecordNew.Id = Guid.NewGuid();
                InvoiceRecordNew.InvalidTjlMInvoiceRecordId = InvoiceRecord.Id;
                InvoiceRecordNew.InvoiceMoney = -InvoiceRecord.InvoiceMoney;
                InvoiceRecordNew.InvoiceNum = InvoiceRecord.InvoiceNum;
                InvoiceRecordNew.MReceiptInfo = null;
                InvoiceRecordNew.MReceiptInfoId = tjlMReceiptInfoNew.Id;
                InvoiceRecordNew.State = receiptsate.ToString();
                _invoiceRecordRepository.Insert(InvoiceRecordNew);


                //CurrentUnitOfWork.SaveChanges();
            }

            //插入收费明细记录
            foreach (var ReceiptDetail in receiptInfoOld.MReceiptDetailses)
            {
                var ReceiptDetailNew = new TjlMReceiptDetails();
                ReceiptDetailNew.Id = Guid.NewGuid();
                ReceiptDetailNew.InvalidTjlMReceiptDetailsId = ReceiptDetail.Id;
                ReceiptDetailNew.ItemGroupId = ReceiptDetail.ItemGroupId;
                ReceiptDetailNew.ItemGroupName = ReceiptDetail.ItemGroupName;
                ReceiptDetailNew.ItemGroupOrder = ReceiptDetail.ItemGroupOrder;
                ReceiptDetailNew.ItemGroupCodeBM = ReceiptDetail.ItemGroupCodeBM;
                ReceiptDetailNew.DepartmentId = ReceiptDetail.DepartmentId;
                ReceiptDetailNew.DepartmentName = ReceiptDetail.DepartmentName;
                ReceiptDetailNew.DepartmentOrder = ReceiptDetail.DepartmentOrder;
                ReceiptDetailNew.DepartmentCodeBM = ReceiptDetail.DepartmentCodeBM;
                ReceiptDetailNew.PayerCat = ReceiptDetail.PayerCat;
                ReceiptDetailNew.IsAddMinus = ReceiptDetail.IsAddMinus;
                ReceiptDetailNew.Discount = ReceiptDetail.Discount;
                ReceiptDetailNew.GroupsDiscountMoney = -ReceiptDetail.GroupsDiscountMoney;
                ReceiptDetailNew.GroupsMoney = -ReceiptDetail.GroupsMoney;
                ReceiptDetailNew.MReceiptInfo = null;
                ReceiptDetailNew.MReceiptInfoID = tjlMReceiptInfoNew.Id;
                ReceiptDetailNew.ReceiptTypeName = ReceiptDetail.ReceiptTypeName;
                _receiptDetailsRepository.Insert(ReceiptDetailNew);

                //CurrentUnitOfWork.SaveChanges();
            }

            //取消组合收费状态            
            if (tjlMReceiptInfoNew.ClientReg == null)
            {
                var payercat = (int)PayerCatType.NoCharge;
                var refundstate = (int)PayerCatType.Refund;

                _customerItemGroupRepository.GetAll().Where(r => r.MReceiptInfoPersonalId == receiptInfoOld.Id).Update(r =>
                    new TjlCustomerItemGroup
                    { MReceiptInfoPersonalId = null, PayerCat = payercat, RefundState = refundstate });
                var tjlCustomerReg = _customerRegRepository.Get(receiptInfoOld.CustomerRegId.Value);
                tjlCustomerReg.CostState = payercat;
                _customerRegRepository.Update(tjlCustomerReg);
                //减去抹零项目
                if (mlMoney != 0)
                {
                    Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
                    var mlgroup = _customerItemGroupRepository.FirstOrDefault(o => o.CustomerRegBMId == receiptInfoOld.CustomerRegId && o.ItemGroupBM_Id == guid);
                    if (mlgroup != null)
                    {
                        mlgroup.ItemPrice = mlgroup.ItemPrice + mlMoney;
                        mlgroup.PriceAfterDis = mlgroup.PriceAfterDis + mlMoney;
                        if (mlgroup.PriceAfterDis == 0)
                        {
                            _customerItemGroupRepository.Delete(mlgroup);
                        }
                        else
                        {
                            _customerItemGroupRepository.Update(mlgroup);
                        }

                    }
                }
                //更新收费表
                var cuspay = _mcusPayMoneyRepository.FirstOrDefault(r => r.CustomerReg.Id == receiptInfoOld.CustomerRegId);
                cuspay.PersonalPayMoney = cuspay.PersonalPayMoney - receiptInfoOld.Actualmoney;
                cuspay.PersonalMoney = cuspay.PersonalMoney + mlMoney;
                _mcusPayMoneyRepository.Update(cuspay);
            }
            else
            {
                _customerItemGroupRepository.GetAll().Where(r => r.MReceiptInfoClientlId == receiptInfoOld.Id).Update(r =>
                    new TjlCustomerItemGroup { MReceiptInfoClientlId = null });
                _customerRegRepository.GetAll().Where(r => r.ClientRegId == receiptInfoOld.ClientRegId)
                    .Update(r => new TjlCustomerReg { CostState = 1 });
            }
            CurrentUnitOfWork.SaveChanges();
            if (!string.IsNullOrEmpty(receiptInfoOld.ewm))
            {

                #region 作废电子发票接口
                var bis = _BasicDictionaries.GetAll().Where(p => p.Type == "DZFP").ToList();
                var fpisop = bis.FirstOrDefault(p => p.Value == 1)?.Remarks;
                if (fpisop == "1")
                {
                    if (!string.IsNullOrEmpty(receiptInfoOld.fpdm))
                    {
                        HCDZPF(receiptInfoOld, receiptInfoNew.Id);
                    }
                    //电子发票不作废只红冲
                    //var fpComName = bis.FirstOrDefault(p => p.Value == 2)?.Remarks;
                    //if (!string.IsNullOrEmpty(fpComName) && fpComName == "航天信息")
                    //{
                    //    try
                    //    {

                    //        //销方名称  常州九洲金东方医院有限公司
                    //        var xfmc = bis.FirstOrDefault(p => p.Value == 3)?.Remarks;
                    //        //销方税号 91320412MA1MBTW87E
                    //        var xfsh = bis.FirstOrDefault(p => p.Value == 4)?.Remarks;
                    //        //销方开户行及银行账号  中国农业银行股份有限公司常州湖塘支行 10602301040017703
                    //        var xfyhzh = bis.FirstOrDefault(p => p.Value == 5)?.Remarks;
                    //        //销方地址电话 常州市武进区湖滨南路99号 0519-81089121
                    //        var xfdzdh = bis.FirstOrDefault(p => p.Value == 6)?.Remarks;
                    //        ZFDZFP zffp = new ZFDZFP();
                    //        zffp.fjh = "1";
                    //        zffp.fpdm = "";
                    //        zffp.fphm = receiptInfoOld.fphm;
                    //        zffp.fpzl= "51";
                    //        zffp.xfsh = xfsh;
                    //        zffp.xsdjbh = receiptInfoOld.xsdjbh;
                    //        zffp.zdh= "1";
                    //        var json = JsonConvert.SerializeObject(zffp);

                    //        DZFP.SalesWebService salesWebService = new DZFP.SalesWebService();
                    //        var outresult = salesWebService.invoiceZF(json);
                    //        var userViewDto = JsonConvert.DeserializeObject<ZFDZFPOUt>(outresult);
                    //        if (userViewDto != null)
                    //        {
                    //            if (userViewDto.returnCode == "0000")
                    //            {


                    //                receiptInfoOld.returnCode = userViewDto.returnCode;
                    //                receiptInfoOld.returnMsg = "电子发票作废成功";                                   
                    //                _receiptInfoRepository.Update(receiptInfoOld);
                    //            }
                    //            else
                    //            {
                    //                //发票返回信息存入收费表
                    //                receiptInfoOld.returnCode = userViewDto.returnCode;
                    //                receiptInfoOld.returnMsg ="电子发票作废失败" + userViewDto.returnMsg;
                    //                _receiptInfoRepository.Update(receiptInfoOld);
                    //            }
                    //        }
                    //    }
                    //    catch (Exception)
                    //    {

                    //        throw;
                    //    }
                    //}
                }
                #endregion

            }
            var mReceiptInfoDto = tjlMReceiptInfoNew.MapTo<MReceiptInfoDto>();

            // CurrentUnitOfWork.SaveChanges();
            return mReceiptInfoDto;
        }

        /// <summary>
        /// 保存抹零项
        /// </summary>
        /// <param name="input"></param>
        public TjlCustomerItemGroupDto InsertMLGroup(SearchMLGroupDto input)
        {
            Guid guid = Guid.Parse("866488A7-2630-429C-8DF4-905B8A5FF734");
            TjlCustomerItemGroupDto tjlCustomerItemGroup = new TjlCustomerItemGroupDto();
            var tjlCustomerGroup = _customerItemGroupRepository.FirstOrDefault(o => o.CustomerRegBMId == input.CustomerRegID && o.ItemGroupBM_Id == guid);
            var bmGrop = _itemGroupRepository.FirstOrDefault(o => o.Id == guid);
            if (bmGrop != null)
            {
                if (tjlCustomerGroup == null)
                {
                    tjlCustomerGroup = new TjlCustomerItemGroup();
                    tjlCustomerGroup.Id = Guid.NewGuid();
                    tjlCustomerGroup.BarState = 1;
                    tjlCustomerGroup.BillingEmployeeBM = null;
                    tjlCustomerGroup.BillingEmployeeBMId = AbpSession.UserId.Value;
                    tjlCustomerGroup.CheckState = 1;
                    tjlCustomerGroup.CollectionState = 1;
                    tjlCustomerGroup.CustomerRegBM = null;
                    tjlCustomerGroup.CustomerRegBMId = input.CustomerRegID;
                    tjlCustomerGroup.DepartmentBM = bmGrop.Department;
                    tjlCustomerGroup.DepartmentName = bmGrop.Department.Name;
                    tjlCustomerGroup.DepartmentId = bmGrop.DepartmentId;
                    tjlCustomerGroup.DepartmentCodeBM = bmGrop.Department.DepartmentBM;
                    tjlCustomerGroup.DepartmentOrder = bmGrop.Department.OrderNum;
                    tjlCustomerGroup.DiscountRate = 1;
                    tjlCustomerGroup.DrawSate = 1;
                    tjlCustomerGroup.GRmoney = input.MLMoney;
                    tjlCustomerGroup.GuidanceSate = 1;
                    tjlCustomerGroup.IsAddMinus = 1;
                    tjlCustomerGroup.ItemGroupBM = null;
                    tjlCustomerGroup.ItemGroupBM_Id = bmGrop.Id;
                    tjlCustomerGroup.ItemGroupCodeBM = bmGrop.ItemGroupBM;
                    tjlCustomerGroup.ItemGroupName = bmGrop.ItemGroupName;
                    tjlCustomerGroup.ItemGroupOrder = bmGrop.OrderNum;
                    tjlCustomerGroup.ItemPrice = input.MLMoney;
                    tjlCustomerGroup.MReceiptInfoPersonalId = input.MReceiptInfoPersonalId;
                    tjlCustomerGroup.PayerCat = 2;
                    tjlCustomerGroup.PriceAfterDis = input.MLMoney;
                    int sfzt = (int)PayerCatType.NotRefund;
                    tjlCustomerGroup.RefundState = sfzt;
                    tjlCustomerGroup.RequestState = 1;
                    if (bmGrop.ChartCode != null)
                    {
                        if (Regex.IsMatch(bmGrop.ChartCode, @"^[+-]?\d*[.]?\d*$"))
                            tjlCustomerGroup.SFType = int.Parse(bmGrop.ChartCode);
                    }
                    tjlCustomerGroup.SummBackSate = 1;
                    tjlCustomerGroup.SuspendState = 1;
                    tjlCustomerGroup.TTmoney = 0;
                    TjlCustomerItemGroup tjlCustomerItemGroupls = _customerItemGroupRepository.Insert(tjlCustomerGroup);
                    tjlCustomerItemGroup = tjlCustomerItemGroupls.MapTo<TjlCustomerItemGroupDto>();
                    CurrentUnitOfWork.SaveChanges();
                }
                else
                {
                    tjlCustomerGroup.PriceAfterDis += input.MLMoney;
                    tjlCustomerGroup.GRmoney += input.MLMoney;
                    tjlCustomerGroup.ItemPrice += input.MLMoney;
                    tjlCustomerItemGroup = _customerItemGroupRepository.Update(tjlCustomerGroup).MapTo<TjlCustomerItemGroupDto>();

                }
                var ReceiptDetail = new TjlMReceiptDetails();
                ReceiptDetail.Id = Guid.NewGuid();
                ReceiptDetail.ItemGroupId = tjlCustomerItemGroup.ItemGroupBM_Id;
                ReceiptDetail.ItemGroupName = tjlCustomerItemGroup.ItemGroupName;
                ReceiptDetail.ItemGroupOrder = tjlCustomerItemGroup.ItemGroupOrder;
                ReceiptDetail.ItemGroupCodeBM = bmGrop.ItemGroupBM;
                ReceiptDetail.DepartmentId = tjlCustomerItemGroup.DepartmentId;
                ReceiptDetail.DepartmentName = tjlCustomerItemGroup.DepartmentName;
                ReceiptDetail.DepartmentOrder = tjlCustomerItemGroup.DepartmentOrder;
                ReceiptDetail.DepartmentCodeBM = bmGrop.Department.DepartmentBM;
                ReceiptDetail.PayerCat = tjlCustomerItemGroup.PayerCat;
                ReceiptDetail.IsAddMinus = tjlCustomerItemGroup.IsAddMinus;
                ReceiptDetail.GroupsMoney = tjlCustomerGroup.ItemPrice;
                ReceiptDetail.Discount = 1;
                ReceiptDetail.GroupsDiscountMoney = tjlCustomerGroup.PriceAfterDis;
                ReceiptDetail.MReceiptInfoID = input.MReceiptInfoPersonalId;
                //TjlMReceiptInfo tjlMReceiptInfo = _receiptInfoRepository.Get(input.MReceiptInfoPersonalId);
                //ReceiptDetail.MReceiptInfo = tjlMReceiptInfo;
                ReceiptDetail.ReceiptTypeName = bmGrop.ChartName;
                _receiptDetailsRepository.Insert(ReceiptDetail);
            }
            //收费关联项目

            return tjlCustomerItemGroup;
        }

        /// <summary>
        /// 获取体检人体检清单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<TjlCustomerItemGroupDto> GetCustomerItemGroupList(EntityDto<Guid> input)
        {
            var itemgroup = _customerItemGroupRepository.GetAll().Where(o => o.MReceiptInfoPersonal.Id == input.Id);
            return itemgroup.MapTo<List<TjlCustomerItemGroupDto>>();
        }

        /// <summary>
        /// 获取收费记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public MReceiptInfoDto GetInvalidReceiptById(EntityDto<Guid> input)
        {
            var result = _receiptInfoRepository.Get(input.Id);
            return result.MapTo<MReceiptInfoDto>();
        }

        /// <summary>
        /// 获取作废记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public MReceiptInfoDto GetInvalid(EntityDto<Guid> input)
        {
            var result = _receiptInfoRepository.FirstOrDefault(o => o.InvalidTjlMReceiptInfoId == input.Id);
            return result.MapTo<MReceiptInfoDto>();
        }

        /// <summary>
        /// 判断该收费记录所管理项目是否已检
        /// </summary>
        /// <returns></returns>
        public bool SFGroupCheck(EntityDto<Guid> input)
        {
            var ischeck = false;
            var receipt = _receiptInfoRepository.Get(input.Id);
            if (receipt.CustomerRegId == null)
            {
                var tjlCustomerItemGroup = _customerItemGroupRepository.GetAll()
                    .Where(r => r.MReceiptInfoClientlId == input.Id && r.CheckState >= 2);
                if (tjlCustomerItemGroup.Count() > 0)
                {
                    ischeck = true;
                }
            }
            else
            {
                var tjlCustomerItemGroup = _customerItemGroupRepository.GetAll()
                    .Where(r => r.MReceiptInfoPersonalId == input.Id && r.CheckState >= 2);
                if (tjlCustomerItemGroup.Count() > 0)
                {
                    ischeck = true;
                }
            }

            return ischeck;
        }

        public void ZFDZFP()
        {

        }


        /// <summary>
        /// 单位明细
        /// </summary>
        /// <param name="query">查询类（可通用）</param>
        public List<SearchKSGZLStatisticsDto> KSGZLStatistics(QueryClass query)
        {
            var data = _customerItemGroupRepository.GetAllIncluding(r => r.CustomerRegBM);
            var guid = query.ClientInfoId[0].Value;
            int nopay = (int)PayerCatType.NoCharge;//未付费
            int grpay = (int)PayerCatType.PersonalCharge;//个人付费
            int minus = (int)AddMinusType.Minus;
            int adjust = (int)AddMinusType.AdjustMinus;
            int complete = (int)ProjectIState.Complete;
            int part = (int)ProjectIState.Part;
            int hascheck = (int)RegisterState.Yes;
            int adxm = (int)AddMinusType.Normal;
            var database = _BasicDictionaries.GetAll().Where(o => o.Type == "ChargeCategory").Select(o => new { o.OrderNum, o.Value });
            data = data.Where(r =>
                r.CustomerRegBM.ClientRegId == guid && r.PayerCat != nopay && r.PayerCat != grpay && r.IsAddMinus != minus &&
                r.IsAddMinus != adjust);
            if (query.ClientTeamID != null)
            {
                data = data.Where(r => query.DepartmentBMList.Contains(r.CustomerRegBM.ClientTeamInfoId.Value));
            }

            var ksgzllist = data.GroupBy(r => new
            {
                r.SFType,
                r.DepartmentId,
                r.DepartmentName,
                r.ItemGroupCodeBM,
                r.ItemGroupName,
                r.ItemPrice,
                r.IsAddMinus

                //r.ItemGroupBM.cod
            }).Select(r => new SearchKSGZLStatisticsDto
            {
                //TeamName = r.Key.TeamName,
                //ClientTeamInfoId = r.Key.ClientTeamInfoId,

                SFType = r.Key.SFType,
                Department_Id = r.Key.DepartmentId,
                DepartmentName = r.Key.DepartmentName,
                ItemGroupName = r.Key.ItemGroupName,
                RegisterNum = r.Where(a => a.CustomerRegBM.RegisterState == hascheck).Count(), //登记状态 1未登记 2已登记                
                CompleteNum = r.Where(a => a.CheckState == complete || a.CheckState == part).Count(), //项目检查状态 1未检查2已检查3部分检查4放弃5待查 6暂存          
                ItemPrice = r.Key.ItemPrice, //单价
                Count = r.Count(),
                ActualMoney = r.Sum(a => a.ItemPrice),
                ShouldMoney = r.Sum(a => a.PriceAfterDis),
                DiscountRate = r.Sum(a => a.PriceAfterDis) == 0
                    ? 0
                    : r.Sum(a => a.ItemPrice) / r.Sum(a => a.PriceAfterDis),
                //CheckMoney = r.Where(a => a.CheckState == 2).Sum(a => a.PriceAfterDis) == null
                //    ? 0
                //    : r.Where(a => a.CheckState == 2).Sum(a => a.PriceAfterDis),
                CheckMoney = r.Where(a => a.CheckState == complete || a.CheckState == part).Sum(a => a.PriceAfterDis) == null ? 0.00m : r.Where(a => a.CheckState == complete || a.CheckState == part).Sum(a => a.PriceAfterDis),
                IsAddMinus = r.Key.IsAddMinus == adxm ? "正常项" : "加项",
                ItemGroupOrder = database.FirstOrDefault(o => o.Value == r.Key.SFType.Value).OrderNum ?? 0,
                //DiscountRate= r.Sum(a => a.ItemPrice)==0.00m?0:r.Sum(a => a.PriceAfterDis)/ r.Sum(a => a.ItemPrice)

            }).ToList();

            //  int? ss = _BasicDictionaries.FirstOrDefault(o => o.Value == 0).OrderNum;
            return ksgzllist.OrderBy(r => r.ItemGroupOrder).ThenBy(r => r.DepartmentName).ToList();
        }

        /// <summary>
        /// 部分退费
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<MReceiptInfoDto> GetReceiptInfoBack(List<ChargeGroupsDto> input)
        {
            var mReceiptInfoPerDto = new List<MReceiptInfoDto>();
            var receipguids = input.Select(o => o.MReceiptInfoPersonalId).Distinct();
            foreach (Guid guid in receipguids)
            {
                //添加作废记录
                var receiptInfoOld = _receiptInfoRepository.Get(guid);
                var backmoney = input.Where(o => o.MReceiptInfoPersonalId == guid).Sum(o => o.GRmoney);
                var backallmoney = input.Where(o => o.MReceiptInfoPersonalId == guid).Sum(o => o.ItemPrice);
                var receiptInfoNew = new TjlMReceiptInfo();
                receiptInfoNew.Id = Guid.NewGuid();
                //重新赋值外键
                receiptInfoNew.Actualmoney = receiptInfoOld.Actualmoney - backmoney;
                receiptInfoNew.Shouldmoney = receiptInfoOld.Shouldmoney - backmoney;
                receiptInfoNew.Summoney = receiptInfoOld.Summoney - backallmoney;
                receiptInfoNew.ChargeDate = DateTime.Now;
                receiptInfoNew.ChargeState = 1;
                receiptInfoNew.SettlementSate = 2;
                receiptInfoNew.TJType = 1;
                receiptInfoNew.CustomerId = receiptInfoOld.CustomerId;
                receiptInfoNew.CustomerReg = receiptInfoOld.CustomerReg;
                receiptInfoNew.CustomerRegId = receiptInfoOld.CustomerRegId;
                receiptInfoNew.Discontmoney = receiptInfoOld.Discontmoney;
                receiptInfoNew.DiscontReason = "";
                if (receiptInfoNew.Actualmoney != 0 && receiptInfoNew.Summoney != 0)
                {
                    receiptInfoNew.Discount = receiptInfoNew.Actualmoney / receiptInfoNew.Summoney;
                }
                else
                {
                    receiptInfoNew.Discount = 0;
                }

                receiptInfoNew.ReceiptSate = 1;
                receiptInfoNew.Remarks = "";
                receiptInfoNew.User = null;
                receiptInfoNew.UserId = AbpSession.UserId.Value;
                #region 金东方微信支付需要
                receiptInfoNew.xsdjbh = receiptInfoOld.xsdjbh;
                receiptInfoNew.hjje = receiptInfoNew.Actualmoney.ToString();
                receiptInfoNew.hjje = receiptInfoOld.hjje;
                receiptInfoNew.kprq = receiptInfoOld.kprq;
                receiptInfoNew.fpdm = receiptInfoOld.fpdm;
                receiptInfoNew.ssyf = receiptInfoOld.ssyf;
                receiptInfoNew.fphm = receiptInfoOld.fphm;
                receiptInfoNew.qdbz = receiptInfoOld.qdbz;
                receiptInfoNew.mw = receiptInfoOld.mw;
                receiptInfoNew.jym = receiptInfoOld.jym;
                receiptInfoNew.qmz = receiptInfoOld.qmz;
                receiptInfoNew.ewm = receiptInfoOld.ewm;
                receiptInfoNew.jqbh = receiptInfoOld.jqbh;
                receiptInfoNew.merchant_id = receiptInfoOld.merchant_id;
                receiptInfoNew.pay_order_id = receiptInfoOld.pay_order_id;
                if (!string.IsNullOrEmpty(receiptInfoOld.Remarks) &&
                    receiptInfoOld.Remarks.Length == receiptInfoOld.Id.ToString().Length)
                {
                    receiptInfoNew.Remarks = receiptInfoOld.Remarks;
                }
                else
                { receiptInfoNew.Remarks = receiptInfoOld.Id.ToString(); }
                #endregion
                var tjlMReceiptInfoNew = _receiptInfoRepository.Insert(receiptInfoNew);

                CurrentUnitOfWork.SaveChanges();

                //插入支付方式作废记录
                var ysmoney = backmoney;
                var ysallMoney = backallmoney;
                tjlMReceiptInfoNew.MPayment = new List<TjlMPayment>();

                foreach (var payment in receiptInfoOld.MPayment)
                {
                    decimal ysmoeyls = 0;
                    decimal ysallmoeyls = 0;
                    if (payment.Actualmoney > ysmoney)
                    {
                        ysmoeyls = ysmoney;
                        ysallmoeyls = ysallMoney;
                    }
                    else
                    {
                        ysmoeyls = payment.Actualmoney;
                        ysallmoeyls = payment.Shouldmoney;
                        ysmoney = ysmoney - ysmoeyls;
                        ysallMoney = ysallMoney - ysallmoeyls;
                    }

                    var MPaymentNew = new TjlMPayment();
                    MPaymentNew.Id = Guid.NewGuid();
                    MPaymentNew.Actualmoney = payment.Actualmoney - ysmoeyls;
                    MPaymentNew.CardNum = payment.CardNum;
                    MPaymentNew.Discount = payment.Discount;
                    MPaymentNew.MChargeType = payment.MChargeType;
                    MPaymentNew.MChargeTypename = payment.MChargeTypename;

                    if (input.FirstOrDefault() != null && input.FirstOrDefault().PaymentId.HasValue)
                    {

                        var paymentInfo = _chargeTypeRepository.Get(input.FirstOrDefault().PaymentId.Value);
                        MPaymentNew.MChargeTypename = paymentInfo.ChargeName;
                        MPaymentNew.MChargeType = paymentInfo;
                    }
                    MPaymentNew.MReceiptInfo = null;
                    MPaymentNew.MReceiptInfoId = tjlMReceiptInfoNew.Id;
                    MPaymentNew.Shouldmoney = -payment.Shouldmoney - ysallMoney;
                    var tjlpayment = _paymentRepository.Insert(MPaymentNew);
                    #region 优康会员卡接口 
                    if (payment.MChargeTypename == "会员卡")
                    {
                        try
                        {

                            ChargCardDto cusinput = new ChargCardDto();
                            cusinput.Amount = MPaymentNew.Actualmoney;
                            cusinput.CardNo = payment.CardNum;
                            cusinput.CheckItemCount = 0;
                            cusinput.ArchivesNum = "";
                            cusinput.SuitName = "";
                            var ykstrh = NYHChargeCard(cusinput);
                            if (ykstrh.code == 0)
                            {
                                throw new FieldVerifyException("会员卡扣分失败", ykstrh.Mess);
                            }

                        }
                        catch (Exception ex)
                        {
                            ;
                            throw new FieldVerifyException("会员卡扣分失败", ex.Message);
                        }
                    }
                    #endregion
                    #region 单位储值卡扣款 
                    //var jjkpayment = input.MPaymentr.FirstOrDefault(o => o.MChargeTypename == "单位储值卡");
                    if (MPaymentNew.MChargeTypename == "单位储值卡")
                    {

                        try
                        {
                            var _tbmCardinfo = _TbmCard.GetAll().FirstOrDefault(p => p.CardNo ==
                           payment.CardNum);


                            if (_tbmCardinfo != null)
                            {

                                _tbmCardinfo.FaceValue = _tbmCardinfo.FaceValue - MPaymentNew.Actualmoney;
                                _tbmCardinfo.CustomerRegId = tjlMReceiptInfoNew.CustomerRegId;
                                _tbmCardinfo.UseTime = System.DateTime.Now;

                                _TbmCard.Update(_tbmCardinfo);



                            }
                        }
                        catch
                        {

                        }
                    }
                    #endregion
                    tjlMReceiptInfoNew.MPayment.Add(tjlpayment);
                    //CurrentUnitOfWork.SaveChanges();
                }

                var guidls = new List<Guid>();
                // List<Guid> guidregls = new List<Guid>();
                //插入收费明细记录
                // var cusPerGroups 
                foreach (var ReceiptDetail in receiptInfoOld.MReceiptDetailses)
                    if (input.Where(o => o.ItemGroupName == ReceiptDetail.ItemGroupName).ToList().Count <= 0)
                    {
                        var ReceiptDetailNew = new TjlMReceiptDetails();
                        ReceiptDetailNew.Id = Guid.NewGuid();
                        ReceiptDetailNew.ItemGroupId = ReceiptDetail.ItemGroupId;
                        ReceiptDetailNew.ItemGroupName = ReceiptDetail.ItemGroupName;
                        ReceiptDetailNew.ItemGroupOrder = ReceiptDetail.ItemGroupOrder;
                        ReceiptDetailNew.ItemGroupCodeBM = ReceiptDetail.ItemGroupCodeBM;
                        ReceiptDetailNew.DepartmentId = ReceiptDetail.DepartmentId;
                        ReceiptDetailNew.DepartmentName = ReceiptDetail.DepartmentName;
                        ReceiptDetailNew.DepartmentOrder = ReceiptDetail.DepartmentOrder;
                        ReceiptDetailNew.DepartmentCodeBM = ReceiptDetail.DepartmentCodeBM;
                        ReceiptDetailNew.PayerCat = ReceiptDetail.PayerCat;
                        ReceiptDetailNew.IsAddMinus = ReceiptDetail.IsAddMinus;
                        ReceiptDetailNew.Discount = ReceiptDetail.Discount;
                        ReceiptDetailNew.GroupsDiscountMoney = ReceiptDetail.GroupsDiscountMoney;
                        ReceiptDetailNew.GroupsMoney = ReceiptDetail.GroupsMoney;
                        ReceiptDetailNew.MReceiptInfo = null;
                        ReceiptDetailNew.MReceiptInfoID = tjlMReceiptInfoNew.Id;
                        ReceiptDetailNew.ReceiptTypeName = ReceiptDetail.ReceiptTypeName;
                        _receiptDetailsRepository.Insert(ReceiptDetailNew);
                        var tjlCustomerItemGroup = _customerItemGroupRepository.FirstOrDefault(o =>
                            o.CustomerRegBMId == tjlMReceiptInfoNew.CustomerRegId &&
                            o.ItemGroupBM_Id == ReceiptDetail.ItemGroupId);
                        guidls.Add(tjlCustomerItemGroup.Id);
                    }

                //CurrentUnitOfWork.SaveChanges();

                var mReceiptInfoDto = tjlMReceiptInfoNew.MapTo<MReceiptInfoDto>();
                mReceiptInfoPerDto.Add(mReceiptInfoDto);
                //更新体检人收费状态及组合结算状态
                var refundstate = (int)PayerCatType.NotRefund;
                var coststate = (int)PayerCatType.Charge;
                var payercat = (int)PayerCatType.PersonalCharge;

                var regid = input.Select(o => o.CustomerRegBMId).First();
                _customerItemGroupRepository.GetAll().Where(r => guidls.Contains(r.Id)).Update(r =>
                    new TjlCustomerItemGroup
                    {
                        MReceiptInfoPersonalId = tjlMReceiptInfoNew.Id,
                        PayerCat = payercat,
                        RefundState = refundstate
                    });
                _customerRegRepository.GetAll().Where(r => r.Id == regid)
                    .Update(r => new TjlCustomerReg { CostState = coststate });
                //更新费用表
                var tjlMcusPayMoney = _mcusPayMoneyRepository.GetAll().FirstOrDefault(r => r.CustomerReg.Id == regid);
                if (tjlMcusPayMoney == null)
                {
                    return null;
                }

                tjlMcusPayMoney.PersonalPayMoney = tjlMcusPayMoney.PersonalPayMoney + receiptInfoNew.Actualmoney;
                tjlMcusPayMoney = _mcusPayMoneyRepository.Update(tjlMcusPayMoney);
                //更新退费项目状态
                var refund = (int)PayerCatType.Refund;
                var payercatno = (int)PayerCatType.NoCharge;
                // int Isaddminus = (int)AddMinusType.Minus;
                var backgroups = input.Select(o => o.Id);
                _customerItemGroupRepository.GetAll().Where(r => backgroups.Contains(r.Id)).Update(r =>
                    new TjlCustomerItemGroup
                    { MReceiptInfoPersonalId = null, PayerCat = payercatno, RefundState = refund });
            }

            return mReceiptInfoPerDto;
            // CurrentUnitOfWork.SaveChanges();
        }

        public decimal SmallChange(ICollection<GroupMoneyDto> ItemGroup, decimal PayMoney)
        {
            var smallChange = ItemGroup.Where(r => r.IsAddMinus >= 0).Sum(r => r.PriceAfterDis) - PayMoney;
            return smallChange;
        }

        private IQueryable<TjlCustomerReg> BuildQuery(ChargQueryCusDto input)
        {
            var query = _customerRegRepository.GetAll(); // .GetAllIncluding(m => m.Department);
            if (input != null)
            {
                //体检号
                if (!string.IsNullOrEmpty(input.CustomerBM))
                {
                    query = query.Where(m =>
                        m.CustomerBM == input.CustomerBM || m.Customer.Name == input.CustomerBM ||
                        m.Customer.IDCardNo == input.CustomerBM);
                }

                //体检日期
                if (input.NavigationStartTime != null && input.NavigationEndTime != null)
                {
                    input.NavigationStartTime = new DateTime(input.NavigationStartTime.Value.Year,
                        input.NavigationStartTime.Value.Month, input.NavigationStartTime.Value.Day, 00, 00, 00);
                    input.NavigationEndTime = new DateTime(input.NavigationEndTime.Value.Year,
                        input.NavigationEndTime.Value.Month, input.NavigationEndTime.Value.Day, 23, 59, 59);
                    query = query.Where(o =>
                        o.LoginDate > input.NavigationStartTime && o.LoginDate < input.NavigationEndTime);
                }
            }

            return query;
        }

        /// <summary>
        /// 获取收费作废
        /// </summary>
        /// <returns></returns>
        public List<MReceiptInfoDetailedViewDto> InsertInvalidReceiptDto(ChargeBM bM)
        {
            var mReceiptInfoDetailedViewDtos = new List<MReceiptInfoDetailedViewDto>();
            var user = _userRepository.Get(AbpSession.UserId.Value);
            var Receipt = _receiptInfoRepository.Get(bM.Id);
            Receipt.Id = Guid.NewGuid();
            Receipt.User = user;
            Receipt.UserId = AbpSession.UserId.Value;
            Receipt.ChargeDate = DateTime.Now;
            Receipt.ChargeState = 3;
            Receipt.CreationTime = DateTime.Now;
            Receipt.CreatorUserId = AbpSession.UserId.Value;

            // Receipt.
            return mReceiptInfoDetailedViewDtos;
        }
        /// <summary>
        /// 修改费用确认状态
        /// </summary>
        /// <param name="input"></param>
        public ClientRegDto UpConfirmState(ChargeBM input)
        {
            var ClientReg = _clientRegRepository.Get(input.Id);
            ClientReg.ConfirmState = int.Parse(input.Name);
            ClientReg = _clientRegRepository.Update(ClientReg);
            return ClientReg.MapTo<ClientRegDto>();
        }
        /// <summary>
        /// 体检业务查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public List<CustomerSFTypeDto> getCusStType(SearchSFTypeDto input)
        {
            var outlist = new List<CustomerSFTypeDto>();
            if (input.SeachType.HasValue && input.SeachType == 1)
            {

                var que = _customerRegRepository.GetAll().Where(p => p.RegisterState == 2);
                if (input.ClientRegID.HasValue)
                {
                    que = que.Where(o => o.ClientRegId == input.ClientRegID.Value);
                }
                if (input.StarDate.HasValue && input.EndDate.HasValue)
                {
                    var start = input.StarDate.Value.Date;
                    var end = input.EndDate.Value.Date.AddDays(1);
                    que = que.Where(o => o.LoginDate >= start && o.LoginDate < end);
                }
                if (input.MaxMoney.HasValue && input.MinMoney.HasValue)
                {
                    que = que.Where(o => (o.McusPayMoney.ClientMoney + o.McusPayMoney.PersonalPayMoney)
                    >= input.MinMoney.Value && (o.McusPayMoney.ClientMoney + o.McusPayMoney.PersonalPayMoney) <= input.MaxMoney.Value);
                }
                if (!string.IsNullOrWhiteSpace(input.SearchName))
                {
                    que = que.Where(o => o.ClientInfo.ClientName == input.SearchName || o.CustomerBM == input.SearchName || o.Customer.Name == input.SearchName || o.Customer.IDCardNo == input.SearchName);
                }
                if (!string.IsNullOrWhiteSpace(input.LinkName))
                {
                    //|| o.CustomerReg.Introducer==input.SearchName
                    que = que.Where(o => o.Introducer == input.LinkName);
                }
                if (input.UserType.HasValue)
                {
                    if (input.UserType.Value == 1)
                    {
                        que = que.Where(o => o.PersonnelCategory.IsFree == true || o.ClientReg.PersonnelCategory.IsFree == true);
                    }
                    else if (input.UserType.Value == 2)
                    {
                        que = que.Where(o => o.PersonnelCategory.IsFree == false
                        || o.ClientReg.PersonnelCategory.IsFree == false
                        || o.PersonnelCategory == null
                        || (o.ClientReg != null && o.ClientReg.PersonnelCategory == null));
                    }
                }
                int sfstate = (int)InvoiceStatus.Valid;
                var bis = _BasicDictionaries.GetAll().Where(p => p.Type == "LargeDepatType");

                //单位
                outlist = que.Where(p => p.ClientRegId != null).GroupBy(p => p.ClientRegId).Select(p =>
                    new CustomerSFTypeDto
                    {
                        Actualmoney = p.Select(n => n.McusPayMoney.ClientMoney).Sum(),
                        ChargeDate = p.Where(n => n.LoginDate != null).FirstOrDefault().LoginDate.Value,
                        ClientName = p.FirstOrDefault().ClientInfo.ClientName,
                        Count = p.Count(),
                        Customer = new GroupReport.Dto.GroupCustomerDto(),
                        CustomerReg = new CustomerSFTypeCusRegDto(),
                        User = new Users.Dto.UserViewDto
                        {
                            Name = p.FirstOrDefault().CustomerItemGroup.FirstOrDefault().BillEmployeeBM.Name
                        },
                        MReceiptDetailses = p.SelectMany(n => n.CustomerItemGroup).Where(n => (n.PayerCat == 3
                        || n.TTmoney > 0) && n.IsDeleted == false
                         && n.IsAddMinus != 3).Select(
                                  r => new MReceiptInfoDetailedViewDto
                                  {
                                      DepartmentCodeBM = r.DepartmentCodeBM,
                                      DepartmentId = r.DepartmentId,
                                      DepartmentName = r.DepartmentName,
                                      DepartmentOrder = r.DepartmentOrder,
                                      Discount = r.DiscountRate,
                                      GroupsDiscountMoney = r.TTmoney,
                                      GroupsMoney = r.ItemPrice,
                                      Id = r.Id,
                                      IsAddMinus = r.IsAddMinus,
                                      ItemGroupCodeBM = r.ItemGroupCodeBM,
                                      ItemGroupId = r.ItemGroupBM_Id.Value,
                                      ItemGroupName = r.ItemGroupName,
                                      ItemGroupOrder = r.ItemGroupOrder,
                                      MReceiptInfoID = p.Key.Value,
                                      PayerCat = r.PayerCat,
                                      ReceiptTypeName = (input.DepatType == 1 && bis.FirstOrDefault(n => n.Value == r.DepartmentBM.LargeDepart) != null) ? bis.FirstOrDefault(n => n.Value == r.DepartmentBM.LargeDepart).Text : r.ItemGroupBM.ChartName,
                                      SfTypeOrder = (input.DepatType == 1 && bis.FirstOrDefault(n => n.Value == r.DepartmentBM.LargeDepart) != null) ? bis.FirstOrDefault(n => n.Value == r.DepartmentBM.LargeDepart).Value : 0,
                                  }).ToList()


                    }).ToList();
                //CustomerSFTypeDto customerSFType = new CustomerSFTypeDto();
                //customerSFType.Actualmoney = outlist.Sum(p=>p.Actualmoney);

                //outlist.Add(customerSFType);
                //自费
                var cusoutlist = que.Where(p => p.ClientRegId == null).GroupBy(p => p.ClientRegId).Select(p => new CustomerSFTypeDto
                {
                    Actualmoney = p.Select(n => n.McusPayMoney.PersonalMoney).Sum(),
                    ChargeDate = p.Where(n => n.LoginDate != null).FirstOrDefault().LoginDate.Value,
                    ClientName = "个人",
                    Count = p.Count(),
                    Customer = new GroupReport.Dto.GroupCustomerDto(),
                    CustomerReg = new CustomerSFTypeCusRegDto(),
                    User = new Users.Dto.UserViewDto
                    {
                        Name = p.FirstOrDefault().CustomerItemGroup.FirstOrDefault().BillEmployeeBM.Name
                    },
                    MReceiptDetailses = p.SelectMany(n => n.CustomerItemGroup).Where(n => n.PayerCat != 3
                    && n.IsAddMinus != 3).Select(
                                  r => new MReceiptInfoDetailedViewDto
                                  {
                                      DepartmentCodeBM = r.DepartmentCodeBM,
                                      DepartmentId = r.DepartmentId,
                                      DepartmentName = r.DepartmentName,
                                      DepartmentOrder = r.DepartmentOrder,
                                      Discount = r.DiscountRate,
                                      GroupsDiscountMoney = r.GRmoney,
                                      GroupsMoney = r.ItemPrice,
                                      Id = r.Id,
                                      IsAddMinus = r.IsAddMinus,
                                      ItemGroupCodeBM = r.ItemGroupCodeBM,
                                      ItemGroupId = r.ItemGroupBM_Id.Value,
                                      ItemGroupName = r.ItemGroupName,
                                      ItemGroupOrder = r.ItemGroupOrder,
                                      PayerCat = r.PayerCat,
                                      ReceiptTypeName = (input.DepatType == 1 && bis.FirstOrDefault(n => n.Value == r.DepartmentBM.LargeDepart) != null) ? bis.FirstOrDefault(n => n.Value == r.DepartmentBM.LargeDepart).Text : r.ItemGroupBM.ChartName,
                                      SfTypeOrder = (input.DepatType == 1 && bis.FirstOrDefault(n => n.Value == r.DepartmentBM.LargeDepart) != null) ? bis.FirstOrDefault(n => n.Value == r.DepartmentBM.LargeDepart).Value : 0,

                                     //ReceiptTypeName = r.ItemGroupBM.ChartName,
                                     //SfTypeOrder = 0
                                 }).ToList()


                }).ToList();

                outlist.AddRange(cusoutlist);
                if (input.TTMoney.HasValue && input.TTMoney == 1)
                {
                    //团体自费
                    var cusoutZFlist = que.Where(p => p.McusPayMoney != null && p.ClientRegId != null
                     && p.McusPayMoney.PersonalMoney != null &&
                     p.McusPayMoney.PersonalMoney > 0).GroupBy(p => "个人").Select(p => new CustomerSFTypeDto
                     {
                         Actualmoney = p.Select(n => n.McusPayMoney.PersonalMoney).Sum(),
                         ChargeDate = p.Where(n => n.LoginDate != null).FirstOrDefault().LoginDate.Value,
                         ClientName = "单位自费",
                         Count = p.Count(),
                         Customer = new GroupReport.Dto.GroupCustomerDto(),
                         CustomerReg = new CustomerSFTypeCusRegDto(),
                         User = new Users.Dto.UserViewDto
                         {
                             Name = p.FirstOrDefault().CustomerItemGroup.FirstOrDefault().BillEmployeeBM.Name
                         },
                         MReceiptDetailses = p.SelectMany(n => n.CustomerItemGroup).Where(n => n.PayerCat != 3
                     && n.IsAddMinus != 3).Select(
                                               r => new MReceiptInfoDetailedViewDto
                                               {
                                                   DepartmentCodeBM = r.DepartmentCodeBM,
                                                   DepartmentId = r.DepartmentId,
                                                   DepartmentName = r.DepartmentName,
                                                   DepartmentOrder = r.DepartmentOrder,
                                                   Discount = r.DiscountRate,
                                                   GroupsDiscountMoney = r.GRmoney,
                                                   GroupsMoney = r.ItemPrice,
                                                   Id = r.Id,
                                                   IsAddMinus = r.IsAddMinus,
                                                   ItemGroupCodeBM = r.ItemGroupCodeBM,
                                                   ItemGroupId = r.ItemGroupBM_Id.Value,
                                                   ItemGroupName = r.ItemGroupName,
                                                   ItemGroupOrder = r.ItemGroupOrder,
                                                   PayerCat = r.PayerCat,
                                                   ReceiptTypeName = (input.DepatType == 1 && bis.FirstOrDefault(n => n.Value == r.DepartmentBM.LargeDepart) != null) ? bis.FirstOrDefault(n => n.Value == r.DepartmentBM.LargeDepart).Text : r.ItemGroupBM.ChartName,
                                                   SfTypeOrder = (input.DepatType == 1 && bis.FirstOrDefault(n => n.Value == r.DepartmentBM.LargeDepart) != null) ? bis.FirstOrDefault(n => n.Value == r.DepartmentBM.LargeDepart).Value : 0,

                                                   //ReceiptTypeName = r.ItemGroupBM.ChartName,
                                                   //SfTypeOrder = 0
                                               }).ToList()


                     }).ToList();
                    outlist.AddRange(cusoutZFlist);
                }
            }
            else
            {
                var que = _receiptInfoRepository.GetAllIncluding(o => o.Customer, o => o.MReceiptDetailses, o => o.User);
                if (input.ClientRegID.HasValue)
                {
                    que = que.Where(o => o.ClientRegId == input.ClientRegID.Value);
                }
                if (input.StarDate.HasValue && input.EndDate.HasValue)
                {
                    var start = input.StarDate.Value.Date;
                    var end = input.EndDate.Value.Date.AddDays(1);
                    que = que.Where(o => o.ChargeDate >= start && o.ChargeDate < end);
                }
                if (input.MaxMoney.HasValue && input.MinMoney.HasValue)
                {
                    que = que.Where(o => o.Actualmoney >= input.MinMoney.Value && o.Actualmoney <= input.MaxMoney.Value);
                }
                if (!string.IsNullOrWhiteSpace(input.SearchName))
                {
                    que = que.Where(o => o.ClientName == input.SearchName || o.CustomerReg.CustomerBM == input.SearchName || o.Customer.Name == input.SearchName || o.Customer.IDCardNo == input.SearchName);
                }
                if (!string.IsNullOrWhiteSpace(input.LinkName))
                {
                    //|| o.CustomerReg.Introducer==input.SearchName
                    que = que.Where(o => o.CustomerReg.Introducer == input.LinkName);
                }
                if (input.UserType.HasValue)
                {
                    if (input.UserType.Value == 1)
                    {
                        que = que.Where(o => o.CustomerReg.PersonnelCategory.IsFree == true || o.ClientReg.PersonnelCategory.IsFree == true);
                    }
                    else if (input.UserType.Value == 2)
                    {
                        que = que.Where(o => o.CustomerReg.PersonnelCategory.IsFree == false || o.ClientReg.PersonnelCategory.IsFree == false || o.CustomerReg != null && o.CustomerReg.PersonnelCategory == null || o.ClientReg != null && o.ClientReg.PersonnelCategory == null);
                    }
                }
                int sfstate = (int)InvoiceStatus.Valid;
                que = que.Where(o => o.ReceiptSate == sfstate);
                outlist = que.MapTo<List<CustomerSFTypeDto>>();
                if (input.DepatType == 1)
                {
                    var bis = _BasicDictionaries.GetAll().Where(p => p.Type == "LargeDepatType").ToList();
                    var depat = _departmentRepository.GetAll();
                    for (int i = 0; i < outlist.Count; i++)
                    {
                        var list = outlist[i].MReceiptDetailses.ToList();
                        for (int n = 0; n < list.Count; n++)
                        {
                            var departID = list[n].DepartmentId;
                            var grouoId = list[n].ItemGroupId;
                            var depatment = depat.FirstOrDefault(p => p.Id == departID);
                            if (depatment.LargeDepart.HasValue)
                            {
                                var bigType = bis.FirstOrDefault(p => p.Value == depatment.LargeDepart.Value);

                                var ent = outlist[i].MReceiptDetailses.FirstOrDefault(p => p.DepartmentId == departID && p.ItemGroupId == grouoId);

                                if (bigType != null)
                                {

                                    ent.ReceiptTypeName = bigType.Text;
                                    ent.SfTypeOrder = bigType.Value;
                                }
                            }

                        }
                    }
                }
            }


            return outlist;
        }

        /// 体检业务查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public List<CustomerSFTypeDto> getCusDepartType(SearchSFTypeDto input)
        {
            var que = _receiptInfoRepository.GetAllIncluding(o => o.Customer, o => o.MReceiptDetailses, o => o.User);
            if (input.ClientRegID.HasValue)
            {
                que = que.Where(o => o.ClientRegId == input.ClientRegID.Value);
            }
            if (input.StarDate.HasValue && input.EndDate.HasValue)
            {
                var start = input.StarDate.Value.Date;
                var end = input.EndDate.Value.Date.AddDays(1);
                que = que.Where(o => o.ChargeDate >= start && o.ChargeDate < end);
            }
            if (input.MaxMoney.HasValue && input.MinMoney.HasValue)
            {
                que = que.Where(o => o.Actualmoney >= input.MinMoney.Value && o.Actualmoney <= input.MaxMoney.Value);
            }
            if (!string.IsNullOrWhiteSpace(input.SearchName))
            {
                que = que.Where(o => o.ClientName == input.SearchName || o.CustomerReg.CustomerBM == input.SearchName || o.Customer.Name == input.SearchName || o.Customer.IDCardNo == input.SearchName);
            }
            if (!string.IsNullOrWhiteSpace(input.LinkName))
            {
                //|| o.CustomerReg.Introducer==input.SearchName
                que = que.Where(o => o.CustomerReg.Introducer == input.LinkName);
            }
            if (input.UserType.HasValue)
            {
                if (input.UserType.Value == 1)
                {
                    que = que.Where(o => o.CustomerReg.PersonnelCategory.IsFree == true || o.ClientReg.PersonnelCategory.IsFree == true);
                }
                else if (input.UserType.Value == 2)
                {
                    que = que.Where(o => o.CustomerReg.PersonnelCategory.IsFree == false || o.ClientReg.PersonnelCategory.IsFree == false || o.CustomerReg != null && o.CustomerReg.PersonnelCategory == null || o.ClientReg != null && o.ClientReg.PersonnelCategory == null);
                }
            }
            int sfstate = (int)InvoiceStatus.Valid;
            que = que.Where(o => o.ReceiptSate == sfstate);
            //var cus = que.MapTo<List<CustomerSFTypeDto>>();
            var outlist = que.MapTo<List<CustomerSFTypeDto>>();
            if (input.DepatType == 1)
            {
                var bis = _BasicDictionaries.GetAll().Where(p => p.Type == "LargeDepatType").ToList();
                var depat = _departmentRepository.GetAll();
                for (int i = 0; i < outlist.Count; i++)
                {
                    var list = outlist[i].MReceiptDetailses.ToList();
                    for (int n = 0; n < list.Count; n++)
                    {
                        var departID = list[n].DepartmentId;
                        var grouoId = list[n].ItemGroupId;
                        var depatment = depat.FirstOrDefault(p => p.Id == departID);
                        var bigType = bis.FirstOrDefault(p => p.Value == depatment.LargeDepart.Value);
                        var ent = outlist[i].MReceiptDetailses.FirstOrDefault(p => p.DepartmentId == departID && p.ItemGroupId == grouoId);

                        if (depatment.LargeDepart.HasValue)
                        {

                            ent.ReceiptTypeName = bigType.Text;
                            ent.SfTypeOrder = bigType.Value;
                        }
                        else
                        {
                            ent.ReceiptTypeName = depatment.Name;
                            ent.SfTypeOrder = depatment.OrderNum;
                        }
                    }
                }
            }
            return outlist;
        }
        /// <summary>
        /// 单位收费信息
        /// </summary>
        /// <returns></returns>
        [UnitOfWork(false)]
        public List<ClientPaymentDto> getClientPayment(SearchSFTypeDto input)
        {
            var que = _clientRegRepository.GetAllIncluding(o => o.ClientInfo, o => o.CustomerReg, o => o.MReceiptInfo);

            if (input.ClientRegID.HasValue)
            {
                que = que.Where(o => o.Id == input.ClientRegID.Value);
            }
            if (input.StarDate.HasValue && input.EndDate.HasValue)
            {
                var start = input.StarDate.Value.Date;
                var end = input.EndDate.Value.Date.AddDays(1);
                que = que.Where(o => o.StartCheckDate >= start && o.StartCheckDate < end);
            }
            if (!string.IsNullOrWhiteSpace(input.LinkName))
            {
                que = que.Where(o => o.linkMan == input.LinkName);
            }
            if (input.UserID.HasValue)
            {
                que = que.Where(o => o.ClientInfo.user.Id == input.UserID.Value);

            }

            var clientlis = que.MapTo<List<ClientPaymentDto>>();
            if (!string.IsNullOrWhiteSpace(input.SearchName))
            {
                clientlis = clientlis.Where(o => o.ChageSt == input.SearchName).ToList();
            }
            return clientlis;
        }
        /// <summary>
        /// 获取发票
        /// </summary>
        /// <param name="input"></param>
        public MInvoiceRecordDto PrintInvoiceId(ChargeBM input)
        {
            var invoiceList = _TjlMInvoiceRecord.FirstOrDefault(o => o.MReceiptInfoId == input.Id);


            return invoiceList.MapTo<MInvoiceRecordDto>();
        }

        /// <summary>
        /// 获取发票
        /// </summary>
        /// <param name="input"></param>
        public MInvoiceRecordDto PrintInvoiceNum(ChargeBM input)
        {
            var invoiceList = _TjlMInvoiceRecord.FirstOrDefault(o => o.InvoiceNum == input.Name);


            return invoiceList.MapTo<MInvoiceRecordDto>();
        }
        [UnitOfWork(false)]
        public List<TJSQDto> HISChargstate(SqlWhereDto input)
        {
            List<TJSQ> tJSQs = new List<TJSQ>();
            // 获取接口数据
            var hisInterfaceDriver = DriverFactory.GetDriver<IHisInterfaceDriver>();
            SqlWhere sqlWhere = new SqlWhere();
            //var search = input.MapTo<InCarNum>();
            //input.MapTo(sqlWhere);
            sqlWhere.CustomerBM = input.CustomerBM;
            sqlWhere.DateEndTime = input.DateEndTime;
            sqlWhere.DateStartTime = input.DateStartTime;
            sqlWhere.Name = input.Name;
            sqlWhere.SQDH = input.SQDH;
            var typ = _BasicDictionaries.GetAll().FirstOrDefault(o => o.Type == "HisInterface" && o.Value == 2);
            sqlWhere.HisName = typ.Remarks;
            var interfaceResult = hisInterfaceDriver.searchTJR(sqlWhere);
            foreach (TJSQ tj in interfaceResult)
            {
                bool isok = false;
                if (tj.HCZT == 2)
                {
                    isok = HISChargeZF(tj);
                    if (isok)
                    {
                        tJSQs.Add(tj);
                        InCarNum inCarNum = new InCarNum();
                        inCarNum.CardNum = tj.SQDH;
                        inCarNum.HISName = typ.Remarks;
                        hisInterfaceDriver.upSfCharg(inCarNum);
                    }
                    else
                    {
                        tj.HCZT = 3;
                        tJSQs.Add(tj);
                    }
                }
                else
                {

                    isok = HISCharge(tj);
                    if (isok)
                    {
                        tJSQs.Add(tj);
                        InCarNum inCarNum = new InCarNum();
                        inCarNum.CardNum = tj.SQDH;
                        inCarNum.HISName = typ.Remarks;
                        hisInterfaceDriver.upSfCharg(inCarNum);
                    }
                    else
                    {
                        tj.HCZT = 3;
                        tJSQs.Add(tj);
                    }
                }
                //  Thread.Sleep(2000);

            }
            return tJSQs.MapTo<List<TJSQDto>>();
        }
        /// <summary>
        /// 
        /// 
        /// 
        /// 收费分布1
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public List<TJSQDto> HISChargsList(SqlWhereDto input)
        {

            var hisInterfaceDriver = DriverFactory.GetDriver<IHisInterfaceDriver>();

            SqlWhere sqlWhere = new SqlWhere();
            //var search = input.MapTo<InCarNum>();
            //input.MapTo(sqlWhere);
            sqlWhere.CustomerBM = input.CustomerBM;
            sqlWhere.DateEndTime = input.DateEndTime;
            sqlWhere.DateStartTime = input.DateStartTime;
            sqlWhere.Name = input.Name;
            sqlWhere.SQDH = input.SQDH;
            var typ = _BasicDictionaries.GetAll().FirstOrDefault(o => o.Type == "HisInterface" && o.Value == 2);
            sqlWhere.HisName = typ.Remarks;
            var interfaceResult = hisInterfaceDriver.searchTJR(sqlWhere);
            return interfaceResult.MapTo<List<TJSQDto>>();

        }
        /// <summary>
        /// HIS收费分布2
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public List<TJSQDto> HISChargstate(List<TJSQDto> interfaceResult)
        {
            var typ = _BasicDictionaries.GetAll().FirstOrDefault(o => o.Type == "HisInterface" && o.Value == 2);

            List<TJSQ> tJSQs = new List<TJSQ>();
            // 获取接口数据
            var hisInterfaceDriver = DriverFactory.GetDriver<IHisInterfaceDriver>();
            var relist = interfaceResult.MapTo<List<TJSQ>>();
            foreach (TJSQ tj in relist)
            {
                bool isok = false;
                if (tj.HCZT == 2)
                {
                    isok = HISChargeZF(tj);
                    if (isok)
                    {
                        tJSQs.Add(tj);
                        InCarNum inCarNum = new InCarNum();
                        inCarNum.CardNum = tj.SQDH;
                        inCarNum.HISName = typ.Remarks;
                        hisInterfaceDriver.upSfCharg(inCarNum);
                    }
                    else
                    {
                        tj.HCZT = 3;
                        tJSQs.Add(tj);
                    }
                }
                else
                {

                    isok = HISCharge(tj);
                    if (isok)
                    {
                        tJSQs.Add(tj);
                        InCarNum inCarNum = new InCarNum();
                        inCarNum.CardNum = tj.SQDH;
                        inCarNum.HISName = typ.Remarks;
                        hisInterfaceDriver.upSfCharg(inCarNum);
                    }
                    else
                    {
                        tj.HCZT = 3;
                        tJSQs.Add(tj);
                    }
                }
                //  Thread.Sleep(2000);

            }
            return tJSQs.MapTo<List<TJSQDto>>();
        }
        /// <summary>
        /// HIS收费
        /// </summary>
        /// <param name="Hisinput"></param>
        [UnitOfWork(false)]
        private bool HISCharge(TJSQ hisinput)
        {
            try
            {
                var application = _TjlApplicationForm.FirstOrDefault(o => o.SQDH == hisinput.SQDH && o.SQSTATUS == 1);
                if (application == null)
                {
                    return false;
                }
                if (application.ClientRegId.HasValue)
                {
                    return HISTTCharge(hisinput);
                }
                CreateReceiptInfoDto input = new CreateReceiptInfoDto();
                input.Actualmoney = application.FYZK;
                input.ChargeDate = System.DateTime.Now;
                input.ChargeState = (int)InvoiceStatus.NormalCharge;
                var ChargeInfoPerSion = _customerRegRepository.FirstOrDefault(o => o.CustomerBM == application.CustomerReg.CustomerBM);
                input.CustomerRegid = ChargeInfoPerSion.Id;//关联已有对象                

                input.DiscontReason = "";
                input.Discount = 1;
                input.ReceiptSate = (int)InvoiceStatus.Valid;
                input.Remarks = "";
                input.SettlementSate = (int)ReceiptState.UnSettled;
                input.Shouldmoney = ChargeInfoPerSion.McusPayMoney.PersonalMoney;
                input.Summoney = ChargeInfoPerSion.McusPayMoney.PersonalMoney;
                input.TJType = 2;

                input.Userid = application.CreatorUserId.Value;

                //支付方式   
                List<CreatePaymentDto> CreatePayments = new List<CreatePaymentDto>();
                var payment = _chargeTypeRepository.FirstOrDefault(o => o.ChargeName == "门诊");
                CreatePaymentDto CreatePayment = new CreatePaymentDto();
                CreatePayment.Actualmoney = application.FYZK;
                CreatePayment.CardNum = "";//暂时不支持会员卡
                CreatePayment.Discount = 1;
                CreatePayment.MChargeTypeId = payment.Id;
                CreatePayment.Shouldmoney = application.FYZK;
                CreatePayments.Add(CreatePayment);

                input.MPaymentr = CreatePayments;
                //未收费项目集合
                List<CreateMReceiptInfoDetailedDto> CreateMReceiptInfoDetaileds = new List<CreateMReceiptInfoDetailedDto>();

                var cusPerGroups = ChargeInfoPerSion.CustomerItemGroup?.Where(r => r.IsAddMinus != 3 && r.PayerCat == 1 && r.TTmoney <= 0);
                //var ChargeGroups = cusPerGroups?.GroupBy(r => r.SFType);
                foreach (var ChargeGroup in cusPerGroups)
                {
                    CreateMReceiptInfoDetailedDto CreateMReceiptInfoDetailed = new CreateMReceiptInfoDetailedDto();
                    CreateMReceiptInfoDetailed.GroupsMoney = ChargeGroup.ItemPrice;
                    CreateMReceiptInfoDetailed.GroupsDiscountMoney = ChargeGroup.GRmoney;
                    if (CreateMReceiptInfoDetailed.GroupsMoney != 0)
                    {
                        CreateMReceiptInfoDetailed.Discount = CreateMReceiptInfoDetailed.GroupsDiscountMoney / CreateMReceiptInfoDetailed.GroupsMoney;
                    }
                    else
                    {
                        CreateMReceiptInfoDetailed.Discount = 1;
                    }
                    if (ChargeGroup.SFType.HasValue)
                        CreateMReceiptInfoDetailed.ReceiptTypeName = _BasicDictionaries.FirstOrDefault(o => o.Value == ChargeGroup.SFType.Value && o.Type == "ChargeCategory").Text;

                    else
                        CreateMReceiptInfoDetailed.ReceiptTypeName = "";
                    //组合
                    CreateMReceiptInfoDetailed.ItemGroupId = ChargeGroup.Id;
                    CreateMReceiptInfoDetaileds.Add(CreateMReceiptInfoDetailed);
                }

                input.MReceiptInfoDetailedgr = CreateMReceiptInfoDetaileds;
                //保存收费记录
                Guid receiptInfoDtoID = InsertReceiptInfoDto(input);
                CurrentUnitOfWork.SaveChanges();
                //更新体检人收费状态及组合结算状态
                UpdateChargeStateDto updateChargeStateDto = new UpdateChargeStateDto();
                var cusGroups = cusPerGroups;
                var CusGroupsIds = cusGroups.Select(r => r.Id);
                var CusRegs = cusGroups.Where(r => r.CustomerRegBMId.HasValue).Select(r => r.CustomerRegBMId);
                updateChargeStateDto.CusGroupids = CusGroupsIds.ToList();
                updateChargeStateDto.CusRegids = CusRegs.Distinct().Cast<Guid>().ToList();
                updateChargeStateDto.ReceiptID = receiptInfoDtoID;
                updateChargeStateDto.CusType = 1;
                updateClientChargeState(updateChargeStateDto);

                //更新费用表
                SearchPayMoneyDto inputcs = new SearchPayMoneyDto();
                inputcs.Id = ChargeInfoPerSion.Id;
                inputcs.PayMoney = application.FYZK;
                inputcs.DistMoney = 0;
                CusPayMoneyViewDto cusPayMoneyViewDto = UpCusMoney(inputcs);
                application.SQSTATUS = 2;
                application.MReceiptInfoId = receiptInfoDtoID;
                application.BRSFH = hisinput.BRSFH;
                application.FPH = hisinput.BRFPH;
                application.HCZT = 1;
                _TjlApplicationForm.Update(application);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        /// <summary>
        /// HIS收费团体
        /// </summary>
        /// <param name="Hisinput"></param>
        public bool HISTTCharge(TJSQ hisinput)
        {
            try
            {
                var application = _TjlApplicationForm.FirstOrDefault(o => o.SQDH == hisinput.SQDH && (o.SQSTATUS == 1 || o.SQSTATUS == 4));
                if (application == null)
                {
                    return false;
                }
                var input = new CreateReceiptInfoDto();
                input.ClientRegid = application.ClientRegId; //关联已有对象
                if (hisinput.FYZK.HasValue && hisinput.FYZK > 0)
                { input.Actualmoney = hisinput.FYZK.Value; }
                else
                {
                    input.Actualmoney = application.FYZK;
                }

                input.ChargeDate = System.DateTime.Now;
                int chargestate = (int)InvoiceStatus.NormalCharge;
                input.ChargeState = chargestate;
                input.Discontmoney = 0;
                input.DiscontReason = "";
                input.Discount = 1;
                int receiptstate = (int)InvoiceStatus.Valid;
                input.ReceiptSate = receiptstate;
                input.Remarks = "";
                int settlementsate = (int)ReceiptState.UnSettled;
                input.SettlementSate = settlementsate;
                input.Shouldmoney = application.FYZK;
                input.Summoney = application.FYZK;
                input.TJType = 1;
                input.Userid = application.CreatorUserId.Value;

                //支付方式记录集合 

                //支付方式   
                List<CreatePaymentDto> CreatePayments = new List<CreatePaymentDto>();
                var payment = _chargeTypeRepository.FirstOrDefault(o => o.ChargeName == "门诊");
                CreatePaymentDto CreatePayment = new CreatePaymentDto();
                if (hisinput.FYZK.HasValue && hisinput.FYZK > 0)
                {
                    CreatePayment.Actualmoney = hisinput.FYZK.Value;
                }
                else
                {
                    CreatePayment.Actualmoney = application.FYZK;
                }
                CreatePayment.CardNum = "";//暂时不支持会员卡
                CreatePayment.Discount = 1;
                CreatePayment.MChargeTypeId = payment.Id;
                if (hisinput.FYZK.HasValue && hisinput.FYZK > 0)
                {
                    CreatePayment.Shouldmoney = hisinput.FYZK.Value;
                }
                else
                {
                    CreatePayment.Shouldmoney = application.FYZK;
                }
                CreatePayments.Add(CreatePayment);

                input.MPaymentr = CreatePayments;
                input.ApplicationFormId = application.Id;
                //保存收费记录
                var receiptInfoDtoID = InsertReceiptInfoDto(input);
                //新增加回款金额
                if (application.REFYZK.HasValue)
                {
                    application.REFYZK = application.REFYZK + input.Actualmoney;
                }
                else
                { application.REFYZK = input.Actualmoney; }
                if (application.REFYZK == application.FYZK)
                {
                    application.SQSTATUS = 2;
                }
                else
                { application.SQSTATUS = 4; }
                //新增加最后回款日志
                application.RETIME = System.DateTime.Now;
                application.MReceiptInfoId = receiptInfoDtoID;
                application.BRSFH = hisinput.BRSFH;
                application.FPH = hisinput.BRFPH;
                application.HCZT = 1;
                _TjlApplicationForm.Update(application);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        /// <summary>
        /// HIS收费团体
        /// </summary>
        /// <param name="Hisinput"></param>

        /// <summary>
        /// 作废HIS收费
        /// </summary>
        [UnitOfWork(false)]
        private bool HISChargeZF(TJSQ Hisinput)
        {
            var guid = _TjlApplicationForm.FirstOrDefault(o => o.SQDH == Hisinput.SQDH && o.SQSTATUS == 2);
            if (guid == null)
            {
                return false;
            }
            GuIdDto entityDto = new GuIdDto();
            entityDto.Id = guid.MReceiptInfoId.Value;
            InsertInvalidReceiptInfoDto(entityDto);
            guid.HCZT = 1;
            guid.SQSTATUS = 3;
            _TjlApplicationForm.Update(guid);
            return true;
        }
        #region HIS收费状态回传接口
        /// <summary>
        /// HIS收费
        /// </summary>
        /// <param name="Hisinput"></param>
        [UnitOfWork(false)]
        public OutMessDto HISGroupChageStateBak(HIsInput inputAll)
        {
            OutMessDto outMess = new OutMessDto();
            string mess = "";
            foreach (var input in inputAll.GroupSQDH)
            {
                try
                {

                    if (string.IsNullOrEmpty(input.SQDH))
                    {
                        mess += "申请单号不能为空！";
                        outMess.code = "0";
                        outMess.mess = mess;
                        return outMess;
                    }
                    if (input.SQSTATUS != 1 && input.SQSTATUS != 2)
                    {
                        outMess.code = "0";
                        outMess.mess = "收费状态必须是1或2！";
                        return outMess;
                    }
                    var payercat = (int)PayerCatType.PersonalCharge;
                    var coststate = (int)PayerCatType.Charge;
                    if (input.SQSTATUS == 2)
                    { payercat = (int)PayerCatType.NoCharge; }
                    var cusGrouplist = _customerItemGroupRepository.GetAll().Where(p =>
                    p.ApplicationNo == input.SQDH || p.fee_no == input.SQDH).ToList();


                    if (cusGrouplist.Count > 0)
                    {
                        foreach (var cusGroup in cusGrouplist)
                        {
                            cusGroup.PayerCat = payercat;
                            _customerItemGroupRepository.Update(cusGroup);
                            if (payercat == (int)PayerCatType.PersonalCharge)
                            {
                                _customerRegRepository.GetAll().Where(r => r.Id == cusGroup.CustomerRegBMId)
                                    .Update(r => new TjlCustomerReg { CostState = coststate });
                            }
                            else
                            {
                                //组合都没收费则为未收费状态
                                if (!_customerItemGroupRepository.GetAll().Any(p => p.IsAddMinus != (int)AddMinusType.Minus
                                 && p.PayerCat == (int)PayerCatType.PersonalCharge))
                                {
                                    _customerRegRepository.GetAll().Where(r => r.Id == cusGroup.CustomerRegBMId)
                                       .Update(r => new TjlCustomerReg { CostState = payercat });
                                }
                            }
                            //不走HIS收费状态处理
                            if (payercat == (int)PayerCatType.PersonalCharge)
                            {
                                var BasItemGrupName = _BasicDictionaries.FirstOrDefault(p => p.Type == "HisInterface" && p.Value == 3);
                                if (BasItemGrupName != null && !string.IsNullOrEmpty(BasItemGrupName.Remarks))
                                {
                                    var ItemGrupNameList = BasItemGrupName.Remarks.Replace("，", "").Split(',');
                                    var ONcusGroup = _customerItemGroupRepository.GetAll().Where(p => p.CustomerRegBMId == cusGroup.CustomerRegBMId &&
                                   ItemGrupNameList.Contains(p.ItemGroupName) && p.IsAddMinus != (int)AddMinusType.Minus
                                   && p.PayerCat == (int)PayerCatType.NoCharge && p.PriceAfterDis == 0).ToList();
                                    foreach (var cusItemGroup in ONcusGroup)
                                    {
                                        cusItemGroup.PayerCat = payercat;
                                        _customerItemGroupRepository.Update(cusItemGroup);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        #region 判断下是否是团体申请单
                        var pay = _receiptInfoRepository.FirstOrDefault(p => p.xsdjbh == input.SQDH);
                        if (pay != null)
                        {
                            pay.returnCode = input.SQSTATUS.ToString();
                            if (input.SQSTATUS == 1)
                            {
                                pay.returnMsg = "收费";
                            }
                            else
                            { pay.returnMsg = "退费"; }
                            _receiptInfoRepository.Update(pay);
                        }
                        #endregion
                        outMess.code = "0";
                        mess += "没有该申请单号:" + input.SQDH;
                        outMess.mess = mess;
                        //return outMess;
                    }

                }
                catch (Exception ex)
                {
                    outMess.code = "0";
                    outMess.mess = "程序异常" + ex.Message;
                    return outMess;
                }
            }
            if (mess == "")
            {
                outMess.code = "1";
                outMess.mess = "修改收费状态成功！";
                return outMess;
            }
            else
            {
                outMess.code = "0";
                outMess.mess = mess;
                return outMess;
            }

        }
        /// <summary>
        /// HIS收费
        /// </summary>
        /// <param name="Hisinput"></param>
        [UnitOfWork(false)]
        public OutMessDto HISGroupChageState(HIsInput inputAll)
        {
            OutMessDto outMess = new OutMessDto();
            string mess = "";
            #region 不用
            //foreach (var input in inputAll.GroupSQDH)
            //{
            //    try
            //    {

            //        if (string.IsNullOrEmpty(input.SQDH))
            //        {
            //            mess += "申请单号不能为空！";
            //            outMess.code = "0";
            //            outMess.mess = mess;
            //            return outMess;
            //        }
            //        if (input.SQSTATUS != 1 && input.SQSTATUS != 2)
            //        {
            //            outMess.code = "0";
            //            outMess.mess = "收费状态必须是1或2！";
            //            return outMess;
            //        }
            //        var payercat = (int)PayerCatType.PersonalCharge;
            //        var coststate = (int)PayerCatType.Charge;
            //        if (input.SQSTATUS == 2)
            //        { payercat = (int)PayerCatType.NoCharge; }
            //        var cusGrouplist = _customerItemGroupRepository.GetAll().Where(p =>
            //        p.ApplicationNo == input.SQDH || p.fee_no == input.SQDH).ToList();


            //        if (cusGrouplist.Count > 0)
            //        {
            //            foreach (var cusGroup in cusGrouplist)
            //            {
            //                cusGroup.PayerCat = payercat;
            //                _customerItemGroupRepository.Update(cusGroup);
            //                if (payercat == (int)PayerCatType.PersonalCharge)
            //                {
            //                    _customerRegRepository.GetAll().Where(r => r.Id == cusGroup.CustomerRegBMId)
            //                        .Update(r => new TjlCustomerReg { CostState = coststate });
            //                }
            //                else
            //                {
            //                    //组合都没收费则为未收费状态
            //                    if (!_customerItemGroupRepository.GetAll().Any(p => p.IsAddMinus != (int)AddMinusType.Minus
            //                     && p.PayerCat == (int)PayerCatType.PersonalCharge))
            //                    {
            //                        _customerRegRepository.GetAll().Where(r => r.Id == cusGroup.CustomerRegBMId)
            //                           .Update(r => new TjlCustomerReg { CostState = payercat });
            //                    }
            //                }
            //                //不走HIS收费状态处理
            //                if (payercat == (int)PayerCatType.PersonalCharge)
            //                {
            //                    var BasItemGrupName = _BasicDictionaries.FirstOrDefault(p => p.Type == "HisInterface" && p.Value == 3);
            //                    if (BasItemGrupName != null && !string.IsNullOrEmpty(BasItemGrupName.Remarks))
            //                    {
            //                        var ItemGrupNameList = BasItemGrupName.Remarks.Replace("，", "").Split(',');
            //                        var ONcusGroup = _customerItemGroupRepository.GetAll().Where(p => p.CustomerRegBMId == cusGroup.CustomerRegBMId &&
            //                       ItemGrupNameList.Contains(p.ItemGroupName) && p.IsAddMinus != (int)AddMinusType.Minus
            //                       && p.PayerCat == (int)PayerCatType.NoCharge && p.PriceAfterDis == 0).ToList();
            //                        foreach (var cusItemGroup in ONcusGroup)
            //                        {
            //                            cusItemGroup.PayerCat = payercat;
            //                            _customerItemGroupRepository.Update(cusItemGroup);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            #region 判断下是否是团体申请单
            //            var pay = _receiptInfoRepository.FirstOrDefault(p => p.xsdjbh == input.SQDH);
            //            if (pay != null)
            //            {
            //                pay.returnCode = input.SQSTATUS.ToString();
            //                if (input.SQSTATUS == 1)
            //                {
            //                    pay.returnMsg = "收费";
            //                }
            //                else
            //                { pay.returnMsg = "退费"; }
            //                _receiptInfoRepository.Update(pay);
            //            }
            //            #endregion
            //            outMess.code = "0";
            //            mess += "没有该申请单号:" + input.SQDH;
            //            outMess.mess = mess;
            //            //return outMess;
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        outMess.code = "0";
            //        outMess.mess = "程序异常" + ex.Message;
            //        return outMess;
            //    }
            //}

            #endregion
            var SQKList = inputAll.GroupSQDH.Select(p => p.SQDH).ToList();

            var cusGrouplist = _customerItemGroupRepository.GetAll().Where(p => p.IsAddMinus !=
            (int)AddMinusType.Minus).Where(p =>
                 SQKList.Contains(p.ApplicationNo) || SQKList.Contains(p.fee_no)).ToList();
            var cusGroup = cusGrouplist.FirstOrDefault();
            if (cusGroup == null)
            {
                mess += "没有该申请单号！";
                outMess.code = "0";
                outMess.mess = mess;
                return outMess;
            }
            //收费
            if (inputAll.GroupSQDH.FirstOrDefault()?.SQSTATUS == 1)
            {
                var BasItemGrupName = _BasicDictionaries.FirstOrDefault(p => p.Type == "HisInterface" && p.Value == 3);
                if (BasItemGrupName != null && !string.IsNullOrEmpty(BasItemGrupName.Remarks))
                {
                    var ItemGrupNameList = BasItemGrupName.Remarks.Replace("，", "").Split(',');
                    var ONcusGroup = _customerItemGroupRepository.GetAll().Where(p => p.CustomerRegBMId == cusGroup.CustomerRegBMId &&
                   ItemGrupNameList.Contains(p.ItemGroupName) && p.IsAddMinus != (int)AddMinusType.Minus
                   && p.PayerCat == (int)PayerCatType.NoCharge && p.PriceAfterDis == 0).ToList();
                    cusGrouplist.AddRange(ONcusGroup);
                }
                CreateReceiptInfoDto input = new CreateReceiptInfoDto();
                decimal ReturnMoney = 0;     //抹零金额    
                input.Discontmoney = 0;
                var ysmoney = cusGrouplist.Sum(p => p.PriceAfterDis);
                input.Actualmoney = cusGrouplist.Sum(p => p.PriceAfterDis);

                input.ChargeDate = System.DateTime.Now;
                input.ChargeState = (int)InvoiceStatus.NormalCharge;

                input.CustomerRegid = cusGroup.CustomerRegBMId;//关联已有对象                

                input.DiscontReason = "";
                input.Discount = 1;
                input.ReceiptSate = (int)InvoiceStatus.Valid;
                input.Remarks = "门诊支付";
                input.SettlementSate = (int)ReceiptState.UnSettled;
                input.Shouldmoney = ysmoney;
                input.Summoney = ysmoney;
                int tjtype = 2;
                input.TJType = tjtype;
                input.Userid = AbpSession.UserId.Value;

                //支付方式
                List<CreatePaymentDto> CreatePayments = new List<CreatePaymentDto>();
                var payment = _chargeTypeRepository
                    .FirstOrDefault(o => o.ChargeName.Contains("门诊支付"));
                //没有微信支付则新建
                if (payment == null)
                {
                    payment = new TbmMChargeType();
                    payment.Id = Guid.NewGuid();
                    payment.AccountingState = 2;
                    payment.ChargeApply = 3;
                    payment.ChargeCode = 100;
                    payment.ChargeName = "门诊支付";
                    payment.HelpChar = "MZ";
                    payment.OrderNum = 100;
                    payment.PrintName = 3;
                    payment.Remarks = "";
                    payment = _chargeTypeRepository.Insert(payment);

                }
                CreatePaymentDto CreatePayment = new CreatePaymentDto();
                CreatePayment.Actualmoney = ysmoney;
                CreatePayment.CardNum = "";//卡号
                CreatePayment.Discount = 1;
                CreatePayment.MChargeTypeId = payment.Id;
                CreatePayment.Shouldmoney = ysmoney;
                CreatePayment.MChargeTypename = payment.ChargeName;
                CreatePayments.Add(CreatePayment);

                input.MPaymentr = CreatePayments;
                //未收费项目集合
                List<CreateMReceiptInfoDetailedDto> CreateMReceiptInfoDetaileds = new List<CreateMReceiptInfoDetailedDto>();

                var cusPerGroups = cusGrouplist;
                //var ChargeGroups = cusPerGroups?.GroupBy(r => r.SFType);
                foreach (var itemgroup in cusPerGroups)
                {
                    //var itemgroup = _jlCustomerItemGroup.FirstOrDefault(o => o.ItemGroupBM.ItemGroupBM == ChargeGroup.ItemGroupBM && o.CustomerRegBMId == cusreg.Id);
                    // var itemgroup = _bmItemGroup.FirstOrDefault(o=>o.ItemGroupBM== ChargeGroup.ItemGroupBM);

                    CreateMReceiptInfoDetailedDto CreateMReceiptInfoDetailed = new CreateMReceiptInfoDetailedDto();
                    CreateMReceiptInfoDetailed.GroupsMoney = itemgroup.ItemPrice;
                    CreateMReceiptInfoDetailed.GroupsDiscountMoney = itemgroup.PriceAfterDis;
                    if (CreateMReceiptInfoDetailed.GroupsMoney != 0)
                    {
                        CreateMReceiptInfoDetailed.Discount = CreateMReceiptInfoDetailed.GroupsDiscountMoney / CreateMReceiptInfoDetailed.GroupsMoney;
                    }
                    else
                    {
                        CreateMReceiptInfoDetailed.Discount = 1;
                    }
                    CreateMReceiptInfoDetailed.ReceiptTypeName = itemgroup.ItemGroupBM.ChartName;
                    //组合
                    CreateMReceiptInfoDetailed.ItemGroupId = itemgroup.Id;
                    CreateMReceiptInfoDetaileds.Add(CreateMReceiptInfoDetailed);
                }

                input.MReceiptInfoDetailedgr = CreateMReceiptInfoDetaileds;
                //保存收费记录
                //  Guid receiptInfoDtoID = ChargeAppService.InsertReceiptInfoDto(input);
                OutErrDto outErrDto = InsertReceiptState(input);

                outMess.code = outErrDto.code;
                outMess.mess = outErrDto.err;
            }
            //退费
            else
            {
                var cusGrouplistzf = _customerItemGroupRepository.GetAll().Where(p => p.IsAddMinus !=
            (int)AddMinusType.Minus).Where(p =>
                 SQKList.Contains(p.ApplicationNo)
                 || SQKList.Contains(p.fee_no)).Select(p => p.MReceiptInfoPersonalId).Distinct().ToList();
                //var cusGroup = cusGrouplist.FirstOrDefault();
                if (cusGrouplistzf.Count == 0)
                {
                    mess += "没有该申请单号！";
                    outMess.code = "0";
                    outMess.mess = mess;
                    return outMess;
                }
                foreach (var cusGroupzf in cusGrouplistzf)
                {
                    var dto = _receiptInfoRepository.FirstOrDefault(o => o.Id == cusGroupzf);
                    if (dto == null)
                    {
                        outMess.code = "0";
                        outMess.mess = "没有该退费记录！";
                        return outMess;
                    }
                    //总检完成不可作废
                    if (dto.CustomerReg?.SummSate >= 3)
                    {

                        outMess.code = "0";
                        outMess.mess = "该体检人已总检，不能作废！";
                        return outMess;
                    }
                    EntityDto<Guid> receiptID = new EntityDto<Guid>();
                    receiptID.Id = dto.Id;
                    bool ischek = SFGroupCheck(receiptID);
                    if (ischek == true)
                    {


                        outMess.code = "0";
                        outMess.mess = "项目已检查，不能作废！";
                        return outMess;
                    }

                    MReceiptInfoDto mReceiptInfoDto = GetInvalid(receiptID);
                    int receiptstate = (int)InvoiceStatus.Cancellation;
                    if (mReceiptInfoDto != null && mReceiptInfoDto.ReceiptSate == receiptstate)
                    {


                        outMess.code = "0";
                        outMess.mess = "此记录已作废，不能重复作废！";
                        return outMess;
                    }
                    GuIdDto input = new GuIdDto();
                    input.Id = dto.Id;
                    MReceiptInfoDto MReceiptInfo = InsertInvalidReceiptInfoDto(input);
                    //日志
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    if (dto.CustomerReg != null)
                    {
                        createOpLogDto.LogBM = dto.CustomerReg.CustomerBM;
                        createOpLogDto.LogName = dto.CustomerReg.Customer.Name;
                    }
                    else if (dto.ClientReg != null)
                    {
                        createOpLogDto.LogBM = dto.ClientReg.ClientRegBM;
                        createOpLogDto.LogName = dto.ClientReg.ClientInfo.ClientName;
                    }

                    createOpLogDto.LogText = "门诊作废收费：" + dto.Actualmoney;
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.ResId;
                    _commonAppService.SaveOpLog(createOpLogDto);
                    outMess.code = "1";

                    outMess.mess = "退费成功！";

                }
            }
            return outMess;


        }

        /// <summary>
        /// HIS收费
        /// </summary>
        /// <param name="Hisinput"></param>
        [UnitOfWork(false)]
        public OutMessDto HISPayCharge(INTJSQDto hisinput)
        {
            OutMessDto outMess = new OutMessDto();
            try
            {

                if (hisinput.SQSTATUS == 2)
                {
                    var guid = _TjlApplicationForm.FirstOrDefault(o => o.SQDH == hisinput.SQDH && o.SQSTATUS == 2);
                    if (guid == null)
                    {
                        outMess.code = "0";
                        outMess.mess = "没有该申请单的收费信息！";
                        return outMess;

                    }
                    GuIdDto entityDto = new GuIdDto();
                    entityDto.Id = guid.MReceiptInfoId.Value;
                    InsertInvalidReceiptInfoDto(entityDto);
                    guid.HCZT = 1;
                    guid.SQSTATUS = 3;
                    _TjlApplicationForm.Update(guid);
                    outMess.code = "1";
                    outMess.mess = "退费成功！";
                    return outMess;
                }
                else
                {
                    var application = _TjlApplicationForm.FirstOrDefault(o => o.SQDH == hisinput.SQDH && o.SQSTATUS == 1);
                    if (application == null)
                    {
                        outMess.code = "0";
                        outMess.mess = "没有该申请单！";
                        return outMess;
                    }
                    if (application.ClientRegId.HasValue)
                    {
                        outMess.code = "0";
                        outMess.mess = "单位申请单暂不支持！";
                        return outMess;
                    }
                    CreateReceiptInfoDto input = new CreateReceiptInfoDto();
                    input.Actualmoney = application.FYZK;
                    input.ChargeDate = System.DateTime.Now;
                    input.ChargeState = (int)InvoiceStatus.NormalCharge;
                    var ChargeInfoPerSion = _customerRegRepository.FirstOrDefault(o => o.CustomerBM == application.CustomerReg.CustomerBM);
                    input.CustomerRegid = ChargeInfoPerSion.Id;//关联已有对象                

                    input.DiscontReason = "";
                    input.Discount = 1;
                    input.ReceiptSate = (int)InvoiceStatus.Valid;
                    input.Remarks = "";
                    input.SettlementSate = (int)ReceiptState.UnSettled;
                    input.Shouldmoney = ChargeInfoPerSion.McusPayMoney.PersonalMoney;
                    input.Summoney = ChargeInfoPerSion.McusPayMoney.PersonalMoney;
                    input.TJType = 2;

                    input.Userid = application.CreatorUserId.Value;

                    //支付方式   
                    List<CreatePaymentDto> CreatePayments = new List<CreatePaymentDto>();
                    var payment = _chargeTypeRepository.FirstOrDefault(o => o.ChargeName == "门诊");
                    CreatePaymentDto CreatePayment = new CreatePaymentDto();
                    CreatePayment.Actualmoney = application.FYZK;
                    CreatePayment.CardNum = "";//暂时不支持会员卡
                    CreatePayment.Discount = 1;
                    CreatePayment.MChargeTypeId = payment.Id;
                    CreatePayment.Shouldmoney = application.FYZK;
                    CreatePayments.Add(CreatePayment);

                    input.MPaymentr = CreatePayments;
                    //未收费项目集合
                    List<CreateMReceiptInfoDetailedDto> CreateMReceiptInfoDetaileds = new List<CreateMReceiptInfoDetailedDto>();

                    var cusPerGroups = ChargeInfoPerSion.CustomerItemGroup?.Where(r => r.IsAddMinus != 3 && r.PayerCat == 1 && r.TTmoney <= 0);
                    //var ChargeGroups = cusPerGroups?.GroupBy(r => r.SFType);
                    foreach (var ChargeGroup in cusPerGroups)
                    {
                        CreateMReceiptInfoDetailedDto CreateMReceiptInfoDetailed = new CreateMReceiptInfoDetailedDto();
                        CreateMReceiptInfoDetailed.GroupsMoney = ChargeGroup.ItemPrice;
                        CreateMReceiptInfoDetailed.GroupsDiscountMoney = ChargeGroup.GRmoney;
                        if (CreateMReceiptInfoDetailed.GroupsMoney != 0)
                        {
                            CreateMReceiptInfoDetailed.Discount = CreateMReceiptInfoDetailed.GroupsDiscountMoney / CreateMReceiptInfoDetailed.GroupsMoney;
                        }
                        else
                        {
                            CreateMReceiptInfoDetailed.Discount = 1;
                        }
                        if (ChargeGroup.SFType.HasValue)
                            CreateMReceiptInfoDetailed.ReceiptTypeName = _BasicDictionaries.FirstOrDefault(o => o.Value == ChargeGroup.SFType.Value && o.Type == "ChargeCategory").Text;

                        else
                            CreateMReceiptInfoDetailed.ReceiptTypeName = "";
                        //组合
                        CreateMReceiptInfoDetailed.ItemGroupId = ChargeGroup.Id;
                        CreateMReceiptInfoDetaileds.Add(CreateMReceiptInfoDetailed);
                    }

                    input.MReceiptInfoDetailedgr = CreateMReceiptInfoDetaileds;
                    //保存收费记录
                    Guid receiptInfoDtoID = InsertReceiptInfoDto(input);
                    CurrentUnitOfWork.SaveChanges();
                    //更新体检人收费状态及组合结算状态
                    UpdateChargeStateDto updateChargeStateDto = new UpdateChargeStateDto();
                    var cusGroups = cusPerGroups;
                    var CusGroupsIds = cusGroups.Select(r => r.Id);
                    var CusRegs = cusGroups.Where(r => r.CustomerRegBMId.HasValue).Select(r => r.CustomerRegBMId);
                    updateChargeStateDto.CusGroupids = CusGroupsIds.ToList();
                    updateChargeStateDto.CusRegids = CusRegs.Distinct().Cast<Guid>().ToList();
                    updateChargeStateDto.ReceiptID = receiptInfoDtoID;
                    updateChargeStateDto.CusType = 1;
                    updateClientChargeState(updateChargeStateDto);

                    //更新费用表
                    SearchPayMoneyDto inputcs = new SearchPayMoneyDto();
                    inputcs.Id = ChargeInfoPerSion.Id;
                    inputcs.PayMoney = application.FYZK;
                    inputcs.DistMoney = 0;
                    CusPayMoneyViewDto cusPayMoneyViewDto = UpCusMoney(inputcs);
                    application.SQSTATUS = 2;
                    application.MReceiptInfoId = receiptInfoDtoID;
                    application.BRSFH = hisinput.BRSFH;
                    application.FPH = hisinput.BRFPH;
                    application.HCZT = 1;
                    _TjlApplicationForm.Update(application);
                    outMess.code = "1";
                    outMess.mess = "收费成功！";
                    return outMess;
                }
            }
            catch (Exception ex)
            {
                outMess.code = "0";
                outMess.mess = ex.Message;
                return outMess;
            }

        }
        #endregion
        /// <summary>
        /// 获取优康云会员卡信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public YHCustomerDto geYKInfor(InCarNumDto input)
        {
            // 获取接口数据
            var ykInterfaceDriver = DriverFactory.GetDriver<IYKYInterfaceDriver>();

            var search = input.MapTo<InCarNum>();
            var interfaceResult = ykInterfaceDriver.GetYHCusInfoByNum(search);

            return interfaceResult.MapTo<YHCustomerDto>();
        }
        /// <summary>
        /// 获取优康云会员扣费
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public YHCustomerDto ChargeYHNum(InYKCarNumDto input)
        {
            // 获取接口数据
            var ykInterfaceDriver = DriverFactory.GetDriver<IYKYInterfaceDriver>();

            var search = input.MapTo<InYKCarNum>();
            var interfaceResult = ykInterfaceDriver.ChargeByYHNum(search);

            return interfaceResult.MapTo<YHCustomerDto>();
        }
        /// <summary>
        /// 获取新优康云会员信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public getCardInfoDto GetNYHCardByNum(ChargeBM input)
        {
            // 获取接口数据
            var NykInterfaceDriver = DriverFactory.GetDriver<INYKInterfaceDriver>();

            var search = input.MapTo<InYKCarNum>();
            var interfaceResult = NykInterfaceDriver.GetNYHCardByNum(input.Name);

            return interfaceResult.MapTo<getCardInfoDto>();
        }
        /// <summary>
        /// 获取会员卡信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public OutCardDto NYHChargeCard(ChargCardDto input)
        {
            // 获取接口数据
            var NykInterfaceDriver = DriverFactory.GetDriver<INYKInterfaceDriver>();

            var interfaceResult = NykInterfaceDriver.NYHChargeCard(input);

            return interfaceResult.MapTo<OutCardDto>();
        }
        public CustomerRegCostDto GetsfState(EntityDto<Guid> input)
        {
            CustomerRegCostDto customerRegCost = new CustomerRegCostDto();
            customerRegCost.CostState = (int)PayerCatType.NoCharge;
            var sfstate = (int)InvoiceStatus.Valid;
            var recpt = _receiptInfoRepository.FirstOrDefault(o => o.CustomerRegId == input.Id && o.ReceiptSate == sfstate);
            if (recpt != null && recpt.Id != Guid.Empty)
            {
                customerRegCost.CostState = (int)PayerCatType.Charge;
            }
            return customerRegCost;
        }

        /// <summary>
        /// 日报明细 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public List<CusPayListDto> getDailyList(SearchChagelistDto input)
        {
            //团付不走收费的人 
            //int sftate = (int)InvoiceStatus.Valid;
            var ttque = _customerRegRepository.GetAllIncluding(o => o.Customer, o => o.MReceiptInfo, o => o.McusPayMoney);
            ttque = ttque.Where(o => o.RegisterState != 1 && o.ClientRegId != null && o.ClientRegId != Guid.Empty && o.McusPayMoney.ClientMoney > 0);
            if (input.ClientRegID.HasValue)
            {
                ttque = ttque.Where(o => o.ClientRegId == input.ClientRegID.Value);
            }
            if (input.StarDate.HasValue && input.EndDate.HasValue)
            {
                var start = input.StarDate.Value.Date;
                var end = input.EndDate.Value.Date.AddDays(1);
                ttque = ttque.Where(o => o.LoginDate >= start && o.LoginDate < end);
            }
            if (input.MaxMoney.HasValue && input.MinMoney.HasValue)
            {

                ttque = ttque.Where(o => (o.McusPayMoney.PersonalMoney + o.McusPayMoney.ClientMoney) >= input.MinMoney.Value && (o.McusPayMoney.PersonalMoney + o.McusPayMoney.ClientMoney) <= input.MaxMoney.Value);
            }
            if (!string.IsNullOrWhiteSpace(input.SearchName))
            {
                ttque = ttque.Where(o => o.CustomerBM == input.SearchName || o.Customer.Name == input.SearchName || o.Customer.IDCardNo == input.SearchName);
            }
            if (input.UserID.HasValue)
            {
                ttque = ttque.Where(o => o.MReceiptInfo.Any(r => r.CreatorUserId == input.UserID));
            }
            if (!string.IsNullOrWhiteSpace(input.LinkName))
            {
                ttque = ttque.Where(o => o.Introducer == input.LinkName);
            }
            var ttlist = ttque.ToList();
            var TTPaylist = new List<CusPayListDto>();
            if (input.StarDate.HasValue && input.EndDate.HasValue)
            {
                var start = input.StarDate.Value.Date;
                var end = input.EndDate.Value.Date.AddDays(1);

                var TTPaylist1 = ttlist.Select(o => new CusPayListDto
                {
                    RegID = o.Id,
                    Age = o.Customer.Age,
                    allMoney = (o.McusPayMoney.ClientMoney + (o.MReceiptInfo.Where(m => m.ChargeDate >= start && m.ChargeDate < end).Sum(n => (decimal?)n.Actualmoney) ?? 0)),
                    PayMoney = (o.MReceiptInfo.Where(m => m.ChargeDate >= start && m.ChargeDate < end).Sum(n => (decimal?)n.Actualmoney) ?? 0),
                    SFRemarklist = o.MReceiptInfo.Where(p => p.Remarks != "").Select(p =>
                new SFDto { Remark = p.Remarks }).ToList(),
                    ClientName = o.ClientInfo.ClientName,
                    CustomerBM = o.CustomerBM,
                    Mobile = o.Customer.Mobile,
                    Name = o.Customer.Name,
                    Sex = o.Customer.Sex,
                    SuitName = o.ItemSuitName,
                    TTMoney = o.McusPayMoney.ClientMoney,
                    Introducer = o.Introducer,
                    GroupName = ("单位挂账 ，（单位：" + o.ClientInfo.ClientName + "）， （套餐：" + o.ItemSuitName + "）  （介绍人：" + o.Introducer + "）， （单价：" + o.McusPayMoney.ClientMoney + "）"),
                    cusPayments = o.MReceiptInfo.Where(n => n.ChargeDate >= start && n.ChargeDate < end).SelectMany(r => r.MPayment).Select
                    (p => new CusChageListDto { Actualmoney = p.Actualmoney, MChargeTypename = p.MChargeTypename }).ToList()
                }).OrderBy(o => o.ClientName).ThenBy(o => o.allMoney).ToList();
                //  TTPaylist = TTPaylist1.MapTo<List<CusPayListDto>>();
                TTPaylist = TTPaylist1.ToList();

            }
            else
            {
                var TTPaylists = ttlist.Select(o => new CusPayListDto
                {
                    RegID = o.Id,
                    Age = o.Customer.Age,
                    allMoney = (o.McusPayMoney.ClientMoney + (o.MReceiptInfo.Sum(n => (decimal?)n.Actualmoney) ?? 0)),
                    PayMoney = (o.MReceiptInfo.Sum(n => (decimal?)n.Actualmoney) ?? 0),
                    SFRemarklist = o.MReceiptInfo.Where(p => p.Remarks != "").Select(p =>
                    new SFDto { Remark = p.Remarks }).ToList(),

                    ClientName = o.ClientInfo.ClientName,
                    CustomerBM = o.CustomerBM,
                    Mobile = o.Customer.Mobile,
                    Name = o.Customer.Name,
                    Sex = o.Customer.Sex,
                    SuitName = o.ItemSuitName,
                    Introducer = o.Introducer,
                    TTMoney = o.McusPayMoney.ClientMoney,
                    GroupName = ("单位挂账， （单位：" + o.ClientInfo.ClientName + "）， （套餐：" + o.ItemSuitName + "）， （介绍人：" + o.Introducer + "）， （单价：" + o.McusPayMoney.ClientMoney + "）"),
                    cusPayments = o.MReceiptInfo.SelectMany(r => r.MPayment).Select(p => new CusChageListDto
                    {
                        MChargeTypename = p.MChargeTypename,
                        Actualmoney = p.Actualmoney
                    }).ToList()
                }).OrderBy(o => o.ClientName).ThenBy(o => o.allMoney).ToList();
                // TTPaylist = TTPaylists.MapTo<List<CusPayListDto>>();
                TTPaylist = TTPaylists.ToList();
            }
            //已收费
            var regid = TTPaylist.Select(o => o.RegID).ToList();
            var Grque = _receiptInfoRepository.GetAll().Where(o => o.CustomerReg != null
            && o.CustomerReg.RegisterState != 1).AsNoTracking();

            // Grque = Grque.Where(o => !regid.Contains(o.CustomerRegId));

            if (input.ClientRegID.HasValue)
            {
                Grque = Grque.Where(o => o.ClientRegId == input.ClientRegID.Value);
            }
            if (input.StarDate.HasValue && input.EndDate.HasValue)
            {
                var start = input.StarDate.Value.Date;
                var end = input.EndDate.Value.Date.AddDays(1);
                Grque = Grque.Where(o => o.ChargeDate >= start && o.ChargeDate < end);
            }
            if (input.MaxMoney.HasValue && input.MinMoney.HasValue)
            {
                Grque = Grque.Where(o => (o.CustomerReg.McusPayMoney.ClientMoney + o.CustomerReg.McusPayMoney.PersonalMoney) >= input.MinMoney.Value && (o.CustomerReg.McusPayMoney.ClientMoney + o.CustomerReg.McusPayMoney.PersonalMoney) <= input.MaxMoney.Value);
            }
            if (!string.IsNullOrWhiteSpace(input.SearchName))
            {
                Grque = Grque.Where(o => o.CustomerReg.CustomerBM == input.SearchName || o.Customer.Name == input.SearchName || o.Customer.IDCardNo == input.SearchName);
            }
            if (input.UserID.HasValue)
            {
                Grque = Grque.Where(r => r.CreatorUserId == input.UserID);
            }
            if (!string.IsNullOrWhiteSpace(input.LinkName))
            {
                Grque = Grque.Where(o => o.CustomerReg.Introducer == input.LinkName);
            }
            var GRPaylist = Grque.GroupBy(n => new
            {
                n.CustomerRegId,
                n.ClientName,
                n.Customer.Age,
                n.Customer.Sex,
                n.Customer.Mobile,
                n.CustomerReg.CustomerBM,
                n.Customer.Name,
                n.CustomerReg.Introducer,
                n.CustomerReg.ItemSuitName,
                n.CustomerReg.ItemSuitBM.Price
            }).Select(o => new CusPayListDto
            {
                RegID = o.Key.CustomerRegId,
                Age = o.Key.Age,
                allMoney = o.Sum(r => r.Actualmoney),
                SFRemarklist = o.Where(p => p.Remarks != "").Select(p =>
                    new SFDto { Remark = p.Remarks }).ToList(),
                PayMoney = o.Sum(r => r.Actualmoney),
                ClientName = o.Key.ClientName,
                CustomerBM = o.Key.CustomerBM,
                Mobile = o.Key.Mobile,
                Name = o.Key.Name,
                Sex = o.Key.Sex,
                SuitName = o.Key.ItemSuitName,
                TTMoney = 0,
                Introducer = o.Key.Introducer,
                GroupName = ((o.Key.CustomerBM == null ? "团体收费" : "个人") + "， （套餐：" + (o.Key.ItemSuitName ?? "") + "）， （介绍人：" + o.Key.Introducer + "）， （单价：" + (o.Key.Price ?? 0) + "）"),
                cusPayments = o.SelectMany(r => r.MPayment).Select(p => new CusChageListDto
                {
                    Actualmoney = p.Actualmoney,
                    MChargeTypename = p.MChargeTypename
                }).ToList()
            }).OrderBy(o => o.SuitName).ThenBy(o => o.allMoney).ToList();

            GRPaylist = GRPaylist.Where(p => !regid.Contains(p.RegID)).ToList();

            var grlist = GRPaylist.ToList();
            var alllist = new List<CusPayListDto>();
            alllist.AddRange(TTPaylist);
            alllist.AddRange(grlist);
            return alllist;
        }

        /// <summary>
        /// 日报支付方式明细 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public List<DayCusListDto> getPayDailyList(SearchChagelistDto input)
        {
            //团付不走收费的人

            var ttque = _customerRegRepository.GetAllIncluding(o => o.Customer, o => o.MReceiptInfo, o => o.McusPayMoney);
            ttque = ttque.Where(o => o.RegisterState != 1 && o.ClientRegId != null && o.ClientRegId != Guid.Empty && o.McusPayMoney.ClientMoney > 0);
            if (input.ClientRegID.HasValue)
            {
                ttque = ttque.Where(o => o.ClientRegId == input.ClientRegID.Value);
            }
            if (input.StarDate.HasValue && input.EndDate.HasValue)
            {
                var start = input.StarDate.Value.Date;
                var end = input.EndDate.Value.Date.AddDays(1);
                ttque = ttque.Where(o => o.LoginDate >= start && o.LoginDate < end);
            }
            if (input.MaxMoney.HasValue && input.MinMoney.HasValue)
            {

                ttque = ttque.Where(o => (o.McusPayMoney.PersonalMoney + o.McusPayMoney.ClientMoney) >= input.MinMoney.Value && (o.McusPayMoney.PersonalMoney + o.McusPayMoney.ClientMoney) <= input.MaxMoney.Value);
            }
            if (!string.IsNullOrWhiteSpace(input.SearchName))
            {
                ttque = ttque.Where(o => o.CustomerBM == input.SearchName || o.Customer.Name == input.SearchName || o.Customer.IDCardNo == input.SearchName);
            }
            if (input.UserID.HasValue)
            {
                ttque = ttque.Where(o => o.MReceiptInfo.Any(r => r.CreatorUserId == input.UserID));
            }
            if (!string.IsNullOrWhiteSpace(input.LinkName))
            {
                ttque = ttque.Where(o => o.Introducer == input.LinkName);
            }
            var ttlist = ttque.ToList();
            var TTPaylist = new List<DayCusListDto>();

            var TTPaylist1 = ttlist.GroupBy(p => p.ClientInfo.ClientName).Select(o => new DayCusListDto
            {
                Allmoney = o.Sum(p => p.McusPayMoney.ClientMoney),
                ClientName = o.FirstOrDefault().ClientInfo.ClientName,
                Actualmoney = o.Sum(p => p.McusPayMoney.ClientMoney),
                PaymentName = "单位挂账"

            }).ToList();
            //  TTPaylist = TTPaylist1.MapTo<List<CusPayListDto>>();
            TTPaylist = TTPaylist1.ToList();
            //已收费

            var Grque = _receiptInfoRepository.GetAll().Where(o => o.CustomerReg.RegisterState != 1).AsNoTracking();

            // Grque = Grque.Where(o => !regid.Contains(o.CustomerRegId));

            if (input.ClientRegID.HasValue)
            {
                Grque = Grque.Where(o => o.ClientRegId == input.ClientRegID.Value);
            }
            if (input.StarDate.HasValue && input.EndDate.HasValue)
            {
                var start = input.StarDate.Value.Date;
                var end = input.EndDate.Value.Date.AddDays(1);
                Grque = Grque.Where(o => o.ChargeDate >= start && o.ChargeDate < end);
            }
            if (input.MaxMoney.HasValue && input.MinMoney.HasValue)
            {
                Grque = Grque.Where(o => (o.CustomerReg.McusPayMoney.ClientMoney + o.CustomerReg.McusPayMoney.PersonalMoney) >= input.MinMoney.Value && (o.CustomerReg.McusPayMoney.ClientMoney + o.CustomerReg.McusPayMoney.PersonalMoney) <= input.MaxMoney.Value);
            }
            if (!string.IsNullOrWhiteSpace(input.SearchName))
            {
                Grque = Grque.Where(o => o.CustomerReg.CustomerBM == input.SearchName || o.Customer.Name == input.SearchName || o.Customer.IDCardNo == input.SearchName);
            }
            if (input.UserID.HasValue)
            {
                Grque = Grque.Where(r => r.CreatorUserId == input.UserID);
            }
            if (!string.IsNullOrWhiteSpace(input.LinkName))
            {
                Grque = Grque.Where(o => o.CustomerReg.Introducer == input.LinkName);
            }

            var GRPaylist = Grque.GroupBy(n => new
            {


                n.ClientName,
                n.MPayment
            }
                 ).Select(o => new DayCusListDto
                 {
                     //RSCount=o.Count(),
                     ClientName = o.FirstOrDefault().CustomerReg.ClientInfoId == null ? "个人" : o.FirstOrDefault().CustomerReg.ClientInfo.ClientName,
                     Actualmoney = o.Sum(p => p.CustomerReg.McusPayMoney.ClientMoney),
                     PaymentName = o.FirstOrDefault().MPayment.FirstOrDefault().MChargeTypename

                 }).ToList();



            var grlist = GRPaylist.ToList();
            var alllist = new List<DayCusListDto>();
            alllist.AddRange(TTPaylist);
            alllist.AddRange(grlist);
            var nowList = alllist.GroupBy(p => new { p.PaymentName, p.ClientName }).Select(
                p => new DayCusListDto
                {
                    ClientName = p.FirstOrDefault().ClientName,
                    PaymentName = p.FirstOrDefault().PaymentName,
                    Actualmoney = p.Sum(o => o.Actualmoney)
                }
                 ).ToList();
            return nowList;
        }

        /// <summary>
        /// 获取收费记录
        /// </summary>
        /// <param name="searchInvoice"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public List<MReceiptInfoDto> GetReceiptlist(SearchInvoiceDto searchInvoice)
        {
            var query = _receiptInfoRepository.GetAllIncluding(r => r.MPayment, r => r.User, r => r.MInvoiceRecord,
                r => r.CustomerReg.Customer);
            if (!string.IsNullOrEmpty(searchInvoice.SearchName))
            {
                query = query.Where(r => r.Customer.Name == searchInvoice.SearchName ||
                    r.CustomerReg.CustomerBM == searchInvoice.SearchName ||
                    r.MInvoiceRecord.Any(u => u.InvoiceNum == searchInvoice.SearchName));
            }

            if (searchInvoice.SFUserId.HasValue)
            {
                query = query.Where(r => r.UserId == searchInvoice.SFUserId);
            }
            if (searchInvoice.ClientRegId.HasValue)
            {
                query = query.Where(o => o.ClientRegId == searchInvoice.ClientRegId.Value);
            }
            if (searchInvoice.ChargeType.HasValue)
            {
                query = query.Where(o => o.MPayment.Any(n => n.MChargeTypeId == searchInvoice.ChargeType.Value));
            }
            //if (searchInvoice.ReceiptSate != null && searchInvoice.ReceiptSate != 0)
            if (searchInvoice.ReceiptSate != 0)
            {
                query = query.Where(r => r.ReceiptSate == searchInvoice.ReceiptSate);
            }

            //体检日期
            if (searchInvoice.StarDate != null && searchInvoice.EndDate != null)
            {
                searchInvoice.StarDate = searchInvoice.StarDate.Value.Date;
                searchInvoice.EndDate = searchInvoice.EndDate.Value.Date.AddDays(1);
                query = query.Where(o =>
                    o.CreationTime > searchInvoice.StarDate && o.CreationTime < searchInvoice.EndDate);
            }

            return query.MapTo<List<MReceiptInfoDto>>();
        }
        /// <summary>
        /// 套餐卡消费
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool ChargeCard(InCardChageDto qinput)
        {
            var card = _TbmCard.GetAll().FirstOrDefault(o => o.CardNo == qinput.CardNo);
            if (card == null)
            {
                return false;
            }
            if (card.Available != 1 || card.HasUse == 1)
            {
                return false;
            }
            //体检卡
            if (card.CardType?.CardType == "体检卡")
            {
                var suit = card.CardType.ItemSuits.FirstOrDefault(o => o.Id == qinput.SuitId);
                if (suit == null)
                {
                    return false;
                }
                else
                {
                    var cusreg = _customerRegRepository.Get(qinput.CusRegId);

                    var suitGroupId = suit.ItemSuitItemGroups.Select(o => o.ItemGroupId).ToList();
                    var chargeGroup = cusreg.CustomerItemGroup.Where(o => suitGroupId.Contains(o.ItemGroupBM_Id.Value)).ToList();
                    var paygroups = chargeGroup.Where(o => o.IsAddMinus != (int)AddMinusType.Minus && o.IsAddMinus != (int)AddMinusType.AdjustMinus);
                    var paymoney = paygroups.Sum(o => o.PriceAfterDis);
                    var allmoney = paygroups.Sum(o => o.ItemPrice);
                    CreateReceiptInfoDto input = new CreateReceiptInfoDto();
                    input.Discontmoney = 0;
                    input.Actualmoney = paymoney;

                    input.ChargeDate = DateTime.Now;
                    input.ChargeState = (int)InvoiceStatus.NormalCharge;

                    input.CustomerRegid = cusreg.Id;//关联已有对象                

                    input.DiscontReason = "";
                    input.Discount = 1;
                    input.ReceiptSate = (int)InvoiceStatus.Valid;
                    input.Remarks = "套餐卡支付";
                    input.SettlementSate = (int)ReceiptState.UnSettled;
                    input.Shouldmoney = paymoney;
                    input.Summoney = allmoney;
                    int tjtype = 2;
                    input.TJType = tjtype;
                    input.Userid = AbpSession.UserId.Value;

                    //支付方式
                    List<CreatePaymentDto> CreatePayments = new List<CreatePaymentDto>();
                    var payment = _chargeTypeRepository.FirstOrDefault(o => o.ChargeName.Contains("健康卡"));
                    //没有健康卡则新建
                    if (payment == null)
                    {
                        payment = new TbmMChargeType();
                        payment.Id = Guid.NewGuid();
                        payment.AccountingState = 2;
                        payment.ChargeApply = 3;
                        payment.ChargeCode = 100;
                        payment.ChargeName = "健康卡";
                        payment.HelpChar = "JJK";
                        payment.OrderNum = 101;
                        payment.PrintName = 3;
                        payment.Remarks = "";
                        payment = _chargeTypeRepository.Insert(payment);

                    }
                    CreatePaymentDto CreatePayment = new CreatePaymentDto();
                    CreatePayment.Actualmoney = paymoney;
                    CreatePayment.CardNum = qinput.CardNo;//卡号
                    CreatePayment.Discount = 1;
                    CreatePayment.MChargeTypeId = payment.Id;
                    CreatePayment.Shouldmoney = paymoney;
                    CreatePayment.MChargeTypename = payment.ChargeName;
                    CreatePayments.Add(CreatePayment);

                    input.MPaymentr = CreatePayments;
                    //未收费项目集合
                    List<CreateMReceiptInfoDetailedDto> CreateMReceiptInfoDetaileds = new List<CreateMReceiptInfoDetailedDto>();

                    var cusPerGroups = paygroups;
                    //var ChargeGroups = cusPerGroups?.GroupBy(r => r.SFType);
                    foreach (var itemgroup in cusPerGroups)
                    {
                        CreateMReceiptInfoDetailedDto CreateMReceiptInfoDetailed = new CreateMReceiptInfoDetailedDto();
                        CreateMReceiptInfoDetailed.GroupsMoney = itemgroup.ItemPrice;
                        CreateMReceiptInfoDetailed.GroupsDiscountMoney = itemgroup.PriceAfterDis;
                        if (CreateMReceiptInfoDetailed.GroupsMoney != 0)
                        {
                            CreateMReceiptInfoDetailed.Discount = CreateMReceiptInfoDetailed.GroupsDiscountMoney / CreateMReceiptInfoDetailed.GroupsMoney;
                        }
                        else
                        {
                            CreateMReceiptInfoDetailed.Discount = 1;
                        }
                        CreateMReceiptInfoDetailed.ReceiptTypeName = itemgroup.ItemGroupBM.ChartName;
                        //组合
                        CreateMReceiptInfoDetailed.ItemGroupId = itemgroup.Id;
                        CreateMReceiptInfoDetaileds.Add(CreateMReceiptInfoDetailed);
                    }

                    input.MReceiptInfoDetailedgr = CreateMReceiptInfoDetaileds;
                    //保存收费记录
                    //  Guid receiptInfoDtoID = ChargeAppService.InsertReceiptInfoDto(input);
                    CurrentUnitOfWork.SaveChanges();
                    OutErrDto outErrDto = InsertReceiptState(input);
                    //使用卡
                    card.HasUse = 1;
                    card.CustomerReg = null;
                    card.CustomerRegId = qinput.CusRegId;
                    card.UseTime = DateTime.Now;
                    _TbmCard.Update(card);
                    return true;
                }
            }
            else if (card.CardCategory == "单位凭证卡")
            {
                //使用卡
                card.HasUse = 1;
                card.CustomerReg = null;
                card.CustomerRegId = qinput.CusRegId;
                card.UseTime = DateTime.Now;
                _TbmCard.Update(card);
                return true;
            }
            else
            { return false; }
        }

        public List<CusReceivsDto> GetIndividuality(IndividualityDto input)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var query = _receiptInfoRepository.GetAll();//.Where(o=>o.CustomerReg.RegisterState !=1);
                input.TJType = 2;
                if (input.StartDate != null)
                {
                    // query = query.Where(o => o.CustomerReg.LoginDate >= input.StartDate);

                    query = query.Where(o => o.ChargeDate >= input.StartDate);
                }
                if (input.EndDate != null)
                {

                    // query = query.Where(o => o.CustomerReg.LoginDate <= input.EndDate);
                    query = query.Where(o => o.ChargeDate <= input.EndDate);
                }
                query = query.OrderBy(o => o.ChargeDate);

                var result = query.Select(o => new CusReceivsDto
                {
                    Actualmoney = o.Actualmoney,
                    ChargeDate = o.ChargeDate,
                    Discount = o.Discount,
                    Shouldmoney = o.CustomerReg.McusPayMoney == null ? 0 : o.CustomerReg.McusPayMoney.PersonalMoney,
                    UserName = o.User.UserName,
                    CusName = o.CustomerReg.IsDeleted == true ? o.Customer.Name + "-已删除" : o.Customer.Name,
                    CusRegBM = o.CustomerReg.CustomerBM,




                }).ToList();

                return result;
            }
        }
        /// <summary>
        /// 个人收费项目统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<IndividualitiesDto> GetIndividualities(IndividualitiesDto input)
        {
            var query = _customerItemGroupRepository.GetAll().Where(o => o.PayerCat != (int)PayerCatType.ClientCharge &&
            o.PayerCat != (int)PayerCatType.GiveCharge && o.CustomerRegBM.RegisterState != 1);
            //var query = _receiptDetailsRepository.GetAll();
            if (input.StartDate != null)
            {
                query = query.Where(o => o.CustomerRegBM.LoginDate >= input.StartDate);
            }
            if (input.EndDate != null)
            {
                query = query.Where(o => o.CustomerRegBM.LoginDate <= input.EndDate);
            }
            var outlist = query.Where(o => o.IsAddMinus != 3).GroupBy(o => new { o.DepartmentName, o.ItemGroupName, o.ItemPrice }).Select(o =>
                     new IndividualitiesDto
                     {
                         DepartmentName = o.Key.DepartmentName,
                         ItemGroupName = o.Key.ItemGroupName,
                         GroupsMoney = o.Key.ItemPrice,
                         Counts = o.Count(),
                         CountGroupsMoney = o.Sum(r => r.ItemPrice),
                         SholdMoney = o.Sum(r => r.PriceAfterDis),
                         SJMoney = o.Where(r => r.PayerCat == (int)PayerCatType.PersonalCharge).Sum(p => p.PriceAfterDis),
                         Discount = o.Sum(r => r.ItemPrice) == 0 ? 0 : o.Sum(r => r.PriceAfterDis) / o.Sum(r => r.ItemPrice),
                     }).OrderBy(s => s.DepartmentName).ToList();
            var result = outlist.MapTo<List<IndividualitiesDto>>();
            return result;
        }
        public List<DoctorDeparmentDto> GetDoctorDeparment(DoctorDeparmentDto input)
        {
            var query = _customerRegRepository.GetAll();
            if (input.StartDate != null)
            {
                query = query.Where(o => o.ClientInfo.CreationTime >= input.StartDate);
            }
            if (input.EndDate != null)
            {
                query = query.Where(o => o.ClientInfo.CreationTime <= input.EndDate);
            }
            var result = query.MapTo<List<DoctorDeparmentDto>>();
            return result;
        }

        public List<BallCheckDto> GetBallCheck(BallCheckDto input)
        {
            var result = new List<BallCheckDto>();
            var query = _customerRegRepository.GetAll().Where(o => o.RegisterState != 1);
            query = query.Where(o => o.ClientRegId != null);
            if (input.StartDate != null)
            {
                query = query.Where(o => o.LoginDate >= input.StartDate);
            }
            if (input.EndDate != null)
            {
                query = query.Where(o => o.LoginDate < input.EndDate);
            }
            if (input.ClientRegId.HasValue)
            {
                query = query.Where(o => o.ClientRegId == input.ClientRegId);
            }
            result = query.Select(r => new BallCheckDto
            {
                CheckDate = r.LoginDate,
                Name = r.Customer.Name,
                Age = r.Customer.Age,
                CustomerBM = r.CustomerBM,
                CheckSate = r.CheckSate,
                SummSate = r.SummSate,
                ClientName = r.ClientInfo.ClientName,
                Department = r.Customer.Department,
                TeamName = r.ClientTeamInfo.TeamName,
                SumPrice = r.CustomerItemGroup.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Sum(o => o.ItemPrice),
                GePrice = r.McusPayMoney.PersonalMoney,
                TuanPrice = r.McusPayMoney.ClientMoney,
                GePayPrice = r.McusPayMoney.PersonalPayMoney,
                检查明细 = r.CustomerItemGroup.Where(o => o.IsAddMinus != (int)AddMinusType.Minus).Select(
                    p => new ChargeCustmoerItemGroupDto
                    {

                        项目检查状态 = p.CheckState == 1 ? "未检" :
                               p.CheckState == 2 ? "已检" :
                                p.CheckState == 3 ? "部分检" :
                                p.CheckState == 4 ? "放弃" :
                                 p.CheckState == 5 ? "待查" : "",

                        科室名称 = p.DepartmentName,
                        项目加减状态 = p.IsAddMinus == 1 ? "正常" :
                                p.IsAddMinus == 2 ? "加项" :
                                p.IsAddMinus == 3 ? "减项" : "",
                        组合名称 = p.ItemGroupName,
                        收费状态 = p.PayerCat == 1 ? "未付费" :
                                p.PayerCat == 2 ? "个人已付费" :
                               p.PayerCat == 3 ? "单位支付" : "",
                        折后价格 = p.PriceAfterDis
                    }).ToList()
            }).ToList();

            return result.OrderBy(o => o.ClientName).ToList();
        }


        /// <summary>
        /// 团体报表3
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ThreeBallCheckDto> GetThreeBallChecks(ThreeBallCheckDto input)
        {
            var query = _TjlApplicationForm.GetAll().Where(o => o.SQSTATUS != 3);
            if (input.StartDateTime != null)
            {
                query = query.Where(o => o.CreationTime >= input.StartDateTime);
            }
            if (input.EndDateTime != null)
            {
                query = query.Where(o => o.CreationTime < input.EndDateTime);
            }
            //var cusreg = _mcusPayMoneyRepository.GetAll();            
            query = query.Where(o => o.ClientRegId != null);
            if (input.ClientRegId.HasValue)
            {
                query = query.Where(o => o.ClientRegId == input.ClientRegId);
            }
            if (input.FZState.HasValue && input.FZState != 0)
            {
                query = query.Where(o => o.ClientReg.FZState == input.FZState);
            }
            if (input.JSState.HasValue && input.JSState != 2)
            {
                if (input.JSState == 0)
                { query = query.Where(o => o.ClientReg.JSState != 1); }
                else
                {
                    query = query.Where(o => o.ClientReg.JSState == input.JSState);
                }
            }
            var applist = query.Select(o => new ThreeBallCheckDto
            {
                ClientName = o.ClientReg.ClientInfo.ClientName,
                ClientMoney = o.ClientReg.CustomerReg.Where(n => n.RegisterState != 1).Sum(m => m.McusPayMoney.ClientMoney),
                SQDHData = o.CreationTime.Year + "-" + o.CreationTime.Month + "-" + o.CreationTime.Day,
                InvoiceDate = o.MReceiptInfo.ChargeDate.Year + "-" + o.MReceiptInfo.ChargeDate.Month + "-" + o.MReceiptInfo.ChargeDate.Day,
                FPH = o.FPH,
                SQDH = o.SQDH,
                nvoiceMoney = o.FYZK,
                REZKJE = o.SQSTATUS == 2 ? o.FYZK : o.REFYZK,
                SQSTATUS = o.SQSTATUS,
                ClientRegId = o.ClientRegId,
                AllnvoiceMoney = (o.ClientReg.ApplicationForm.Where(p => p.SQSTATUS == 2).Sum(r => r.FYZK) +
                o.ClientReg.ApplicationForm.Where(p => p.SQSTATUS == 4).Sum(r => r.REFYZK)),
                Discount = o.ClientReg.McusPayMoney.Where(r => r.CustomerReg.RegisterState != 1).Sum(r => r.ClientMoney) == 0 ? 0 : (o.ClientReg.ApplicationForm.Sum(r => r.FYZK) / o.ClientReg.McusPayMoney.Where(r => r.CustomerReg.RegisterState != 1).Sum(r => r.ClientMoney)),
                FPName = o.FPName,
                FZState = o.ClientReg.FZState,
                FZTime = o.ClientReg.FZTime,
                JSState = o.ClientReg.JSState ?? 0,
                GRPayMoney = o.ClientReg.CustomerReg.Where(r => r.RegisterState != 1).Sum(p => p.McusPayMoney.PersonalPayMoney)



            }).ToList();
            //太卡暂时去掉
            var clientregID = applist.Select(o => o.ClientRegId).Distinct().ToList();
            var groupque = _customerItemGroupRepository.GetAll().Where(o => o.CustomerRegBM.ClientRegId != null
              && o.IsAddMinus != 3 && o.ItemGroupBM.MaxDiscount == 1
              && clientregID.Contains(o.CustomerRegBM.ClientRegId) && o.CustomerRegBM.RegisterState != 1 && o.PayerCat == (int)PayerCatType.ClientCharge).GroupBy(o => o.CustomerRegBM.ClientRegId).Select(
                o => new { o.Key, ItemPrice = o.Sum(r => r.ItemPrice) });
            var cusGrouplist = groupque.Where(o => o.ItemPrice != null && o.ItemPrice > 0).ToList();
            if (cusGrouplist.Count() > 0)
            {
                foreach (var cusitemg in applist)
                {
                    var lscusGroup = cusGrouplist.FirstOrDefault(o => o.Key == cusitemg.ClientRegId);
                    if (lscusGroup != null && lscusGroup.ItemPrice != null && lscusGroup.ItemPrice > 0)
                    {
                        cusitemg.Discount = (cusitemg.AllnvoiceMoney + cusitemg.GRPayMoney - lscusGroup.ItemPrice) / (cusitemg.ClientMoney + cusitemg.GRPayMoney - lscusGroup.ItemPrice);
                    }
                }
            }

            return applist.OrderBy(o => o.ClientName).ThenBy(o => o.SQDH).ToList();
        }

        /// <summary>
        /// 团体金额统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public List<ClientGroupStatisDto> clientGroupStatisDtos(SearchClientGroupDto input)
        {
            List<ClientGroupStatisDto> clientGroupStatisDtos = new List<ClientGroupStatisDto>();
            var que = _customerItemGroupRepository.GetAll().Where(o => o.CustomerRegBM.ClientRegId != null && o.IsAddMinus != 3 &&
            o.CustomerRegBM.RegisterState != 1 && o.PayerCat == (int)PayerCatType.ClientCharge);

            if (input.StarDate.HasValue)
            {
                que = que.Where(o => o.CustomerRegBM.LoginDate >= input.StarDate);
            }
            if (input.EndDate.HasValue)
            {
                que = que.Where(o => o.CustomerRegBM.LoginDate < input.EndDate);
            }
            if (input.ClientRegId.HasValue)
            {
                que = que.Where(o => o.CustomerRegBM.ClientRegId == input.ClientRegId);
            }
            var outresult = que.GroupBy(o => new
            {
                o.CustomerRegBM.ClientRegId,
                o.CustomerRegBM.ClientInfo.ClientName,
                o.ItemGroupBM_Id,
                o.ItemGroupName,
                o.IsAddMinus,
                o.ItemPrice,
                o.DepartmentName
            }).Select(o => new ClientGroupStatisDto
            {
                ClientName = o.Key.ClientName,
                CroupName = o.Key.ItemGroupName,
                AddState = o.Key.IsAddMinus,
                RegCount = o.Count(),
                ItemPrice = o.Key.ItemPrice,
                AllGroupMoney = o.Sum(r => r.ItemPrice),
                DepartName = o.Key.DepartmentName,
                CheckCount = o.Where(r => r.CheckState == 2 || r.CheckState == 3).Count(),
                CheckGroupMoney = (o.Where(r => r.CheckState == 2 || r.CheckState == 3).Count() * o.Key.ItemPrice),
                NZKMoney = o.Where(r => r.ItemGroupBM.MaxDiscount == 1).Sum(r => r.ItemPrice),
                ZKMoney = o.Where(r => r.ItemGroupBM.MaxDiscount != 1).Sum(r => r.ItemPrice),
                CheckNZKMoney = o.Where(r => (r.CheckState == 2 || r.CheckState == 3) && r.ItemGroupBM.MaxDiscount == 1).Sum(r => r.ItemPrice),
                CheckZKMoney = o.Where(r => (r.CheckState == 2 || r.CheckState == 3) && r.ItemGroupBM.MaxDiscount != 1).Sum(r => r.ItemPrice),


            }).OrderBy(o => o.ClientName).ThenBy(o => o.DepartName).ThenBy(o => o.CroupName).ToList();
            return outresult;
        }
        /// <summary>
        /// 更新结算状态
        /// </summary>
        /// <param name="clientregids"></param>
        public void UPClientJSState(searchIDListDto clientregids)
        {
            var time = System.DateTime.Now;
            var jsuser = AbpSession.UserId;
            var client = _TjlClientReg.GetAll().Where(p => clientregids.Ids.Contains(p.Id)).Update(
                p => new TjlClientReg
                {
                    JSState = 1,
                    JStTime = time,
                    JSUserId = jsuser
                });

        }


    }
}
