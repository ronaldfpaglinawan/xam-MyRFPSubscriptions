using MyRFPSubscriptions.Models;
using MyRFPSubscriptions.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyRFPSubscriptions.ViewModels
{
    public class SubscriptionsVM
    {
        public ObservableCollection<Subscription> Subscriptions { get; set; }
        public SubscriptionsVM()
        {
            Subscriptions = new ObservableCollection<Subscription>();
        }

        public async void ReadSubscriptions()
        {
            var subscriptions = await DatabaseHelper.ReadSubscriptions();

            Subscriptions.Clear();
            foreach(var s in subscriptions)
            {
                Subscriptions.Add(s);
            }
        }
    }
}
