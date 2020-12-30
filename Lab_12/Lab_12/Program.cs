using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Lab_12;

namespace lab12
{
    static class Reflector
    {
        static public void AllClassContent(object obj)
        {
            string writePath = @"D:\write.txt"; // задаем путь к файлу


            StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default);


            Type m = obj.GetType();// берем тип
            MemberInfo[] members = m.GetMembers();// вывод информации
            foreach (MemberInfo item in members)// запись информации
            {
                sw.WriteLine($"{item.DeclaringType} {item.MemberType} {item.Name}");
            }
            sw.Close();

        }



        static public void PublicMethods(object obj)
        {
            Type m = obj.GetType();// берем тип
            MethodInfo[] pubMethods = m.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);//Обнаруживает атрибуты метода и предоставляет доступ к метаданным метода.
            Console.WriteLine("Только публичные методы:");
            foreach (MethodInfo item in pubMethods)
            {
                Console.WriteLine(item.ReturnType.Name + " " + item.Name);
            }

        }



        static public void FieldsAndProperties(object obj)
        {
            Type m = obj.GetType();
            Console.WriteLine("Поля:");
            FieldInfo[] fields = m.GetFields();//Обнаруживает атрибуты поля и предоставляет доступ к метаданным поля.

            foreach (FieldInfo item in fields)
            {
                Console.WriteLine(item.FieldType + " " + item.Name);

            }
            Console.WriteLine("Свойства:");
            PropertyInfo[] properties = m.GetProperties();//Обнаруживает атрибуты свойства и предоставляет доступ к метаданным свойства.
            foreach (PropertyInfo item in properties)
            {
                Console.WriteLine($"{item.PropertyType} {item.Name}");
            }

        }


        static public void Interfaces(object obj)
        {
            Type m = obj.GetType();
            Console.WriteLine("Реализованные интерфейсы:");
            foreach (Type item in m.GetInterfaces())
            {
                Console.WriteLine(item.Name);
            }

        }

        static public void MethodsWithParams(object obj)
        {
            //Console.WriteLine("Введите название типа для параметров:");
            string findType = Console.ReadLine();


            Type m = obj.GetType();
            MethodInfo[] methods = m.GetMethods();
            foreach (MethodInfo item in methods)
            {
                ParameterInfo[] p = item.GetParameters();//Обнаруживает атрибуты параметра и предоставляет доступ к метаданным параметра.

                foreach (ParameterInfo param in p)
                {
                    if (param.ParameterType.Name == findType)
                    {
                        Console.WriteLine("Метод:" + item.ReturnType.Name + " " + item.Name);
                    }
                }


            }

        }
        public static void LastTask(string Class, string MethodName)
        {
            StreamReader reader = new StreamReader(@"D:\read.txt", Encoding.Default);
            string param1, param2, param3;
            param1 = reader.ReadLine();
            param2 = reader.ReadLine();
            param3 = reader.ReadLine();
            reader.Close();

            Type m = Type.GetType(Class, true);

            object st = Activator.CreateInstance(m, null);  //cоздание объекта типа m
            MethodInfo method = m.GetMethod(MethodName);
            method.Invoke(st, new object[] { param1, char.Parse(param2), int.Parse(param3) });
        }
    }
    public class TestParams
    {
        public static void showParams(string str, char symbol, int number)
        {
            Console.WriteLine($"{str} {symbol} {number}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Programmer programmer = new Programmer("Ivan", 20, 100, 1200);
            Reflector.AllClassContent(programmer);
            Console.WriteLine("----------------Поля и свойства объекта train----------------");
            Reflector.FieldsAndProperties(programmer);
            Console.WriteLine("----------------Интерфейсы, реализованные объектом car----------------");
            Reflector.Interfaces(programmer);
            Console.WriteLine("----------------Для объекта car----------------");
            Reflector.PublicMethods(programmer);
            Reflector.MethodsWithParams(programmer);
            Reflector.LastTask("lab12.TestParams", "showParams");
        }
    }
}