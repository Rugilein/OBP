using System;
using System.IO;

namespace R_OOP
{
    class DataIO
    {
        public void loadData(Form1 F)
        {
            string[] Kategorijos = File.ReadAllLines("kategorijos.txt");

            for (int i = 0; i < Kategorijos.Length; i++)
            {
                string[] Kategorija = Kategorijos[i].Split('|');

                Category C = new Category();
                C.setID(Convert.ToInt32(Kategorija[0]));
                C.setName(Kategorija[1]);



                string[] Meals = File.ReadAllLines("patiekalai.txt");

                for (int a = 0; a < Meals.Length; a++)
                {
                    string[] Meal = Meals[a].Split('|');

                    if (Convert.ToInt32(Kategorija[0]) == Convert.ToInt32(Meal[1]))
                    {
                        Meal M = new Meal();

                        M.setMealID(Convert.ToInt32(Meal[0]));
                        M.setCategoryID(Convert.ToInt32(Meal[1]));
                        M.setName(Meal[2]);
                        M.setPrice(float.Parse(Meal[3]));
                        M.setDescription(Meal[4]);
                        M.setImagePath(Meal[5]);

                        C.lMeal.Add(M);
                    }
                }

                F.lCategory.Add(C);
            }

            F.updateListBox1();
        }

        public void outputData(Form1 F)
        {
             int orderNr = (Convert.ToInt32(File.ReadAllText("Kvitai\\last.txt")) + 1);

            string output = DateTime.Now.ToString();

            output += string.Format("\n\nUžsakymo numeris: {0}\n\n", orderNr);

            for (int i = 0; i < F.B.lMeal.Count; i++)
                output += string.Format("{0}. {1}  {2}€\n", i + 1, F.B.lMeal[i].getName(), F.B.lMeal[i].getPrice());

            output += "\n" + F.label3.Text;

            File.WriteAllText(string.Format("Kvitai\\{0}.txt", orderNr), output);
            File.WriteAllText("Kvitai\\last.txt", orderNr.ToString());
        }
    }
}
