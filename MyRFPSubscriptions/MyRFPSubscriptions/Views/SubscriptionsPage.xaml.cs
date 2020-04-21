using MyRFPSubscriptions.ViewModels;
using MyRFPSubscriptions.ViewModels.Helpers;
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
    public partial class SubscriptionsPage : ContentPage
    {
        SubscriptionsVM vm;
        public SubscriptionsPage()
        {
            InitializeComponent();

            vm = Resources["vm"] as SubscriptionsVM;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!Auth.IsAuthenticated())
            {
                await Task.Delay(300);
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                vm.ReadSubscriptions();
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewSubscriptionPage());
        }
    }
}