using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CustomerData.Models;
using CustomerDemo.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDemo.ApiService
{
    public class CustomerDataService
    {
        public async Task<List<Customers>> FetchCustomers()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetStringAsync(AppConstants.CustomersUrl);
                    return JsonConvert.DeserializeObject<List<Customers>>(response);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return null;
                }
            }
        }
        public async Task<List<Order>> FetchOrders()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetStringAsync(AppConstants.OrdersUrl);
                    return JsonConvert.DeserializeObject<List<Order>>(response);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return null;
                }
            }
        }
        public async Task<List<OrderItems>> FetchOrderItems()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetStringAsync(AppConstants.OrderItemsUrl);
                    return JsonConvert.DeserializeObject<List<OrderItems>>(response);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return null;
                }
            }
        }
    }
}