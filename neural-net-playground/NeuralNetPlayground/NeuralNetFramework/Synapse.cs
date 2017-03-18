

namespace NeuralNetFramework
{
	public class Synapse
	{
		public Synapse(Node inputNode, Node outputNode, double weight)
		{
			InputNode = inputNode;
			OutputNode = outputNode;
			Weight = weight;
		}

		public Node InputNode { get; private set; }

		public Node OutputNode { get; private set; }

		public double Weight { get; set; }

        // Hack for now
        public double OldWeight;
	}
}
