using System.Collections.Generic;

namespace Library.Models
{
  public class Book
  {
    public Book()
    {
      this.JoinEntities = new HashSet<AuthorBook>();
      //this.Checkouts = new HashSet<BookPatron>();
    }

    public int BookId { get; set; }
    public string Title { get; set; }

    public virtual ApplicationUser User { get; set; }

    public virtual ICollection<AuthorBook> JoinEntities { get; }
    //public ICollection<BookPatron> Checkouts { get; }
  }
}