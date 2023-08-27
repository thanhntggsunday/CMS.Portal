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
    public class ElaContentController : AdmBaseController
    {
        public readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

        private IContentBusinessService _contentBusinessService;

        public ElaContentController(IContentBusinessService contentBusinessService) 
        {
            _contentBusinessService = contentBusinessService;
        }

        // GET: eLearningAdmin/Content
        public ActionResult Index()
        {
            return View();
        }

        [Route("GetAllPaging")]
        [HttpGet]
        [AjaxHasCredential(Function = FunctionConstants.POST, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetAllPaging()
        {
            var result = new DataTableViewModel<ContentDto>();
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

                result = _contentBusinessService.GetAllPaging(startIndex, endIndex, intDraw, search);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                result.returnStatus = false;
                result.returnMessage.Add(ex.Message);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("Create")]
        [HttpPost]
        [AjaxHasCredential(Function = FunctionConstants.POST, Action = LevelPermissionsConstants.CREATE)]
        public ActionResult Create(ContentDto dto)
        {

            ApiResult<ContentDto> viewModel = new ApiResult<ContentDto>();
            viewModel.ResultObj = dto;
            viewModel.ResultObj.Status = true;

            try
            {
                _contentBusinessService.Create(viewModel);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                viewModel.ReturnStatus = false;
                viewModel.ReturnMessage.Add(ex.Message);

                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Route("GetById")]
        [HttpGet]
        [AjaxHasCredential(Function = FunctionConstants.POST, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetById(ContentDto dto)
        {
            ApiResult<ContentDto> viewModel = new ApiResult<ContentDto>();
            try
            {
                viewModel = _contentBusinessService.GetById(dto);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                viewModel.ReturnStatus = false;
                viewModel.ReturnMessage.Add(ex.Message);
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Route("Update")]
        [HttpPost]
        [AjaxHasCredential(Function = FunctionConstants.POST, Action = LevelPermissionsConstants.EDIT)]
        public ActionResult Update(ContentDto dto)
        {
            ApiResult<ContentDto> viewModel = new ApiResult<ContentDto>();
            viewModel.ResultObj = dto;

            try
            {
                _contentBusinessService.Update(viewModel);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                viewModel.ReturnStatus = false;
                viewModel.ReturnMessage.Add(ex.Message);
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Route("Delete")]
        [HttpGet]
        [AjaxHasCredential(Function = FunctionConstants.POST, Action = LevelPermissionsConstants.DELETE)]
        public ActionResult Delete(ContentDto dto)
        {
            ApiResult<ContentDto> viewModel = new ApiResult<ContentDto>();
            viewModel.ResultObj = dto;

            try
            {
                _contentBusinessService.Delete(viewModel);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                viewModel.ReturnStatus = false;
                viewModel.ReturnMessage.Add(ex.Message);
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [Route("GetAll")]
        [HttpGet]
        [AjaxHasCredential(Function = FunctionConstants.POST, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetAll()
        {
            var result = new DataTableViewModel<ContentDto>();
            try
            {
                result = _contentBusinessService.GetAll();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                result.returnStatus = false;
                result.returnMessage.Add(ex.Message);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}