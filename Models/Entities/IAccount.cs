namespace WeVsVirus.Models.Entities
{
    public interface IAccount
    {
        int Id { get; set; }
        string AppUserId { get; set; }
        AppUser AppUser { get; set; }
        int AddressId { get; set; }
        Address Address { get; set; }
    }
}
