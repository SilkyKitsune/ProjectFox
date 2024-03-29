﻿#if DEBUG
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math
{
    //sine/cosine methods are not ready for deployment

    public static partial class Math
    {
        /// <summary> the closest approximation to π/2 (1.57079632679) </summary>
        public const float HalfPi = 1.57079637050628662109375f;

        /// <summary> the closest approximation to π (3.14159265359) </summary>
        public const float Pi = 3.1415927410125732421875f;

        /// <summary> the closest approximation to 3/4τ (4.71238898038) </summary>
        public const float ThreeQuartersTau = 4.7123889923095703125f;

        /// <summary> the closest approximation to τ (6.28318530718) </summary>
        public const float Tau = 6.283185482025146484375f;

        private static readonly float[] sine = new float[101]
        {
            0f,
            0.0157073173118f, 0.0314107590781f, 0.0471064507096f, 0.0627905195293f, 0.0784590957278f, 0.0941083133185f, 0.109734311091f, 0.125333233564f, 0.140901231938f, 0.15643446504f,
            0.171929100279f,  0.187381314586f,  0.202787295357f,  0.218143241397f,  0.233445363856f,  0.248689887165f,  0.263873049965f, 0.278991106039f, 0.294040325232f, 0.309016994375f,
            0.323917418198f,  0.338737920245f,  0.353474843779f,  0.368124552685f,  0.382683432365f,  0.397147890635f,  0.411514358605f, 0.425779291565f, 0.439939169856f, 0.45399049974f,
            0.467929814261f,  0.481753674102f,  0.495458668432f,  0.50904141575f,   0.522498564716f,  0.535826794979f,  0.549022817998f, 0.562083377852f, 0.575005252043f, 0.587785252292f,
            0.600420225326f,  0.612907053653f,  0.625242656336f,  0.637423989749f,  0.64944804833f,   0.661311865324f,  0.67301251351f,  0.684547105929f, 0.695912796592f, 0.707106781187f,
            0.718126297763f,  0.728968627421f,  0.739631094979f,  0.75011106963f,   0.7604059656f,    0.770513242776f,  0.780430407338f, 0.790155012376f, 0.799684658487f, 0.809016994375f,
            0.818149717425f,  0.827080574275f,  0.835807361368f,  0.844327925502f,  0.852640164354f,  0.860742027004f,  0.868631514438f, 0.876306680044f, 0.883765630089f, 0.891006524188f,
            0.898027575761f,  0.904827052466f,  0.911403276635f,  0.917754625684f,  0.923879532511f,  0.929776485888f,  0.93544403083f,  0.940880768954f, 0.946085358828f, 0.951056516295f,
            0.955793014798f,  0.960293685677f,  0.964557418458f,  0.968583161129f,  0.972369920398f,  0.975916761939f,  0.979222810622f, 0.982287250729f, 0.985109326155f, 0.987688340595f,
            0.990023657717f,  0.992114701314f,  0.993960955455f,  0.995561964603f,  0.996917333733f,  0.998026728428f,  0.998889874962f, 0.999506560366f, 0.999876632482f, 1f
        };//still need to go through these
          //https://stackoverflow.com/questions/2284860/how-does-c-compute-sin-and-other-math-functions
          //https://sourceware.org/git/?p=glibc.git;a=blob;f=sysdeps/ieee754/dbl-64/s_sin.c;hb=HEAD#l194

        [MethodImpl(MethodImplOptions.AggressiveInlining)]//should these just be manual inlined?
        public static float Cosine(float amount) => amount == 0.25f || amount == 0.75f ? 0f : Sine(amount + 0.25f);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CosineRadians(float radians) => radians == HalfPi || radians == ThreeQuartersTau ? 0f : SineRadians(radians + HalfPi);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CosineDegrees(float degrees) => degrees == 90f || degrees == 270f ? 0f : SineDegrees(degrees + 90f);
        
        public static float Sine(float amount)
        {
            //float amount = (float)Wrap((decimal)amount2, 0m, 1m);
            //int amount3 = Wrap((int)(amount2 * 10000f), 0, 10000);
            //float amount = (float)amount3 / 10000f;
            
            //if (amount == 0f || amount == 0.5f || amount == 1f) return 0f;
            //if (amount == 0.25f || amount == 0.75f) return 1f;

            //int i = Wrap((int)(amount * 400) - 1, 0, 99);
            int i = Wrap((int)(amount * 400), 0, 400);
            //System.Diagnostics.Debug.WriteLine($"{amount}=>{amount * 400}=>{i}");
            System.Diagnostics.Debug.WriteLine($"{amount}=>{amount * 400}=>{i}");

            //if (amount > 0.75f) return -sine[^i];
            if (i >= 300)
            {
                i -= 300;
                System.Diagnostics.Debug.WriteLine($"i > 300 =>{i}");
                return -sine[sine.Length - i];
            }
            //if (amount > 0.5f) return -sine[i];
            if (i >= 200)
            {
                i -= 200;
                System.Diagnostics.Debug.WriteLine($"i > 200 =>{i}");
                return -sine[i];
            }
            //if (amount > 0.25f) return sine[^i];
            if (i >= 100)
            {
                i -= 100;
                System.Diagnostics.Debug.WriteLine($"i > 100 =>{i}");
                return sine[sine.Length - i];
            }
            //i -= 1;
            System.Diagnostics.Debug.WriteLine($"i > 0 =>{i}");
            return sine[i];
        }

        public static float SineRadians(float radians)
        {
            radians = (float)Wrap((decimal)radians, 0m, (decimal)Tau);

            if (radians == 0f || radians == Pi || radians == Tau) return 0f;

            int i = Wrap((int)(radians / Tau * 400) - 1, 0, 99);

            if (radians > ThreeQuartersTau) return -sine[^i];
            if (radians > Pi) return -sine[i];
            if (radians > HalfPi) return sine[^i];
            return sine[i];
        }

        public static float SineDegrees(float degrees)
        {
            degrees = (float)Wrap((decimal)degrees, 0m, 360m);

            if (degrees == 0f || degrees == 180f || degrees == 360f) return 0f;

            int i = Wrap((int)(degrees / 360f * 400) - 1, 0, 99);

            if (degrees > 270f) return -sine[^i];
            if (degrees > 180f) return -sine[i];
            if (degrees > 90f) return sine[^i];
            return sine[i];
        }

        public static void SineCosine(float amount, out float sine, out float cosine)
        {
            amount = (float)Wrap((decimal)amount, 0m, 1m);

            switch (amount)
            {
                case 0f:
                    sine = 0f;
                    cosine = 1f;
                    break;
                case 0.25f:
                    sine = 1f;
                    cosine = 0f;
                    break;
                case 0.5f:
                    sine = 0f;
                    cosine = -1f;
                    break;
                case 0.75f:
                    sine = -1f;
                    cosine = 0f;
                    break;
                case 1f:
                    sine = 0f;
                    cosine = 1f;
                    break;
                default:
                    {
                        int i = Wrap((int)(amount * 400) - 1, 0, 99);

                        if (amount >= 0.75)
                        {
                            sine = -Math.sine[^i];
                            cosine = Math.sine[i];
                            return;
                        }
                        if (amount >= 0.5)
                        {
                            sine = -Math.sine[i];
                            cosine = -Math.sine[^i];
                            return;
                        }
                        if (amount >= 0.25)
                        {
                            sine = Math.sine[^i];
                            cosine = -Math.sine[i];
                            return;
                        }
                        sine = Math.sine[i];
                        cosine = Math.sine[^i];
                        break;
                    }
            }
        }

        public static void SineCosineRadians(float radians, out float sine, out float cosine)
        {
            radians = (float)Wrap((decimal)radians, 0m, (decimal)Tau);

            switch (radians)
            {
                case 0f:
                    sine = 0f;
                    cosine = 1f;
                    break;
                case HalfPi:
                    sine = 1f;
                    cosine = 0f;
                    break;
                case Pi:
                    sine = 0f;
                    cosine = -1f;
                    break;
                case ThreeQuartersTau:
                    sine = -1f;
                    cosine = 0f;
                    break;
                case Tau:
                    sine = 0f;
                    cosine = 1f;
                    break;
                default:
                    {
                        int i = Wrap((int)(radians / Tau * 400) - 1, 0, 99);

                        if (radians >= ThreeQuartersTau)
                        {
                            sine = -Math.sine[^i];
                            cosine = Math.sine[i];
                            return;
                        }
                        if (radians >= Pi)
                        {
                            sine = -Math.sine[i];
                            cosine = -Math.sine[^i];
                            return;
                        }
                        if (radians >= HalfPi)
                        {
                            sine = Math.sine[^i];
                            cosine = -Math.sine[i];
                            return;
                        }
                        sine = Math.sine[i];
                        cosine = Math.sine[^i];
                        break;
                    }
            }
        }

        public static void SineCosineDegrees(float degrees, out float sine, out float cosine)
        {
            degrees = (float)Wrap((decimal)degrees, 0m, 360m);

            switch (degrees)
            {
                case 0f:
                    sine = 0f;
                    cosine = 1f;
                    break;
                case 90f:
                    sine = 1f;
                    cosine = 0f;
                    break;
                case 180f:
                    sine = 0f;
                    cosine = -1f;
                    break;
                case 270f:
                    sine = -1f;
                    cosine = 0f;
                    break;
                case 360f:
                    sine = 0f;
                    cosine = 1f;
                    break;
                default:
                    {
                        int i = Wrap((int)(degrees / 360f * 400) - 1, 0, 99);

                        if (degrees >= 270f)
                        {
                            sine = -Math.sine[^i];
                            cosine = Math.sine[i];
                            return;
                        }
                        if (degrees >= 180f)
                        {
                            sine = -Math.sine[i];
                            cosine = -Math.sine[^i];
                            return;
                        }
                        if (degrees >= 90f)
                        {
                            sine = Math.sine[^i];
                            cosine = -Math.sine[i];
                            return;
                        }
                        sine = Math.sine[i];
                        cosine = Math.sine[^i];
                        break;
                    }
            }
        }

        //getwholesine()?
    }
}
#endif