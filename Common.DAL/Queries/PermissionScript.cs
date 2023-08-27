namespace Common.DAL.Queries
{
    public static class PermissionScript
    {
        public const string INSERT_INTO_ROLE_PERMISSION = @"INSERT INTO [dbo].[Role_Permission]
                                                           ([RoleID]
                                                           ,[Function_ActionID])
                                                     VALUES
                                                           (@RoleID, @Function_ActionID);
                                                    SELECT  @ID = SCOPE_IDENTITY();";

        public const string REMOVE_ALL_ROLE_PERMISSION_OF_ROLE = @"DELETE FROM [Role_Permission] WHERE AppRoleId = @RoleID AND Function_ActionID = @Function_ActionID";

        public static string GetQueryAllPagingCommand()
        {
            string getAllPaging = @"SELECT  *
                            FROM    ( SELECT ROW_NUMBER() OVER ( ORDER BY RoleName ) AS RowNum, *
                                      FROM ( {v} ) AS  V_ALL_ROLE_PERMISSION
                                      {0}
                                    ) AS RowConstrainedResult
                            WHERE   RowNum >= @StartIndex
                                    AND RowNum <= @EndIndex
                            ORDER BY RoleName;
                            SELECT  @TotalRows = Count(*) From ( {v} ) AS  V_ALL_ROLE_PERMISSION {0};";

            getAllPaging = getAllPaging.Replace("{v}", Queries00.V_ALL_ROLE_PERMISSION);

            return getAllPaging;
        }

        public static string GetQueryAllUserPermissionsByUserId()
        {
            string strAllUserPermission = @"SELECT *  FROM ( {v} ) AS V_ALL_USER_ROLE_PERMISSION WHERE UserId = @UserId";
            strAllUserPermission = strAllUserPermission.Replace("{v}", Queries00.V_ALL_USER_ROLE_PERMISSION);

            return strAllUserPermission;
        }
    }
}