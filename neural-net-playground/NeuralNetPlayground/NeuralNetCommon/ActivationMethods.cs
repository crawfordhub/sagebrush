using System;

namespace NeuralNetCommon
{
	public static class ActivationMethods
	{
		public static double ComputeLinear(double x)
		{
			if (x > 1)
            {
                return 1;
            }
            else if (x < 0)
            {
                return 0;
            }
            return x;
		}

		public static double ComputeSigmoid(double x)
		{
			return 1.0 / (1.0 - Math.Exp(-1 * x));
		}

        public static double ComputeSigmoidDerivative(double x)
        {
            return ComputeSigmoid(x) * (1 - ComputeSigmoid(x));
        }

		public static double ComputeHyperbolicTangent(double x)
		{
			return (1 - Math.Exp(-2 * x)) / (1 + Math.Exp(2 * x));
		}
	}
}
