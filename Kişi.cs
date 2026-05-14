namespace LibraryManagementSystem.Models
{
    // Person sınıfı: Sistemdeki tüm kullanıcıların ortak özelliklerini barındırır.
    //
    // INHERITANCE  : Student ve Librarian bu sınıftan türeyecek (base class).
    // POLYMORPHISM : DisplayMenu() metodu "virtual" tanımlandığından alt sınıflar
    //                kendi menülerini göstermek için bu metodu "override" edebilir.
    public class Person
    {
        // ── Properties ───────────────────────────────────────────────────────────
        public int Id { get; set; }
        public string Name { get; set; } = "";

        // ── Constructor ──────────────────────────────────────────────────────────
        public Person(int id, string name)
        {
            Id = id;
            Name = name;
        }

        // System.Text.Json için parametresiz constructor
        public Person() { }

        // virtual → Alt sınıflar bu metodu override edebilir (Polymorphism).
        //           Eğer override edilmezse bu genel mesaj görünür.
        public virtual void DisplayMenu()
        {
            Console.WriteLine("Menü yükleniyor...");
        }
    }
}