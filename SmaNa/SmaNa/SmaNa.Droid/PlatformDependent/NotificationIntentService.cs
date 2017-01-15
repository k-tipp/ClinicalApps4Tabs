using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using SmaNa.Model;
using System.Threading;
using Android.Support.V4.Content;
using Android.Support.V4.App;
using Newtonsoft.Json;

namespace SmaNa.Droid.PlatformDependent
{
    /// <summary>
    /// @created: Kevin Tippenhauer
    /// This class handles the service which will be started to create the notifications on alarm time.
    /// </summary>
    /// <seealso cref="http://stackoverflow.com/questions/20501225/using-service-to-run-background-and-create-notification/34207954"/>

    [Service(Enabled = true, Exported = false)]
    class NotificationIntentService : IntentService
    {

        private static int NOTIFICATION_ID = 1;
        private static readonly String ACTION_START = "ACTION_START";
        private static readonly String ACTION_DELETE = "ACTION_DELETE";

        /// <summary>
        /// @created: Kevin Tippenhauer
        /// The constructor calls <see cref="Service.OnCreate()"/>
        /// </summary>
        public NotificationIntentService()
        {
            base.OnCreate();
        }

        /// <summary>
        /// Creates an Intent to start the notification service.
        /// </summary>
        /// <param name="context">the context for the Intent</param>
        /// <param name="bundleForNotificationIntent">the bundle to be put as Extras of the Intent</param>
        /// <returns>the created Intent</returns>
        public static Intent CreateIntentStartNotificationService(Context context, Bundle bundleForNotificationIntent)
        {
            Intent intent = new Intent(context, typeof(NotificationIntentService));
            intent.SetAction(ACTION_START);
            intent.PutExtras(bundleForNotificationIntent);
            return intent;
        }

        /// <summary>
        /// Creates an Intent to delete a notification
        /// </summary>
        /// <param name="context">the context for the Intent</param>
        /// <returns>the created Intent</returns>
        public static Intent CreateIntentDeleteNotification(Context context)
        {
            Intent intent = new Intent(context, typeof(NotificationIntentService));
            intent.SetAction(ACTION_DELETE);
            return intent;
        }

        /// <summary>
        /// Handles a given Intent <see cref="IntentService.OnHandleIntent(Intent)"/>
        /// </summary>
        /// <param name="intent">the Intent to handle</param>
        protected override void OnHandleIntent(Intent intent)
        {
            // onHandleIntent, started handling a notification event
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

        /// <summary>
        /// Does currently nothing, can be used to do somthing when a delete notification intent occures <seealso cref="NotificationIntentService.processStartNotification(Bundle)"/>
        /// </summary>
        /// <param name="intent">the delete notification Intent</param>
        private void processDeleteNotification(Intent intent)
        {
            // Log something?
        }

        /// <summary>
        /// Creates for each appointment in the bundle a notification if necessary.
        /// </summary>
        /// <param name="notificationData">the Bundle with the appointments</param>
        private void processStartNotification(Bundle notificationData)
        {
            IEnumerable<Appointment> appointments = extractAppointments(notificationData);
            NOTIFICATION_ID = 1; // Make sure that NOTIFICATION_ID is set to 1
            foreach (Appointment appointment in appointments)
            {
                if (isUpcomingAppointment(appointment))
                    CreateNotification(appointment);
            }
        }

        /// <summary>
        /// Checks if a notification needs to be made for an appointment. A notification will be created if the appointment day is less than 15 days from now and the appointment time is in the future.
        /// </summary>
        /// <param name="appointment">the appointment to check</param>
        /// <returns>true, if a notification is needed.</returns>
        private bool isUpcomingAppointment(Appointment appointment)
        {
            DateTime notificationDate = (appointment.AppointmentDate.Equals(default(DateTime)) ? appointment.AppointmentPeriode : appointment.AppointmentDate);
            if (DateTime.Compare(notificationDate, DateTime.Now.AddDays(15)) < 0 &&
                DateTime.Compare(notificationDate, DateTime.Now) >= 0)
                return true;
            return false;
        }

        /// <summary>
        /// Creates a notification on the smartphone for a given appointment.
        /// </summary>
        /// <param name="appointment">the appointment which needs a notification</param>
        private void CreateNotification(Appointment appointment)
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

        /// <summary>
        /// Creates a Enumerable of Appointment from a given Bundle.
        /// </summary>
        /// <param name="notificationData">the Bundle of an Intent containing a Enumerable of Appointments</param>
        /// <returns>the Enumerable of Appointment elements</returns>
        private IEnumerable<Appointment> extractAppointments(Bundle notificationData)
        {
            string data = notificationData.GetString("Appointments");
            if (String.IsNullOrEmpty(data))
                return new List<Appointment>();
            return JsonConvert.DeserializeObject<IEnumerable<Appointment>>(data);
        }
    }
}