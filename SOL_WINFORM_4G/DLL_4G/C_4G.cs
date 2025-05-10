using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DLL_4G;
public class C_4G {
    public int Id { get; set; }
    public string Adm { get; set; }
    public int Sup { get; set; }
    public string Sys { get; set; }
    public string Dpt { get; set; }
    public string CP { get; set; }
    public string Type { get; set; }
    public string Adr { get; set; }
    public float[] XY { get; set; }
    public string Etat { get; set; }
    public void Affichage() {
        Console.WriteLine($"{Id,5} {Adm,5} {Sup,5} {Sys,-32} {Dpt, 5}" +
            $"{CP, 5} {Type, 5} {Adr, 5} {XY, 5} {Etat, 5}");
    }

    public class C_BASE();

}

