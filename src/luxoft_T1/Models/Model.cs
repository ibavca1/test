using System.Collections.Generic;

namespace luxoft_T1.Models
{
    public struct MyModel
    {
        public IFakeRepo<Customer> listCustomer;
        public IFakeRepo<Contractor> listContractor;
    }
}
