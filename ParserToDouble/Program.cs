namespace ParserToDouble
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            try
            {
                double output = StringToDouble(input);
                Console.WriteLine(output);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static double StringToDouble(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException("Вы ввели пустое число.");
            }

            bool isNegative = false;
            bool hasDecimalPoint = false;
            double result = 0.0;
            int decimalPosition = 0;

            int i = 0;

            // Обработка знака
            if (str[i] == '-')
            {
                isNegative = true;
                i++;
            }
            else if (str[i] == '+')
            {
                i++;
            }

            for (; i < str.Length; i++)
            {
                // Основная часть числа
                char currentChar = str[i];
                double decimalFactor = 0.1;

                if (currentChar == '.' || currentChar == ',')
                {
                    if (hasDecimalPoint)
                    {
                        throw new FormatException("Много разделителей.");
                    }
                    hasDecimalPoint = true;
                    decimalPosition = i;
                    continue;
                }

                if (currentChar < '0' || currentChar > '9')
                {
                    throw new FormatException($"Некорректный символ '{currentChar}'.");
                }


                //Дробная часть числа
                int digit = currentChar - '0';

                if (hasDecimalPoint)
                {
                    decimalFactor = Math.Pow(decimalFactor, i - decimalPosition);
                    result += digit * decimalFactor;
                
                }
                else
                {
                    result = result * 10 + digit;
                }
            }
            result = Math.Round(result, i - decimalPosition);
            return isNegative ? -result : result;
        }
    }
}


