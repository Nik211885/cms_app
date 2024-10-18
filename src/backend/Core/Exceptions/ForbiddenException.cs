namespace backend.Core.Exceptions
{
    public class ForbiddenException : Exception, IException
    {
        public ForbiddenException() : base("Ban khong duoc phep truy cap vao phan nay")
        {
            
        }
    }
}
