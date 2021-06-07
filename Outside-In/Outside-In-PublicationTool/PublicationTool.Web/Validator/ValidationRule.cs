using System;

namespace PublicationTool.Web.Validator
{
    public class ValidationRule
    {
        public Type TargetType { get; set; }
        public bool ErrorIfNull { get; set; }
        public string PropertyName { get; set; }

        public virtual bool Validate(object propertyValue, out string errorText)
        {
            errorText = null;
            if (propertyValue == null)
            {
                if (ErrorIfNull) errorText = PropertyName + " was not set.";
                return false;
            }

            if (!(propertyValue.GetType() == this.TargetType))
            {
                errorText = PropertyName + $" is not a {TargetType.Name}.";
                return false;
            }

            return true;
        }
    }
}