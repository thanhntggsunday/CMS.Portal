using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearning.DAL.SQL
{
    public class ContentScript
    {
        public const string GET_ALL_COMMAND = @"SELECT [ID]
              ,[Name]
              ,[MetaTitle]
              ,[Description]
              ,[Image]
              ,[CategoryID]
              ,[Detail]
              ,[Warranty]
              ,[CreatedDate]
              ,[CreatedBy]
              ,[ModifiedDate]
              ,[ModifiedBy]
              ,[MetaKeywords]
              ,[MetaDescriptions]
              ,[Status]
              ,[TopHot]
              ,[ViewCount]
              ,[Tags]
              ,[Language]
          FROM [Content] -- WHERE ItemType = {0}";

        public static string GetQueryAllPagingCommand()
        {
            string getAllPaging = @"SELECT  *
                                    FROM (SELECT ROW_NUMBER() OVER ( ORDER BY Co.Name ) AS RowNum, Co.[ID]
			                                      ,Co.[Name] As ContentName
			                                      ,Co.[MetaTitle]
			                                      ,Co.[Description]
			                                      ,Co.[Image]
			                                      ,Co.[CategoryID]
			                                      ,Co.[Detail]
			                                      ,Co.[Warranty]
			                                      ,Co.[CreatedDate]
			                                      ,Co.[CreatedBy]
			                                      ,Co.[ModifiedDate]
			                                      ,Co.[ModifiedBy]
			                                      ,Co.[MetaKeywords]
			                                      ,Co.[MetaDescriptions]
			                                      ,Co.[Status]
			                                      ,Co.[TopHot]
			                                      ,Co.[ViewCount]
			                                      ,Co.[Tags]
			                                      ,Co.[Language]
			                                      ,Co.[MetaCode]
			                                      ,Ca.[Name] As CategoryName
                                                FROM Content Co
			                                    LEFT JOIN ContentCategories CA ON  Co.CategoryId = CA.Id
                                                {0}
                                            ) AS RowConstrainedResult
                                    WHERE RowNum >= @StartIndex
                                            AND RowNum <= @EndIndex
                                    ORDER BY ContentName;
                                    SELECT @TotalRows = Count(*) From  
                                      (SELECT ROW_NUMBER() OVER ( ORDER BY Co.Name ) AS RowNum
                                                FROM Content Co
			                                    LEFT JOIN ContentCategories CA ON  Co.CategoryId = CA.Id
                                                {0}
                                            ) AS RowConstrainedResult";

            return getAllPaging;
        }

        public static string GetTopNewPost()
        {
            string getAllPaging = @"SELECT Top(12) *
                                    FROM [Content] C
                                    --WHERE C.ItemType = {0}
                                    Order By CreatedDate Desc";

            return getAllPaging;
        }
    }
}
