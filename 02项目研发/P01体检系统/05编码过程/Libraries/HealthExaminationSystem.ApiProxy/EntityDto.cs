namespace Abp.Application.Services.Dto
{
    public interface IEntityDto<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }

    public class EntityDto<TPrimaryKey> : IEntityDto<TPrimaryKey>
    {
		public EntityDto()
		{
		}

		public EntityDto(TPrimaryKey id)
	    {
		    Id = id;
	    }

	    public TPrimaryKey Id { get; set; }
    }
}