namespace eLearning.DAL.SQL
{
    public class CourseLessonScript
    {
        public const string GET_ALL_COMMAND = @"SELECT CL.[Id]
                                              ,CL.[Name] AS LessonName
                                              ,[VideoPath]
                                              ,[SlidePath]
                                              ,[Attachment]
                                              ,[SortOrder]
                                              ,CL.[Status]
                                              ,[CourseId]
                                              ,CL.[CreatedDate]
                                              ,Cl.[ModifiedDate]
                                              ,CL.[CreatedBy]
                                              ,CL.[ModifiedBy]
	                                          ,C.Name AS CourseName
	                                          ,C.Status AS CourseStatus
                                          FROM [CourseLessons] AS CL
                                          INNER JOIN Courses AS C
                                          ON CL.CourseId = C.Id;";

        //public static string GetQueryAllPagingCommand()
        //{
        //    string getAllPaging = @"SELECT  *
        //                    FROM    (
        //                                SELECT ROW_NUMBER() OVER ( ORDER BY CL.[Name] ) AS RowNum, CL.[Id]
        //                                    ,CL.[Name] AS LessonName
        //                                    ,[VideoPath]
        //                                    ,[SlidePath]
        //                                    ,[Attachment]
        //                                    ,[SortOrder]
        //                                    ,CL.[Status]
        //                                    ,[CourseId]
        //                                    ,CL.[CreatedDate]
        //                                    ,Cl.[ModifiedDate]
        //                                    ,CL.[CreatedBy]
        //                                    ,CL.[ModifiedBy]
	       //                                 ,C.Name AS CourseName
	       //                                 ,C.Status AS CourseStatus
        //                                FROM [CourseLessons] AS CL
        //                                INNER JOIN Courses AS C
        //                                ON CL.CourseId = C.Id
        //                              {0}
        //                            ) AS RowConstrainedResult
        //                    WHERE   RowNum >= @StartIndex
        //                            AND RowNum <= @EndIndex
        //                    ORDER BY LessonName;
        //                    SELECT  @TotalRows = Count(*) From (
        //                                SELECT ROW_NUMBER() OVER ( ORDER BY CL.[Name] ) AS RowNum, CL.[Id]
        //                                    ,CL.[Name] AS LessonName
        //                                    ,[VideoPath]
        //                                    ,[SlidePath]
        //                                    ,[Attachment]
        //                                    ,[SortOrder]
        //                                    ,CL.[Status]
        //                                    ,[CourseId]
        //                                    ,CL.[CreatedDate]
        //                                    ,Cl.[ModifiedDate]
        //                                    ,CL.[CreatedBy]
        //                                    ,CL.[ModifiedBy]
	       //                                 ,C.Name AS CourseName
	       //                                 ,C.Status AS CourseStatus
        //                                FROM [CourseLessons] AS CL
        //                                INNER JOIN Courses AS C
        //                                ON CL.CourseId = C.Id
        //                              {0}
        //                            ) AS RowConstrainedResult;";

        //    return getAllPaging;
        //}

        public static string GetQueryAllCourseLessonByCourseIDPagingCommand()
        {
            string getAllPaging = @"SELECT  *
                            FROM    (
                                        SELECT ROW_NUMBER() OVER ( ORDER BY CL.[Name] ) AS RowNum, CL.[Id]
                                            ,CL.[Name] AS LessonName
                                            ,[VideoPath]
                                            ,[SlidePath]
                                            ,[Attachment]
                                            ,[SortOrder]
                                            ,CL.[Status]
                                            ,[CourseId]
                                            ,CL.[CreatedDate]
                                            ,Cl.[ModifiedDate]
                                            ,CL.[CreatedBy]
                                            ,CL.[ModifiedBy]
	                                        ,C.Name AS CourseName
	                                        ,C.Status AS CourseStatus
                                        FROM [CourseLessons] AS CL
                                        INNER JOIN Courses AS C
                                        ON CL.CourseId = C.Id AND C.Id = @CourseId
                                      {0}
                                    ) AS RowConstrainedResult
                            WHERE   RowNum >= @StartIndex
                                    AND RowNum <= @EndIndex
                            ORDER BY LessonName;
                            SELECT  @TotalRows = Count(*) From (
                                        SELECT ROW_NUMBER() OVER ( ORDER BY CL.[Name] ) AS RowNum, CL.[Id]
                                            ,CL.[Name] AS LessonName
                                            ,[VideoPath]
                                            ,[SlidePath]
                                            ,[Attachment]
                                            ,[SortOrder]
                                            ,CL.[Status]
                                            ,[CourseId]
                                            ,CL.[CreatedDate]
                                            ,Cl.[ModifiedDate]
                                            ,CL.[CreatedBy]
                                            ,CL.[ModifiedBy]
	                                        ,C.Name AS CourseName
	                                        ,C.Status AS CourseStatus
                                        FROM [CourseLessons] AS CL
                                        INNER JOIN Courses AS C 
                                        ON CL.CourseId = C.Id AND C.Id = @CourseId
                                      {0}
                                    ) AS RowConstrainedResult;";

            return getAllPaging;
        }
    }
}