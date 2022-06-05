using System;
using static Movies.Country;

namespace Movies.Tests;

public class CustomerBuilder
{
    private Country? _country;
    private Age? _age;

    public CustomerBuilder LivingIn(Country country)
    {
        _country = country;
        return this;
    }

    public CustomerBuilder Major()
    {
        _age = FuzzyAge(18, 90);
        return this;
    }

    public CustomerBuilder Minor()
    {
        _age = FuzzyAge(7, 17);
        return this;
    }

    private Age FuzzyAge(int from, int to) => new Random().Next(@from, to).ToAge();

    public Customer Build() => new Customer("Jane Doe", _country ?? France, _age ?? 40.ToAge());

    public static CustomerBuilder ANewCustomer() => new();
}