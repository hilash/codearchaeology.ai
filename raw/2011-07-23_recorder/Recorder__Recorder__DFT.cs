using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recorder
{
    public static class DFT
    {
        public static int furier(byte[] XX, double[] REX, double[] IMX,double[] output, int n)// n = 512
        {
            const double PI = 3.14159265;
            double maxvalue = 0;
            int maxcnt = 0;

            for (int k = 0; k <= n / 2; k++)
            {
                REX[k] = IMX[k] = 0;
            }

            for (int k = 0; k <= n / 2; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    REX[k] = REX[k] + ((double)XX[i]) * Math.Cos(2 * PI * k * i / n);
                    IMX[k] = REX[k] - ((double)XX[i]) * Math.Sin(2 * PI * k * i / n);
                }
            }
            for (int k = 6; k <= n / 2; k++)
            {
                output[k] = Math.Sqrt(IMX[k] * IMX[k] + REX[k] * REX[k]);
                if (maxvalue <= output[k])
                {
                    maxcnt = k;
                    maxvalue = output[k];
                }
            }
            return maxcnt;
        }

        public static int FFT(double[] REX, double[] IMX, int n)// n = 512
        {
            const double PI = 3.14159265;
            int nm1 = n - 1;
            int nd2 = n / 2;
            int m = (int)(Math.Log(n) / Math.Log(2));
            int j = nd2;
            double tr, ti;

            for (int i = 1; i <= n - 2; i++)
            {
                if (i < j)
                {
                    tr = REX[j];
                    ti = IMX[j];
                    REX[j] = REX[i];
                    IMX[j] = IMX[i];
                    REX[i] = tr;
                    IMX[i] = ti;
                }
                int k = nd2;
                while (k <= j)
                {
                    j = j - k;
                    k = k / 2;
                }
                j = j + k;
            }

            for (int l = 1; l <= m; l++)
            {
                double le = (int)Math.Pow(2, l);
                int le2 = (int)(le / 2);
                double ur = 1;
                double ui = 0;
                double sr = Math.Cos(PI / le2);
                double si = -Math.Sin(PI / le2);
                for (j = 1; j <= le2; j++)
                {
                    int jm1 = j - 1;
                    for (int i = jm1; i <= nm1; i += (int)le)
                    {
                        int ip = i + le2;
                        tr = REX[ip] * ur - IMX[ip] * ui;
                        ti = REX[ip] * ui + IMX[ip] * ur;
                        REX[ip] = REX[i] - tr;
                        IMX[ip] = IMX[i] - ti;
                        REX[i] = REX[i] + tr;
                        IMX[i] = IMX[i] + ti;
                    }
                    tr = ur;
                    ur = tr * sr - ui * si;
                    ui = tr * si + ui * sr;
                }
            }

            double maxvalue = 0;
            int maxindex = 0;
            for (int k = 3; k <= n / 2; k++)
            {
                REX[k] = Math.Sqrt(IMX[k] * IMX[k] + REX[k] * REX[k]);
                if (maxvalue <= REX[k])
                {
                    maxindex = k;
                    maxvalue = REX[k];
                }
            }
            return maxindex;
        }
    }
}
