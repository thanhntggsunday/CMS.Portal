using Common.DAL.Dapper;
using Common.ViewModel.Common;
using Dapper;
using eLearning.BL.Interfaces;
using eLearning.Common.Classes;
using eLearning.Common.Dto;
using eLearning.DAL.SQL;
using eLearning.Model;
using eLearning.Model.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace eLearning.BL.BusinessImp
{
    public class CourseLessonBusinessService : ICourseLessonBusinessService
    {
        public void Create(ApiResult<CourseLessonDto> viewModel)
        {
            var course = new CourseLessons();
            course.CopyFromDto(viewModel.ResultObj);

            using (var context = new ELearningDbContext())
            {
                context.CourseLessons.Add(course);
                context.SaveChanges();
            }

            viewModel.ReturnStatus = true;
            viewModel.ReturnMessage.Add("Created success!");

            return;
        }

        public void Delete(ApiResult<CourseLessonDto> viewModel)
        {
            using (var context = new ELearningDbContext())
            {
                var obj = context.CourseLessons.Find(viewModel.ResultObj.Id);

                context.CourseLessons.Remove(obj);
                context.SaveChanges();

                viewModel.ReturnStatus = true;
                viewModel.ReturnMessage.Add("Deleted Successfully!");
            }

            return;
        }

        public DataTableViewModel<CourseLessonDto> GetAll()
        {
            List<CourseLessonDto> data = new List<CourseLessonDto>();
            var viewModel = new DataTableViewModel<CourseLessonDto>();

            data = DataAccess.GetInstance().GetAllItems<CourseLessonDto>(CourseLessonScript.GET_ALL_COMMAND, CommandType.Text);
            DataAccess.GetInstance().Dispose();

            viewModel.returnStatus = true;
            viewModel.returnMessage.Add("OK");
            viewModel.data = data.ToArray();
            viewModel.recordsTotal = data.Count();
            viewModel.recordsFiltered = data.Count();

            return viewModel;
        }

        public DataTableViewModel<CourseLessonDto> GetAllCouresLessonByCourseIDPaging(int courseID, int startIndex, int endIndex, int pageSize, int intDraw, string search)
        {
            var viewModel = new DataTableViewModel<CourseLessonDto>();
            var items = new List<CourseLessonDto>();
            var totalRows = 0;

            var param = new DynamicParameters();
            param.Add("@CourseId", courseID);
            param.Add("@StartIndex", startIndex + 1);
            param.Add("@EndIndex", endIndex + 1);
            param.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

            string sqlQuery = CourseLessonScript.GetQueryAllCourseLessonByCourseIDPagingCommand();
            string strWhere = "";

            if (!string.IsNullOrEmpty(search))
            {
                search = "%" + search + "%";
                strWhere = " WHERE CL.[Name] LIKE '" + search + "' ";
            }
            sqlQuery = sqlQuery.Replace("{0}", strWhere);

            items = DataAccess.GetInstance().GetItems<CourseLessonDto>(sqlQuery, param, CommandType.Text);
            DataAccess.GetInstance().Dispose();

            totalRows = param.Get<int>("@TotalRows");

            viewModel.returnStatus = true;
            viewModel.returnMessage.Add("OK");
            viewModel.data = items.ToArray();
            viewModel.recordsTotal = totalRows;
            viewModel.recordsFiltered = totalRows;
            viewModel.draw = intDraw;

            return viewModel;
        }

        //public DataTableViewModel<CourseLessonDto> GetAllPaging(int startIndex, int endIndex, int pageSize, int intDraw, string search)
        //{
        //    var viewModel = new DataTableViewModel<CourseLessonDto>();
        //    var items = new List<CourseLessonDto>();
        //    var totalRows = 0;

        //    var param = new DynamicParameters();
        //    param.Add("@StartIndex", startIndex + 1);
        //    param.Add("@EndIndex", endIndex + 1);
        //    param.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

        //    string sqlQuery = CourseLessonScript.GetQueryAllPagingCommand();
        //    string strWhere = "";

        //    if (!string.IsNullOrEmpty(search))
        //    {
        //        search = "%" + search + "%";
        //        strWhere = " WHERE LessonName LIKE '" + search + "' ";
        //    }
        //    sqlQuery = sqlQuery.Replace("{0}", strWhere);

        //    items = DataAccess.GetInstance().GetListItems<CourseLessonDto>(sqlQuery, param, CommandType.Text);
        //    DataAccess.GetInstance().Dispose();

        //    totalRows = param.Get<int>("@TotalRows");

        //    viewModel.returnStatus = true;
        //    viewModel.returnMessage.Add("OK");
        //    viewModel.data = items.ToArray();
        //    viewModel.recordsTotal = totalRows;
        //    viewModel.recordsFiltered = totalRows;
        //    viewModel.draw = intDraw;

        //    return viewModel;
        //}

        public ApiResult<CourseLessonDto> GetById(CourseLessonDto dto)
        {
            ApiResult<CourseLessonDto> viewModel = new ApiResult<CourseLessonDto>();
            viewModel.ResultObj = new CourseLessonDto();

            using (var context = new ELearningDbContext())
            {
                var query = from c in context.Courses
                            join cl in context.CourseLessons on c.Id equals cl.CourseId
                            select new CourseLessonDto
                            {
                                Id = cl.Id,
                                CourseId = c.Id,
                                LessonName = cl.Name,
                                VideoPath = cl.VideoPath,
                                SlidePath = cl.SlidePath,
                                Status = cl.Status
                            };
                var item = query.FirstOrDefault(o => o.Id == dto.Id);

                viewModel.ResultObj = item;
                viewModel.ReturnMessage.Add("GetById successfully!");
            }

            return viewModel;
        }

        public void Update(ApiResult<CourseLessonDto> viewModel)
        {
            using (var context = new ELearningDbContext())
            {
                var obj = context.CourseLessons.Find(viewModel.ResultObj.Id);

                obj.CopyFromDto(viewModel.ResultObj);

                context.Entry(obj).State = EntityState.Modified;
                context.SaveChanges();
            }

            viewModel.ReturnStatus = true;
            viewModel.ReturnMessage.Add("Update success!");

            return;
        }
    }
}