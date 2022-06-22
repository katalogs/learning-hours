namespace Password;

public record PasswordWithPolicy(string Password, IEnumerable<int> Range, char Letter);