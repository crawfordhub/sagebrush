using NeuralNetFramework;
using System.Collections.Generic;
using System;

namespace Networks.Example.XOr
{
    public class Network
    {
		const int NumHiddenLayerNodes = 3;

		public Network()
		{
			TrainingFunction = new List<TrainingData>
			{
				new TrainingData(0, 0, 0),
				new TrainingData(1, 0, 1),
				new TrainingData(0, 1, 1),
				new TrainingData(1, 1, 0)
			};

			Initialize();
		}

		public void Initialize()
		{
			HiddenLayer = new List<Node>();
			Synapses = new List<Synapse>();

			Input1 = new Node(ActivationMethodType.None);
			Input2 = new Node(ActivationMethodType.None);
			Output = new Node(ActivationMethodType.Sigmoid);

			for (int i = 0; i < NumHiddenLayerNodes; i++)
			{
				HiddenLayer.Add(new Node(ActivationMethodType.Sigmoid));
			}

			// Create synapses between the nodes:
			var random = new Random();
			double weight;
			foreach(var node in HiddenLayer)
			{
				weight = (double)random.Next(0, 100) / 100;
				Console.WriteLine("Creating synapse with weight = {0}", weight);
				Synapses.Add(new Synapse(Input1, node, weight));

				weight = (double)random.Next(0, 100) / 100;
				Console.WriteLine("Creating synapse with weight = {0}", weight);
				Synapses.Add(new Synapse(Input2, node, weight));

				weight = (double)random.Next(0, 100) / 100;
				Console.WriteLine("Creating synapse with weight = {0}", weight);
				Synapses.Add(new Synapse(node, Output, weight));
			}
		}

		public Node Input1 { get; set; }
		public Node Input2 { get; set; }

		public List<Node> HiddenLayer;

		public List<Synapse> Synapses;

		public Node Output { get; set; }

		public List<TrainingData> TrainingFunction;
    }
}
