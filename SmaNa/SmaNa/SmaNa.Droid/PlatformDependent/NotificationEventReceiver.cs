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
    /// <summary>
    /// @created: Kevin Tippenhauer
    /// This class handles the notifications of the Application. It can create, cancel or delete a notification of an appointment.
    /// WakefulBroadcastReceiver used to receive intents fired from the AlarmManager for showing notifications and from the notification itself if it is deleted.
    /// </summary>
    /// <seealso cref="http://stackoverflow.com/questions/20501225/using-service-to-run-background-and-create-notification/34207954"/>
    [BroadcastReceiver(Enabled = true)]
    public class NotificationEventReceiver : WakefulBroadcastReceiver, INotificationEventReceiver
    {

        private static readonly String ACTION_START_NOTIFICATION_SERVICE = "ACTION_START_NOTIFICATION_SERVICE";
        private static readonly String ACTION_DELETE_NOTIFICATION = "ACTION_DELETE_NOTIFICATION";

        // Defines how often the notifications are checken/shown. Currently pending notifications will be shown every two hours.
        private static readonly int NOTIFICATIONS_INTERVAL_IN_HOURS = 2;

        /// <summary>
        /// Updates notifications based on the delivered appointments.
        /// </summary>
        /// <param name="appointments">an Enumerable of Appointments which had their notification configuration changed.</param>
        public static void UpdateAlarms(IEnumerable<Appointment> appointments)
        {
            Context context = MainActivity.GetInstance().ApplicationContext;
            UpdateAlarms(context, appointments);
        }

        /// <summary>
        /// Updates notifications based on the delivered appointments. This method fulfills the requirements of the INotificationEventReceiver interface, since static methods can't be declared by an interface.
        /// </summary>
        /// <param name="appointments">an Enumerable of Appointments which had their notification configuration changed.</param>
        void INotificationEventReceiver.UpdateAlarms(IEnumerable<Appointment> appointments)
        {
            UpdateAlarms(appointments);
        }

        /// <summary>
        /// Updates notifications based on the delivered appointments.
        /// </summary>
        /// <param name="context">the application context for the alarm/notification.</param>
        /// <param name="appointments">an Enumerable of Appointments which had their notification configuration changed.</param>
        public static void UpdateAlarms(Context context, IEnumerable<Appointment> appointments)
        {
            AlarmManager alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
            PendingIntent alarmIntent = GetStartPendingIntent(context, appointments);
            Console.WriteLine(GetTriggerAt(DateTime.UtcNow));
            alarmManager.SetRepeating(AlarmType.RtcWakeup,
                    GetTriggerAt(DateTime.UtcNow),
                    NOTIFICATIONS_INTERVAL_IN_HOURS * AlarmManager.IntervalHour,
                    alarmIntent);
        }

        /// <summary>
        /// Cancels notifications based on the delivered appointments.
        /// </summary>
        /// <param name="appointments">an Enumerable of Appointments which notifications had to be cancled.</param>
        public static void CancelAlarm(IEnumerable<Appointment> appointments)
        {
            Context context = MainActivity.GetInstance().BaseContext.ApplicationContext;
            AlarmManager alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
            PendingIntent alarmIntent = GetStartPendingIntent(context, appointments);
            alarmManager.Cancel(alarmIntent);
        }

        /// <summary>
        /// Converts a DateTime to use it as trigger for a notification.
        /// </summary>
        /// <param name="now">the time to convert in milliseconds</param>
        /// <returns>the submitted time in milliseconds since 1970 as long value</returns>
        private static long GetTriggerAt(DateTime now)
        {
            TimeSpan span = (now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            return (long)span.TotalMilliseconds;
        }

        /// <summary>
        /// Creates a PendingIntent containing the delivered context and appointments
        /// </summary>
        /// <param name="context">the Context for the Intent</param>
        /// <param name="appointments">a Enumerable of Appointments to add to the PendingIntent</param>
        /// <returns>a PendingIntent containing the appointments as Extra with the key "Appointments"</returns>
        private static PendingIntent GetStartPendingIntent(Context context, IEnumerable<Appointment> appointments)
        {
            Intent intent = new Intent(context, typeof(NotificationEventReceiver));
            string s = JsonConvert.SerializeObject(appointments);
            intent.PutExtra("Appointments", s);
            intent.SetAction(ACTION_START_NOTIFICATION_SERVICE);
            return PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.UpdateCurrent);
        }

        /// <summary>
        /// Creates a PendingIntent to delete an delete a notification.
        /// </summary>
        /// <param name="context">the Context for the Intent</param>
        /// <returns>a PendingIntent with the delivered Context</returns>
        public static PendingIntent GetDeleteIntent(Context context)
        {
            Intent intent = new Intent(context, typeof(NotificationEventReceiver));
            intent.SetAction(ACTION_DELETE_NOTIFICATION);
            return PendingIntent.GetBroadcast(context, 0, intent, PendingIntentFlags.UpdateCurrent);
        }

        /// <summary>
        /// Exectuted when an alarm is received.
        /// <seealso cref="BroadcastReceiver.OnReceive(Context, Intent)"/>
        /// </summary>
        /// <param name="context">the received Context</param>
        /// <param name="intent">the received intent</param>
        public override void OnReceive(Context context, Intent intent)
        {
            String action = intent.Action;
            Intent serviceIntent = null;
            if (ACTION_START_NOTIFICATION_SERVICE.Equals(action))
            {
                // onReceive from alarm, starting notification service
                serviceIntent = NotificationIntentService.CreateIntentStartNotificationService(context, intent.Extras);
            }
            else if (ACTION_DELETE_NOTIFICATION.Equals(action))
            {
                // onReceive delete notification action, starting notification service to handle delete
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