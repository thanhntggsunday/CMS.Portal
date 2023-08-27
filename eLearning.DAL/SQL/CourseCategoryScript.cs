namespace eLearning.DAL.SQL
{
    public class CourseCategoryScript
    {
        public const string GET_ALL_COMMAND = @"SELECT [Id]
                              ,[Name]
                              ,[SortOrder]
                              ,[SeoAlias]
                              ,[SeoMetaKeywords]
                              ,[SeoMetaDescription]
                              ,[SeoTitle]
                              ,[ParentId]
                              ,[Status]
                              ,CreatedDate
                              ,ModifiedDate
                              ,CreatedBy
                              ,ModifiedBy
                          FROM [CourseCategories]";

        public static string GetQueryAllPagingCommand()
        {
            string getAllPaging = @"SELECT  *
                            FROM    ( SELECT ROW_NUMBER() OVER ( ORDER BY Name ) AS RowNum, *
                                      FROM CourseCategories
                                      {0}
                                    ) AS RowConstrainedResult
                            WHERE   RowNum >= @StartIndex
                                    AND RowNum <= @EndIndex
                            ORDER BY Name;
                            SELECT  @TotalRows = Count(*) From CourseCategories {0};";

            return getAllPaging;
        }
    }
}