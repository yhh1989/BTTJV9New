using System;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Picture.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Picture
{
    public interface IPictureAppService : IApplicationService
    {
        PictureDto Create(CreateOrUpdatePictureDto input);

        PictureDto CreateTenaId(CreateOrUpdatePictureTenIdDto input);

        PictureDto GetById(EntityDto<Guid> input);

        PictureDto Update(CreateOrUpdatePictureDto input);

        void Delete(EntityDto<Guid> input);

        PictureDto CreateUser(CreateOrUpdatePictureDto input);

       

        PictureDto GetByIdUser(EntityDto<Guid> input);

        PictureDto UpdateUser(CreateOrUpdatePictureDto input);

        void DeleteUser(EntityDto<Guid> input);
    }
}