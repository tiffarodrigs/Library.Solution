namespace Library.Models
{
  public class BookUser
  {
    public int BookUserId { get; set;}
    public int  UserId {get; set;}
    public int BookId {get; set;}
    public virtual ApplicationUser User {get; set;}
    public virtual Book Book {get; set;}
  }
}