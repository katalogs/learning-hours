namespace ParameterizedTests;

public static class TimeUtility
{
    public static string GetTimeOfDay()
    {
        var time = DateTime.Now;

        return time.Hour switch
        {
            >= 0 and < 6 => "Night",
            >= 6 and < 12 => "Morning",
            >= 12 and < 18 => "Afternoon",
            _ => "Evening"
        };
    }
}