using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json;
using MemoryPack;
using OpenGLDotNet;
//RAJOUTER JEU TRAP TRAP======================================
//QUAND MA SPHERE TOUCHE LA SPHERE D'UN AUTRE RAJOUTER 1 POINT

/*
 * 
 * 
 * Exemple de base d'une application OPENGL. Cet exemple est destiné au SIO 1B CCI de nimes. 
 * C'est une base pour développer un ensemble de projets - session 2022
 */
namespace BASE_OPEN_GL {

    static class RESEAUX {
        const int PORT_DEFAUT = 9999;
        static Socket Le_Socket;
        static IPEndPoint Adresse_Process_Distant;
        static IPEndPoint Adresse_Process_Local;
        static EndPoint Adresse_Process_Client;



        //----------------------------------
        public static void Initialisation() {
            Le_Socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);

            IPAddress Adresse_Host_Distant = IPAddress.Parse("10.5.104.7");

            Adresse_Process_Distant = new IPEndPoint(Adresse_Host_Distant,PORT_DEFAUT);

            Adresse_Process_Local = new IPEndPoint(IPAddress.Any,9999);
            try {
                Le_Socket.Bind(Adresse_Process_Local);
            } catch (Exception P_Erreur) {
                Console.WriteLine(P_Erreur.Message);
            }

