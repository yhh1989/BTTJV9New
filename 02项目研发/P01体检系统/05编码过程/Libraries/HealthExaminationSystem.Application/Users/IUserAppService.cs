using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Application.Roles.Dto;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Users
{
    /// <summary>
    /// �û�Ӧ�÷���
    /// </summary>
    public interface IUserAppService
#if !Proxy
        : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
#endif
    {
#if !Proxy
        /// <summary>
        /// ��ȡ��ɫ
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<RoleDto>> GetRoles();
#endif

        /// <summary>
        /// ��ȡ�û�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        UserViewDto GetUser(EntityDto<long> input);

        /// <summary>
        /// ��ȡ�û��б�
        /// </summary>
        /// <returns></returns>
        List<UserFormDto> GetUsers();
        /// <summary>
        /// ��λ������Ա�б�
        /// </summary>
        /// <returns></returns>
        List<UserClientZKDto> GetClientZKUsers();
        /// <summary>
        /// �����û��б�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<UserFormDto> GetUsersByCondition(SearchUserDto input);

        List<UserFormDto> GetUsersByName(SearchUserDto input);

        /// <summary>
        /// �����û�
        /// </summary>
        /// <param name="input"></param>
        void CreateUser(CreateUserFormDto input);
        /// <summary>
        /// ����ָ���⻧�û�
        /// </summary>
        /// <param name="input"></param>
        void CreateUserHasTeandID(CreateUserFormTeandIDDto input);

        /// <summary>
        /// �����û�
        /// </summary>
        /// <param name="input"></param>
        void UpdateUser(UpdateUserFormDto input);

        /// <summary>
        /// ɾ���û�
        /// </summary>
        /// <param name="input"></param>
        void DeleteUser(EntityDto<long> input);

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="input"></param>
        void UpdatePassword(UpdatePwdDto input);

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="input"></param>
        void ResetPassword(UpdatePwdDto input);
        /// <summary>
        /// �����¼��Ϣ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        LoginlistDto SaveLogin(LoginlistDto input);
        /// <summary>
        /// ɾ����¼��Ϣ
        /// </summary>
        /// <param name="input"></param>
        void DelLogin(LoginlistDto input);

        bool QueLogin(LoginlistDto input);
        /// <summary>
        /// �ж��Ƿ��е�¼�û�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool QueOnlinLogin(LoginlistDto input);
        /// <summary>
        /// ��ȡ��������û��б�
        /// </summary>
        /// <returns></returns>

#if Application
        Task<List<UserForComboDto>> GetComboUsers();
#elif Proxy
        List<UserForComboDto> GetComboUsers();
#endif
        /// <summary>
        /// �ж��û���   �����Ƿ�һ��
        /// </summary>
        /// <param name="input"></param>
        bool VerificationUser(VerificationUserDto input);
    }
}