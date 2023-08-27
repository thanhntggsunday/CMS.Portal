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
    public class ElaCourseLessonController : Controller
    {
        private ICourseLessonBusinessService _courseLessonBusinessService;

        public ElaCourseLessonController(ICourseLessonBusinessService courseLessonBusinessService)
        {
            _courseLessonBusinessService = courseLessonBusinessService;
        }

        // GET: Admin/CoursLesson

        [Route("Index")]
        [HasCredential(Function = FunctionConstants.COURSE_LESSON, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult Index()
        {
            return View();
        }

        [Route("GetAllPaging")]
        [HttpGet]
        [AjaxHasCredential(Function = FunctionConstants.COURSE_LESSON, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetAllPaging()
        {
            var result = new DataTableViewModel<CourseLessonDto>();
            try
            {
                var draw = Request.Params.GetValues("draw").FirstOrDefault();
                var start = Request.Params.GetValues("start").FirstOrDefault();
                var length = Request.Params.GetValues("length").FirstOrDefault();
                var search = Request.Params.GetValues("search[value]").FirstOrDefault();
                var strCourseID = Request.Params.GetValues("courseID").FirstOrDefault();

                int courseID = strCourseID != null ? Convert.ToInt32(strCourseID) : 0;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int startIndex = start != null ? Convert.ToInt32(start) : 0;
                int intDraw = draw != null ? Convert.ToInt32(draw) : 0;
                int endIndex = startIndex + pageSize - 1;

                result = _courseLessonBusinessService.GetAllCouresLessonByCourseIDPaging(courseID, startIndex, endIndex, pageSize, intDraw, search);

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
        [AjaxHasCredential(Function = FunctionConstants.COURSE_LESSON, Action = LevelPermissionsConstants.CREATE)]
        public ActionResult Create(CourseLessonDto dto)
        {
            ApiResult<CourseLessonDto> viewModel = new ApiResult<CourseLessonDto>();
            viewModel.ResultObj = dto;
            viewModel.ResultObj.Status = true;
            viewModel.ResultObj.CreatorId = User.Identity.GetUserId();
            viewModel.ResultObj.CreatedBy = User.Identity.GetUserName();
            viewModel.ResultObj.CreatedDate = DateTime.Now;

            try
            {
                _courseLessonBusinessService.Create(viewModel);
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
        [AjaxHasCredential(Function = FunctionConstants.COURSE_LESSON, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetById(CourseLessonDto dto)
        {
            ApiResult<CourseLessonDto> viewModel = new ApiResult<CourseLessonDto>();
            try
            {
                viewModel = _courseLessonBusinessService.GetById(dto);
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
        [AjaxHasCredential(Function = FunctionConstants.COURSE_LESSON, Action = LevelPermissionsConstants.EDIT)]
        public ActionResult Update(CourseLessonDto dto)
        {
            ApiResult<CourseLessonDto> viewModel = new ApiResult<CourseLessonDto>();
            viewModel.ResultObj = dto;

            try
            {
                _courseLessonBusinessService.Update(viewModel);
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
        [AjaxHasCredential(Function = FunctionConstants.COURSE_LESSON, Action = LevelPermissionsConstants.DELETE)]
        public ActionResult Delete(CourseLessonDto dto)
        {
            ApiResult<CourseLessonDto> viewModel = new ApiResult<CourseLessonDto>();
            viewModel.ResultObj = dto;

            try
            {
                _courseLessonBusinessService.Delete(viewModel);
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