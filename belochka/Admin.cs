using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static belochka.Menu;

namespace belochka
{
    internal class Admin : ICrud
    {
        public List<User> users = SerDeser.DeserData<User>("user.json");

        public int ReadAllUser()
        {

            
            string hello = Menu.Hello();
            Console.Clear();
            int max = users.Count + 3;
            int j = 4;
            int g = 4;
            
            Console.WriteLine("Вечер добрый  " + hello);
            Console.SetCursorPosition(2, 3);
            Console.WriteLine("ID_User");
            Console.SetCursorPosition(20, 3);
            Console.WriteLine("Login");
            Console.SetCursorPosition(40, 3);
            Console.WriteLine("Password");
            Console.SetCursorPosition(60, 3);
            Console.WriteLine("Role");
            Console.SetCursorPosition(80, 3);
            Menu.ReadAllMenu();


            List<string> role = ConvertRole();

            for (int i = 0; i < users.Count; i++)
            {

                Console.SetCursorPosition(2, j);
                Console.WriteLine(users[i].ID_User);
                Console.SetCursorPosition(20, j);
                Console.WriteLine(users[i].login);
                Console.SetCursorPosition(40, j);
                Console.WriteLine(users[i].password);
                Console.SetCursorPosition(60, j);

                j++;
            }
            foreach(string q in role)
            {
                Console.SetCursorPosition(60, g);
                Console.WriteLine(q);
                g++;
            }
            int poz = Menu.strela(4, max);
            return poz - 4;
        }

        public void Create()
        {
            Console.Clear();
            
            User user = new User();
            bool isRun = false;
            Console.WriteLine("\t Добавление пользователя");
            Console.WriteLine("  ID: ");
            Console.WriteLine("  Login: ");
            Console.WriteLine("  Password: ");
            Console.WriteLine("  Role: ");
            Menu.CreateMenu();
            Menu.RoleMenu();
            while (!isRun)
            {

                int poz = Menu.strela(1, 4);

                if ((SystemKey)Menu.key.Key == SystemKey.Escape)
                {
                    break;
                }
                else if ((SystemKey)Menu.key.Key == SystemKey.S)
                {
                    users.Add(user);
                    SerDeser.SerData<User>(users, "user.json");
                    break;
                }


                switch (poz)
                {
                    case 1:
                        Console.SetCursorPosition(6, 1);
                        user.ID_User = CheckType.CheckInt(6, 1);
                        break;
                    case 2:
                        Console.SetCursorPosition(9, 2);
                        user.login = Console.ReadLine();
                        break;
                    case 3:
                        Console.SetCursorPosition(12, 3);
                        user.password = Console.ReadLine();
                        break;
                    case 4:
                        Console.SetCursorPosition(9, 4);
                        user.role = CheckType.CheckInt( 9, 4);
                        break;

                }


            }

        }

        public void Delete(int poz)
        {

            Console.Clear();
            Console.WriteLine("Вы уверены что хотите удалить запись? Да/Нет");
            while (true)
            {
                string otvet = Console.ReadLine();
                if (otvet == "Да")
                {
                    users.RemoveAt(poz);
                    SerDeser.SerData(users, "user.json");
                    break;
                }
                else if (otvet == "Нет")
                {
                    break;
                }
                else { Console.WriteLine("Ошибка ответа!"); }
            }
        }

        public int Read(int poz)
        {
            Console.Clear();
            User user = users[poz];
            
            int ID = user.ID_User;
            string login = user.login;
            string password = user.password;
            int role = user.role;
            bool isRun = false;

            Console.Clear();
            Console.WriteLine("Подробная информация");
            Console.WriteLine($"  ID: {ID}");
            Console.WriteLine($"  Login: {login}");
            Console.WriteLine($"  Password: {password}");
            Console.WriteLine($"  Role: {role}");
            Menu.ReadMenu();
            int poz1 = Menu.strela(1, 4);

            if ((SystemKey)Menu.key.Key == SystemKey.F1)
            {
                Update(poz, poz1);
            }


            return poz1;

        }

