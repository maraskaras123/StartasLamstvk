using System.ComponentModel.DataAnnotations;

namespace StartasLamstvk.Shared
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is null)
            {
                return true;
            }

            if (value.GetType() != typeof(DateTime))
            {
                return false;
            }

            var date = (DateTime)value;

            return DateTime.Now < date;
        }
    }
}
