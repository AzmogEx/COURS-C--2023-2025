using System;
using OpenGLDotNet;


/*
 * 
 * 
 * Exemple de base d'une application OPENGL. Cet exemple est destiné au SIO 1B CCI de nimes. 
 * C'est une base pour développer un ensemble de projets - session 2022
 */
namespace BASE_OPEN_GL {
    public enum DIRECTION { AUCUN = 0, HAUT = 1, BAS = 2, GAUCHE = 4, DROITE = 8 };

    class C_JOUEUR :C_PERSONNAGE {

        public DIRECTION Direction;
        public double Vitesse_Deplacement;
        public C_JOUEUR() {

            Vitesse_Deplacement = 0.04;
            Id = Guid.NewGuid();

            Random Generateur = new Random();
            R = Generateur.NextDouble();
            G = Generateur.NextDouble();
            B = Generateur.NextDouble();
        }
        //--------------------------------------
        public override void Dessine_Toi() {
            FG.SolidSphere(1,20,20);
        }

        public void Deplace_Toi() {
            if((Direction & DIRECTION.HAUT) == DIRECTION.HAUT) {
                if (Y<Zone_Affichage.Limite_V) Y += Vitesse_Deplacement;
              };

            if((Direction & DIRECTION.BAS) == DIRECTION.BAS) {
                if (Y>-Zone_Affichage.Limite_V) Y -= Vitesse_Deplacement;
            }

            if((Direction & DIRECTION.DROITE) == DIRECTION.DROITE) {
                if (X<Zone_Affichage.Limite_H) X += Vitesse_Deplacement;
            }

            if((Direction & DIRECTION.GAUCHE) == DIRECTION.GAUCHE) {
                if (X>-Zone_Affichage.Limite_H) X -= Vitesse_Deplacement;
            }
            //Envoi du message dans le réseaux
            if (Direction != DIRECTION.AUCUN) {
                RESEAUX.Emission_Personnage(this);
            }
        }
    }
}
