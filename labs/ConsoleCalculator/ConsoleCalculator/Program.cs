namespace ConsoleCalculator
{
    class Program
    {
        private const int EXIT_SUCCESS = 0;
        private const int EXIT_FAILURE = 1;

        private enum Operation
        {
            ADDITION = '+',
            SUBTRACTION = '-',
            MULTIPLICATION = '*',
            DIVISION = '/',
        }

        public static int Main(string[] args)
        {
            try
            {
                double operand1 = ReadNumber("Enter first operand");
                double operand2 = ReadNumber("Enter second operand");
                Operation operation = ReadOperation("Enter operation (+, -, *, /)");

                double result = CalculateResult(operand1, operand2, operation);
                PrintResult(operand1, operand2, operation, result);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Error: " + e.Message);
                return EXIT_FAILURE;
            }

            return EXIT_SUCCESS;
        }

        private static double ReadNumber(string? message = null)
        {
            if (message != null)
            {
                Console.Write(message + ": ");
            }

            string? input;
            input = Console.ReadLine();

            try
            {
                return Convert.ToDouble(input);
            }
            catch (FormatException)
            {
                throw new FormatException($"'{input}' is not a valid number");
            }
            catch (OverflowException)
            {
                throw new OverflowException($"number '{input}' is too big");
            }
        }

        private static Operation ReadOperation(string? message = null)
        {
            if (message != null)
            {
                Console.Write(message + ": ");
            }

            char input;
            input = (char)Console.Read();
            Console.ReadLine();

            return input switch
            {
                '+' => Operation.ADDITION,
                '-' => Operation.SUBTRACTION,
                '*' => Operation.MULTIPLICATION,
                '/' => Operation.DIVISION,
                _ => throw new InvalidOperationException($"invalid operation: '{input}'"),
            };
        }

        private static double CalculateResult(double operand1, double operand2, Operation operation)
        {
            if (operation == Operation.DIVISION && operand2.Equals(0))
            {
                throw new DivideByZeroException("zero division");
            }

            double result = operation switch
            {
                Operation.ADDITION => operand1 + operand2,
                Operation.SUBTRACTION => operand1 - operand2,
                Operation.MULTIPLICATION => operand1 * operand2,
                Operation.DIVISION => operand1 / operand2,
                _ => throw new NotImplementedException("operation not implemented"),
            };

            if (double.IsInfinity(result))
            {
                throw new OverflowException("overflow while calculating the result");
            }

            return result;
        }

        private static void PrintResult(double operand1, double operand2, Operation operation, double result)
        {
            Console.WriteLine($"{operand1:0.00} {(char)operation} {operand2:0.00} = {result:0.00}");
        }
    }
}
