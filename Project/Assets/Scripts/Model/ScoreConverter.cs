using System;

public static class ScoreConverter
{
    public static string Convert(double score)
    {
        if(score > 1000)
        {
            var pow = (int)Math.Log10(score);
            var index = pow / 3;
            return Math.Round(score / Math.Pow(10, index * 3), (pow % 3 > 1 ? 0 : 1)).ToString() + (ScoreIndex)index;
        }

        return Math.Round(score, 1).ToString();
    }

    enum ScoreIndex
    {
        K = 1,
        M = 2,
        B = 3,
    }
}
