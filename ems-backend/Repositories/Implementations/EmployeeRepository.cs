using Dapper;
using ems_backend.Data;
using ems_backend.Models;
using ems_backend.Repositories.Interfaces;

namespace ems_backend.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _context;

        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            var query = "SELECT * FROM Employees";

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Employee>(query);
        }

        public async Task<Employee> GetById(int id)
        {
            var query = "SELECT * FROM Employees WHERE Id = @Id";

            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Employee>(query, new { Id = id });
        }

        public async Task<int> Create(Employee employee)
        {
            var query = @"INSERT INTO Employees (FirstName, LastName, Email)
                          VALUES (@FirstName, @LastName, @Email);
                          SELECT CAST(SCOPE_IDENTITY() as int);";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(query, employee);
        }

        public async Task<bool> Update(Employee employee)
        {
            var query = @"UPDATE Employees
                          SET FirstName = @FirstName,
                              LastName = @LastName,
                              Email = @Email
                          WHERE Id = @Id";

            using var connection = _context.CreateConnection();
            var rows = await connection.ExecuteAsync(query, employee);
            return rows > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var query = "DELETE FROM Employees WHERE Id = @Id";

            using var connection = _context.CreateConnection();
            var rows = await connection.ExecuteAsync(query, new { Id = id });
            return rows > 0;
        }
    }
}