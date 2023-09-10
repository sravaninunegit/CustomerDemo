using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Android.Net;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Newtonsoft.Json;
using static Android.Content.ClipData;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CustomerDemo.Utils;
using CustomerDemo.CustomerData;
using System.Linq;
using CustomerData.Models;
using Android.Content.Res;
using Java.Nio.Channels;
using static Android.App.DownloadManager;
using static Android.Graphics.Paint;
using Android.Hardware;
using CustomerDemo.ApiService;

namespace CustomerDemo
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button fetchButton, applydiscount;
        private RecyclerView recyclerView;
        private CustomerAdapter itemAdapter;       
        List<Dictionary<string, string>> dataList;
        ProgressBar progressBar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            InitViews();

           
        }

        private void InitViews()
        {
            fetchButton = FindViewById<Button>(Resource.Id.fetchButton);
            applydiscount = FindViewById<Button>(Resource.Id.applydiscount);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            recyclerView = FindViewById<RecyclerView>(Resource.Id.customerView);
            applydiscount.Click += ApplyDiscount;

            fetchButton.Click += async (sender, e) =>
            {
                if (IsConnected())
                {
                    progressBar.Visibility = ViewStates.Visible;
                    CustomerDataService apiService = new CustomerDataService();
                    List<Customers> customers = await apiService.FetchCustomers();
                    List<Order> orders = await apiService.FetchOrders();
                    List<OrderItems> ordersItems = await apiService.FetchOrderItems();

                    if (customers != null && orders != null && ordersItems != null)
                    {
                        progressBar.Visibility = ViewStates.Gone;
                        PopulateData(customers, orders, ordersItems);
                    }
                    else
                    {
                        progressBar.Visibility = ViewStates.Gone;
                        ShowSnackbar("Error fetching data.");
                    }
                }
                else
                {
                    progressBar.Visibility = ViewStates.Gone;
                    ShowSnackbar("No internet connection. Please check and try again");
                }




            };
        }

        private void ApplyDiscount(object sender, EventArgs e)
        {
            foreach (var customer in dataList)
            {
                if (customer.ContainsKey("State") && customer["State"] == "CA")
                {
                  
                    if (customer.ContainsKey("Listprice"))
                    {
                        if (double.TryParse(customer["Listprice"], out double currentDiscount))
                        {
                            
                            double newDiscount = currentDiscount * 0.95;                            
                            customer["Listprice"] = newDiscount.ToString();

                            
                        }
                    }
                }
            }

            if (itemAdapter!=null)
            {
                itemAdapter.NotifyDataSetChanged();
            }
            
        }

        private void PopulateData(List<Customers> customers, List<Order> orders, List<OrderItems> orderItems)
        {
            try
            {
                if (customers.Count>0 && orders.Count>0 && orderItems.Count>0)
                {
                    var joinedData = (from customer in customers
                                      join order in orders on customer.CustomerId equals order.CustomerId
                                      join orderItem in orderItems on order.OrderId equals orderItem.OrderId
                                      select new
                                      {
                                          FirstName = customer.FirstName,
                                          LastName = customer.LastName,
                                          State = customer.State,
                                          OrderStatus = order.OrderStatus,
                                          OrderDate = order.OrderDate,
                                          RequiredDate = order.RequiredDate,
                                          ShippedDate = order.ShippedDate,
                                          Listprice = orderItem.Listprice,
                                          Discount = orderItem.Discount

                                      }).GroupBy(data => new { data.FirstName, data.LastName })
                     .Select(group => group.First()).ToList();

                    dataList = new List<Dictionary<string, string>>();

                    foreach (var data in joinedData)
                    {
                        Dictionary<string, string> row = new Dictionary<string, string>();



                        row["FirstName"] = data?.FirstName ?? "";
                        row["LastName"] = data?.LastName ?? "";
                        row["State"] = data?.State ?? "";
                        row["OrderStatus"] = data?.OrderStatus ?? "";
                        row["OrderDate"] = data?.OrderDate ?? "";
                        row["RequiredDate"] = data?.RequiredDate ?? "";
                        row["ShippedDate"] = data?.ShippedDate ?? "";
                        row["Listprice"] = data?.Listprice.ToString() ?? "";
                        row["Discount"] = data?.Discount.ToString() ?? "";
                        dataList.Add(row);
                    }

                    itemAdapter = new CustomerAdapter(dataList);
                    LinearLayoutManager layoutManager = new LinearLayoutManager(this);
                    recyclerView.SetLayoutManager(layoutManager);
                    recyclerView.SetAdapter(itemAdapter);
                    itemAdapter.NotifyDataSetChanged();
                }

                
            }


            catch (Exception ex)
            {

                throw;
            }
            
           



        }
       
        private bool IsConnected()
        {
            var connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
            var networkInfo = connectivityManager.ActiveNetworkInfo;
            return networkInfo != null && networkInfo.IsConnectedOrConnecting;
        }

        private void ShowSnackbar(string message)
        {
            Snackbar.Make(recyclerView, message, Snackbar.LengthLong).Show();
        }
    }
}