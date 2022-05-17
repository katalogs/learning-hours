using Bouchonnois.Domain.Exceptions;

namespace Bouchonnois.Domain
{
    public class Réserve
    {
        private int _nbGalinettes;

        public Réserve(int nbGalinettes) => _nbGalinettes = nbGalinettes;
        public int NbGalinettes => _nbGalinettes;

        public void LacherGalinettes(int nbGalinettes, TerrainDeChasse terrain)
        {
            if (nbGalinettes > _nbGalinettes)
                throw new PasPossibleDeLacherAutantDeGalinettes(nbGalinettes);
        
            terrain.AjouterGalinettes(nbGalinettes);
            _nbGalinettes -= nbGalinettes;
        }
    }
}