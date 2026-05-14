using LibraryManagementSystem.Models;
using LibraryManagementSystem;

// ── Program Başlangıcı ────────────────────────────────────────────────────────
// LibraryManager oluşturulduğunda LoadData() otomatik çalışır.
LibraryManager manager = new LibraryManager();

// Sabit kullanıcılar (gerçek projede giriş sistemi eklenebilir)
Librarian librarian = new Librarian(1, "yönetici");
Student student = new Student(2, "öğrenci");

bool running = true;

while (running)
{
    ShowMainMenu();

    string? input = Console.ReadLine();

    switch (input)
    {
        case "1":
            // POLYMORPHISM: Person tipindeki referans, Librarian'ın DisplayMenu()'sünü çağırır.
            RunLibrarianMenu(librarian, manager);
            break;

        case "2":
            // POLYMORPHISM: Person tipindeki referans, Student'ın DisplayMenu()'sünü çağırır.
            RunStudentMenu(student, manager);
            break;

        case "0":
            running = false;
            break;

        default:
            Console.WriteLine("HATA: Geçersiz seçenek. Lütfen tekrar deneyin.");
            break;
    }
}

// Program kapanırken tüm verileri JSON'a kaydet
manager.SaveData();
Console.WriteLine("\nProgram kapatıldı. Görüşmek üzere!");

// ════════════════════════════════════════════════════════════════════════════
//  ANA MENÜ
// ════════════════════════════════════════════════════════════════════════════

void ShowMainMenu()
{
    Console.WriteLine();
    Console.WriteLine("╔════════════════════════════════╗");
    Console.WriteLine("║   KÜTÜPHANE YÖNETİM SİSTEMİ    ║");
    Console.WriteLine("╠════════════════════════════════╣");
    Console.WriteLine("║  1. Kütüphaneci Girişi         ║");
    Console.WriteLine("║  2. Öğrenci Girişi             ║");
    Console.WriteLine("║  0. Çıkış                      ║");
    Console.WriteLine("╚════════════════════════════════╝");
    Console.Write("Seçiminiz: ");
}

// ════════════════════════════════════════════════════════════════════════════
//  KÜTÜPHANECİ MENÜSÜ
// ════════════════════════════════════════════════════════════════════════════

void RunLibrarianMenu(Librarian librarian, LibraryManager manager)
{
    bool inMenu = true;

    while (inMenu)
    {
        Console.WriteLine();

        // POLYMORPHISM: librarian.DisplayMenu() → Librarian'daki override çalışır
        librarian.DisplayMenu();

        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                manager.AddBook();
                break;

            case "2":
                manager.DeleteBook();
                break;

            case "3":
                manager.UpdateBook();
                break;

            case "4":
                manager.ListBooks();
                break;

            case "0":
                inMenu = false;
                break;

            default:
                Console.WriteLine("HATA: Geçersiz seçenek.");
                break;
        }

        if (inMenu && choice != "0")
        {
            Console.WriteLine("\nDevam etmek için Enter'a basın...");
            Console.ReadLine();
        }
    }
}

// ════════════════════════════════════════════════════════════════════════════
//  ÖĞRENCİ MENÜSÜ
// ════════════════════════════════════════════════════════════════════════════

void RunStudentMenu(Student student, LibraryManager manager)
{
    bool inMenu = true;

    while (inMenu)
    {
        Console.WriteLine();

        // POLYMORPHISM: student.DisplayMenu() → Student'daki override çalışır
        student.DisplayMenu();

        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                manager.ListBooks();
                break;

            case "2":
                manager.SearchBook();
                break;

            case "3":
                // Hangi öğrencinin ödünç aldığını bilmek için adı geçiriyoruz
                manager.BorrowBook(student.Name);
                break;

            case "4":
                manager.ReturnBook(student.Name);
                break;

            case "0":
                inMenu = false;
                break;

            default:
                Console.WriteLine("HATA: Geçersiz seçenek.");
                break;
        }

        if (inMenu && choice != "0")
        {
            Console.WriteLine("\nDevam etmek için Enter'a basın...");
            Console.ReadLine();
        }
    }
}
