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
    public class CourseStudentBusinessService : ICourseStudentBusinessService
    {
        public void Create(ApiResult<CourseStudentDto> viewModel)
        {
            var obj = new CoursesStudents();
            obj.CopyFromDto(viewModel.ResultObj);

            using (var context = new ELearningDbContext())
            {
                context.CoursesStudents.Add(obj);
                context.SaveChanges();
            }

            viewModel.ReturnStatus = true;
            viewModel.ReturnMessage.Add("Created success!");

            return;
        }

        public void Delete(ApiResult<CourseStudentDto> viewModel)
        {
            using (var context = new ELearningDbContext())
            {
                var obj = context.CoursesStudents.Find(viewModel.ResultObj.Id);

                context.CoursesStudents.Remove(obj);
                context.SaveChanges();

                viewModel.ReturnStatus = true;
                viewModel.ReturnMessage.Add("Deleted Successfully!");
            }

            return;
        }

        public DataTableViewModel<CourseStudentDto> GetAll()
        {
            List<CourseStudentDto> data = new List<CourseStudentDto>();
            var viewModel = new DataTableViewModel<CourseStudentDto>();

            data = DataAccess.GetInstance().GetAllItems<CourseStudentDto>(CourseStudentScript.GET_ALL_COMMAND, CommandType.Text);
            DataAccess.GetInstance().Dispose();

            viewModel.returnStatus = true;
            viewModel.returnMessage.Add("OK");
            viewModel.data = data.ToArray();
            viewModel.recordsTotal = data.Count();
            viewModel.recordsFiltered = data.Count();

            return viewModel;
        }

        public DataTableViewModel<CourseStudentDto> GetAllStudentByCourseIDPaging(int courseID, int startIndex, int endIndex, int pageSize, int intDraw, string search)
        {
            var viewModel = new DataTableViewModel<CourseStudentDto>();
            var items = new List<CourseStudentDto>();
            var totalRows = 0;

            var param = new DynamicParameters();
            param.Add("@CourseId", courseID);
            param.Add("@StartIndex", startIndex + 1);
            param.Add("@EndIndex", endIndex + 1);
            param.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

            string sqlQuery = CourseStudentScript.GetQueryAllPagingCommand();
            string strWhere = "";

            if (!string.IsNullOrEmpty(search))
            {
                search = "%" + search + "%";
                strWhere = " WHERE U.Email LIKE '" + search + "' " + " OR " + " Email LIKE '" + search + "' ";
            }

            sqlQuery = sqlQuery.Replace("{0}", strWhere);

            items = DataAccess.GetInstance().GetItems<CourseStudentDto>(sqlQuery, param, CommandType.Text);
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

        public ApiResult<CourseStudentDto> GetById(CourseStudentDto dto)
        {
            ApiResult<CourseStudentDto> viewModel = new ApiResult<CourseStudentDto>();
            viewModel.ResultObj = new CourseStudentDto();

            using (var context = new ELearningDbContext())
            {
                var obj = context.CoursesStudents.Find(dto.Id);

                viewModel.ResultObj.CopyFromModel(obj);

                viewModel.ReturnMessage.Add("GetById successfully!");
            }

            return viewModel;
        }

        public void Update(ApiResult<CourseStudentDto> viewModel)
        {
            using (var context = new ELearningDbContext())
            {
                var obj = context.CoursesStudents.Find(viewModel.ResultObj.Id);

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