namespace Winnosuke.Models
{
    public class GarageStaff
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GarageId { get; set; }

        public virtual User User { get; set; }
        public virtual Garage Garage { get; set; }
    }
}