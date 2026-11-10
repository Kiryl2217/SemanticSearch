using Microsoft.Extensions.Configuration;
using SemanticClassLibrary.Models;

namespace SemanticTest
{
    public static class TestContext
    {
        private static string GetConnectionString()
        {
            // Создаем билдер
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                // Добавляем источник appsettings.json
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                // Добавление переменных среды
                .AddEnvironmentVariables();

            // Строим конфигурацию
            IConfiguration config = builder.Build();

            return config.GetConnectionString("ConnectionString");
        }

        // Выключили параллелизм: GlobalUsings.cs 

        // Паттерн Singleton
        private static ApplicationContext? instance = null;
        private static object obj = new object(); // Объект синхронизации.

        public static ApplicationContext GetApplicationContext()
        {
            if (instance == null)
            {
                lock (obj)
                {
                    if (instance == null)
                    {
                        instance = new ApplicationContext(GetConnectionString());
                    }
                }
            }
            return instance;
        }
    }
}
