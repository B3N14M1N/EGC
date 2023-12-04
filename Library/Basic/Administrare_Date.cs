using CIOBAN.Librarie.Randomizer;
using CIOBAN.Library.Models;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * CIOBAN BENIAMIN
 * 3134A
 */
namespace CIOBAN.Librarie
{
    public class Administrare_Date
    {
        private const char DELIMITATOR = ',';
        private const char DELIMITATOR_OBJ = ' ';

        private string FileName;
        public Administrare_Date(string FileName)
        {
            this.FileName = FileName;
            /* se incearca deschiderea fisierului in modul OpenOrCreate
            astfel incat sa fie creat daca nu exista */
            Stream streamFisierText = File.Open(FileName, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }
        public Vector3 GetCoords()
        {
            // Se va apela implicit streamReader.Close()
            // la iesirea din blocul instructiunii "using"
            using (StreamReader streamReader = new StreamReader(FileName))
            {
                string linieFisier;
                // Citeste linia si creaza un vector3
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    if (linieFisier.Split(DELIMITATOR).Count()==3)
                    {
                        var date = linieFisier.Split(DELIMITATOR);
                        return new Vector3((float)Convert.ToDouble(date[0].Trim()),
                            (float)Convert.ToDouble(date[1].Trim()),
                            (float)Convert.ToDouble(date[2].Trim()));
                    }
                }
            }
            return Vector3.Zero;
        }
        public MeshData ReadOBJ()
        {
            List<Vector3> vertex = new List<Vector3>();
            List<Vector3> faces = new List<Vector3>();

            Stream streamOBJFile = File.Open(FileName, FileMode.OpenOrCreate);
            streamOBJFile.Close();

            using (StreamReader streamReader = new StreamReader(FileName))
            {
                string fileLine;
                // Citeste linia si creaza un vector3
                while ((fileLine = streamReader.ReadLine()) != null)
                {
                    if (fileLine.Split(DELIMITATOR_OBJ).Count() == 4)
                    {
                        var date = fileLine.Split(DELIMITATOR_OBJ);
                        if (date[0] == "v")
                            vertex.Add(new Vector3((float)Convert.ToDouble(date[1].Trim()),
                                    (float)Convert.ToDouble(date[2].Trim()),
                                    (float)Convert.ToDouble(date[3].Trim())));
                        else if (date[0] == "f")
                            faces.Add(new Vector3(Convert.ToInt32(date[1].Trim()),
                                    Convert.ToInt32(date[2].Trim()),
                                    Convert.ToInt32(date[3].Trim())));
                    }
                }
            }
            List<Color> colors = new List<Color>();
            for(int i=0;i<faces.Count; i++)
            {
                //colors.Add(RandomGenerator.GetRandomColor());
                int val = 255 * i / faces.Count;
                colors.Add(Color.FromArgb(val, val, val));
            }
            MeshData meshData = new MeshData(vertex,faces);
            meshData.SetColors(colors);
            return meshData;
        }
    }
}
