
var possible = AllPossibleAnswers;
var outcome = "";
while (outcome != "*****")
{
    var attempt = BestAttempt(possible, ValidWords);
    Console.WriteLine(attempt);
    outcome = Console.ReadLine();
    possible = PossibleAnswersAfterAttempt(possible, attempt, outcome);
}

//Alternative program to analyse performance, tested over all 2,309 possible answer words
//var dict = new Dictionary<int, int>();
//foreach (var target in AllPossibleAnswers)
//{
//    var possible = AllPossibleAnswers;
//    var outcome = "";
//    var attempt = "RAISE";
//    int count = 0;
//    while (outcome != "*****")
//    {
//        Console.Write($"{attempt} ");
//        count++;
//        outcome = MarkAttempt(attempt, target);
//        possible = PossibleAnswersAfterAttempt(possible, attempt, outcome);
//        attempt = BestAttempt1(possible, ValidWords);
//    }
//    Console.WriteLine();
//    if (dict.Keys.Contains(count))
//    {
//        dict[count]++;
//    }
//    else
//    {
//        dict[count] = 1;
//    }
//}
//foreach (var attempts in dict.Keys.OrderBy(k => k))
//{
//    Console.WriteLine($"{attempts} attempts: {dict[attempts]} games");
//}
