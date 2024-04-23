using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison
{
    public class InterfaceItemAppService : IInterfaceItemAppService
    {
        private readonly IRepository<TdbInterfaceItemComparison, Guid> _InterfaceItemComparison;
        private readonly IRepository<TbmItemInfo, Guid> _TbmItemInfo;
        private readonly IRepository<TbmItemGroup, Guid> _TbmItemGroup;
        private readonly IRepository<TdbInterfaceItemGroupComparison, Guid> _InterfaceItemGroup;
        private readonly IRepository<User, long> _User;
        private readonly IRepository<TdbInterfaceEmployeeComparison, Guid> _InterfaceEmployeeComparison;
        private readonly IRepository<TbmDepartment, Guid> _TbmDepartment;
        public InterfaceItemAppService(IRepository<TdbInterfaceItemComparison, Guid> InterfaceItemComparison,
            IRepository<TbmItemInfo, Guid> TbmItemInfo,
             IRepository<TbmItemGroup, Guid> TbmItemGroup,
             IRepository<TdbInterfaceItemGroupComparison, Guid> InterfaceItemGroup,
              IRepository<User, long> User,
              IRepository<TdbInterfaceEmployeeComparison, Guid> InterfaceEmployeeComparison,
              IRepository<TbmDepartment, Guid> TbmDepartment)
        {
            _InterfaceItemComparison = InterfaceItemComparison;
            _TbmItemInfo = TbmItemInfo;
            _TbmItemGroup = TbmItemGroup;
            _InterfaceItemGroup = InterfaceItemGroup;
            _User = User;
            _InterfaceEmployeeComparison = InterfaceEmployeeComparison;
            _TbmDepartment = TbmDepartment;

        }
        /// <summary>
        /// 获取指定条件项目对应
        /// </summary>
        /// <param name="GetInterfaceItemComparison"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public List<InterfaceItemsDto> GetInterfaceItemComparison(SearchInterIFaceItemComparisonDto Interfaceresult)
        {

            var dto = new List<InterfaceItemsDto>();

            if (Interfaceresult.CheckType != "未对应")
            {
                var query = _InterfaceItemComparison.GetAllIncluding(o=>o.ItemInfo).AsNoTracking();
                if (Interfaceresult.departmentId != Guid.Empty)
                {
                    query = query.Where(o => o.ItemInfo.DepartmentId == Interfaceresult.departmentId);
                }
                if (Interfaceresult.ItemGroupId != Guid.Empty)
                {
                   // query = query.Where(o => o.ItemGroupId == Interfaceresult.ItemGroupId);
                    query = query.Where(o => o.ItemInfo.ItemGroups.Any(n=>n.Id== Interfaceresult.ItemGroupId));
                }
                if (Interfaceresult.DeptTypes != null && Interfaceresult.DeptTypes.Count > 0)
                {
                    query = query.Where(o => Interfaceresult.DeptTypes.Contains(o.ItemInfo.Department.Category));
                }
                if (Interfaceresult.ItemID != Guid.Empty)
                {
                    query = query.Where(o => o.ItemInfoId == Interfaceresult.ItemID);
                }              // var lsii = query.ToList();
                query = query.Where(o =>  o.ItemInfo.IsDeleted == false);
               // var ls = query.ToList();
                var interitem = query.Select(o => new InterfaceItemsDto
                { Id= o.Id,
                    DepartmentName = o.ItemInfo.Department==null?"": o.ItemInfo.Department.Name,
                    DepartmentId= o.ItemInfo.DepartmentId,                   
                    ItemGroupId = o.ItemInfo.ItemGroups.FirstOrDefault().Id,
                     ItemGroupName= o.ItemInfo.ItemGroups.FirstOrDefault().ItemGroupName,
                    ItemBM = o.ItemInfo.ItemBM,
                    ItemInfoId = o.ItemInfo.Id,
                    ItemName = o.ItemInfo.Name,
                    ObverseItemId=  o.ObverseItemId,
                    ObverseItemName= o.ObverseItemName,
                    Remarks= o.Remarks,
                    InstrumentModelNumber= o.InstrumentModelNumber  ,
                   departNum = o.ItemInfo.Department==null?0: o.ItemInfo.Department.OrderNum,
                    GroupNum= o.ItemInfo.ItemGroups.FirstOrDefault().OrderNum,
                    itemNum=o.ItemInfo.OrderNum
                }).ToList();
                dto = interitem.MapTo<List<InterfaceItemsDto>>();
                
            }
            //未对应项目
            if (Interfaceresult.CheckType != "已对应")
            {
                var que = _TbmItemInfo.GetAll().AsNoTracking();
                if (Interfaceresult.departmentId != Guid.Empty)
                {
                    que = que.Where(o => o.DepartmentId == Interfaceresult.departmentId);
                }
                if (Interfaceresult.ItemGroupId != Guid.Empty)
                {
                    que = que.Where(o => o.ItemGroups.Any(n => n.Id == Interfaceresult.ItemGroupId));
                    // que = que.Where(o => o.ItemGroups.Any(n => Interfaceresult.ItemGroups.Contains(n.Id)));
                }
                if (Interfaceresult.DeptTypes != null && Interfaceresult.DeptTypes.Count > 0)
                {
                    que = que.Where(o => Interfaceresult.DeptTypes.Contains(o.Department.Category));
                }
                if (Interfaceresult.ItemID != Guid.Empty)
                {
                    que = que.Where(o => o.Id == Interfaceresult.ItemID);
                }
               //var ls11 = que.ToList();
                que = que.Where(o => o.InterfaceItemComparison.Where(n =>   n.ItemInfo.IsDeleted == false).ToList().Count <= 0);
               // var ls = que.ToList();
                var itemls = que.Where(o=>o.ItemGroups.Count>0).Select(o=> new InterfaceItemsDto
                { DepartmentName=o.Department.Name,
                    DepartmentId= o.DepartmentId,
                   ItemGroupName=  o.ItemGroups.FirstOrDefault().ItemGroupName,
                    ItemGroupId =o.ItemGroups.FirstOrDefault().Id,
                     ItemBM=o.ItemBM,
                    ItemInfoId =o.Id,
                    ItemName =o.Name,
                    departNum = o.Department.OrderNum,                  
                    itemNum = o.OrderNum
                }).ToList();
               // var itemls=items.MapTo<List<InterfaceItemsDto>>();
                dto.AddRange(itemls);
            }
            dto = dto.OrderBy(o=> o.departNum).ThenBy(o => o.GroupNum).ThenBy(o => o.itemNum).ToList();
            return dto;
        }

        /// <summary>
        /// 保存项目对应
        /// </summary>
        /// <param name="SaveInterfaceItems"></param>
        /// <returns></returns>
        public InterfaceItemsDto SaveInterfaceItems(InsertInterfaceItemDto input)
        {
            InterfaceItemsDto interfaceItemsDto = new InterfaceItemsDto();
            if (input.Id == Guid.Empty)
            {
                TdbInterfaceItemComparison tdbInterfaceItemComparison = new TdbInterfaceItemComparison();
                //input.MapTo(tdbInterfaceItemComparison);
                //var tdbInterfaceItemComparison = input.MapTo<TdbInterfaceItemComparison>();
                tdbInterfaceItemComparison.Id = Guid.NewGuid();
                //tdbInterfaceItemComparison.DepartmentId = input.DepartmentId;
                //var dep = _TbmDepartment.Get(input.DepartmentId);
                //tdbInterfaceItemComparison.Department = dep;
                //tdbInterfaceItemComparison.ItemGroupId = input.ItemGroupId;
                //var itemgroup = _TbmItemGroup.Get(input.ItemGroupId);
                //tdbInterfaceItemComparison.ItemGroup = itemgroup;
                tdbInterfaceItemComparison.ItemInfoId = input.ItemInfoId;
                //var item = _TbmItemInfo.Get(input.ItemInfoId);
                //tdbInterfaceItemComparison.ItemInfo = item;
                tdbInterfaceItemComparison.ItemName = input.ItemName;
                tdbInterfaceItemComparison.ObverseItemId = input.ObverseItemId;
                tdbInterfaceItemComparison.ObverseItemName = input.ObverseItemName;
                tdbInterfaceItemComparison.InstrumentModelNumber = input.InstrumentModelNumber;
                tdbInterfaceItemComparison.Remarks = input.Remarks;

                var interfaceItem = _InterfaceItemComparison.Insert(tdbInterfaceItemComparison);
                interfaceItemsDto = interfaceItem.MapTo<InterfaceItemsDto>();
            }
            else
            {
                TdbInterfaceItemComparison tdbInterfaceItemComparison = _InterfaceItemComparison.Get(input.Id);
                //tdbInterfaceItemComparison.DepartmentId = input.DepartmentId;
                //tdbInterfaceItemComparison.ItemGroupId = input.ItemGroupId;
                tdbInterfaceItemComparison.ItemInfoId = input.ItemInfoId;
                tdbInterfaceItemComparison.ItemName = input.ItemName;
                tdbInterfaceItemComparison.ObverseItemId = input.ObverseItemId;
                tdbInterfaceItemComparison.ObverseItemName = input.ObverseItemName;
                tdbInterfaceItemComparison.InstrumentModelNumber = input.InstrumentModelNumber;
                tdbInterfaceItemComparison.Remarks = input.Remarks;
                interfaceItemsDto = _InterfaceItemComparison.Update(tdbInterfaceItemComparison).MapTo<InterfaceItemsDto>(); ;

            }
            return interfaceItemsDto;
        }
        /// <summary>
        /// 获取指定条件组合对应
        /// </summary>
        /// <param name="GetInterfaceItemComparison"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public List<InterfaceItemGroupsDto> GetInterfaceItemGroupComparison(SearchInterIFaceItemComparisonDto Interfaceresult)
        {

            var dto = new List<InterfaceItemGroupsDto>();

            if (Interfaceresult.CheckType != "未对应")
            {
                var query = _InterfaceItemGroup.GetAllIncluding(o=>o.Department,o=>o.ItemGroup);
                if (Interfaceresult.departmentId != Guid.Empty)
                {
                    query = query.Where(o => o.DepartmentId == Interfaceresult.departmentId);
                }
                if (Interfaceresult.ItemGroupId != Guid.Empty)
                {
                    query = query.Where(o => o.ItemGroupId == Interfaceresult.ItemGroupId);
                }
                if (Interfaceresult.DeptTypes != null && Interfaceresult.DeptTypes.Count > 0)
                {
                    query = query.Where(o => Interfaceresult.DeptTypes.Contains(o.Department.Category));
                }
                dto = query.OrderBy(o=>o.Department.OrderNum).ThenBy(o=>o.ItemGroup.OrderNum).MapTo<List<InterfaceItemGroupsDto>>();
            }
            //未对应项目
            if (Interfaceresult.CheckType != "已对应")
            {
                var que = _TbmItemGroup.GetAllIncluding(o => o.Department, o => o.InterfaceItemGroupComparison);
                if (Interfaceresult.departmentId != Guid.Empty)
                {
                    que = que.Where(o => o.DepartmentId == Interfaceresult.departmentId);
                }
                if (Interfaceresult.ItemGroupId != Guid.Empty)
                {
                    que = que.Where(o => o.Id == Interfaceresult.ItemGroupId);
                    // que = que.Where(o => o.ItemGroups.Any(n => Interfaceresult.ItemGroups.Contains(n.Id)));
                }
                if (Interfaceresult.DeptTypes != null && Interfaceresult.DeptTypes.Count > 0)
                {
                    que = que.Where(o => Interfaceresult.DeptTypes.Contains(o.Department.Category));
                }
                que = que.Where(o => o.InterfaceItemGroupComparison.Count <= 0);
                var items = que.ToList();
                foreach (TbmItemGroup itemgroup in items)
                {

                    InterfaceItemGroupsDto interfaceItemsDto = new InterfaceItemGroupsDto();
                    interfaceItemsDto.Id = Guid.Empty;
                    interfaceItemsDto.ItemGroupId = itemgroup.Id;
                    interfaceItemsDto.ItemGroup = itemgroup.MapTo<InterfaceGroupDto>();
                    interfaceItemsDto.ItemGroupName = itemgroup.ItemGroupName;
                    interfaceItemsDto.DepartmentId = itemgroup.DepartmentId;
                    interfaceItemsDto.Department = itemgroup.Department.MapTo<InterfaceDepartmentDto>();
                    dto.Add(interfaceItemsDto);

                }
            }
            dto = dto.OrderBy(o => o.Department?.OrderNum).ThenBy(o => o.ItemGroup?.OrderNum).ToList();
            return dto;
        }
        /// <summary>
        /// 保存组合对应
        /// </summary>
        /// <param name="SaveInterfaceItems"></param>
        /// <returns></returns>
        public InterfaceItemGroupsDto SaveInterfaceItemGroups(InsertInterfaceItemGroupDto input)
        {
            InterfaceItemGroupsDto interfaceItemsDto = new InterfaceItemGroupsDto();
            if (input.Id == Guid.Empty)
            {
                TdbInterfaceItemGroupComparison tdbInterfaceItemComparison = new TdbInterfaceItemGroupComparison();
                tdbInterfaceItemComparison.Id = Guid.NewGuid();
                tdbInterfaceItemComparison.DepartmentId = input.DepartmentId;
                //var dep = _TbmDepartment.FirstOrDefault(input.DepartmentId);
                //tdbInterfaceItemComparison.Department = dep;
                tdbInterfaceItemComparison.ItemGroupId = input.ItemGroupId;
                TbmItemGroup tbmItemGroup = _TbmItemGroup.Get(input.ItemGroupId);
                tdbInterfaceItemComparison.ItemGroupName = tbmItemGroup.ItemGroupName;
                tdbInterfaceItemComparison.ItemGroup = tbmItemGroup;
                tdbInterfaceItemComparison.ObverseItemId = input.ObverseItemId;
                tdbInterfaceItemComparison.ObverseItemName = input.ObverseItemName;
                tdbInterfaceItemComparison.InstrumentModelNumber = input.InstrumentModelNumber;
                tdbInterfaceItemComparison.Remarks = input.Remarks;

                interfaceItemsDto = _InterfaceItemGroup.Insert(tdbInterfaceItemComparison).MapTo<InterfaceItemGroupsDto>(); ;
            }
            else
            {
                TdbInterfaceItemGroupComparison tdbInterfaceItemComparison = _InterfaceItemGroup.Get(input.Id);
                tdbInterfaceItemComparison.DepartmentId = input.DepartmentId;
                tdbInterfaceItemComparison.ItemGroupId = input.ItemGroupId;
                tdbInterfaceItemComparison.ObverseItemId = input.ObverseItemId;
                tdbInterfaceItemComparison.ObverseItemName = input.ObverseItemName;
                tdbInterfaceItemComparison.InstrumentModelNumber = input.InstrumentModelNumber;
                tdbInterfaceItemComparison.Remarks = input.Remarks;
                interfaceItemsDto = _InterfaceItemGroup.Update(tdbInterfaceItemComparison).MapTo<InterfaceItemGroupsDto>(); ;

            }
            return interfaceItemsDto;
        }
        /// <summary>
        /// 获取用户对应
        /// </summary>
        /// <param name="Interfaceresult"></param>
        /// <returns></returns>
        [UnitOfWork(false)]
        public List<InterfaceUserDto> getInterfaceUser(SearchInterIFaceItemComparisonDto Interfaceresult)
        {
            var dto = new List<InterfaceUserDto>();

            if (Interfaceresult.CheckType != "未对应")
            {
                var query = _InterfaceEmployeeComparison.GetAllIncluding(o=>o.Employee).AsNoTracking();
               

                //if (Interfaceresult.EmpID != null && Interfaceresult.EmpID != 0)
                if (Interfaceresult.EmpID != 0)
                {
                    query = query.Where(o => o.EmployeeId == Interfaceresult.EmpID);
                }
                dto = query.Select(o=>new InterfaceUserDto
                { EmployeeNum= o.Employee.EmployeeNum,
                   EmployeeName= o.EmployeeName,
                     EmployeeId= o.EmployeeId,
                     Id=o.Id,
                    ObverseEmpId= o.ObverseEmpId,
                    ObverseEmpName= o.ObverseEmpName}).ToList();

                //dto = userlis.MapTo<List<InterfaceUserDto>>();
            }
            //未对应项目
            if (Interfaceresult.CheckType != "已对应")
            {
                var que = _User.GetAllIncluding(o=>o.InterfaceEmployeeComparison).AsNoTracking();                
                //if (Interfaceresult.EmpID != null && Interfaceresult.EmpID!=0)
                if (Interfaceresult.EmpID!=0)
                {
                    que = que.Where(o => o.Id == Interfaceresult.EmpID);
                   
                }               
                que = que.Where(o => o.InterfaceEmployeeComparison.Count <= 0);
                var items = que.ToList();
                foreach (User use in items)
                {
                    InterfaceUserDto interfaceItemsDto = new InterfaceUserDto();
                    interfaceItemsDto.Id = Guid.Empty;
                    interfaceItemsDto.EmployeeId = use.Id;
                    interfaceItemsDto.EmployeeName = use.Name;
                    interfaceItemsDto.EmployeeNum = use.EmployeeNum;                   
                    dto.Add(interfaceItemsDto);
                }
               
            }
            dto = dto.OrderBy(o => o.EmployeeName).ToList();
            return dto;

        }
        /// <summary>
        /// 保存用户对应
        /// </summary>
        /// <param name="SaveInterfaceItems"></param>
        /// <returns></returns>
        public InterfaceUserDto SaveInterfaceUser(InsertInterfaceEmpDto input)
        {
            InterfaceUserDto interfaceItemsDto = new InterfaceUserDto();
            if (input.Id == Guid.Empty)
            {
                TdbInterfaceEmployeeComparison tdbInterfaceItemComparison = new TdbInterfaceEmployeeComparison();
                tdbInterfaceItemComparison.Id = Guid.NewGuid();
                tdbInterfaceItemComparison.EmployeeId = input.EmployeeId;
                tdbInterfaceItemComparison.EmployeeName = input.EmployeeName;
                tdbInterfaceItemComparison.ObverseEmpId = input.ObverseEmpId;
                tdbInterfaceItemComparison.ObverseEmpName = input.ObverseEmpName;
                interfaceItemsDto = _InterfaceEmployeeComparison.Insert(tdbInterfaceItemComparison).MapTo<InterfaceUserDto>(); 
            }
            else
            {
                TdbInterfaceEmployeeComparison tdbInterfaceItemComparison = _InterfaceEmployeeComparison.Get(input.Id);
                tdbInterfaceItemComparison.EmployeeId = input.EmployeeId;
                tdbInterfaceItemComparison.EmployeeName = input.EmployeeName;
                tdbInterfaceItemComparison.ObverseEmpId = input.ObverseEmpId;
                tdbInterfaceItemComparison.ObverseEmpName = input.ObverseEmpName;
                interfaceItemsDto = _InterfaceEmployeeComparison.Update(tdbInterfaceItemComparison).MapTo<InterfaceUserDto>(); ;

            }
            return interfaceItemsDto;
        }
        /// <summary>
        /// 获取项目对应
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public InterfaceItemsDto getInterfaceItems(SearchInterfaceItemDto input)
        {
           var dto = new InterfaceItemsDto();         
           dto = _InterfaceItemComparison.FirstOrDefault(o => o.ItemInfo.Department.Name == input.DeptName && o.ItemInfo.ItemGroups.Any(p=>p.ItemGroupName== input.GroupName) && o.ItemName==input.ItemName && o.ObverseItemId==input.ObverseItemId && o.ObverseItemName==input.ObverseItemName && o.InstrumentModelNumber==input.InstrumentModelNumber).MapTo<InterfaceItemsDto>();
               
            if (dto==null)
            {
                TbmItemGroup tbmItemGroup = _TbmItemGroup.FirstOrDefault(o=>o.ItemGroupName==input.GroupName && o.Department.Name ==input.DeptName);
                var item = _TbmItemInfo.FirstOrDefault(o=>o.Name==input.ItemName &&  o.Department.Name==input.DeptName && o.ItemGroups.Select(n=>n.ItemGroupName).Contains(input.GroupName));
                if (item != null && tbmItemGroup !=null)
                {
                    InterfaceItemsDto interfaceItemsDto = new InterfaceItemsDto();
                    interfaceItemsDto.Id = Guid.Empty;
                    interfaceItemsDto.ItemGroupId = tbmItemGroup.Id;
                    interfaceItemsDto.ItemGroupName = tbmItemGroup.ItemGroupName;
                    interfaceItemsDto.ItemInfoId = item.Id;
                    interfaceItemsDto.ItemBM = item.ItemBM;
                    interfaceItemsDto.ItemName = item.Name;
                    interfaceItemsDto.DepartmentId = item.DepartmentId;
                    interfaceItemsDto.DepartmentName = item.Department.Name;
                    interfaceItemsDto.ObverseItemId = input.ObverseItemId;
                    interfaceItemsDto.ObverseItemName = input.ObverseItemName;
                    interfaceItemsDto.InstrumentModelNumber = input.InstrumentModelNumber;
                    interfaceItemsDto.Remarks = input.Remarks;
                    dto = interfaceItemsDto;
                }               
                
            }
            else
            {
                dto.ObverseItemId = input.ObverseItemId;
                dto.ObverseItemName = input.ObverseItemName;
                dto.InstrumentModelNumber = input.InstrumentModelNumber;
                dto.Remarks = input.Remarks;

            }
            return dto;
        }

        /// <summary>
        /// 获取组合对应
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public InterfaceItemGroupsDto getInterfaceItemGroups(SearchInterfaceItemDto input)
        {
            var dto = new InterfaceItemGroupsDto();
            dto = _InterfaceItemGroup.FirstOrDefault(o => o.Department.Name == input.DeptName && o.ItemGroup.ItemGroupName == input.GroupName  && o.ObverseItemId == input.ObverseItemId && o.ObverseItemName == input.ObverseItemName && o.InstrumentModelNumber == input.InstrumentModelNumber).MapTo<InterfaceItemGroupsDto>();

            if (dto == null)
            {
                TbmItemGroup tbmItemGroup = _TbmItemGroup.FirstOrDefault(o => o.ItemGroupName == input.GroupName && o.Department.Name==input.DeptName);
                if (tbmItemGroup != null)
                {
                    InterfaceItemGroupsDto interfaceItemsDto = new InterfaceItemGroupsDto();
                    interfaceItemsDto.Id = Guid.Empty;
                    interfaceItemsDto.ItemGroupId = tbmItemGroup.Id;
                    interfaceItemsDto.ItemGroup = tbmItemGroup.MapTo<InterfaceGroupDto>();
                    interfaceItemsDto.DepartmentId = tbmItemGroup.DepartmentId;
                    interfaceItemsDto.Department = tbmItemGroup.Department.MapTo<InterfaceDepartmentDto>();
                    interfaceItemsDto.ObverseItemId = input.ObverseItemId;
                    interfaceItemsDto.ObverseItemName = input.ObverseItemName;
                    interfaceItemsDto.InstrumentModelNumber = input.InstrumentModelNumber;
                    dto = interfaceItemsDto;
                }
            }
            else
            {
                dto.ObverseItemId = input.ObverseItemId;
                dto.ObverseItemName = input.ObverseItemName;
                dto.InstrumentModelNumber = input.InstrumentModelNumber;
                dto.Remarks = input.Remarks;

            }
            return dto;
        }

        /// <summary>
        /// 获取医生对应
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public InterfaceUserDto getInterfaceEmp(SearchInterfaceEmpDto input)
        {
            var dto = new InterfaceUserDto();            
            dto = _InterfaceEmployeeComparison.FirstOrDefault(o => o.Employee.Name == input.EmployeeName && o.Employee.EmployeeNum==input.EmployeeNum   && o.ObverseEmpId==input.ObverseEmpId && o.ObverseEmpName==input.ObverseEmpName ).MapTo<InterfaceUserDto>();

            if (dto == null)
            {
                var use = _User.FirstOrDefault(o => o.EmployeeNum == input.EmployeeNum && o.Name == input.EmployeeName);
                if (use != null)
                {
                    InterfaceUserDto interfaceItemsDto = new InterfaceUserDto();
                    interfaceItemsDto.Id = Guid.Empty;
                    interfaceItemsDto.EmployeeId = use.Id;
                    interfaceItemsDto.EmployeeName = use.Name;
                    interfaceItemsDto.EmployeeNum = use.EmployeeNum;
                    interfaceItemsDto.ObverseEmpId = input.ObverseEmpId;
                    interfaceItemsDto.ObverseEmpName = input.ObverseEmpName;
                    dto = interfaceItemsDto;
                }
            }
            else
            {
                dto.ObverseEmpId = input.ObverseEmpId;
                dto.ObverseEmpName = input.ObverseEmpName;

            }
            return dto;
        }
        public void delInterface(ChargeBM input)
        {

            if (input.Name == "项目对应")
            {

                if (input.Id != Guid.Empty)
                {
                    _InterfaceItemComparison.Delete(input.Id);
                }
            }
            else if (input.Name == "组合对应")
            {
                if (input.Id != Guid.Empty)
                {
                    _InterfaceItemGroup.Delete(input.Id);
                }
            }
            else if (input.Name == "医生对应")
            {
                if (input.Id != Guid.Empty)
                {
                    _InterfaceEmployeeComparison.Delete(input.Id);
                }

            }

        }

    }
}
