using CIOBAN.Librarie;
using CIOBAN.Librarie.Basic;
using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Platform;
using System.Configuration;
using OpenTK.Graphics;
using CIOBAN.Librarie.RandomThings;
using System.Collections.Generic;
/*
* CIOBAN BENIAMIN
* 3134A
*/
namespace CIOBAN.Scripturi
{
    // L4
    // O clasa care randeaza un cub de dimensiuni
    // variabile (determinate de parametrul "length")
    // la pozitia determinata de parametrul "Transform" 
    // mostenit din "GameObject".
    // In metoda Update se proceseaza evenimete pentru
    // a face cubul sa cada, si alte modificari (ex. schimbare culoare)
    public class FallingObject : GameObject
    {
        #region Parametri
        #region Falling
        private KeyboardState lastKeyboardState;
        private bool isFalling = false;
        private bool onGround = false;
        private float initialY;
        private float fallingSpeed = 1f;
        private float size = 1f;
        // Parametrii publici
        public float FallingSpeed { 
            get { return fallingSpeed; }
            set { fallingSpeed=value>0?value:1f;}
        }
        public Key colorChangeKey = Key.B;
        #endregion

        #region Model 3d
        // Un model 3d
        IRenderer cub;
        #endregion
        #endregion

        #region Constructori
        public FallingObject()
        {
            Transform.Position = new Vector3();
        }
        public FallingObject(Vector3 position)
        {
            Transform.Position = position;
        }
        #endregion

        public override void Start()
        {
            Administrare_Date date = new Administrare_Date(ConfigurationManager.AppSettings["NumeFisier"]);
            Transform.Position = date.GetCoords();
            lastKeyboardState = Keyboard.GetState();
            initialY=Transform.Position.Y;
        }
        public override void Update()
        {
            // Preia starea perifericelor
            MouseState mouse = Mouse.GetState();
            KeyboardState keyboard = Keyboard.GetState();

            // Verifica daca s-a apasat o data tasta pentru schimbarea culorii
            if (cub!=null && keyboard.IsKeyDown(colorChangeKey) && !lastKeyboardState.IsKeyDown(colorChangeKey))
            {
                // Genereaza culori aleatorii pentru Top si Bottom
                Random seed = new Random();
                cub.SetColors(new List<Color>() { RandomThings.GetRandomColor(seed.Next()), RandomThings.GetRandomColor(seed.Next()) });
                Console.WriteLine(cub.ToString());
            }
            // Daca nu cade si daca e apasat butonul stanga mouse
            // seteaza obiectul sa cada
            if (!isFalling && mouse.IsButtonDown(MouseButton.Left))
            {
                cub = new Cub(size);
                isFalling = true;
            }
            if(isFalling && !onGround)
            {
                Transform.Position -= new Vector3(0f, FallingSpeed * (float)Time.deltaTime, 0f);
                if (Transform.Position.Y <= size / 2)
                { 
                    isFalling = false;
                    onGround = true;
                }
            }
            if(onGround && mouse.IsButtonDown(MouseButton.Right))
            {
                cub = null;
                Transform.Position = new Vector3(Transform.Position.X, initialY, Transform.Position.Z);
                isFalling = false;
                onGround = false;
            }

            lastKeyboardState=keyboard;
        }

        public override void Draw()
        {
            GL.PushMatrix();
            GL.Translate(Transform.Position);
            cub?.Draw();
            GL.PopMatrix();
        }
        public override string ToString()
        {
            return "\nFalling Object Controls:" +
                "\n\tFall - Mouse." + MouseButton.Left +
                ",\n\tReset - Mouse."+ MouseButton.Right +
                ",\n\tColor change - " + colorChangeKey +".\n";
        }
    }
}
