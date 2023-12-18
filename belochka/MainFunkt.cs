using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static belochka.Menu;

namespace belochka
{
    public static class SerDeser
    {
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Белочка";
        public static void SerData<T>(List<T> data, string fileName)
        {


            string fullpath = path + "\\" + fileName;
            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(fullpath, json);


        }
        public static List<T> DeserData<T>(string fileName)
        {
            string fullpath = path + "\\" + fileName;
            if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }

            if (!File.Exists(fullpath))
            {

                string jsonContent = "[]";
                File.WriteAllText(fullpath, jsonContent);
            }
            string json = File.ReadAllText(fullpath);
            List<T> data = JsonConvert.DeserializeObject<List<T>>(json);
            return data;
        }
    }
    static class Menu
    {
        public static ConsoleKeyInfo key;
        public static int strela(int minposition, int maxposition)
        {
            bool isRun = false;
            int poz = minposition;
            while (!isRun)
            {
                key = Console.ReadKey();
                Console.SetCursorPosition(0, poz);
                Console.WriteLine("  ");
                if ((SystemKey)key.Key == SystemKey.UpArrow)
                {
                    poz--;
                    if (poz < minposition)
                    {
                        poz = maxposition;
                    }
                }
                else if ((SystemKey)key.Key == SystemKey.DownArrow)
                {
                    poz++;
                    if (poz > maxposition)
                    {
                        poz = minposition;
                    }

                }
                else if ((SystemKey)key.Key == SystemKey.Escape)
                {
                    isRun = true;
                    break;
                }
                else if ((SystemKey)key.Key == SystemKey.F1)
                {
                    isRun = true;
                    break;
                }

                else if ((SystemKey)key.Key == SystemKey.S) { isRun = true; }

                else if ((SystemKey)key.Key == SystemKey.Enter) { isRun = true; }
                else if ((SystemKey)key.Key == SystemKey.F3) { isRun = true; }
                else if ((SystemKey)key.Key == SystemKey.Delete) { isRun = true; }
                else if ((SystemKey)key.Key == SystemKey.F2) { isRun = true; }

                Console.SetCursorPosition(0, poz);
                Console.WriteLine("->");

            }
            return poz;
        }



        public static string Hello()
        {
            Admin admin = new Admin();
            Personnel personnel = new Personnel();
            string name = "";

            string password = admin.users[Authorization.ID].password;
            for (int j = 0; j < admin.users.Count(); j++)
            {
                if (password == admin.users[j].password)
                {
                    for (int q = 0; q < personnel.employees.Count(); q++)
                    {
                        if (admin.users[j].ID_User == personnel.employees[q].ID_Employee)
                        {
                            name = personnel.employees[q].name;
                        }
                        else { continue; }
                    }
                }
                else { continue; }
            }
            if(name == "")
            {
                List<string> roleuser = admin.ConvertRole();
                name = roleuser[Authorization.ID];
            }

            return name;
        }

        public static void CreateMenu()
        {
            Console.SetCursorPosition(90, 2);
            Console.WriteLine("Сохранить - S");
            Console.SetCursorPosition(90, 3);
            Console.WriteLine("HUB - Esc");
        }

        public static void ReadAllMenu()
        {
            Console.SetCursorPosition(90, 2);
            Console.WriteLine("Удалить - Del");
            Console.SetCursorPosition(90, 3);
            Console.WriteLine("HUB - Esc");
            Console.SetCursorPosition(90, 4);
            Console.WriteLine("Создать - F2");
            Console.SetCursorPosition(90, 5);
            Console.WriteLine("Поиск - F3");
        }

        public static void ReadMenu()
        {
            Console.SetCursorPosition(90, 2);
            Console.WriteLine("Обновить - F1");
            Console.SetCursorPosition(90, 3);
            Console.WriteLine("HUB - Esc");
            Console.SetCursorPosition(90, 4);
            Console.WriteLine("Сохранить - S");
        }
        public static void RoleMenu()
        {
            Console.SetCursorPosition(90, 6);
            Console.WriteLine("Админ - 1");
            Console.SetCursorPosition(90, 7);
            Console.WriteLine("Кадровик - 2");
            Console.SetCursorPosition(90, 8);
            Console.WriteLine("Склад - 3");
            Console.SetCursorPosition(90, 9);
            Console.WriteLine("Кассир - 4");
            Console.SetCursorPosition(90, 10);
            Console.WriteLine("Бухгалтер - 5");
        }

    }




    static class Authorization
    {
        public static int ID = 0;
        public static int AutoUser()
        {
            Admin admin = new Admin();
            int role = -1;
            string pas = "";
            string log = "";
            bool isRun = false;
            while (!isRun)
            {
                Console.Clear();
                Console.WriteLine("Добро пожаловать в алкомаркет \"Белочка\"");
                Console.SetCursorPosition(0, 1);
                Console.WriteLine("  Логин: ");
                Console.SetCursorPosition(0, 2);
                Console.WriteLine("  Пароль: ");
                Console.SetCursorPosition(0, 3);
                Console.WriteLine("  Авторизоваться");


                while (true)
                {
                    int poz = Menu.strela(1, 3);

                    if (poz == 1)
                    {
                        Console.SetCursorPosition(10, 1);
                        log = Console.ReadLine();
                    }
                    else if (poz == 2)
                    {
                        
                        Console.SetCursorPosition(12, 2);
                        while (true)
                        {
                            int j = 0;
                            bool check = true;
                            ConsoleKeyInfo key = Console.ReadKey(true);
                            if (key.Key == ConsoleKey.Enter) { break; };
                            if (key.Key == ConsoleKey.Backspace && pas.Length > 0 )
                            {

                                pas = pas.Substring(0, pas.Length - 1);
                                check = false;
                            }
                            else
                            {
                                pas += Convert.ToString(key.KeyChar);
                                check = true;
                            }

                            foreach (char i in pas)
                            {
                                if (check == false)
                                {
                                    Console.SetCursorPosition(12 + pas.Length, 2);
                                    Console.WriteLine(" ");
                                    break;
                                }
                                else
                                {
                                    Console.SetCursorPosition(12 + j, 2);
                                    Console.WriteLine("*");
                                }
                                j++;
                            }
                            
                        }
                    }
                    if (poz == 3)
                    {
                        for (int i = 0; i < admin.users.Count(); i++)
                        {
                            if (admin.users[i].login == log && admin.users[i].password == pas)
                            {
                                role = admin.users[i].role;
                                ID = i;
                                break;
                            }
                        }
                        if (role != -1)
                        {
                            isRun = true;
                            break;
                        }
                        else
                        {
                            pas = "";
                            log = "";
                            Console.WriteLine("Неверный логин или пароль!" +
                                "\n Нажмите любую клавишу ...");
                            ConsoleKeyInfo key1 = Console.ReadKey(true);
                            break;
                        }
                    }
                }
            }
            return role;
        }
    }


    public static class CheckType
    {
        public static int CheckInt( int left, int top)
        {
            int per = 0;
            while (true)
            {
                Console.SetCursorPosition(left, top);
                string check = Console.ReadLine();
                try
                {
                    per = Convert.ToInt32(check);
                    break;
                }
                catch
                {
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine("Ошибка!");
                    Thread.Sleep(1000);
                    Console.SetCursorPosition(left - 1, top);
                    Console.WriteLine("                       ");
                }
            }
            return per;
        }

        public static bool CheckBool(int left, int top)
        {
            bool per = true;
            while (true)
            {
                Console.SetCursorPosition(left, top);
                string check = Console.ReadLine();
                try
                {
                    per = Convert.ToBoolean(check);
                    break;
                }
                catch
                {
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine("Ошибка!");
                    Thread.Sleep(1000);
                    Console.SetCursorPosition(left - 1, top);
                    Console.WriteLine("                       ");
                }
            }
            return per;
        }
    }








    public enum RoleApp
    {
        Admin = 1,
        Personnel = 2,
        Storage = 3,
        Kass = 4,
        Money = 5 
    }




    public enum SystemKey
    {
        F1 = ConsoleKey.F1,
        Escape = ConsoleKey.Escape,
        F2 = ConsoleKey.F2,
        S = ConsoleKey.S,
        Enter = ConsoleKey.Enter,
        F3 = ConsoleKey.F3,
        Delete = ConsoleKey.Delete,
        DownArrow = ConsoleKey.DownArrow,
        UpArrow = ConsoleKey.UpArrow,
        Plus = ConsoleKey.OemPlus,
        Minus = ConsoleKey.OemMinus
    }
    
            


























        


}