using System;
using Common.DAL.Dapper;
using Common.ViewModel.Common;
using Dapper;
using eLearning.BL.Interfaces;
using eLearning.Common.Classes;
using eLearning.Common.Dto;
using eLearning.Common.Enums;
using eLearning.DAL.SQL;
using eLearning.Model;
using eLearning.Model.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using eLearning.Common.Constants;

namespace eLearning.BL.BusinessImp
{
    public class ContentBusinessService : IContentBusinessService
    {
        public DataTableViewModel<ContentDto> GetAll()
        {
            List<ContentDto> data = new List<ContentDto>();
            var viewModel = new DataTableViewModel<ContentDto>();

            var sql = ContentScript.GET_ALL_COMMAND;

            data = DataAccess.GetInstance().GetAllItems<ContentDto>(sql, CommandType.Text);
            DataAccess.GetInstance().Dispose();

            viewModel.returnStatus = true;
            viewModel.returnMessage.Add("OK");
            viewModel.data = data.ToArray();
            viewModel.recordsTotal = data.Count();
            viewModel.recordsFiltered = data.Count();

            return viewModel;
        }

        public DataTableViewModel<ContentDto> GetTopTen()
        {
            List<ContentDto> data = new List<ContentDto>();
            var viewModel = new DataTableViewModel<ContentDto>();
            var sql = ContentScript.GetTopNewPost();
         
            data = DataAccess.GetInstance().GetAllItems<ContentDto>(sql, CommandType.Text);
            DataAccess.GetInstance().Dispose();

            viewModel.returnStatus = true;
            viewModel.returnMessage.Add("OK");
            viewModel.data = data.ToArray();
            viewModel.recordsTotal = data.Count();
            viewModel.recordsFiltered = data.Count();

            return viewModel;
        }

        public void Create(ApiResult<ContentDto> viewModel)
        {
            var content = new Content();
            content.CopyFromDto(viewModel.ResultObj);

            using (var context = new ELearningDbContext())
            {
                context.Content.Add(content);
                context.SaveChanges();
            }

            viewModel.ReturnStatus = true;
            viewModel.ReturnMessage.Add("Created success!");
        }

        public void Delete(ApiResult<ContentDto> viewModel)
        {
            using (var context = new ELearningDbContext())
            {
                var category = context.Category.Find(viewModel.ResultObj.Id);

                context.Category.Remove(category);
                context.SaveChanges();

                viewModel.ReturnStatus = true;
                viewModel.ReturnMessage.Add("Deleted Successfully!");
            }
        }

        public DataTableViewModel<ContentDto> GetAllPaging(int startIndex, int endIndex, int intDraw, string search)
        {
            var viewModel = new DataTableViewModel<ContentDto>();
            var items = new List<ContentDto>();
            var totalRows = 0;

            var param = new DynamicParameters();
            param.Add("@StartIndex", startIndex + 1);
            param.Add("@EndIndex", endIndex + 1);
            param.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

            string sqlQuery = ContentScript.GetQueryAllPagingCommand();
            string strWhere = "";

            if (!string.IsNullOrEmpty(search))
            {
                search = "%" + search + "%";
                strWhere = " WHERE Name LIKE '" + search + "' ";
            }
            
            sqlQuery = sqlQuery.Replace("{0}", strWhere);

            items = DataAccess.GetInstance().GetItems<ContentDto>(sqlQuery, param, CommandType.Text);
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

        public ApiResult<ContentDto> GetById(ContentDto dto)
        {
            ApiResult<ContentDto> viewModel = new ApiResult<ContentDto>();
            viewModel.ResultObj = new ContentDto();

            using (var context = new ELearningDbContext())
            {
                var content = context.Content.Find(dto.Id);

                viewModel.ResultObj.CopyFromModel(content);

                viewModel.ReturnMessage.Add("GetById successfully!");
            }

            return viewModel;
        }

        public void Update(ApiResult<ContentDto> viewModel)
        {
            using (var context = new ELearningDbContext())
            {
                var content = context.Content.Find(viewModel.ResultObj.Id);

                content.CopyFromDto(viewModel.ResultObj);
                // content.ItemType = (Int32)ItemType.Blog;
                context.Entry(content).State = EntityState.Modified;

                context.SaveChanges();

            }

            viewModel.ReturnStatus = true;
            viewModel.ReturnMessage.Add("Update success!");
        }
    }
}