using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static belochka.Menu;

namespace belochka
{
    internal class Personnel : ICrud
    {
        public List<Employee> employees;

        public Personnel()
        {
            employees = SerDeser.DeserData<Employee>("Personnel.json");
        }

        public void Create()
        {
            Console.Clear();
            Employee employee = new Employee();
            bool isRun = false;
            Console.Clear();
            Console.WriteLine("     Добавленеи сотруднкиа " +
                "\n  ID: " +
                "\n  Фамилия: " +
                "\n  Имя: " +
                "\n  Отчество: " +
                "\n  Дата рождения(00/00/0000): " +
                "\n  Серия и номер паспорта: " +
                "\n  Должность: " +
                "\n  Зарплата: " +
                "\n  ID пользователя: ");
            Menu.CreateMenu();



            while (!isRun)
            {
                int poz = Menu.strela(1, 10);

                if ((SystemKey)Menu.key.Key == SystemKey.Escape)
                {
                    break;
                }
                else if ((SystemKey)Menu.key.Key == SystemKey.S)
                {
                    employees.Add(employee);
                    SerDeser.SerData<Employee>(employees, "Personnel.json");
                    break;
                }

                switch (poz)
                {
                    case 1:
                        Console.SetCursorPosition(7, 1);
                        employee.ID_Employee = CheckType.CheckInt(7, 1);
                        break;
                    case 2:
                        Console.SetCursorPosition(11, 2);
                        employee.surName = Console.ReadLine();
                        break;
                    case 3:
                        Console.SetCursorPosition(8, 3);
                        employee.name = Console.ReadLine();
                        break;
                    case 4:
                        Console.SetCursorPosition(12, 4);
                        employee.midlleName = Console.ReadLine();
                        break;
                    case 5:
                        Console.SetCursorPosition(30, 5);
                        employee.dateBorn = Console.ReadLine();
                        break;
                    case 6:
                        Console.SetCursorPosition(25, 6);
                        employee.pasData = Console.ReadLine();
                        break;
                    case 7:
                        Console.SetCursorPosition(14, 7);
                        employee.role = Console.ReadLine();
                        break;
                    case 8:
                        Console.SetCursorPosition(12, 8);
                        employee.salary = CheckType.CheckInt(12, 8);
                        break;
                    case 9:
                        Console.SetCursorPosition(20, 9);
                        employee.userRole_ID = CheckType.CheckInt(20, 9);
                        break;
                }
            }



        }


        public int ReadAll()
        {
            string hello = Menu.Hello();
            Console.Clear();
            int max = employees.Count() + 3;
            int j = 4;
            Console.WriteLine("Вечер добрый  " + hello);
            Console.SetCursorPosition(2, 3);
            Console.WriteLine("ID");
            Console.SetCursorPosition(10, 3);
            Console.WriteLine("Фамилия");
            Console.SetCursorPosition(30, 3);
            Console.WriteLine("Имя");
            Console.SetCursorPosition(50, 3);
            Console.WriteLine("Отчество");
            Console.SetCursorPosition(70, 3);
            Console.WriteLine("Роль");
            Console.SetCursorPosition(90, 3);
            Console.WriteLine("ID User");
            Menu.ReadAllMenu();


            for (int i = 0; i < employees.Count(); i++)
            {
                Console.SetCursorPosition(2, j);
                Console.WriteLine(employees[i].ID_Employee);
                Console.SetCursorPosition(10, j);
                Console.WriteLine(employees[i].surName);
                Console.SetCursorPosition(30, j);
                Console.WriteLine(employees[i].name);
                Console.SetCursorPosition(50, j);
                Console.WriteLine(employees[i].midlleName);
                Console.SetCursorPosition(70, j);
                Console.WriteLine(employees[i].role);
                Console.SetCursorPosition(90, j);
                Console.WriteLine(employees[i].userRole_ID);

                j++;
            }
            int poz = Menu.strela(4, max);
            return poz - 4;
        }





