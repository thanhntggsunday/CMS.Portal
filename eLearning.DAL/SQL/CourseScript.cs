namespace eLearning.DAL.SQL
{
    public class CourseScript
    {
        public const string GET_ALL_COMMAND = @"SELECT C.[Id]
                              ,C.[Name] AS CourseName
                              ,[Description]
                              ,[Image]
                              ,[Content]
                              ,[Price]
                              ,[PromotionPrice]                           
                              ,C.[Status]
                              ,[CategoryId]
                              ,[TrainerId]
                              ,C.[CreatedDate]
                              ,C.[ModifiedDate]
                              ,C.[CreatedBy]
                              ,C.[ModifiedBy],
	                          CA.Name As CatagoryName
                          FROM [Courses] AS C
                          INNER JOIN CourseCategories AS CA ON
                          C.CategoryId = CA.Id";

        public static string GetQueryAllPagingCommand()
        {
            string getAllPaging = @"SELECT  *
                            FROM    (
                                        SELECT ROW_NUMBER() OVER ( ORDER BY C.Name ) AS RowNum, C.[Id]
                                            ,C.[Name] AS CourseName
                                            ,[Description]
                                            ,[Image]
                                            ,[Content]
                                            ,[Price]
                                            ,[PromotionPrice]                                          
                                            ,C.[Status]
                                            ,[CategoryId]
                                            ,[TrainerId]
                                            ,C.[CreatedDate]
                                            ,C.[ModifiedDate]
                                            ,C.[CreatedBy]
                                            ,C.[ModifiedBy],
	                                        CA.Name As CatagoryName
                                        FROM [Courses] AS C
                                        INNER JOIN CourseCategories AS CA ON
                                        C.CategoryId = CA.Id
                                      {0}
                                    ) AS RowConstrainedResult
                            WHERE   RowNum >= @StartIndex
                                    AND RowNum <= @EndIndex
                            ORDER BY CourseName;

                            SELECT  @TotalRows = Count(*) From (
                                        SELECT ROW_NUMBER() OVER ( ORDER BY C.Name ) AS RowNum, C.[Id]
                                            ,C.[Name] AS CourseName
                                            ,[Description]
                                            ,[Image]
                                            ,[Content]
                                            ,[Price]
                                            ,[PromotionPrice]                                          
                                            ,C.[Status]
                                            ,[CategoryId]
                                            ,[TrainerId]
                                            ,C.[CreatedDate]
                                            ,C.[ModifiedDate]
                                            ,C.[CreatedBy]
                                            ,C.[ModifiedBy],
	                                        CA.Name As CatagoryName
                                        FROM [Courses] AS C
                                        INNER JOIN CourseCategories AS CA ON
                                        C.CategoryId = CA.Id
                                      {0}
                                    ) AS RowConstrainedResult;";

            return getAllPaging;
        }
    }
}