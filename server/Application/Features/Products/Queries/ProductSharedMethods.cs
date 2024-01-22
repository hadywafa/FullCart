using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Application.Features.Products.Queries
{
    internal  static class ProductSharedMethods
    {
        public static async Task<string>GetProductCategoriesTreeString(int catId , IConfiguration configuration)
        {
            var connStr = configuration.GetConnectionString("DefaultConnection");

            await using (var conn = new SqlConnection(connStr))
            {
                string query =
                    @"DECLARE @CatId AS int = @parentId
                        ;WITH
                            Category_CTE([id], [name], [parentId], [level] , [url])
                            AS
                            (
                                            SELECT c.Id, c.Name, c.ParentCatId, 0 , CAST(c.Code AS varchar(max))
                                    from [dbo].[Categories] c
                                    where c.ParentCatId is Null

                                UNION  ALL

                                    SELECT c.Id, c.Name, c.ParentCatId , x.[level]+1 , CAST(x.[url]+','+c.Code  AS varchar(max))
                                    from [dbo].[Categories] c , Category_CTE x
                                    WHERE c.ParentCatId = x.[id]
                            )
                        SELECT url
                        FROM Category_CTE c
                        where [url]  NOT like '%,' AND c.id = @CatId ;";
                string result;
                try
                {

                     result = await  conn.QueryFirstOrDefaultAsync<string>(query, new { parentId = catId } );

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    result = e.ToString();
                    throw;
                }

                return result;
            }
        }

    }
}
