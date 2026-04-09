using ems_backend.DTOs;
using ems_backend.Models;
using ems_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ems_backend.Controllers
{
    /// <summary>
    /// API endpoints for managing employees
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeesController"/> class
        /// </summary>
        /// <param name="service">Employee service dependency</param>
        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves all employees
        /// </summary>
        /// <returns>List of all employees</returns>
        /// <response code="200">Returns the list of employees</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _service.GetAll();
            return Ok(employees);
        }

        /// <summary>
        /// Retrieves an employee by ID
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>Employee details</returns>
        /// <response code="200">Returns the employee</response>
        /// <response code="404">Employee not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _service.GetById(id);

            if (employee == null)
                return NotFound($"Employee with ID {id} not found.");

            return Ok(employee);
        }

        /// <summary>
        /// Creates a new employee
        /// </summary>
        /// <param name="employee">Employee object to create</param>
        /// <returns>The created employee</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/employees
        ///     {
        ///         "firstName": "Mayank",
        ///         "lastName": "Srivastava",
        ///         "email": "mayank@test.com"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Employee created successfully</response>
        /// <response code="400">Invalid input data</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] EmployeeCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };

            var result = await _service.Create(employee);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates an existing employee
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <param name="employee">Updated employee object</param>
        /// <returns>No content if update is successful</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/employees/1
        ///     {
        ///         "id": 1,
        ///         "firstName": "Updated",
        ///         "lastName": "Name",
        ///         "email": "updated@test.com"
        ///     }
        ///
        /// </remarks>
        /// <response code="204">Employee updated successfully</response>
        /// <response code="400">Invalid input or ID mismatch</response>
        /// <response code="404">Employee not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeUpdateDto dto)
        {
            if (!ModelState.IsValid || id != dto.Id)
                return BadRequest();

            var employee = new Employee
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };

            var updated = await _service.Update(id, employee);

            if (!updated)
                return NotFound($"Employee with ID {id} not found.");

            return NoContent();
        }

        /// <summary>
        /// Deletes an employee by ID
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>No content if deletion is successful</returns>
        /// <response code="204">Employee deleted successfully</response>
        /// <response code="404">Employee not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.Delete(id);

            if (!deleted)
                return NotFound($"Employee with ID {id} not found.");

            return NoContent();
        }
    }
}