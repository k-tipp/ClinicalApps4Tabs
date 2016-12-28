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
using Android.Support.V7.App;
using SmaNa.LocalDataAccess;
using Xamarin.Forms;
using SmaNa.Droid.PlatformDependent;

[assembly: Dependency(typeof(DroidNotificationService))]
namespace SmaNa.Droid.PlatformDependent
{
    //(Exported = true, Name = "ch.bfh.smana.DroidNotificationService", IsolatedProcess = true)
    [Service]
    class DroidNotificationService : Service, IDroidNotificationService
    {
        static readonly string TAG = typeof(DroidNotificationService).FullName;
        static readonly int CycleTime = 0 * 30 * 1000; // = Service checks every 6h for pending notifications
        static List<Appointment> appointments_with_notification = new List<Appointment>();
        DateTime startTime;
        static Timer timer;

        public override void OnCreate()
        {
            // This method is optional, perform any initialization here.
            base.OnCreate();
            startTime = DateTime.UtcNow;
            timer = new Timer(new TimerCallback(HandleTimerCallback), null, 1000, 10000);
        }

        public override IBinder OnBind(Intent intent)
        {
            return new DroidNotificationServiceBinder(this);
        }

        public override bool OnUnbind(Intent intent)
        {
            // This method is optional
            return base.OnUnbind(intent);
        }

        public override void OnDestroy()
        {
            // This method is optional
            timer.Dispose();
            timer = null;
            base.OnDestroy();
        }

        public void AddAppointment(Appointment appointment)
        {
            if (!appointment.AppointmentReminder)
            {
                throw new Exception("Reminder is not activated for appointment with ID '" + appointment.AppointmentID + "'");
            }
            appointments_with_notification.Add(appointment);
        }

        public void RemoveAppointment(Guid GuidOfAppointment)
        {
            if (!appointments_with_notification.Any(appointment => appointment.AppointmentID.Equals(GuidOfAppointment)))
            {
                throw new NullReferenceException("No appointment with ID '" + GuidOfAppointment.ToString() + "' was found.");
            }
            appointments_with_notification.RemoveAll(appointment => appointment.AppointmentID.Equals(GuidOfAppointment));
        }

        static void HandleTimerCallback(Object state)
        {
            IEnumerable<Appointment> pendingAppointments = appointments_with_notification.Where<Appointment>(appointment =>
                (appointment.AppointmentDate == null ? appointment.AppointmentPeriode : appointment.AppointmentDate)
                    .CompareTo(DateTime.UtcNow) < 1);

            foreach (var appointment in pendingAppointments)
            {
                DroidNotificationManager.createNotification(appointment);
                Console.WriteLine("added a notification");
            }
            Console.WriteLine("test");
        }
    }
}