using System;
using System.Linq;

namespace lab5v8
{
    // Клас Polynomial (Поліном) згідно з варіантом 8
    public class Polynomial
    {
        // Поле: масив коефіцієнтів за степенями (індекс масиву відповідає степеню x)
        private double[] _coefficients;

        // Конструктор із масивом коефіцієнтів
        public Polynomial(params double[] coefficients)
        {
            if (coefficients == null || coefficients.Length == 0)
            {
                _coefficients = new double[] { 0.0 };
            }
            else
            {
                _coefficients = (double[])coefficients.Clone();
            }
        }

        // Властивість для отримання степеня полінома
        public int Degree => _coefficients.Length - 1;

        // Індексатор: доступ до коефіцієнта за степенем (читання та запис)
        public double this[int degree]
        {
            get
            {
                if (degree < 0) return 0.0;
                if (degree >= _coefficients.Length) return 0.0;
                return _coefficients[degree];
            }
            set
            {
                if (degree < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(degree), "Степінь не може бути від'ємною.");
                }

                // Якщо новий степінь виходить за межі поточного масиву, розширюємо його
                if (degree >= _coefficients.Length)
                {
                    double[] newCoefficients = new double[degree + 1];
                    Array.Copy(_coefficients, newCoefficients, _coefficients.Length);
                    _coefficients = newCoefficients;
                }

                _coefficients[degree] = value;
            }
        }

        // Додатковий метод: обчислення значення полінома в точці x
        public double Evaluate(double x)
        {
            double result = 0.0;
            for (int i = 0; i < _coefficients.Length; i++)
            {
                result += _coefficients[i] * Math.Pow(x, i);
            }
            return result;
        }

        // 1. Перевантажений оператор + (додавання двох поліномів)
        public static Polynomial operator +(Polynomial p1, Polynomial p2)
        {
            if (p1 is null || p2 is null)
            {
                throw new ArgumentNullException("Поліноми не можуть бути null.");
            }

            int maxDegree = Math.Max(p1.Degree, p2.Degree);
            double[] resultCoeffs = new double[maxDegree + 1];

            for (int i = 0; i <= maxDegree; i++)
            {
                resultCoeffs[i] = p1[i] + p2[i];
            }

            return new Polynomial(resultCoeffs);
        }

        // 2. Перевантажений оператор * (множення полінома на скаляр)
        public static Polynomial operator *(Polynomial p, double scalar)
        {
            if (p is null) throw new ArgumentNullException(nameof(p));

            double[] resultCoeffs = new double[p._coefficients.Length];
            for (int i = 0; i < p._coefficients.Length; i++)
            {
                resultCoeffs[i] = p._coefficients[i] * scalar;
            }

            return new Polynomial(resultCoeffs);
        }

        public static Polynomial operator *(double scalar, Polynomial p)
        {
            return p * scalar;
        }

        // Перевантажені оператори == та !=
        public static bool operator ==(Polynomial p1, Polynomial p2)
        {
            if (ReferenceEquals(p1, p2)) return true;
            if (p1 is null || p2 is null) return false;
            return p1.Equals(p2);
        }

        public static bool operator !=(Polynomial p1, Polynomial p2)
        {
            return !(p1 == p2);
        }

        // Перевизначення Equals
        public override bool Equals(object obj)
        {
            if (obj is Polynomial other)
            {
                int maxDegree = Math.Max(this.Degree, other.Degree);
                for (int i = 0; i <= maxDegree; i++)
                {
                    if (Math.Abs(this[i] - other[i]) > 0.0001)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        // Перевизначення GetHashCode
        public override int GetHashCode()
        {
            int hash = 17;
            foreach (var coeff in _coefficients)
            {
                hash = hash * 31 + coeff.GetHashCode();
            }
            return hash;
        }

        // Перевизначення ToString для красивого виводу
        public override string ToString()
        {
            string termList = "";
            for (int i = _coefficients.Length - 1; i >= 0; i--)
            {
                double c = _coefficients[i];
                if (Math.Abs(c) < 0.0001 && _coefficients.Length > 1) continue;

                string sign = (c < 0) ? " - " : (termList == "" ? "" : " + ");
                double absC = Math.Abs(c);

                string term = "";
                if (i == 0)
                {
                    term = $"{absC}";
                }
                else if (i == 1)
                {
                    term = (Math.Abs(absC - 1.0) < 0.0001) ? "x" : $"{absC}x";
                }
                else
                {
                    term = (Math.Abs(absC - 1.0) < 0.0001) ? $"x^{i}" : $"{absC}x^{i}";
                }

                termList += sign + term;
            }

            return string.IsNullOrEmpty(termList) ? "0" : termList;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== ДЕМОНСТРАЦІЯ РОБОТИ КЛАСУ POLYNOMIAL (Варіант 8) ===\n");

            // 1. Створення об'єктів поліномів (коефіцієнти від x^0 і вище)
            // P1(x) = 2 + 3x + 4x^2
            Polynomial p1 = new Polynomial(2.0, 3.0, 4.0);
            // P2(x) = 1 + 2x
            Polynomial p2 = new Polynomial(1.0, 2.0);

            Console.WriteLine($"Поліном P1: P1(x) = {p1}");
            Console.WriteLine($"Поліном P2: P2(x) = {p2}\n");

            // 2. Демонстрація роботи індексатора (читання та запис)
            Console.WriteLine("--- Робота з індексатором ---");
            Console.WriteLine($"Коефіцієнт біля x^1 у P1 (читання через p1[1]): {p1[1]}");
            
            p1[1] = 5.0; // Зміна коефіцієнта біля x^1
            Console.WriteLine($"Після зміни p1[1] = 5.0: P1(x) = {p1}");

            p1[3] = 2.0; // Додавання нового старшого степеня (x^3) через індексатор
            Console.WriteLine($"Після додавання p1[3] = 2.0: P1(x) = {p1}\n");

            // Повернемо p1 у початковий вигляд для зручності подальших перевірок
            p1[1] = 3.0;
            p1[3] = 0.0;

            // 3. Демонстрація додаткового методу Evaluate
            double xVal = 2.0;
            Console.WriteLine($"--- Обчислення значення ---");
            Console.WriteLine($"Значення P1({xVal}) = {p1.Evaluate(xVal)} (розрахунок: 2 + 3*(2) + 4*(2^2) = 2 + 6 + 16 = 24)\n");

            // 4. Демонстрація перевантажених операторів (+ та *)
            Console.WriteLine("--- Перевантажені оператори ---");
            Polynomial pSum = p1 + p2;
            Console.WriteLine($"Додавання поліномів (P1 + P2): {pSum}");

            Polynomial pScaled = p2 * 3.0;
            Console.WriteLine($"Множення полінома P2 на скаляр 3 (P2 * 3): {pScaled}\n");

            // 5. Демонстрація порівнянь (==, !=, Equals)
            Polynomial p3 = new Polynomial(2.0, 3.0, 4.0);
            Console.WriteLine("--- Порівняння поліномів ---");
            Console.WriteLine($"P1: {p1}");
            Console.WriteLine($"P3: {p3}");
            Console.WriteLine($"Чи рівні P1 та P3 (оператор ==)? {p1 == p3}");
            Console.WriteLine($"Чи рівні P1 та P2 (метод Equals)? {p1.Equals(p2)}");
        }
    }
}