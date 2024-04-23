using System;

namespace Abp.Application.Services.Dto
{
    public class FullAuditedEntityDto<TPrimaryKey> : EntityDto<TPrimaryKey>
    {
     

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsDeleted { get; set; }

        public long? DeleterUserId { get; set; }

        public DateTime? DeletionTime { get; set; }
        
        public DateTime? LastModificationTime { get; set; }

        public long? LastModifierUserId { get; set; }

        public DateTime CreationTime { get; set; }

        public long? CreatorUserId { get; set; }
    }
}