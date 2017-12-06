using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    public class Indivdual
    {
        private byte[,] picb = new byte[Config.SIZE, Config.SIZE];
        int totalcount;
        int[] h = new int[Config.basecount];
        int[] w = new int[Config.basecount];
        public System.Drawing.Bitmap pic = new System.Drawing.Bitmap(Config.SIZE, Config.SIZE, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        public int ts = 1;
        public string rcs = "";
        public Double fitnessvalueh = 0;
        public Double fitnessvaluew = 0;
        public Double fitnessvalueb = 0;
        public Double totalfitness = 0;

        public void generategenes()
        {
            double blackpercentage = (Config.basecount *100)/ (Config.SIZE*Config.SIZE);
   //   picb = (byte[,])Config.baseIndivdual.Clone();
            Boolean add = true;
            float perce = Config.basecount / (Config.SIZE * Config.SIZE);
            if (Config.basediif > 0)
                add = true;
            else add = false;
            int reminder = 5;
            if (add)
            {
                reminder = 3;
            }
            else
                reminder = 2;
            //  int x = Config.random.Next(1, 100);
            //   int y = Config.random.Next(1, 100);
            int count = 0; ;
        //  Console.WriteLine(blackpercentage + " " + Config.basecount);
            /*/    for (int i = 0; i < Math.Abs(Config.basecount)*4; i++)
                {

                    //    for (int j = 0; j < Config.SIZE; j++)
                    {

                        int x = Config.random.Next(1, Config.SIZE);
                        int y = Config.random.Next(1, Config.SIZE);
                        if (picb[x, y] == 0)
                        {
                            picb[x, y] = 1;
                              count++;
                             if(count > Config.basecount) break;
                        }

                    }

                }
    */
            int nu = 0;
            for (int i = 0; i < Config.SIZE; i++)
            {

                 for (int j = 0; j < Config.SIZE; j++)
                {

                    Double x = Config.random.Next(1, 100);
                    if (x < blackpercentage && nu < Config.basecount)
                    { picb[i, j] = 1; nu++; }
                    else
                        picb[i, j] = 0;
                }

            }
            //Console.WriteLine(Math.Abs(Config.basediif));
            calculatefitness();

        }

        public void generateimage()
        {

           // System.Drawing.Bitmap pic = new System.Drawing.Bitmap(Config.SIZE, Config.SIZE, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            for (int x = 0; x < Config.SIZE; x++)
            {
                for (int y = 0; y < Config.SIZE; y++)
                {
                    if (picb[x, y] == 0)
                        pic.SetPixel(x, y, System.Drawing.Color.White);
                    else
                        pic.SetPixel(x, y, System.Drawing.Color.Black);
                }
            }
           
           

            ;
        }
        public void calculatefitness()
        {
            ts=Config.TS;
            calculatehw();
            int sizes = Config.SIZE * Config.SIZE;
            fitnessvalueb =Math.Abs(Config.basecount - totalcount) ;
            int[] difh = new int[Config.SIZE];
            int[] difw = new int[Config.SIZE];
            int totalh = 0;
            int totalw = 0;
            int hc = 0;
            int wc = 0;
            int tc = 0;
            for (int i = 0; i < Config.SIZE; i++)
            {
                difh[i]= Math.Abs(Config.baseh[i] - h[i]);

                difw[i] = Math.Abs(Config.basew[i] - w[i]);

                totalh = totalh - difh[i];
                if (difh[i] < 3*ts)
                    hc = hc + 5 ;
                else
                {
                    hc = hc - 2;
                    if (difw[i] > 10 * ts)
                        totalh = totalh - 10 ;
                    else
                    {
                       
                        totalh = totalh - (difh[i] - 3 );
                    }
                }
                totalw = totalw - difw[i];
                if (difw[i] < 3 * ts)
                    wc = wc + 5 ;
                else
                {
                    wc = wc - 2 ;
                    if (difw[i] > 10 * ts)
                        totalw = totalw -  10 ;
                   else
                    totalw = totalw - (difw[i] - 3 * ts);
                }
                if (difh[i] <= 3 * ts && difw[i] <= 3 * ts)
                    tc = tc + 25 ;
               else if (difh[i] <= 2 * ts && difw[i]<=2 * ts)
                    tc = tc + 50 ;
                else if (difh[i]*ts + difw[i] <= 2 * ts)
                    tc = tc + 100 ;
                // Console.WriteLine(w[i] + " " + difw[i]);

            }
           
            fitnessvalueh = totalh * ts;///(Config.SIZE);
            fitnessvaluew = totalw * ts;///(Config.SIZE);
            hc = hc*3 * ts;// Config.SIZE;
            wc = wc*3 * ts;// Config.SIZE;
            tc = tc *6 * ts;
            rcs = tc + ", " + wc + ", " + hc;
            if(Config.refimage)
             totalfitness = hc + wc + tc +(matchcount*5* ts) + fitnessvalueh+ fitnessvaluew;
            else
                totalfitness = hc + wc + tc  + fitnessvalueh + fitnessvaluew;
            //   Console.WriteLine(totalfitness + " " + fitnessvalueh + " " + fitnessvaluew + " " + fitnessvalueb + " " + hc + " " + wc + " " +tc);
        }

        int matchcount = 0;
        void calculatehw()
        {
            matchcount = 0;
            totalcount = 0;
            for (int i = 0; i < Config.SIZE; i++)
            {
                int k = 0;
                int t = 0;
                for (int j = 0; j < Config.SIZE; j++)
                {
                    if (picb[i, j] == Config.baseIndivdual[i, j] && picb[i, j] == 1)
                    {
                        matchcount++;
                    }
                    else
                    {
                    }
                    if (picb[i, j] == 0)
                    {

                    }
                    else
                    {
                        totalcount++;
                        k++;
                    }
                    if (picb[j, i] == 0)
                    {

                    }
                    else
                    {
                        t++;
                    }
                    h[i] = k;
                    w[i] = t;
                }

            }
        }

        public string getchromosome()
        {

            string s =null;
            for (int x = 0; x < Config.SIZE; x++)
            {
                for (int y = 0; y < Config.SIZE; y++)
                {
                    s = s + picb[x, y];
                }
            }

            return s;
        }
        public void getchromosometopic(String s)
        {

            Char[] ca = s.ToCharArray();
            int i = 0;
            for (int x = 0; x < Config.SIZE; x++)
            {
                for (int y = 0; y < Config.SIZE; y++)
                {
                   picb[x, y]=Byte.Parse(ca[i].ToString());
                    i++;
                }
            }

           
        }

    }
}
