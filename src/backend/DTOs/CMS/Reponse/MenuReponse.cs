using System.Text.Json.Serialization;

namespace backend.DTOs.CMS.Reponse
{
    public class MenuReponse
    {
        public int id { get; private set; }
        public string name { get; private set; } = null!;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<MenuReponse>? menu_child { get; set; }
    }
}   
