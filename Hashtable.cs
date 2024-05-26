using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary10;

namespace лаба_12_4_часть
{
    public class HashTable<TKey, TValue> : IDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : ICloneable where TValue : ICloneable
    {
        int count = 0;                                  //Счетчик количества элементов в таблице
        Point<TKey, TValue>[] table;
        private List<TKey> КeysList = new List<TKey>();       //Список ключей
        private List<TValue> ValuesList = new List<TValue>(); //Список значений
        public int Capacity => table.Length;              //Свойство для чтения размера таблицы
        public int Count => count;                        //Свойство для чтения количества элементов в таблице
        public ICollection<TKey> Keys => КeysList;        //Свойство для чтения коллекции ключей
        public ICollection<TValue> Values => ValuesList;  //Свойство для чтения коллекции значений
        public bool IsReadOnly => false;                  //Коллекция доступна не только для чтения

        public TValue this[TKey key]  //Индексатор для доступа к значению по ключу
        {
            get  //Свойство для чтения
            {
                int index = GetIndex(key);
                if (table[index] == null)
                    throw new Exception("Ключ в коллекции не найден");
                foreach (Point<TKey, TValue> item in table[index])        //Поиск значениия в цепочке
                {
                    if (item.Key.Equals(key))                             //Элемент найден
                        return item.Value;
                }
                throw new Exception("Ключ в коллекции не найден");
            }
            set  //Свойство для записи
            {
                int index = GetIndex(key);
                if (table[index] == null)                                 //Значения по ключу нет
                    table[index] = new Point<TKey, TValue>(key, value);   //Добавление элемента с указанным ключом 
                else                                                      //Цепочка не пустая
                {
                    foreach (Point<TKey, TValue> item in table[index])    //Поиск значениия в цепочке
                    {
                        if (item.Key.Equals(key))                         //Элемент найден
                        {
                            ValuesList.Remove(item.Value);                //Удаление старого значения из списка значеий 
                            item.Value = value;                           //Обновление значения
                            ValuesList.Add(value);                        //Добавление нового значения в список значений 
                            return;
                        }
                    }
                    Point<TKey, TValue> current = table[index];           //Ключ не найден -> добавление нового элемента
                    while (current.Next != null)
                    {
                        current = current.Next;
                    }
                    current.Next = new Point<TKey, TValue>(key, value);   //Добавление нового элемента
                    current.Next.Previous = current;                      //Связь нового элемента с предыдущим
                }
                if (!КeysList.Contains(key))                              //Элемента не было в таблице
                {
                    КeysList.Add(key);                                    //Добавление ключа в список ключей 
                    ValuesList.Add(value);                                //Добавление значения в список значений
                    count++;                                              //Увеличение счетчика
                }
            }
        }


        //konsrtuct
        public HashTable(int length = 10)
        {
            table = new Point<TKey, TValue>[length];
        }
        public HashTable(HashTable<TKey, TValue> c)  //Конструктор для копирования коллекции
        {
            table = new Point<TKey, TValue>[c.Capacity];                     //Таблица такого же размера
            foreach (KeyValuePair<TKey, TValue> keyValuePair in c)           //Перебор всех пар ключ-значение
            {
                this[keyValuePair.Key] = (TValue)keyValuePair.Value.Clone(); //Счетчик увеличивается тут же
            }
        }

