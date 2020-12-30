using System;
using System.Linq;
using System.Collections.Generic;
namespace lab11
{
    class Player
    {
        // в классе создаем поля
        public string Name { get; set; }
        public string Team { get; set; }
    }
    class Team
    {
        // в классе создаем поля
        public string Name { get; set; }
        public string Country { get; set; }
    }
    public class Product
    {
        // в классе создаем поля
        private readonly int ID;//поле-только для чтения 
        public int Id
        {
            get
            {
                return ID;
            }
        }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        private int UPC;
        public int Upc
        {
            get
            {
                return UPC;
            }
        }
        private string manufacturer;//поле-константа 
        public string Manufacturer
        {
            get
            {
                return manufacturer;
            }
            set
            {
                manufacturer = value;
            }
        }
        private int price;
        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
        private string term;
        public string Term
        {
            get
            {
                return term;
            }
            set
            {
                term = value;
            }
        }
        private int quantity;
        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }
        }
        public static int count = 0;//счётчик созданных объектов 



        public Product(string iManuf, int iID, string iName, int iUPC, int iPrice, string iTerm, int iQuantity)//конструктор с параметрами
        {
            count++;
            this.ID = iID;
            this.Name = iName;
            this.UPC = base.GetHashCode();
            this.Price = iPrice;
            this.Term = iTerm;
            this.Quantity = iQuantity;
            this.Manufacturer = iManuf;
        }
        public void Info() // вывод информцаии 
        {
            Console.WriteLine($"Производитель: {manufacturer}, наименование: {name}, ID: {ID}, UPC: {UPC}, срок хранения: {term}, цена: {price}$, количество: {quantity}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----------------------Первое задание----------------------");
            string[] year = { "January", "February", "Mart", "April", "May", "June", "July", "September", "October", "November", "December" };// задаем список месяцев
            var selectedTeams = from t in year where t.Count() == 4 select t;
            Console.WriteLine("Месяцы, название которых содержит 4 символа:");
            foreach (string s in selectedTeams)
                Console.WriteLine(s);
            Console.WriteLine("----------------------");
            string[] Winter = { "December", "January", "February" };
            selectedTeams = null;
            selectedTeams = from t in year where Winter.Contains(t) select t;
            Console.WriteLine("Зимние месяцы: ");
            foreach (string s in selectedTeams)
                Console.WriteLine(s);
            Console.WriteLine("----------------------");
            selectedTeams = null;
            selectedTeams = from t in year orderby t select t;
            Console.WriteLine("Месяцы по алфавиту: ");
            foreach (string s in selectedTeams)
                Console.WriteLine(s);
            Console.WriteLine("----------------------Второе задание задание----------------------");
            List<Product> list = new List<Product>();
            Product prod1 = new Product("man1", 1, "prod1", 413532, 100, "2 года", 100);
            Product prod2 = new Product("man1", 2, "prod2", 485782, 110, "3 года", 100);
            Product prod3 = new Product("man2", 3, "prod1", 846284, 120, "3 года", 200);
            Product prod4 = new Product("man3", 4, "prod2", 624171, 90, "3 года", 150);
            Product prod5 = new Product("man4", 5, "prod5", 124512, 110, "3 года", 100);
            list.Add(prod1);
            list.Add(prod2);
            list.Add(prod3);
            list.Add(prod4);
            list.Add(prod5);
            var selectedTeams1 = from prod in list where prod.Name == "prod1" select prod;
            Console.WriteLine("Товары с наименованием 'prod1':");
            foreach (Product p in selectedTeams1)
            {
                p.Info();
            }
            Console.WriteLine("----------------------");
            selectedTeams1 = null;
            selectedTeams1 = list.Where(prod => prod.Name == "prod2" && prod.Price < 100);
            Console.WriteLine("Товары с наименованием 'prod2' и ценой меньше 100$:");
            foreach (Product p in selectedTeams1)
            {
                p.Info();
            }
            Console.WriteLine("----------------------");
            selectedTeams1 = null;
            selectedTeams1 = from i in list where i.Price > 100 select i;
            Console.WriteLine("Товары с ценой больше 100$:");
            foreach (Product p in selectedTeams1)
            {
                p.Info();
            }
            Console.WriteLine("----------------------");
            selectedTeams1 = null;
            selectedTeams1 = (from i in list orderby i.Price select i);
            Console.WriteLine("Товар с макcимальной ценой:");
            selectedTeams1.Last().Info();//используется метод расширения Lаst()
            Console.WriteLine("----------------------");
            List<Team> teams = new List<Team>()
            {
                new Team { Name = "Бавария", Country ="Германия" },
                new Team { Name = "Барселона", Country ="Испания" }
            };
            List<Player> players = new List<Player>()
            {
            new Player {Name="Месси", Team="Барселона"},
            new Player {Name="Неймар", Team="Барселона"},
            new Player {Name="Роббен", Team="Бавария"}
            };
            var result = players.Join(teams, // второй набор
             p => p.Team, // свойство-селектор объекта из первого набора
             t => t.Name, // свойство-селектор объекта из второго набора
             (p, t) => new { Name = p.Name, Team = p.Team, Country = t.Country }); // результат


            foreach (var item in result)
                Console.WriteLine($"{item.Name} - {item.Team} ({item.Country})");
        }
    }
}