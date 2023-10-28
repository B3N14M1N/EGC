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
namespace CIOBAN.Librarie
{
    public class Camera
    {
        #region Parametrii
        // L3 
        // Vector pentru pozitie 
        public Vector3 position { get; set; }
        // Vector pentru directie ()
        private Vector3 direction;
        // Forward indica spre fata camerei
        public Vector3 Forward { get { return new Vector3((float) Math.Sin(direction.X), 0, (float)Math.Cos(direction.X)); } }
        // Right indica spre dreapta fata de rotatia camerei
        public Vector3 Right { get { return new Vector3(-Forward.Z, 0, Forward.X); } }

        // Parametrii pentru Cliping
        private float _zNear;
        private float _zFar;
        public float zNear {
            get { return _zNear; }
            set { _zNear = (value <= 0) ? 0.1f : (value>=_zFar)? 0.1f: value; }
        }
        public float zFar {
            get { return _zFar; }
            set { _zFar = (value <= _zNear) ? _zNear + 1f :value; }
        }
        #endregion

        #region Constructori
        // Constructor
        public Camera()
        {
            _zNear = 0.1f;
            _zFar = 1000f;
            position = Vector3.Zero;
            direction = new Vector3((float)Math.PI, 0f, 0f);
        }
        // Constructor cu parametru pozitia camerei
        public Camera(Vector3 Position)
        {
            _zNear = 0.1f;
            _zFar = 1000f;
            position = Position;
            direction = new Vector3((float)Math.PI, 0f, 0f);
        }
        #endregion

        #region Metode
        // Returneaza matrice pentru view cu target-ul in fata camerei
        public Matrix4 GetMatrix()
        {
            Vector3 lookat = new Vector3((float)(Math.Sin(direction.X) * Math.Cos(direction.Y)),
                                            (float)Math.Sin(direction.Y),
                                            (float)(Math.Cos(direction.X) * Math.Cos(direction.Y)));
            return Matrix4.LookAt(position, position + lookat, Vector3.UnitY);
        }
        // Functie care primeste parametru o directie locala
        // o prelucreaza pentru rotatia curenta si adauga
        // la pozitia curenta
        // !!!!! Viteza de miscare depinde de parametrul primit (Momentan)!!!!!
        public void MoveCamera(Vector3 Direction)
        {
            Vector3 offset = Direction.X * Right;

            offset += Direction.Y * Forward;
            offset.Y += Direction.Z;

            // Daca inmultesc vectorul normalizat
            // cu o variabila pot crea sensivitatea
            // miscarii dorite
            //offset.NormalizeFast();

            position += offset;
        }
        // Functie pentru adaugarea rotatiei
        // Margineste rotatia de sus-jos a camerei
        public void AddRotation(float x, float y)
        {
            direction.X = (direction.X + x) % ((float)Math.PI * 2.0f);
            direction.Y = Math.Max(Math.Min(direction.Y + y, (float)Math.PI / 2.0f - 0.1f), (float)-Math.PI / 2.0f + 0.1f);
        }

        // Metoda pentru updatarea camerei
        public void UpdateCamera()
        {
            Matrix4 lookat = GetMatrix();
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);
        }
        // Metoda pentru updatarea perspectivei
        public void ChangePerspectiveFieldOfView(float aspect_ratio)
        {
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, _zNear, _zFar);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }
        #endregion
    }
}
