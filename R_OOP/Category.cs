using System.Collections.Generic;

namespace OOP_5
{
    public class Category
    {
        private int ID;
        private string Name;

        public List<Meal> lMeal = new List<Meal>();
        private int Constant;

        #region SET

        //Inkapsuliacija

        public void setID(int x)
        {
            ID = x;
        }

        public void setName(string x)
        {
            Name = x;
        }

        #endregion

        #region GET

        public int getID()
        {
            return ID;
        }

        public string getName()
        {
            return Name;
        }

        #endregion
    }
}
