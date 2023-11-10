using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Platform;
using CIOBAN.Librarie;
using System.Configuration;
using OpenTK.Graphics;
namespace CIOBAN.Librarie.RandomThings
{
    public static class RandomGenerator
    {
        // L3
        // Functie pentru generarea unei culor aleatorie
        public static Color GetRandomColor()
        {
            Random rnd = new Random();
            return Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
        }
        public static Color GetRandomColor(int seed)
        {
            Random rnd = new Random(seed);
            return Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
        }
        /// <summary>
        /// Returneaza un Vector3 cu valori intregi x,y,z
        /// Valori care se afla intre punctul 1 si 2
        /// Adica niste valori random dintr-un intr-un cub cu
        /// colturile opuse p1 si p2
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static Vector3 GetRandomVector3(Vector3 point1, Vector3 point2) {
            Random rnd = new Random();
            return new Vector3(rnd.Next((int)point1.X,(int)point2.X), rnd.Next((int)point1.Y, (int)point2.Y), rnd.Next((int)point1.Z, (int)point2.Z));
        }

    }
}
