namespace AutonomyApi.ValueTypes.Documents
{
    public abstract class Document
    {
        protected string Value { get; }

        public Document(string value)
        {
            Value = HandleInputValue(value);
        }

        public override string ToString()
        {
            return Value;
        }

        protected abstract string HandleInputValue(string value);
    }
}
