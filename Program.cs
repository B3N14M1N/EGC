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
 * 
 */
namespace CIOBAN
{
    class Program : GameWindow
    {
        //Laboratorul 2
        #region L2
        private KeyboardState lastFrameKeyboard;
        Key moveTriangleKey = Key.Q;
        private bool moveTriangle = true;
        Key renderTriangleKey = Key.E;
        private bool renderTriangle = true;

        Vector3 poz = new Vector3();
        private float zPoz = 0;
        private float mouseSensitivity = 0.1f;

        #endregion
        #region L3-4
        Vector3 cameraPosition = new Vector3(5,5,5);
        private float movementSpeed = .5f;
        Key cameraForwardKey = Key.W;
        Key cameraBackwardsKey = Key.S;
        Key cameraLeftKey = Key.A;
        Key cameraRightKey = Key.D;
        Key cameraUpKey = Key.LShift;
        Key cameraDownKey = Key.LControl;

        #endregion

        public Program() : base(800, 600)
        {
            KeyDown += WindowSettings;
        }
        // Functie in care prelucreaza inputul pentru modificarea ferestrei
        void WindowSettings(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit();

            if (e.Key == Key.F11)
                this.WindowState = (this.WindowState == WindowState.Fullscreen)? WindowState.Normal : WindowState.Fullscreen;
        }
        public Color GetRandomColor()
        {
            Random rnd = new Random();
            return Color.FromArgb(rnd.Next(0,256), rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
        }
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
            /*
             * Vizualizare 2d (X,Y)
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Ortho(-2.0, 2.0, -2.0, 2.0, 0.0, 4.0);
            */

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 0.1f, 1000);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            Matrix4 lookat = Matrix4.LookAt(cameraPosition, new Vector3(0, 0, 0), new Vector3(0, 1f, 0));
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            GetInput((float)e.Time);

        }

        // L2
        // Functie petnru preluarea si prelucrarea  
        // inputului mouse-ului si a tastaturii.
        public void GetInput(float deltaTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            if(moveTriangle)
            {
                float delta = 1 / 30f;
                poz.X = mouse.X * delta * mouseSensitivity;
                poz.Y = -mouse.Y * delta * mouseSensitivity;

                if(zPoz - mouse.WheelPrecise > 0)
                {
                    poz.Z -= delta;
                }
                else if(zPoz - mouse.WheelPrecise < 0)
                    poz.Z += delta;
                if (poz.Z == 0)
                    poz.Z = .1f;
                zPoz = mouse.WheelPrecise;
            }
            // Prelucreaza variabilele boolene pentru a
            // lucra doar la o singura apasare a tasteii
            moveTriangle = (keyboard.IsKeyDown(moveTriangleKey) && lastFrameKeyboard.IsKeyUp(moveTriangleKey)) ? !moveTriangle : moveTriangle;
            renderTriangle = (keyboard.IsKeyDown(renderTriangleKey) && lastFrameKeyboard.IsKeyUp(renderTriangleKey)) ? !renderTriangle : renderTriangle;

            cameraPosition.X += (keyboard.IsKeyDown(cameraLeftKey) ? (-1f) : keyboard.IsKeyDown(cameraRightKey) ? (1f) : 0) * deltaTime;
            cameraPosition.Y += (keyboard.IsKeyDown(cameraDownKey) ? (-1f) : keyboard.IsKeyDown(cameraUpKey) ? (1f) : 0) * deltaTime;
            cameraPosition.Z += (keyboard.IsKeyDown(cameraBackwardsKey) ? (-1f) : keyboard.IsKeyDown(cameraForwardKey) ? (1f) : 0) * deltaTime;

            lastFrameKeyboard = keyboard;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            DrawAxes();
            DrawGrid(Color.White,16);
            // L2
            // Randeaza triunghiul doar daca 
            // renderTriangle = true
            if(renderTriangle)
            {
                DrawTriangle(poz.X, poz.Y, poz.Z);
            }
            // L3
            // Modifica pozitia camerei
            Matrix4 lookat = Matrix4.LookAt(cameraPosition, new Vector3(0, 0, 0), new Vector3(0, 1f, 0));
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            this.SwapBuffers();
        }

        // L2
        // Functie in care se deseneaza un triunghi 
        // la pozitiile specificate.
        // Z determina scalarea triunghiului
        public void DrawTriangle(float x, float y, float z)
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.Red);
            GL.Vertex3(x - z, y + z, 0);
            GL.Color3(Color.Green);
            GL.Vertex3(x, y - z, 0);
            GL.Color3(Color.Blue);
            GL.Vertex3(x + z, y + z, 0);
            GL.End();
        }
        // L3
        // Afiseaza axele de coordonate 
        public void DrawAxes()
        {
            GL.LineWidth(1);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(1, 0, 0);
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 1, 0);
            GL.Color3(Color.Blue);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 1);
            GL.End();
            GL.LineWidth(0.1f);
        }
        public void DrawGrid(Color gridColor,int halfSize)
        {
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(gridColor);
            for (int i=-halfSize;i<=halfSize;i++)
            {
                GL.Vertex3(i,0,-halfSize);
                GL.Vertex3(i,0,halfSize);

                GL.Vertex3(-halfSize,0,-i);
                GL.Vertex3(halfSize, 0, -i);
            }
            GL.End();
        }
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
