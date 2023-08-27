using Common.Constants;
using Common.Model.Entities;
using Common.ViewModel.Common;
using eLearning.BL.Interfaces;
using Portal.ServerProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eLearning.Common.Dto;

namespace Portal.Controllers.Ela
{
    public class ElaCourseCategoryController : AdmBaseController
    {
        private ICourseCategoryBusinessService _courseCategoryBusinessService;

        public ElaCourseCategoryController(ICourseCategoryBusinessService courseCategoryBusinessService)
        {
            _courseCategoryBusinessService = courseCategoryBusinessService;
        }

        // GET: Admin/CourseCategory
        [Route("Index")]
        [HasCredential(Function = FunctionConstants.COURSE_CATEGORY, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult Index()
        {
            return View();
        }

        [Route("GetAllPaging")]
        [HttpGet]
        [AjaxHasCredential(Function = FunctionConstants.COURSE_CATEGORY, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetAllPaging()
        {
            var result = new DataTableViewModel<CourseCategoryDto>();
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

                result = _courseCategoryBusinessService.GetAllPaging(startIndex, endIndex, pageSize, intDraw, search);

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
        [AjaxHasCredential(Function = FunctionConstants.COURSE_CATEGORY, Action = LevelPermissionsConstants.CREATE)]
        public ActionResult Create(CourseCategoryDto dto)
        {
            ApiResult<CourseCategoryDto> viewModel = new ApiResult<CourseCategoryDto>();
            viewModel.ResultObj = dto;
            viewModel.ResultObj.Status = true;

            try
            {
                _courseCategoryBusinessService.Create(viewModel);
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
        [AjaxHasCredential(Function = FunctionConstants.COURSE_CATEGORY, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetById(CourseCategoryDto dto)
        {
            ApiResult<CourseCategoryDto> viewModel = new ApiResult<CourseCategoryDto>();
            try
            {
                viewModel = _courseCategoryBusinessService.GetById(dto);
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
        [AjaxHasCredential(Function = FunctionConstants.COURSE_CATEGORY, Action = LevelPermissionsConstants.EDIT)]
        public ActionResult Update(CourseCategoryDto dto)
        {
            ApiResult<CourseCategoryDto> viewModel = new ApiResult<CourseCategoryDto>();
            viewModel.ResultObj = dto;

            try
            {
                _courseCategoryBusinessService.Update(viewModel);
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
        [AjaxHasCredential(Function = FunctionConstants.COURSE_CATEGORY, Action = LevelPermissionsConstants.DELETE)]
        public ActionResult Delete(CourseCategoryDto dto)
        {
            ApiResult<CourseCategoryDto> viewModel = new ApiResult<CourseCategoryDto>();
            viewModel.ResultObj = dto;

            try
            {
                _courseCategoryBusinessService.Delete(viewModel);
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
        [AjaxHasCredential(Function = FunctionConstants.COURSE_CATEGORY, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetAll()
        {
            var result = new DataTableViewModel<CourseCategoryDto>();
            try
            {
                result = _courseCategoryBusinessService.GetAll();

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