using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ClassLibrary10;

namespace лаба_12_4_часть
{
    internal class Program
    {
        static int Number(int minValue, int maxValue, string msg = "") // Ввод числа от minValue доmaxValue
        {
            Console.Write(msg + $" (целое число от {minValue} до {maxValue}): ");
            int number;
            bool isConvert;
            do
            {
                string buf = Console.ReadLine();
                isConvert = int.TryParse(buf, out number);
                if (!isConvert || number < minValue || number > maxValue)
                    Console.WriteLine("Неправильно введено число. \nПопробуйте еще раз.");
            } while (!isConvert || number < minValue || number > maxValue);
            return number;
        }
        static void Main(string[] args)
        {
            HashTable<string, BankCard> table = new HashTable<string, BankCard>();
            HashTable<string, BankCard> TempTable = table;
            int answer;
            do
            {
                Console.WriteLine("\n1. Создать таблицу");
                Console.WriteLine("2. Распечатать таблицу");
                Console.WriteLine("3. Поиск и удаление");
                Console.WriteLine("4. Добавление элемента в список");
                Console.WriteLine("------ 4 часть ------");
                Console.WriteLine("5. Получение знначения по ключу TryGetValue");
                Console.WriteLine("6. Копирование элементов в массив CopyTo");
                Console.WriteLine("7. Глубокая копия таблицы");
                Console.WriteLine("8. Поверхностное копирование");
                Console.WriteLine("9. Сменить таблицу на копию/оригинал");
                Console.WriteLine("10. Удалить таблицу");
                Console.WriteLine("11. Свойства");
                Console.WriteLine("12. Выход");
                answer = Number(1, 12, "Выберите нoмер задания");
                switch (answer)
                {
                    case 1:     //Создать таблицу                
                        {
                            int size = Number(1, int.MaxValue, "Введите размер таблицы");
                            table = new HashTable<string, BankCard>(size);
                            int count = Number(0, 24, "Введите количество элементов таблицы");  //В библиотеке 24 уникальных имени
                            for (int i = 0; i < count; i++)
                            {
                                CreditCard planet = new CreditCard();
                                planet.RandomInit();
                                try
                                {
                                    table.Add(planet.Name, planet);
                                }
                                catch
                                {
                                    i--;
                                }
                            }
                            break;
                        }
                    case 2:  //Распечатать таблицу
                        {
                            if (table == null || table.Count == 0)
                                Console.WriteLine("\nТаблица пустая");
                            else
                                table.PrintTable();
                            break;
                        }
                    case 3:  //Поиск и удаление элемента
                        {
                            if (table.Count == 0)
                                Console.WriteLine("\nТаблица пустая");
                            else
                            {
                                Console.WriteLine("Введите элемент для поиска");
                                CreditCard planet = new CreditCard();
                                planet.Init();                            //Ввод элемента для поиска и удаления   
                                KeyValuePair<string, BankCard> pair = new KeyValuePair<string, BankCard>(planet.Name, planet);
                                do
                                {
                                    Console.WriteLine("\n1. Поиск элемента по ключу");
                                    Console.WriteLine("2. Поиск пары ключ-значение");
                                    Console.WriteLine("3. Удаление элемента по ключу");
                                    Console.WriteLine("4. Удаление пары ключ-значение");
                                    Console.WriteLine("5. Назад");
                                    answer = Number(1, 5, "Выберите нoмер задания");
                                    switch (answer)
                                    {
                                        case 1:   //Поиск элемента по ключу
                                            {
                                                bool ok = table.ContainsKey(planet.Name);
                                                if (ok)
                                                    Console.WriteLine($"\nЭлемент найден");
                                                else
                                                    Console.WriteLine($"\nЭлемент не найден");
                                                break;
                                            }
                                        case 2:   //Поиск элемента по значению
                                            {
                                                bool ok = table.Contains(pair);
                                                if (ok)
                                                    Console.WriteLine($"\nЭлемент найден");
                                                else
                                                    Console.WriteLine($"\nЭлемент не найден");
                                                break;
                                            }
                                        case 3:  //Удаление элемента по ключу
                                            {
                                                bool ok = table.Remove(planet.Name);
                                                if (ok)
                                                    Console.WriteLine($"\nЭлемент удален");
                                                else
                                                    Console.WriteLine($"\nЭлемент не найден");
                                                if (table.Count == 0)
                                                    answer = 5;
                                                break;
                                            }
                                        case 4:  //Удаление элемента по значению
                                            {
                                                bool ok = table.Remove(pair);
                                                if (ok)
                                                    Console.WriteLine($"\nЭлемент удален");
                                                else
                                                    Console.WriteLine($"\nЭлемент не найден");
                                                if (table.Count == 0)
                                                    answer = 5;
                                                break;
                                            }
                                    }
                                } while (answer != 5);
                            }
                            break;
                        }
                    case 4:  //Добавление элемента в список
                        {
                            CreditCard planet = new CreditCard();
                            Console.WriteLine("\n1. Добавление случайного элемента");
                            Console.WriteLine("2. Ввод элемента с клавиатуры");
                            answer = Number(1, 2, "Выберите нoмер задания");
                            switch (answer)
                            {
                                case 1:
                                    {
                                        planet.RandomInit();
                                        break;
                                    }
                                case 2:
                                    {
                                        Console.WriteLine("Введите элемент");
                                        planet.Init();
                                        break;
                                    }
                            }
                            try
                            {
                                KeyValuePair<string, BankCard> pair = new KeyValuePair<string, BankCard>(planet.Name, planet);
                                table.Add(pair);
                                Console.WriteLine($"\nЭлемент добавлен");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.WriteLine("Элемент не добавлен");
                            }
                            break;
                        }
                    case 5:  //Получение знначения по ключу TryGetValue
                        {
                            if (table == null || table.Count == 0)
                                Console.WriteLine("\nТаблица пустая");
                            else
                            {
                                Console.WriteLine("Введите имя");
                                string name = Console.ReadLine();
                                bool exist = table.TryGetValue(name, out var value);
                                if (exist)
                                {
                                    Console.WriteLine($"Значение найдено: {value}");
                                }
                                else
                                {
                                    Console.WriteLine("Значение не найдено");
                                }
                            }

                            break;
                        }
                    case 6:  //CopyTo
                        {
                            if (table == null || table.Count == 0)
                                Console.WriteLine("\nТаблица пустая");
                            else
                            {
                                int capacity = table.Count;  //Количество элементов в таблице
                                KeyValuePair<string, BankCard>[] array = new KeyValuePair<string, BankCard>[capacity];
                                try
                                {
                                    table.CopyTo(array, 0); //Копирование элементов в массив
                                    Console.WriteLine("\n1. Распечатать массив");
                                    Console.WriteLine("2. Назад");
                                    answer = Number(1, 2, "Выберите нoмер задания");
                                    switch (answer)
                                    {
                                        case 1:   //Печать массива
                                            {
                                                foreach (var item in array)
                                                    Console.WriteLine(item);
                                                break;
                                            }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            break;
                        }
                    case 7:  //Клонирование
                        {
                            table["Планета"] = new BankCard();
                            Console.WriteLine("В таблицу добавлен элемент с ключом \"Планета\"");
                            HashTable<string, BankCard> clone = table.Clone();
                            clone["Планета"] = new CreditCard(765,"Clon", DateTime.MaxValue, 1000, 10, 10);
                            Console.WriteLine("В таблице-клоне изменен элемент с ключом \"Планета\"");
                            Console.WriteLine("Значение оригинала: " + table["Планета"]);
                            Console.WriteLine("Значение клона: " + clone["Планета"]);
                            Console.WriteLine("\n1. Продолжить изменять оригинал");
                            Console.WriteLine("2. Изменять копию");
                            answer = Number(1, 2, "Выберите таблицу");
                            switch (answer)
                            {
                                case 1:   //оригинал
                                    {
                                        TempTable = clone; //Временно запомнили копию
                                        break;
                                    }
                                case 2:   //клон
                                    {
                                        TempTable = table; //Временно запомнили оригинал
                                        table = clone;
                                        break;
                                    }
                            }
                            break;
                        }
                    case 8:  //Поверхностная копия
                        {
                            table["Звезда"] = new YoungCard();
                            Console.WriteLine("В таблицу добавлен элемент с ключом \"Звезда\":" + table["Звезда"]);
                            HashTable<string, BankCard> copy = table.ShallowCopy();
                            copy["Звезда"] = new YoungCard(87876,"Копия", DateTime.Now, 100, 55, 3600);
                            Console.WriteLine("В таблице-копии изменен элемент с ключом \"Звезда\"");
                            Console.WriteLine("Значение оригинала: " + table["Звезда"]);
                            Console.WriteLine("Значение копии: " + copy["Звезда"]);
                            //copy.PrintTable();
                            break;
                        }
                    case 9:  //Поменять таблицу
                        {
                            if (TempTable == table)  //Ссылки совпадают
                                Console.WriteLine("\nДругой таблицы нет");
                            else
                            {
                                HashTable<string, BankCard> temp = table;
                                table = TempTable;
                                TempTable = temp;
                                Console.WriteLine("\nТаблица переключена");
                            }
                            break;
                        }
                    case 10:  //Удалить таблицу
                        {
                            if (table == null || table.Count == 0)
                                Console.WriteLine("\nТаблица была пустой");
                            else
                            {
                                table.Clear();
                                Console.WriteLine("\nТаблица удалена");
                            }
                            break;
                        }
                    case 11:  //Свойства
                        {
                            if (table == null || table.Count == 0)
                                Console.WriteLine("\nТаблица пустая");
                            else
                            {
                                do
                                {
                                    Console.WriteLine("\n1. Count, Capacity");
                                    Console.WriteLine("2. Коллекция ключей ICollection<TKey> Keys");
                                    Console.WriteLine("3. Коллекция значений List<TValue> ValuesList");
                                    Console.WriteLine("4. Свойство IsReadOnly");
                                    Console.WriteLine("5. Индексатор доступа по ключу");
                                    Console.WriteLine("6. Нумератор (Печать элементов таблицы)");
                                    Console.WriteLine("7. Конструктор копирования");
                                    Console.WriteLine("8. Назад");
                                    answer = Number(1, 8, "Выберите нoмер задания");
                                    switch (answer)
                                    {
                                        case 1:     //Count, Capacity
                                            {
                                                Console.WriteLine($"Количесвто элементов = {table.Count}");
                                                Console.WriteLine($"Размер таблицы = {table.Capacity}");
                                                break;
                                            }
                                        case 2:     //Коллекция ключей ICollection<TKey> Keys
                                            {
                                                Console.WriteLine("\nСписок ключей:");
                                                foreach (var key in table.Keys)
                                                {
                                                    Console.WriteLine(key);
                                                }
                                                break;
                                            }
                                        case 3:     //Коллекция значений List<TValue> ValuesList
                                            {
                                                Console.WriteLine("\nСписок значений:");
                                                foreach (var value in table.Values)
                                                {
                                                    Console.WriteLine(value);
                                                }
                                                break;
                                            }
                                        case 4:     //Свойство IsReadOnly
                                            {
                                                if (table.IsReadOnly)
                                                    Console.WriteLine("\nКоллекция доступна только для чтения");
                                                else
                                                    Console.WriteLine("\nКоллекция доступна НЕ только для чтения");
                                                break;
                                            }
                                        case 5:     //Индексатор доступа по ключу
                                            {
                                                Console.WriteLine("\nВведите ключ - имя небесного тела");
                                                string name = Console.ReadLine();
                                                Regex pattern = new Regex(@"[А-Яа-яA-Za-z0-9]+");  //При вводе пустой строки имя будет изменено на "No name"
                                                if (!pattern.IsMatch(name))
                                                    name = "No name";
                                                Console.WriteLine("\n1. Чтение значения элемента");
                                                Console.WriteLine("2. Запись значения элемента");
                                                answer = Number(1, 2, "Выберите таблицу");
                                                switch (answer)
                                                {
                                                    case 1:   //get
                                                        {
                                                            try
                                                            {
                                                                Console.WriteLine(table[name]);
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                Console.WriteLine(ex.Message);
                                                            }
                                                            break;
                                                        }
                                                    case 2:   //set
                                                        {
                                                            Console.WriteLine("\nВведите значение - небесное тело");
                                                            BankCard celbody = new BankCard();
                                                            celbody.Init();
                                                            table[name] = celbody;
                                                            Console.WriteLine("\nЗначение добавлено/изменено");
                                                            break;
                                                        }
                                                }
                                                break;
                                            }
                                        case 6:     //Печать элементов таблицы Нумератор
                                            {
                                                foreach (var item in table)
                                                {
                                                    Console.WriteLine(item);
                                                }
                                                break;
                                            }
                                        case 7:     //Контруктор копирования коллекции
                                            {
                                                HashTable<string, BankCard> CopyTable = new HashTable<string, BankCard>(table);
                                                Console.WriteLine("\nЭлементы скопированной таблицы:");
                                                foreach (var item in CopyTable)
                                                {
                                                    Console.WriteLine(item);
                                                }
                                                break;
                                            }
                                    }
                                } while (answer != 8);
                            }
                            break;
                        }
                }
            } while (answer != 12);
        }
    }
}
