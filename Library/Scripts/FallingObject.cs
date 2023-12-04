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


        private const float GRAVITY = 9.8f; // m/s^2
        private float initialY; // Pozitia initiala (din fisier)
        private float fallingSpeed = 1f; // m/s
        private float size = 1f;

        private Key colorChangeKey = Key.B;
        private KeyboardState lastKeyboardState;
        // Am creat o lista de Transforms pentru a genera noi obiecte la
        // pozitii aleatorii.
        // Modelul 3d este reutilizat!
        private const float MAX_DISTANCE = 20f; // Dimensiunea (lungimea) ariei unde se pot genera
        private int counts = 100;
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
                transforms.Add(GenerateRandomTransform());
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
                    transforms.Add(GenerateRandomTransform());
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
                    Console.WriteLine("Obiecte curente:\t"+ counts);
                }
            }

            // Daca nu cade si daca e apasat butonul stanga mouse
            // seteaza obiectul sa cada
            if (!isFalling && !onGround && mouse.IsButtonDown(MouseButton.Left))
            {
                cub = new Cub(size);
                cub.SetColors(new List<Color>() { RandomGenerator.GetRandomColor(), RandomGenerator.GetRandomColor() });
                fallingSpeed = 1f;
                isFalling = true;
                onGround = false;
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

                for (int i = 0; i < transforms.Count; i++)
                {
                    transforms[i] = GenerateRandomTransform();
                }
            }

            // Daca cade, scade din lista de transforms, din poz.Y pentru a da impresia de cadere
            // daca ajunge la planul Y=0, seteaza Y=0 (erau unele cazuri in care se opreau sub 0)
            // Se ia in considerare scalarea pe Y
            if(isFalling)
            {
                foreach(Transform position in transforms)
                {
                    if (position.Position.Y >  position.Scale.Y * size / 2)
                    {
                        position.Position -= new Vector3(0f, fallingSpeed * (float)Time.deltaTime, 0f);
                    }
                    else
                    {
                        position.Position = new Vector3(position.Position.X, position.Scale.Y * size / 2, position.Position.Z);
                    }
                }
            }
            lastKeyboardState=keyboard;
        }

        // Genereaza un Transform cu pozitie, rotatie si scala aleatorie
        private Transform GenerateRandomTransform()
        {
            return new Transform(
                RandomGenerator.GetRandomVector3f(new Vector3(-MAX_DISTANCE, 1, -MAX_DISTANCE), new Vector3(MAX_DISTANCE, initialY, MAX_DISTANCE)),
                RandomGenerator.GetRandomVector3(new Vector3(0f, -180f, 0f), new Vector3(0f, 180f, 0f)),
                RandomGenerator.GetRandomVector3f(new Vector3(0.25f, 0.25f, 0.25f), new Vector3(2.5f, 2.5f, 2.5f)));
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
                GL.Rotate(position.Rotation.Y,new Vector3(0f,1f,0f));
                GL.Scale(position.Scale);
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
