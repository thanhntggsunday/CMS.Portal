using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.BL.Interfaces;
using Common.Constants;
using Common.Dto;
using Common.Model;
using Common.ViewModel.Common;
using Common.ViewModel.System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Portal.ServerProvider;

namespace Portal.Controllers.Cma
{
    public class CmaUserRoleController : AdmBaseController
    {
        private ApplicationUserManager _userManager;
        private IUserRoleBusinessService _userRoleBusinessService;

        public CmaUserRoleController(IUserRoleBusinessService userRoleBusinessService)
        {
            _userRoleBusinessService = userRoleBusinessService;
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
        [HasCredential(Function = FunctionConstants.USER_ROLE, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult Index()
        {
            return View();
        }

        [Route("GetAllUserRolesPaging")]
        [AjaxHasCredential(Function = FunctionConstants.USER_ROLE, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetAllUserRolesPaging()
        {
            var result = new DataTableViewModel<AppUserRolesDto>();
            var data = new List<AppUserRolesDto>();

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

                result = _userRoleBusinessService.GetAllUserRolesPaging(startIndex, endIndex, pageSize, intDraw, search);
            }
            catch (Exception ex)
            {
                result.returnStatus = false;
                result.returnMessage.Add(ex.Message);

                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Route("GetJsonAllUser")]
        [AjaxHasCredential(Function = FunctionConstants.USER_ROLE, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetJsonAllUser()
        {
            var result = _userRoleBusinessService.GetJsonAllUser();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Route("GetJsonAllRole")]
        [AjaxHasCredential(Function = FunctionConstants.USER_ROLE, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetJsonAllRole()
        {
            var result = _userRoleBusinessService.GetJsonAllRole();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Route("GetJsonAllRoleOfUserByUserId")]
        [AjaxHasCredential(Function = FunctionConstants.USER_ROLE, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetJsonAllRoleOfUserByUserId(string userId)
        {
            var result = _userRoleBusinessService.GetJsonAllRoleOfUserByUserId(userId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Route("AssignUserRole")]
        [HttpPost]
        [AjaxHasCredential(Function = FunctionConstants.USER_ROLE, Action = LevelPermissionsConstants.ASSIGN_USER_TO_ROLE)]
        public ActionResult AssignUserRole(UserRoleAssignViewModel viewModel)
        {
            try
            {
                if (viewModel == null)
                {
                    viewModel = new UserRoleAssignViewModel();
                    viewModel.ReturnStatus = false;
                    viewModel.ReturnMessage.Add("Error view model is null");

                    return Json(viewModel, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    using (var context = new CommonDbContext())
                    {
                        var arrUserIds = viewModel.UserIds.Split(';');
                        var arrRoleNames = viewModel.RoleNames.Split(';');

                        for (int uix = 0; uix < arrUserIds.Length; uix++)
                        {
                            var userid = arrUserIds[uix];

                            var query = from user in context.Users
                                        join user_role in context.UserRoles on user.Id equals user_role.UserId
                                        join role in context.AppRoles on user_role.RoleId equals role.Id
                                        where user.Id == userid
                                        select new AppUserRolesDto
                                        {
                                            UserId = user.Id,
                                            Email = user.Email,
                                            UserName = user.UserName,
                                            RoleName = role.Name,
                                            RoleId = role.Id
                                        };
                            var userRoles = query.ToArray();

                            // Clear user-roles for assign:
                            for (int i = 0; i < userRoles.Length; i++)
                            {
                                UserManager.RemoveFromRole(userid, userRoles[i].RoleName);
                            }

                            // Assig user to role
                            for (int rix = 0; rix < arrRoleNames.Length; rix++)
                            {
                                var roleId = arrRoleNames[rix].Trim();
                                UserManager.AddToRole(userid, roleId);
                            }
                        }

                        viewModel.ReturnStatus = true;
                        viewModel.ReturnMessage.Add("Inserted Successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                if (viewModel == null)
                {
                    viewModel = new UserRoleAssignViewModel();
                }

                viewModel.ReturnStatus = false;
                viewModel.ReturnMessage.Add(ex.Message);

                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Route("Delete")]
        [HttpPost]
        [AjaxHasCredential(Function = FunctionConstants.USER_ROLE, Action = LevelPermissionsConstants.DELETE)]
        public ActionResult Delete(UserRoleAssignViewModel viewModel)
        {
            try
            {
                var arrUserIds = viewModel.UserIds.Split(';');
                var arrRoleNames = viewModel.RoleNames.Split(';');

                for (int uix = 0; uix < arrUserIds.Length; uix++)
                {
                    var userid = arrUserIds[uix];

                    // Clear user-roles for assign:
                    for (int i = 0; i < arrRoleNames.Length; i++)
                    {
                        UserManager.RemoveFromRole(userid, arrRoleNames[i]);
                    }
                }

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

        [Route("GetById")]
        [AjaxHasCredential(Function = FunctionConstants.USER_ROLE, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetById(string Id)
        {
            var user = _userRoleBusinessService.GetById(Id);

            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}