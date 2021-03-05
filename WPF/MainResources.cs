using System;
using System.Collections.Generic;

namespace WPF
{
    public static class MainResources
    {
        public static MainWindow MainWindow;
        public static List<bool> Scores;
        private static int _LifePoints = 0;

        private static readonly Random Rand = new Random(Guid.NewGuid().GetHashCode());
        
        // Look at my amazing Fisher-Yates implementation
        public static void Shuffle<T>(this IList<T> list)
        {
            for (int i = list.Count - 1; i > 1; i--)
            {
                int j = Rand.Next(i + 1);
                T obj = list[i];
                list[i] = list[j];
                list[j] = obj;
            }
        }

        public static void ReduceLifePoints(int amount)
        {
            _LifePoints -= amount;
        }

        public static void SetLifePoints(int amount)
        {
            _LifePoints = amount;
        }

        public static int GetLifePoints()
        {
            return _LifePoints;
        }
    }
}