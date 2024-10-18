namespace backend.Core.Exceptions
{
    public class NotFoundException : Exception, IException
    {
        public NotFoundException(int id) : base ($"Khong tim thay muc co id la {id}") 
        {
            
        }
    }
}
