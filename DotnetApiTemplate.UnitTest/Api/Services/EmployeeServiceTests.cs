namespace DotnetApiTemplate.UnitTest.Api.Services
{
    public class EmployeeService_GET_Tests
    {
        [Fact]
        public async void Get_WhenCalled_ReturnsAllEmployees()
        {
            Mock<IRepository> repositoryMock = new Mock<IRepository>();
            Mock<ILogger<EmployeeService>> loggerMock = new Mock<ILogger<EmployeeService>>();
            Mock<IConfiguration> iConfigMock = new Mock<IConfiguration>();
            Mock<IMapper> iMapperMock = new Mock<IMapper>();
            Mock<IHttpContextAccessor> iHttpContextAccessorMock = new Mock<IHttpContextAccessor>();

            Mock<DbSet<Employee>> mockSet = new Mock<DbSet<Employee>>();
            mockSet.ConfigureMock(DBDataSets.GetEmployeeTableTestData());

            repositoryMock.Setup(r => r.GetList<Employee>()).Returns(() => mockSet.Object);

            iMapperMock.Setup(m => m.Map<List<EmployeeReadDto>>(It.IsAny<List<Employee>>())).Returns(() =>
            {
                List<EmployeeReadDto> returnVal = new List<EmployeeReadDto>();
                returnVal.Add(new EmployeeReadDto { EmployeeId = 1, UserId = "CORP\\e999999", FirstName = "TestOne", LastName = "One", Email = "pp@g.com" });
                returnVal.Add(new EmployeeReadDto { EmployeeId = 2, UserId = "CORP\\e777777", FirstName = "TestTwo", LastName = "Two", Email = "gg@g.com" });
                returnVal.Add(new EmployeeReadDto { EmployeeId = 3, UserId = "CORP\\e666666", FirstName = "TestThree", LastName = "Three", Email = "kk@g.com" });

                return returnVal;
            });

            EmployeeService empService = new EmployeeService(repositoryMock.Object
                                                            ,loggerMock.Object
                                                            ,iConfigMock.Object
                                                            ,iMapperMock.Object
                                                            ,iHttpContextAccessorMock.Object);

            var result = await empService.Get();

            Assert.NotNull(result);
            Assert.True(result.Count == 3);

        }
    }
}
