using MyRFPSubscriptions.Models;
using MyRFPSubscriptions.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyRFPSubscriptions.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubscriptionDetailsPage : ContentPage
    {
        SubscriptionDetailsVM vm;

        public SubscriptionDetailsPage()
        {
            InitializeComponent();

            vm = Resources["vm"] as SubscriptionDetailsVM;
        }

        public SubscriptionDetailsPage(Subscription selectedSubscription)
        {
            InitializeComponent();

            vm = Resources["vm"] as SubscriptionDetailsVM;
            vm.Subscription = selectedSubscription;
        }
    }
}