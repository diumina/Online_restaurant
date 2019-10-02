namespace Simple_shop.Services.Infrastructure
{
	public class OperationDetails
	{
		public bool Succedeed { get; private set; }

		public string Message { get; private set; }

		public OperationDetails(bool succedeed, string message = "")
		{
			Succedeed = succedeed;
			Message = message;
		}
	}
}
