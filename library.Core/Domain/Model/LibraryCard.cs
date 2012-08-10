namespace library.Core.Domain.Model
{
  using System.Collections.Generic;

  public class LibraryCard
    {
        public virtual long Id { get; set; }
        public virtual int Number { get; set; }
        public virtual IEnumerable<Book> Loans { get; set; }
    
        public LibraryCard()
        {
            Loans = new List<Book>();
        }
    }
}
