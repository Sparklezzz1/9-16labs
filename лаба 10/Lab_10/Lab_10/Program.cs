using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace lab10
{
    class Image<T> : SortedSet<T>, ISet<T>
    {
        public string format { get; set; 
    }
    public int number { get; set; }// задаем поля
    public Image(string format, int number)// метод со свойствами
    {
        this.format = format;
        this.number = number;
    }
    public void Show()// вывод информации
    {
        Console.WriteLine("Формат: " + format + ", номер: " + number);
    }
}
class Program
{
    static void Main(string[] args)
    {
        //task 1
        Console.WriteLine("-------------------Первое задание-------------------");
        LinkedList<Image<int>> list1 = new LinkedList<Image<int>>(); // связаный список
        Image<int> image1 = new Image<int>("jpg", 1);
        Image<int> image2 = new Image<int>("png", 2);
            list1.AddFirst(image1);// добавляем в начало
            list1.AddLast(image2);// добавляем в конец
            list1.RemoveFirst();// удаляем из начала
        Console.WriteLine("Вывод списка: ");
        foreach (Image<int> item in list1) // поиск информации в списке
        {
            item.Show();
        }
        //task 2
        Console.WriteLine("-------------------Второе задание-------------------");
        LinkedList<int> list2 = new LinkedList<int>();
            list2.AddFirst(1);
            list2.AddFirst(2);
            list2.AddFirst(3);
            list2.AddFirst(4);
            list2.AddFirst(5);
        Console.Write("Список: ");
        foreach (int item in list2)
        {
            Console.Write(item + "");
        }
        for (int i = 0; i < 2; i++)
        {
                list2.RemoveFirst();
        }
        Console.WriteLine();
        Console.Write("Список после удаления первых 2-х элементов: ");
        foreach (int item in list2)
        {
            Console.Write(item + " ");
        }
            list2.AddLast(0);
        Console.WriteLine();
        Console.Write("Список после добавление элемента в конец: ");
        foreach (int item in list2)
        {
            Console.Write(item + " ");
        }
        List<int> list3 = new List<int>();
        list3.Add(3);
        list3.Add(2);
        list3.Add(1);
        list3.Add(0);
        Console.WriteLine();
        Console.Write("Второй список: ");
        foreach (int item in list3)
        {
            Console.Write(item + "");
        }
        Console.WriteLine();
        if (list3.Contains(3))
            Console.WriteLine("В списке найден элемент '3'");
        else Console.WriteLine("В списке не найден элемент '3'");
        Console.WriteLine("-------------------Третье задание-------------------");
        ObservableCollection<Image<int>> observList = new ObservableCollection<Image<int>>();
        observList.CollectionChanged += Changes;                                  //подписание метода changes на событие collectionCgandeg
        observList.Add(image1);
        observList.Remove(image1);
    }
    private static void Changes(object sender, NotifyCollectionChangedEventArgs e)// объект NotifyCollectionChangedEventArgs e.
                                                                                  //Его свойство Action позволяет узнать характер изменений. 
                                                                                  //Оно хранит одно из значений из перечисления NotifyCollectionChangedAction.
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add: // если добавление
                Image<int> newImage = e.NewItems[0] as Image<int>;
                Console.WriteLine($"Добавлен новый элемент: {newImage.number}");
                break;
            case NotifyCollectionChangedAction.Remove: // если удаление
                Image<int> oldUser = e.OldItems[0] as Image<int>;
                Console.WriteLine($"Удален объект: {oldUser.number}");
                break;
        }
    }
}
}