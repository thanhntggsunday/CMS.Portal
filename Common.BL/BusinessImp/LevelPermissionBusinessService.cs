using Common.BL.Interfaces;
using Common.DAL.Dapper;
using Common.DAL.Queries;
using Common.Dto;
using Common.ViewModel.Common;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Common.BL.BusinessImp
{
    public class LevelPermissionBusinessService : ILevelPermissionBusinessService
    {
        public DataTableViewModel<AppLevelPermissionDto> GetAll()
        {
            List<AppLevelPermissionDto> data = new List<AppLevelPermissionDto>();
            var viewModel = new DataTableViewModel<AppLevelPermissionDto>();

            // var dataAccess = new DataAccess();
            data = DataAccess.GetInstance().GetAllItems<AppLevelPermissionDto>(LevePermissionScript.GetQueryAllCommnand(), CommandType.Text);
            DataAccess.GetInstance().Dispose();

            viewModel.returnStatus = true;
            viewModel.returnMessage.Add("OK");
            viewModel.data = data.ToArray();
            viewModel.recordsTotal = data.Count();
            viewModel.recordsFiltered = data.Count();

            return viewModel;
        }

        public DataTableViewModel<AppLevelPermissionDto> GetAllLevelPermissionByFunctionId(string functionId)
        {
            List<AppLevelPermissionDto> items = new List<AppLevelPermissionDto>();
            var viewModel = new DataTableViewModel<AppLevelPermissionDto>();

            var param = new DynamicParameters();
            param.Add("@FunctionId", functionId);

            // var dataAccess = new DataAccess();
            items = DataAccess.GetInstance().GetItems<AppLevelPermissionDto>(LevePermissionScript.GetQueryAllCommnandByFunctionId(), param, CommandType.Text);
            DataAccess.GetInstance().Dispose();

            viewModel.returnStatus = true;
            viewModel.returnMessage.Add("OK");
            viewModel.data = items.ToArray();
            viewModel.recordsTotal = items.Count();

            return viewModel;
        }
    }
}