namespace eLearning.DAL.SQL
{
    public class OrderScript
    {
        public const string GET_ALL_COMMAND = @"SELECT  O.[Id]
                                              ,[CustomerName]
                                              ,[CustomerAddress]
                                              ,[CustomerEmail]
                                              ,[CustomerMobile]
                                              ,[CustomerMessage]
                                              ,[PaymentMethod]
                                              ,O.[CreatedDate]
                                              ,O.[ModifiedDate]
                                              ,O.[CreatedBy]
                                              ,O.[ModifiedBy]
                                              ,[PaymentStatus]
                                              ,O.[Status]
                                              ,[Total]
                                              ,[CourseId]
                                              ,[AppUserId]
	                                          ,U.Email
	                                          ,U.UserName
	                                          ,U.FullName
	                                          ,U.PhoneNumber
	                                          ,U.Address
	                                          ,C.Name AS CourseName
	                                          ,C.Image
	                                          ,C.Description
	                                          ,C.Price
	                                          ,C.PromotionPrice
	                                          ,T.Name AS TrainerName
                                          FROM [Orders]  AS O
                                          INNER JOIN AppUsers AS U ON O.AppUserId = U.Id
                                          INNER JOIN Courses AS C ON O.CourseId = C.Id
                                          LEFT JOIN Trainners AS T ON C.TrainerId = T.Id;";

        public static string GetQueryAllPagingCommand()
        {
            string getAllPaging = @"SELECT  *
                                  FROM (
                                        SELECT ROW_NUMBER() OVER ( ORDER BY Email ) AS RowNum,  O.[Id]
                                              ,[CustomerName]
                                              ,[CustomerAddress]
                                              ,[CustomerEmail]
                                              ,[CustomerMobile]
                                              ,[CustomerMessage]
                                              ,[PaymentMethod]
                                              ,O.[CreatedDate]
                                              ,O.[ModifiedDate]
                                              ,O.[CreatedBy]
                                              ,O.[ModifiedBy]
                                              ,[PaymentStatus]
                                              ,O.[Status]
                                              ,[Total]
                                              ,[CourseId]
                                              ,[AppUserId]
	                                          ,U.Email
	                                          ,U.UserName
	                                          ,U.FullName
	                                          ,U.PhoneNumber
	                                          ,U.Address
	                                          ,C.Name AS CourseName
	                                          ,C.Image
	                                          ,C.Description
	                                          ,C.Price
	                                          ,C.PromotionPrice
	                                          ,T.Name AS TrainerName
                                        FROM [Orders]  AS O
                                        INNER JOIN AppUsers AS U ON O.AppUserId = U.Id
                                        INNER JOIN Courses AS C ON O.CourseId = C.Id
                                        LEFT JOIN Trainners AS T ON C.TrainerId = T.Id
                                        {0}
                                    ) AS RowConstrainedResult
                            WHERE   RowNum >= @StartIndex
                                    AND RowNum <= @EndIndex
                            ORDER BY Email;

                            SELECT  @TotalRows = Count(*) From (
                                        SELECT ROW_NUMBER() OVER ( ORDER BY Email ) AS RowNum,  O.[Id]
                                              ,[CustomerName]
                                              ,[CustomerAddress]
                                              ,[CustomerEmail]
                                              ,[CustomerMobile]
                                              ,[CustomerMessage]
                                              ,[PaymentMethod]
                                              ,O.[CreatedDate]
                                              ,O.[ModifiedDate]
                                              ,O.[CreatedBy]
                                              ,O.[ModifiedBy]
                                              ,[PaymentStatus]
                                              ,O.[Status]
                                              ,[Total]
                                              ,[CourseId]
                                              ,[AppUserId]
	                                          ,U.Email
	                                          ,U.UserName
	                                          ,U.FullName
	                                          ,U.PhoneNumber
	                                          ,U.Address
	                                          ,C.Name AS CourseName
	                                          ,C.Image
	                                          ,C.Description
	                                          ,C.Price
	                                          ,C.PromotionPrice
	                                          ,T.Name AS TrainerName
                                        FROM [Orders]  AS O
                                        INNER JOIN AppUsers AS U ON O.AppUserId = U.Id
                                        INNER JOIN Courses AS C ON O.CourseId = C.Id
                                        LEFT JOIN Trainners AS T ON C.TrainerId = T.Id
                                        {0}
                                    ) AS RowConstrainedResult;";

            return getAllPaging;
        }
    }
}