namespace Movies;

public record Age
{
    private readonly int _age;

    public int Value => _age;

    private Age(int age) => _age = age;

    public static Age From(int age)
    {
        if (age is < 0 or > 120)
            throw new ArgumentOutOfRangeException("age");
        return new Age(age);
    }
}