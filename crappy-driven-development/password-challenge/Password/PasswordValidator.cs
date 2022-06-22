namespace Password;

public static class PasswordValidator
{
    private static bool IsValid(PasswordWithPolicy passwordWithPolicy) =>
        passwordWithPolicy.Range
            .Contains(passwordWithPolicy
                .Password
                .Count(p => p == passwordWithPolicy.Letter)
            );

    public static int CountValidPasswords(IEnumerable<string> lines) =>
        lines
            .Select(line => line.ToPasswordWithPolicy())
            .Count(IsValid);
}