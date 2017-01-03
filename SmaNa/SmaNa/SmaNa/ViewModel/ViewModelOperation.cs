using SmaNa.LocalDataAccess;
using SmaNa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaNa.ViewModel
{
    class ViewModelOperation
    {
        private XMLAccess<Operation> _xmlAccess;
        public Operation Operation;
        public ViewModelOperation() {

            _xmlAccess = new XMLAccess<Operation>("Operation");

        }
    }
}
