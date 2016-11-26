
namespace luxoft_T1.Models{

/*
    public class Customer
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
*/
   
    public interface ICustomer
    {
        int id { get; set; }
        string Name { get; set; }
        string Phone { get; set; }
    }
    public class Customer : ICustomer
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }

}