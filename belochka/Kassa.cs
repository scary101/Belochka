using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static belochka.Menu;

namespace belochka
{
    internal class Kassa
    {
        public List<Buy> buys = new List<Buy>();
        public int final = 0;
        public Kassa()
        {
            Storage storage = new Storage();

            for (int i = 0; i < storage.products.Count();  i++)
            {
                Buy buy = new Buy(storage.products[i].ID_product, storage.products[i].name, storage.products[i].price, storage.products[i].quantity);
                buys.Add(buy);
            }
        }

        public int Read()
        {
            string hello = Menu.Hello();
            Console.Clear();
            Console.WriteLine("Вечер добрый  " + hello);
            Console.SetCursorPosition(2, 3);
            Console.WriteLine("ID");
            Console.SetCursorPosition(10, 3);
            Console.WriteLine("Наименования");
            Console.SetCursorPosition(30, 3);
            Console.WriteLine("Цена за шт.");
            Console.SetCursorPosition(50, 3);
            Console.WriteLine("В корзине");
            Console.SetCursorPosition(67, 3);
            Console.WriteLine("Сумма в чеке");

            Console.SetCursorPosition(80, 1);
            Console.WriteLine("Завершить заказ - S");
            Console.SetCursorPosition(80, 2);
            Console.WriteLine("Выйти в HUB - Esc");


            int j = 4;
            for (int i = 0; i < buys.Count(); i++)
            {
                Console.SetCursorPosition(2, j);
                Console.WriteLine(buys[i].ID_product);
                Console.SetCursorPosition(10, j);
                Console.WriteLine(buys[i].name);
                Console.SetCursorPosition(30, j);
                Console.WriteLine(buys[i].price);
                Console.SetCursorPosition(50, j);
                Console.WriteLine(buys[i].selected);
                Console.SetCursorPosition(67, j);
                Console.WriteLine(buys[i].selected * buys[i].price);
                j++;
            }
            Console.SetCursorPosition(67, buys.Count() + 6);
            Console.WriteLine("Итог");
            Console.SetCursorPosition(67, buys.Count() + 7);
            Console.WriteLine(final + "руб.");
            int poz = Menu.strela(4, buys.Count() + 3);
            return poz - 4;
        }
        public void Korzina(int poz)
        {
            int price = buys[poz].price;
            int sklad = buys[poz].quantity;
            while (true)
            {
                
                key = Console.ReadKey(true);

                if ((SystemKey)key.Key == SystemKey.S)
                {
                    break;
                }
                else if ((SystemKey)key.Key == SystemKey.Escape)
                {
                    buys[poz].selected = 0;
                    break;
                }

                if ((SystemKey)key.Key == SystemKey.Plus && buys[poz].selected < sklad)
                {
                    final += price;
                    buys[poz].selected++;
                }
                else if ((SystemKey)key.Key == SystemKey.Minus && buys[poz].selected > 0)
                {
                    final -= price;
                    buys[poz].selected--;
                }
                Console.SetCursorPosition(13, 4);
                Console.WriteLine(buys[poz].selected);
            }
        }

        public void ReadProduct(int poz)
        {
            Console.Clear();

            Console.WriteLine(" " +
                "\n  ID: " +
                "\n  Наименования: " +
                "\n  Цена за шт.: " +
                "\n  В корзине: ");


            Console.SetCursorPosition(6, 1);
            Console.WriteLine(buys[poz].ID_product);
            Console.SetCursorPosition(16, 2);
            Console.WriteLine(buys[poz].name);
            Console.SetCursorPosition(15, 3);
            Console.WriteLine(buys[poz].price);

            Console.SetCursorPosition(30, 1);
            Console.WriteLine("Положить в корзину - S");
            Console.SetCursorPosition(30, 2);
            Console.WriteLine("Выйти в каталог - Esc");

            Korzina(poz);
        }

        public void KassaMain()
        {
            bool isRun = false;
            Storage storage = new Storage();
            Money money = new Money();    
            while (!isRun)
            {
                int poz = Read();

                switch((SystemKey)Menu.key.Key)
                {
                    case SystemKey.S:
                        Accounting zakaz = new Accounting();
                        zakaz.sum = final;
                        zakaz.name = "Продажа товара";
                        zakaz.date = Convert.ToString(DateTime.Now);
                        zakaz.typeOperation = true;
                        zakaz.ID_Accounting = 1;
                        money.money.Add(zakaz);
                        SerDeser.SerData(money.money, "money.json");

                        for(int i = 0; i < storage.products.Count();i++)
                        {
                            Product product = storage.products[i];
                            int minus = buys[i].selected;
                            product.quantity = product.quantity - minus;
                            storage.products.RemoveAt(i);
                            storage.products.Insert(i, product);
                            SerDeser.SerData(storage.products, "storage.json");
                        }
                        isRun = true;
                        break;
                    case SystemKey.Escape:
                        isRun = true;
                        break;
                    case SystemKey.Enter:
                        ReadProduct(poz);
                        break;
                }  
            }
        }

    }
}
