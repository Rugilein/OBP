using System;
using System.Collections.Generic;
using System.IO;

namespace R_OOP
{
    class DataIO
    {
        public void outputData(Form1 F)
        {
            int orderNr = (Convert.ToInt32(File.ReadAllText("Kvitai\\last.txt")) + 1);

            string output = DateTime.Now.ToString();

            output += string.Format("\n\nUžsakymo numeris: {0}\n\n", orderNr);

            for (int i = 0; i < F.B.lMeal.Count; i++)
                output += string.Format("{0}. {1}  {2}€  | x{3}\n", i + 1, F.B.lMeal[i].getName(), F.B.lMeal[i].getPrice(), F.B.lMeal[i].getCount());

            output += F.label3.Text;

            string sum = F.label3.Text.Replace("Užsakymo suma: ", "");
            sum = sum.Replace("€", "");

            output += "\nPVM: " + (float.Parse(sum) * 0.21) + "€";

            File.WriteAllText(string.Format("Kvitai\\{0}.txt", orderNr), output);
            File.WriteAllText("Kvitai\\last.txt", orderNr.ToString());
        }

        public List<Category> loadCategory()
        {
            List<Category> C = new SQL().loadCategory();

            return C;
        }
    }
}
