namespace ArchUnit.Kata.Examples.Models
{
    public record ApiResponse<TData>(TData Data, ApiError[]? Errors = null) { }
}