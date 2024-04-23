using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;

namespace Sw.Hospital.HealthExaminationSystem.Core.Editions
{
    /// <summary>
    /// 版本管理
    /// </summary>
    public class EditionManager : AbpEditionManager
    {
        /// <summary>
        /// 默认版本名称
        /// </summary>
        public const string DefaultEditionName = "Standard";

        /// <summary>
        /// 版本管理
        /// </summary>
        /// <param name="editionRepository"></param>
        /// <param name="featureValueStore"></param>
        /// <param name="unitOfWorkManager"></param>
        public EditionManager(
            IRepository<Edition> editionRepository,
            IAbpZeroFeatureValueStore featureValueStore,
            IUnitOfWorkManager unitOfWorkManager)
            : base(
                editionRepository,
                featureValueStore,
                unitOfWorkManager
            )
        {
        }
    }
}