using System.Collections.Immutable;
using System.Windows.Media;

namespace Playtech.Results;

public class ResultsModel
{
    private static readonly int[] redNumbers = [1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36];

    public ImmutableArray<RouletteResult> Results = [];
    
    public void AddRandomResult()
    {
        int position = new Random().Next(0, 37);
        int streak = CalculateStreak(position);
        int multiplier = position * streak;
        Color color = GetColor(position);
        Results = Results.Add(new RouletteResult(position, multiplier, color, streak));
    }

    private static Color GetColor(int position)
    {
        return position switch
        {
            0 => Colors.Green,
            _ => redNumbers.Contains(position) ? Colors.Red : Colors.Black,
        };
    }

    private int CalculateStreak(int position)
    {
        Color color = GetColor(position);
        int streak = 0;

        foreach (var result in Results.Reverse())
        {
            if (GetColor(result.Position) == color)
            {
                streak++;
            }
            else
            {
                break;
            }
        }

        return streak;
    }

    public record RouletteResult(int Position, int Multiplier, Color Color, int Streak)
    {
        public int Position { get; init; } = Position;
        public int Multiplier { get; init; } = Multiplier;
        public int Streak { get; init; } = Streak;
        private Color Color { get; init; } = Color;
        public string ColorString => Color.ToString();
        public SolidColorBrush ColorBrush => new(Color);
        public SolidColorBrush BorderBrush => GetBorderBrush(ColorString);
    }

    private static SolidColorBrush GetBorderBrush(string color)
    {
        return color switch
        {
            "#FFFF0000" => new SolidColorBrush(Colors.IndianRed),
            "#FF000000" => new SolidColorBrush(Colors.Violet),
            _ => new SolidColorBrush(Colors.LightGreen),
        };
    }
}
