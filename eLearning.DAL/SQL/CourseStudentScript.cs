namespace eLearning.DAL.SQL
{
    public class CourseStudentScript
    {
        public const string GET_ALL_COMMAND = @"SELECT  CS.[Id]
                                                  ,[CourseId]
                                                  ,[AppUserId]
                                                  ,CS.[Price]
                                                  ,CS.[CreatedDate]
                                                  ,CS.[ModifiedDate]
                                                  ,CS.[CreatedBy]
                                                  ,CS.[ModifiedBy]
	                                              ,C.Name AS CourseName
	                                              ,U.Email
	                                              ,U.UserName
	                                              ,U.FullName
	                                              ,U.PhoneNumber
                                              FROM [CoursesStudents] AS CS
                                              INNER JOIN Courses AS C ON CS.CourseId = C.Id
                                              INNER JOIN AppUsers AS U ON U.Id = CS.AppUserId;";

        public static string GetQueryAllPagingCommand()
        {
            string getAllPaging = @"SELECT  *
                            FROM    (
                                        SELECT  ROW_NUMBER() OVER ( ORDER BY U.Email ) AS RowNum, CS.[Id]
                                                ,[CourseId]
                                                ,[AppUserId]
                                                ,CS.[Price]
                                                ,CS.[CreatedDate]
                                                ,CS.[ModifiedDate]
                                                ,CS.[CreatedBy]
                                                ,CS.[ModifiedBy]
	                                            ,C.Name AS CourseName
	                                            ,U.Email
	                                            ,U.UserName
	                                            ,U.FullName
	                                            ,U.PhoneNumber
                                            FROM [CoursesStudents] AS CS
                                            INNER JOIN Courses AS C ON CS.CourseId = C.Id AND C.Id = @CourseId
                                            INNER JOIN AppUsers AS U ON U.Id = CS.AppUserId
                                      {0}
                                    ) AS RowConstrainedResult
                            WHERE   RowNum >= @StartIndex
                                    AND RowNum <= @EndIndex
                            ORDER BY CourseName;
                            SELECT  @TotalRows = Count(*) From (
                                        SELECT  ROW_NUMBER() OVER ( ORDER BY U.Email ) AS RowNum, CS.[Id]
                                                ,[CourseId]
                                                ,[AppUserId]
                                                ,CS.[Price]
                                                ,CS.[CreatedDate]
                                                ,CS.[ModifiedDate]
                                                ,CS.[CreatedBy]
                                                ,CS.[ModifiedBy]
	                                            ,C.Name AS CourseName
	                                            ,U.Email
	                                            ,U.UserName
	                                            ,U.FullName
	                                            ,U.PhoneNumber
                                            FROM [CoursesStudents] AS CS
                                            INNER JOIN Courses AS C ON CS.CourseId = C.Id AND C.Id = @CourseId
                                            INNER JOIN AppUsers AS U ON U.Id = CS.AppUserId
                                      {0}
                                    ) AS RowConstrainedResult;";

            return getAllPaging;
        }
    }
}