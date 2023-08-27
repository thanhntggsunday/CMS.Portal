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
    public class ElaOrderController : Controller
    {
        private IOrderBusinessService _orderBusinessService;

        public ElaOrderController(IOrderBusinessService orderBusinessService)
        {
            _orderBusinessService = orderBusinessService;
        }

        // GET: Admin/Order
        [Route("Index")]
        [HasCredential(Function = FunctionConstants.ORDER, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult Index()
        {
            return View();
        }

        [Route("GetAllPaging")]
        [HttpGet]
        [AjaxHasCredential(Function = FunctionConstants.ORDER, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetAllPaging()
        {
            var result = new DataTableViewModel<OrderDto>();
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

                result = _orderBusinessService.GetAllPaging(startIndex, endIndex, pageSize, intDraw, search);

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
        [AjaxHasCredential(Function = FunctionConstants.ORDER, Action = LevelPermissionsConstants.CREATE)]
        public ActionResult Create(OrderDto dto)
        {
            ApiResult<OrderDto> viewModel = new ApiResult<OrderDto>();
            viewModel.ResultObj = dto;
            viewModel.ResultObj.Status = true;

            try
            {
                _orderBusinessService.Create(viewModel);
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
        [AjaxHasCredential(Function = FunctionConstants.ORDER, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetById(OrderDto dto)
        {
            ApiResult<OrderDto> viewModel = new ApiResult<OrderDto>();
            try
            {
                viewModel = _orderBusinessService.GetById(dto);
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
        [AjaxHasCredential(Function = FunctionConstants.ORDER, Action = LevelPermissionsConstants.EDIT)]
        public ActionResult Update(OrderDto dto)
        {
            ApiResult<OrderDto> viewModel = new ApiResult<OrderDto>();
            viewModel.ResultObj = dto;

            try
            {
                _orderBusinessService.Update(viewModel);
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
        [AjaxHasCredential(Function = FunctionConstants.ORDER, Action = LevelPermissionsConstants.DELETE)]
        public ActionResult Delete(OrderDto dto)
        {
            ApiResult<OrderDto> viewModel = new ApiResult<OrderDto>();
            viewModel.ResultObj = dto;

            try
            {
                _orderBusinessService.Delete(viewModel);
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