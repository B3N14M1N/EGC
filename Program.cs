using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Platform;
/*
 * CIOBAN BENIAMIN
 * 3134A
 * REZOLVARI
 * * L2
 * * L3
 * * L4
 */
namespace CIOBAN
{
    class Program : GameWindow
    {
        #region Parametrii
        //Laboratorul 2
        #region L2
        private KeyboardState lastFrameKeyboard;

        private Key moveTriangleKey = Key.Q;
        private bool moveTriangle = true;
        private Key renderTriangleKey = Key.E;
        private bool renderTriangle = true;

        Vector3 poz = new Vector3();
        // zPoz folosit pentru scalarea tiunghiului folosind rotita
        private float zPoz = 0;
        private float mouseSensitivity = 0.1f;

        #endregion
        #region L3-4
        // Folosesc clasa creata "Camera"
        // pentru a putea controla view-ul scenei
        public Camera camera;
        // rotX si rotY sunt folosite pentru a calcula rotatia camerei
        // folosind datele de la mouse .X si .Y
        private float rotX = 0;
        private float rotY = 0;
        // "movementSpeed" si "mouseSensitivity" (din L2)
        // determina sensivitatea controlului camerei
        private float movementSpeed = 2f;
        // Un set de key pentru determinarea inputului
        // care va misca camera
        private readonly Key cameraForwardKey = Key.W;
        private readonly Key cameraBackwardsKey = Key.S;
        private readonly Key cameraLeftKey = Key.A;
        private readonly Key cameraRightKey = Key.D;
        private readonly Key cameraUpKey = Key.LShift;
        private readonly Key cameraDownKey = Key.LControl;
        private readonly Key lockCameraKey = Key.Number1;
        // "lockCamera" blocheaza controlul camerei
        // daca este setat pe True
        private bool lockCamera = false;
        // Un set de key pentru schimbarea 
        // colorilor triunghiului randat
        private readonly Key change1Key = Key.Z;
        private Color color1 = Color.White;
        private readonly Key change2Key = Key.X;
        private Color color2 = Color.White;
        private readonly Key change3Key = Key.C;
        private Color color3 = Color.White;
        #endregion
        #endregion
        #region Constructor
        public Program() : base(800, 600)
        {
            KeyDown += WindowSettings;
            // L3 
            // Initializeaza camera si o pozitie aleatorie
            camera = new Camera(new Vector3(1,1,5));
        }
        // Functie in care prelucreaza inputul pentru modificarea ferestrei
        #endregion
        #region Evenimete
        void WindowSettings(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit();

            if (e.Key == Key.F11)
                this.WindowState = (this.WindowState == WindowState.Fullscreen)? WindowState.Normal : WindowState.Fullscreen;
        }
        #endregion
        #region Metode
        // L3
        // Functie pentru generarea unei culor aleatorie
        public Color GetRandomColor()
        {
            Random rnd = new Random();
            return Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
        }

