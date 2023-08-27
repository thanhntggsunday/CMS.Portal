using Common.DAL.Dapper;
using Common.ViewModel.Common;
using Dapper;
using eLearning.BL.Interfaces;
using eLearning.Common.Classes;
using eLearning.Common.Dto;
using eLearning.DAL.SQL;
using eLearning.Model;
using eLearning.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace eLearning.BL.BusinessImp
{
    public class CourseBusinessService : ICourseBusinessService
    {
        public void Create(ApiResult<CourseDto> viewModel)
        {
            var course = new Courses();
            course.CopyFromDto(viewModel.ResultObj);
            course.CreatedDate = DateTime.Now;

            using (var context = new ELearningDbContext())
            {
                course.CreatedBy = context.AppUsers.Find(viewModel.ResultObj.CreatorId)?.Email;

                context.Courses.Add(course);
                context.SaveChanges();
            }

            viewModel.ReturnStatus = true;
            viewModel.ReturnMessage.Add("Created success!");

            return;
        }

        public void Delete(ApiResult<CourseDto> viewModel)
        {
            using (var context = new ELearningDbContext())
            {
                var obj = context.Courses.Find(viewModel.ResultObj.Id);

                context.Courses.Remove(obj);
                context.SaveChanges();

                viewModel.ReturnStatus = true;
                viewModel.ReturnMessage.Add("Deleted Successfully!");
            }

            return;
        }

        public DataTableViewModel<CourseDto> GetAll()
        {
            List<CourseDto> data = new List<CourseDto>();
            var viewModel = new DataTableViewModel<CourseDto>();

            data = DataAccess.GetInstance().GetAllItems<CourseDto>(CourseScript.GET_ALL_COMMAND, CommandType.Text);
            DataAccess.GetInstance().Dispose();

            viewModel.returnStatus = true;
            viewModel.returnMessage.Add("OK");
            viewModel.data = data.ToArray();
            viewModel.recordsTotal = data.Count();
            viewModel.recordsFiltered = data.Count();

            return viewModel;
        }

        public DataTableViewModel<CourseDto> GetAllPaging(int startIndex, int endIndex, int intDraw, string search)
        {
            var viewModel = new DataTableViewModel<CourseDto>();
            var items = new List<CourseDto>();
            var totalRows = 0;

            var param = new DynamicParameters();
            param.Add("@StartIndex", startIndex + 1);
            param.Add("@EndIndex", endIndex + 1);
            param.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

            string sqlQuery = CourseScript.GetQueryAllPagingCommand();
            string strWhere = "";

            if (!string.IsNullOrEmpty(search))
            {
                search = "%" + search + "%";
                strWhere = " WHERE CourseName LIKE '" + search + "' ";
            }
            sqlQuery = sqlQuery.Replace("{0}", strWhere);

            items = DataAccess.GetInstance().GetItems<CourseDto>(sqlQuery, param, CommandType.Text);
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

        public ApiResult<CourseDto> GetById(CourseDto dto)
        {
            ApiResult<CourseDto> viewModel = new ApiResult<CourseDto>();
            viewModel.ResultObj = new CourseDto();

            using (var context = new ELearningDbContext())
            {
                var obj = context.Courses.Find(dto.Id);

                viewModel.ResultObj.CopyFromModel(obj);

                viewModel.ReturnMessage.Add("GetById successfully!");
            }

            return viewModel;
        }

        public void Update(ApiResult<CourseDto> viewModel)
        {
            using (var context = new ELearningDbContext())
            {
                var obj = context.Courses.Find(viewModel.ResultObj.Id);

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