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
    public class ContentCategoryBusinessService : IContentCategoryBusinessService
    {
        public DataTableViewModel<ContentCategoryDto> GetAll()
        {
            List<ContentCategoryDto> data = new List<ContentCategoryDto>();
            var viewModel = new DataTableViewModel<ContentCategoryDto>();

            data = DataAccess.GetInstance().GetAllItems<ContentCategoryDto>(ContentCategoryScript.GET_ALL_COMMAND, CommandType.Text);
            DataAccess.GetInstance().Dispose();

            viewModel.returnStatus = true;
            viewModel.returnMessage.Add("OK");
            viewModel.data = data.ToArray();
            viewModel.recordsTotal = data.Count();
            viewModel.recordsFiltered = data.Count();

            return viewModel;
        }

        public void Create(ApiResult<ContentCategoryDto> viewModel)
        {
            var category = new ContentCategory();
            category.CopyFromDto(viewModel.ResultObj);

            using (var context = new ELearningDbContext())
            {
                context.Category.Add(category);
                context.SaveChanges();
            }

            viewModel.ReturnStatus = true;
            viewModel.ReturnMessage.Add("Created success!");
        }

        public void Delete(ApiResult<ContentCategoryDto> viewModel)
        {
            using (var context = new ELearningDbContext())
            {
                var category = context.Category.Find(viewModel.ResultObj.ID);

                context.Category.Remove(category);
                context.SaveChanges();

                viewModel.ReturnStatus = true;
                viewModel.ReturnMessage.Add("Deleted Successfully!");
            }
        }

        public DataTableViewModel<ContentCategoryDto> GetAllPaging(int startIndex, int endIndex, int pageSize, int intDraw, string search)
        {
            var viewModel = new DataTableViewModel<ContentCategoryDto>();
            var items = new List<ContentCategoryDto>();
            var totalRows = 0;

            var param = new DynamicParameters();
            param.Add("@StartIndex", startIndex + 1);
            param.Add("@EndIndex", endIndex + 1);
            param.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

            string sqlQuery = ContentCategoryScript.GetQueryAllPagingCommand();
            string strWhere = "";

            if (!string.IsNullOrEmpty(search))
            {
                search = "%" + search + "%";
                strWhere = " WHERE Name LIKE '" + search + "' ";
            }
            sqlQuery = sqlQuery.Replace("{0}", strWhere);

            items = DataAccess.GetInstance().GetItems<ContentCategoryDto>(sqlQuery, param, CommandType.Text);
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

        public ApiResult<ContentCategoryDto> GetById(ContentCategoryDto dto)
        {
            ApiResult<ContentCategoryDto> viewModel = new ApiResult<ContentCategoryDto>();
            viewModel.ResultObj = new ContentCategoryDto();

            using (var context = new ELearningDbContext())
            {
                var category = context.Category.Find(dto.ID);

                viewModel.ResultObj.CopyFromModel(category);

                viewModel.ReturnMessage.Add("GetById successfully!");
            }

            return viewModel;
        }

        public void Update(ApiResult<ContentCategoryDto> viewModel)
        {
            using (var context = new ELearningDbContext())
            {
                var category = context.Category.Find(viewModel.ResultObj.ID);

                category.CopyFromDto(viewModel.ResultObj);

                context.Entry(category).State = EntityState.Modified;
                context.SaveChanges();
            }

            viewModel.ReturnStatus = true;
            viewModel.ReturnMessage.Add("Update success!");
        }
    }
}