using MyRFPSubscriptions.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyRFPSubscriptions.ViewModels
{
    public class NewSubscriptionVM : INotifyPropertyChanged
    {
        private string name;

        public string Name
        {
            get { return name; }
            set 
            { 
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set 
            { 
                isActive = value;
                OnPropertyChanged("IsActive");
            }
        }

        public ICommand SaveSubscriptionCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public NewSubscriptionVM()
        {
            SaveSubscriptionCommand = new Command(SaveSubscription, SaveSubscriptionCanExecute);
        }

        private void SaveSubscription(object obj)
        {
            DatabaseHelper.InsertSubscription(new Models.Subscription
            {
                IsActive = IsActive,
                Name = Name,
                UserId = Auth.GetCurrentUserId(),
                SubscribedDate = DateTime.Now
            });
        }

        private bool SaveSubscriptionCanExecute(object arg)
        {
            return !string.IsNullOrEmpty(Name);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