        public void PrintTable()
        {
            for (int i = 0; i < table.Length; i++)
            {
                Console.WriteLine($"{i}:");                          //Индекс таблицы
                if (table[i] != null)                                //Не пустая ссылка
                {
                    foreach (Point<TKey, TValue> item in table[i])   //Перебор элементов цепочки
                    {
                        Console.WriteLine(item);
                    }
                }
            }
        }
        /// <summary>
        /// Добавление элемента 
        /// </summary>
        /// <exception cref="Exception">Ключ уже есть в таблице</exception>
        public void Add(TKey key, TValue value)
        {
            int index = GetIndex(key);                               //Находит по хэш коду индекс элемента в таблице
            if (table[index] == null)                                //Позиция путсая
                table[index] = new Point<TKey, TValue>(key, value);  //Добавление элемента
            else                                                     //Есть цепочка
            {
                Point<TKey, TValue> current = table[index];
                while (current.Next != null)
                {
                    if (current.Key.Equals(key) || current.Value.Equals(value))
                        throw new Exception("Ключ уже есть в таблице");
                    current = current.Next;
                }
                if (current.Key.Equals(key) || current.Value.Equals(value))
                    throw new Exception("Ключ уже есть в таблице");
                current.Next = new Point<TKey, TValue>(key, value);  //Добавление в конец цепочки
                current.Next.Previous = current;
            }
            КeysList.Add(key);                                       //Добавление ключа в список ключей
            ValuesList.Add(value);                                   //Добавление значения в список значений
            count++;                                                 //Увеличение счетчика
        }
        /// <summary>
        /// Добавление пары ключ-значение
        /// </summary>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }
        /// <summary>
        /// Метод, определяющий наличие элемента по значению ключа
        /// </summary>
        /// <exception cref="Exception">Пустая таблица</exception>
        public bool ContainsKey(TKey key)
        {
            if (count == 0)                                 //Таблица пустая
                throw new Exception("Таблица пустая");      //Сообщение об ошибке
            int index = GetIndex(key);                      //Находит по хэш коду индекс элемента в таблице
            if (table[index] == null)                       //Цепочка пустая, элемента нет
                return false;                               //Элемент не найден                                  
            foreach (Point<TKey, TValue> item in table[index])//Перебор цепочки
            {
                if (item.Key.Equals(key))                   //Нужный элемент найден
                    return true;
            }
            return false;                                   //Элемент не найден
        }
        /// <summary>
        ///  Метод, определяющий наличие пары элементов
        /// </summary>
        /// <exception cref="Exception">Пустая таблица</exception>
        public bool Contains(KeyValuePair<TKey, TValue> pair)
        {
            if (ContainsKey(pair.Key))                        //Ключ найден
                return (this[pair.Key].Equals(pair.Value));  //Значение по ключу соответствует искомому
            return false;                                     //Элемент не найден
        }
        /// <summary>
        /// Метод удаления узла таблицы по ключу
        /// </summary>
        /// <param name="key">Ключ для поиска и удаления элемента</param>
        /// <exception cref="Пустая таблица"></exception>
        public bool Remove(TKey key)
        {
            if (count == 0)                                     //Таблица пустая
                throw new Exception("Таблица пустая");

            int index = GetIndex(key);                          //Находит по хэш коду индекс элемента в таблице
            if (table[index] == null)                           //Цепочка пустая
                return false;                                   //Элемент не удален
            if (table[index].Key.Equals(key))                   //Первый элемент цепочки - удаляемый
                return RemovePoint(table[index]);               //Удаление элемента
            else                                                //Удаляемый элемент не первый
            {
                foreach (Point<TKey, TValue> item in table[index]) //Перебор всех элементов цепочки
                {
                    if (item.Key.Equals(key))                   // Элемент найден
                        return RemovePoint(item);               //Удаление элемента
                }
                return false;                                   //Элемент не найден
            }
        }
        /// <summary>
        /// Удаление пары ключ-значение
        /// </summary>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (this[item.Key].Equals(item.Value))           //По заданному ключу находится заданное значение
                return Remove(item.Key);
            return false;
        }
        /// <summary>
        /// Метод удаления узла из таблицы
        /// </summary>
        /// <param name="item">Удаляемый элемент</param>
        private bool RemovePoint(Point<TKey, TValue> item)
        {
            if (item.Previous == null)                       //Первый элемент цепочки - удаляемый
            {
                if (item.Next == null)                       //Один элемент в цепочке
                    table[GetIndex(item.Key)] = null;        //Удаление ссылки на элемент
                else                                         //Несколько элементов в цепочке
                {
                    table[GetIndex(item.Key)] = item.Next;   //Присваивание ссылки на след. элемент
                    item.Next.Previous = null;               //Удаление связи удаляемого элемента c таблицей
                    item.Next = null;                        //Удаление связи удаляемого элемента c таблицей
                }
            }
            else                                             //Элемент не первый в цепочке
            {
                item.Previous.Next = item.Next;              //Предыдущий связывается с следующим
                if (item.Next != null)                       //Следующий не null
                    item.Next.Previous = item.Previous;      //Связь следующего с предыдущим
                item.Previous = null;                        //Удаление связи удаляемого элемента c таблицей 
                item.Next = null;                            //Удаление связи удаляемого элемента c таблицей 
            }
            КeysList.Remove(item.Key);                       //Удаление ключа из списка ключей
            ValuesList.Remove(item.Value);                   //Удаление значения из списка значений
            count--;                                         //Уменьшение счетчика кол-ва элементов
            return true;
        }
        /// <summary>
        /// Метод получения индекса таблицы по хэш коду ключа
        /// </summary>
        public int GetIndex(TKey key)  //public для тестирования
        {
            return Math.Abs(key.GetHashCode() % Capacity);
        }
        /// <summary>
        /// Удаление таблицы
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                    if (table[i].Next != null)
                        table[i].Next.Previous = null;   //Удаление связи цепочки с таблицей 
                table[i] = null;                         //Удаление первого элемента цепочки 
            }
            count = 0;
        }
        /// <summary>
        /// Копирование элементов в указанный массив начиная с заданного индекса
        /// </summary>
        /// <param name="array">Массив, в который копируются элементы таблицы</param>
        /// <param name="index">Индекс массива, с которого будет начинаться копирование</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            if (array == null)
                throw new Exception("Массив пустой");

            if (index < 0 || index >= array.Length)
                throw new Exception("Индекс находится за пределами массива");

            if (array.Length - index < count)
                throw new Exception("Недостаточно места в массиве для копирования коллекции");

            int CurrentIndex = index;                          //Индекс массива, в который будет записан элемент
            foreach (KeyValuePair<TKey, TValue> item in this)  //Перебор элементов коллекции
            {
                TKey NewKey = (TKey)item.Key.Clone();          //Глубокое копирование
                TValue NewValue = (TValue)item.Value.Clone();  //Глубокое копирование
                array[CurrentIndex] = new KeyValuePair<TKey, TValue>(NewKey, NewValue);
                CurrentIndex++;
            }
        }
        /// <summary>
        /// Получение знначения по ключу
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            int index = GetIndex(key);
            if (table[index] != null)
            {
                foreach (Point<TKey, TValue> item in table[index]) //Поиск элемента в цепочке
                {
                    if (item.Key.Equals(key))
                    {
                        value = item.Value;
                        return true;
                    }
                }
            }
            value = default(TValue);  //Ключ не найден
            return false;
        }
        public HashTable<TKey, TValue> Clone()
        {
            HashTable<TKey, TValue> CloneTable = new HashTable<TKey, TValue>(this.Capacity);
            foreach (KeyValuePair<TKey, TValue> item in this)
            {
                TKey NewKey = (TKey)item.Key.Clone();               //Глубокое копирование
                TValue NewValue = (TValue)item.Value.Clone();       //Глубокое копирование
                CloneTable.Add(NewKey, NewValue);                   //Добавление элемента  
            }
            return CloneTable;
        }
        /// <summary>
        /// Добавление ссылки на цепочку для копирования
        /// </summary>
        private void AddChain(Point<TKey, TValue> point)
        {
            int index = GetIndex(point.Key);                     //Находит по хэш коду индекс элемента в таблице
            if (table[index] == null)                            //Позиция путсая
                table[index] = point;                            //Добавление ссылки на цепочку
        }
        /// <summary>
        /// Поверхностное копирование
        /// </summary>
        public HashTable<TKey, TValue> ShallowCopy()
        {
            HashTable<TKey, TValue> CloneTable = new HashTable<TKey, TValue>(this.Capacity);
            foreach (Point<TKey, TValue> item in table)
            {
                if (item != null)
                {
                    foreach (var point in item)
                        CloneTable.AddChain(point);
                }
            }
            CloneTable.SetCount(this.Count);
            return CloneTable;
        }
        /// <summary>
        /// Обновление счетчика для копии
        /// </summary>
        private void SetCount(int NewCount)
        {
            count = NewCount; // Устанавливаем новое значение счетчика
        }
        /// <summary>
        /// Нумератор для перебора всех элементов таблицы
        /// </summary>

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            //Console.WriteLine("Перебор элементов");
            foreach (Point<TKey, TValue> item in table)
            {
                Point<TKey, TValue> current = item;
                while (current != null)
                {
                    yield return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
                    current = current.Next;
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


    }
}

