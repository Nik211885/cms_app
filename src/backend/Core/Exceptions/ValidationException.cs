namespace backend.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string messageValidation) :base($"Error validation data {messageValidation}")
        {
            
        }
    }
}
