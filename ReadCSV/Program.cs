using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadCSV.Requests;
using ReadCSV.Services;
using System;
using System.IO;

namespace ReadCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddScoped<ICSVFileService,CSVFileService>()                       
                .AddSingleton<ServiceManager>()    
                .AddOptions()
                .Configure<AppSettings>(configuration.GetSection("CSVFileIndex"))
                .BuildServiceProvider();
            
            ReadAndDisplayCSV(configuration, serviceProvider);            
        }

        /// <summary>
        /// Display returned results into the Console
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="serviceProvider"></param>
        private static void ReadAndDisplayCSV(IConfigurationRoot configuration, ServiceProvider serviceProvider)
        {
            string folderPath = configuration["ReadCSV.FolderPath"];
            string percentageStr = configuration["ReadCSV.Percentage"];
            int percentage = percentageStr.TryGetInt();
            
            ReadCSVFilesRequest request = new ReadCSVFilesRequest
            {
                FolderPath = folderPath,
                Percentage = percentage
            };

            var serviceManager = serviceProvider.GetService<ServiceManager>();

            var response = serviceManager.DisplayRecords(request);

            if(response.Records != null)
            {
                foreach (var record in response.Records)
                {
                    Console.WriteLine(record);
                }
            }            
        }
    }
}
