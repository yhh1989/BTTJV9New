using System.Collections.Generic;

namespace Abp.Application.Services.Dto
{
    public class ListResultDto<T>
    {
        public IList<T> Items { get; set; }
    }
}