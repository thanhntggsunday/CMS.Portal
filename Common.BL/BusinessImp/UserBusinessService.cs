using Common.BL.Interfaces;
using Common.Classes;
using Common.Dto;
using Common.Model;
using Common.ViewModel.Common;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Common.BL.BusinessImp
{
    public class UserBusinessService : IUserBusinessService
    {
        public DataTableViewModel<AppUserDto> GetAllPaging(int startIndex, int endIndex, int pageSize, int intDraw, string search)
        {
            var result = new DataTableViewModel<AppUserDto>();

            using (var context = new CommonDbContext())
            {
                var allUser = context.Users.AsQueryable()
                    .Where(o => o.Email.Contains(search) || o.FullName.Contains(search) || o.UserName.Contains(search));

                var users = context.Users.AsQueryable()
                    .Where(o => o.Email.Contains(search) || o.FullName.Contains(search) || o.UserName.Contains(search))
                    .OrderBy(o => o.Id)
                    .Skip(startIndex).Take(pageSize);

                var userDTOs = new List<AppUserDto>();

                foreach (var item in users)
                {
                    var userDto = new AppUserDto();
                    userDto.CopyFromModel(item);

                    userDTOs.Add(userDto);
                }

                result.data = userDTOs.ToArray();
                result.draw = intDraw;
                result.recordsFiltered = allUser.AsQueryable().Count();
                result.recordsTotal = allUser.AsQueryable().Count();
            }

            return result;
        }

        public void UpdateUserAvatar(AppUserDto dto)
        {
            using (var context = new CommonDbContext())
            {
                var user = context.Users.Find(dto.Id);

                user.Avatar = dto.Avatar;
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}