using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerDemo.Utils
{
    internal class AppConstants
    {
        public static String CustomersUrl = "https://customerdemoservice.azurewebsites.net/Customer/customers";
        public static String OrdersUrl = "https://customerdemoservice.azurewebsites.net/Customer/orders";
        public static String OrderItemsUrl = "https://customerdemoservice.azurewebsites.net/Customer/orderitems";
        
    }
}