        // L2
        // Functie petnru preluarea si prelucrarea  
        // inputului mouse-ului si a tastaturii.
        public void GetInput(float deltaTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            // Prelucreaza variabilele boolene pentru a
            // lucra doar la o singura apasare a tasteii
            moveTriangle = (keyboard.IsKeyDown(moveTriangleKey) && lastFrameKeyboard.IsKeyUp(moveTriangleKey)) ? !moveTriangle : moveTriangle;
            renderTriangle = (keyboard.IsKeyDown(renderTriangleKey) && lastFrameKeyboard.IsKeyUp(renderTriangleKey)) ? !renderTriangle : renderTriangle;

            // Misca triunghiul dupa inputul mouse-ului
            if(moveTriangle)
            {
                poz.X = mouse.X  * mouseSensitivity;
                poz.Y = -mouse.Y * mouseSensitivity;

                if(zPoz - mouse.WheelPrecise > 0)
                {
                    poz.Z -= deltaTime;
                }
                else if(zPoz - mouse.WheelPrecise < 0)
                    poz.Z += deltaTime;
                if (poz.Z == 0)
                    poz.Z = .1f;
                zPoz = mouse.WheelPrecise;
            }

            // L3
            // Prelucreaza inputul pentru a seta culori noi vertexurilor triunghiului
            if (keyboard.IsKeyDown(change1Key) && lastFrameKeyboard.IsKeyUp(change1Key))
                color1 = GetRandomColor();
            if (keyboard.IsKeyDown(change2Key) && lastFrameKeyboard.IsKeyUp(change2Key))
                color2 = GetRandomColor();
            if (keyboard.IsKeyDown(change3Key) && lastFrameKeyboard.IsKeyUp(change3Key))
                color3 = GetRandomColor();

            // L3
            // Blocheaza sau nu controlul camerei
            lockCamera = (keyboard.IsKeyDown(lockCameraKey) && lastFrameKeyboard.IsKeyUp(lockCameraKey)) ? !lockCamera : lockCamera;

            // L3
            // Prelucreaza inputul pentru a misca camera
            if(!lockCamera)
            {
                // cameraPosition este pozitia camerei
                // Reprezinta un vector3 (este vizualizat ca un vector local
                // adica z+ fata, z- spate,
                // y+ sus, y- jos,
                // x- stanga, x+ dreapta)
                Vector3 cameraPosition = camera.position;
                cameraPosition.X = (keyboard.IsKeyDown(cameraLeftKey) ? (-1f) : keyboard.IsKeyDown(cameraRightKey) ? (1f) : 0) * deltaTime * movementSpeed;
                cameraPosition.Z = (keyboard.IsKeyDown(cameraDownKey) ? (-1f) : keyboard.IsKeyDown(cameraUpKey) ? (1f) : 0) * deltaTime * movementSpeed;
                cameraPosition.Y = (keyboard.IsKeyDown(cameraBackwardsKey) ? (-1f) : keyboard.IsKeyDown(cameraForwardKey) ? (1f) : 0) * deltaTime * movementSpeed;

                // Apeleaza metodele de miscare a pozitiei si a rotatiei
                // prelucreaza vectorul local pentru a aplica directiei sensului camerei
                camera.MoveCamera(cameraPosition);
                // calculeaza discrepanta dintre miscarea mouse-ului in functie de timpul parcurs in frame
                camera.AddRotation((rotX - mouse.X) * deltaTime * mouseSensitivity, -(mouse.Y - rotY) * deltaTime * mouseSensitivity);
            }
            // L3
            // Updateaza rotatiile precedente cu a mouse-ului
            rotX = mouse.X;
            rotY = mouse.Y;

            lastFrameKeyboard = keyboard;
        }

        // L2
        // Functie in care se deseneaza un triunghi 
        // la pozitiile specificate X si Y,
        // Z determina scalarea triunghiului
        public void DrawTriangle(float x, float y, float z)
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(color1);
            GL.Vertex3(x - z, y + z, 0);
            GL.Color3(color2);
            GL.Vertex3(x, y - z, 0);
            GL.Color3(color3);
            GL.Vertex3(x + z, y + z, 0);
            GL.End();
        }

        // L3
        // Afiseaza axele de coordonate 
        public void DrawAxes()
        {
            GL.LineWidth(5);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(2, 0, 0);
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 2, 0);
            GL.Color3(Color.Blue);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 2);
            GL.End();
            GL.LineWidth(0.1f);
        }
        
        // L3
        // Randeaza un grid cu o anumita
        // culoare si un nr de linii
        public void DrawGrid(Color gridColor, int halfSize)
        {
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(gridColor);
            for (int i = -halfSize; i <= halfSize; i++)
            {
                GL.Vertex3(i, 0, -halfSize);
                GL.Vertex3(i, 0, halfSize);

                GL.Vertex3(-halfSize, 0, -i);
                GL.Vertex3(halfSize, 0, -i);
            }
            GL.End();
        }
        #endregion
        #region Metode de baza
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color.Black);

            // L2 preia valoarea curenta a rotitei mouse-ului
            zPoz = Mouse.GetState().WheelPrecise;
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnLoad(e);
            /* L2
             * Vizualizare 2d (X,Y)
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Ortho(-2.0, 2.0, -2.0, 2.0, 0.0, 4.0);
            */

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;
            // L3-4 Seteaza perspectiva si camera
            camera.ChangePerspectiveFieldOfView((float)aspect_ratio);
            camera.UpdateCamera();
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            GetInput((float)e.Time);

        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            // L3
            // Modifica pozitia camerei
            // Randeaza un grid
            // Randeaza axele de coordonate
            camera.UpdateCamera();
            DrawGrid(Color.White,16);
            DrawAxes();
            // L2
            // Randeaza triunghiul doar daca 
            // renderTriangle = true
            if(renderTriangle)
            {
                DrawTriangle(poz.X, poz.Y, poz.Z);
            }

            this.SwapBuffers();
        }
        #endregion

        [STAThread]
        static void Main(string[] args)
        {
            using (Program example = new Program())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}
