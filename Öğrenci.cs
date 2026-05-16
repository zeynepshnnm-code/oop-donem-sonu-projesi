using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Models
{
    
    public class Student : Person
    {
       
        public Student(int id, string name) : base(id, name) { }

        public Student() { }

        public override void DisplayMenu()
        {
            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║       ÖĞRENCİ MENÜSÜ         ║");
            Console.WriteLine("╠══════════════════════════════╣");
            Console.WriteLine($"║  Hoş geldin, {Name,-17}║");
            Console.WriteLine("╠══════════════════════════════╣");
            Console.WriteLine("║  1. Kitapları Listele        ║");
            Console.WriteLine("║  2. Kitap Ara               ║");
            Console.WriteLine("║  3. Kitap Ödünç Al          ║");
            Console.WriteLine("║  4. Kitap İade Et           ║");
            Console.WriteLine("║  0. Geri Dön                ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.Write("Seçiminiz: ");
        }
    }
}
