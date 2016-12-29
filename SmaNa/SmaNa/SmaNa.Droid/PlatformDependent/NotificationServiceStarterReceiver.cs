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

namespace SmaNa.Droid.PlatformDependent
{
    //Broadcast receiver for: BOOT_COMPLETED, TIMEZONE_CHANGED, and TIME_SET events.Sets Alarm Manager for notification;
    [BroadcastReceiver(Name = "PlatformDependent.NotificationServiceStarterReceiver", Enabled = true)]
    [IntentFilter(new[] { Intent.ActionBootCompleted,
        Intent.ActionTimezoneChanged,
        Intent.ActionTimeChanged})]
    public sealed class NotificationServiceStarterReceiver : BroadcastReceiver
    {

        public override void OnReceive(Context context, Intent intent)
        {
            NotificationEventReceiver.SetupAlarm(context);
        }
    }
}