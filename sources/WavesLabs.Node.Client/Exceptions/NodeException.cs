namespace WavesLabs.Node.Client.Exceptions
{
    public class NodeException : Exception
    {
        public int ErrorCode { get; init; }

        public NodeException(ApiError apiError) : base(apiError.Message)
        {
            ErrorCode = apiError.Error;
        }
    }
}
