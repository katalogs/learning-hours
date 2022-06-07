namespace Movies;

public static class DomainExtensions
{
    public static Age ToAge(this int age) => Age.From(age);
}