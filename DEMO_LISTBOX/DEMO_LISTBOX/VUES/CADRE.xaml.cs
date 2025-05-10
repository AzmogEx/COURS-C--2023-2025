using DEMO_LISTBOX.DATA;
using System.Windows;

namespace DEMO_LISTBOX; 
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class CADRE :Window {

    BASE La_Base = null;
    
    public CADRE() {

        La_Base = new BASE();
        InitializeComponent();

        //List<int> Les_entiers = new List<int>();
        //for(int Compteur = 0; Compteur < 10; Compteur++) {
        //    LST_Info.Items.Add(Compteur);
        //}
        //LST_Info.ItemsSource = Les_entiers;

        LST_Info.ItemsSource = La_Base.Get_All_Personnes();


    }
    PERSONNE Personne_Selectionnee = null;
    private void LST_Info_SelectionChanged(object sender,System.Windows.Controls.SelectionChangedEventArgs e) {
        Personne_Selectionnee = (PERSONNE)LST_Info.SelectedItem;

        if (Personne_Selectionnee != null) {
            TXT_Nom.Text = Personne_Selectionnee.Nom;
            TXT_Prenom.Text = Personne_Selectionnee.Prenom;
        }
    }
}