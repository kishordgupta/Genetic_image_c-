using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    class Config
    {
        public static int TS = 1;
        public  static  int SIZE = 20;
        public static int popSIZE = 24;
        public static byte[,] baseIndivdual = new byte[Config.SIZE, Config.SIZE];
        public static int basediif = 0;
        public static int basecount = 0;
        public static int[] baseh = new int[SIZE];
        public static int[] basew = new int[SIZE];
        public static Random random = new Random();
        public static bool refimage =true;
      
    }
}
