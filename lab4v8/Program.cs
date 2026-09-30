using System;

namespace lab4v8
{
    // Клас Rectangle згідно з варіантом 8
    public class Rectangle
    {
        // Приватні поля
        private double _width;
        private double _height;

        // Конструктор за замовчуванням
        public Rectangle() : this(1.0, 1.0) { }

        // Конструктор із параметрами
        public Rectangle(double width, double height)
        {
            Width = width;   // Використовуємо властивості для валідації
            Height = height;
        }

        // Публічні властивості з валідацією (> 0)
        public double Width
        {
            get => _width;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Ширина прямокутника має бути більшою за 0.");
                }
                _width = value;
            }
        }

        public double Height
        {
            get => _height;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Висота прямокутника має бути більшою за 0.");
                }
                _height = value;
            }
        }

        // Обчислювана властивість для площі
        public double Area => _width * _height;

        // Статичний член класу
        public static Rectangle DefaultRectangle => new Rectangle(1.0, 1.0);

        // Перевантажений оператор множення на скаляр (змінює розміри прямокутника)
        public static Rectangle operator *(Rectangle rect, double scalar)
        {
            if (scalar <= 0)
            {
                throw new ArgumentException("Множник має бути додатним числом.");
            }
            return new Rectangle(rect.Width * scalar, rect.Height * scalar);
        }

        public static Rectangle operator *(double scalar, Rectangle rect)
        {
            return rect * scalar;
        }

        // Перевантажені оператори порівняння == та != (порівняння за площею)
        public static bool operator ==(Rectangle left, Rectangle right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return Math.Abs(left.Area - right.Area) < 0.0001;
        }

        public static bool operator !=(Rectangle left, Rectangle right)
        {
            return !(left == right);
        }

        // Перевизначення Equals
        public override bool Equals(object obj)
        {
            if (obj is Rectangle other)
            {
                return Math.Abs(_width - other._width) < 0.0001 && 
                       Math.Abs(_height - other._height) < 0.0001;
            }
            return false;
        }

        // Перевизначення GetHashCode
        public override int GetHashCode()
        {
            return HashCode.Combine(_width, _height);
        }

        // Перевизначення ToString
        public override string ToString()
        {
            return $"Rectangle [Ширина: {_width}, Висота: {_height}, Площа: {Area:F2}]";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== ДЕМОНСТРАЦІЯ РОБОТИ КЛАСУ RECTANGLE (Варіант 8) ===\n");

            // 1. Створення об'єктів та використання статичного члена
            Rectangle rect1 = new Rectangle(4.0, 5.0);
            Rectangle rect2 = new Rectangle(2.0, 10.0);
            Rectangle defaultRect = Rectangle.DefaultRectangle;

            Console.WriteLine($"Прямокутник 1: {rect1}");
            Console.WriteLine($"Прямокутник 2: {rect2}");
            Console.WriteLine($"Прямокутник за замовчуванням (статичний член): {defaultRect}\n");

            // 2. Демонстрація валідації властивостей
            try
            {
                Console.WriteLine("Спроба встановити від'ємну ширину (-3.0)...");
                rect1.Width = -3.0;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"[ПОМИЛКА ВАЛІДАЦІЇ СПІЙМАНА]: {ex.Message}\n");
            }

            // 3. Демонстрація перевантаженого оператора множення на скаляр
            Rectangle scaledRect = rect1 * 2.5;
            Console.WriteLine($"Множення rect1 на скаляр 2.5: {scaledRect}");

            // 4. Демонстрація перевантажених операторів порівняння (== та !=) за площею
            Console.WriteLine($"\nПорівняння rect1 (площа {rect1.Area}) та rect2 (площа {rect2.Area}):");
            if (rect1 == rect2)
            {
                Console.WriteLine("rect1 та rect2 рівні за площею (оператор ==).");
            }
            else
            {
                Console.WriteLine("rect1 та rect2 НЕ рівні за площею (оператор ==).");
            }

            // 5. Демонстрація Equals та GetHashCode
            Rectangle rect3 = new Rectangle(4.0, 5.0);
            Console.WriteLine($"\nrect3: {rect3}");
            Console.WriteLine($"rect1.Equals(rect3): {rect1.Equals(rect3)}");
            Console.WriteLine($"Хеш-код rect1: {rect1.GetHashCode()}");
            Console.WriteLine($"Хеш-код rect3: {rect3.GetHashCode()}");
        }
    }
}