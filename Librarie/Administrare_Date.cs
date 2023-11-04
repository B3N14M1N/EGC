using OpenTK;
using System;
using System.Collections.Generic;
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

        private string numeFisier;
        public Administrare_Date(string numeFisier)
        {
            this.numeFisier = numeFisier;
            /* se incearca deschiderea fisierului in modul OpenOrCreate
            astfel incat sa fie creat daca nu exista */
            Stream streamFisierText = File.Open(numeFisier, FileMode.OpenOrCreate);
            streamFisierText.Close();
        }
        public Vector3 GetCoords()
        {
            // Se va apela implicit streamReader.Close()
            // la iesirea din blocul instructiunii "using"
            using (StreamReader streamReader = new StreamReader(numeFisier))
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
    }
}
