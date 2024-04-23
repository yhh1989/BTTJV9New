using System;
using System.IO;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Sw.Hospital.HealthExaminationSystem.Application.Picture.Dto;
using Sw.Hospital.HealthExaminationSystem.UserException.Others;

namespace Sw.Hospital.HealthExaminationSystem.Application.Picture
{
    [AbpAuthorize]
    public class PictureAppService : MyProjectAppServiceBase, IPictureAppService
    {
        private readonly IRepository<Core.AppSystem.Picture, Guid> _pictureRepository;
        private readonly IRepository<Core.AppSystem.UserPicture, Guid> _UserpictureRepository;
        public PictureAppService(IRepository<Core.AppSystem.Picture, Guid> pictureRepository,
            IRepository<Core.AppSystem.UserPicture, Guid> UserpictureRepository)
        {
            _pictureRepository = pictureRepository;
            _UserpictureRepository = UserpictureRepository;
        }

        public PictureDto Create(CreateOrUpdatePictureDto input)
        {
            var entity = input.MapTo<Core.AppSystem.Picture>();
            var result = _pictureRepository.Insert(entity);
            return result.MapTo<PictureDto>();
        }
        public PictureDto CreateUser(CreateOrUpdatePictureDto input)
        {
            var entity = input.MapTo<Core.AppSystem.UserPicture>();
            var result = _UserpictureRepository.Insert(entity);
            return result.MapTo<PictureDto>();
        }
        public PictureDto CreateTenaId(CreateOrUpdatePictureTenIdDto input)
        {
            var entity = input.MapTo<Core.AppSystem.Picture>();
            var result = _pictureRepository.Insert(entity);
            return result.MapTo<PictureDto>();
        }

        [AbpAllowAnonymous]
        public PictureDto GetById(EntityDto<Guid> input)
        {
            try
            {
                var result = _pictureRepository.Get(input.Id);
                return result.MapTo<PictureDto>();
            }
            catch (EntityNotFoundException e)
            {
                throw new IdNotFoundExecption(e.Message, "未查询到任何图片！");
            }
        }
        [AbpAllowAnonymous]
        public PictureDto GetByIdUser(EntityDto<Guid> input)
        {
            try
            {
                var result = _UserpictureRepository.Get(input.Id);
                return result.MapTo<PictureDto>();
            }
            catch (EntityNotFoundException e)
            {
                throw new IdNotFoundExecption(e.Message, "未查询到任何图片！");
            }
        }
        public PictureDto Update(CreateOrUpdatePictureDto input)
        {
            var entity = _pictureRepository.Get(input.Id);
            input.MapTo(entity);
            var result = _pictureRepository.Update(entity);
            return result.MapTo<PictureDto>();
        }
        public PictureDto UpdateUser(CreateOrUpdatePictureDto input)
        {
            var entity = _UserpictureRepository.Get(input.Id);
            input.MapTo(entity);
            var result = _UserpictureRepository.Update(entity);
            return result.MapTo<PictureDto>();
        }
        public void Delete(EntityDto<Guid> input)
        {
            var row = _pictureRepository.Get(input.Id);
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var fileName = Path.Combine(baseDirectory, row.RelativePath);
            var thumbnailFileName = Path.Combine(baseDirectory, row.Thumbnail);
            if (File.Exists(fileName))
                File.Delete(fileName);
            if (File.Exists(thumbnailFileName))
                File.Delete(thumbnailFileName);
            _pictureRepository.Delete(input.Id);
        }
        public void DeleteUser(EntityDto<Guid> input)
        {
            var row = _UserpictureRepository.Get(input.Id);
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var fileName = Path.Combine(baseDirectory, row.RelativePath);
            var thumbnailFileName = Path.Combine(baseDirectory, row.Thumbnail);
            if (File.Exists(fileName))
                File.Delete(fileName);
            if (File.Exists(thumbnailFileName))
                File.Delete(thumbnailFileName);
            _UserpictureRepository.Delete(input.Id);
        }
    }
}