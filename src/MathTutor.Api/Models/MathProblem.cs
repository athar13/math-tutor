namespace MathTutor.Api.Models;

public class MathProblem
{
    public long Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public int Answer { get; set; }

    public static MathProblem Generate()
    {
        var rnd = new Random();
        int num1 = rnd.Next(1, 10);
        int num2 = rnd.Next(1, 10);

        return new MathProblem
        {
            Question = $"{num1} + {num2}",
            Answer = num1 + num2
        };
    }
}
