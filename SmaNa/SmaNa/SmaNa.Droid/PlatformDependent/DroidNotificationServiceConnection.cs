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
    class DroidNotificationServiceConnection : Java.Lang.Object, IServiceConnection
    {
        static readonly string TAG = typeof(DroidNotificationServiceConnection).FullName;

        public bool IsConnected { get; private set; }
        public DroidNotificationServiceBinder Binder { get; private set; }

        public DroidNotificationServiceConnection()
        {
            IsConnected = false;
            Binder = null;
        }

        public void OnServiceConnected(ComponentName name, IBinder binder)
        {
            Binder = binder as DroidNotificationServiceBinder;
            IsConnected = Binder != null;
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            IsConnected = false;
            Binder = null;
        }
    }
}