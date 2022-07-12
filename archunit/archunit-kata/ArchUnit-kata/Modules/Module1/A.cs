using ArchUnit.Kata.Modules.Module2;

namespace ArchUnit.Kata.Modules.Module1
{
    public class A
    {
        private readonly B b;

        public A(B b)
        {
            this.b = b;
        }
    }
}