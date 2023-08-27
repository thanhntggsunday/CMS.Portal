using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearning.DAL.SQL
{
    public class ContentCategoryScript
    {
        public const string GET_ALL_COMMAND = @"SELECT *
                                                  FROM [dbo].[ContentCategories]";

        public static string GetQueryAllPagingCommand()
        {
            string getAllPaging = @"SELECT  *
                            FROM    ( SELECT ROW_NUMBER() OVER ( ORDER BY Name ) AS RowNum, *
                                      FROM ContentCategories
                                      {0}
                                    ) AS RowConstrainedResult
                            WHERE   RowNum >= @StartIndex
                                    AND RowNum <= @EndIndex
                            ORDER BY Name;
                            SELECT  @TotalRows = Count(*) From ContentCategories {0};";

            return getAllPaging;
        }
    }
}
