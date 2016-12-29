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
using System.Threading;
using SmaNa.LocalDataAccess;
using Xamarin.Forms;
using SmaNa.Droid.PlatformDependent;
using Android.Support.V4.Content;
using Android.Support.V4.App;

namespace SmaNa.Droid.PlatformDependent
{
    [Service (Enabled= true, Exported = false, Name = "PlatformDependent.NotificationIntentService")]
    class NotificationIntentService : IntentService
    {

        private static readonly int NOTIFICATION_ID = 1;
        private static readonly String ACTION_START = "ACTION_START";
        private static readonly String ACTION_DELETE = "ACTION_DELETE";

        public NotificationIntentService()
        {
            base.OnCreate();
        }

        public static Intent CreateIntentStartNotificationService(Context context)
        {
            Intent intent = new Intent(context, typeof(NotificationIntentService));
            intent.SetAction(ACTION_START);
            return intent;
        }

        public static Intent CreateIntentDeleteNotification(Context context)
        {
            Intent intent = new Intent(context, typeof(NotificationIntentService));
            intent.SetAction(ACTION_DELETE);
            return intent;
        }

        protected override void OnHandleIntent(Intent intent)
        {
            Console.WriteLine(typeof(NotificationIntentService).Name, "onHandleIntent, started handling a notification event");
            try
            {
                String action = intent.Action;
                if (ACTION_START.Equals(action))
                {
                    processStartNotification();
                }
            }
            finally
            {
                WakefulBroadcastReceiver.CompleteWakefulIntent(intent);
            }
        }

        private void processDeleteNotification(Intent intent)
        {
            // Log something?
        }

        private void processStartNotification()
        {
            // Do something. For example, fetch fresh data from backend to create a rich notification?

            NotificationCompat.Builder builder = new NotificationCompat.Builder(this);
            builder.SetContentTitle("Scheduled Notification")
                    .SetAutoCancel(true)
                    .SetContentText("This notification has been triggered by Notification Service")
                    .SetSmallIcon(Resource.Drawable.logo);

            Intent mainIntent = new Intent(this, typeof(NotificationActivity));
            PendingIntent pendingIntent = PendingIntent.GetActivity(this,
                    NOTIFICATION_ID,
                    mainIntent,
                    PendingIntentFlags.UpdateCurrent);

            builder.SetContentIntent(pendingIntent);
            builder.SetDeleteIntent(NotificationEventReceiver.GetDeleteIntent(this));

            NotificationManager manager = (NotificationManager)GetSystemService(NotificationService);
            manager.Notify(NOTIFICATION_ID, builder.Build());
        }
    }
}