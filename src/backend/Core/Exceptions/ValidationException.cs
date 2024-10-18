namespace backend.Core.Exceptions
{
    public class ValidationException : Exception, IException
    {
        public ValidationException(string messageValidation) :base($"Error validation data {messageValidation}")
        {
            
        }
    }
}
