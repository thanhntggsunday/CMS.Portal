using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Constants;
using Common.DTO.DTO;
using Common.ViewModel.Common;
using eLearning.BL.Interfaces;
using Portal.Controllers.Cma;
using Portal.ServerProvider;

namespace Portal.Controllers.Ela
{
    public class ElaHomeController : CmaHomeController
    {
        private IAboutBusinessService _aboutBusinessService;

        public ElaHomeController(IAboutBusinessService aboutBusinessService)
        {
            _aboutBusinessService = aboutBusinessService;
        }

       
        public ActionResult About()
        {
            return View();
        }

        //public ActionResult Contact()
        //{
        //    return View();
        //}

        [HttpPost]
        [HasCredential(Function = FunctionConstants.HOME, Action = LevelPermissionsConstants.EDIT)]
        public ActionResult Update(AboutDto dto)
        {
            ApiResult<AboutDto> viewModel = new ApiResult<AboutDto>();
            viewModel.ResultObj = dto;

            try
            {
                _aboutBusinessService.Update(viewModel);
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