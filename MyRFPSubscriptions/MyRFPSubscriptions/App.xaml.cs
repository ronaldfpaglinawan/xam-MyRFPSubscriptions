﻿using MyRFPSubscriptions.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyRFPSubscriptions
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SubscriptionsPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
