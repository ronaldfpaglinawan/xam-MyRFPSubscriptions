using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using MyRFPSubscriptions.Models;
using MyRFPSubscriptions.ViewModels.Helpers;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(MyRFPSubscriptions.iOS.Dependencies.Firestore))]
namespace MyRFPSubscriptions.iOS.Dependencies
{
    public class Firestore : IFirestore
    {
        public Firestore()
        {

        }

        public async Task<bool> DeleteSubscription(Subscription subscription)
        {
            try
            {
                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("subscriptions");
                await collection.GetDocument(subscription.Id).DeleteDocumentAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool InsertSubscription(Subscription subscription)
        {
            try
            {
                var keys = new[]
                {
                    new NSString("author"),
                    new NSString("name"),
                    new NSString("isActive"),
                    new NSString("subscribedDate")
                };

                var values = new NSObject[]
                {
                    new NSString(Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid),
                    new NSString(subscription.Name),
                    new NSNumber(subscription.IsActive),
                    DateTimeToNSDate(subscription.SubscribedDate)
                };

                var subscriptionDocument = new NSDictionary<NSString, NSObject>(keys, values);
                Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("subscriptions").AddDocument(subscriptionDocument);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IList<Subscription>> ReadSubscriptions()
        {
            try
            {
                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("subscriptions");
                var query = collection.WhereEqualsTo("author", Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid);
                var documents = await query.GetDocumentsAsync();

                List<Subscription> subscriptions = new List<Subscription>();
                foreach (var doc in documents.Documents)
                {
                    var subscriptionDictiionary = doc.Data;
                    var subscription = new Subscription
                    {
                        IsActive = (bool)(subscriptionDictiionary.ValueForKey(new NSString("isActive")) as NSNumber),
                        Name = subscriptionDictiionary.ValueForKey(new NSString("isActive")) as NSString,
                        UserId = subscriptionDictiionary.ValueForKey(new NSString("author")) as NSString,
                        SubscribedDate = FIRTimeToDateTime(subscriptionDictiionary.ValueForKey(new NSString("subcribedDate")) as Firebase.CloudFirestore.Timestamp),
                        Id = doc.Id
                    };
                    subscriptions.Add(subscription);
                }

                return subscriptions;
            }
            catch (Exception ex)
            {
                return new List<Subscription>();
            }
        }

        public async Task<bool> UpdateSubscription(Subscription subscription)
        {
            try
            {
                var keys = new[]
                {
                    new NSString("name"),
                    new NSString("isActive")
                };

                var values = new NSObject[]
                {
                    new NSString(subscription.Name),
                    new NSNumber(subscription.IsActive)
                };

                var subscriptionDocument = new NSDictionary<NSObject, NSObject>(keys, values);

                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("subscriptions");
                await collection.GetDocument(subscription.Id).UpdateDataAsync(subscriptionDocument);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        private static NSDate DateTimeToNSDate(DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
                date = DateTime.SpecifyKind(date, DateTimeKind.Local);
            return (NSDate)date;
        }

        private static DateTime FIRTimeToDateTime(Firebase.CloudFirestore.Timestamp date)
        {
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
            return reference.AddSeconds(date.Seconds);
        }
    }
}