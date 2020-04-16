using MyRFPSubscriptions.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyRFPSubscriptions.ViewModels.Helpers
{
    public interface IFirestore
    {
        Task<bool> InsertSubscription(Subscription subscription);
        Task<bool> DeleteSubscription(Subscription subscription);
        Task<bool> UpdateSubscription(Subscription subscription);
        Task<IList<Subscription>> ReadSubscription();
    }

    public class DatabaseHelper
    {
        private static IFirestore firestore;

        public Task<bool> DeleteSubscription(Subscription subscription)
        {
            return firestore.DeleteSubscription(subscription);
        }

        public Task<bool> InsertSubscription(Subscription subscription)
        {
            return firestore.InsertSubscription(subscription);
        }

        public Task<IList<Subscription>> ReadSubscription()
        {
            return firestore.ReadSubscription();
        }

        public Task<bool> UpdateSubscription(Subscription subscription)
        {
            return firestore.UpdateSubscription(subscription);
        }
    }
}
