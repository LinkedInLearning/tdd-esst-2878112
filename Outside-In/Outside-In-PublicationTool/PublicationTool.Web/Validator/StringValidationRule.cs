namespace PublicationTool.Web.Validator
{
    public class StringValidationRule : ValidationRule
    {
        public int MinLength { get; set; }
        public int MaxLength { get; set; }

        public StringValidationRule(int minLength, int maxLength, bool errorIfNull, string propertyName) : this()
        {
            MinLength = minLength;
            MaxLength = maxLength;
            ErrorIfNull = errorIfNull;
            PropertyName = propertyName;
        }

        public StringValidationRule()
        {
            TargetType = typeof(string);
        }

        public override bool Validate(object propertyValue, out string errorText)
        {
            errorText = null;

            if (!base.Validate(propertyValue, out errorText))
                return false;

            var stringValue = propertyValue.ToString();
            if (stringValue.Length < MinLength)
            {
                errorText = PropertyName +
                            $" should be {MinLength} characters long but is {stringValue.Length} characters long.";
                return false;
            }

            if (stringValue.Length > MaxLength)
            {
                errorText = PropertyName +
                            $" should be {MaxLength} characters long but is {stringValue.Length} characters long.";
                return false;
            }

            return string.IsNullOrEmpty(errorText);
        }
    }
}