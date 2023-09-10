using CustomerAPi.Utls;
using CustomerData.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace CustomerAPi.DataRepostitories
{
    public class DataService : IDataService
    {
        
        private readonly IConfiguration _configuration;

        public DataService(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        private string GetCsvFilePath(string key)
        {
            return _configuration["CsvFilePaths:" + key];
        }

        public IEnumerable<Customers> GetCustomers()
        {
              try
                {
                var filePath = GetCsvFilePath(AppConstants.CustomerCsv);

                    if (!System.IO.File.Exists(filePath))
                    {
                        return null;
                    }
                   return  AppConstants.ReadCsvFile<Customers>(filePath);
            }
               
            
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error reading customer CSV: {ex.Message}");
                return null; 
            }
        }
        public IEnumerable<OrderItems> GetOrderItems()
        {
            try
            {
                var filePath = GetCsvFilePath(AppConstants.OrderItemsCsv);

                if (!System.IO.File.Exists(filePath))
                {
                    return null;
                }
                return AppConstants.ReadCsvFile<OrderItems>(filePath);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error reading OrderItems CSV: {ex.Message}");
                return null;
            }
        }
        public IEnumerable<Order> GetOrders()
        {
            try
            {
                var filePath = GetCsvFilePath(AppConstants.OrdersCsv);

                if (!System.IO.File.Exists(filePath))
                {
                    return null;
                }
                return AppConstants.ReadCsvFile<Order>(filePath);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error reading Order CSV: {ex.Message}");
                return null;
            }
        }
    }
}

