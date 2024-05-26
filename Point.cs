using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace лаба_12_4_часть
{
    public class Point<TKey, TValue> : IEnumerable<Point<TKey, TValue>>
    {
        public TKey Key { get; set; }                 //Информационное поле ключ
        public TValue Value { get; set; }             //Информационное поле значение
        public Point<TKey, TValue> Next { get; set; }     //Адресное поле на след. элеменет 
        public Point<TKey, TValue> Previous { get; set; } //Адресное поле на предыдущий элеменет 
        public Point()            //Конструктор без параметров 
        {
            Key = default;        //Ключ 0 или null
            Value = default;      //Значение 0 или null
            Previous = null;      //Адресное поле на предыдущий элеменет 
            Next = null;          //Адресное поле на след. элеменет 
        }
        public Point(TKey key, TValue value)      //Конструктор c параметром 
        {
            Key = key;                            //Значение ключа
            Value = value;                        //Значение
            Previous = null;                      //Адресное поле на предыдущий элеменет 
            Next = null;                          //Адресное поле на след. элеменет
        }
        public override string ToString()                //Метод для печати
        {
            return Key == null ? "" : $"Ключ: {Key.ToString()}\nЗначение: {Value.ToString()}";
            //При пустом информационном поле возвращается путсая строка, иначе ключ и значение
        }
        public override int GetHashCode()                //Получение хэш кода по инф. полю
        {
            return Key == null ? 0 : Key.GetHashCode();
        }
        public IEnumerator<Point<TKey, TValue>> GetEnumerator()
        {
            //Console.WriteLine("Перебор элементов цепочки");
            Point<TKey, TValue> current = this;
            while (current != null)
            {
                yield return current;
                current = current.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}


