using SmaNa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaNa.LocalDataAccess
{
    //public interface IDroidNotificationService
    //{
    //    void AddAppointment(Appointment appointment);
    //    void RemoveAppointment(Guid GuidOfAppointment);
    //}

    public interface INotificationEventReceiver
    {
        void SetupAlarm();
        void CancelAlarm();

    }
}
