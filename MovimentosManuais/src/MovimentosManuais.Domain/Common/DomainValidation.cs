namespace MovimentosManuais.Domain.Common
{
    public static class DomainValidation
    {
        public static void NotNullOrWhiteSpace(string? value, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException(message);
        }

        public static void MaxLength(string? value, int maxLength, string message)
        {
            if (!string.IsNullOrWhiteSpace(value) && value.Length > maxLength)
                throw new DomainException(message);
        }

        public static void GreaterThan(decimal value, decimal minValue, string message)
        {
            if (value <= minValue)
                throw new DomainException(message);
        }

        public static void Between(short value, short minValue, short maxValue, string message)
        {
            if (value < minValue || value > maxValue)
                throw new DomainException(message);
        }

        public static void GreaterThanOrEqual(short value, short minValue, string message)
        {
            if (value < minValue)
                throw new DomainException(message);
        }
    }
}
