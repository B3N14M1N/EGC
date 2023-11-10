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
using CIOBAN.Librarie.Randomizer;
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
        #region Logic
        private bool isFalling = false;
        private bool onGround = false;
        private float initialY; // Pozitia initiala (din fisier)
        private float fallingSpeed = 1f; // m/s
        private float size = 1f;
        private const float GRAVITY = 9.8f; // m/s^2
        private Key colorChangeKey = Key.B;
        private KeyboardState lastKeyboardState;
        // Am creat o lista de Transforms pentru a genera noi obiecte la
        // pozitii aleatorii.
        // Modelul 3d este reutilizat!
        private int counts = 0;
        private List<Transform> transforms = new List<Transform>();
        private Key addObject = Key.Right;
        private Key removeObject = Key.Left;
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
            lastKeyboardState = Keyboard.GetState();

            Administrare_Date date = new Administrare_Date(ConfigurationManager.AppSettings["NumeFisier"]);
            Transform.Position = date.GetCoords();
            // InitialY coordonata Y din fisier
            initialY=Transform.Position.Y;

            // Initializare lista cu pozitii diferite
            for (int i = 0; i < counts; i++)
            {
                transforms.Add(new Transform(RandomGenerator.GetRandomVector3(new Vector3(-10, 1, -10), new Vector3(10, initialY, 10))));
            }
        }
        public override void Update()
        {
            // Preia starea Mouse-ului si a tastaturii la momentul curent
            MouseState mouse = Mouse.GetState();
            KeyboardState keyboard = Keyboard.GetState();

            // Verifica daca s-a apasat o data tasta pentru schimbarea culorii
            if (cub!=null && keyboard.IsKeyDown(colorChangeKey) && !lastKeyboardState.IsKeyDown(colorChangeKey))
            {
                // Genereaza culori aleatorii pentru Top si Bottom
                cub.SetColors(new List<Color>() { RandomGenerator.GetRandomColor(), RandomGenerator.GetRandomColor() });
                Console.WriteLine(cub.ToString());
            }

            // Adauga/Sterge obiecte doar cand nu este utilizat obiectul randat,
            // adica cand nu face nimic codul.
            if (cub == null)
            {
                bool changed=false;
                if(keyboard.IsKeyDown(addObject) && !lastKeyboardState.IsKeyDown(addObject))
                {
                    transforms.Add(new Transform(RandomGenerator.GetRandomVector3(new Vector3(-10, 1, -10), new Vector3(10, initialY, 10))));
                    counts++;
                    changed = true;
                }
                if (counts!=0 && keyboard.IsKeyDown(removeObject) && !lastKeyboardState.IsKeyDown(removeObject))
                {
                    transforms.RemoveAt(0);
                    counts--;
                    changed = true;
                }
                if(changed)
                {
                    Console.WriteLine("\nObiecte curente:\t"+ counts);
                }
            }

            // Daca nu cade si daca e apasat butonul stanga mouse
            // seteaza obiectul sa cada
            if (!isFalling && mouse.IsButtonDown(MouseButton.Left))
            {
                cub = new Cub(size);
                fallingSpeed = 1f;
                isFalling = true;
            }
            // Cat timp cade scade din coordonata Y, o distanta afectata de gravitatie
            if(isFalling && !onGround)
            {
                Transform.Position -= new Vector3(0f, fallingSpeed * (float)Time.deltaTime, 0f);
                fallingSpeed += GRAVITY * (float)Time.deltaTime;
                if (Transform.Position.Y <= size / 2)
                {
                    Transform.Position = new Vector3(Transform.Position.X, size/2, Transform.Position.Z);
                    isFalling = false;
                    onGround = true;
                }
            }
            // Daca nu mai cade, si este apasat mouse right
            // reseteaza complet
            if(onGround && mouse.IsButtonDown(MouseButton.Right))
            {
                cub = null;
                Transform.Position = new Vector3(Transform.Position.X, initialY, Transform.Position.Z);
                isFalling = false;
                onGround = false;

                foreach(Transform position in transforms)
                {
                    position.Position = RandomGenerator.GetRandomVector3(new Vector3(-10, 1, -10), new Vector3(10, initialY, 10));
                }
            }
            if(isFalling)
            {
                foreach(Transform position in transforms)
                {
                    if (position.Position.Y >  size / 2)
                    {
                        position.Position -= new Vector3(0f, fallingSpeed * (float)Time.deltaTime, 0f);
                    }
                    else
                    {
                        position.Position = new Vector3(position.Position.X, size/2, position.Position.Z);
                    }
                }
            }
            lastKeyboardState=keyboard;
        }

        public override void Draw()
        {
            GL.PushMatrix();
            GL.Translate(Transform.Position);
            cub?.Draw();
            GL.PopMatrix();
            foreach(Transform position in transforms)
            {
                GL.PushMatrix();
                GL.Translate(position.Position);
                cub?.Draw();
                GL.PopMatrix();
            }
        }
        public override string ToString()
        {
            return "\nFalling Object Controls:" +
                "\n\tFall - Mouse." + MouseButton.Left +
                ",\n\tReset - Mouse." + MouseButton.Right +
                ",\n\tAdd (+1) Objects - " + addObject +
                ",\n\tRemove (-1) Objects - " + removeObject +
                ",\n\tColor change - " + colorChangeKey + ".\n";
        }
    }
}
