namespace dapper_unitofwork.Entities
{
  using System;
  using System.Collections.Generic;

  public class Employee
  {
    public ICollection<EmployeeTerritory> EmployeeTerritories { get; private set; }
    public ICollection<Employee> DirectReports { get; private set; }
    public ICollection<Order> Orders { get; private set; }
    public Employee Manager { get; set; }
    
    public Employee()
    {
      EmployeeTerritories = new HashSet<EmployeeTerritory>();
      DirectReports = new HashSet<Employee>();
      Orders = new HashSet<Order>();
    }

    public int EmployeeId { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Title { get; set; }
    public string TitleOfCourtesy { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime? HireDate { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string HomePhone { get; set; }
    public string Extension { get; set; }
    public byte[] Photo { get; set; }
    public string Notes { get; set; }
    public int? ReportsTo { get; set; }
    public string PhotoPath { get; set; }

  }
}