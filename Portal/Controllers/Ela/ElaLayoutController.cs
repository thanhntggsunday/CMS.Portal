using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.DTO.DTO;
using Common.Model;
using Common.ViewModel.Common;
using eLearning.Model;

namespace Portal.Controllers.Ela
{
    public class ElaLayoutController : AdmBaseController
    {
        // GET: eLearningAdmin/Layout
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetFooterFirstOrDefault()
        {
            var viewModel = new ApiResult<FooterDto>();
            var dto = new FooterDto();

            using (var context = new CommonDbContext())
            {
                var item = context.Footers.FirstOrDefault();

                if (item != null)
                {
                    dto.ID = item.ID;
                    dto.Content = item.Content;
                    dto.Status = item.Status;
                }

                viewModel.ResultObj = dto;
                viewModel.ReturnStatus = true;
                viewModel.ReturnMessage.Add("Get Item Successfully!");
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateFooter(FooterDto dto)
        {
            var viewModel = new ApiResult<FooterDto>();

            try
            {
                using (var context = new CommonDbContext())
                {
                    var item = context.Footers.Find(dto.ID);

                    if (item != null)
                    {
                        item.Content = dto.Content;
                        item.Status = dto.Status;
                    }

                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChanges();

                    viewModel.ResultObj = dto;
                    viewModel.ReturnStatus = true;
                    viewModel.ReturnMessage.Add("Get Item Successfully!");
                }
            }
            catch (Exception ex)
            {
                viewModel.ReturnStatus = false;
                viewModel.ReturnMessage.Add(ex.Message);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
    }
}