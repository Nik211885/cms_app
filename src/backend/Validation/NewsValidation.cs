using backend.Core.ValueObject;

namespace backend.Validation
{
    public static class NewsValidation
    {
        public static void NewsEdit(Status status)
        {
            if (status >= Status.Waiting)
            {
                throw new Exception($"Bạn không thể thay đổi bài sang trạng thái {status}");
            }
        }
    }
}
