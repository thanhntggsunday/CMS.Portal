using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Constants;
using Common.DTO.DTO;
using Common.ViewModel.Common;
using eLearning.Model;
using Portal.ServerProvider;

namespace Portal.Controllers.Cma
{
    public class CmaHomeController : AdmBaseController
    {
        // GET: Admin/Home
        // [HasCredential(Function = FunctionConstants.HOME, Action = "")]
        public ActionResult Index()
        {
            var viewModel = new ApiResult<AdmDashboardDto>();
            var dto = new AdmDashboardDto();

            using (var context = new ELearningDbContext())
            {
                dto.OrdersCount = context.Orders.Count();
                dto.UsersCount = context.AppUsers.Count();

                viewModel.ResultObj = dto;
                viewModel.ReturnStatus = true;
                viewModel.ReturnMessage.Add("Get Item Successfully!");
            }

            return View(viewModel);
        }
        
    }
}