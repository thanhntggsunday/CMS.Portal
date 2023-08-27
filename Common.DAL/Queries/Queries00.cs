namespace Common.DAL.Queries
{
    public static class Queries00
    {
        public const string V_ALL_ROLE_PERMISSION = @"SELECT R.Name As RoleName, R.Id As RoleId,
	        F.ID As FunctionId, F.Name As FunctionName, A.ID As ActionId, A.Name As ActionName
            FROM AppRoles As R
            LEFT JOIN Role_Permission As RP ON R.Id = RP.AppRoleId
            LEFT JOIN Function_Action As FA ON RP.Function_ActionID = FA.ID
            LEFT JOIN Functions As F ON FA.FunctionId = F.ID
            LEFT JOIN Action As A ON FA.ActionId = A.ID";

        public const string V_ALL_USER_ROLE_PERMISSION = @"SELECT U.Email, U.Id As UserId,
            R.Name As RoleName, R.Id As RoleId,
	        F.ID As FunctionId, F.Name As FunctionName, A.ID As ActionId, A.Name As ActionName
            FROM AppUsers AS U
            LEFT JOIN AppUserRoles As UR ON U.Id = UR.UserId
            LEFT JOIN AppRoles As R ON UR.RoleId = R.Id
            LEFT JOIN Role_Permission As RP ON R.Id = RP.AppRoleId
            LEFT JOIN Function_Action As FA ON RP.Function_ActionID = FA.ID
            LEFT JOIN Functions As F ON FA.FunctionId = F.ID
            LEFT JOIN Action As A ON FA.ActionId = A.ID";

        public const string V_LEVEL_PERMISSION = @"SELECT FA.ID As FunctionActionId, F.ID As FunctionId,
            F.Name As FunctionName, A.ID As ActionId, A.Name As ActionName
            From Functions AS F
            INNER JOIN Function_Action As FA ON F.ID = FA.FunctionId
            INNER JOIN Action As A ON FA.ActionId = A.ID";
    }
}