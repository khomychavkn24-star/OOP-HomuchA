using System;

namespace Lab6v9
{
    // ==========================================
    // БАЗОВИЙ КЛАС: Weapon
    // ==========================================
    public class Weapon
    {
        // Приватні поля
        private string _name;
        private int _damage;

        // Публічні властивості
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public int Damage
        {
            get => _damage;
            set => _damage = value;
        }

        // Конструктор базового класу
        public Weapon(string name, int damage)
        {
            _name = name;
            _damage = damage;
        }

        // Віртуальний метод для перевизначення (override)
        public virtual void Attack()
        {
            Console.WriteLine($"[Weapon] Нанесення базової атаки з шкодою: {Damage}");
        }

        // Метод для демонстрації приховування (new)
        public string GetWeaponType()
        {
            return "Базова зброя (Base Weapon)";
        }
    }

    // ==========================================
    // ПОХІДНИЙ КЛАС 1: Sword (Меч)
    // ==========================================
    public class Sword : Weapon
    {
        // Власна властивість
        public string Material { get; set; }

        // Конструктор з викликом base(...)
        public Sword(string name, int damage, string material) 
            : base(name, damage)
        {
            Material = material;
        }

        // Перевизначення (override) віртуального методу
        public override void Attack()
        {
            Console.WriteLine($"[Sword] Рубаючий удар мечем \"{Name}\" з матеріалу ({Material})! Шкода: {Damage}");
        }

        // Власний унікальний метод
        public void Parry()
        {
            Console.WriteLine($"[Sword] Меч \"{Name}\" парирує ворожу атаку!");
        }

        // Демонстрація new: Приховування методу GetWeaponType() базового класу
        public new string GetWeaponType()
        {
            return "Клинок / Меч (Hidden with new)";
        }
    }

    // ==========================================
    // ПОХІДНИЙ КЛАС 2: Bow (Лук)
    // ==========================================
    public class Bow : Weapon
    {
        // Власна властивість
        public string ArrowType { get; set; }

        // Конструктор з викликом base(...)
        public Bow(string name, int damage, string arrowType) 
            : base(name, damage)
        {
            ArrowType = arrowType;
        }

        // Перевизначення (override) віртуального методу
        public override void Attack()
        {
            Console.WriteLine($"[Bow] Постріл з лука \"{Name}\" стрілою ({ArrowType})! Шкода: {Damage}");
        }

        // Власний унікальний метод
        public void Aim()
        {
            Console.WriteLine($"[Bow] Прицілювання з лука \"{Name}\" у вразливе місце...");
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

            Console.WriteLine("=== 1. Створення об'єктів та використання власних методів ===");
            Sword excalibur = new Sword("Екскалібур", 85, "Сталь");
            Bow longbow = new Bow("Довгий Лук", 45, "Огненна стріла");

            excalibur.Parry();
            longbow.Aim();
            Console.WriteLine();

            Console.WriteLine("=== 2. Демонстрація Поліморфізму (virtual / override) ===");
            // Масив посилань базового типу Weapon, що містить об'єкти похідних класів
            Weapon[] weapons = new Weapon[]
            {
                new Weapon("Іржавий ніж", 10),
                excalibur,
                longbow
            };

            foreach (var weapon in weapons)
            {
                // Завдяки override викликається реалізація того класу, об'єкт якого насправді створено
                weapon.Attack();
            }
            Console.WriteLine();

            Console.WriteLine("=== 3. Демонстрація різниці між override та new ===");
            
            // Виклик через посилання типу Sword
            Sword swordRef = new Sword("Катана", 60, "Дамаська сталь");
            Console.WriteLine($"Виклик через Sword reference:    {swordRef.GetWeaponType()}");

            // Виклик через посилання типу Weapon (вказівка на той самий об'єкт Sword)
            Weapon weaponRef = swordRef;
            Console.WriteLine($"Виклик через Weapon reference:   {weaponRef.GetWeaponType()}");

            Console.WriteLine("\n--- Порівняльний аналіз виклику Attack() [override] ---");
            Console.Write("через Sword reference:  ");
            swordRef.Attack();
            Console.Write("через Weapon reference: ");
            weaponRef.Attack();
        }
    }
}