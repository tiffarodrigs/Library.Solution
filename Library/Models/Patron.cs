using System.Collections.Generic;

namespace Library.Models
{
  public class Patron
  {
    public Patron()
    {
      this.Checkouts = new HashSet<BookPatron>();
    }

    public int PatronId { get; set; }
    public string PatronName { get; set; }

    public virtual ApplicationUser User { get; set; }

    public  ICollection<BookPatron> Checkouts { get; }
  }
}