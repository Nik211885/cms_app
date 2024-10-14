namespace backend.Core.Entities.CMS
{
    public class cms_menu_news
    {
        public int menu_id { get; private set; }
        public int news_id { get; private set; }
        public cms_menu_news()
        {

        }
        public cms_menu_news(int menuId, int newsId)
        {
            menu_id = menuId;
            news_id = newsId;
        }
    }
}
