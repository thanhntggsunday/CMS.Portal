using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.BL.Interfaces;
using Common.Classes;
using Common.Constants;
using Common.Dto;
using Common.Model.Entities;
using Common.ViewModel.Common;
using Common.ViewModel.System;
using e_Learning.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Portal.ServerProvider;

namespace Portal.Controllers.Cma
{
    public class CmaUserController : AdmBaseController
    {
        private ApplicationUserManager _userManager;
        private IUserBusinessService _userBusinessService;

        public CmaUserController(IUserBusinessService userBusinessService)
        {
            _userBusinessService = userBusinessService;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [Route("Index")]
        [HasCredential(Function = FunctionConstants.USER, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult Index()
        {
            return View();
        }

        [Route("GetAllPaging")]
        [HttpGet]
        [AjaxHasCredential(Function = FunctionConstants.USER, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetAllPaging()
        {
            var result = new DataTableViewModel<AppUserDto>();
            try
            {
                var draw = Request.Params.GetValues("draw").FirstOrDefault();
                var start = Request.Params.GetValues("start").FirstOrDefault();
                var length = Request.Params.GetValues("length").FirstOrDefault();
                var search = Request.Params.GetValues("search[value]").FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int startIndex = start != null ? Convert.ToInt32(start) : 0;
                int intDraw = draw != null ? Convert.ToInt32(draw) : 0;
                int endIndex = startIndex + pageSize - 1;

                result = _userBusinessService.GetAllPaging(startIndex, endIndex, pageSize, intDraw, search);

                if (result != null && result.data != null)
                {
                    for (int i = 0; i < result.data.Length; i++)
                    {
                        result.data[i].Avatar = "data:image/png;base64," + result.data[i].Avatar;
                    }
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result.returnStatus = false;
                result.returnMessage.Add(ex.Message);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("Create")]
        [HttpPost]
        [AjaxHasCredential(Function = FunctionConstants.USER, Action = LevelPermissionsConstants.CREATE)]
        public ActionResult Create(AppUserViewModel viewModel)
        {
            try
            {
                var user = new AppUser();
                viewModel.UserName = viewModel.Email;

                user.CopyFromViewModel(viewModel);
                user.Id = Guid.NewGuid().ToString();

                var result = UserManager.Create(user, viewModel.Password);
                if (result.Succeeded)
                {
                    viewModel.ReturnStatus = true;
                    viewModel.ReturnMessage.Add("Created Successfully!");

                    if (viewModel.Roles != null)
                    {
                        UserManager.AddToRoles(user.Id, viewModel.Roles.ToArray());
                    }
                }
                else
                {
                    viewModel.ReturnStatus = false;
                    viewModel.ReturnMessage.Add("Created failed!");
                    viewModel.ReturnMessage.AddRange(result.Errors);
                }
            }
            catch (Exception ex)
            {
                viewModel.ReturnStatus = false;
                viewModel.ReturnMessage.Add(ex.Message);
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Route("Update")]
        [HttpPost]
        [AjaxHasCredential(Function = FunctionConstants.USER, Action = LevelPermissionsConstants.EDIT)]
        public ActionResult Update(AppUserViewModel viewModel)
        {
            try
            {
                var user = UserManager.FindById(viewModel.Id);

                if (user.PasswordHash != viewModel.Password)
                {
                    ChangePassword(viewModel);
                }

                user.Gender = viewModel.Gender;
                user.BirthDay = viewModel.BirthDay;
                user.Avatar = viewModel.Avatar;
                // user.Address = viewModel.Address;
                user.PhoneNumber = viewModel.PhoneNumber;
                user.FullName = viewModel.FullName;
                user.Status = viewModel.Status;

                var result = UserManager.Update(user);

                if (result.Succeeded)
                {
                    viewModel.ReturnStatus = true;
                    viewModel.ReturnMessage.Add("Update Successfully!");

                    if (viewModel.Roles != null)
                    {
                        UserManager.AddToRoles(user.Id, viewModel.Roles.ToArray());
                    }
                }
                else
                {
                    viewModel.ReturnStatus = false;
                    viewModel.ReturnMessage.AddRange(result.Errors.ToList());
                }
            }
            catch (Exception ex)
            {
                viewModel.ReturnStatus = false;
                viewModel.ReturnMessage.Add(ex.Message);
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Route("GetById")]
        [HttpGet]
        [AjaxHasCredential(Function = FunctionConstants.USER, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetById(AppUserViewModel viewModel)
        {
            try
            {
                var user = UserManager.FindById(viewModel.Id);
                viewModel.CopyFromModel(user);
                viewModel.Password = viewModel.PasswordHash;

                if (viewModel.BirthDay != null)
                {
                    viewModel.StrBirthDay = viewModel.BirthDay.Value.ToString("yyyy-MM-dd");
                }
                else
                {
                    viewModel.StrBirthDay = DateTime.Now.ToString("yyyy-MM-dd");
                }

                viewModel.ReturnStatus = true;
                viewModel.ReturnMessage.Add("Get user successfully!");
            }
            catch (Exception ex)
            {
                viewModel.ReturnStatus = false;
                viewModel.ReturnMessage.Add(ex.Message);
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Route("Delete")]
        [HttpGet]
        public ActionResult Delete(AppUserViewModel viewModel)
        {
            try
            {
                var appUser = UserManager.FindById(viewModel.Id);
                var result = UserManager.Delete(appUser);

                viewModel.ReturnStatus = true;
                viewModel.ReturnMessage.Add("Deleted Successfully!");
            }
            catch (Exception ex)
            {
                viewModel.ReturnStatus = false;
                viewModel.ReturnMessage.Add(ex.Message);
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        public bool ChangePassword(AppUserViewModel usermodel)
        {
            AppUser user = UserManager.FindById(usermodel.Id);
            if (user == null)
            {
                return false;
            }
            user.PasswordHash = UserManager.PasswordHasher.HashPassword(usermodel.Password);
            var result = UserManager.Update(user);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }

        [Route("GetUserPermissions")]
        [HttpGet]
        public ActionResult GetUserPermissions()
        {
            var user = Session[SessionConstants.USER_SESSION] as AppUser;
            PermissionHandle permissionHandle = new PermissionHandle();
            if (user != null)
            {
                var userPermissions = permissionHandle.GetAllUserPermissionByUserId(user.Id);

                return Json(userPermissions, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new object(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Create()
        {
            AppUserViewModel viewModel = new AppUserViewModel();
            viewModel.Status = true;
            viewModel.Gender = "MALE";

            return View(viewModel);
        }

        public ActionResult Edit(string userId)
        {
            AppUserViewModel viewModel = new AppUserViewModel();
            var user = UserManager.FindById(userId);
            viewModel.CopyFromModel(user);
            viewModel.Id = userId;
            viewModel.Avatar = "data:image/png;base64," + viewModel.Avatar;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            // string userId = String.Empty;

            // Checking no of files injected in Request object
            if (Request.Files.Count > 0)
            {
                try
                {
                    var uid = Request.Params.GetValues("userId").FirstOrDefault();
                    var dto = new AppUserDto();
                    dto.Id = uid;

                    //  Get all files from Request object
                    HttpFileCollectionBase files = Request.Files;
                    var imgDto = new ImgDto();

                    imgDto.FileAttach = files[0];
                    FileLib.GetFileFromFileAttach(imgDto);

                    dto.Avatar = imgDto.FileContent;

                    _userBusinessService.UpdateUserAvatar(dto);

                    return Json("Success.");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        // EditUserAvatar

        public ActionResult EditUserAvatar(string userId)
        {
            AppUserViewModel viewModel = new AppUserViewModel();
            var user = UserManager.FindById(userId);
            viewModel.CopyFromModel(user);
            viewModel.Id = userId;
            viewModel.Avatar = "data:image/png;base64," + viewModel.Avatar;

            return View(viewModel);
        }
    }
}