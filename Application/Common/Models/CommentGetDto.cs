namespace Application.Common.Models;

public class CommentGetDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? AuthorId { get; set; }
    public Guid? PostId { get; set; }
}
