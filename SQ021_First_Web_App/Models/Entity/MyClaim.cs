namespace SQ021_First_Web_App.Models.Entity
{
    public class MyClaim
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = "";
    }
}
