using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.IdentityFramework;
using Abp.MultiTenancy;
using Microsoft.AspNet.Identity;
using Sw.Hospital.HealthExaminationSystem.Application.Roles.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Core;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.MultiTenancy;
using Sw.Hospital.HealthExaminationSystem.UserException.Verification;

namespace Sw.Hospital.HealthExaminationSystem.Application.Users
{
    //[AbpAuthorize(PermissionNames.Pages_Users)]
    /// <inheritdoc cref="IUserAppService" />
    [AbpAuthorize]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly TenantManager _tenantManager;
        private readonly IRepository<User, long> _userRepository;

        private readonly IRepository<FormRole, Guid> _formRoleRepository;

        private readonly IRepository<TbmDepartment, Guid> _departmentRepository;
        private readonly IRepository<TjlLoginList, Guid> _loginListRepository;
        private readonly ISqlExecutor _sqlExecutor;
        //private readonly IRepository<FormRoleUser, Guid> _formRoleRepository;

        /// <summary>
        /// 用户应用服务
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="userManager"></param>
        /// <param name="roleRepository"></param>
        /// <param name="roleManager"></param>
        /// <param name="userRepository"></param>
        /// <param name="formRoleRepository"></param>
        /// <param name="departmentRepository"></param>
        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            IRepository<Role> roleRepository,
            RoleManager roleManager, IRepository<User, long> userRepository, IRepository<FormRole, Guid> formRoleRepository, IRepository<TbmDepartment, Guid> departmentRepository,
            TenantManager tenantManager,
              ISqlExecutor sqlExecutor,
            IRepository<TjlLoginList, Guid> loginListRepository)
            : base(repository)
        {
            _sqlExecutor = sqlExecutor;
            _userManager = userManager;
            _roleRepository = roleRepository;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _formRoleRepository = formRoleRepository;
            _departmentRepository = departmentRepository;
             _tenantManager= tenantManager;
            _loginListRepository = loginListRepository;

        }

        public override async Task<UserDto> GetAsync(EntityDto<long> input)
        {
            var user = await base.GetAsync(input);
            var userRoles = await _userManager.GetRolesAsync(user.Id);
            user.Roles = userRoles.Select(ur => ur).ToArray();
            return user;
        }

        public override async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.Password = new PasswordHasher().HashPassword(input.Password);
            user.IsEmailConfirmed = true;

            //Assign roles
            user.Roles = new Collection<UserRole>();
            foreach (var roleName in input.RoleNames)
            {
                var role = await _roleManager.GetRoleByNameAsync(roleName);
                user.Roles.Add(new UserRole(AbpSession.TenantId, user.Id, role.Id));
            }

            CheckErrors(await _userManager.CreateAsync(user));

            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(user);
        }

        public override async Task<UserDto> UpdateAsync(UpdateUserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            return await GetAsync(input);
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            return user;
        }

        protected override void MapToEntity(UpdateUserDto input, User user)
        {
            ObjectMapper.Map(input, user);
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles);
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = Repository.GetAllIncluding(x => x.Roles).FirstOrDefault(x => x.Id == id);
            return await Task.FromResult(user);
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public UserViewDto GetUser(EntityDto<long> input)
        {
            if (input.Id == -1 && AbpSession.UserId.HasValue)
            {
                input.Id = AbpSession.UserId.Value;
            }

            var user = _userRepository.FirstOrDefault(r => r.Id == input.Id);

            return user.MapTo<UserViewDto>();
        }

        public List<UserFormDto> GetUsers()
        {
            var query = _userRepository.GetAll().AsNoTracking();
            query = query.Include(r => r.FormRoles);
            query = query.Include(r => r.TbmDepartments);
            var result = query.ToList();
            return result.MapTo<List<UserFormDto>>();
        }
        /// <summary>
        /// 获取单位审核人员列表
        /// </summary>
        /// <returns></returns>
        public List<UserClientZKDto> GetClientZKUsers()
        {
            var query = _userRepository.GetAll().AsNoTracking();
            query = query.Where(o=>o.ClientZKS== (int)ClientZKState.Scatter);           
           
            return query.MapTo<List<UserClientZKDto>>();
        }
        /// <summary>
        /// 不用token验证根据获取用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public List<UserFormDto> GetUsersByName(SearchUserDto input)
        {
            var tenant = _tenantManager.FindByTenancyName(input.TenantName);
            if (tenant != null && tenant.IsActive)
            {
                using (CurrentUnitOfWork.SetTenantId(tenant.Id))
                {
                    if (input.FormRole != null && input.FormRole != Guid.Empty)
                    {
                        var role = _formRoleRepository.Get(input.FormRole.Value);
                        var users = role.Users.ToList();
                        if (!string.IsNullOrWhiteSpace(input.EmployeeNum))
                            users = users.Where(r => r.EmployeeNum.Contains(input.EmployeeNum)).ToList();
                        if (!string.IsNullOrWhiteSpace(input.Name))
                            users = users.Where(r => r.Name.Contains(input.Name)).ToList();

                        return users.MapTo<List<UserFormDto>>();
                    }

                    var query = _userRepository.GetAll();
                    if (!string.IsNullOrWhiteSpace(input.EmployeeNum))
                        query = query.Where(r => r.EmployeeNum == input.EmployeeNum);
                    if (!string.IsNullOrWhiteSpace(input.Name))
                        query = query.Where(r => r.Name.Contains(input.Name));
                    if (!string.IsNullOrWhiteSpace(input.NameOrEmployeeNum))
                        query = query.Where(o => o.Name.Contains(input.NameOrEmployeeNum) || o.EmployeeNum.Contains(input.NameOrEmployeeNum));
                    var result = query.ToList();
                    return result.MapTo<List<UserFormDto>>();
                }
            }
            return null;
        }
  

        public List<UserFormDto> GetUsersByCondition(SearchUserDto input)
        {
            if (input.FormRole != null && input.FormRole != Guid.Empty)
            {
                var role = _formRoleRepository.Get(input.FormRole.Value);
                var users = role.Users.ToList();
                if (!string.IsNullOrWhiteSpace(input.EmployeeNum))
                    users = users.Where(r => r.EmployeeNum.Contains(input.EmployeeNum)).ToList();
                if (!string.IsNullOrWhiteSpace(input.Name))
                    users = users.Where(r => r.Name.Contains(input.Name)).ToList();

                return users.MapTo<List<UserFormDto>>();
            }

            var query = _userRepository.GetAll();
            if (!string.IsNullOrWhiteSpace(input.EmployeeNum))
                query = query.Where(r => r.EmployeeNum.Contains(input.EmployeeNum));
            if (!string.IsNullOrWhiteSpace(input.Name))
                query = query.Where(r => r.Name.Contains(input.Name));
            if (!string.IsNullOrWhiteSpace(input.NameOrEmployeeNum))
                query = query.Where(o => o.Name.Contains(input.NameOrEmployeeNum) || o.EmployeeNum.Contains(input.NameOrEmployeeNum));
            var result = query.ToList();
            return result.MapTo<List<UserFormDto>>();
        }

        public void CreateUser(CreateUserFormDto input)
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);
            if (user.FormRoles == null)
            {
                user.FormRoles = new List<FormRole>();
            }
            foreach (var formRoleId in input.FormRoleIds)
            {
                user.FormRoles.Add(_formRoleRepository.Get(formRoleId));
            }

            if (user.TbmDepartments == null)
            {
                user.TbmDepartments = new List<TbmDepartment>();
            }
            foreach (var departmentId in input.DepartmentIds)
            {
                user.TbmDepartments.Add(_departmentRepository.Get(departmentId));
            }

            user.TenantId = AbpSession.TenantId;
            user.Password = new PasswordHasher().HashPassword(input.Password);
            user.IsEmailConfirmed = true;
            user.Surname = input.Name;

            user.Roles = new Collection<UserRole>();

            CheckErrors(_userManager.Create(user));

            CurrentUnitOfWork.SaveChanges();
        }
        public void CreateUserHasTeandID(CreateUserFormTeandIDDto input)
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);
            if (user.FormRoles == null)
            {
                user.FormRoles = new List<FormRole>();
            }
            foreach (var formRoleId in input.FormRoleIds)
            {
                user.FormRoles.Add(_formRoleRepository.Get(formRoleId));
            }

            if (user.TbmDepartments == null)
            {
                user.TbmDepartments = new List<TbmDepartment>();
            }
            foreach (var departmentId in input.DepartmentIds)
            {
                user.TbmDepartments.Add(_departmentRepository.Get(departmentId));
            }

            user.TenantId = input.TenantId;
            user.Password = new PasswordHasher().HashPassword(input.Password);
            user.IsEmailConfirmed = true;
            user.Surname = input.Name;

            user.Roles = new Collection<UserRole>();

            CheckErrors(_userManager.Create(user));

            CurrentUnitOfWork.SaveChanges();
        }
        public void UpdateUser(UpdateUserFormDto input)
        {
            CheckUpdatePermission();

            var user = _userRepository.Get(input.Id);

            ObjectMapper.Map(input, user);

            CheckErrors(_userManager.Update(user));

            if (user.FormRoles == null)
            {
                user.FormRoles = new List<FormRole>();
            }
            else
            {
                user.FormRoles.Clear();
            }
            foreach (var formRoleId in input.FormRoleIds)
            {
                user.FormRoles.Add(_formRoleRepository.Get(formRoleId));
            }

            if (user.TbmDepartments == null)
            {
                user.TbmDepartments = new List<TbmDepartment>();
            }
            else
            {
                user.TbmDepartments.Clear();
            }
            foreach (var departmentId in input.DepartmentIds)
            {
                user.TbmDepartments.Add(_departmentRepository.Get(departmentId));
            }

            CurrentUnitOfWork.SaveChanges();
        }
       
        public void DeleteUser(EntityDto<long> input)
        {
            try
            {
                using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
                {
                
                    

                    //SqlParameter[] parameter = {
                    //     new SqlParameter("@id",input.Id)
                    //       };
                    //string sql = "delete AbpUsers where id=@id";
                    //    _sqlExecutor.Execute(sql, parameter);

                    var user = _userRepository.Get(input.Id);
                    user.FormRoles.Clear();
                    user.TbmDepartments.Clear();
                    _userRepository.Update(user);
                   _userRepository.HardDelete(user);
                   // _userManager.HardDelete(user);
                }
            }
            catch (Exception ex)
            {
                throw;
                //throw new FieldVerifyException("用户已使用不能删除！",""); 
            }
        }

        public void UpdatePassword(UpdatePwdDto input)
        {
            var user = _userRepository.Get(input.UserId);
            var passwordHasher = new PasswordHasher();
            if (passwordHasher.VerifyHashedPassword(user.Password, input.Password) == PasswordVerificationResult.Failed)
            {
                throw new FieldVerifyException("密码验证失败！", "输入密码与原密码不一致，如果忘记密码，请联系管理员重置密码。");
            }
            user.Password = passwordHasher.HashPassword(input.NewPassword);
            _userManager.Update(user);
        }

        public void ResetPassword(UpdatePwdDto input)
        {
            var user = _userRepository.Get(input.UserId);
            var passwordHasher = new PasswordHasher();
            user.Password = passwordHasher.HashPassword(input.NewPassword);
            _userManager.Update(user);
        }
        /// <summary>
        /// 保存登录记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public LoginlistDto SaveLogin(LoginlistDto input)
        {
            LoginlistDto loginlistDto = new LoginlistDto();
            TjlLoginList entity = new TjlLoginList();
            var cust = _loginListRepository.GetAll().FirstOrDefault(p=>
            p.UserId== input.UserId);
            if (cust != null && input.hasDel==1)
            {
                _loginListRepository.Delete(cust);
                loginlistDto.hasDel = 1;
            }
            
           
            entity.Id = Guid.NewGuid();
            entity.UserId = input.UserId;
            //var entity = input.MapTo<TjlLoginList>();
            
            _loginListRepository.Insert(entity);
            loginlistDto.Id = entity.Id;
            return loginlistDto;
        }
        /// <summary>
        /// 删除登录记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void DelLogin(LoginlistDto input)
        {
            var cust = _loginListRepository.GetAll().FirstOrDefault(p =>
            p.UserId == input.UserId);
            if (cust != null)
            {
                _loginListRepository.Delete(cust);
            }
            
        }
        /// <summary>
        ///查询是否注销
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool QueLogin(LoginlistDto input)
        {
            var cust = _loginListRepository.GetAll().FirstOrDefault(p =>
            p.Id==input.Id );
            if (cust != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        ///查询是否有登录用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool QueOnlinLogin(LoginlistDto input)
        {
            var cust = _loginListRepository.GetAll().FirstOrDefault(p =>
            p.UserId == input.UserId);
            if (cust != null)
            {//有登录用户
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<List<UserForComboDto>> GetComboUsers()
        {
            var users =await _userRepository.GetAllListAsync();            
            return users.MapTo<List<UserForComboDto>>();
        }

        public bool VerificationUser(VerificationUserDto input)
        {
            var username = input.UserName;
            var password = input.Password;
            var passwordHasher = new PasswordHasher();
            var user = _userRepository.GetAll().FirstOrDefault(o=>o.UserName== username);
          
            if (user !=null)
            {
              var pd=  passwordHasher.VerifyHashedPassword(user.Password, password);
                string ss = pd.ToString();
                if (pd.ToString() == "Success")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}