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
    public class OrderBusinessService : IOrderBusinessService
    {
        public void Create(ApiResult<OrderDto> viewModel)
        {
            var obj = new Order();
            obj.CopyFromDto(viewModel.ResultObj);

            using (var context = new ELearningDbContext())
            {
                context.Orders.Add(obj);
                context.SaveChanges();
            }

            viewModel.ReturnStatus = true;
            viewModel.ReturnMessage.Add("Created success!");

            return;
        }

        public void Delete(ApiResult<OrderDto> viewModel)
        {
            using (var context = new ELearningDbContext())
            {
                var obj = context.Orders.Find(viewModel.ResultObj.Id);

                context.Orders.Remove(obj);
                context.SaveChanges();

                viewModel.ReturnStatus = true;
                viewModel.ReturnMessage.Add("Deleted Successfully!");
            }

            return;
        }

        public DataTableViewModel<OrderDto> GetAll()
        {
            List<OrderDto> data = new List<OrderDto>();
            var viewModel = new DataTableViewModel<OrderDto>();

            data = DataAccess.GetInstance().GetAllItems<OrderDto>(OrderScript.GET_ALL_COMMAND, CommandType.Text);
            DataAccess.GetInstance().Dispose();

            viewModel.returnStatus = true;
            viewModel.returnMessage.Add("OK");
            viewModel.data = data.ToArray();
            viewModel.recordsTotal = data.Count();
            viewModel.recordsFiltered = data.Count();

            return viewModel;
        }

        public DataTableViewModel<OrderDto> GetAllPaging(int startIndex, int endIndex, int pageSize, int intDraw, string search)
        {
            var viewModel = new DataTableViewModel<OrderDto>();
            var items = new List<OrderDto>();
            var totalRows = 0;

            var param = new DynamicParameters();
            param.Add("@StartIndex", startIndex + 1);
            param.Add("@EndIndex", endIndex + 1);
            param.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

            string sqlQuery = OrderScript.GetQueryAllPagingCommand();
            string strWhere = "";

            if (!string.IsNullOrEmpty(search))
            {
                search = "%" + search + "%";
                strWhere = " WHERE CourseName LIKE '" + search + "' " + " OR " + " Email LIKE '" + search + "' ;";
            }
            sqlQuery = sqlQuery.Replace("{0}", strWhere);

            items = DataAccess.GetInstance().GetItems<OrderDto>(sqlQuery, param, CommandType.Text);
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

        public ApiResult<OrderDto> GetById(OrderDto dto)
        {
            ApiResult<OrderDto> viewModel = new ApiResult<OrderDto>();
            viewModel.ResultObj = new OrderDto();

            using (var context = new ELearningDbContext())
            {
                var obj = context.Orders.Find(dto.Id);

                viewModel.ResultObj.CopyFromModel(obj);

                viewModel.ReturnMessage.Add("GetById successfully!");
            }

            return viewModel;
        }

        public void Update(ApiResult<OrderDto> viewModel)
        {
            using (var context = new ELearningDbContext())
            {
                var obj = context.Orders.Find(viewModel.ResultObj.Id);

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