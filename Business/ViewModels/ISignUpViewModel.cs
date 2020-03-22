namespace WeVsVirus.Business.ViewModels
{
    public interface ISignUpViewModel
    {
        string Email { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }
        AddressViewModel Address { get; set; }
    }
}
