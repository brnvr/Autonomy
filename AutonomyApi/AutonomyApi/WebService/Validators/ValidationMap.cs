using System.Collections;
using System.Collections.Frozen;

namespace AutonomyApi.WebService.Validators
{
    public class ValidationMap<TEnum, TValue> : IEnumerable<KeyValuePair<TEnum, Func<TValue, IValidator<TValue>>>> where TEnum : struct
    {
        private Dictionary<TEnum, Func<TValue, IValidator<TValue>>> _validations;

        public FrozenDictionary<TEnum, Func<TValue, IValidator<TValue>>>? Validations { get; private set; }

        public ValidationMap()
        {
            _validations = new Dictionary<TEnum, Func<TValue, IValidator<TValue>>>();
        }

        public void Add(TEnum key, Func<TValue, IValidator<TValue>> validation)
        {
            _validations.Add(key, validation);
        }

        public ValidationMap<TEnum, TValue> Freeze()
        {
            Validations = _validations.ToFrozenDictionary();

            return this;
        }

        public bool IsValid(TEnum type, TValue value, out string errorMessage)
        {
            if (Validations is null)
            {
                return _validations[type](value).IsValid(out errorMessage);
            }

            try
            {
                return Validations[type](value).IsValid(out errorMessage);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Validation not defined for type {type}.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error validating type {type} with value {value}: {ex.Message}", ex);
            }
        }

        public IEnumerator<KeyValuePair<TEnum, Func<TValue, IValidator<TValue>>>> GetEnumerator()
        {
            return _validations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
