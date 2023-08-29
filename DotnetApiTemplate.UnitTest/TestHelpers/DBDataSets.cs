namespace DotnetApiTemplate.UnitTest.TestHelpers
{
    public static class DBDataSets
    {
        public static List<Employee> GetEmployeeTableTestData()
        {
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee { EmployeeId = 1, UserId = "CORP\\e999999", FirstName = "TestOne", LastName = "One", Email = "pp@g.com", CreatedBy = DateTime.Now.ToString() });
            employees.Add(new Employee { EmployeeId = 2, UserId = "CORP\\e777777", FirstName = "TestTwo", LastName = "Two", Email = "gg@g.com", CreatedBy = DateTime.Now.ToString() });
            employees.Add(new Employee { EmployeeId = 3, UserId = "CORP\\e666666", FirstName = "TestThree", LastName = "Three", Email = "kk@g.com", CreatedBy = DateTime.Now.ToString() });

            return employees;
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(GetEmployeeTableTestData());
        }
    }
}
