

namespace Networks.Example.XOr
{
	public class TrainingData
	{
		public TrainingData(int input1, int input2, int output)
		{
			Input1 = input1;
			Input2 = input2;
			Output = output;
		}

		public int Input1 { get; set; }
		public int Input2 { get; set; }
		public int Output { get; set; }
	}
}
