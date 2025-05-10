using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO_LISTBOX.DATA;
public class PERSONNE {

    public string Nom { get; set; }
    public string Prenom { get; set; }

    public override string ToString() {
        return $"{Nom} - {Prenom}";
    }
}
public class BASE {
    List<PERSONNE> Les_personnes = null;
    public BASE() {
        Les_personnes = new List<PERSONNE>();
        for(int Compteur = 0; Compteur < 10; Compteur++) {
            PERSONNE Nouvelle = new PERSONNE() {
                Nom = $"NOM_{Compteur + 1}",
                Prenom = $"Prenom_{Compteur + 1}"
            };

            Les_personnes.Add(Nouvelle);
        }
    }
    public List<PERSONNE> Get_All_Personnes() {
        return Les_personnes;
    }
}
