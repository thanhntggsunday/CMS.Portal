using Common.Dto;
using Common.Model.Entities;
using Common.ViewModel.System;

namespace Common.Classes
{
    public static class Extensions
    {
        #region AppRole

        public static void CopyFromaModel(this AppRoleDto roleDTO, AppRole appRole)
        {
            roleDTO.Id = appRole.Id;
            roleDTO.Name = appRole.Name;
            roleDTO.Description = appRole.Description;
        }

        public static void CopyFromModel(this RoleViewModel vm, AppRole appRole)
        {
            vm.Id = appRole.Id;
            vm.Name = appRole.Name;
            vm.Description = appRole.Description;
        }

        #endregion AppRole

        #region AppUser

        public static void CopyFromViewModel(this AppUser appUser, AppUserViewModel appUserViewModel)
        {
            appUser.Id = appUserViewModel.Id;
            appUser.FullName = appUserViewModel.FullName;
            appUser.BirthDay = appUserViewModel.BirthDay;
            appUser.Email = appUserViewModel.Email;
            appUser.Address = appUserViewModel.Address;
            appUser.UserName = appUserViewModel.UserName;
            appUser.PhoneNumber = appUserViewModel.PhoneNumber;
            appUser.Gender = appUserViewModel.Gender;
            appUser.Status = appUserViewModel.Status;
            appUser.Address = appUserViewModel.Address;
            appUser.Avatar = appUserViewModel.Avatar;
            appUser.Department = appUserViewModel.Department;
            appUser.Position = appUserViewModel.Position;
            appUser.Country = appUserViewModel.Country;
            appUser.City = appUserViewModel.City;
            appUser.CountryRegionCode = appUserViewModel.CountryRegionCode;
            appUser.Postcode = appUserViewModel.Postcode;
        }

        public static void CopyFromModel(this AppUserDto appUserDto, AppUser appUser)
        {
            appUserDto.Id = appUser.Id;
            appUserDto.FullName = appUser.FullName;
            appUserDto.BirthDay = appUser.BirthDay;
            appUserDto.Email = appUser.Email;
            appUserDto.Address = appUser.Address;
            appUserDto.UserName = appUser.UserName;
            appUserDto.PhoneNumber = appUser.PhoneNumber;
            appUserDto.Gender = appUser.Gender;
            appUserDto.Status = appUser.Status;
            appUserDto.Address = appUser.Address;
            appUserDto.Avatar = appUser.Avatar;
            appUserDto.PasswordHash = appUser.PasswordHash;
        }

        public static void CopyFromModel(this AppUserViewModel appUserViewModel, AppUser appUser)
        {
            appUserViewModel.Id = appUser.Id;
            appUserViewModel.FullName = appUser.FullName;
            appUserViewModel.BirthDay = appUser.BirthDay;
            appUserViewModel.Email = appUser.Email;
            appUserViewModel.Address = appUser.Address;
            appUserViewModel.UserName = appUser.UserName;
            appUserViewModel.PhoneNumber = appUser.PhoneNumber;
            appUserViewModel.Gender = appUser.Gender;
            appUserViewModel.Status = appUser.Status;
            appUserViewModel.Address = appUser.Address;
            appUserViewModel.Avatar = appUser.Avatar;
            appUserViewModel.PasswordHash = appUser.PasswordHash;
            appUserViewModel.Position = appUser.Position;
            appUserViewModel.Department = appUser.Department;
            appUserViewModel.Country = appUser.Country;
            appUserViewModel.City = appUser.City;
            appUserViewModel.CountryRegionCode = appUser.CountryRegionCode;
            appUserViewModel.Postcode = appUser.Postcode;
        }

        #endregion AppUser
    }
}