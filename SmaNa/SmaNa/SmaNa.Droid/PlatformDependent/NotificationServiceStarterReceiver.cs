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
    //Broadcast receiver for: BOOT_COMPLETED, TIMEZONE_CHANGED, and TIME_SET events.Sets Alarm Manager for notification;
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { Intent.ActionBootCompleted,
        Intent.ActionTimezoneChanged,
        Intent.ActionTimeChanged})]
    public sealed class NotificationServiceStarterReceiver : BroadcastReceiver
    {

        public override void OnReceive(Context context, Intent intent)
        {
            Console.WriteLine("asdfasfasfd");
            XMLAccess<List<Appointment>> xmlAccess = new XMLAccess<List<Appointment>>("Appointments");
            IEnumerable<Appointment> appointmentsWithOpenNotifications = xmlAccess.Load().Where(appointment => appointment.AppointmentReminder && !appointment.AppointmentDone);

            NotificationEventReceiver.UpdateAlarms(context, appointmentsWithOpenNotifications);
        }
    }
}