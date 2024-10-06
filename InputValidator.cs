using UniversityManagement;

public class InputValidator
{
    private readonly Logger logger;

    public InputValidator(Logger logger)
    {
        this.logger = logger;
    }

    public string GetValidatedInput(string prompt, string warningMessage)
    {
        string? input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(warningMessage);
                logger.LogWarning(warningMessage);
            }
        } while (string.IsNullOrWhiteSpace(input));

        return input;
    }

    public int GetValidatedIntInput(string prompt, string warningMessage)
    {
        string? input;
        int result;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine();
            if (!int.TryParse(input, out result))
            {
                Console.WriteLine(warningMessage);
                logger.LogWarning(warningMessage);
            }
        } while (!int.TryParse(input, out result));

        return result;
    }

    public DateTime GetValidatedDateInput(string prompt, string warningMessage)
    {
        string? input;
        DateTime date;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine();
            if (!DateTime.TryParse(input, out date))
            {
                Console.WriteLine(warningMessage);
                logger.LogWarning($"{warningMessage}: {input}");
            }
        } while (!DateTime.TryParse(input, out date));

        return date;
    }
}