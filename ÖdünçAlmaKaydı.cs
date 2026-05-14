namespace LibraryManagementSystem.Models
{
    // BorrowRecord sınıfı: Bir kitabın ödünç alınma kaydını tutar.
    // Her ödünç alma işlemi için bir BorrowRecord nesnesi oluşturulur.
    public class BorrowRecord
    {
        // Hangi kitap ödünç alındı?
        public int BookId { get; set; }

        // Kitabı alan öğrencinin adı
        public string StudentName { get; set; } = "";

        // Kitabın ödünç alındığı tarih
        public DateTime BorrowDate { get; set; }

        // Kitabın iade edildiği tarih (null ise henüz iade edilmemiş demektir)
        public DateTime? ReturnDate { get; set; }

        // ── Constructor ──────────────────────────────────────────────────────────
        public BorrowRecord(int bookId, string studentName, DateTime borrowDate)
        {
            BookId = bookId;
            StudentName = studentName;
            BorrowDate = borrowDate;
            ReturnDate = null; // Henüz iade edilmedi
        }

        // System.Text.Json için parametresiz constructor
        public BorrowRecord() { }

        // Kayıt bilgisini tek satırda göstermek için kullanılır.
        public override string ToString()
        {
            string returnInfo = ReturnDate.HasValue
                ? ReturnDate.Value.ToShortDateString()
                : "Henüz iade edilmedi";

            return $"Kitap ID: {BookId} | Öğrenci: {StudentName,-15} | " +
                   $"Alış: {BorrowDate.ToShortDateString()} | İade: {returnInfo}";
        }
    }
}