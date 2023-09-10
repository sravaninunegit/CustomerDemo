using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CustomerData.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using Context = Android.Content.Context;

namespace CustomerDemo.CustomerData
{
    
    internal class OfflineTesting
    {
        Context mContext;
        AssetManager assets;
           public OfflineTesting(MainActivity activity)
        {
            mContext = activity;
            assets = mContext.Assets;
        }
       public  void  LoadOffLineData()
        {
            
            var customers = GetCustomers();
            var orders = GetOrders();
            var orderItems =GetOrderItems();
        }
        public  IEnumerable<Customers> GetCustomers()
        {
            try
            {
               
                var reader = new StreamReader(assets.Open("Customers.csv"));
                if (reader!=null)
                {
                    return ReadCsvFile<Customers>(reader);
                }
               
                return null;
                
            }


            catch (Exception ex)
            {

                Console.WriteLine($"Error reading customer CSV: {ex.Message}");
                return null;
            }
        }
        public  IEnumerable<OrderItems> GetOrderItems()
        {
            try
            {
                var reader = new StreamReader(assets.Open("OrderItems.csv"));
                if (reader != null)
                {
                    return ReadCsvFile<OrderItems>(reader);
                }

                return null;

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error reading OrderItems CSV: {ex.Message}");
                return null;
            }
        }
        public  IEnumerable<Order> GetOrders()
        {
            try
            {
                var reader = new StreamReader(assets.Open("Orders.csv"));
                if (reader != null)
                {
                    return ReadCsvFile<Order>(reader);
                }

                return null;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error reading Order CSV: {ex.Message}");
                return null;
            }
        }
        public static List<T> ReadCsvFile<T>(StreamReader reader)
        {
            var records = new List<T>();

            try
            {
               
                    var csvRecords = reader.ReadToEnd().Split("\n").Skip(1).ToList();

                    foreach (var csvRecord in csvRecords)
                    {
                        var values = csvRecord.Split(',');

                        if (values.Length > 0)
                        {
                            var record = Activator.CreateInstance<T>();

                            for (var i = 0; i < values.Length; i++)
                            {
                                var property = typeof(T).GetProperties()[i];
                                var value = values[i];

                                if (property.PropertyType == typeof(DateTime?))
                                {

                                    if (DateTime.TryParse(value, out DateTime parsedDate))
                                    {
                                        property.SetValue(record, parsedDate);
                                    }
                                    else
                                    {
                                        property.SetValue(record, null);
                                    }
                                }
                                else if (property.PropertyType == typeof(int))
                                {

                                    if (int.TryParse(value, out int parsedInt))
                                    {
                                        property.SetValue(record, parsedInt);
                                    }
                                    else
                                    {
                                        property.SetValue(record, 0);
                                    }
                                }
                                else
                                {

                                    property.SetValue(record, Convert.ChangeType(value, property.PropertyType));
                                }
                            }

                            records.Add(record);
                        }
                    }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading CSV: {ex.Message}");

            }

            return records;
        }
    }
}