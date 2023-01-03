public static class WordleFunctions
{
    public static string Set(string word, int n, char newChar) => 
        word.Substring(0, n) + newChar + word.Substring(n + 1);

    public static bool IsGreen(string attempt, string target, int n) => 
        target[n] == attempt[n];

    public static string SetAttemptIfGreen(string attempt, string target, int n) => 
        IsGreen(attempt, target, n) ? Set(attempt, n, '*') : attempt;

    public static string SetTargetIfGreen(string attempt, string target, int n) => 
        IsGreen(attempt, target, n) ? Set(target, n, '.') : target;

    public static (string, string) EvaluateGreens(string attempt, string target) => 
        Enumerable.Range(0, 5).Aggregate((attempt, target), (a, x) => (SetAttemptIfGreen(a.Item1, a.Item2, x), SetTargetIfGreen(a.Item1, a.Item2, x)));

    public static bool IsYellow(string attempt, string target, int n) => 
        target.Contains(attempt[n]);

    public static bool IsAlreadyMarkedGreen(string attempt, int n) => 
        attempt[n] == '*';

    public static string SetAttemptIfYellow(string attempt, string target, int n) => 
        IsAlreadyMarkedGreen(attempt, n) ? attempt : IsYellow(attempt, target, n) ? Set(attempt, n, '+') : Set(attempt, n, '_');

    public static string SetTargetIfYellow(string attempt, string target, int n) => 
        IsAlreadyMarkedGreen(attempt, n) ? target : IsYellow(attempt, target, n) ? Set(target, target.IndexOf(attempt[n]), '.') : target;

    public static (string, string) EvaluateYellows(string attempt, string target) => 
        Enumerable.Range(0, 5).Aggregate((attempt, target), (a, x) => (SetAttemptIfYellow(a.Item1, a.Item2, x), SetTargetIfYellow(a.Item1, a.Item2, x)));

    public static string MarkAttempt(string attempt, string target) => 
        EvaluateYellows(EvaluateGreens(attempt, target).Item1, EvaluateGreens(attempt, target).Item2).Item1;

    public static ImmutableList<string> PossibleAnswersAfterAttempt(ImmutableList<string> prior, string attempt, string mark) => 
        prior.Where(w => MarkAttempt(attempt, w) == mark).ToImmutableList();

    public static int WordCountLeftByWorstOutcome(ImmutableList<string> possibleWords, string attempt) => 
        possibleWords.GroupBy(w => MarkAttempt(attempt, w)).Max(g => g.Count());

    public static string BestAttempt(ImmutableList<string> possAnswers, ImmutableList<string> possAttempts) => 
        possAttempts.AsParallel().Select(w => (WordCountLeftByWorstOutcome(possAnswers, w), w)).Aggregate((best, x) => (x.Item1 < best.Item1) || (x.Item1 == best.Item1 && possAnswers.Contains(x.Item2)) ? x : best).Item2;

    //Alternative algorithm. To use this, change BestAttempt to call this new function in place of WordCountLeftByWorstOutcome
    static double ExpectedRemainingWordCountAfterAttempt(ImmutableList<string> possibleWords, string attempt) => possibleWords.GroupBy(w => MarkAttempt(attempt, w)).Sum(g => g.Count() * g.Count()) / possibleWords.Count;
}
        
