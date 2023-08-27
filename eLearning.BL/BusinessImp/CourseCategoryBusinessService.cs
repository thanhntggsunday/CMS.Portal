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
    public class CourseCategoryBusinessService : ICourseCategoryBusinessService
    {
        public void Create(ApiResult<CourseCategoryDto> viewModel)
        {
            var category = new CourseCategory();
            category.CopyFromDto(viewModel.ResultObj);

            using (var context = new ELearningDbContext())
            {
                context.CourseCategories.Add(category);
                context.SaveChanges();
            }

            viewModel.ReturnStatus = true;
            viewModel.ReturnMessage.Add("Created success!");

            return;
        }

        public void Delete(ApiResult<CourseCategoryDto> viewModel)
        {
            using (var context = new ELearningDbContext())
            {
                var category = context.CourseCategories.Find(viewModel.ResultObj.ID);

                context.CourseCategories.Remove(category);
                context.SaveChanges();

                viewModel.ReturnStatus = true;
                viewModel.ReturnMessage.Add("Deleted Successfully!");
            }

            return;
        }

        public DataTableViewModel<CourseCategoryDto> GetAll()
        {
            List<CourseCategoryDto> data = new List<CourseCategoryDto>();
            var viewModel = new DataTableViewModel<CourseCategoryDto>();

            data = DataAccess.GetInstance().GetAllItems<CourseCategoryDto>(CourseCategoryScript.GET_ALL_COMMAND, CommandType.Text);
            DataAccess.GetInstance().Dispose();

            viewModel.returnStatus = true;
            viewModel.returnMessage.Add("OK");
            viewModel.data = data.ToArray();
            viewModel.recordsTotal = data.Count();
            viewModel.recordsFiltered = data.Count();

            return viewModel;
        }

        public DataTableViewModel<CourseCategoryDto> GetAllPaging(int startIndex, int endIndex, int pageSize, int intDraw, string search)
        {
            var viewModel = new DataTableViewModel<CourseCategoryDto>();
            var items = new List<CourseCategoryDto>();
            var totalRows = 0;

            var param = new DynamicParameters();
            param.Add("@StartIndex", startIndex + 1);
            param.Add("@EndIndex", endIndex + 1);
            param.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

            string sqlQuery = CourseCategoryScript.GetQueryAllPagingCommand();
            string strWhere = "";

            if (!string.IsNullOrEmpty(search))
            {
                search = "%" + search + "%";
                strWhere = " WHERE Name LIKE '" + search + "' ";
            }
            sqlQuery = sqlQuery.Replace("{0}", strWhere);

            items = DataAccess.GetInstance().GetItems<CourseCategoryDto>(sqlQuery, param, CommandType.Text);
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

        public ApiResult<CourseCategoryDto> GetById(CourseCategoryDto dto)
        {
            ApiResult<CourseCategoryDto> viewModel = new ApiResult<CourseCategoryDto>();
            viewModel.ResultObj = new CourseCategoryDto();

            using (var context = new ELearningDbContext())
            {
                var category = context.CourseCategories.Find(dto.ID);

                viewModel.ResultObj.CopyFromMoel(category);

                viewModel.ReturnMessage.Add("GetById successfully!");
            }

            return viewModel;
        }

        public void Update(ApiResult<CourseCategoryDto> viewModel)
        {
            using (var context = new ELearningDbContext())
            {
                var category = context.CourseCategories.Find(viewModel.ResultObj.ID);

                category.CopyFromDto(viewModel.ResultObj);

                context.Entry(category).State = EntityState.Modified;
                context.SaveChanges();
            }

            viewModel.ReturnStatus = true;
            viewModel.ReturnMessage.Add("Update success!");

            return;
        }
    }
}