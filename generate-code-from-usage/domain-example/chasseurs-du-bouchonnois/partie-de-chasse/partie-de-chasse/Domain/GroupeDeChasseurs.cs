using Bouchonnois.Domain.Exceptions;

namespace Bouchonnois.Domain
{
    public class GroupeDeChasseurs
    {
        private readonly Chasseur[] _chasseurs;
        private Réserve? _réserve;

        private GroupeDeChasseurs(params Chasseur[] chasseurs) => _chasseurs = chasseurs;

        public static GroupeDeChasseurs New(params Chasseur[] chasseurs) => new(chasseurs);

        public GroupeDeChasseurs AvecUneReserve(int nbGalinettes)
        {
            _réserve = new Réserve(nbGalinettes);
            return this;
        }

        public bool EstBrocouille() => _chasseurs.All(_ => _.EstBrocouille());

        public Chasseur? QuiATuéLePlusDeGalinettes() => _chasseurs.MaxBy(_ => _.NbGalinetteTuées);

        public int EtatDeLaRéserve() => _réserve?.NbGalinettes ?? 0;

        public void LacherGalinettes(int nbGalinettes, TerrainDeChasse terrain)
        {
            CheckRéserve();

            _réserve!.LacherGalinettes(nbGalinettes, terrain);
            Console.WriteLine($"Ils décident donc de lâcher {nbGalinettes} galinettes.");
        }

        private void CheckRéserve()
        {
            if (_réserve == null)
                throw new PasDeRéserveDisponiblePourOrganiserLeLâcher();
        }

        public void TirerBallesRestantes(TerrainDeChasse terrain)
        {
            Console.WriteLine("Ils tirent toutes leurs balles restantes");
            _chasseurs
                .ToList()
                .ForEach(chasseur => chasseur.TireBallesRestantes(terrain));
        }

        public override string ToString() => string.Join(',', _chasseurs.Select(_ => _.Name));
    }
}