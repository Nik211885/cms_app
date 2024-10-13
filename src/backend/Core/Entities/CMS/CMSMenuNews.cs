namespace backend.Core.Entities.CMS
{
    public class CMSMenuNews
    {
        public int menu_id { get; private set; }
        public int news_id { get; private set; }
        public CMSMenuNews(int menuId, int newsId)
        {
            menu_id = menuId;
            news_id = newsId;
        }
    }
}
