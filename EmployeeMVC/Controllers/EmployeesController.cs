using EmployeeMVC.Data;
using EmployeeMVC.Models;
using EmployeeMVC.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public EmployeesController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await applicationDbContext.Employees.ToListAsync();
            return View(employees);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

         [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel model)
        {

            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Email = model.Email,
                Salary = model.Salary,
                Department = model.Department,
                DateOfBirth = model.DateOfBirth
            };

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await applicationDbContext.Employees.AddAsync(employee);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        /*
                [HttpPost]
                public async Task<IActionResult> Create(CreateEmployeeViewModel createEmployeeRequest)
                {
                    var employee = new Employee()
                    {
                        Id = Guid.NewGuid(),
                        Name = createEmployeeRequest.Name,
                        Email = createEmployeeRequest.Email,
                        Salary = createEmployeeRequest.Salary,
                        Department = createEmployeeRequest.Department,
                        DateOfBirth = createEmployeeRequest.DateOfBirth
                    };

                    await applicationDbContext.Employees.AddAsync(employee);
                    await applicationDbContext.SaveChangesAsync();

                    return RedirectToAction("Index");
                }


                [HttpGet]
                public IActionResult View(Guid id)
                {
                    var employee =  applicationDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

                    return View(employee);
                }*/

    

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
             var employee = await applicationDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
             if (employee != null)
             {
                var viewModel = new UpdateEmployeeViewModel()
                {
                            Id = employee.Id,
                            Name = employee.Name,
                            Email = employee.Email,
                            Salary = employee.Salary,
                            Department = employee.Department,
                            DateOfBirth = employee.DateOfBirth                
                };   
                  return await Task.Run(()=>View("View",viewModel));
             }
                  return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await applicationDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.Department = model.Department;
                employee.DateOfBirth = model.DateOfBirth;

                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await applicationDbContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                applicationDbContext.Employees.Remove(employee);
                await applicationDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}