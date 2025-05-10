using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
internal class Program {

    static int F(int n) {
        //if (n == 0) return 0;
        //else {
        //    return 10 + F(n-1);
        //}

        if (n!=0) return F(n - 1) + 10;
        else return 0;
    }
    //--------------------------------
    static int Factorielle(int n) {
        if (n>1) return n * Factorielle(n - 1);
       else return 1;
    }
    //--------------------------------
    static int Suite_Fibonachi(int n) {
        if(n == 0) return 0;
        if(n > 1) return Suite_Fibonachi(n-1) + Suite_Fibonachi(n - 2);
        else return 1;
    }
    //--------------------------------
    static void Main() {
        long Resultat = Suite_Fibonachi(5);
        Console.WriteLine(Resultat);
    }
}
