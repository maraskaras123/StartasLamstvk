using System.ComponentModel.DataAnnotations;

namespace StartasLamstvk.Shared.Models.Wage
{
    public class WageWriteModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Amount is required")]
        [Range(typeof(decimal), "0.1", "1000", ConvertValueInInvariantCulture = true,
            ParseLimitsInInvariantCulture = true)]
        public decimal Amount { get; set; }

        public string Note { get; set; }
    }
}