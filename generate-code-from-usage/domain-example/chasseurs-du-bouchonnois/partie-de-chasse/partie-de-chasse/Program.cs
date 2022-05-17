using Bouchonnois.Domain;

var bernard = new Chasseur("Bernard");
var dédé = new Chasseur("Dédé");
var robert = new Chasseur("Robert");

var chasseurs =
    GroupeDeChasseurs
        .New(bernard, dédé, robert)
        .AvecUneReserve(6);

Console.WriteLine($"{chasseurs} font une partie de chasse.");
Console.WriteLine($"Ils partent avec {chasseurs.EtatDeLaRéserve()} galinettes en réserve, et 20 balles chacuns");
Console.WriteLine("Ils passent la matinée à chasser en forêt");

bernard.TireDansLeVide(6);
dédé.TireDansLeVide(2);
robert.TireDansLeVide(15);

if (chasseurs.EstBrocouille())
{
    Console.WriteLine("À midi, ils sont brocouilles.");

    var terrainPourLaRéserve = new TerrainDeChasse();
    chasseurs.LacherGalinettes(5, terrainPourLaRéserve);
    chasseurs.TirerBallesRestantes(terrainPourLaRéserve);

    if (chasseurs.EstBrocouille())
        Console.WriteLine("Tous brocouilles");
    else
    {
        var meilleurChasseur = chasseurs.QuiATuéLePlusDeGalinettes();
        Console.WriteLine(
            $"Le meilleur chasseur du bouchonnois est {meilleurChasseur!.Name} avec {meilleurChasseur.NbGalinetteTuées} galinettes tuées");
    }
}