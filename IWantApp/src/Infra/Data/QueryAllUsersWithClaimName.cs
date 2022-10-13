using Dapper;
using IWantApp.Endpoints.Employees;
using Microsoft.Data.SqlClient;

namespace IWantApp.Infra.Data;

public class QueryAllUsersWithClaimName
{
    private readonly IConfiguration _configuration;

    public QueryAllUsersWithClaimName(IConfiguration configuration)
        => (_configuration) = (configuration);

    public IEnumerable<EmployeeResponse> Execute(int page, int rows)
    {
        var sqlConnection = new SqlConnection(_configuration["Database:IWantApp"]);

        var query = @"SELECT 
                         U.Email,
                         C.ClaimValue AS Name
                    FROM AspNetUsers U
	                       LEFT JOIN AspNetUserClaims C ON C.UserId = U.Id AND C.ClaimType = 'Name'
                    ORDER BY Name
                    OFFSET (@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

        var employees = sqlConnection.Query<EmployeeResponse>(query, new { page, rows }).ToList();
        return employees;
    }
}
