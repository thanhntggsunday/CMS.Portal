using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Constants;
using Common.ViewModel.Common;
using eLearning.BL.Interfaces;
using eLearning.Common.Classes;
using eLearning.Common.Dto;
using Microsoft.AspNet.Identity;
using Portal.ServerProvider;

namespace Portal.Controllers.Ela
{
    public class ElaTrainnerController : Controller
    {
        private ITrainnerBusinessService _trainnerBusinessService;

        public ElaTrainnerController(ITrainnerBusinessService trainnerBusinessService)
        {
            _trainnerBusinessService = trainnerBusinessService;
        }

        // GET: Admin/Course
        [Route("Index")]
        [HasCredential(Function = eLearningFunctionConstants.TRAINNER, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult Index()
        {
            return View();
        }

        [Route("GetAllPaging")]
        [HttpGet]
        [AjaxHasCredential(Function = eLearningFunctionConstants.TRAINNER, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetAllPaging()
        {
            var result = new DataTableViewModel<TrainnerDto>();
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

                result = _trainnerBusinessService.GetAllPaging(startIndex, endIndex, pageSize, intDraw, search);

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
        [AjaxHasCredential(Function = eLearningFunctionConstants.TRAINNER, Action = LevelPermissionsConstants.CREATE)]
        public ActionResult Create(TrainnerDto dto)
        {
            ApiResult<TrainnerDto> viewModel = new ApiResult<TrainnerDto>();
            viewModel.ResultObj = dto;
            viewModel.ResultObj.Status = true;

            viewModel.ResultObj.CreatorId = User.Identity.GetUserId();

            try
            {
                _trainnerBusinessService.Create(viewModel);
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
        [AjaxHasCredential(Function = eLearningFunctionConstants.TRAINNER, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetById(TrainnerDto dto)
        {
            ApiResult<TrainnerDto> viewModel = new ApiResult<TrainnerDto>();
            try
            {
                viewModel = _trainnerBusinessService.GetById(dto);
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
        [AjaxHasCredential(Function = eLearningFunctionConstants.TRAINNER, Action = LevelPermissionsConstants.EDIT)]
        public ActionResult Update(TrainnerDto dto)
        {
            ApiResult<TrainnerDto> viewModel = new ApiResult<TrainnerDto>();
            viewModel.ResultObj = dto;

            try
            {
                _trainnerBusinessService.Update(viewModel);
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
        [AjaxHasCredential(Function = eLearningFunctionConstants.TRAINNER, Action = LevelPermissionsConstants.DELETE)]
        public ActionResult Delete(TrainnerDto dto)
        {
            ApiResult<TrainnerDto> viewModel = new ApiResult<TrainnerDto>();
            viewModel.ResultObj = dto;

            try
            {
                _trainnerBusinessService.Delete(viewModel);
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