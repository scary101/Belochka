using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace belochka
{
    internal class User
    {
        public int ID_User;
        public string login;
        public string password;
        public int role;

    }


    internal class Employee
    {
        public int ID_Employee;
        public string surName;
        public string name;
        public string midlleName = "NULL";
        public string dateBorn;
        public string pasData;
        public string role;
        public int salary;
        public int userRole_ID;
    }

    internal class Product
    {
        public int ID_product;
        public string name;
        public int price;
        public int quantity;
    }

    internal class Accounting
    {
        public int ID_Accounting;
        public string name;
        public int sum;
        public string date;
        public bool typeOperation;

    }

    internal class Buy : Product
    {
        public int selected = 0;
        

        public Buy(int ID_Accounting, string name, int price, int quantity) 
        {
            this.ID_product = ID_Accounting;
            this.name = name;
            this.price = price;
            this.quantity = quantity;
        }
    }
}
