public static class WordleFunctions
{
    public static bool IsGreen(string attempt, string target, int n) => target[n] == attempt[n];

    public static string SetChar(string word, int n, char newChar) => word.Substring(0, n) + newChar + word.Substring(n + 1);

    public static string SetAttemptIfGreen(string attempt, string target, int n) =>
            IsGreen(attempt, target, n) ? SetChar(attempt, n, '*') : attempt;

    public static string SetTargetIfGreen(string attempt, string target, int n) =>
        IsGreen(attempt, target, n) ? SetChar(target, n, '.') : target;

    public static (string attempt, string target) EvaluateGreens(string attempt, string target) =>
        Range(0, 5).Aggregate((attempt, target), (a, n) =>
            (SetAttemptIfGreen(a.attempt, a.target, n), SetTargetIfGreen(a.attempt, a.target, n)));

    public static bool IsYellow(string attempt, string target, int n) => target.Contains(attempt[n]);

    public static bool IsAlreadyMarkedGreen(string attempt, int n) => attempt[n] == '*';

    public static string SetAttemptIfYellow(string attempt, string target, int n) =>
        IsAlreadyMarkedGreen(attempt, n) ? attempt : IsYellow(attempt, target, n) ? SetChar(attempt, n, '+') : SetChar(attempt, n, '_');

    public static string SetTargetIfYellow(string attempt, string target, int n) =>
            IsAlreadyMarkedGreen(attempt, n) ? target : IsYellow(attempt, target, n) ? SetChar(target, target.IndexOf(attempt[n]), '.') : target;

    public static (string attempt, string target) EvaluateYellows(string attempt, string target) =>
        Range(0, 5).Aggregate((attempt, target), (a, n) =>
            (SetAttemptIfYellow(a.attempt, a.target, n), SetTargetIfYellow(a.attempt, a.target, n)));

    public static string MarkAttempt(string attempt, string target) =>
        EvaluateYellows(EvaluateGreens(attempt, target).attempt, EvaluateGreens(attempt, target).target).attempt;

    public static IEnumerable<string> PossibleAnswersAfterAttempt(IEnumerable<string> prior, string attempt, string mark) =>
        prior.Where(w => MarkAttempt(attempt, w) == mark).ToList();

    public static int WordCountRemainingAfterAttempt(IEnumerable<string> possibleAnswers, string attempt) =>
        possibleAnswers.GroupBy(w => MarkAttempt(attempt, w)).Max(g => g.Count());

    public static IEnumerable<(string word, int count)> AllRemainingWordCounts(IEnumerable<string> possAnswers, IEnumerable<string> possAttempts) =>
        possAttempts.AsParallel().Select(w => (w, WordCountRemainingAfterAttempt(possAnswers, w)));

    public static (string word, int count) BetterOf((string word, int count) word1, (string word, int count) word2, IEnumerable<string> possAnswers) =>
        (word2.count < word1.count) || (word2.count == word1.count && possAnswers.Contains(word2.word)) ? word2 : word1;

    public static string BestAttempt(IEnumerable<string> possAnswers, IEnumerable<string> possAttempts) =>
        AllRemainingWordCounts(possAnswers, possAttempts).Aggregate((bestSoFar, next) => BetterOf(bestSoFar, next, possAnswers)).word;
}
        
