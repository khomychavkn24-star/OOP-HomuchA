using System;

class Animal
{
    private string species;
    private string nickname;
    private int age;

    public int Age
    {
        get { return age; }
        set { age = value; }
    }

    public Animal(string species, string nickname, int age)
    {
        this.species = species;
        this.nickname = nickname;
        this.Age = age;

        Console.WriteLine($"Створено тварину: {nickname}");
    }

    public void Speak()
    {
        Console.WriteLine($"{nickname} ({species}), {Age} років, видає звук.");
    }

    ~Animal()
    {
        Console.WriteLine($"Об'єкт тварини {nickname} знищується.");
    }
}

class Program
{
    static void Main()
    {
        Animal animal1 = new Animal("Собака", "Бобік", 3);
        Animal animal2 = new Animal("Кіт", "Мурчик", 2);
        Animal animal3 = new Animal("Папуга", "Кеша", 5);

        animal1.Speak();
        animal2.Speak();
        animal3.Speak();

        animal1.Age = 4;

        Console.WriteLine("\nПісля зміни віку:");
        animal1.Speak();

        Console.WriteLine("\nНатисніть Enter для завершення...");
        Console.ReadLine();
    }
}