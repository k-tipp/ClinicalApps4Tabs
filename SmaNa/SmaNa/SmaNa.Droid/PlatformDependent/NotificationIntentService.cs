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
using Newtonsoft.Json;
/// <summary>
/// Created: Kevin
/// http://stackoverflow.com/questions/20501225/using-service-to-run-background-and-create-notification/34207954
/// </summary>
namespace SmaNa.Droid.PlatformDependent
{
    [Service(Enabled = true, Exported = false)]
    class NotificationIntentService : IntentService
    {

        private static int NOTIFICATION_ID = 1;
        private static readonly String ACTION_START = "ACTION_START";
        private static readonly String ACTION_DELETE = "ACTION_DELETE";

        public NotificationIntentService()
        {
            base.OnCreate();
        }

        public static Intent CreateIntentStartNotificationService(Context context, Bundle bundleForNotificationIntent)
        {
            Intent intent = new Intent(context, typeof(NotificationIntentService));
            intent.SetAction(ACTION_START);
            intent.PutExtras(bundleForNotificationIntent);
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
                    processStartNotification(intent.Extras);
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

        private void processStartNotification(Bundle notificationData)
        {
            // Do something. For example, fetch fresh data from backend to create a rich notification?
            IEnumerable<Appointment> appointments = extractAppointments(notificationData);
            NOTIFICATION_ID = 1;
            foreach(Appointment appointment in appointments)
            {
                if(isUpcomingAppointment(appointment))
                    CreateNotification(appointment);
            }
        }

        private bool isUpcomingAppointment(Appointment appointment)
        {
            DateTime notificationDate = (appointment.AppointmentDate.Equals(default(DateTime)) ? appointment.AppointmentPeriode : appointment.AppointmentDate);
            if (DateTime.Compare(notificationDate, DateTime.Now.AddDays(15)) < 0 &&
                DateTime.Compare(notificationDate, DateTime.Now) >= 0)
                return true;
            return false;
        }

        private void CreateNotification(Appointment appointment)
        {
            if (NOTIFICATION_ID < 2)
            {
                var ci = SmaNa.ViewModel.ViewModelSettings.SmaNaSettings.Language;
                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;
                NotificationCompat.Builder builder = new NotificationCompat.Builder(this);
                builder.SetContentTitle(appointment.PushMessageHeader)
                        .SetAutoCancel(true)
                        .SetContentText("")
                        .SetSmallIcon(Resource.Drawable.logo);

                Intent mainIntent = new Intent(this, typeof(NotificationActivity));
                string appointmentID = appointment.AppointmentID.ToString();
                mainIntent.PutExtra("AppointmentID", appointmentID);
                PendingIntent pendingIntent = PendingIntent.GetActivity(this,
                        NOTIFICATION_ID,
                        mainIntent,
                        PendingIntentFlags.UpdateCurrent);

                builder.SetContentIntent(pendingIntent);
                builder.SetDeleteIntent(NotificationEventReceiver.GetDeleteIntent(this));

                NotificationManager manager = (NotificationManager)GetSystemService(NotificationService);
                manager.Notify(NOTIFICATION_ID, builder.Build());
                NOTIFICATION_ID++;
            }     
        }

        private IEnumerable<Appointment> extractAppointments(Bundle notificationData)
        {
            string data = notificationData.GetString("Appointments");
            if (String.IsNullOrEmpty(data))
                return new List<Appointment>();
            return JsonConvert.DeserializeObject<IEnumerable<Appointment>>(data);
        }
    }
}