using System;
using NeuralNetCommon;

namespace NeuralNetFramework
{
    public class Node
    {
		public Node(ActivationMethodType method)
		{
			activationMethodType = method;
		}

		public double Sum { get; set; }

		public double Result 
		{ 
			get
			{
				switch (activationMethodType)
				{
					case ActivationMethodType.None:
						return Sum;

					case ActivationMethodType.Linear:
						return ActivationMethods.ComputeLinear(Sum);

					case ActivationMethodType.Sigmoid:
						return ActivationMethods.ComputeSigmoid(Sum);

					case ActivationMethodType.HyperbolicTangent:
						return ActivationMethods.ComputeHyperbolicTangent(Sum);

					default:
						return Sum;
				}
			}
		}

		public override string ToString()
		{
			return String.Format("Sum: {0}, Result: {1}", Sum, Result);
		}

		private ActivationMethodType activationMethodType;

        // Hack for now;
        public double DeltaHiddenSum;
    }

	public enum ActivationMethodType
	{
		None,
		Linear,
		Sigmoid,
		HyperbolicTangent
	}
}
