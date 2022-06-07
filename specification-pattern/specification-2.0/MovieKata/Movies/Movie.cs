namespace Movies;

public record Movie(string Name, MpaaRating MpaaRating, Country[] restrictedIn);
public enum MpaaRating { G, PG, PG13, R, NC17 }