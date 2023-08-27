using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.DTO.DTO;
using Common.Model;
using Common.ViewModel.Common;
using eLearning.BL.Interfaces;
using eLearning.Common.Classes;
using eLearning.Common.Dto;
using eLearning.Model;
using Portal.Utils;

namespace Portal.Controllers.Elc
{
    public class ElcHomeController : Controller
    {
        private IContentCategoryBusinessService _categoryBusinessService;
        private IAboutBusinessService _aboutBusinessService;
        private IContentBusinessService _contentBusinessService;
        private ICourseCategoryBusinessService _courseCategoryBusinessService;

        public ElcHomeController(
            IAboutBusinessService aboutBusinessService,
            IContentBusinessService contentBusinessService,
            IContentCategoryBusinessService categoryBusinessService,
            ICourseCategoryBusinessService courseCategoryBusinessService)
        {
            _aboutBusinessService = aboutBusinessService;
            _contentBusinessService = contentBusinessService;
            _categoryBusinessService = categoryBusinessService;
            _courseCategoryBusinessService = courseCategoryBusinessService;
        }

        // GET: eLearningClient/Home
        public ActionResult Index()
        {
            //
            var domain = Request.GetFullDomain();

            using (var context = new CommonDbContext())
            {
                ViewBag.Slides = context.Slides.ToArray();
            }

            ViewBag.NewPosts = GetByNewPosts();

            return View();
        }



        [ChildActionOnly]
        public PartialViewResult PostCategory()
        {
            var result = new DataTableViewModel<ContentCategoryDto>();

            try
            {
                result = _categoryBusinessService.GetAll();
                result.returnStatus = true;
            }
            catch (Exception ex)
            {
                result.returnStatus = false;
                result.returnMessage.Add(ex.Message);
            }

            return PartialView(result);
        }


        [ChildActionOnly]
        public PartialViewResult CourseCategory()
        {
            var result = new DataTableViewModel<CourseCategoryDto>();

            try
            {
                result = _courseCategoryBusinessService.GetAll();
                result.returnStatus = true;
            }
            catch (Exception ex)
            {
                result.returnStatus = false;
                result.returnMessage.Add(ex.Message);
            }

            return PartialView(result);
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

        public ActionResult Calendar()
        {
            var vm = _aboutBusinessService.GetFirstOrDefault();
            return View(vm);
        }

        [ChildActionOnly]
        //  [OutputCache(Duration = 3600 * 24)]
        public ActionResult Footer()
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

            return PartialView(viewModel);
        }

        private DataTableViewModel<ContentDto> GetByNewPosts()
        {
            DataTableViewModel<ContentDto> viewModel = new DataTableViewModel<ContentDto>();
            try
            {
                viewModel = _contentBusinessService.GetTopTen();
            }
            catch (Exception ex)
            {
                viewModel.returnStatus = false;
                viewModel.returnMessage.Add(ex.Message);
                return viewModel;
            }

            return viewModel;
        }

        [ChildActionOnly]
        //  [OutputCache(Duration = 3600 * 24)]
        public ActionResult GetMainSlides()
        {
            var viewModel = new DataTableViewModel<SlideDto>();
            var data = new List<SlideDto>();

            try
            {
                using (var context = new CommonDbContext())
                {
                    var items = context.Slides.OrderBy(o => o.DisplayOrder).ToList();

                    foreach (var item in items)
                    {
                        var dto = new SlideDto();

                        dto.CopyFromModel(item);
                        data.Add(dto);
                    }
                }

                viewModel.data = data.ToArray();
                viewModel.returnStatus = true;
                viewModel.returnMessage.Add("OK");

            }
            catch (Exception ex)
            {
                viewModel.returnStatus = false;
                viewModel.returnMessage.Add(ex.ToString());
            }



            return PartialView(viewModel);
        }
    }
}