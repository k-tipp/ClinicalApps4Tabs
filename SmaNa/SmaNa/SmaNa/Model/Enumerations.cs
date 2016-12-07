using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaNa.Model
{
    public class Enumerations
    {
        public enum CancerType
        {
            Colonkarzinom = 1,
            Rektumkarzinom = 2
        }
        public enum TnmT
        {
            T0 = 0,
            T1 = 1,
            T2 = 2,
            T3 = 3,
            T4 = 4
        }
        public enum TnmN
        {
            N0 = 0,
            N1 = 1,
            N2 = 2,
            N3 = 3
        }
        public enum TnmM
        {
            M0 = 0,
            M1 = 1
        }
        public enum AppointmentState
        {
            geplant = 1,
            abgemacht = 2,
            durchgeführt = 3
        }
    }
}
