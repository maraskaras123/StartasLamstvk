namespace StartasLamstvk.Shared.Models
{
    public class Error
    {
        public bool IsInternal { get; set; }
        public string Message { get; set; }
        public string FieldName { get; set; }

        public Error()
        {
        }

        public Error(string message, bool isInternal)
        {
            Message = message;
            IsInternal = isInternal;
        }

        public Error(string message, bool isInternal, string fieldName)
        {
            IsInternal = isInternal;
            Message = message;
            FieldName = fieldName;
        }
    }
}