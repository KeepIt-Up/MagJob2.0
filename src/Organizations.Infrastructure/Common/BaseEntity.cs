namespace Organizations.API.Common.Models;

public class BaseEntity<T> where T : notnull
{
    public T Id { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}