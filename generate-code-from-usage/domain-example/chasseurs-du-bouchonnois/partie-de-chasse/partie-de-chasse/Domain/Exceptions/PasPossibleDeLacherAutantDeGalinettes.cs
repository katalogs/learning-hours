namespace Bouchonnois.Domain.Exceptions
{
    public class PasPossibleDeLacherAutantDeGalinettes : Exception
    {
        public PasPossibleDeLacherAutantDeGalinettes(int nbGalinettes) :
            base($"Pas possible de lâcher {nbGalinettes}, pas assez dans la réserver.")
        {
        }
    }
}