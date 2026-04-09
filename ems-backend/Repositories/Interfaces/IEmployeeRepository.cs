using ems_backend.Models;

namespace ems_backend.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> GetById(int id);
        Task<int> Create(Employee employee);
        Task<bool> Update(Employee employee);
        Task<bool> Delete(int id);
    }
}