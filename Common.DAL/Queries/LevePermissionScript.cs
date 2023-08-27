namespace Common.DAL.Queries
{
    public static class LevePermissionScript
    {
        public static string GetQueryAllCommnand()
        {
            string strAllCommand = @"SELECT * FROM ( {v} ) AS V_LEVEL_PERMISSION ";
            strAllCommand = strAllCommand.Replace("{v}", Queries00.V_LEVEL_PERMISSION);

            return strAllCommand;
        }

        public static string GetQueryAllCommnandByFunctionId()
        {
            string strAllCommand = @"SELECT * FROM ( {v} ) AS V_LEVEL_PERMISSION  WHERE FunctionId = @FunctionId";
            strAllCommand = strAllCommand.Replace("{v}", Queries00.V_LEVEL_PERMISSION);

            return strAllCommand;
        }
    }
}