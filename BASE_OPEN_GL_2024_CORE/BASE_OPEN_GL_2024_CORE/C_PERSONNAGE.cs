using System;
using System.Text.Json.Serialization;
using MemoryPack;
using OpenGLDotNet;


/*
 * 
 * 
 * Exemple de base d'une application OPENGL. Cet exemple est destiné au SIO 1B CCI de nimes. 
 * C'est une base pour développer un ensemble de projets - session 2022
 */
namespace BASE_OPEN_GL;
[MemoryPackable]
partial class C_PERSONNAGE {

    public Guid Id;

    public double X;
    public double Y;

    public double R;
    public double G;
    public double B;
    //-----------------------

    public void Affiche_Toi() {
        GL.PushMatrix();

        GL.Translated(X,Y,0);
        GL.Color3d(R,G,B);
        Dessine_Toi();

        GL.PopMatrix();
    }

    public virtual void Dessine_Toi() {
        FG.SolidCube(1);
    }
}
