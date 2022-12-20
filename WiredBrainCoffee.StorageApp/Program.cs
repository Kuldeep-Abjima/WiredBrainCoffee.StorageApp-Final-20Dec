using System;
using WiredBrainCoffee.StorageApp.Data;
using WiredBrainCoffee.StorageApp.Entities;
using WiredBrainCoffee.StorageApp.Repositories;

namespace WiredBrainCoffee.StorageApp
{
  class Program
  {
    static void Main(string[] args)
    {
      ItemAdded<Employee> itemAdded = new ItemAdded<Employee>(EmployeeAdded);
      var employeeRepository = new SqlRepository<Employee>(new StorageAppDbContext());
            employeeRepository.itemAddedCallBack += EmployeeAdded;


      ItemAdded<Manager> mangerAdded = itemAdded;
            
      AddEmployees(employeeRepository);
      AddManagers(employeeRepository);
      GetEmployeeById(employeeRepository);
      WriteAllToConsole(employeeRepository);

      var organizationRepository = new ListRepository<Organization>();
      AddOrganizations(organizationRepository);
      WriteAllToConsole(organizationRepository);      

      Console.ReadLine();
    }

        private static void EmployeeAdded(Employee employee)
        {
            Console.WriteLine($"Employee added => {employee.FirstName}");

        }

        private static void AddManagers(IWriteRepository<Manager> managerRepository)
    {
            var SaraManger = new Manager { FirstName = "Sara" };
            var SaraMangerCopy = SaraManger.Copy();
            managerRepository.Add(SaraManger);
            if(SaraMangerCopy is not null)
            {
                SaraMangerCopy.FirstName += "Copy";
                managerRepository.Add(SaraMangerCopy);
            }
      managerRepository.Add(new Manager { FirstName = "Henry" });
      managerRepository.Save();
    }

    private static void WriteAllToConsole(IReadRepository<IEntity> repository)
    {
      var items = repository.GetAll();
      foreach (var item in items)
      {
        Console.WriteLine(item);
      }
    }

    private static void GetEmployeeById(IRepository<Employee> employeeRepository)
    {
      var employee = employeeRepository.GetById(2);
      Console.WriteLine($"Employee with Id 2: {employee.FirstName}");
    }

    private static void AddEmployees(IRepository<Employee> employeeRepository)
    {
            var employees = new[] {
                        new Employee { FirstName = "Julia" },
                        new Employee { FirstName = "Anna" },
                        new Employee { FirstName = "Thomas" }
            };
            employeeRepository.AddBatch(employees);
            //RepositoryExtension.AddBatch(employeeRepository, employees);
           employeeRepository.Save();
    }

    private static void AddOrganizations(IRepository<Organization> organizationRepository)
    {
            var organizations = new[] { new Organization { Name = "Pluralsight" }, new Organization { Name = "Globomantics" } };
            organizationRepository.AddBatch(organizations);
            //RepositoryExtension.AddBatch(organizationRepository, organizations);
            
    }

     
    }
}
