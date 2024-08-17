using AutonomyApi.WebService;
using AutonomyApi.WebService.Validators;

namespace AutonomyApi.Validators
{
    public class CpfValidator : IValidator<string>
    {
        public string Value { get; }

        public CpfValidator(string value)
        {
            Value = value;
        }

        public bool IsValid(out string errorMessage)
        {
            int[] mult1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            if (Value.Length != 11)
            {
                errorMessage = "CPF must contain 11 digits.";
                return false;
            }

            var tempCpf = Value.Substring(0, 9);
            var sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * mult1[i];
            }

            var rest = sum % 11;
            var digito = (rest < 2 ? 0 : 11 - rest).ToString();

            tempCpf = tempCpf + digito;
            sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * mult2[i];
            }

            rest = sum % 11;
            digito += rest < 2 ? 0 : 11 - rest;

            if (Value.EndsWith(digito))
            {
                errorMessage = "";
                return true;
            }

            errorMessage = "Invalid CPF format.";
            return false;
        }
    }
}