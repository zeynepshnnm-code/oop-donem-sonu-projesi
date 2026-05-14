namespace LibraryManagementSystem.Models
{
    // Book sınıfı: Kütüphanedeki bir kitabı temsil eder.
    //
    // ENCAPSULATION: Tüm alanlar private tanımlandı.
    //                Dışarıdan erişim yalnızca Property üzerinden yapılabilir.
    //                Property'lerin set bloklarında geçerlilik (validation) kontrolü var.
    public class Book
    {
        // ── Private Fields ────────────────────────────────────────────────────────
        // Bu alanlar sınıf dışından doğrudan okunamaz veya değiştirilemez.
        private int _id;
        private string _title = "";
        private string _author = "";
        private string _category = "";
        private int _stock;

        // ── Properties ───────────────────────────────────────────────────────────
        // get  → dışarıdan okuma izni verir
        // set  → dışarıdan yazma izni verir (içinde doğrulama yapılabilir)

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Kitap başlığı boş olamaz.");
                _title = value;
            }
        }

        public string Author
        {
            get { return _author; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Yazar adı boş olamaz.");
                _author = value;
            }
        }

        public string Category
        {
            get { return _category; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Kategori boş olamaz.");
                _category = value;
            }
        }

        public int Stock
        {
            get { return _stock; }
            set
            {
                // VALIDATION: Stok hiçbir zaman negatif olamaz.
                if (value < 0)
                    throw new ArgumentException("Stok miktarı negatif olamaz.");
                _stock = value;
            }
        }

        // ── Constructor ──────────────────────────────────────────────────────────
        // new Book(1, "Sefiller", "Victor Hugo", "Roman", 5) şeklinde kullanılır.
        public Book(int id, string title, string author, string category, int stock)
        {
            Id = id;
            Title = title;
            Author = author;
            Category = category;
            Stock = stock;
        }

        // System.Text.Json ile JSON'dan nesne üretebilmek için
        // parametresiz constructor zorunludur.
        public Book() { }

        // Kitap bilgisini tek satırda ekrana yazdırmak için kullanılır.
        public override string ToString()
        {
            return $"[{Id,3}] {Title,-30} | {Author,-20} | {Category,-12} | Stok: {Stock}";
        }
    }
}

