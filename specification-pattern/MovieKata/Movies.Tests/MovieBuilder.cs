using System.Collections.Generic;

namespace Movies.Tests;

public class MovieBuilder
{
    private string? _name;
    private readonly List<Country> _restrictedIn = new();

    public static MovieBuilder AMovie() => new();

    public MovieBuilder OnPoutine()
    {
        _name = "Un palais pour Poutine : L'Histoire du plus gros pot-de-vin";
        _restrictedIn.Add(Country.Russia);

        return this;
    }

    public Movie Build() => new(_name ?? "Tenet", MpaaRating.G, _restrictedIn.ToArray());
}