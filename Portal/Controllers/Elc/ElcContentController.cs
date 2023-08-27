using System;
using System.Linq;
using System.Web.Mvc;
using Common.ViewModel.Common;
using eLearning.BL.Interfaces;
using eLearning.Common.Constants;
using eLearning.Common.Dto;
using eLearning.Model;
using PagedList;

namespace Portal.Controllers.Elc
{
    public class ElcContentController : Controller
    {
        private IContentBusinessService _contentBusinessService;

        public ElcContentController(IContentBusinessService contentBusinessService)
        {
            _contentBusinessService = contentBusinessService;
        }

        // GET: eLearningClient/Content
        public ActionResult Index(int? page)
        {
            var viewModel = new ListViewModel<ContentDto>();

            try
            {
                using (var context = new ELearningDbContext())
                {
                    int pageSize = 2;
                    int pageNumber = (page ?? 1);

                    var categoryNewsID =
                        context.Category.Where(o => o.MetaKeywords.ToLower() == CategoryKeywords.NEWS.ToLower())?.FirstOrDefault()?.ID;

                    var data = context.Content.AsQueryable()
                        .Where(o => o.CategoryID == categoryNewsID).Select(o => new ContentDto()
                        {
                            Id = o.ID,
                            ContentName = o.Name,
                            Detail = o.Detail,
                            Image = o.Image,
                            Description = o.Description,
                            CreatedDate = o.CreatedDate,
                            CreatedBy = o.CreatedBy
                        })
                      .OrderByDescending(o => o.CreatedDate).ToPagedList(pageNumber, pageSize);

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
            var viewModel = new ListViewModel<ContentDto>();
            ViewBag.name = name;

            try
            {
                using (var context = new ELearningDbContext())
                {
                    int pageSize = 2;


                    var data = context.Content.AsQueryable()
                        .Where(o => o.Name.Contains(name)).Select(o => new ContentDto()
                        {
                            Id = o.ID,
                            ContentName = o.Name,
                            Detail = o.Detail,
                            Image = o.Image,
                            Description = o.Description,
                            CreatedDate = o.CreatedDate,
                            CreatedBy = o.CreatedBy
                        })
                        .OrderByDescending(o => o.CreatedDate).ToPagedList(page, pageSize);

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

        //[HttpGet]
        //public JsonResult GetJsonData(int page = 1, int pageSize = 12, string name = "")
        //{
        //    var result = new DataTableViewModel<ContentDto>();

        //    try
        //    {
        //        var startindex = (page - 1) * pageSize;
        //        var endIndex = startindex + pageSize - 1;

        //        result = _contentBusinessService.GetAllPaging(startindex, endIndex, 0, name);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.returnStatus = false;
        //        result.returnMessage.Add(ex.Message);
        //    }

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Detail(int? Id)
        {
            var dto = new ContentDto();
            if (Id != null) dto.Id = Id.Value;

            var item = _contentBusinessService.GetById(dto);

            return View(item);
        }


        public ActionResult GetContentByCategoryID(long? cateID, int? page)
        {
            var viewModel = new ListViewModel<ContentDto>();
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
                    var data = context.Content.AsQueryable()
                        .Where(o => o.CategoryID.Value == cateID.Value)
                        .Select(o => new ContentDto()
                        {
                            Id = o.ID,
                            ContentName = o.Name,
                            Detail = o.Detail,
                            Image = o.Image,
                            Description = o.Description,
                            CreatedDate = o.CreatedDate,
                            CreatedBy = o.CreatedBy
                        })
                        .OrderByDescending(o => o.CreatedDate).ToPagedList(pageNumber, pageSize);

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