        public void Update(int poz, int poz1)
        {
            User user = users[poz];
            bool isRun = false;
            while (!isRun)
            {

                if ((SystemKey)Menu.key.Key == SystemKey.Escape)
                {
                    break;
                }
                else if ((SystemKey)Menu.key.Key == SystemKey.S)
                {
                    users.RemoveAt(poz);
                    users.Insert(poz, user);
                    SerDeser.SerData(users, "user.json");
                    break;
                }


                switch (poz1)
                {
                    case 0:
                        Console.SetCursorPosition(6, 0);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(8, 0);
                        user.ID_User = CheckType.CheckInt(8, 0);
                        break;
                    case 1:
                        Console.SetCursorPosition(8, 1);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(10, 1);
                        user.login = Console.ReadLine();
                        break;
                    case 2:
                        Console.SetCursorPosition(11, 2);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(13, 2);
                        user.password = Console.ReadLine();
                        break;
                    case 3:
                        Console.SetCursorPosition(8, 3);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(10, 3);
                        user.role = CheckType.CheckInt(10, 3);
                        break;
                }
                
                poz1 = Menu.strela(0, 3);
            }
        }
        public void Search()
        {
            Console.Clear();
            Console.WriteLine("Выберите атрибут для поиска:");
            Console.WriteLine("  ID");
            Console.WriteLine("  Login");
            Console.WriteLine("  Password");
            Console.WriteLine("  Role");
            int poz = Menu.strela(1, 4);

            List<int> ID = new List<int>();
            for (int i = 0; i < users.Count; i++) { ID.Add(users[i].ID_User); }
            List<string> login = new List<string>();
            for (int i = 0; i < users.Count; i++) { login.Add(users[i].login); }
            List<string> password = new List<string>();
            for (int i = 0; i < users.Count; i++) { password.Add(users[i].password); }
            List<int> role = new List<int>();
            for (int i = 0; i < users.Count; i++) { role.Add(users[i].role); }


            string searchstr;
            int searchint;
            List<int> index = new List<int>();
            while (true)
            {

                if ((SystemKey)Menu.key.Key == SystemKey.Escape)
                {
                    break;
                }

                switch (poz)
                {
                    case 1:
                        Console.SetCursorPosition(0, 7);
                        searchint = Convert.ToInt32(Console.ReadLine());
                        index = VivodSearh<int>(ID, searchint);
                        if (index.Count != 0)
                        {
                            ReadSearch(index);
                        }
                        else
                        {
                            Console.WriteLine("Элемент не найден");
                        }
                        break;
                    case 2:
                        Console.SetCursorPosition(0, 7);
                        searchstr = Console.ReadLine();
                        index = VivodSearh<string>(login, searchstr);
                        if (index.Count != 0)
                        {
                            ReadSearch(index);
                        }
                        else
                        {
                            Console.WriteLine("Элемент не найден");
                        }
                        break;
                    case 3:
                        Console.SetCursorPosition(0, 7);
                        searchstr = Console.ReadLine();
                        index = VivodSearh<string>(password, searchstr);
                        if (index.Count != 0)
                        {
                            ReadSearch(index);
                        }
                        else
                        {
                            Console.WriteLine("Элемент не найден");
                        }
                        break;
                    case 4:
                        Console.SetCursorPosition(0, 7);
                        searchint = Convert.ToInt32(Console.ReadLine());
                        index = VivodSearh<int>(role, searchint);
                        if (index.Count != 0)
                        {
                            ReadSearch(index);
                        }
                        else
                        {
                            Console.WriteLine("Элемент не найден");
                        }
                        break;
                }
            }

        }

        void ReadSearch(List<int> index)
        {
            int j = 4;
            Console.Clear();
            Console.SetCursorPosition(2, 3);
            Console.WriteLine("ID_User");
            Console.SetCursorPosition(20, 3);
            Console.WriteLine("Login");
            Console.SetCursorPosition(40, 3);
            Console.WriteLine("Password");
            Console.SetCursorPosition(60, 3);
            Console.WriteLine("Role");
            Console.SetCursorPosition(80, 3);


            foreach (int i in index)
            {
                Console.SetCursorPosition(2, j);
                Console.WriteLine(users[i].ID_User);
                Console.SetCursorPosition(20, j);
                Console.WriteLine(users[i].login);
                Console.SetCursorPosition(40, j);
                Console.WriteLine(users[i].password);
                Console.SetCursorPosition(60, j);
                Console.WriteLine(users[i].role);
                Console.SetCursorPosition(80, j);
                j++;
            }
            bool isRun = false;
            while (!isRun)
            {
                int poz = Menu.strela(4, index.Count + 3);

                switch ((SystemKey)Menu.key.Key)
                {
                    case SystemKey.Enter:
                        Read(poz);
                        isRun = true;
                        break;

                    case SystemKey.Escape:
                        isRun = true;
                        break;

                }
            }
        }
        static public List<int> VivodSearh<T>(List<T> data, T atr)
        {
            List<int> index = new List<int>();
            int count = data.Count;
            for (int i = 0; i < count; i++)
            {
                if (atr.Equals(data[i]))
                {
                    index.Add(i);
                }
            }
            return index;
        }
        public void AdmMain()
        {
            bool isRun = false;
            while (!isRun)
            {
                int poz = ReadAllUser();
                switch ((SystemKey)Menu.key.Key)
                {
                    case SystemKey.Enter:
                        Read(poz);
                        break;

                    case SystemKey.Delete:
                        Delete(poz);
                        break;

                    case SystemKey.Escape:
                        isRun = true;
                        break;

                    case SystemKey.F3:
                        Search();
                        break;

                    case SystemKey.F2:
                        Create();
                        break;
                }
            }
        }


        public List<string> ConvertRole()
        {
            List<string> list = new List<string>();

            string strRole = "";

            for(int i = 0; i < users.Count(); i++)
            {
                int role = users[i].role;
                if (role == (int)RoleApp.Admin) { strRole = "Admin";  list.Add(strRole); }
                else if (role == (int)RoleApp.Money) { strRole = "Accounting"; list.Add(strRole); }
                else if (role == (int)RoleApp.Kass) { strRole = "Kassir"; list.Add(strRole); }
                else if (role == (int)RoleApp.Storage) { strRole = "Storage"; list.Add(strRole); }
                else if (role == (int)RoleApp.Personnel) { strRole = "Personnel"; list.Add(strRole); }
            }
            
            return list;
        }



    }
}
