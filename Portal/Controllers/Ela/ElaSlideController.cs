using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Constants;
using Common.Model;
using Common.Model.Entities;
using Common.ViewModel.Common;
using eLearning.Common.Classes;
using eLearning.Common.Dto;
using eLearning.Model;
using eLearning.Model.Entities;
using Portal.ServerProvider;

namespace Portal.Controllers.Ela
{
    public class ElaSlideController : AdmBaseController
    {
        // GET: eLearningAdmin/Slide
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetAllPaging()
        {
            var viewModel = new DataTableViewModel<SlideDto>();
            var data = new List<SlideDto>();

            try
            {
                var draw = Request.Params.GetValues("draw").FirstOrDefault();
                var start = Request.Params.GetValues("start").FirstOrDefault();
                var length = Request.Params.GetValues("length").FirstOrDefault();
                var search = Request.Params.GetValues("search[value]").FirstOrDefault();

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int startIndex = start != null ? Convert.ToInt32(start) : 0;

                using (var context = new CommonDbContext())
                {
                    var items = context.Slides.OrderBy(o => o.DisplayOrder).Skip(startIndex).Take(pageSize).ToList();

                    foreach (var item in items)
                    {
                        var dto = new SlideDto();

                        dto.CopyFromModel(item);
                        data.Add(dto);
                    }

                    viewModel.recordsTotal = context.Slides.Count();
                    viewModel.recordsFiltered = context.Slides.Count();
                }

                viewModel.data = data.ToArray();
                viewModel.returnStatus = true;
                viewModel.returnMessage.Add("OK");

                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                viewModel.returnStatus = false;
                viewModel.returnMessage.Add(ex.Message);

                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Create(SlideDto dto)
        {
            ApiResult<SlideDto> viewModel = new ApiResult<SlideDto>();
            viewModel.ResultObj = dto;
            viewModel.ResultObj.Status = true;

            if (String.IsNullOrEmpty(dto.Image))
            {
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            try
            {
                using (var context = new CommonDbContext())
                {
                    var item = new Slide();

                    item.CopyFromDto(dto);
                    context.Slides.Add(item);
                    context.SaveChanges();
                }

                viewModel.ReturnStatus = true;
                viewModel.ReturnMessage.Add("OK");
            }
            catch (Exception ex)
            {
                viewModel.ReturnStatus = false;
                viewModel.ReturnMessage.Add(ex.Message);

                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [AjaxHasCredential(Function = FunctionConstants.POST, Action = LevelPermissionsConstants.VIEW)]
        public ActionResult GetById(SlideDto dto)
        {
            ApiResult<SlideDto> viewModel = new ApiResult<SlideDto>();

            try
            {
                using (var context = new CommonDbContext())
                {
                    var item = context.Slides.Find(dto.ID);

                    viewModel.ResultObj = new SlideDto();
                    viewModel.ResultObj.CopyFromModel(item);
                    viewModel.ReturnStatus = true;
                    viewModel.ReturnMessage.Add("OK");
                }
            }
            catch (Exception ex)
            {
                viewModel.ReturnStatus = false;
                viewModel.ReturnMessage.Add(ex.Message);
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [AjaxHasCredential(Function = FunctionConstants.POST, Action = LevelPermissionsConstants.EDIT)]
        public ActionResult Update(SlideDto dto)
        {
            ApiResult<SlideDto> viewModel = new ApiResult<SlideDto>();
            viewModel.ResultObj = dto;

            try
            {
                using (var context = new CommonDbContext())
                {
                    var item = context.Slides.Find(dto.ID);

                    item.CopyFromDto(dto);

                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChanges();

                    viewModel.ResultObj = dto;
                    viewModel.ReturnStatus = true;
                    viewModel.ReturnMessage.Add("OK");
                }
            }
            catch (Exception ex)
            {
                viewModel.ReturnStatus = false;
                viewModel.ReturnMessage.Add(ex.Message);
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [AjaxHasCredential(Function = FunctionConstants.POST, Action = LevelPermissionsConstants.DELETE)]
        public ActionResult Delete(SlideDto dto)
        {
            ApiResult<SlideDto> viewModel = new ApiResult<SlideDto>();
            viewModel.ResultObj = dto;

            try
            {
                using (var context = new CommonDbContext())
                {
                    var item = context.Slides.Find(dto.ID);

                    context.Slides.Remove(item);
                    context.SaveChanges();

                    viewModel.ResultObj = dto;
                    viewModel.ReturnStatus = true;
                    viewModel.ReturnMessage.Add("OK");
                }
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