namespace Movies;

public record Age
{
    public int Value { get; }

    private Age(int age) => Value = age;

    public static Age From(int age)
    {
        if (age is < 0 or > 120)
            throw new ArgumentOutOfRangeException("age");
        return new Age(age);
    }

    public static bool operator >(Age from, int compare) => from.Value > compare;
    public static bool operator <(Age from, int compare) => from.Value < compare;
}