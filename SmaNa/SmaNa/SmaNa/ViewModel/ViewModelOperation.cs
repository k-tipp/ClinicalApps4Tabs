using SmaNa.LocalDataAccess;
using SmaNa.Model;

namespace SmaNa.ViewModel
{
    /// <summary>
    /// ViewModel to edit the OperationData
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
