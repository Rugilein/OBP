using System.Collections.Generic;

namespace OOP_5
{
    public class Basket
    {
        public List<Meal> lMeal = new List<Meal>();

        public double getSumPVM()
        {
            double sum = 0;

            for (int i = 0; i < lMeal.Count; i++)
                sum += lMeal[i].getPrice() * lMeal[i].getCount();

            return sum * 0.21;
        }

        public double getSum()
        {
            double sum = 0;

            for (int i = 0; i < lMeal.Count; i++)
                sum += lMeal[i].getPrice() * lMeal[i].getCount();

            return sum;
        }
    }
}
