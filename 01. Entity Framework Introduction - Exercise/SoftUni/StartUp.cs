using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();

            string result = IncreaseSalaries(context);
            Console.WriteLine(result);
        }
        //03.
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .OrderBy(e => e.EmployeeId)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary
                });


            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} " +
                    $"{employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
            }


            return sb.ToString().TrimEnd();
        }

        //04.
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .Where(e => e.Salary > 50000)
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary
                })
                .OrderBy(e => e.FirstName)
                .ToList();

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.FirstName} - {emp.Salary:f2}");
            }

            return sb.ToString().TrimEnd();

        }

        //05.
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Department,
                    e.Salary
                })
                .Where(e => e.Department.Name == "Research and Development")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .ToList();

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.FirstName} {emp.LastName} from {emp.Department.Name} - ${emp.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        //6.
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {

            StringBuilder sb = new StringBuilder();

            Address addressToAdd = new Address
            {
                AddressText = "Vitoshka 25",
                TownId = 4
            };

            context.Add(addressToAdd);

            Employee employeeNakov = context
                .Employees
                .FirstOrDefault(e => e.LastName == "Nakov");

            employeeNakov.Address = addressToAdd;

            context.SaveChanges();

            var employees = context
                .Employees
                .OrderByDescending(e => e.AddressId)
                .Select(e => e.Address.AddressText)
                .Take(10)
                .ToList();

            foreach (var emp in employees)
            {
                sb.AppendLine(emp);
            }

            return sb.ToString().TrimEnd();
        }

        //07.
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {

            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .Where(ep => ep.EmployeesProjects
                            .Any(p => p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003))
                .Select(e => new
                {
                    employeeFirstName = e.FirstName,
                    employeesLastName = e.LastName,
                    managerFirstName = e.Manager.FirstName,
                    managerLastName = e.Manager.LastName,
                    project = e.EmployeesProjects.Select(p => new
                    {
                        p.Project.Name,
                        p.Project.StartDate,
                        p.Project.EndDate
                    }).ToList()
                })
                .Take(10)
                .ToList();


            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.employeeFirstName} {emp.employeesLastName} " +
                    $"- Manager: {emp.managerFirstName} {emp.managerLastName}");

                foreach (var proj in emp.project)
                {
                    if (proj.EndDate == null)
                    {
                        sb.AppendLine($"--{proj.Name} - {proj.StartDate} - not finished");
                    }
                    else
                    {
                        sb.AppendLine($"--{proj.Name} - {proj.StartDate} - {proj.EndDate}");
                    }
                }
            }

            return sb.ToString().TrimEnd();
        }


        //08.
        public static string GetAddressesByTown(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var addresses = context
                .Addresses
                .Select(a => new
                {
                    addressText = a.AddressText,
                    townName = a.Town.Name,
                    employeesCount = a.Employees.Count
                })
                .OrderByDescending(a => a.employeesCount)
                .ThenBy(a => a.townName)
                .ThenBy(a => a.addressText)
                .Take(10)
                .ToList();

            foreach (var address in addresses)
            {
                sb.AppendLine($"{address.addressText}, {address.townName} - {address.employeesCount} employees");
            }

            return sb.ToString().TrimEnd();
        }

        //09.
        public static string GetEmployee147(SoftUniContext context)
        {

            var employee = context
                .Employees
                .Select(e => new
                {
                    EmployeeId = e.AddressId,
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    employeesProjects = e.EmployeesProjects
                                            .OrderBy(ep => ep.Project.Name)
                                            .Select(ep => ep.Project.Name)
                                            .ToList()
                })
                .FirstOrDefault(e => e.EmployeeId == 147);

            return $"{employee.FirstName} {employee.LastName} - {employee.JobTitle}" +
                $"\n{string.Join(Environment.NewLine, employee.employeesProjects)}";
        }

        //10.
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departments = context
                .Departments
                .Select(d => new
                {
                    departmentName = d.Name,
                    managerFirstName = d.Manager.FirstName,
                    managerLastName = d.Manager.LastName,
                    emplyees = d.Employees.Select(e => new
                    {
                        employeeFirstName = e.FirstName,
                        employeeLastName = e.LastName,
                        employeeJobTitle = e.JobTitle
                    })
                   .OrderBy(e => e.employeeFirstName)
                   .ThenBy(e => e.employeeLastName)
                   .ToList()
                })
                .Where(d => d.emplyees.Count > 5)
                .OrderBy(d => d.emplyees.Count)
                .ThenBy(d => d.departmentName)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var dept in departments)
            {
                sb.AppendLine($"{dept.departmentName} - {dept.managerFirstName} {dept.managerLastName}");

                foreach (var emp in dept.emplyees)
                {
                    sb.AppendLine($"{emp.employeeFirstName} {emp.employeeLastName} - {emp.employeeJobTitle}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        //11.
        public static string GetLatestProjects(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var latestProjects = context
                .Projects
                .Select(p => new
                {
                    projectName = p.Name,
                    projectDesc = p.Description,
                    projectStartDate = p.StartDate
                })
                .OrderBy(p => p.projectName)
                .Take(10)
                .ToList();

            foreach (var project in latestProjects)
            {
                sb.AppendLine(project.projectName);
                sb.AppendLine(project.projectDesc);

                string formattedStartDate = project.projectStartDate
                    .ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                sb.AppendLine(formattedStartDate);
            }

            return sb.ToString();
        }

        //12.
        public static string IncreaseSalaries(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .Where(e => e.Department.Name == "Engineering" ||
                e.Department.Name == "Tool Design" ||
                e.Department.Name == "Marketing" ||
                e.Department.Name == "Information Services");

            foreach (var emp in employees)
            {
                emp.Salary *= 1.12m;
            }

            context.SaveChanges();
            
            var employeesToPrint = employees
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.Salary
            })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            foreach (var emp in employeesToPrint)
            {
                sb.AppendLine($"{emp.FirstName} {emp.LastName} (${emp.Salary:f2})");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
