using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.BL.Interfaces;
using Common.Constants;
using Common.Dto;
using Common.Model.Entities;
using Common.ViewModel;
using Common.ViewModel.Common;
using Common.ViewModel.System.Permission;
using Portal.ServerProvider;

namespace Portal.Controllers.Cma
{
    public class CmaPermissionController : AdmBaseController
    {
        private IRoleBusinessService _roleBusinessService;
        private IFunctionBusinessService _functionBusinessService;
        private ILevelPermissionBusinessService _levelPermissionBusinessService;
        private IPermissionBusinessService _permissionBusinessService;

        public CmaPermissionController(IFunctionBusinessService functionBusinessService,
            ILevelPermissionBusinessService levelPermissionBusinessService,
            IPermissionBusinessService permissionBusinessService,
            IRoleBusinessService roleBusinessService)
        {
            _functionBusinessService = functionBusinessService;
            _levelPermissionBusinessService = levelPermissionBusinessService;
            _permissionBusinessService = permissionBusinessService;
            _roleBusinessService = roleBusinessService;
        }

        [Route("Index")]
        [HasCredential(Function = FunctionConstants.PERMISSION, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult Index()
        {
            return View();
        }

        [Route("GetJsonPermissionAllPaging")]
        [AjaxHasCredential(Function = FunctionConstants.PERMISSION, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetJsonPermissionAllPaging()
        {
            var viewModel = new DataTableViewModel<AppPermissionDto>();
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

                viewModel = _permissionBusinessService.GetAllPaging(startIndex, endIndex, search);
            }
            catch (Exception ex)
            {
                viewModel.returnStatus = false;
                viewModel.returnMessage.Add(ex.Message);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Route("GetRoleAll")]
        [AjaxHasCredential(Function = FunctionConstants.PERMISSION, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetRoleAll()
        {
            var responseData = new DataTableViewModel<AppRoleDto>();

            try
            {
                responseData = _roleBusinessService.GetAll();

                responseData.returnStatus = true;
                responseData.returnMessage.Add("Success.");
            }
            catch (Exception ex)
            {
                responseData.returnStatus = false;
                responseData.returnMessage.Add(ex.Message);
            }

            return Json(responseData, JsonRequestBehavior.AllowGet);
        }

        [Route("GetFunctionAll")]
        [AjaxHasCredential(Function = FunctionConstants.PERMISSION, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetFunctionAll()
        {
            var viewModel = new DataTableViewModel<AppFunctionDto>();
            try
            {
                viewModel = _functionBusinessService.GetAll();
            }
            catch (Exception ex)
            {
                viewModel.returnStatus = false;
                viewModel.returnMessage.Add(ex.Message);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Route("GetLevePermissionAll")]
        [AjaxHasCredential(Function = FunctionConstants.PERMISSION, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetLevePermissionAll()
        {
            var permissions = new DataTableViewModel<AppLevelPermissionDto>();
            try
            {
                permissions = _levelPermissionBusinessService.GetAll();
            }
            catch (Exception ex)
            {
                permissions.returnStatus = false;
                permissions.returnMessage.Add(ex.Message);
            }

            return Json(permissions, JsonRequestBehavior.AllowGet);
        }

        [Route("AssignPermissionToRole")]
        [AjaxHasCredential(Function = FunctionConstants.PERMISSION, Action = LevelPermissionsConstants.ASSIGN_PERMISSION_TO_ROLE)]
        [HttpPost]
        public ActionResult AssignPermissionToRole(PermissionViewModel viewModel)
        {
            try
            {
                if (viewModel != null
                && !string.IsNullOrEmpty(viewModel.StrArrayFunctionActionId)
                && !string.IsNullOrEmpty(viewModel.StrArryRolesId))
                {
                    var functionActionId = viewModel.FunctionActionId;
                    var arrRoleId = viewModel.StrArryRolesId.Split(';');
                    var arrEmpty = new string[0];
                    var arrFunctionActionId = viewModel.StrArrayFunctionActionId != null ? viewModel.StrArrayFunctionActionId.Split(';') : arrEmpty;
                    var arrFunctionActionIdSelected = viewModel.StrArrayFunctionActionIdSelected != null ? viewModel.StrArrayFunctionActionIdSelected.Split(';') : arrEmpty;
                    List<Role_Permission> permissions = new List<Role_Permission>();

                    if (arrFunctionActionIdSelected.Length > 0)
                    {
                        for (int i = 0; i < arrRoleId.Length; i++)
                        {
                            for (int j = 0; j < arrFunctionActionIdSelected.Length; j++)
                            {
                                Role_Permission permission = new Role_Permission();

                                permission.Function_ActionID = Int32.Parse(arrFunctionActionIdSelected[j]);
                                permission.AppRoleId = arrRoleId[i];

                                permissions.Add(permission);
                            }
                        }
                    }

                    _permissionBusinessService.BulkInsert(permissions, arrRoleId, arrFunctionActionId, out TransactionalInformation transactional);

                    viewModel.ReturnStatus = transactional.ReturnStatus;
                    viewModel.ReturnMessage = transactional.ReturnMessage;
                }
                else
                {
                    if (viewModel == null)
                    {
                        viewModel = new PermissionViewModel();
                    }

                    viewModel.ReturnStatus = false;
                    viewModel.ReturnMessage.Add("Error ViewModel");
                }
            }
            catch (Exception ex)
            {
                viewModel = new PermissionViewModel();
                viewModel.ReturnStatus = false;
                viewModel.ReturnMessage.Add(ex.Message);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Route("GetActionsByFunctionId")]
        [AjaxHasCredential(Function = FunctionConstants.PERMISSION, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetActionsByFunctionId(string functionId)
        {
            var permissions = new DataTableViewModel<AppLevelPermissionDto>();

            try
            {
                permissions = _levelPermissionBusinessService.GetAllLevelPermissionByFunctionId(functionId);
            }
            catch (Exception ex)
            {
                permissions.returnStatus = false;
                permissions.returnMessage.Add(ex.Message);
            }

            return Json(permissions, JsonRequestBehavior.AllowGet);
        }
    }
}