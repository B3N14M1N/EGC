using CIOBAN.Librarie;
using CIOBAN.Librarie.Basic;
using CIOBAN.Librarie.RandomThings;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIOBAN.Scripturi
{
    internal class Triangle : GameObject
    {

        private KeyboardState lastFrameKeyboard;
        // Un triunghi 
        Triunghi triunghi = new Triunghi();
        // Un set de key pentru schimbarea 
        // colorilor triunghiului randat
        private readonly Key UniColor = Key.V;
        private readonly Key change1Key = Key.Z;
        private Color color1 = Color.White;
        private readonly Key change2Key = Key.X;
        private Color color2 = Color.White;
        private readonly Key change3Key = Key.C;
        private Color color3 = Color.White;

        public override void Start()
        {
            // L3
            // Initializeaza obiectul pentru citire date din fisier
            Administrare_Date administrare = new Administrare_Date(ConfigurationManager.AppSettings["NumeFisier"]);
            triunghi.Position = administrare.GetCoords();
        }

        public override void Update()
        {

            KeyboardState keyboard = Keyboard.GetState();
            // L3
            // Prelucreaza inputul pentru a seta culori noi vertexurilor triunghiului
            // variabila locala schimbat verifica daca s-au schimbat vre-o culoare
            // ca sa afiseze numai o singura data la tastatura;
            if (keyboard.IsKeyDown(UniColor) && lastFrameKeyboard.IsKeyUp(UniColor))
                triunghi.uniColor = !triunghi.uniColor;
            bool schimbat = false;
            if (keyboard.IsKeyDown(change1Key) && lastFrameKeyboard.IsKeyUp(change1Key))
            {
                color1 = RandomThings.GetRandomColor();
                schimbat = true;
            }
            if (keyboard.IsKeyDown(change2Key) && lastFrameKeyboard.IsKeyUp(change2Key))
            {
                color2 = RandomThings.GetRandomColor();
                if (!triunghi.uniColor)
                    schimbat = true;
            }
            if (keyboard.IsKeyDown(change3Key) && lastFrameKeyboard.IsKeyUp(change3Key))
            {
                color3 = RandomThings.GetRandomColor();
                if (!triunghi.uniColor)
                    schimbat = true;
            }
            // L3
            // Daca s-au schimbat vre-o culoare afiseaza culorile triunghiului la consola
            if (schimbat)
            {
                // uniColor de setat pe false pentru modificarea
                // culorii fiecarui vertex
                triunghi.ver1Color = color1;
                triunghi.ver2Color = color2;
                triunghi.ver3Color = color3;
                Console.WriteLine(triunghi.ToString());
            }
            lastFrameKeyboard = keyboard;
        }
        public override void Draw()
        {
            triunghi.Draw();
        }
        public override string ToString()
        {
            return "\nTriangle Color Change Controls:" +
                "\n\tUnicolor - " + UniColor +
                ",\n\tVertex1 - " + change1Key +
                ",\n\tVertex2 - " + change2Key +
                ",\n\tVertex3 - " + change3Key + ".\n";
        }
    }
}
