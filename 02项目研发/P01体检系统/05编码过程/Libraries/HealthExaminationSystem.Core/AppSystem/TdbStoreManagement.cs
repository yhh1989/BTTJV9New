using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.AppSystem
{
    /// <summary>
    /// 多店管理表
    /// </summary>
    [Obsolete("暂停使用", true)]
    public class TdbStoreManagement : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 门店id
        /// </summary>
        public virtual int StoreID { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public virtual string StoreName { get; set; }

        /// <summary>
        /// 所属省
        /// </summary>
        public virtual string StoreAdressP { get; set; }

        /// <summary>
        /// 所属市
        /// </summary>
        public virtual string StoreAdressS { get; set; }

        /// <summary>
        /// 所属区
        /// </summary>
        public virtual string StoreAdressQ { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string StoreAdress { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public virtual string StoreAnswer { get; set; }

        /// <summary>
        /// 负责人电话
        /// </summary>
        public virtual string StoreAnswerTel { get; set; }

        /// <summary>
        /// 信息负责人
        /// </summary>
        public virtual string StoreInfo { get; set; }

        /// <summary>
        /// 信息负责人电话
        /// </summary>
        public virtual string StoreInfoTel { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public virtual int StoreLevel { get; set; }

        /// <summary>
        /// 上级门店ID
        /// </summary>
        public virtual int StoreParent { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 套装名称
        /// </summary>
        public virtual string DataSetName { get; set; }

        /// <summary>
        /// 服务器
        /// </summary>
        public virtual string ServerIP { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public virtual string DBName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string DBUserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string DBUserPassword { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public virtual string Port { get; set; }

        /// <summary>
        /// 最大连接数
        /// </summary>
        public virtual int MaxPoolSize { get; set; }

        /// <summary>
        /// 是否启用连接池
        /// </summary>
        public virtual int Pooling { get; set; }

        /// <summary>
        /// 档案号前缀
        /// </summary>
        public virtual string ArchivesNUMPrefix { get; set; }

        /// <summary>
        /// ID前缀
        /// </summary>
        public virtual string IDNumberPrefix { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual int State { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Password { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}