using Bouchonnois.Domain.Exceptions;

namespace Bouchonnois.Domain
{
    public class Chasseur
    {
        private int _nbGalinettesTuées = 0;
        private int _ballesRestantes;
        private readonly Random _shootAgility;
        public string Name { get; }

        public int NbGalinetteTuées => _nbGalinettesTuées;
        public Chasseur(string name)
        {
            Name = name;
            _ballesRestantes = 20;
            _shootAgility = new Random();
        }

        public void TireDansLeVide(int nombreTir)
        {
            Enumerable.Range(0, nombreTir)
                .ToList()
                .ForEach(_ => TireDansLeVide());
            Console.WriteLine($"{Name} tire {nombreTir} balles dans le vide...");
        }

        private void TireDansLeVide() => _ballesRestantes--;

        public bool EstBrocouille() => _nbGalinettesTuées == 0;

        public void TireBallesRestantes(TerrainDeChasse terrain) =>
            Enumerable.Range(0, _ballesRestantes)
                .ToList()
                .ForEach(_ => Tire(terrain));

        private void Tire(TerrainDeChasse terrain)
        {
            if (_ballesRestantes == 0)
                throw new TasPlusDeBallesMonVieuxChasseALaMain();

            if (ATouchéQuelqueChose())
            {
                Console.WriteLine($"{Name} a touché quelque chose");
                terrain.UnTirFaitMouche();
                _nbGalinettesTuées++;
            }
        }

        private bool ATouchéQuelqueChose() 
            => _shootAgility.Next(1, 10) == 2;
    }
}