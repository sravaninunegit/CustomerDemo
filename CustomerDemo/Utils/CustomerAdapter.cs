using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Content.ClipData;

namespace CustomerDemo.Utils
{
    internal class CustomerAdapter : RecyclerView.Adapter
    {
        List<Dictionary<string, string>> customerDictionaryList;

        public CustomerAdapter(List<Dictionary<string, string>> data)
        {
            customerDictionaryList = data;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.customeritem, parent, false);
            TabViewHolder viewHolder = new TabViewHolder(itemView);
            return viewHolder;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is TabViewHolder viewHolder)
            {
                var item = customerDictionaryList[position];
                viewHolder.firstName.Text ="Name:"+ item["FirstName"] + " " + item["LastName"];
                viewHolder.OrderDate.Text = "OrderDate:" + item["OrderDate"];
                viewHolder.OrderStatus.Text = "OrderStatus:" + item["OrderStatus"];
                viewHolder.RequiredDate.Text = "RequiredDate:" + item["RequiredDate"];
                viewHolder.ShippedDate.Text = "ShippedDate:" + item["ShippedDate"];
                viewHolder.Listprice.Text = "Listprice:" + item["Listprice"];
                viewHolder.Discount.Text = "Discount:" + item["Discount"];
                viewHolder.State.Text = "State:" + item["State"];
            }


            
        }

        public override int ItemCount => customerDictionaryList.Count;

        private class TabViewHolder : RecyclerView.ViewHolder
        {
            public TextView firstName { get; set; }         
            public TextView RequiredDate { get; set; }         
            public TextView OrderDate { get; set; }
            public TextView OrderStatus { get; set; }
            public TextView ShippedDate { get; set; }
            public TextView Discount { get; set; }
            public TextView Listprice { get; set; }
            public TextView State { get; set; }
          
          

            public TabViewHolder(View itemView) : base(itemView)
            {
                firstName = itemView.FindViewById<TextView>(Resource.Id.firstName);
                RequiredDate = itemView.FindViewById<TextView>(Resource.Id.RequiredDate);
                OrderDate = itemView.FindViewById<TextView>(Resource.Id.OrderDate);
                OrderStatus = itemView.FindViewById<TextView>(Resource.Id.OrderStatus);
                ShippedDate = itemView.FindViewById<TextView>(Resource.Id.ShippedDate);
                Listprice = itemView.FindViewById<TextView>(Resource.Id.Listprice);
                Discount = itemView.FindViewById<TextView>(Resource.Id.Discount);
                State = itemView.FindViewById<TextView>(Resource.Id.State);
               
                // Add more TextViews for additional columns if needed
            }
        }
    }
}