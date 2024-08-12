namespace AutonomyApi.ValueTypes.Documents
{
    public class Cpf : Document
    {
        public Cpf(string value) : base(value)
        {
            if (!IsValid(value, out string error))
            {
                throw new FormatException(error);
            }
        }

        protected override string HandleInputValue(string value)
        {
            return value.Trim().Replace(".", "").Replace("-", "");
        }

        public static bool IsValid(string value, out string error)
        {
            int[] mult1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            if (value.Length != 11)
            {
                error = "CPF must contain 11 digits.";

                return false;
            }
                
            var tempCpf = value.Substring(0, 9);
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

            if (value.EndsWith(digito))
            {
                error = "";
                return true;
            }

            error = "Invalid CPF format.";

            return false;
        }
    }
}