        public void Delete(int poz)
        {
            Personnel personnel = new Personnel();
            Console.WriteLine("Вы уверены что хотите удалить запись? Да/Нет");
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Вы уверены что хотите удалить запись? Да/Нет");
                string otvet = Console.ReadLine();
                if (otvet == "Да")
                {
                    employees.RemoveAt(poz);
                    SerDeser.SerData(employees, "Personnel.json");
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
            Console.WriteLine("Подробная информация" +
                $"\n  ID: {employees[poz].ID_Employee}" +
                $"\n  Фамилия: {employees[poz].surName}" +
                $"\n  Имя: {employees[poz].name}" +
                $"\n  Отчество: {employees[poz].midlleName}" +
                $"\n  Дата рождения: {employees[poz].dateBorn}" +
                $"\n  Серия номер паспорта: {employees[poz].pasData}" +
                $"\n  Должность: {employees[poz].role}" +
                $"\n  Зарплата: {employees[poz].salary}" +
                $"\n  UserID: {employees[poz].userRole_ID}");
            Menu.ReadMenu();
            int poz1 = Menu.strela(1, 9);

            if ((SystemKey)Menu.key.Key == SystemKey.F1)
            {
                Update(poz, poz1);
            }

            return poz1;
        }

        public void Update(int poz, int poz1)
        {
            Employee employee = employees[poz];
            bool isRun = false;
            while (!isRun)
            {

                if ((SystemKey)Menu.key.Key == SystemKey.Escape)
                {
                    break;
                }
                else if ((SystemKey)Menu.key.Key == SystemKey.S)
                {
                    employees.RemoveAt(poz);
                    employees.Insert(poz, employee);
                    SerDeser.SerData<Employee>(employees, "Personnel.json");
                    break;
                }

                switch (poz1)
                {
                    case 1:
                        Console.SetCursorPosition(6, 1);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(7, 1);
                        employee.ID_Employee = CheckType.CheckInt(7, 1);
                        break;
                    case 2:
                        Console.SetCursorPosition(10, 2);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(11, 2);
                        employee.surName = Console.ReadLine();
                        break;
                    case 3:
                        Console.SetCursorPosition(7, 3);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(8, 3);
                        employee.name = Console.ReadLine();
                        break;
                    case 4:
                        Console.SetCursorPosition(11, 4);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(12, 4);
                        employee.midlleName = Console.ReadLine();
                        break;
                    case 5:
                        Console.SetCursorPosition(29, 5);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(30, 5);
                        employee.dateBorn = Console.ReadLine();
                        break;
                    case 6:
                        Console.SetCursorPosition(24, 6);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(25, 6);
                        employee.pasData = Console.ReadLine();
                        break;
                    case 7:
                        Console.SetCursorPosition(13, 7);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(14, 7);
                        employee.role = Console.ReadLine();
                        break;
                    case 8:
                        Console.SetCursorPosition(11, 8);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(12, 8);
                        employee.salary = CheckType.CheckInt(12, 8);
                        break;
                    case 9:
                        Console.SetCursorPosition(19, 9);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(20, 9);
                        employee.userRole_ID = CheckType.CheckInt(20, 9);
                        break;
                }
                poz1 = Menu.strela(1, 10);
            }
        }
        public void Search()
        {
            int count = employees.Count();
            Console.Clear();
            Console.WriteLine("     Поиск сотруднкиа " +
                "\n  ID: " +
                "\n  Фамилия: " +
                "\n  Имя: " +
                "\n  Отчество: " +
                "\n  Дата рождения(00/00/0000): " +
                "\n  Серия и номер паспорта: " +
                "\n  Должность: " +
                "\n  Зарплата: " +
                "\n  ID User: ");

            int poz = Menu.strela(1, 9);
            List<int> index = new List<int>();

            List<int> ID = new List<int>();
            for (int i = 0; i < count; i++) { ID.Add(employees[i].ID_Employee); }

            List<string> SurName = new List<string>();
            for (int i = 0; i < count; i++) { SurName.Add(employees[i].surName); }

            List<string> name = new List<string>();
            for (int i = 0; i < count; i++) { name.Add(employees[i].name); }

            List<string> midlleName = new List<string>();
            for (int i = 0; i < count; i++) { midlleName.Add(employees[i].midlleName); }

            List<string> dateBorn = new List<string>();
            for (int i = 0; i < count; i++) { dateBorn.Add(employees[i].dateBorn); }

            List<string> pasData = new List<string>();
            for (int i = 0; i < count; i++) { pasData.Add(employees[i].pasData); }

            List<string> role = new List<string>();
            for (int i = 0; i < count; i++) { role.Add(employees[i].role); }

            List<int> salary = new List<int>();
            for (int i = 0; i < count; i++) { salary.Add(employees[i].salary); }

            List<int> IDrole = new List<int>();
            for (int i = 0; i < count; i++) { IDrole.Add(employees[i].userRole_ID); }


            string searchstr;
            int searchint;

            while (true)
            {

                if ((SystemKey)Menu.key.Key == SystemKey.Escape)
                {
                    break;
                }

                switch (poz)
                {

                    case 1:
                        Console.SetCursorPosition(0, 11);
                        searchint = Convert.ToInt32(Console.ReadLine());
                        index = Admin.VivodSearh<int>(ID, searchint);
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
                        Console.SetCursorPosition(0, 11);
                        searchstr = Console.ReadLine();
                        index = Admin.VivodSearh<string>(SurName, searchstr);
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
                        Console.SetCursorPosition(0, 11);
                        searchstr = Console.ReadLine();
                        index = Admin.VivodSearh<string>(name, searchstr);
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
                        Console.SetCursorPosition(0, 11);
                        searchstr = Console.ReadLine();
                        index = Admin.VivodSearh<string>(midlleName, searchstr);
                        if (index.Count != 0)
                        {
                            ReadSearch(index);
                        }
                        else
                        {
                            Console.WriteLine("Элемент не найден");
                        }
                        break;
                    case 5:
                        Console.SetCursorPosition(0, 11);
                        searchstr = Console.ReadLine();
                        index = Admin.VivodSearh<string>(dateBorn, searchstr);
                        if (index.Count != 0)
                        {
                            ReadSearch(index);
                        }
                        else
                        {
                            Console.WriteLine("Элемент не найден");
                        }
                        break;
                    case 6:
                        Console.SetCursorPosition(0, 11);
                        searchstr = Console.ReadLine();
                        index = Admin.VivodSearh<string>(pasData, searchstr);
                        if (index.Count != 0)
                        {
                            ReadSearch(index);
                        }
                        else
                        {
                            Console.WriteLine("Элемент не найден");
                        }
                        break;
                    case 7:
                        Console.SetCursorPosition(0, 11);
                        searchstr = Console.ReadLine();
                        index = Admin.VivodSearh<string>(role, searchstr);
                        if (index.Count != 0)
                        {
                            ReadSearch(index);
                        }
                        else
                        {
                            Console.WriteLine("Элемент не найден");
                        }
                        break;
                    case 8:
                        Console.SetCursorPosition(0, 11);
                        searchint = Convert.ToInt32(Console.ReadLine());
                        index = Admin.VivodSearh<int>(salary, searchint);
                        if (index.Count != 0)
                        {
                            ReadSearch(index);
                        }
                        else
                        {
                            Console.WriteLine("Элемент не найден");
                        }
                        break;
                    case 9:
                        Console.SetCursorPosition(0, 11);
                        searchint = Convert.ToInt32(Console.ReadLine());
                        index = Admin.VivodSearh<int>(IDrole, searchint);
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
            void ReadSearch(List<int> index)
            {
                Console.Clear();
                int j = 4;
                Console.SetCursorPosition(2, 3);
                Console.WriteLine("ID");
                Console.SetCursorPosition(10, 3);
                Console.WriteLine("Фамилия");
                Console.SetCursorPosition(30, 3);
                Console.WriteLine("Имя");
                Console.SetCursorPosition(50, 3);
                Console.WriteLine("Отчество");
                Console.SetCursorPosition(70, 3);
                Console.WriteLine("Роль");
                Console.SetCursorPosition(90, 3);
                Console.WriteLine("ID User");

                foreach (int i in index)
                {
                    Console.SetCursorPosition(2, j);
                    Console.WriteLine(employees[i].ID_Employee);
                    Console.SetCursorPosition(10, j);
                    Console.WriteLine(employees[i].surName);
                    Console.SetCursorPosition(30, j);
                    Console.WriteLine(employees[i].name);
                    Console.SetCursorPosition(50, j);
                    Console.WriteLine(employees[i].midlleName);
                    Console.SetCursorPosition(70, j);
                    Console.WriteLine(employees[i].role);
                    Console.SetCursorPosition(90, j);
                    Console.WriteLine(employees[i].userRole_ID);

                    j++;
                }

                bool isRun = false;
                while (!isRun)
                {
                    int poz = Menu.strela(4, index.Count + 3);

                    switch ((SystemKey)Menu.key.Key)
                    {
                        case SystemKey.Enter:
                            Read(poz - 4);
                            isRun = true;
                            break;

                        case SystemKey.Escape:
                            isRun = true;
                            break;

                    }
                }
            }
        }

        public void PersonnelMain()
        {
            bool isRun = false;
            while (!isRun)
            {
                int poz = ReadAll();
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

    }
}
