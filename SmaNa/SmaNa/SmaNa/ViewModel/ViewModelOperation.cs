using SmaNa.LocalDataAccess;
using SmaNa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaNa.ViewModel
{
    /// <summary>
    /// Created: Michel
    /// </summary>
    class ViewModelOperation
    {
        private XMLAccess<Operation> _xmlAccess;
        public Operation Operation;
        public ViewModelOperation() {

            _xmlAccess = new XMLAccess<Operation>("Operation");
            Operation = _xmlAccess.Load();
            if (Operation == null)
            {
                Operation = new Operation();
            }
        }

        public void Save()
        {
            _xmlAccess.Save(Operation);
        }
    }
}
