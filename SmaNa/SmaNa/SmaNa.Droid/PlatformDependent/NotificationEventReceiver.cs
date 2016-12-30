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
using Android.Support.V4.Content;
using Xamarin.Forms;
using SmaNa.Droid.PlatformDependent;
using SmaNa.LocalDataAccess;
using SmaNa.Model;
using Newtonsoft.Json;

[assembly: Dependency(typeof(NotificationEventReceiver))]
namespace SmaNa.Droid.PlatformDependent
{

    //WakefulBroadcastReceiver used to receive intents fired from the AlarmManager for showing notifications
    //and from the notification itself if it is deleted.
    [BroadcastReceiver(Enabled = true)]
    public class NotificationEventReceiver : WakefulBroadcastReceiver, INotificationEventReceiver
    {

        private static readonly String ACTION_START_NOTIFICATION_SERVICE = "ACTION_START_NOTIFICATION_SERVICE";
        private static readonly String ACTION_DELETE_NOTIFICATION = "ACTION_DELETE_NOTIFICATION";

        private static readonly int NOTIFICATIONS_INTERVAL_IN_HOURS = 2;

        public static void UpdateAlarms(IEnumerable<Appointment> appointments)
        {
            Context context = MainActivity.GetInstance().ApplicationContext;
            UpdateAlarms(context, appointments);
        }

        void INotificationEventReceiver.UpdateAlarms(IEnumerable<Appointment> appointments)
        {
            UpdateAlarms(appointments);
        }

        public static void UpdateAlarms(Context context, IEnumerable<Appointment> appointments)
        {
            AlarmManager alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
            PendingIntent alarmIntent = GetStartPendingIntent(context, appointments);
            Console.WriteLine(GetTriggerAt(DateTime.UtcNow));
            alarmManager.SetRepeating(AlarmType.RtcWakeup,
                    GetTriggerAt(DateTime.UtcNow),
                    NOTIFICATIONS_INTERVAL_IN_HOURS * 15000,
                    alarmIntent);
        }

        public static void CancelAlarm(IEnumerable<Appointment> appointments)
        {
            Context context = MainActivity.GetInstance().BaseContext.ApplicationContext;
            AlarmManager alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
            PendingIntent alarmIntent = GetStartPendingIntent(context, appointments);
            alarmManager.Cancel(alarmIntent);
        }

        //void INotificationEventReceiver.CancelAlarm(IEnumerable<Appointment> appointments)
        //{
        //    CancelAlarm(appointments);
        //}

        private static long GetTriggerAt(DateTime now)
        {
            TimeSpan span = (now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            return (long)span.TotalMilliseconds;
        }

        private static PendingIntent GetStartPendingIntent(Context context, IEnumerable<Appointment> appointments)
        {
            Intent intent = new Intent(context, typeof(NotificationEventReceiver));
            string s = JsonConvert.SerializeObject(appointments);
            intent.PutExtra("Appointments", s);
            intent.SetAction(ACTION_START_NOTIFICATION_SERVICE);
            return PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.UpdateCurrent);
        }

        public static PendingIntent GetDeleteIntent(Context context)
        {
            Intent intent = new Intent(context, typeof(NotificationEventReceiver));
            intent.SetAction(ACTION_DELETE_NOTIFICATION);
            return PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.UpdateCurrent);
        }

        public override void OnReceive(Context context, Intent intent)
        {
            String action = intent.Action;
            Intent serviceIntent = null;
            if (ACTION_START_NOTIFICATION_SERVICE.Equals(action))
            {
                Console.WriteLine(typeof(NotificationEventReceiver).Name, "onReceive from alarm, starting notification service");
                serviceIntent = NotificationIntentService.CreateIntentStartNotificationService(context, intent.Extras);
            }
            else if (ACTION_DELETE_NOTIFICATION.Equals(action))
            {
                Console.WriteLine(typeof(NotificationEventReceiver).Name, "onReceive delete notification action, starting notification service to handle delete");
                serviceIntent = NotificationIntentService.CreateIntentDeleteNotification(context);
            }

            if (serviceIntent != null)
            {
                // Start the service, keeping the device awake while it is launching.
                StartWakefulService(context, serviceIntent);
            }
        }
    }
}