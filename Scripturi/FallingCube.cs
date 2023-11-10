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
    public class FallingCube : GameObject
    {
        #region Parametri
        #region Falling
        private KeyboardState lastKeyboardState;
        private bool isFalling = false;
        private bool onGround = false;
        private float initialY;
        private float fallingSpeed = 1f;

        // Parametrii publici
        public float FallingSpeed { 
            get { return fallingSpeed; }
            set { fallingSpeed=value>0?value:1f;}
        }
        public Key colorChangeKey = Key.Z;
        #endregion
        #region Model
        Cub cub;
        #endregion
        #endregion

        #region Constructori
        public FallingCube(float lenght)
        {
            Transform.Position = new Vector3();
            cub = new Cub(lenght);
            cub.Top = RandomThings.GetRandomColor(1);
            cub.Bottom = RandomThings.GetRandomColor(10);
            cub.isVisible = true;
        }
        public FallingCube()
        {
            Transform.Position = new Vector3();
            cub = new Cub();
            cub.Top = RandomThings.GetRandomColor(1);
            cub.Bottom = RandomThings.GetRandomColor(10);
            cub.isVisible = true;
        }
        public FallingCube(Vector3 position, float lenght)
        {
            Transform.Position = position;
            cub = new Cub(lenght);
            cub.Top = RandomThings.GetRandomColor(1);
            cub.Bottom = RandomThings.GetRandomColor(10);
            cub.isVisible = true;
        }
        #endregion

        public override void Start()
        {
            lastKeyboardState = Keyboard.GetState();
            initialY=Transform.Position.Y;
        }
        public override void Update()
        {
            // Preia starea perifericelor
            MouseState mouse = Mouse.GetState();
            KeyboardState keyboard = Keyboard.GetState();

            // Verifica daca s-a apasat o data tasta pentru schimbarea culorii
            if (keyboard.IsKeyDown(colorChangeKey) && !lastKeyboardState.IsKeyDown(colorChangeKey))
            {
                // Genereaza culori aleatorii pentru Top si Bottom
                Random seed = new Random();
                cub.Top = RandomThings.GetRandomColor(seed.Next());
                cub.Bottom = RandomThings.GetRandomColor();
            }
            // Daca nu cade si daca e apasat butonul stanga mouse
            // seteaza obiectul sa cada
            if (!isFalling && mouse.IsButtonDown(MouseButton.Left))
            {
                isFalling = true;
            }
            if(isFalling&& !onGround)
            {
                Transform.Position -= new Vector3(0f, FallingSpeed * (float)Time.deltaTime, 0f);
                if (Transform.Position.Y <= cub.Length / 2)
                { 
                    isFalling = false;
                    onGround = true;
                }
            }
            if(onGround && mouse.IsButtonDown(MouseButton.Right))
            {
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
            cub.Draw();
            GL.PopMatrix();
        }
    }
}
