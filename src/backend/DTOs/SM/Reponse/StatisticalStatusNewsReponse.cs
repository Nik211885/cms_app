using backend.Core.ValueObject;

namespace backend.DTOs.SM.Reponse
{
    public record StatisticalStatusNewsReponse
    {
        public Status status { get; set; }
        public long count_status { get; set; }
    }
}
