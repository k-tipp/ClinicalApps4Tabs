using SmaNa.Model;
using System.Collections.Generic;

namespace SmaNa.LocalDataAccess
{
    /// <summary>
    /// Interface used to access PushNotifications on Android
    /// @created: Kevin Tippenhauer
    /// </summary>
    public interface INotificationEventReceiver
    {
        void UpdateAlarms(IEnumerable<Appointment> appointments);
    }
}
