using BackgammonOnline.Web.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BackgammonOnline.Web.ViewModels
{
    public class CreateGameRoomViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Please enter a room name.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Room name must contain between 3 and 100 characters")]
        [Display(Name = "Room Name")]
        public string Name { get; set; } = String.Empty;

        [Required]
        [Display(Name = "Game Variant")]
        public GameVariant Variant { get; set; } = GameVariant.Portes;

        [Display(Name = "Private Room")]
        public bool IsPrivate { get; set; } = false;

        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6,
            ErrorMessage = "Password must contain between 6 and 100 characters")]
        [Display(Name = "Room Password")]
        public string? Password { get; set; }

        public IEnumerable<ValidationResult> Validate
            (ValidationContext validationContext)
        {
            if (IsPrivate && string.IsNullOrWhiteSpace(Password))
            {
                yield return new ValidationResult(
                    "A password is required for private room.",
                    new[] { nameof(Password) });
            }

            if(!IsPrivate && !string.IsNullOrEmpty(Password))
            {
                yield return new ValidationResult(
                    "Public rooms must not have a password",
                    new[] { nameof(Password) });
            }
        }
    }
}
