using System;
using System.Linq;
using System.Web.Mvc;
using Common.ViewModel.Common;
using eLearning.Common.Constants;
using eLearning.Common.Dto;
using eLearning.Model;
using PagedList;

namespace Portal.Controllers.Elc
{
    public class ElcEventController : Controller
    {
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

                    long? categoryNewsID =
                        context.Category.Where(
                            o => o.MetaKeywords.ToLower() == CategoryKeywords.CALENDAR.ToLower() 
                                 || o.MetaKeywords.ToLower() == CategoryKeywords.EVENT.ToLower())?.FirstOrDefault()?.ID;

                    var data = context.Content.AsQueryable()
                        .Where(o => o.CategoryID.Value == categoryNewsID.Value)
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

    }
}