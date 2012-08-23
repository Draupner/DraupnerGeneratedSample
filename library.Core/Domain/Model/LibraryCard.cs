namespace Library.Core.Domain.Model
{
  using System.Collections.Generic;

  public class LibraryCard
  {
    public virtual long Id { get; set; }
    public virtual int Number { get; set; }
    public virtual ICollection<Book> Loans { get; set; }
  }
}
