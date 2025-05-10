using System.Text.Json;

namespace DLL_4G; 
//==================================================
public class C_BASE {

    //-----------------------------------------------
    
    List<C_BASE> La_Base;
    List<C_4G> La_4G;

    //-----------------------------------------------
    public C_BASE() {
        Chargement_Memoire();
    }
    //----------------------------------------------
    void Chargement_Memoire() {
        string Data_Json = File.ReadAllText("4G.json");
        La_Base = JsonSerializer.Deserialize<List<C_BASE>>(Data_Json);
    }
    //-----------------------------------------------
    public void Affiche_4G() {
        foreach(C_4G Un_Element in La_4G) {
            Un_Element.Affichage();
        }
    }


}


