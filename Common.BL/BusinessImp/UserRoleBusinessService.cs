using Common.BL.Interfaces;
using Common.Dto;
using Common.ViewModel.Common;
using System.Collections.Generic;
using System.Linq;
using Common.Model;

namespace Common.BL.BusinessImp
{
    public class UserRoleBusinessService : IUserRoleBusinessService
    {
        public DataTableViewModel<AppUserRolesDto> GetAllUserRolesPaging(int startIndex, int endIndex, int pageSize, int intDraw, string search)
        {
            var result = new DataTableViewModel<AppUserRolesDto>();
            var data = new List<AppUserRolesDto>();

            using (var context = new CommonDbContext())
            {
                var query = from user in context.Users
                            join user_role in context.UserRoles on user.Id equals user_role.UserId
                            join role in context.AppRoles on user_role.RoleId equals role.Id
                            //where post.ID == id
                            select new AppUserRolesDto
                            {
                                UserId = user.Id,
                                Email = user.Email,
                                UserName = user.UserName,
                                RoleName = role.Name
                            };

                result.data = query.Where(o => o.RoleName.Contains(search) || o.Email.Contains(search)).OrderBy(o => o.UserId).Skip(startIndex).Take(pageSize).ToArray();
                result.draw = intDraw;
                result.recordsFiltered = query.Where(o => o.RoleName.Contains(search) || o.Email.Contains(search)).Count();
                result.recordsTotal = query.Where(o => o.RoleName.Contains(search) || o.Email.Contains(search)).Count();

                result.returnStatus = true;
                result.returnMessage.Add("Get Successfully!");
            }

            return result;
        }

        public AppUserDto[] GetJsonAllUser()
        {
            using (var context = new CommonDbContext())
            {
                var query = from user in context.Users
                                //where post.ID == id
                            select new AppUserDto
                            {
                                Id = user.Id,
                                Address = user.Address,
                                Avatar = user.Avatar,
                                BirthDay = user.BirthDay,
                                Email = user.Email,
                                FullName = user.FullName,
                                Gender = user.Gender,
                                PhoneNumber = user.PhoneNumber,
                                Status = user.Status,
                                UserName = user.UserName
                            };

                return query.ToArray();
            }
        }

        public AppRoleDto[] GetJsonAllRole()
        {
            using (var context = new CommonDbContext())
            {
                var query = from role in context.AppRoles
                            select new AppRoleDto
                            {
                                Id = role.Id,
                                Name = role.Name,
                                Description = role.Description
                            };

                return query.ToArray();
            }
        }

        public AppRoleDto[] GetJsonAllRoleOfUserByUserId(string userId)
        {
            using (var context = new CommonDbContext())
            {
                var query = from role in context.AppRoles
                            join user_role in context.UserRoles on role.Id equals user_role.RoleId
                            where user_role.UserId == userId
                            select new AppRoleDto
                            {
                                Id = role.Id,
                                Name = role.Name,
                                Description = role.Description
                            };

                return query.ToArray();
            }
        }

        public AppUserDto GetById(string Id)
        {
            using (var context = new CommonDbContext())
            {
                var query = from u in context.Users
                            select new AppUserDto
                            {
                                Id = u.Id,
                                Email = u.Email,
                                UserName = u.UserName
                            };
                var user = query.Where(o => o.Id == Id).FirstOrDefault();

                return user;
            }
        }
    }
}