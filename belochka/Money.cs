using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static belochka.Menu;

namespace belochka
{
    internal class Money : ICrud
    {
        public List<Accounting> money;
        public Money()
        {
            money = SerDeser.DeserData<Accounting>("money.json");
        }

        public void Create()
        {
            Accounting accounting = new Accounting();
            bool isRun = false;
            Console.Clear();
            Console.WriteLine("" +
                "\n  ID: " +
                "\n  Название: " +
                "\n  Сумма: " +
                "\n  Дата: " +
                "\n  Приход/уход: ");
            Menu.CreateMenu();

            while (!isRun)
            {
                int poz = Menu.strela(1, 5);
                if ((SystemKey)Menu.key.Key == SystemKey.Escape)
                {
                    break;
                }
                else if ((SystemKey)Menu.key.Key == SystemKey.S)
                {
                    money.Add(accounting);
                    SerDeser.SerData<Accounting>(money, "money.json");
                    break;
                }

                switch (poz)
                {
                    case 1:
                        Console.SetCursorPosition(7, 1);
                        accounting.ID_Accounting = CheckType.CheckInt(7, 1);
                        break;
                    case 2:
                        Console.SetCursorPosition(13, 2);
                        accounting.name = Console.ReadLine();
                        break;
                    case 3:
                        Console.SetCursorPosition(9, 3);
                        accounting.sum = Convert.ToInt32(Console.ReadLine());
                        break;
                    case 4:
                        Console.SetCursorPosition(8, 4);
                        accounting.date = Console.ReadLine();
                        break;
                    case 5:
                        Console.SetCursorPosition(17, 5);
                        accounting.typeOperation = CheckType.CheckBool(17, 5);
                        break;
                }
            }
        }

        public int ReadAll()
        {
            Console.Clear();

            string hello = Menu.Hello();
            int j = 4;
            int max = money.Count() + 3;
            int itog = Finaly();
            Console.WriteLine("Вечер добрый  " + hello);
            Console.SetCursorPosition(2, 3);
            Console.WriteLine("ID");
            Console.SetCursorPosition(10, 3);
            Console.WriteLine("Название");
            Console.SetCursorPosition(30, 3);
            Console.WriteLine("Сумма");
            Console.SetCursorPosition(40, 3);
            Console.WriteLine("Дата транзакции");
            Console.SetCursorPosition(62, 3);
            Console.WriteLine("Поступление/уход");
            Console.SetCursorPosition(85, 3);
            Console.WriteLine("Итог руб.");
            Console.SetCursorPosition(85, 4);
            Console.WriteLine(itog);
            Menu.ReadAllMenu();


            for (int i = 0; i < money.Count(); i++)
            {
                Console.SetCursorPosition(2, j);
                Console.WriteLine(money[i].ID_Accounting);
                Console.SetCursorPosition(10, j);
                Console.WriteLine(money[i].name);
                Console.SetCursorPosition(30, j);
                Console.WriteLine(money[i].sum);
                Console.SetCursorPosition(40, j);
                Console.WriteLine(money[i].date);
                Console.SetCursorPosition(62, j);
                Console.WriteLine(money[i].typeOperation);
                j++;    
            }
            int poz = Menu.strela(4, max);

            return poz - 4;

        }


