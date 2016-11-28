using System.Collections.Generic;

namespace luxoft_T1.Models{

    public class Contractor
    {
        public int id { get; set; }
        public int customer_id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string email { get; set; }
        public List<string> Status { get; set; }
    }

}