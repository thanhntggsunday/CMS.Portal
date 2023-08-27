using Common.Constants;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.DTO.DTO;
using Common.Model;
using Common.Model.Entities;
using Common.ViewModel.Common;
using eLearning.BL.Interfaces;
using eLearning.Model;

namespace Portal.Controllers
{
    public class HomeController : Controller
    {
        
        private IAboutBusinessService _aboutBusinessService;

        public HomeController(IAboutBusinessService aboutBusinessService)
        {
            _aboutBusinessService = aboutBusinessService;
        }

        public ActionResult Index()
        {
            // return View();
            return Redirect("/ElcHome");
        }

        
        public ActionResult GetAboutData()
        {
            ApiResult<AboutDto> viewModel = new ApiResult<AboutDto>();
            try
            {
                viewModel = _aboutBusinessService.GetFirstOrDefault();
            }
            catch (Exception ex)
            {
                viewModel.ReturnStatus = false;
                viewModel.ReturnMessage.Add(ex.Message);
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckTimeOut()
        {
            try
            {
                if (Session == null)
                {
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                    return Json(true, JsonRequestBehavior.AllowGet);
                }

                var user = Session[SessionConstants.USER_SESSION] as AppUser;

                if (user == null)
                {
                    // Redirect("/Account/Login");
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }


            return Json(false, JsonRequestBehavior.AllowGet);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult About()
        {
            var vm = _aboutBusinessService.GetFirstOrDefault();
            return View(vm);
        }

        public ActionResult Contact()
        {
            var vm = _aboutBusinessService.GetFirstOrDefault();
            return View(vm);
        }

      

        [ChildActionOnly]
        // [OutputCache(Duration = 3600 * 24)]
        public ActionResult Footer()
        {

            using (var context = new CommonDbContext())
            {
                ViewBag.Footer = context.Footers.FirstOrDefault();
            }
            return PartialView();
        }


    }
}