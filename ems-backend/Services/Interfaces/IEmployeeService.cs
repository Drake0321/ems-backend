using ems_backend.DTOs;
using ems_backend.Models;

namespace ems_backend.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeResponseDto>> GetAll();
        Task<Employee> GetById(int id);
        Task<Employee> Create(Employee employee);
        Task<bool> Update(int id, Employee employee);
        Task<bool> Delete(int id);
    }
}