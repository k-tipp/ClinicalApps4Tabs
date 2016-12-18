using SmaNa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaNa.LocalDataAccess
{
    public interface IDroidNotificationManager
    {
        void createNotification(Appointment appointment);
        void removeNotification(Appointment appointment);
    }
}
