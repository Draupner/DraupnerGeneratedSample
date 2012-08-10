namespace library.Core.Domain.Model
{
  using System.Collections.Generic;

  public class Book
    {
        public virtual long Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string ISBN { get; set; }
        public virtual IEnumerable<Author> Authors { get; set; }
    }
}
