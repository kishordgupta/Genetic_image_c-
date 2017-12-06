

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace project
{
    class MainGA
    {
        String[] population = new String[Config.popSIZE];
        Double[] fitness= new double[Config.popSIZE];
        Double[] probability = new double[Config.popSIZE];
        int[] count = new int[Config.popSIZE];
        int size = 0;
        Double totalfitness = 0;
       public Double Avgtotalfitness = 0;
       public  Double best = 0;
        public Double test = 0;
        int crossoverrate = 80;
        public static int na=0;
      public static double[,] testValues = new double[100, 2];
        //  int generation = 2000;
        String[] bufferpopulation = new String[Config.popSIZE];
        String[] tournamentchamp = new String[Config.popSIZE];
        public  int mutationrate =15;
        public static bool start = true;
        public static bool GAoff = false;
        internal decimal genartationtime = 10000;
        ThreadStart childref;
        Thread childThread;
        public void GA()
        {
            GAoff = false;
            // threadcalling();
            try { childThread.Abort(); } catch (Exception e) { }
            childref = new ThreadStart(threadcalling);
           Console.WriteLine("In Main: Creating the Child thread");
           childThread = new Thread(childref);
            childThread.Start();
        }
        public  void threadcalling()
        {
            Console.Write("called");
            size = Config.SIZE * Config.SIZE;
            if(start)
            initpouplationGA();
            na = 0;
            start = false;
            testValues = new double[100, 2];
            for (int i = 0; i < 100; i++)
            {
                testValues[i, 0] = i; // X values
                testValues[i, 1] = 0;
            }
            for (int i = (int)genartationtime-1, k=0 ; i >= 0; i--,k++)
            {
                if(i%25==0)
              Console.WriteLine(i);
                na++;
                if (k >= 100)
                    k = 0;
                testValues[k, 0] = k;
                testValues[k, 1] = Avgtotalfitness;
                calculatefitness();
                selectiontournamet();//selection();
                Crossover();
                mutation();
                population = (String[])bufferpopulation.Clone();
                if (GAoff == true)
                    break;

            }
            genartationtime = 0;
            setprimaryset();
           try { Thread.CurrentThread.Abort(); } catch (ThreadAbortException e) { }
           
        }

        private void setprimaryset()
        {
            for (int i = 0; i < population.Length; i++)
            {
                Gaform.population[i].getchromosometopic(bufferpopulation[i]);
            }
        }

        private void Crossover()
        {
            int n = population.Length/2;
            for (int i = 0; i < n - 1; i++)
            {
                //int nex = Config.random.Next(1, Config.SIZE);
                // if (nex < 80)
                {

                   doCrossover(i,n+i);
                   // doCrossover(i, n + i);
                }
            }
        }

        private void doCrossover(int i,int n)
        {
            String s1 = bufferpopulation[i];
            String s2 = bufferpopulation[i+1];

            int point1 = Config.random.Next(1, size / 3);
            int point2 = Config.random.Next(size / 3, (size * 2 / 3));
            int point3 = Config.random.Next( (size * 2 / 3), size-1);

            String s1part1 = s1.Substring(0, point1);
            String s1part2 = s1.Substring(point1,point2- point1);
            String s1part3 = s1.Substring(point2, point3- point2);
            String s1part4 = s1.Substring(point3);

            String s2part1 = s2.Substring(0, point1);
            String s2part2 = s2.Substring(point1,  point2- point1);
            String s2part3 = s2.Substring(point2, point3- point2);
            String s2part4 = s2.Substring(point3);//, point3);
            // combine the parts
            bufferpopulation[n] = s1part1 + s2part2 + s1part3 + s2part4;
            bufferpopulation[n+1] = s2part1 + s1part2 + s2part3 + s1part4;

            
        }
        private void doCrossoverchamp(int i, int n)
        {
            String s1 = tournamentchamp[i];
            String s2 = tournamentchamp[i + 1];

            int point1 = Config.random.Next(1, size / 3);
            int point2 = Config.random.Next(size / 3, (size * 2 / 3));
            int point3 = Config.random.Next((size * 2 / 3), size - 1);

            String s1part1 = s1.Substring(0, point1);
            String s1part2 = s1.Substring(point1, point2 - point1);
            String s1part3 = s1.Substring(point2, point3 - point2);
            String s1part4 = s1.Substring(point3);

            String s2part1 = s2.Substring(0, point1);
            String s2part2 = s2.Substring(point1, point2 - point1);
            String s2part3 = s2.Substring(point2, point3 - point2);
            String s2part4 = s2.Substring(point3);//, point3);
            // combine the parts
            bufferpopulation[i] = s1part1 + s2part2 + s1part3 + s2part4;
            bufferpopulation[i + 1] = s2part1 + s1part2 + s2part3 + s1part4;


        }
        void mutation()
        {

            int n = population.Length ;
            for (int i = 0; i < n - 1; i++)
            {
                int nex = Config.random.Next(1, 100);

                 if (nex < mutationrate)
                {

                    domutationindiv(i);
                }
            }

        }

        private void domutationindiv(int k)
        {
            int nex = Config.random.Next(3, Config.popSIZE/2);
            char[] a = bufferpopulation[k].ToCharArray();
            for (int i = 0; i < nex; i++)
            {
                int next = Config.random.Next(1, population[k].Length-3);
                int nextz = Config.random.Next(next+1, population[k].Length - 1);
                if (a[next] == '1')
                {
                    a[next] = '0';

                }
                else
                    a[next] = '1';

                if (a[nextz] == '1')
                    a[nextz] = '0';
                else
                    a[nextz] = '1';

            }
            bufferpopulation[k] =new string(a);
        }

       

        private void selectiontournamet()
        {
            int n = population.Length/2;
            int tournamentsize = 4;
            int k = 0;
            int tournamentcount = 0;
            for(int i=0;i<n; i++)
            {
              
                int first = Config.random.Next(0, population.Length-1);
                double max = fitness[first];
                int champion1 = first;
                int second = Config.random.Next(0, population.Length - 1);
                if (max < fitness[second])
                {
                    max = fitness[second];
                    champion1 = second;
                }
                int third = Config.random.Next(0, population.Length - 1);
                if (max < fitness[third])
                {
                    max = fitness[third];
                    champion1 = third;
                }
                int fourth = Config.random.Next(0, population.Length - 1);
                if (max < fitness[fourth])
                {
                    max = fitness[fourth];
                    champion1 = fourth;
                }

                bufferpopulation[i] = population[champion1];
                tournamentchamp[i] = population[champion1];
            }
            


        }
        private void selection()
        {
            int n = population.Length;
              for (int i = 0; i < n -1; i++)
               {
                   for (int j = 0; j < n - i-1 ; j++)
                   {
                       if (fitness[j] > fitness[j + 1])
                       {
                           // swap temp and arr[i]
                           Double temp = fitness[j];
                           fitness[j] = fitness[j + 1];
                           fitness[j + 1] = temp;

                           String tempa = population[j];
                           population[j] = population[j + 1];
                           population[j + 1] = tempa;
                       }
                   }
               }

            Double pastprobability = 0;
            int popsize = 0;
            for (int i = 0; i < n ; i++)
            {
                
                probability[i] =  (fitness[i] / totalfitness);
                count[i] = (int)(Math.Round(probability[i] * Config.popSIZE));
                if (popsize==n )
                { break; }
                for (int k = 0; k < count[i]; k++)
                {
                    bufferpopulation[popsize] = population[i];
                        popsize++;
                          if (popsize== n )
                    { break; }
                }
            }
          

        }

        private void calculatefitness()
        {
            totalfitness = 0;
            best = 0.0;
            for (int i = 0; i < population.Length; i++)
            {
            
                Gaform.population[i].getchromosometopic(population[i]);
                Gaform.population[i].calculatefitness();
                fitness[i] =Gaform.population[i].totalfitness;
             //  population[i] = Gaform.getindividual(i);///
                // Console.WriteLine(i + " " + fitness[i]);
                totalfitness = totalfitness + fitness[i];
                if (Gaform.population[i].totalfitness > best)
                    best = Gaform.population[i].totalfitness;


            }
            Avgtotalfitness = totalfitness / population.Length;
        }

        void initpouplationGA()
        {
         
          for (int i = 0; i < population.Length; i++)
            {
                population[i] = Gaform.getindividual(i);
            }
        }
    }
}
