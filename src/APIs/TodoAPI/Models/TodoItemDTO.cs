namespace TodoAPI.Models;

public sealed record TodoItemDTO(long Id, string? Name, bool IsComplete);