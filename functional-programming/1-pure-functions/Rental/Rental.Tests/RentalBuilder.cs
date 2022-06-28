using System;

namespace Rental.Tests;

public class RentalBuilder
{
    private DateOnly _date;
    private double _amount;
    private string? _label;

    public static RentalBuilder ARental() => new();

    public RentalBuilder OnThe(DateOnly date)
    {
        _date = date;
        return this;
    }

    public RentalBuilder Labelled(string? label)
    {
        _label = label;
        return this;
    }

    public RentalBuilder For(double amount)
    {
        _amount = amount;
        return this;
    }

    public Rental Build() => new Rental(_label, _date, _amount);
}