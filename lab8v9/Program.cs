using System;
using System.Collections.Generic;

namespace Lab8v9
{
    // ==========================================
    // БАЗОВИЙ КЛАС: Tool
    // ==========================================
    public class Tool
    {
        public string Name { get; set; }

        public Tool(string name)
        {
            Name = name;
        }

        // Віртуальний метод для поліморфного виклику
        public virtual void Use()
        {
            Console.WriteLine($"[Tool] Використовується базовий інструмент \"{Name}\".");
        }
    }

    // ==========================================
    // ПОХІДНИЙ КЛАС 1: Hammer (Молоток)
    // ==========================================
    public class Hammer : Tool
    {
        public double Weight { get; set; } // Вага в кг

        public Hammer(string name, double weight) : base(name)
        {
            Weight = weight;
        }

        // Перевизначення віртуального методу (override)
        public override void Use()
        {
            Console.WriteLine($"[Hammer] Молотком \"{Name}\" (вага: {Weight} кг) забивають цвях.");
        }
    }

    // ==========================================
    // ПОХІДНИЙ КЛАС 2: Screwdriver (Викрутка)
    // ==========================================
    public class Screwdriver : Tool
    {
        public string TipType { get; set; } // Тип наконечника (напр., Хрестоподібна/Плоска)

        public Screwdriver(string name, string tipType) : base(name)
        {
            TipType = tipType;
        }

        // Перевизначення віртуального методу (override)
        public override void Use()
        {
            Console.WriteLine($"[Screwdriver] Викруткою \"{Name}\" (наконечник: {TipType}) закручують шуруп.");
        }
    }

    // ==========================================
    // ПОХІДНИЙ КЛАС 3: Wrench (Гайковий ключ)
    // ==========================================
    public class Wrench : Tool
    {
        public int Size { get; set; } // Розмір у мм

        public Wrench(string name, int size) : base(name)
        {
            Size = size;
        }

        // Перевизначення віртуального методу (override)
        public override void Use()
        {
            Console.WriteLine($"[Wrench] Гайковим ключем \"{Name}\" (розмір: {Size} мм) затягують болт.");
        }
    }

    // ==========================================
    // ГОЛОВНИЙ КЛАС ПРОГРАМИ
    // ==========================================
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== ЛАБОРАТОРНА РОБОТА №8 (Варіант 9) ===");
            Console.WriteLine("Тема: Поліморфізм: динамічне зв’язування та перевизначення методів\n");

            // 1. Створення колекції об'єктів базового типу List<Tool>
            List<Tool> toolbox = new List<Tool>
            {
                new Hammer("Stayer Heavy", 1.5),
                new Screwdriver("Stanley Ph1", "Хрестоподібна (PH)"),
                new Wrench("Intertool 17", 17),
                new Hammer("Rubber Mallet", 0.8),
                new Screwdriver("Pro'sKit Flat", "Шліцьова (SL)")
            };

            // Список для агрегації результатів (фіксація використаних інструментів)
            List<string> usedToolsLog = new List<string>();

            Console.WriteLine("--- 2. Демонстрація поліморфного виклику методу Use() у циклі ---");
            foreach (Tool tool in toolbox)
            {
                // Завдяки динамічному зв'язуванню викликається відповідна реалізація override void Use()
                tool.Use();

                // Агрегація: збереження відомостей про використані інструменти
                usedToolsLog.Add($"{tool.Name} ({tool.GetType().Name})");
            }

            Console.WriteLine("\n--- 3. Агрегація результатів ---");
            Console.WriteLine($"Всього використано інструментів: {usedToolsLog.Count}");
            Console.WriteLine("Список усіх задіяних інструментів:");
            for (int i = 0; i < usedToolsLog.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {usedToolsLog[i]}");
            }
        }
    }
}