using luxoft_T1.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace luxoft_T1.Models{
    public abstract class Repo<T>:IFakeRepo<T> where T:class
    {
        protected List<T> _db;
        public List<T> GetAll()
        {
            return _db;
        }
        public void Add(T unit)
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
        public void Edit(T unit)
        {

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
    }
    public interface IHateYou<T> where T: class
    {

    }
    public class dbRepoCustomer : Repo<Customer> //, IHateYou<Customer>
    {
        public dbRepoCustomer()
        {
            _db = new List<Customer>();
            _db.Add(new Customer { id = 1, Name = "Anton", Phone = "0" });
        }
    }
    public class dbRepoContractor : Repo<Contractor>
    {
        public dbRepoContractor()
        {
            _db = new List<Contractor>();
            _db.Add(new Contractor
            {
                id = 1,
                customer_id = 1,
                Name = "T1",
                Phone = "904",
                email = "ibavca@gmail.com"
            });
        }
    }

    /*
    public class dbRepo: IFakeRepo {
        private List<Customer> _db;
        private List<Contractor> _dbContractor;
        public dbRepo(){
            _db = new List<Customer>();
            _dbContractor = new List<Contractor>();

            //TODO: Заментить чтением из файла сериализованных объектов
            _db.Add(new Customer{
                id = 1,
                Name = "Customer 1",
                Phone = "9043036666"
            });
            _db.Add(new Customer{
                id = 2,
                Name = "Customer 2",
                Phone = "9043035555"
            });

        }
        public List<Customer> GetAll(){
            return _db;
        }

        public void Add(Customer customer){
            _db.Add(customer);
        }
        public int GetMax(){
            return _db.Max(x=>x.id);            
        }
        public Customer GetById(int id){
            return _db.FirstOrDefault(x=>x.id==id);
        }
        public void Edit(Customer customer){
            var index = _db.FindIndex(x=>x.id==customer.id);
            Console.WriteLine(customer.id);
            Console.WriteLine(index);
            _db[index].id = customer.id;
            _db[index].Name = customer.Name;
            _db[index].Phone = customer.Phone;
        }
        public void Delete(int id){
            var customer  = _db.FirstOrDefault(x=>x.id==id);
            _db.Remove(customer);
        }
    }
    */
}