namespace DEMO_REPERTOIRE; 
internal class Program {

   //static void Affiche_Info_Repertoire_V1() {
   //     Console.WriteLine("Les fichiers:");
   //     Console.WriteLine();
   //     var Reponse_Fichier = Repertoire_Cible.EnumerateFiles();
   //     foreach(var La_Reponse in Reponse_Fichier) {
   //         Console.WriteLine(La_Reponse.Name);
   //     }
   //     Console.WriteLine("-----------------------");

   //     Console.WriteLine("Les dossiers:");
   //     Console.WriteLine();
   //     var Reponse_Dossier = Repertoire_Cible.EnumerateDirectories();
   //     foreach(var La_Reponse in Reponse_Dossier) {
   //         Console.WriteLine(La_Reponse.Name);
   //     }
   // }

    static void Affiche_Info_Repertoire_V2(string P_Chemin_Racine) {
        DirectoryInfo Racine = new DirectoryInfo(P_Chemin_Racine);

        var Les_Elements = Racine.GetFileSystemInfos();

        foreach(var Item in Les_Elements) {

            if((Item.Attributes & FileAttributes.Directory) == FileAttributes.Directory) {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Repertoires:");
                Console.WriteLine($"Nom: {Item.Name}");
                Console.WriteLine($"Emplacement: {Item.FullName}");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("--------------------");
                Affiche_Info_Repertoire_V2(Item.FullName);
            }

            if((Item.Attributes & FileAttributes.Normal) == FileAttributes.Normal) {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Fichiers:");
                Console.WriteLine($"Nom: {Item.Name}");
                Console.WriteLine($"Emplacement: {Item.FullName}");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("========FIN_REPERTOIRE============");
            }
        }
    }
    static void Main(string[] args) {

        string Chemin_Racine = "Y:\\Bts-Sio\\C#\\DEMO_REPERTOIRE";
        Affiche_Info_Repertoire_V2(Chemin_Racine);

    }
}