            Adresse_Process_Client = new IPEndPoint(IPAddress.Any,0);

        }
        //------------------------------------
        public static void Emission_Personnage(C_PERSONNAGE P_Personnage) {
            //==========================VERSION MEMORY PACK================
            var Message = MemoryPackSerializer.Serialize(P_Personnage);
            Le_Socket.SendTo(Message,Adresse_Process_Distant);

            //==========================VERSION JSON========================
            //string Data_Json = JsonSerializer.Serialize(P_Personnage);
            ////Console.WriteLine(Data_Json);

            //byte[] Message = Encoding.ASCII.GetBytes(Data_Json);

            //Le_Socket.SendTo(Message,Adresse_Process_Distant);
        }
        //-------------------------------------------
        public static C_PERSONNAGE Reception() {

            if(Le_Socket.Available > 0) {
                //==========================VERSION MEMORY PACK===============
                byte[] Message = new byte[Le_Socket.Available];
                int Taille = Le_Socket.ReceiveFrom(Message,ref Adresse_Process_Client);
                var Personage = MemoryPackSerializer.Deserialize<C_PERSONNAGE>(Message);
                return Personage;

                //==========================VERSION JSON================
                //byte[] Message = new byte[Le_Socket.Available];

                //int Taille_Recu = Le_Socket.ReceiveFrom(Message,ref Adresse_Process_Client);

                //string Data_Json = Encoding.ASCII.GetString(Message);
                //try {
                //    C_PERSONNAGE Personnage = JsonSerializer.Deserialize<C_PERSONNAGE>(Data_Json);
                //    return Personnage;
                //} catch (Exception) { }
            }
            return null;
        }
    }

    partial class Program {

        // vous pouvez mettre vos variables globales ici
        // vous pouvez mettre vos fonctions (méthodes statiques) ici
        static C_JOUEUR Moi;

        static List<C_PERSONNAGE> Les_Personages = new List<C_PERSONNAGE>();


        //==========================================================
        // Cette fonction est invoquée qu'une seule fois avant que le moteur OpenGl travaille.
        // elle est utile pour initialiser des éléments globaux à l'application
        static void Initialisation_Animation() {
            Moi = new C_JOUEUR();

            RESEAUX.Initialisation();

            OPENGL_Active_Reflexion();
        }


        //==========================================================
        // Cette fonction est invoquée par le Moteur de manière périodique pour Afficher une Frame
        static void Afficher_Ma_Scene() {
            // c'est ici que vous pouvez coder l'affichage d'une frame

            Moi.Affiche_Toi();

            foreach(var Un_Personnage in Les_Personages) {
                Un_Personnage.Affiche_Toi();
            }
        }

        //=========================================================
        // cette fonction est invoquée en boucle par openGl.
        // Peut être utilisée pour modifier des variables globales utilisée dans "Afficher_Ma_Scene"
        static void Animation_Scene() {
            Moi.Deplace_Toi();

            C_PERSONNAGE Perso_Recu = RESEAUX.Reception();

            if (Perso_Recu != null) {

                if(Perso_Recu.Id != Moi.Id) {



                    bool Trouve = false;
                    foreach(var Un_Personnage in Les_Personages) {
                        if(Un_Personnage.Id == Perso_Recu.Id) {

                            Un_Personnage.X = Perso_Recu.X;
                            Un_Personnage.Y = Perso_Recu.Y;
                            Trouve = true;
                            break;
                        }
                    }
                    if(Trouve == false) {
                        Les_Personages.Add(Perso_Recu);
                    }
                }
            }

            //fortement recommandé
            FG.PostRedisplay(); // Pour demander de réafficher une Frame afin de tenir compte des modifications
        }

        //======================================================================
        // cette fonction est invoquée par OpenGl lorsqu'on appuie sur une touche spéciale (flèches, Fx, ...)
        // P_Touche contient le code de la touche, P_X et P_Y contiennent les coordonnées de la souris quand on appuie sur une touche
        static void Gestion_Touches_Speciales(int P_Touche,int P_X,int P_Y) {
            //if (P_Touche == FG.GLUT_KEY_F1) FG.FullScreen();    
            //if (P_Touche == FG.GLUT_KEY_F2) FG.LeaveFullScreen(); 
            if(P_Touche == FG.GLUT_KEY_UP) Moi.Direction |= DIRECTION.HAUT;
            if(P_Touche == FG.GLUT_KEY_DOWN) Moi.Direction |= DIRECTION.BAS;

            if(P_Touche == FG.GLUT_KEY_RIGHT) Moi.Direction |= DIRECTION.DROITE;
            if(P_Touche == FG.GLUT_KEY_LEFT) Moi.Direction |= DIRECTION.GAUCHE;

            //fortement recommandé
            FG.PostRedisplay(); // Pour demander de réafficher une Frame afin de tenir compte des modifications
        }

        //======================================================================
        // cette fonction est invoquée par OpenGl lorsqu'on relache une touche spéciale (flèches, Fx, ...)
        // P_Touche contient le code de la touche, P_X et P_Y contiennent les coordonnées de la souris quand on relache sur une touche
        static void Gestion_Touches_Speciales_Relachees(int P_Touche,int P_X,int P_Y) {
            if(P_Touche == FG.GLUT_KEY_UP) Moi.Direction &= ~DIRECTION.HAUT;
            if(P_Touche == FG.GLUT_KEY_DOWN) Moi.Direction &= ~DIRECTION.BAS;

            if(P_Touche == FG.GLUT_KEY_RIGHT) Moi.Direction &= ~DIRECTION.DROITE;
            if(P_Touche == FG.GLUT_KEY_LEFT) Moi.Direction &= ~DIRECTION.GAUCHE;
            // FG.PostRedisplay(); // Pour demander de réafficher une Frame afin de tenir compte des modifications
        }

        //======================================================================
        // cette fonction est invoquée par OpenGl lorsqu'on appuie sur une touche normale (A,Z,E, ...)
        // P_Touche contient le code de la touche, P_X et P_Y contiennent les coordonnées de la souris quand on appuie sur une touche
        static void Gestion_Clavier(byte P_Touche,int P_X,int P_Y) {
            // 27 est le code de la touche "Echap"
            if(P_Touche == 27) FG.LeaveMainLoop();



            //fortement recommandé
            FG.PostRedisplay(); // Pour demander de réafficher une Frame afin de tenir compte des modifications
        }

        //======================================================================
        // cette fonction est invoquée par OpenGl lorsqu'on relache une touche normale (A,Z,E, ...)
        // P_Touche contient le code de la touche, P_X et P_Y contiennent les coordonnées de la souris quand on relache sur une touche
        static void Gestion_Clavier_Relache(byte P_Touche,int P_X,int P_Y) {


            // FG.PostRedisplay(); // Pour demander de réafficher une Frame afin de tenir compte des modifications
        }

        //==================================================================================
        // cette fonction est invoquée par OpenGl lorsqu'on appuie sur un bouton de la souris
        // P_Bouton contient le code du bouton (gauche ou droite), P_Etat son etat, les coordonnées de la souris quand on appuie sur un bouton sont dans P_X et P_Y

        static void Gestion_Bouton_Souris(int P_Bouton,int P_Etat,int P_X,int P_Y) {

            //fortement recommandé
            FG.PostRedisplay(); // Pour demander de réafficher une Frame afin de tenir compte des modifications
        }

        //====================================================================
        // cette fonction est invoquée par OpenGl lorsqu'on tourne la molette de la souris
        // P_Molette contient le code de la molette, P_Sens son sens de rotation, les coordonnées de la souris quand on tourne la molette sont dans P_X et P_Y

        static void Gestion_Molette(int P_Molette,int P_Sens,int P_X,int P_Y) {

            //  FG.PostRedisplay(); // Pour demander de réafficher une Frame afin de tenir compte des modifications
        }

        //====================================================================
        // cette fonction est invoquée par OpenGl lorsqu'on bouge la souris sans appuyer sur un bouton
        // les coordonnées de la souris ont dans P_X et P_Y
        static void Gestion_Souris_Libre(int P_X,int P_Y) {

            // FG.PostRedisplay(); // Pour demander de réafficher une Frame afin de tenir compte des modifications
        }


        //====================================================================
        // cette fonction est invoquée par OpenGl lorsqu'on bouge la souris tout en appuyant sur un bouton
        // les coordonnées de la souris ont dans P_X et P_Y
        static void Gestion_Souris_Clique(int P_X,int P_Y) {

            // FG.PostRedisplay(); // Pour demander de réafficher une Frame afin de tenir compte des modifications
        }
    }
}
