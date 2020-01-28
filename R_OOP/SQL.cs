using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace OOP_5
{
    class SQL
    {
        private static SQLiteConnection SQLITE_CONNECTION;

        public SQL()
        {
            SQLITE_CONNECTION = CreateConnection();
        }

        static SQLiteConnection CreateConnection()
        {
            SQLITE_CONNECTION = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; Compress = True; ");

            try
            {
                SQLITE_CONNECTION.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR CreateConnection(): {0}", ex.ToString());
            }

            return SQLITE_CONNECTION;
        }

        public void createTableWithContents()
        {
            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                //Insert Category

                sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTS fCategory (Name VARCHAR(20), UNIQUE(Name))";
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = "INSERT OR IGNORE INTO fCategory (Name) VALUES('Mėsainis')";
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = "INSERT OR IGNORE INTO fCategory (Name) VALUES('Pica')";
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = "INSERT OR IGNORE INTO fCategory (Name) VALUES('Kotletas')";
                sqlite_cmd.ExecuteNonQuery();


                //Insert Food

                sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTS Food (Name VARCHAR(20), Price INT, Description VARCHAR(200), Image VARCHAR(100), Category INT, UNIQUE(Name))";
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = "INSERT OR IGNORE INTO Food (Name, Price, Description, Image, Category) VALUES('Vegetariškas mėsainis', '6.00', 'Mėsainis su sūriu', 'Surainis.jpg', '1')";
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = "INSERT OR IGNORE INTO Food (Name, Price, Description, Image, Category) VALUES('Mėsainis su jautiena', '5.00', 'Mėsainis su jautiena', 'Jautienainis.jpg', '1')";
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = "INSERT OR IGNORE INTO Food (Name, Price, Description, Image, Category) VALUES('Pepperoni pica', '6.00', 'Pica su pepperoni dešra', 'Pepperoni_pica.jpg', '2')";
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = "INSERT OR IGNORE INTO Food (Name, Price, Description, Image, Category) VALUES('Pica su ananasais', '7.00', 'Pica su kumpiu i ananasais', 'Pica_su_ananasais.jpg', '2')";
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = "INSERT OR IGNORE INTO Food (Name, Price, Description, Image, Category) VALUES('Kijevo kotletai', '7.00', 'Kiejvo kotletai (vištiena)', 'Kijevo_kotletai.jpg', '3')";
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = "INSERT OR IGNORE INTO Food (Name, Price, Description, Image, Category) VALUES('Jautienos kotletai', '8.00', 'Jautienos kotletai', 'Jautienos_kotletai.jpg', '3')";
                sqlite_cmd.ExecuteNonQuery();




                //Orders
                sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTS cOrders (Date VARCHAR(19), oText VARCHAR(256), PVM INT, noPVM INT)";
                sqlite_cmd.ExecuteNonQuery();


                //Users
                sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTS User (Username VARCHAR(256), Password VARCHAR(256))";
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = "INSERT OR IGNORE INTO User (Username, Password) VALUES('admin', 'admin')";
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public List<Category> loadCategory()
        {
            List<Category> result = new List<Category>();

            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                sqlite_cmd.CommandText = "SELECT rowid, Name FROM fCategory";

                using (SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader())
                {
                    while (sqlite_datareader.Read())
                    {
                        Category C = new Category();
                        C.setID(sqlite_datareader.GetInt32(0));
                        C.setName(sqlite_datareader.GetString(1));

                        result.Add(C);
                    }
                }
            }

            for (int i = 0; i < result.Count; i++)
            {
                using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
                {
                    sqlite_cmd.CommandText = "SELECT *, rowid FROM Food";

                    using (SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader())
                    {
                        while (sqlite_datareader.Read())
                        {
                            if (sqlite_datareader.GetInt32(4) == result[i].getID())
                            {
                                Meal M = new Meal();

                                M.setName(sqlite_datareader.GetString(0));
                                M.setPrice(sqlite_datareader.GetFloat(1));
                                M.setDescription(sqlite_datareader.GetString(2));
                                M.setImagePath(sqlite_datareader.GetString(3));
                                M.setCategoryID(sqlite_datareader.GetInt32(4));
                                M.setMealID(sqlite_datareader.GetInt32(5));

                                result[i].lMeal.Add(M);
                            }
                        }
                    }
                }
            }

            return result;
        }

        public void addNewCategory(string Name)
        {
            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                sqlite_cmd.CommandText = string.Format("INSERT OR IGNORE INTO fCategory (Name) VALUES('{0}')", Name);
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public void addNewFood(int Category, string Name, double Price, string pImage, string Description)
        {
            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                sqlite_cmd.CommandText = string.Format("INSERT OR IGNORE INTO Food (Name, Price, Description, Image, Category) VALUES('{0}', '{1}', '{2}', '{3}', '{4}')", Name, Price, Description, pImage, Category);
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public void updateFood(double Price, string pImage, string Description, int rowid)
        {
            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                sqlite_cmd.CommandText = string.Format("UPDATE Food SET Price = '{0}', Description = '{1}', Image = '{2}' WHERE rowid='{3}'", Price, Description, pImage, rowid);
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public void deleteFood(int rowid)
        {
            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                sqlite_cmd.CommandText = string.Format("DELETE FROM Food WHERE rowid='{0}'", rowid);
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public void addNewOrder(Basket B)
        {
            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                string oText = "";

                for (int i = 0; i < B.lMeal.Count; i++)
                {
                    var cMeal = B.lMeal[i];
                    oText += string.Format("{0}. {1}  {2}€  | x{3}\n", i + 1, cMeal.getName(), cMeal.getPrice(), cMeal.getCount());
                }

                string Date = DateTime.Now.ToString();

                sqlite_cmd.CommandText = string.Format("INSERT OR IGNORE INTO cOrders (Date, oText, PVM, noPVM) VALUES('{0}', '{1}', '{2}', '{3}')", Date, oText, B.getSumPVM(), B.getSum());
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public void addNewUser(string Username, string Password)
        {
            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                sqlite_cmd.CommandText = string.Format("INSERT OR IGNORE INTO User (Username, Password) VALUES('{0}', '{1}')", Username, Password);
                sqlite_cmd.ExecuteNonQuery();
            }
        }

        public bool bUserExists(string Username, string Password)
        {
            bool result = false;

            using (SQLiteCommand sqlite_cmd = SQLITE_CONNECTION.CreateCommand())
            {
                sqlite_cmd.CommandText = string.Format("SELECT * FROM User WHERE Username='{0}' AND Password='{1}'", Username, Password);

                using (SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader())
                {
                    while (sqlite_datareader.Read())
                        if (sqlite_datareader.GetString(0) == Username && sqlite_datareader.GetString(1) == Password)
                            result = true;
                        else
                            result = false;
                }
            }
            return result;
        }

        public void Save(Basket B)
        {
            addNewOrder(B);
        }
    }
}
