using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analysis
{
    public class FurierTransformException : System.Exception
    {
        public FurierTransformException(string reason) : base(reason) { }
    }

    public static class FurierTransform
    {
        public enum TRANSFORM : int {DFT, FFT}

        private static bool IsPowerOfTwo(ulong x)
        {
            return (x != 0) && ((x & (x - 1)) == 0);
        }
        
        /// <summary>
        /// preform a frequncy analysis
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="inputBuffer">must be at least at size of numberOfSamples</param>
        /// <param name="outputBuffer">must be bigger than numberOfSamples/2</param>
        /// <param name="numberOfSamples"></param>
        /// <returns>the max pitch (frequency)</returns>
        public static int preformTranform(TRANSFORM transform, byte[] inputBuffer, double[] outputBuffer, int numberOfSamples)
        {
            double[] REX;
            double[] IMX;
            int maxPitch = 0;

            // buffer size must be multiple of 2
            if (false == IsPowerOfTwo((ulong)numberOfSamples))
            {
                string message = "Number of Samples for FurierTransform buffer must be a power of two. Number given: " + 
                    numberOfSamples.ToString() + "\n";
                throw new FieldAccessException(message);
            }
            else if (inputBuffer.Length < numberOfSamples)
            {
                string message = "The length of the input buffer (" + inputBuffer.Length.ToString() + 
                    ") must be at least the number of samples given " + numberOfSamples.ToString() + "\n";
                throw new FieldAccessException(message);
            }
            else if (outputBuffer.Length < (numberOfSamples / 2 + 1))
            {
                string message = "The length of the output buffer (" + inputBuffer.Length.ToString() +
                    ") must be bigger than half of the number of samples " + ((int)(numberOfSamples/2)).ToString() + "\n";
                throw new FieldAccessException(message);
            }

            REX = new double[numberOfSamples];
            IMX = new double[numberOfSamples];

            if (TRANSFORM.DFT == transform)
            {
                maxPitch = DFT(inputBuffer, REX, IMX, outputBuffer, numberOfSamples);
            }
            else if (TRANSFORM.FFT == transform)
            {
                Array.Copy(inputBuffer, REX, numberOfSamples);
                //Array.Clear(IMX, 0, numberOfSamples);
                maxPitch = FFT(REX, IMX, outputBuffer, numberOfSamples);
            }
            else
            {
                throw new FieldAccessException("Unknwon tranform.");
            }

            return maxPitch;
        }

        /// <summary>
        /// fourier tranform.
        /// </summary>
        /// <param name="XX">wave samples.</param>
        /// <param name="REX"></param>
        /// <param name="IMX"></param>
        /// <param name="output"></param>
        /// <param name="n">length of n</param>
        /// <returns></returns>
        public static int DFT(byte[] XX, double[] REX, double[] IMX, double[] output, int n)
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

        /// <summary>
        /// Fast-Fourier transform. Given wave samples at array REX[n], preform a frequency analysis
        /// and return the result in REX[n/2]
        /// </summary>
        /// <param name="REX"> input array at size n, contains the wavesamples. output array, at size (n/2)</param>
        /// <param name="IMX"> input array, must be initialized to zeros.</param>
        /// <param name="n"></param>
        /// <returns>frequency analysis of the given wave</returns>
        public static int FFT(double[] REX, double[] IMX, double[] output, int n)
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
                output[k] = Math.Sqrt(IMX[k] * IMX[k] + REX[k] * REX[k]);
                if (maxvalue <= output[k])
                {
                    maxindex = k;
                    maxvalue = output[k];
                }
            }
            return maxindex;
        }
    }
}