        public void Delete(int poz)
        {
            Console.Clear();
            Console.WriteLine("Вы уверены что хотите удалить запись? Да/Нет");
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Вы уверены что хотите удалить запись? Да/Нет");
                string otvet = Console.ReadLine();
                if (otvet == "Да")
                {
                    money.RemoveAt(poz);
                    SerDeser.SerData(money, "money.json");
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

            Console.WriteLine("" +
                $"\n  ID {money[poz].ID_Accounting}" +
                $"\n  Название {money[poz].name}" +
                $"\n  Сумма {money[poz].sum}" +
                $"\n  Дата {money[poz].date}" +
                $"\n  Приход/уход {money[poz].typeOperation}");
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

            Accounting accounting = money[poz];

            bool isRun = false;

            while (!isRun)
            {

                if ((SystemKey)Menu.key.Key == SystemKey.Escape)
                {
                    break;
                }
                else if ((SystemKey)Menu.key.Key == SystemKey.S)
                {
                    money.RemoveAt(poz);
                    money.Insert(poz, accounting);
                    SerDeser.SerData(money, "money.json");
                    break;
                }

                switch (poz1)
                {
                    case 1:
                        Console.SetCursorPosition(5, 1);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(8, 1);
                        accounting.ID_Accounting = CheckType.CheckInt(8, 1);
                        break;
                    case 2:
                        Console.SetCursorPosition(11, 2);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(14, 2);
                        accounting.name = Console.ReadLine();
                        break;
                    case 3:
                        Console.SetCursorPosition(7, 3);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(10, 3);
                        accounting.sum = CheckType.CheckInt(10, 3);
                        break;
                    case 4:
                        Console.SetCursorPosition(6, 4);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(9, 4);
                        accounting.date = Console.ReadLine();
                        
                        break;
                    case 5:
                        Console.SetCursorPosition(15, 5);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(18, 5);
                        accounting.typeOperation = CheckType.CheckBool(18, 5);
                        break;
                }
                poz1 = Menu.strela(0, 5);
            }
        }


        private int Finaly()
        {

            List<int> sum = new List<int>();
            for (int i = 0; i < money.Count(); i++) { sum.Add(money[i].sum); }

            List<bool> oper = new List<bool>();
            for (int i = 0; i < money.Count(); i++) { oper.Add(money[i].typeOperation); }

            int itog = 0;
            int shet = 0;
            foreach (bool i in oper)
            {
                  
                if (i == true)
                {
                    itog += sum[shet];
                }
                else
                {
                    if (itog <= 0)
                    {
                        continue;
                    }
                    else
                    {
                        itog -= sum[shet];
                    }
                }
                shet++;
            }
            return itog;

        }

        public void Search()
        {
            Console.Clear();
            int count = money.Count();
            Console.WriteLine("     Поиск сотруднкиа " +
                "\n  ID" +
                "\n  Наименование" +
                "\n  Сумма" +
                "\n  Дата" +
                "\n  Приход/уход");

            int poz = Menu.strela(1, 5);

            List<int> index = new List<int>();

            List<int> ID = new List<int>();
            for(int i = 0; i < count; i++) { ID.Add(money[i].ID_Accounting);}

            List<string> name = new List<string>();
            for (int i = 0; i < count; i++) { name.Add(money[i].name); }

            List<int> sum = new List<int>();
            for (int i = 0; i < count; i++) { sum.Add(money[i].sum); }

            List<string> date = new List<string>();
            for (int i = 0; i < count; i++) { date.Add(money[i].date); }

            List<bool> type = new List<bool>();
            for (int i = 0; i < count; i++) { type.Add(money[i].typeOperation); }

            string searchstr;
            int searchint;
            bool searchbool;

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
                        Console.SetCursorPosition(0, 7);
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
                    case 3:
                        Console.SetCursorPosition(0, 7);
                        searchint = Convert.ToInt32(Console.ReadLine());
                        index = Admin.VivodSearh<int>(sum, searchint);
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
                        searchstr = Console.ReadLine();
                        index = Admin.VivodSearh<string>(date, searchstr);
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
                        Console.SetCursorPosition(0, 7);
                        searchbool = Convert.ToBoolean(Console.ReadLine());
                        index = Admin.VivodSearh<bool>(type, searchbool);
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
            Console.Clear();
            Money money = new Money();
            int j = 4;
            Console.SetCursorPosition(2, 3);
            Console.WriteLine("ID");
            Console.SetCursorPosition(10, 3);
            Console.WriteLine("Название");
            Console.SetCursorPosition(30, 3);
            Console.WriteLine("Сумма");
            Console.SetCursorPosition(40, 3);
            Console.WriteLine("Дата транзакции");
            Console.SetCursorPosition(62, 3);
            Console.WriteLine("Поступление/уход");
            Console.SetCursorPosition(85, 3);
            Console.WriteLine("Итог руб.");


            foreach(int i in index)
            {

                Console.SetCursorPosition(2, j);
                Console.WriteLine(money.money[i].ID_Accounting);
                Console.SetCursorPosition(10, j);
                Console.WriteLine(money.money[i].name);
                Console.SetCursorPosition(30, j);
                Console.WriteLine(money.money[i].sum);
                Console.SetCursorPosition(40, j);
                Console.WriteLine(money.money[i].date);
                Console.SetCursorPosition(62, j);
                Console.WriteLine(money.money[i].typeOperation);
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
        public void MoneyMain()
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
