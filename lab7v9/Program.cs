using System;

namespace Lab7v9
{
    // ==========================================
    // БАЗОВИЙ КЛАС: Media
    // ==========================================
    public class Media
    {
        public string Title { get; set; }
        public int DurationMinutes { get; set; }

        public Media(string title, int durationMinutes)
        {
            Title = title;
            DurationMinutes = durationMinutes;
        }

        // Віртуальний метод базового класу
        public virtual void Play()
        {
            Console.WriteLine($"[Media] Відтворення базового медіафайлу \"{Title}\" (Тривалість: {DurationMinutes} хв.)");
        }
    }

    // ==========================================
    // ПОХІДНИЙ КЛАС A: Video (використовує override)
    // ==========================================
    public class Video : Media
    {
        public string Resolution { get; set; }

        public Video(string title, int durationMinutes, string resolution) 
            : base(title, durationMinutes)
        {
            Resolution = resolution;
        }

        // Перевизначення (override) забезпечує ДИНАМІЧНИЙ поліморфізм
        public override void Play()
        {
            Console.WriteLine($"[Video] Відтворення відео \"{Title}\" у роздільній здатності {Resolution} (Тривалість: {DurationMinutes} хв.)");
        }
    }

    // ==========================================
    // ПОХІДНИЙ КЛАС B: Audio (використовує new)
    // ==========================================
    public class Audio : Media
    {
        public int BitrateKbps { get; set; }

        public Audio(string title, int durationMinutes, int bitrateKbps) 
            : base(title, durationMinutes)
        {
            BitrateKbps = bitrateKbps;
        }

        // Приховування (new) повністю РВЕ ланцюжок поліморфізму (раннє зв'язування)
        public new void Play()
        {
            Console.WriteLine($"[Audio] Програвання аудіотреку \"{Title}\" із бітрейтом {BitrateKbps} kbps (Тривалість: {DurationMinutes} хв.)");
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

            Console.WriteLine("=== ЛАБОРАТОРНА РОБОТА №7 (Варіант 9) ===");
            Console.WriteLine("Тема: Приховування методів (new) vs Перевизначення (override)\n");

            // 1. Створення об'єктів класів Video та Audio
            Video myVideo = new Video("Матриця", 136, "4K UHD");
            Audio myAudio = new Audio("Bohemian Rhapsody", 6, 320);

            // 2. Upcasting: Збереження об'єктів у змінних типу базового класу Media
            Media mediaRef1 = myVideo; // Upcasting Video -> Media
            Media mediaRef2 = myAudio; // Upcasting Audio -> Media

            // ----------------------------------------------------
            // 3. Виклики методів через ПОСИЛАННЯ БАЗОВОГО КЛАСУ (Media)
            // ----------------------------------------------------
            Console.WriteLine("--- 1. Виклики через посилання БАЗОВОГО класу (Media reference) ---");
            
            // Спрацює override: викликається реалізація об'єкта Video, оскільки Play() був перевизначений (динамічне зв'язування)
            Console.Write("Video об'єкт: ");
            mediaRef1.Play(); 

            // Спрацює new: викликається метод базового класу Media, оскільки new рве поліморфізм (раннє зв'язування)
            Console.Write("Audio об'єкт: ");
            mediaRef2.Play(); 
            Console.WriteLine();

            // ----------------------------------------------------
            // 4. Виклики методів через ПОСИЛАННЯ ПОХІДНИХ КЛАСІВ (з явним приведенням)
            // ----------------------------------------------------
            Console.WriteLine("--- 2. Виклики через посилання ПОХІДНИХ класів (Downcasting / Derived reference) ---");
            
            Console.Write("((Video)mediaRef1): ");
            ((Video)mediaRef1).Play(); // Викликається Video.Play()

            Console.Write("((Audio)mediaRef2): ");
            ((Audio)mediaRef2).Play(); // Викликається Audio.Play()
            Console.WriteLine();

            // ----------------------------------------------------
            // 5. Пояснення результату в консолі
            // ----------------------------------------------------
            Console.WriteLine("--- 3. Аналіз та порівняння поведінки ---");
            Console.WriteLine("• Для Video (override): Незалежно від типу посилання (Media чи Video) ДИНАМІЧНО викликається метод Video.Play().");
            Console.WriteLine("• Для Audio (new):      При виклику через посилання Media виконується Media.Play(), а через Audio — Audio.Play().");
        }
    }
}