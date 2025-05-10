using System.Diagnostics;

namespace DEMO_LISTE_CHAINEE;

class C_LISTE_CHAINEE {
    class C_MAILLON {
        public int Data;
        public C_MAILLON Suivant = null;
    }
    C_MAILLON Debut = null;

    public void Rajoute_Info(int P_Data) {

        C_MAILLON Nouveau = new C_MAILLON() { Data = P_Data };
        Nouveau.Suivant = Debut;
        Debut = Nouveau;

    }

    public void Affiche_Liste_Iter() {
        var Courant = Debut;

        while(Courant != null) {
            Console.WriteLine(Courant.Data);
            Courant = Courant.Suivant;
        }
    }

    public void Affiche_Liste_Maillons() {
        Affiche_Liste_Recursif(Debut);
    }

    void Affiche_Liste_Recursif(C_MAILLON P_Maillon) {
        if(P_Maillon != null) {
            Console.WriteLine(P_Maillon.Data);
            Affiche_Liste_Recursif(P_Maillon.Suivant);
        }
    }
}

class Program {


    //=============================================
    static void Main(string[] args) {
        //===========V1========================================
        //Debut = new C_Maillon() { Data = 10,Suivant = null };
        //C_Maillon Nouveau = new C_Maillon() { Data = 20,Suivant = null };
        //Debut.Suivant = Nouveau;
        //Nouveau = new C_Maillon() { Data = 30,Suivant = null };
        //Debut.Suivant.Suivant = Nouveau;

        C_LISTE_CHAINEE Ma_liste = new C_LISTE_CHAINEE();
        Ma_liste.Rajoute_Info(10);
        Ma_liste.Rajoute_Info(20);
        Ma_liste.Rajoute_Info(30);
        Ma_liste.Rajoute_Info(87);
        Ma_liste.Rajoute_Info(23);
        Ma_liste.Rajoute_Info(564);
        Ma_liste.Rajoute_Info(3098);

        Ma_liste.Affiche_Liste_Iter();
        Console.WriteLine("-----------");
        Ma_liste.Affiche_Liste_Maillons();
    }
}
