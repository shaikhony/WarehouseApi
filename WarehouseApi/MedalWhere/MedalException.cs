namespace WarehouseApi.MedalWhere
{
    public class MedalException : Exception
    {
        public ErrorType ErrorType { get; }

        public MedalException(ErrorType errorType, string message) : base(message)
        {
            ErrorType = errorType;
        }
    }
}
