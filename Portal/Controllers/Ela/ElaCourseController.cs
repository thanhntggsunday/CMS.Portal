using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Constants;
using Common.ViewModel.Common;
using eLearning.BL.Interfaces;
using eLearning.Common.Dto;
using Microsoft.AspNet.Identity;
using Portal.ServerProvider;

namespace Portal.Controllers.Ela
{
    public class ElaCourseController : AdmBaseController
    {
        private ICourseBusinessService _courseBusinessService;

        public ElaCourseController(ICourseBusinessService courseBusinessService)
        {
            _courseBusinessService = courseBusinessService;
        }

        // GET: Admin/Course
        [Route("Index")]
        [HasCredential(Function = FunctionConstants.COURSE, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult Index()
        {
            return View();
        }

        [Route("GetAllPaging")]
        [HttpGet]
        [AjaxHasCredential(Function = FunctionConstants.COURSE, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetAllPaging()
        {
            var result = new DataTableViewModel<CourseDto>();
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

                result = _courseBusinessService.GetAllPaging(startIndex, endIndex, intDraw, search);

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
        [AjaxHasCredential(Function = FunctionConstants.COURSE, Action = LevelPermissionsConstants.CREATE)]
        public ActionResult Create(CourseDto dto)
        {
            ApiResult<CourseDto> viewModel = new ApiResult<CourseDto>();
            viewModel.ResultObj = dto;
            viewModel.ResultObj.Status = true;

            viewModel.ResultObj.CreatorId = User.Identity.GetUserId();

            try
            {
                _courseBusinessService.Create(viewModel);
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
        [AjaxHasCredential(Function = FunctionConstants.COURSE, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetById(CourseDto dto)
        {
            ApiResult<CourseDto> viewModel = new ApiResult<CourseDto>();
            try
            {
                viewModel = _courseBusinessService.GetById(dto);
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
        [AjaxHasCredential(Function = FunctionConstants.COURSE, Action = LevelPermissionsConstants.EDIT)]
        public ActionResult Update(CourseDto dto)
        {
            ApiResult<CourseDto> viewModel = new ApiResult<CourseDto>();
            viewModel.ResultObj = dto;

            try
            {
                _courseBusinessService.Update(viewModel);
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
        [AjaxHasCredential(Function = FunctionConstants.COURSE, Action = LevelPermissionsConstants.DELETE)]
        public ActionResult Delete(CourseDto dto)
        {
            ApiResult<CourseDto> viewModel = new ApiResult<CourseDto>();
            viewModel.ResultObj = dto;

            try
            {
                _courseBusinessService.Delete(viewModel);
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
        [AjaxHasCredential(Function = FunctionConstants.COURSE, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetAll()
        {
            var result = new DataTableViewModel<CourseDto>();
            try
            {
                result = _courseBusinessService.GetAll();

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