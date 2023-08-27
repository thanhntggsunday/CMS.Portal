using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Constants;
using Common.ViewModel.Common;
using eLearning.BL.Interfaces;
using eLearning.Common.Dto;
using Portal.ServerProvider;

namespace Portal.Controllers.Ela
{
    public class ElaContentCategoryController : AdmBaseController
    {
        private IContentCategoryBusinessService _categoryBusinessService;

        public ElaContentCategoryController(IContentCategoryBusinessService categoryBusinessService)
        {
            _categoryBusinessService = categoryBusinessService;
        }

        // GET: Admin/CourseCategory
        [Route("Index")]
        [HasCredential(Function = FunctionConstants.POST_CATEGORY, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult Index()
        {
            return View();
        }

        [Route("GetAllPaging")]
        [HttpGet]
        [AjaxHasCredential(Function = FunctionConstants.POST_CATEGORY, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetAllPaging()
        {
            var result = new DataTableViewModel<ContentCategoryDto>();
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

                result = _categoryBusinessService.GetAllPaging(startIndex, endIndex, pageSize, intDraw, search);

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
        [AjaxHasCredential(Function = FunctionConstants.POST_CATEGORY, Action = LevelPermissionsConstants.CREATE)]
        public ActionResult Create(ContentCategoryDto dto)
        {
            ApiResult<ContentCategoryDto> viewModel = new ApiResult<ContentCategoryDto>();
            viewModel.ResultObj = dto;
            viewModel.ResultObj.Status = true;

            try
            {
                _categoryBusinessService.Create(viewModel);
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
        [AjaxHasCredential(Function = FunctionConstants.POST_CATEGORY, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetById(ContentCategoryDto dto)
        {
            ApiResult<ContentCategoryDto> viewModel = new ApiResult<ContentCategoryDto>();
            try
            {
                viewModel = _categoryBusinessService.GetById(dto);
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
        [AjaxHasCredential(Function = FunctionConstants.POST_CATEGORY, Action = LevelPermissionsConstants.EDIT)]
        public ActionResult Update(ContentCategoryDto dto)
        {
            ApiResult<ContentCategoryDto> viewModel = new ApiResult<ContentCategoryDto>();
            viewModel.ResultObj = dto;

            try
            {
                _categoryBusinessService.Update(viewModel);
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
        [AjaxHasCredential(Function = FunctionConstants.POST_CATEGORY, Action = LevelPermissionsConstants.DELETE)]
        public ActionResult Delete(ContentCategoryDto dto)
        {
            ApiResult<ContentCategoryDto> viewModel = new ApiResult<ContentCategoryDto>();
            viewModel.ResultObj = dto;

            try
            {
                _categoryBusinessService.Delete(viewModel);
            }
            catch (Exception ex)
            {
                viewModel.ReturnStatus = false;
                viewModel.ReturnMessage.Add(ex.Message);
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Route("GetAll")]
        [HttpGet]
        [AjaxHasCredential(Function = FunctionConstants.POST_CATEGORY, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetAll()
        {
            var result = new DataTableViewModel<ContentCategoryDto>();
            try
            {
                result = _categoryBusinessService.GetAll();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result.returnStatus = false;
                result.returnMessage.Add(ex.Message);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}