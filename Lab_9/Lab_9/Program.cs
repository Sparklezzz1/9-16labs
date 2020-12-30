using System;

namespace Lab_9
{
    //Объявляем делегаты
    public delegate void GMutation(string messege);
    public delegate void BMutation(string messege);
    public delegate void CSalary(string messege);
    class Programmer
    {
        //События объявляются в классе с помощью ключевого слова event
        public event GMutation good;
        public event BMutation bad;
        public event CSalary change;
        // Иницилизируем поля
        public string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int age;
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        public int intaligent;
        public int Intaligent
        {
            get { return intaligent; }
            set { intaligent = value; }
        }
        public int salary;
        public int Salary
        {
            get { return salary; }
            set { salary = value; }
        }
        // иницилизация метода
        public Programmer(string name, int age, int intaligent, int salary)
        {
            Name = name;
            Age = age;
            Intaligent = intaligent;
            Salary = salary;
        }
        // Задаем команду для вывода информации
        public void ShowInfo()
        {
            Console.WriteLine("----------Информация о программисте----------");
            Console.WriteLine($"Имя: {name}");
            Console.WriteLine($"Возраст: {age}");
            Console.WriteLine($"Интеллект: {intaligent}");
            Console.WriteLine($"Зарплата: {salary}");
            Console.WriteLine("---------------------------------------------");
        }
        public void GoodMutation()
        {
            intaligent += 15;
            good?.Invoke("Произошла положительная мутация");//способ вызова делегата
        }
        public void BadMutation()
        {
            if (intaligent >= 20)
            {
                intaligent -= 20;
                bad?.Invoke("Произошла отрицательная мутация");//способ вызова делегата
            } 
        } 
        public void ChangeSalary()
        {
            Random CSalary = new Random();
            int rand = CSalary.Next(-100, 500);
            salary += rand;
            change?.Invoke($"Зарплата программиста изменилась на {rand} рублей");//способ вызова делегата
        }
    }
    static public class ChangeString
    {



        public static string OneSpace(string str)//задаем метод с свойствами добавления одного пробела
        {
            int del = 0;
            for (int i = 0; i < str.Length; i++)
            {

                if (str[i] == ' ')
                    if (str[i + 1] == ' ')
                        del = i + 1;
            }
            str = str.Remove(del, 1);


            return str;
        }

        public static void AddLetter(string str, char letter)//задаем метод с свойствами добавления одной буквы
        {
            str = str + letter;
            Console.WriteLine(str);


        }

        public static string AddPoint(string str)//задаем метод с свойствами добавления одной точки
        {
            str = str + ".";

            return str;
        }

        public static string MyToApper(string str)//задаем метод с свойствами перевода строки в верхний регистр
        {
            str = str.ToUpper();

            return str;
        }

        public static string Oo(string str)//задаем метод с свойствами изменения большой буквы на маленькую
        {
            str = str.Replace("O", "o");

            return str;
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            Programmer user = new Programmer("Николай", 25, 45, 1200);
            user.good += (x) => { Console.WriteLine(x); };
            user.ShowInfo();
            user.GoodMutation();
            user.ShowInfo();
            user.BadMutation();
            user.bad += (y) => { Console.WriteLine(y); };
            user.ShowInfo();
            user.ChangeSalary();
            user.change += (z) => { Console.WriteLine(z); };
            user.ShowInfo();

            string str1 = "GoOd  mornin";
            Func<string, string> func;
            Action<string, char> action;
            func = ChangeString.OneSpace;
            str1 = func(str1);
            Console.WriteLine(str1);
            action = ChangeString.AddLetter;
            action(str1, 'g');
            func += ChangeString.Oo;
            str1 = func(str1);
            Console.WriteLine(str1);
            func += ChangeString.MyToApper;
            str1 = func(str1);
            Console.WriteLine(str1);
            func += ChangeString.AddPoint;
            str1 = func(str1);
            Console.WriteLine(str1);


            Console.WriteLine(str1);




            Console.ReadKey();
        }
        
    }
}
