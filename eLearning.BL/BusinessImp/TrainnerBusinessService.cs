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
    public class TrainnerBusinessService : ITrainnerBusinessService
    {
        public void Create(ApiResult<TrainnerDto> viewModel)
        {
            var trainner = new Trainners();
            trainner.CopyFromDto(viewModel.ResultObj);
            trainner.CreatedDate = DateTime.Now;

            using (var context = new ELearningDbContext())
            {
                trainner.CreatedBy = context.AppUsers.Find(viewModel.ResultObj.CreatorId)?.Email;

                context.Trainners.Add(trainner);
                context.SaveChanges();
            }

            viewModel.ReturnStatus = true;
            viewModel.ReturnMessage.Add("Created success!");

            return;
        }

        public void Delete(ApiResult<TrainnerDto> viewModel)
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

        public DataTableViewModel<TrainnerDto> GetAll()
        {
            List<TrainnerDto> data = new List<TrainnerDto>();
            var viewModel = new DataTableViewModel<TrainnerDto>();

            data = DataAccess.GetInstance().GetAllItems<TrainnerDto>(TrainnerScripts.GET_ALL_COMMAND, CommandType.Text);
            DataAccess.GetInstance().Dispose();

            viewModel.returnStatus = true;
            viewModel.returnMessage.Add("OK");
            viewModel.data = data.ToArray();
            viewModel.recordsTotal = data.Count();
            viewModel.recordsFiltered = data.Count();

            return viewModel;
        }

        public DataTableViewModel<TrainnerDto> GetAllPaging(int startIndex, int endIndex, int pageSize, int intDraw, string search)
        {
            var viewModel = new DataTableViewModel<TrainnerDto>();
            var items = new List<TrainnerDto>();
            var totalRows = 0;

            var param = new DynamicParameters();
            param.Add("@StartIndex", startIndex + 1);
            param.Add("@EndIndex", endIndex + 1);
            param.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

            string sqlQuery = TrainnerScripts.GetQueryAllPagingCommand();
            string strWhere = "";

            if (!string.IsNullOrEmpty(search))
            {
                search = "%" + search + "%";
                strWhere = " WHERE CourseName LIKE '" + search + "' ";
            }
            sqlQuery = sqlQuery.Replace("{0}", strWhere);

            items = DataAccess.GetInstance().GetItems<TrainnerDto>(sqlQuery, param, CommandType.Text);
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

        public ApiResult<TrainnerDto> GetById(TrainnerDto dto)
        {
            ApiResult<TrainnerDto> viewModel = new ApiResult<TrainnerDto>();
            viewModel.ResultObj = new TrainnerDto();

            using (var context = new ELearningDbContext())
            {
                var obj = context.Trainners.Find(dto.Id);

                viewModel.ResultObj.CopyFromModel(obj);

                viewModel.ReturnMessage.Add("GetById successfully!");
            }

            return viewModel;
        }

        public void Update(ApiResult<TrainnerDto> viewModel)
        {
            using (var context = new ELearningDbContext())
            {
                var obj = context.Trainners.Find(viewModel.ResultObj.Id);

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