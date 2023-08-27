using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.BL.Interfaces;
using Common.Constants;
using Common.Dto;
using Common.ViewModel.Common;
using Common.ViewModel.System;
using Portal.ServerProvider;

namespace Portal.Controllers.Cma
{
    public class CmaRoleController : AdmBaseController
    {
        private IRoleBusinessService _roleBusinessService;

        public CmaRoleController(IRoleBusinessService roleBusinessService)
        {
            _roleBusinessService = roleBusinessService;
        }

        [Route("Index")]
        [HasCredential(Function = FunctionConstants.ROLE, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult Index()
        {
            return View();
        }

        [Route("GetAllPaging")]
        [HttpGet]
        [AjaxHasCredential(Function = FunctionConstants.ROLE, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetAllPaging()
        {
            var result = new DataTableViewModel<AppRoleDto>();
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

                result = _roleBusinessService.GetAllPaging(startIndex, endIndex, pageSize, intDraw, search);

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
        [AjaxHasCredential(Function = FunctionConstants.ROLE, Action = LevelPermissionsConstants.CREATE)]
        public ActionResult Create(RoleViewModel viewModel)
        {
            try
            {
                _roleBusinessService.Create(viewModel);
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
        [AjaxHasCredential(Function = FunctionConstants.ROLE, Action = LevelPermissionsConstants.EDIT)]
        public ActionResult Update(RoleViewModel viewModel)
        {
            try
            {
                _roleBusinessService.Update(viewModel);
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
        [AjaxHasCredential(Function = FunctionConstants.ROLE, Action = LevelPermissionsConstants.DELETE)]
        public ActionResult Delete(RoleViewModel roleViewModel)
        {
            try
            {
                _roleBusinessService.Delete(roleViewModel);
            }
            catch (Exception ex)
            {
                roleViewModel.ReturnStatus = false;
                roleViewModel.ReturnMessage.Add(ex.Message);
                return Json(roleViewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(roleViewModel, JsonRequestBehavior.AllowGet);
        }

        [Route("GetById")]
        [HttpGet]
        [AjaxHasCredential(Function = FunctionConstants.ROLE, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetById(RoleViewModel viewModel)
        {
            try
            {
                _roleBusinessService.GetById(viewModel);
            }
            catch (Exception ex)
            {
                viewModel.ReturnStatus = false;
                viewModel.ReturnMessage.Add(ex.Message);
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
    }
}