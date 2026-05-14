using System.Text.Json;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem
{
    // LibraryManager sınıfı: Tüm kütüphane işlemlerini yürüten ana sınıftır.
    // Kitap CRUD işlemleri, ödünç alma/iade ve JSON veri saklama burada yönetilir.
    public class LibraryManager
    {
        // ── Private Fields ────────────────────────────────────────────────────────
        private List<Book> _books = new List<Book>();
        private List<BorrowRecord> _borrowRecords = new List<BorrowRecord>();

        // JSON dosyalarının kaydedileceği yollar
        private const string BooksFilePath = "books.json";
        private const string RecordsFilePath = "borrowRecords.json";

        // JSON çıktısını okunabilir yapmak için seçenek
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        // ── Constructor ──────────────────────────────────────────────────────────
        // LibraryManager oluşturulduğunda otomatik olarak verileri yükler.
        public LibraryManager()
        {
            LoadData();
        }

        // ════════════════════════════════════════════════════════════════════════
        //  CRUD — KİTAP İŞLEMLERİ
        // ════════════════════════════════════════════════════════════════════════

        // CREATE: Yeni kitap ekler.
        public void AddBook()
        {
            Console.WriteLine("\n── Yeni Kitap Ekle ──────────────────────");

            try
            {
                // Yeni kitap için otomatik ID üret (listedeki en büyük ID + 1)
                int newId = _books.Count > 0 ? _books.Max(b => b.Id) + 1 : 1;

                Console.Write("Başlık    : ");
                string title = Console.ReadLine() ?? "";

                Console.Write("Yazar     : ");
                string author = Console.ReadLine() ?? "";

                Console.Write("Kategori  : ");
                string category = Console.ReadLine() ?? "";

                Console.Write("Stok      : ");
                if (!int.TryParse(Console.ReadLine(), out int stock) || stock < 0)
                {
                    Console.WriteLine("HATA: Geçerli bir stok miktarı giriniz (0 veya daha büyük).");
                    return;
                }

                // Book constructor'ı property'leri çağırır → validation otomatik çalışır
                Book newBook = new Book(newId, title, author, category, stock);
                _books.Add(newBook);

                Console.WriteLine($"\nKitap başarıyla eklendi. (ID: {newId})");
            }
            catch (ArgumentException ex)
            {
                // Book property'sindeki validation hataları buraya düşer
                Console.WriteLine($"HATA: {ex.Message}");
            }
        }

        // DELETE: ID'ye göre kitap siler.
        public void DeleteBook()
        {
            Console.WriteLine("\n── Kitap Sil ────────────────────────────");
            ListBooks();

            Console.Write("Silinecek kitabın ID'si: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("HATA: Geçersiz ID.");
                return;
            }

            // Kitabı listede ara
            Book? book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                Console.WriteLine("HATA: Bu ID'ye ait kitap bulunamadı.");
                return;
            }

            _books.Remove(book);
            Console.WriteLine($"'{book.Title}' kitabı silindi.");
        }

        // UPDATE: Mevcut kitabın bilgilerini günceller.
        public void UpdateBook()
        {
            Console.WriteLine("\n── Kitap Güncelle ───────────────────────");
            ListBooks();

            Console.Write("Güncellenecek kitabın ID'si: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("HATA: Geçersiz ID.");
                return;
            }

            Book? book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                Console.WriteLine("HATA: Bu ID'ye ait kitap bulunamadı.");
                return;
            }

            try
            {
                // Boş bırakılırsa mevcut değer korunur
                Console.Write($"Yeni Başlık    [{book.Title}]: ");
                string title = Console.ReadLine() ?? "";
                if (!string.IsNullOrWhiteSpace(title)) book.Title = title;

                Console.Write($"Yeni Yazar     [{book.Author}]: ");
                string author = Console.ReadLine() ?? "";
                if (!string.IsNullOrWhiteSpace(author)) book.Author = author;

                Console.Write($"Yeni Kategori  [{book.Category}]: ");
                string category = Console.ReadLine() ?? "";
                if (!string.IsNullOrWhiteSpace(category)) book.Category = category;

                Console.Write($"Yeni Stok      [{book.Stock}]: ");
                string stockInput = Console.ReadLine() ?? "";
                if (!string.IsNullOrWhiteSpace(stockInput))
                {
                    if (!int.TryParse(stockInput, out int stock))
                    {
                        Console.WriteLine("HATA: Geçerli bir stok giriniz.");
                        return;
                    }
                    book.Stock = stock; // Property validation devreye girer
                }

                Console.WriteLine("Kitap başarıyla güncellendi.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"HATA: {ex.Message}");
            }
        }

        // READ: Tüm kitapları listeler.
        public void ListBooks()
        {
            Console.WriteLine("\n── Kitap Listesi ────────────────────────");

            if (_books.Count == 0)
            {
                Console.WriteLine("Sistemde kayıtlı kitap bulunmamaktadır.");
                return;
            }

            foreach (Book book in _books)
            {
                Console.WriteLine(book); // Book.ToString() çağrılır
            }

            Console.WriteLine($"\nToplam: {_books.Count} kitap");
        }

        // READ: Ada veya yazara göre kitap arar.
        public void SearchBook()
        {
            Console.WriteLine("\n── Kitap Ara ────────────────────────────");
            Console.Write("Arama kelimesi (başlık veya yazar): ");
            string keyword = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(keyword))
            {
                Console.WriteLine("HATA: Arama kelimesi boş olamaz.");
                return;
            }

            // Küçük harfe çevirerek büyük/küçük harf duyarsız arama yap
            string lowerKeyword = keyword.ToLower();

            List<Book> results = _books
                .Where(b => b.Title.ToLower().Contains(lowerKeyword) ||
                            b.Author.ToLower().Contains(lowerKeyword))
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("Aramanızla eşleşen kitap bulunamadı.");
                return;
            }

            Console.WriteLine($"\n{results.Count} sonuç bulundu:");
            foreach (Book book in results)
            {
                Console.WriteLine(book);
            }
        }

        // ════════════════════════════════════════════════════════════════════════
        //  ÖDÜNÇ ALMA / İADE
        // ════════════════════════════════════════════════════════════════════════

        // Öğrencinin kitap ödünç almasını sağlar.
        public void BorrowBook(string studentName)
        {
            Console.WriteLine("\n── Kitap Ödünç Al ───────────────────────");
            ListBooks();

            Console.Write("Ödünç almak istediğiniz kitabın ID'si: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("HATA: Geçersiz ID.");
                return;
            }

            Book? book = _books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
            {
                Console.WriteLine("HATA: Bu ID'ye ait kitap bulunamadı.");
                return;
            }

            if (book.Stock <= 0)
            {
                Console.WriteLine("HATA: Bu kitabın stokta kopyası kalmamıştır.");
                return;
            }

            // Aynı öğrenci aynı kitabı tekrar ödünç alamaz (iade etmeden)
            bool alreadyBorrowed = _borrowRecords.Any(r =>
                r.BookId == bookId &&
                r.StudentName == studentName &&
                r.ReturnDate == null);

            if (alreadyBorrowed)
            {
                Console.WriteLine("HATA: Bu kitabı zaten ödünç almışsınız. Önce iade ediniz.");
                return;
            }

            // Stoktan bir adet düş ve kaydı oluştur
            book.Stock--;
            _borrowRecords.Add(new BorrowRecord(bookId, studentName, DateTime.Now));

            Console.WriteLine($"'{book.Title}' kitabı başarıyla ödünç alındı.");
        }

        // Öğrencinin kitap iade etmesini sağlar.
        public void ReturnBook(string studentName)
        {
            Console.WriteLine("\n── Kitap İade Et ────────────────────────");

            // Bu öğrencinin iade bekleyen kayıtlarını bul
            List<BorrowRecord> activeRecords = _borrowRecords
                .Where(r => r.StudentName == studentName && r.ReturnDate == null)
                .ToList();

            if (activeRecords.Count == 0)
            {
                Console.WriteLine("Ödünç aldığınız aktif kitap kaydı bulunamadı.");
                return;
            }

            Console.WriteLine("Ödünç Aldığınız Kitaplar:");
            foreach (BorrowRecord record in activeRecords)
            {
                Book? book = _books.FirstOrDefault(b => b.Id == record.BookId);
                string bookTitle = book != null ? book.Title : "Bilinmiyor";
                Console.WriteLine($"  Kitap ID: {record.BookId} | Başlık: {bookTitle} | Alış: {record.BorrowDate.ToShortDateString()}");
            }

            Console.Write("İade etmek istediğiniz kitabın ID'si: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("HATA: Geçersiz ID.");
                return;
            }

            // İlgili aktif kaydı bul
            BorrowRecord? recordToReturn = activeRecords.FirstOrDefault(r => r.BookId == bookId);
            if (recordToReturn == null)
            {
                Console.WriteLine("HATA: Bu kitaba ait aktif ödünç kaydı bulunamadı.");
                return;
            }

            // İade tarihini işle ve stoğu geri artır
            recordToReturn.ReturnDate = DateTime.Now;

            Book? returnedBook = _books.FirstOrDefault(b => b.Id == bookId);
            if (returnedBook != null)
            {
                returnedBook.Stock++;
                Console.WriteLine($"'{returnedBook.Title}' başarıyla iade edildi.");
            }
        }

        // ════════════════════════════════════════════════════════════════════════
        //  JSON VERİ SAKLAMA
        // ════════════════════════════════════════════════════════════════════════

        // Tüm verileri JSON dosyalarına kaydeder.
        // Program kapanmadan önce çağrılır.
        public void SaveData()
        {
            try
            {
                string booksJson = JsonSerializer.Serialize(_books, _jsonOptions);
                string recordsJson = JsonSerializer.Serialize(_borrowRecords, _jsonOptions);

                File.WriteAllText(BooksFilePath, booksJson);
                File.WriteAllText(RecordsFilePath, recordsJson);

                Console.WriteLine("Veriler kaydedildi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HATA: Veriler kaydedilemedi. {ex.Message}");
            }
        }

        // JSON dosyalarından verileri yükler.
        // Program başladığında constructor tarafından çağrılır.
        public void LoadData()
        {
            try
            {
                // Kitapları yükle
                if (File.Exists(BooksFilePath))
                {
                    string booksJson = File.ReadAllText(BooksFilePath);
                    _books = JsonSerializer.Deserialize<List<Book>>(booksJson) ?? new List<Book>();
                }

                // Ödünç kayıtlarını yükle
                if (File.Exists(RecordsFilePath))
                {
                    string recordsJson = File.ReadAllText(RecordsFilePath);
                    _borrowRecords = JsonSerializer.Deserialize<List<BorrowRecord>>(recordsJson) ?? new List<BorrowRecord>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UYARI: Veri dosyaları yüklenirken hata oluştu. {ex.Message}");
                // Hata olsa bile program çalışmaya devam etsin
                _books = new List<Book>();
                _borrowRecords = new List<BorrowRecord>();
            }
        }
    }
}
