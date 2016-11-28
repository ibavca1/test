using System;
using System.Collections.Generic;
using System.Linq;


namespace luxoft_T1.Models
{
    public abstract class Repo<T>:IFakeRepo<T> where T:class
    {
        protected List<T> _db;
        private object id;

        public List<T> GetAll()
        {
            return _db;
        }
        public virtual void Add(T unit)
        {
            _db.Add(unit);
        }
        public T GetById(int id)
        {           
            return _db.FirstOrDefault<dynamic>(x => x.id == id);
        }
        public void Delete(int id)
        {
            T unit = _db.FirstOrDefault<dynamic>(x => x.id == id);
            _db.Remove(unit);
        }
        public int GetMax()
        {
            if (_db.Count() == 0)
                return 0;
            return _db.Max<dynamic>(x => x.id);
        }

        public abstract void Edit(T item);

        public virtual List<T> GetListBy(int id)
        {
            return null;
        }
        public virtual List<string> GetAvailableStatus()
        {
            return null;
        }
    }
    public interface IFakeRepo<T> 
                        where T: class {

        List<T> GetAll();
        void Add(T item);
        int GetMax();
        T GetById(int id);
        void Edit(T item);
        void Delete(int id);
        List<T> GetListBy(int id);
        List<string> GetAvailableStatus();
    }

    public class dbRepoCustomer : Repo<Customer> 
    {
        public Customer resord;
        public dbRepoCustomer()
        {
            _db = new List<Customer>();
            _db.Add(new Customer { id = 1, Name = "Anton", Phone = "0" });
            _db.Add(new Customer { id = 2, Name = "Anna", Phone = "0" });
            _db.Add(new Customer { id = 3, Name = "Alena", Phone = "00.0111" });
        }

        public override void Edit(Customer item)
        {
            var index = _db.FindIndex(x => x.id == item.id);

            _db[index].id = item.id;
            _db[index].Name = item.Name;
            _db[index].Phone = item.Phone;
        }

 
    }
    public class dbRepoContractor : Repo<Contractor>
    {
        public dbRepoContractor()
        {
            _db = new List<Contractor>();
            List<string> status = new List<string>();
            status.Add("Черновик");
            status.Add("Утверждается");
            _db.Add(new Contractor
            {
                id = 1,
                customer_id = 1,
                Name = "T1",
                Phone = "904",
                email = "ibavca@gmail.com",
                Status = status
            });
        }
        public override void Add(Contractor unit)
        {
            unit.Status = new List<string>();
            unit.Status.Add("Черновик");
            unit.Status.Add("Утверждается");
            base.Add(unit);
        }
        public override List<Contractor> GetListBy(int id)
        {
            var result = from a in _db where a.customer_id == id select a;
            return result.ToList();
        }
        public override void Edit(Contractor item)
        {
            var index = _db.FindIndex(x => x.id == item.id);
            if (item.Status.Contains("Утверждается"))
            {
                item.Status.Clear();
                item.Status.Add("Утвержден");
                item.Status.Add("Отклонен");
            }
            _db[index].id = item.id;
            _db[index].Name = item.Name;
            _db[index].Phone = item.Phone;
            _db[index].email = item.email;
            _db[index].Status = item.Status;
        }
    }
}