using System;
using System.Collections.Generic;

namespace IndependentWork1
{
    // ==========================================
    // КЛАС 1: Recipe (Рецепт страви)
    // ==========================================
    public class Recipe
    {
        // Приватні поля
        private string _title;
        private int _cookingTimeMinutes;
        private int _baseServings;

        // Властивості (одна read-only, одна з get/set)
        public string Title => _title; // Read-only властивість

        public int CookingTimeMinutes
        {
            get => _cookingTimeMinutes;
            set => _cookingTimeMinutes = value > 0 ? value : 1;
        }

        // Конструктор
        public Recipe(string title, int cookingTimeMinutes, int baseServings)
        {
            _title = title;
            _cookingTimeMinutes = cookingTimeMinutes;
            _baseServings = baseServings > 0 ? baseServings : 1;
        }

        // Метод: перерахунок кількості інгредієнтів під нову кількість порцій
        public double CalculateIngredientAmount(double baseAmountGrams, int targetServings)
        {
            if (targetServings <= 0) return baseAmountGrams;
            return (baseAmountGrams / _baseServings) * targetServings;
        }

        public void PrintRecipeSummary()
        {
            Console.WriteLine($"Рецепт: \"{Title}\" | Час приготування: {CookingTimeMinutes} хв. | Порцій у базі: {_baseServings}");
        }
    }

    // ==========================================
    // КЛАС 2: BankAccount (Банківський рахунок)
    // ==========================================
    public class BankAccount
    {
        // Приватні поля
        private string _accountNumber;
        private decimal _balance;

        // Публічні властивості
        public string AccountNumber => _accountNumber; // Read-only
        
        public decimal Balance
        {
            get => _balance;
            private set => _balance = value;
        }

        // Конструктор
        public BankAccount(string accountNumber, decimal initialBalance)
        {
            _accountNumber = accountNumber;
            _balance = initialBalance >= 0 ? initialBalance : 0;
        }

        // Метод: зняття коштів з перевіркою стану
        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Сума зняття повинна бути більшою за 0.");
                return false;
            }

            if (amount <= _balance)
            {
                _balance -= amount;
                Console.WriteLine($"[Успішно] Знято {amount:C2}. Залишок: {_balance:C2}");
                return true;
            }

            Console.WriteLine($"[Помилка] Недостатньо коштів на рахунку {AccountNumber}. Баланс: {_balance:C2}");
            return false;
        }

        // Метод: поповнення рахунку
        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                _balance += amount;
                Console.WriteLine($"[Поповнення] Додано {amount:C2}. Новий баланс: {_balance:C2}");
            }
        }
    }

    // ==========================================
    // КЛАС 3: Playlist (Музичний плейліст)
    // ==========================================
    public class Playlist
    {
        // Приватні поля
        private string _name;
        private List<string> _tracks;

        // Властивість
        public string Name
        {
            get => _name;
            set => _name = string.IsNullOrWhiteSpace(value) ? "Без назви" : value;
        }

        public int TrackCount => _tracks.Count; // Read-only обчислювана властивість

        // Конструктор
        public Playlist(string name)
        {
            _name = name;
            _tracks = new List<string>();
        }

        // Метод: додавання треку
        public void AddTrack(string trackTitle)
        {
            if (!string.IsNullOrWhiteSpace(trackTitle))
            {
                _tracks.Add(trackTitle);
                Console.WriteLine($"Трек \"{trackTitle}\" додано до плейлісту \"{Name}\".");
            }
        }

        // Метод: перевірка чи плейліст порожній
        public bool IsEmpty()
        {
            return _tracks.Count == 0;
        }
    }

    // ==========================================
    // ГОЛОВНИЙ КЛАС ПРОГРАМИ
    // ==========================================
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== САМОСТІЙНА РОБОТА №1 ===");
            Console.WriteLine("Демонстрація роботи оголошених класів\n");

            // 1. Демонстрація класу Recipe
            Console.WriteLine("--- 1. Тестування класу Recipe ---");
            Recipe borsch = new Recipe("Український Борщ", 120, 4);
            borsch.PrintRecipeSummary();
            double neededMosaGrams = borsch.CalculateIngredientAmount(500, 8); // Обчислення на 8 порцій
            Console.WriteLine($"Для 8 порцій знадобиться м'яса: {neededMosaGrams} г.");
            Console.WriteLine();

            // 2. Демонстрація класу BankAccount
            Console.WriteLine("--- 2. Тестування класу BankAccount ---");
            BankAccount account = new BankAccount("UA1234567890", 1500.00m);
            Console.WriteLine($"Створено рахунок №{account.AccountNumber} з балансом: {account.Balance:C2}");
            account.Deposit(500.00m);
            account.Withdraw(1200.00m);
            account.Withdraw(1000.00m); // Демонстрація перевірки браку коштів
            Console.WriteLine();

            // 3. Демонстрація класу Playlist
            Console.WriteLine("--- 3. Тестування класу Playlist ---");
            Playlist myPlaylist = new Playlist("Улюблені треки");
            Console.WriteLine($"Чи порожній плейліст '{myPlaylist.Name}'? -> {myPlaylist.IsEmpty()}");
            myPlaylist.AddTrack("Океан Ельзи — Обійми");
            myPlaylist.AddTrack("Бумбокс — Люди");
            Console.WriteLine($"Кількість треків після додавання: {myPlaylist.TrackCount}");
            Console.WriteLine($"Чи порожній плейліст тепер? -> {myPlaylist.IsEmpty()}");
        }
    }
}