using Common.BL.Interfaces;
using Common.DAL.Dapper;
using Common.DAL.Queries;
using Common.Dto;
using Common.ViewModel.Common;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Common.BL.BusinessImp
{
    public class FunctionBusinessService : IFunctionBusinessService
    {
        public DataTableViewModel<AppFunctionDto> GetAll()
        {
            List<AppFunctionDto> functions = new List<AppFunctionDto>();
            var viewModel = new DataTableViewModel<AppFunctionDto>();
            // var dataAccess = new DataAccess();

            functions = DataAccess.GetInstance().GetAllItems<AppFunctionDto>(FunctionScript.GET_ALL_COMMAND, CommandType.Text);
            DataAccess.GetInstance().Dispose();

            viewModel.returnStatus = true;
            viewModel.returnMessage.Add("OK");
            viewModel.data = functions.ToArray();
            viewModel.recordsTotal = functions.Count();
            viewModel.recordsFiltered = functions.Count();

            return viewModel;
        }
    }
}