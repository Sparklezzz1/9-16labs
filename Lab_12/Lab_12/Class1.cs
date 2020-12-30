using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_12
{
    class Programmer
    {
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

        public Programmer(string name, int age, int intaligent, int salary)
        {
            Name = name;
            Age = age;
            Intaligent = intaligent;
            Salary = salary;
        }

        public void ShowInfo()
        {
            Console.WriteLine("----------Информация о программисте----------");
            Console.WriteLine($"Имя: {name}");
            Console.WriteLine($"Возраст: {age}");
            Console.WriteLine($"Интеллект: {intaligent}");
            Console.WriteLine($"Зарплата: {salary}");
            Console.WriteLine("---------------------------------------------");
        }
    }
}
