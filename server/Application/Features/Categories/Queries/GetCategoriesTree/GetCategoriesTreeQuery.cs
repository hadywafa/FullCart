using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Contracts;
using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Application.Features.Categories.Queries.GetCategoriesTree
{
    public class GetCategoriesTreeQuery : IRequest<StringBuilder>
    {
        public int ParentId { get; set; }
    }

    public class GetCategoriesTreeQueryHandler : IRequestHandler<GetCategoriesTreeQuery, StringBuilder>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public GetCategoriesTreeQueryHandler(IApplicationDbContext context , IMapper mapper , IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<StringBuilder> Handle(GetCategoriesTreeQuery request, CancellationToken cancellationToken)
        {

            var connStr = _configuration.GetConnectionString("DefaultConnection");
            await CreateSqlFunction(connStr);
            await CreateSqlSp(connStr);
            var queryWithForJson = "exec [dbo].[spGetJson]";
            await using (var conn = new SqlConnection(connStr))
            {
                await using (var cmd = new SqlCommand(queryWithForJson, conn))
                {
                    await conn.OpenAsync(cancellationToken);
                    var result = new StringBuilder();
                    var reader = await cmd.ExecuteReaderAsync(cancellationToken);
                    if (!reader.HasRows)
                    {
                        result.Append("[]");
                    }
                    else
                    {
                        while (await reader.ReadAsync(cancellationToken))
                        {
                            result.Append(reader.GetValue(0).ToString());
                        }
                    }
                    return result;
                }
            }
        }

        private async Task CreateSqlFunction(string connectionString)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the command and set its properties.
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = $@" CREATE OR ALTER FUNCTION dbo.GetJson (@parentId int)
                            RETURNS nvarchar(max)
                            AS BEGIN
                                RETURN (
                                    SELECT
                                    [Id] as id,
                                    [Code] as code,
                                    [Name] as name,
                                    [NameArabic] as nameAr,
                                    [ParentCatId] as parentId,
                                    children = JSON_QUERY(dbo.GetJson([Id]))
                                FROM [dbo].[Categories]
                                WHERE EXISTS ( SELECT [ParentCatId]
                                INTERSECT
                                    SELECT @parentId)
                                FOR JSON PATH
                                );
                            END ;";
                command.CommandType = CommandType.Text;

                // Open the connection and execute the reader.
                connection.Open();
                await using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0}: {1:C}", reader[0], reader[1]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    await reader.CloseAsync();
                }
            }
        }

        private async Task CreateSqlSp(string connectionString)
        {
            await using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the command and set its properties.
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"CREATE OR ALTER PROCEDURE [dbo].[spGetJson]
                AS
                    begin
                SELECT dbo.GetJson(NULL);
                end ";
                command.CommandType = CommandType.Text;
                //command.CommandType = CommandType.StoredProcedure;

                // Add the input parameter and set its properties.
                //SqlParameter parameter = new SqlParameter();
                //parameter.ParameterName = "@CategoryName";
                //parameter.SqlDbType = SqlDbType.NVarChar;
                //parameter.Direction = ParameterDirection.Input;
                //parameter.Value = categoryName;

                // Add the parameter to the Parameters collection.
                //command.Parameters.Add(parameter);

                // Open the connection and execute the reader.
                connection.Open();
                await using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0}: {1:C}", reader[0], reader[1]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    await reader.CloseAsync();
                }
            }
        }

        
        #region Dapper Can't Work With Function | Stored Procedure | CTE

        private async Task DapperCreateFunction(int parentId)
        {
            var connStr = _configuration.GetConnectionString("DefaultConnection");

            var query = @" CREATE OR ALTER FUNCTION dbo.GetJson (@parentId int)
                            RETURNS nvarchar(max)
                            AS BEGIN
                                RETURN (
                                    SELECT
                                    [Id] as id,
                                    [Code] as code,
                                    [Name] as name,
                                    [NameArabic] as nameAr,
                                    [ParentCatId] as parentId,
                                    children = JSON_QUERY(dbo.GetJson([Id]))
                                FROM [dbo].[Categories]
                                WHERE EXISTS ( SELECT [ParentCatId]
                                INTERSECT
                                    SELECT @parentId)
                                FOR JSON PATH
                                );
                            END; ";
            await using (var conn = new SqlConnection(connStr))
            {
                try
                {
                    await conn.ExecuteAsync(query, new { parentId = parentId } , commandType:CommandType.Text );

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        private async Task DapperCreateStoredProcedure()
        {
            var connStr = _configuration.GetConnectionString("DefaultConnection");

            var query = @" CREATE OR ALTER PROCEDURE [dbo].[spGetJson]
                        AS
                        BEGIN
                            SELECT dbo.GetJson(NULL);
                        END ; ";
            await using (var conn = new SqlConnection(connStr))
            {
                try
                {
                    await conn.ExecuteAsync(query , commandType: CommandType.StoredProcedure );

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        private async Task<string> DapperCategoriesTree()
        {
            var connStr = _configuration.GetConnectionString("DefaultConnection");

            //await DapperCreateFunction(request.ParentId);
            //await DapperCreateStoredProcedure();
            await CreateSqlFunction(connStr);
            await CreateSqlSp(connStr);

            var query = @"EXECUTE [dbo].[spGetJson]";
            await using (var conn = new SqlConnection(connStr))
            {
                // create string Builder
                string result;
                try
                {
                    //result = await conn.QueryFirstOrDefaultAsync<string>(query, new { parentId = request.ParentId }, commandType: CommandType.Text);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                //return result;
                return "result";
            }
        }

        #endregion

    }
}
