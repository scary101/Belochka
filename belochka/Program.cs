using System.Net;
using System.Transactions;

namespace belochka
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Admin admin = new Admin();
            if(admin.users.Count() == 0)
            {
                User user = new User();
                user.login = "adm";
                user.password = "777";
                user.ID_User = 1;
                user.role = 1;
                admin.users.Add(user);
                SerDeser.SerData(admin.users, "user.json");
            }



            while (true)
            {
                int role = Authorization.AutoUser();

                if(role == (int)RoleApp.Admin)
                {
                    admin.AdmMain();

                }
                else if(role == (int)RoleApp.Money)
                {
                    Money money = new Money();
                    money.MoneyMain();
                }
                else if (role == (int)RoleApp.Kass)
                {
                    Kassa kassa = new Kassa();
                    kassa.KassaMain();
                }
                else if(role == (int)RoleApp.Storage)
                {
                    Storage storage = new Storage();
                    storage.SrorageMain();
                }
                else if (role == (int)RoleApp.Personnel)
                {
                    Personnel personnel = new Personnel();
                    personnel.PersonnelMain();
                }
                else
                {
                    Console.WriteLine("ASdasdasdasd");
                    break;
                }

            }




        }
    }
}