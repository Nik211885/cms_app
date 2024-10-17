namespace backend.DTOs.SM.Reponse
{
    public class StatisticalNewsReponse
    {
        public long count_news_censor_success { get; set; }
        public long count_news_active { get; set; }
        public long count_news_inactive { get; set; }
        public long count_news_censor_failure { get; set; }
    }
}
