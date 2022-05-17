using Bouchonnois.Domain.Exceptions;

namespace Bouchonnois.Domain
{
    public class TerrainDeChasse
    {
        private int _nbGalinettes;

        public void AjouterGalinettes(int nbGalinettes) => _nbGalinettes = nbGalinettes;

        public void UnTirFaitMouche()
        {
            if (_nbGalinettes == 0)
                throw new TasTropPicoléMonVieuxTasRienTouché();
            _nbGalinettes--;
        }
    }
}