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
using SmaNa.LocalDataAccess;
using SmaNa.Model;

namespace SmaNa.Droid.PlatformDependent
{
    /// <summary>
    /// @created: Kevin Tippenhauer
    /// Broadcast receiver for: BOOT_COMPLETED, TIMEZONE_CHANGED, and TIME_SET events. Sets Alarm Manager for notification
    /// </summary>
    /// <seealso cref="http://stackoverflow.com/questions/20501225/using-service-to-run-background-and-create-notification/34207954"/>
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { Intent.ActionBootCompleted,
        Intent.ActionTimezoneChanged,
        Intent.ActionTimeChanged})]
    public sealed class NotificationServiceStarterReceiver : BroadcastReceiver
    {
        /// <summary>
        /// Executed if one of the Intents is received defined by the IntentFilter.
        /// <seealso cref="BroadcastReceiver.OnReceive(Context, Intent)"/>
        /// <seealso cref="IntentFilter"/>
        /// </summary>
        /// <param name="context">the received Context</param>
        /// <param name="intent">the received Intent</param>
        public override void OnReceive(Context context, Intent intent)
        {
            XMLAccess<List<Appointment>> xmlAccess = new XMLAccess<List<Appointment>>("Appointments");
            IEnumerable<Appointment> appointmentsWithOpenNotifications = xmlAccess.Load().Where(appointment => appointment.AppointmentReminder && !appointment.AppointmentDone);

            NotificationEventReceiver.UpdateAlarms(context, appointmentsWithOpenNotifications);
        }
    }
}