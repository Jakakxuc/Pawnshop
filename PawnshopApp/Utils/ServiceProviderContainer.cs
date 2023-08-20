using Microsoft.Extensions.DependencyInjection;
using PawnshopApp.Repository.Interfaces;
using PawnshopApp.Repository;
using PawnshopApp.Services.Interfaces;
using PawnshopApp.Services;
using PawnshopApp.Services.BusinessLogic.Interfaces;
using PawnshopApp.Services.BusinessLogic;
using System.IO;
using Microsoft.Extensions.Configuration;
using System;
using System.Xml.Linq;
using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace PawnshopApp.Utils
{

    public static class ServiceProviderContainer
    {
        private static readonly ServiceProvider _serviceProvider;
        private static readonly string _dbConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config/DbConfig.json");

        static ServiceProviderContainer()
        {
            var services = new ServiceCollection();

            string jsonConfig = File.ReadAllText(_dbConfigPath);
            var jObject = JObject.Parse(jsonConfig);

            string connectionString = jObject["ConnectionStrings"]["MSSQL"].Value<string>();

            services.AddSingleton<ICustomerRepository>(new CustomerRepository(connectionString));
            services.AddSingleton<ILoanRepository>(new LoanRepository(connectionString));
            services.AddSingleton<IPaymentRepository>(new PaymentRepository(connectionString));
            services.AddSingleton<IPawnedItemRepository>(new PawnedItemRepository(connectionString));
            services.AddSingleton<IInsuranceRepository>(new InsuranceRepository(connectionString));
            services.AddSingleton<ISaleRepository>(new SaleRepository(connectionString));
            services.AddSingleton<IUserRepository>(new UserRepository(connectionString));
            services.AddSingleton<IDocumentRepository>(new DocumentRepository(connectionString));
            services.AddSingleton<IDocumentNumberRepository>(new DocumentNumberRepository(connectionString));

            _serviceProvider = services.BuildServiceProvider();

            services.AddSingleton<IUserService>(new UserService());
            services.AddSingleton<ILoanService>(new LoanService());
            services.AddSingleton<IPaymentService>(new PaymentService());
            services.AddSingleton<IPawnedItemService>(new PawnedItemService());
            services.AddSingleton<IInsuranceService>(new InsuranceService());
            services.AddSingleton<ISaleService>(new SaleService());
            services.AddSingleton<ICustomerService>(new CustomerService());
            services.AddSingleton<IDocumentService>(new DocumentService());

            services.AddSingleton<IAuthenticationService>(new AuthenticationService());
            services.AddSingleton<IDocumentNumberService>(new DocumentNumberService());

            _serviceProvider = services.BuildServiceProvider();
        }

        public static T GetService<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
