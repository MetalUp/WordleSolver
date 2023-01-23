using System.Diagnostics;

var sw = new Stopwatch();
sw.Start();
BestAttempt(AllPossibleAnswers, ValidWords);
sw.Stop();
Console.WriteLine($"{sw.ElapsedMilliseconds} ms");


//var possible = AllPossibleAnswers;
//var outcome = "";
//while (outcome != "*****")
//{
//    var attempt = BestAttempt(possible, ValidWords);
//    Console.WriteLine(attempt);
//    outcome = Console.ReadLine();
//    possible = PossibleAnswersAfterAttempt(possible, attempt, outcome).ToList();
//}


//Alternative program to analyse performance

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
