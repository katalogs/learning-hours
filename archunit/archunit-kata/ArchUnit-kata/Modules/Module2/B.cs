using ArchUnit.Kata.Modules.Module1;

namespace ArchUnit.Kata.Modules.Module2
{
    public class B
    {
        private A CreateA() => new A(this);
    }
}