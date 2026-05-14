namespace LibraryManagementSystem.Models
{
    // Librarian sınıfı: Person'dan türer (INHERITANCE).
    //
    // POLYMORPHISM: DisplayMenu() metodu Person'daki virtual metodun
    //               override edilmiş halidir. Kütüphaneciye özel menüyü gösterir.
    public class Librarian : Person
    {
        // ── Constructor ──────────────────────────────────────────────────────────
        public Librarian(int id, string name) : base(id, name) { }

        public Librarian() { }

        // ── DisplayMenu() — override ─────────────────────────────────────────────
        // Kütüphaneciye özel işlem listesi gösterilir.
        public override void DisplayMenu()
        {
            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║     KÜTÜPHANECİ MENÜSÜ      ║");
            Console.WriteLine("╠══════════════════════════════╣");
            Console.WriteLine($"║  Hoş geldin, {Name,-17}║");
            Console.WriteLine("╠══════════════════════════════╣");
            Console.WriteLine("║  1. Kitap Ekle              ║");
            Console.WriteLine("║  2. Kitap Sil               ║");
            Console.WriteLine("║  3. Kitap Güncelle          ║");
            Console.WriteLine("║  4. Kitapları Listele       ║");
            Console.WriteLine("║  0. Geri Dön                ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.Write("Seçiminiz: ");
        }
    }
}