using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.ViewModel.Common;
using eLearning.BL.Interfaces;
using eLearning.Common.Constants;
using eLearning.Common.Dto;
using eLearning.Model;
using PagedList;

namespace Portal.Controllers.Elc
{
    public class ElcCourseController : Controller
    {
        private ICourseBusinessService _courseBusinessService;

        public ElcCourseController(ICourseBusinessService courseBusinessService)
        {
            _courseBusinessService = courseBusinessService;
        }

        // GET: eLearningClient/Content
        public ActionResult Index(int? page)
        {
            var viewModel = new ListViewModel<CourseDto>();

            try
            {
                using (var context = new ELearningDbContext())
                {
                    int pageSize = 4;
                    int pageNumber = (page ?? 1);
                    
                    var td =
                        from o in context.Courses
                        join r in context.CourseCategories on o.CategoryId equals r.Id
                        select new CourseDto()
                        {
                            Id = o.Id,
                            CourseName = o.Name,
                            CatagoryName = r.Name,
                            Content = o.Content,
                            CreatedDate = o.CreatedDate,
                            CreatedBy = o.CreatedBy,
                            Description = o.Description,
                            ModifiedBy = o.ModifiedBy,
                            ModifiedDate = o.ModifiedDate,
                            Image = o.Image,
                            TrainerName = o.Trainners.Name,
                            Price = o.Price,
                            PromotionPrice = o.PromotionPrice,
                            Status = o.Status,
                            TrainerId = o.TrainerId
                        };

                    var data = td.OrderByDescending(o => o.CreatedDate).ToPagedList(pageNumber, pageSize);

                    viewModel.Data = data;
                    viewModel.ReturnStatus = true;
                    viewModel.ReturnMessage.Add("OK");

                    return View(viewModel);

                }
            }
            catch (Exception ex)
            {
                viewModel.ReturnStatus = false;
                viewModel.ReturnMessage.Add(ex.ToString());

                return View(viewModel);
            }
        }

        [HttpGet]
        public ActionResult SearchContentByName(string name = "", int page = 1)
        {
            var viewModel = new ListViewModel<CourseDto>();
            ViewBag.name = name;

            try
            {
                using (var context = new ELearningDbContext())
                {
                    int pageSize = 4;
                    var td =
                        from o in context.Courses
                        join r in context.CourseCategories on o.CategoryId equals r.Id
                        where o.Name.Contains(name)
                        select new CourseDto()
                        {
                            Id = o.Id,
                            CourseName = o.Name,
                            CatagoryName = r.Name,
                            Content = o.Content,
                            CreatedDate = o.CreatedDate,
                            CreatedBy = o.CreatedBy,
                            Description = o.Description,
                            ModifiedBy = o.ModifiedBy,
                            ModifiedDate = o.ModifiedDate,
                            Image = o.Image,
                            TrainerName = o.Trainners.Name,
                            Price = o.Price,
                            PromotionPrice = o.PromotionPrice,
                            Status = o.Status,
                            TrainerId = o.TrainerId
                        };

                    var data = td.OrderByDescending(o => o.CreatedDate).ToPagedList(page, pageSize);

                    viewModel.Data = data;
                    viewModel.ReturnStatus = true;
                    viewModel.ReturnMessage.Add("OK");

                    return View(viewModel);

                }
            }
            catch (Exception ex)
            {
                viewModel.ReturnStatus = false;
                viewModel.ReturnMessage.Add(ex.ToString());

                return View(viewModel);
            }
        }

        public ActionResult Detail(int? Id)
        {
            var dto = new CourseDto();
            if (Id != null) dto.Id = Id.Value;

            var item = _courseBusinessService.GetById(dto);

            return View(item);
        }


        public ActionResult GetContentByCategoryID(long? cateID, int? page)
        {
            var viewModel = new ListViewModel<CourseDto>();
            var categoryName = String.Empty;

            ViewBag.cateID = cateID;

            try
            {
                if (cateID == null || cateID <= 0)
                {
                    viewModel.ReturnStatus = false;
                    viewModel.ReturnMessage.Add("Error");

                    return View(viewModel);
                }

                using (var context = new ELearningDbContext())
                {
                    int pageSize = 2;
                    int pageNumber = (page ?? 1);
                   
                    var td =
                        from o in context.Courses
                        join r in context.CourseCategories on o.CategoryId equals r.Id
                        where o.CategoryId != null && o.CategoryId.Value == cateID.Value 
                        select new CourseDto()
                        {
                            Id = o.Id,
                            CourseName = o.Name,
                            CatagoryName = r.Name,
                            Content = o.Content,
                            CreatedDate = o.CreatedDate,
                            CreatedBy = o.CreatedBy,
                            Description = o.Description,
                            ModifiedBy = o.ModifiedBy,
                            ModifiedDate = o.ModifiedDate,
                            Image = o.Image,
                            TrainerName = o.Trainners.Name,
                            Price = o.Price,
                            PromotionPrice = o.PromotionPrice,
                            Status = o.Status,
                            TrainerId = o.TrainerId
                        };

                    var data = td.OrderByDescending(o => o.CreatedDate).ToPagedList(pageNumber, pageSize);

                    viewModel.Data = data;

                    categoryName = context.Category.Find(cateID)?.Name;

                    viewModel.ReturnStatus = true;
                    viewModel.ReturnMessage.Add(categoryName);

                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                viewModel.ReturnStatus = false;
                viewModel.ReturnMessage.Add(ex.ToString());

                return View(viewModel);
            }
        }
    }
}