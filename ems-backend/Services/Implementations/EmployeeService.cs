using ems_backend.DTOs;
using ems_backend.Models;
using ems_backend.Repositories.Interfaces;
using ems_backend.Services.Interfaces;

namespace ems_backend.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<EmployeeResponseDto>> GetAll()
        {
            var employees = await _repo.GetAll();

            return employees.Select(e => new EmployeeResponseDto
            {
                Id = e.Id,
                FullName = $"{e.FirstName} {e.LastName}",
                Email = e.Email
            });
        }

        /// <inheritdoc/>
        public async Task<Employee> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        /// <inheritdoc/>
        public async Task<Employee> Create(Employee employee)
        {
            var id = await _repo.Create(employee);
            employee.Id = id;
            return employee;
        }

        /// <inheritdoc/>
        public async Task<bool> Update(int id, Employee employee)
        {
            if (id != employee.Id)
                return false;

            return await _repo.Update(employee);
        }


        /// <inheritdoc/>
        public async Task<bool> Delete(int id)
        {
            return await _repo.Delete(id);
        }
    }
}