using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SmaNa.Model;
using Android.Support.V7.App;
using SmaNa.LocalDataAccess;
using SmaNa.Droid.PlatformDependent;
using Xamarin.Forms;

[assembly: Dependency(typeof(DroidNotificationManager))]
namespace SmaNa.Droid.PlatformDependent
{
    class DroidNotificationManager: IDroidNotificationManager
    {
        public void createNotification(Appointment appointment)
        {

            // Pass the current button press count value to the next activity:
            Bundle valuesForActivity = new Bundle();
            valuesForActivity.PutString("AppointmentID", appointment.AppointmentID.ToString());

            // When the user clicks the notification, SecondActivity will start up.
            Intent resultIntent = new Intent(Android.App.Application.Context, typeof(NotificationActivity));

            // Pass some values to SecondActivity:
            resultIntent.PutExtras(valuesForActivity);

            // Construct a back stack for cross-task navigation:
            TaskStackBuilder stackBuilder = TaskStackBuilder.Create(Android.App.Application.Context);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(NotificationActivity)));
            stackBuilder.AddNextIntent(resultIntent);

            // Create the PendingIntent with the back stack:            
            PendingIntent resultPendingIntent =
                stackBuilder.GetPendingIntent(0, PendingIntentFlags.UpdateCurrent);

            // Build the notification:
            //NotificationCompat.Builder builder = new NotificationCompat.Builder(Application.Context)
            //    .SetAutoCancel(true)                    // Dismiss from the notif. area when clicked
            //    .SetContentIntent(resultPendingIntent)  // Start 2nd activity when the intent is clicked.
            //    .SetContentTitle("Button Clicked")      // Set its title
            //    .SetNumber(1)                       // Display the count in the Content Info
            //    .SetSmallIcon(Resource.Drawable.icon)  // Display this icon
            //    .SetContentText(string.Format(
            //    "The button has been clicked {0} times.", 1)); // The message to display.

            NotificationCompat.Builder builder = new NotificationCompat.Builder(Android.App.Application.Context);
            builder.SetAutoCancel(true);
            builder.SetContentIntent(resultPendingIntent);
            builder.SetContentTitle("Appointment");
            builder.SetNumber(1);
            builder.SetSmallIcon(Resource.Drawable.logo);
            builder.SetContentText("asdfasfdasdfasdfasdf");

            // Finally, publish the notification:
            NotificationManager notificationManager = (NotificationManager)Android.App.Application.Context.GetSystemService(Context.NotificationService);
            notificationManager.Notify(1000, builder.Build());
        }

        public void removeNotification(Appointment appointment)
        {

        }
    }
}