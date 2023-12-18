using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static belochka.Menu;

namespace belochka
{
    internal class Storage : ICrud
    {
        public List<Product> products;

        public Storage()
        {
            products = SerDeser.DeserData<Product>("storage.json");
        }




        public void Create()
        {
            Console.Clear();
            Product product = new Product();
            bool isRun = false;
            Console.WriteLine("Создание работника" +
                "\n  ID: " +
                "\n  Наименование: " +
                "\n  Цена за штуки: " +
                "\n  Кол-во на складе: ");
            Menu.CreateMenu();



            while (!isRun)
            {
                int poz = Menu.strela(1, 4);

                if ((SystemKey)Menu.key.Key == SystemKey.Escape)
                {
                    break;
                }
                else if ((SystemKey)Menu.key.Key == SystemKey.S)
                {
                    products.Add(product);
                    SerDeser.SerData<Product>(products, "storage.json");
                    break;
                }

                switch (poz)
                {
                    case 1:
                        Console.SetCursorPosition(6, 1);
                        product.ID_product = CheckType.CheckInt(6, 1);
                        break;
                    case 2:
                        Console.SetCursorPosition(17, 2);
                        product.name = Console.ReadLine();
                        break;
                    case 3:
                        Console.SetCursorPosition(17, 3);
                        product.price = CheckType.CheckInt(17, 3);
                        break;
                    case 4:
                        Console.SetCursorPosition(20, 4);
                        product.quantity = CheckType.CheckInt(20, 4);
                        break;
                }
            }
        }


        public int ReadAll()
        {
            Console.Clear();
            string hello = Menu.Hello();
            int max = products.Count() + 3;
            int j = 4;
            Console.WriteLine("Вечер добрый  " + hello);
            Console.SetCursorPosition(2, 3);
            Console.WriteLine("ID");
            Console.SetCursorPosition(10, 3);
            Console.WriteLine("Наименования");
            Console.SetCursorPosition(30, 3);
            Console.WriteLine("Цена за шт.");
            Console.SetCursorPosition(50, 3);
            Console.WriteLine("Кол-во на складе");
            Menu.ReadAllMenu();

            for (int i = 0; i <products.Count(); i++)
            {
                Console.SetCursorPosition(2, j);
                Console.WriteLine(products[i].ID_product);
                Console.SetCursorPosition(10, j);
                Console.WriteLine(products[i].name);
                Console.SetCursorPosition(30, j);
                Console.WriteLine(products[i].price);
                Console.SetCursorPosition(50, j);
                Console.WriteLine(products[i].quantity);
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
                    products.RemoveAt(poz);
                    SerDeser.SerData(products, "storage.json");
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
                $"\n  ID: {products[poz].ID_product}" +
                $"\n  Наименование: {products[poz].name}" +
                $"\n  Цена за штуку: {          products[poz].price}" +
                $"\n  Кол-во на складе: {products[poz].quantity}");
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

            Product product = products[poz];
            bool isRun = false;
            while (!isRun)
            {

                if ((SystemKey)Menu.key.Key == SystemKey.Escape)
                {
                    break;
                }
                else if ((SystemKey)Menu.key.Key == SystemKey.S)
                {
                    products.RemoveAt(poz);
                    products.Insert(poz, product);
                    SerDeser.SerData<Product>(products, "storage.json");
                    break;
                }

                switch (poz1)
                {
                    case 1:
                        Console.SetCursorPosition(6, 1);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(7, 1);
                        product.ID_product = CheckType.CheckInt(7, 1);
                        break;
                    case 2:
                        Console.SetCursorPosition(16, 2);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(18, 2);
                        product.name = Console.ReadLine();
                        break;
                    case 3:
                        Console.SetCursorPosition(17, 3);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(18, 3);
                        product.price = CheckType.CheckInt(18, 3);
                        break;
                    case 4:
                        Console.SetCursorPosition(20, 4);
                        Console.WriteLine("                                         ");
                        Console.SetCursorPosition(21, 4);
                        product.quantity = CheckType.CheckInt(21, 4);
                        break;
                }

                poz1 = Menu.strela(1, 4);

            }
        }

        public void Search()
        {
            
            int count = products.Count();
            Console.Clear();
            Console.WriteLine("" +
                "\n  ID" +
                "\n  Наименование" +
                "\n  Цена за штуку" +
                "\n  Кол-во на складе");

            int poz = Menu.strela(1, count + 1);

            List<int> index = new List<int>();
            List<int> ID = new List<int>();
            for(int i = 0; i < count; i++) { ID.Add(products[i].ID_product); }

            List<string> name = new List<string>();
            for(int i = 0; i < count; i++) { name.Add(products[i].name); }

            List<int> price = new List<int>();
            for(int i = 0; i < count; i++) { price.Add(products[i].price); }

            List<int> quantity = new List<int>();
            for (int i = 0; i < count; i++) { quantity.Add(products[i].quantity); }

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
                        Console.SetCursorPosition(0, 6);
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
                        Console.SetCursorPosition(0, 6);
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
                        Console.SetCursorPosition(0, 6);
                        searchint = Convert.ToInt32(Console.ReadLine());
                        index = Admin.VivodSearh<int>(price, searchint);
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
                        Console.SetCursorPosition(0, 6);
                        searchint = Convert.ToInt32(Console.ReadLine());
                        index = Admin.VivodSearh<int>(quantity, searchint);
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
            int j = 4;
            Console.SetCursorPosition(2, 3);
            Console.WriteLine("ID");
            Console.SetCursorPosition(10, 3);
            Console.WriteLine("Наименования");
            Console.SetCursorPosition(30, 3);
            Console.WriteLine("Цена за шт.");
            Console.SetCursorPosition(50, 3);
            Console.WriteLine("Кол-во на складе");


            foreach(int i in index)
            {
                Console.SetCursorPosition(2, j);
                Console.WriteLine(products[i].ID_product);
                Console.SetCursorPosition(10, j);
                Console.WriteLine(products[i].name);
                Console.SetCursorPosition(30, j);
                Console.WriteLine(products[i].price);
                Console.SetCursorPosition(50, j);
                Console.WriteLine(products[i].quantity);
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


        public void SrorageMain()
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