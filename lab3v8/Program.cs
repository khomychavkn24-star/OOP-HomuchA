using System;

namespace lab3v8
{
    // Реалізація класу TemporaryFile згідно з варіантом 8 та патерном Dispose
    public class TemporaryFile : IDisposable
    {
        private bool _disposed = false;
        private string _tempFilePath;
        private bool _fileExists;

        // Конструктор: імітує виділення некерованого ресурсу (створення файлу)
        public TemporaryFile(string tempFilePath)
        {
            _tempFilePath = tempFilePath;
            _fileExists = true;
            Console.WriteLine($"[Конструктор] Тимчасовий файл створено: {_tempFilePath}");
        }

        // Публічний метод для роботи з ресурсом
        public void Write(string content)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(TemporaryFile), "Неможливо писати у знищений файл.");
            }

            if (_fileExists)
            {
                Console.WriteLine($"[Write] Запис даних у файл '{_tempFilePath}': \"{content}\"");
            }
            else
            {
                Console.WriteLine("[Write] Файл не відкрито або вже видалений.");
            }
        }

        // Захищений віртуальний метод Dispose(bool disposing)
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Звільнення керованих ресурсів (якщо такі є)
                    Console.WriteLine("[Dispose(true)] Звільнення керованих ресурсів.");
                }

                // Звільнення некерованих ресурсів (імітація видалення тимчасового файлу)
                if (_fileExists)
                {
                    Console.WriteLine($"[Dispose] Видалення тимчасового файлу з диска: {_tempFilePath}");
                    _fileExists = false;
                }

                _disposed = true;
            }
        }

        // Реалізація інтерфейсу IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Скасовуємо фіналізацію, оскільки ресурси вже звільнені вручну
        }

        // Деструктор (фіналізатор)
        ~TemporaryFile()
        {
            Dispose(false);
            Console.WriteLine($"[Деструктор] Фіналізатор спрацював для файлу: {_tempFilePath}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== СЦЕНАРІЙ 1: Використання оператора using ===");
            {
                using (var tempFile = new TemporaryFile("temp_file_1.tmp"))
                {
                    tempFile.Write("Тестовий рядок номер 1");
                } // Блок закінчується — автоматично викликається Dispose()
            }

            Console.WriteLine("\n=== СЦЕНАРІЙ 2: Явний виклик Dispose() ===");
            {
                var tempFile = new TemporaryFile("temp_file_2.tmp");
                tempFile.Write("Тестовий рядок номер 2");
                tempFile.Dispose(); // Явне звільнення ресурсу розробником
            }

            Console.WriteLine("\n=== СЦЕНАРІЙ 3: Робота без виклику Dispose() через GC ===");
            {
                CreateAndForgetFile();
            }

            // Штучний виклик збирання сміття та очікування фіналізаторів для демонстрації роботи деструктора
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("\nПрограму завершено.");
        }

        static void CreateAndForgetFile()
        {
            var tempFile = new TemporaryFile("temp_file_3.tmp");
            tempFile.Write("Тестовий рядок номер 3");
            // Об'єкт виходить з області видимості, але Dispose() не викликається явно і немає using
        }
    }
}