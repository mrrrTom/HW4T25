// Задача 25: Напишите цикл, который принимает на вход два числа (A и B) и возводит число A в натуральную степень B.
// 3, 5 -> 243 (3⁵)
// 2, 4 -> 16


namespace MathGB
{
    public class ConsoleApp
    {
        static void Main()
        {
            Console.WriteLine("Welcome to the power finder application!");
            var @base = default(int);
            var pow = default(int);

            // To allow negative/zero powers change check in power`s TryGetInputNumber
            if (!TryGetInputNumber(true, "Input base:", out @base) || !TryGetInputNumber(true, "Input power (N):", out pow, (int num) => num > 0))
            {
                return;
            }

            Console.WriteLine( Pow(@base, pow) );
        }

        static double Pow(int @base, int pow)
        {
            var operation = default(Func<double, int, double>);
            switch (pow)
            {
                case > 0:
                operation = new Func<double, int, double>((double a, int b) => (double)a * (double)b);
                break;
                case < 0:
                operation = new Func<double, int, double>((double a, int b) => (double)a / (double)b);
                break;
                case 0:
                return 1;
            }
            
            var cycle = new Cycle(@base, Math.Abs(pow), operation);
            return cycle.Evaluate();
        }

        static bool TryGetInputNumber(bool throwConsoleNotification, string outputMessage, out int result, Func<int, bool>check = null)
        {
            result = default(int);
            Console.WriteLine(outputMessage);
            var input = Console.ReadLine();
            if (!int.TryParse(input, out result) || (check != null && !check(result)))
            {
                if (throwConsoleNotification) Console.WriteLine("Sorry, program could not handle inserted value... Bye!");
                return false;
            }
            
            return true;
        }
    }

    internal class Cycle
    {
        private readonly Func<double, int, double> _operation;
        private readonly int _value;
        private readonly int _iterateMax;
        public Cycle(int value, int max, Func<double, int, double> operation)
        {
            _operation = operation;
            _iterateMax = max;
            _value = value;
        }

        public double Evaluate()
        {
            var result = 1d;
            for (int i = 0; i < _iterateMax; i++)
            {
                result = _operation(result, _value);
            }

            return result;
        }
    }
}
