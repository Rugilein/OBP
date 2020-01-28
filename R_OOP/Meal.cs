namespace OOP_5
{
    public class Meal
    {
        private int categoryID, mealID, Count = 1;
        private double Price;
        private string Name, Description, imagePath;

        #region SET

        public void setCategoryID(int x)
        {
            categoryID = x;
        }

        public void setMealID(int x)
        {
            mealID = x;
        }

        public void setPrice(float x)
        {
            Price = x;
        }

        public void setName(string x)
        {
            Name = x;
        }

        public void setDescription(string x)
        {
            Description = x;
        }

        public void setImagePath(string x)
        {
            imagePath = x;
        }

        public void setCount(int x)
        {
            Count = x;
        }

        #endregion

        #region GET

        public int getCategoryID()
        {
            return categoryID;
        }

        public int getMealID()
        {
            return mealID;
        }

        public double getPrice()
        {
            return Price;
        }

        public string getName()
        {
            return Name;
        }

        public string getDescription()
        {
            return Description;
        }

        public string getImagePath()
        {
            return imagePath;
        }

        public int getCount()
        {
            return Count;
        }

        #endregion
    }
}
