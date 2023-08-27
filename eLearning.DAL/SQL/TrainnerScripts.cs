using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearning.DAL.SQL
{
    public class TrainnerScripts
    {
        public const string GET_ALL_COMMAND = @"SELECT  [Id]
                                                  ,[Name]
                                                  ,[Avatar]
                                                  ,[Bio]
                                                  ,[CreatedDate]
                                                  ,[ModifiedDate]
                                                  ,[CreatedBy]
                                                  ,[ModifiedBy]
                                              FROM [Trainners];";

        public static string GetQueryAllPagingCommand()
        {
            string getAllPaging = @"SELECT  *
                            FROM    (
                                        SELECT ROW_NUMBER() OVER ( ORDER BY T.Name ) AS RowNum, 
                                                   [Id]
                                                  ,[Name]
                                                  ,[Avatar]
                                                  ,[Bio]
                                                  ,[CreatedDate]
                                                  ,[ModifiedDate]
                                                  ,[CreatedBy]
                                                  ,[ModifiedBy]
                                        FROM [Trainners] AS T                                       
                                        {0}
                                    ) AS RowConstrainedResult
                            WHERE   RowNum >= @StartIndex
                                    AND RowNum <= @EndIndex
                            ORDER BY Name;

                            SELECT  @TotalRows = Count(*) From (                                       
                                        SELECT ROW_NUMBER() OVER ( ORDER BY T.Name ) AS RowNum, 
                                                   [Id]
                                                  ,[Name]
                                                  ,[Avatar]
                                                  ,[Bio]
                                                  ,[CreatedDate]
                                                  ,[ModifiedDate]
                                                  ,[CreatedBy]
                                                  ,[ModifiedBy]
                                        FROM [Trainners] AS T     
                                      {0}
                                    ) AS RowConstrainedResult;";

            return getAllPaging;
        }
    }
}
