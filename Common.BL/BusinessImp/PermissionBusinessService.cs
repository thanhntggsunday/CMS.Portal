using Common.BL.Interfaces;
using Common.DAL.Dapper;
using Common.DAL.Queries;
using Common.Dto;
using Common.Model;
using Common.Model.Entities;
using Common.ViewModel;
using Common.ViewModel.Common;
using Dapper;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Common.BL.BusinessImp
{
    public class PermissionBusinessService : IPermissionBusinessService
    {
        public DataTableViewModel<AppPermissionDto> GetAll()
        {
            List<AppPermissionDto> data = new List<AppPermissionDto>();
            var viewModel = new DataTableViewModel<AppPermissionDto>();

            // var dataAccess = new DataAccess();
            data = DataAccess.GetInstance().GetAllItems<AppPermissionDto>(PermissionScript.GetQueryAllPagingCommand(), CommandType.Text);
            DataAccess.GetInstance().Dispose();

            viewModel.returnStatus = true;
            viewModel.returnMessage.Add("OK");
            viewModel.data = data.ToArray();
            viewModel.recordsTotal = data.Count();
            viewModel.recordsFiltered = data.Count();

            return viewModel;
        }

        public DataTableViewModel<AppPermissionDto> GetAllPaging(int startIndex, int endIndex, string search)
        {
            var viewModel = new DataTableViewModel<AppPermissionDto>();
            var items = new List<AppPermissionDto>();
            var totalRows = 0;

            var param = new DynamicParameters();
            param.Add("@StartIndex", startIndex + 1);
            param.Add("@EndIndex", endIndex + 1);
            param.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

            string sqlQuery = PermissionScript.GetQueryAllPagingCommand();
            string strWhere = "";

            if (!string.IsNullOrEmpty(search))
            {
                search = "%" + search + "%";
                strWhere = " WHERE RoleName LIKE '" + search + "' OR FunctionId LIKE '" + search + "' OR ActionId LIKE '" + search + "'";
            }
            sqlQuery = sqlQuery.Replace("{0}", strWhere);

            //var dataAccess = new DataAccess();
            items = DataAccess.GetInstance().GetItems<AppPermissionDto>(sqlQuery, param, CommandType.Text);
            DataAccess.GetInstance().Dispose();

            totalRows = param.Get<int>("@TotalRows");

            viewModel.returnStatus = true;
            viewModel.returnMessage.Add("OK");
            viewModel.data = items.ToArray();
            viewModel.recordsTotal = totalRows;
            viewModel.recordsFiltered = totalRows;

            return viewModel;
        }

        public DataTableViewModel<AppAllUserPermissionDto> GetAllUserPermissionsByUserId(string userId)
        {
            var viewModel = new DataTableViewModel<AppAllUserPermissionDto>();
            var items = new List<AppAllUserPermissionDto>();

            var param = new DynamicParameters();
            param.Add("@UserId", userId);

            //var dataAccess = new DataAccess();
            items = DataAccess.GetInstance().GetItems<AppAllUserPermissionDto>(PermissionScript.GetQueryAllUserPermissionsByUserId(), param, CommandType.Text);
            DataAccess.GetInstance().Dispose();

            viewModel.returnStatus = true;
            viewModel.returnMessage.Add("OK");
            viewModel.data = items.ToArray();
            viewModel.recordsTotal = items.Count();
            viewModel.recordsFiltered = items.Count();

            return viewModel;
        }

        public void Insert(Role_Permission permission, out TransactionalInformation transactional)
        {
            transactional = new TransactionalInformation();

            using (var context = new CommonDbContext())
            {
                context.Role_Permission.Add(permission);
                context.SaveChanges();
            }

            transactional.ReturnStatus = true;
            transactional.ReturnMessage.Add("Success");
        }

        public void BulkInsert(List<Role_Permission> permissions, string[] arrRolesId,
            string[] arrFunctionActionId, out TransactionalInformation transactional)
        {
            transactional = new TransactionalInformation();

            using (var context = new CommonDbContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //Remove permission
                        for (int i = 0; i < arrRolesId.Length; i++)
                        {
                            var roleId = arrRolesId[i];
                            for (int j = 0; j < arrFunctionActionId.Length; j++)
                            {
                                // Remove permission
                                var functionActionId = Int32.Parse(arrFunctionActionId[j]);
                                var obj = context.Role_Permission
                                    .Where(o => o.AppRoleId.Equals(roleId)
                                      && o.Function_ActionID.Equals(functionActionId))
                                    .FirstOrDefault();
                                if (obj != null)
                                {
                                    context.Role_Permission.Remove(obj);
                                    context.SaveChanges();
                                }
                            }
                        }

                        for (int i = 0; i < permissions.Count; i++)
                        {
                            var item = permissions[i];
                            if (!string.IsNullOrEmpty(item.AppRoleId))
                            {
                                context.Role_Permission.Add(item);
                                context.SaveChanges();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        transactional.ReturnStatus = false;
                        transactional.ReturnMessage.Add(ex.Message);
                        return;
                    }
                }
            }

            transactional.ReturnStatus = true;
            transactional.ReturnMessage.Add("Success");
            return;
        }
    }
}