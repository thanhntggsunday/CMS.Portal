using Common.BL.Interfaces;
using Common.Classes;
using Common.Dto;
using Common.Model;
using Common.Model.Entities;
using Common.ViewModel.Common;
using Common.ViewModel.System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Common.BL.BusinessImp
{
    public class RoleBusinessService : IRoleBusinessService
    {
        public DataTableViewModel<AppRoleDto> GetAllPaging(int startIndex, int endIndex, int pageSize, int intDraw, string search)
        {
            var result = new DataTableViewModel<AppRoleDto>();
            var roleDtos = new List<AppRoleDto>();

            using (var context = new CommonDbContext())
            {
                var allRoles = context.AppRoles.AsQueryable()
               .Where(o => o.Name.Contains(search));

                var roles = context.AppRoles.AsQueryable()
                    .Where(o => o.Name.Contains(search))
                    .OrderBy(o => o.Id)
                    .Skip(startIndex).Take(pageSize);

                foreach (var item in roles)
                {
                    var roleDto = new AppRoleDto();
                    roleDto.CopyFromaModel(item);

                    roleDtos.Add(roleDto);
                }

                result.recordsFiltered = allRoles.AsQueryable().Count();
                result.recordsTotal = allRoles.AsQueryable().Count();
            }

            result.data = roleDtos.ToArray();
            result.draw = intDraw;

            return result;
        }

        public DataTableViewModel<AppRoleDto> GetAll()
        {
            var result = new DataTableViewModel<AppRoleDto>();
            var roleDtos = new List<AppRoleDto>();

            using (var context = new CommonDbContext())
            {
                var roles = context.AppRoles.AsQueryable();

                foreach (var item in roles)
                {
                    var roleDto = new AppRoleDto();
                    roleDto.CopyFromaModel(item);

                    roleDtos.Add(roleDto);
                }

                result.recordsFiltered = roles.AsQueryable().Count();
                result.recordsTotal = roles.AsQueryable().Count();
            }

            result.data = roleDtos.ToArray();

            return result;
        }

        public void Create(RoleViewModel viewModel)
        {
            var role = new AppRole();
            role.Name = viewModel.Name;
            role.Description = viewModel.Description;

            using (var context = new CommonDbContext())
            {
                context.AppRoles.Add(role);
                context.SaveChanges();
            }

            viewModel.ReturnStatus = true;
            viewModel.ReturnMessage.Add("Created success!");

            return;
        }

        public void Update(RoleViewModel viewModel)
        {
            using (var context = new CommonDbContext())
            {
                var role = context.AppRoles.Find(viewModel.Id);

                role.Name = viewModel.Name;
                role.Description = viewModel.Description;
                context.Entry(role).State = EntityState.Modified;
                context.SaveChanges();
            }

            viewModel.ReturnStatus = true;
            viewModel.ReturnMessage.Add("Update success!");

            return;
        }

        public void Delete(RoleViewModel viewModel)
        {
            using (var context = new CommonDbContext())
            {
                var roleId = viewModel.Id;
                var role = context.Roles.Find(roleId);

                context.Roles.Remove(role);
                context.SaveChanges();

                viewModel.ReturnStatus = true;
                viewModel.ReturnMessage.Add("Deleted Successfully!");
            }

            return;
        }

        public RoleViewModel GetById(RoleViewModel viewModel)
        {
            using (var context = new CommonDbContext())
            {
                var user = context.AppRoles.Find(viewModel.Id);

                viewModel.CopyFromModel(user);
                viewModel.ReturnStatus = true;
                viewModel.ReturnMessage.Add("Get role successfully!");
            }

            return viewModel;
        }
    }
}