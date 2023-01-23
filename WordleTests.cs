

using System.Collections;

[TestClass]
    public class Tests
    {
    
[TestMethod]
public void TestIsGreen()
{
    Test("ABCDE", "A____", 0, true);
    Test("ABCDE", "____E", 4, true);
    Test("ABCDE", "_A___", 1, false);
    Test("BABBB", "B____", 1, false);

    void Test(string attempt, string target, int charNo, bool expected)
    {
        Assert.AreEqual(expected, IsGreen(attempt, target, charNo));
    }
}

[TestMethod]
public void TestSetChar()
{
    Test("ABCDE", 0, '_', "_BCDE");
    Test("ABCDE", 4, '_', "ABCD_");

    void Test(string word, int charNo, char newChar, string expected)
    {
        Assert.AreEqual(expected, SetChar(word, charNo, newChar));
    }
}

[TestMethod]
public void TestSetAttemptIfGreen()
{
    Test("ABCDE", "ABCDE", 0, "*BCDE");
    Test("ABCDE", "ABCDE", 4, "ABCD*");
    Test("BBCDE", "ABCDE", 0, "BBCDE");
    Test("ABCDE", "AACDE", 0, "*BCDE");

    void Test(string attempt, string target, int charNo, string expected)
    {
        Assert.AreEqual(expected, SetAttemptIfGreen(attempt, target, charNo));
    }
}

[TestMethod]
public void TestSetTargetIfGreen()
{
    Test("ABCDE", "ABCDE", 0, ".BCDE");
    Test("ABCDE", "ABCDE", 4, "ABCD.");
    Test("BBCDE", "ABCDE", 0, "ABCDE");
    Test("ABCDE", "AACDE", 0, ".ACDE");

    void Test(string attempt, string target, int charNo, string expected)
    {
        Assert.AreEqual(expected, SetTargetIfGreen(attempt, target, charNo));
    }
}

[TestMethod]
public void TestEvaluateGreens()
{
    Test("ABCDE", "AXXXX", ("*BCDE", ".XXXX"));
    Test("ABCDE", "XXXXE", ("ABCD*", "XXXX."));
    Test("ABCDE", "ABCDE", ("*****", "....."));
    Test("AACDE", "AXXXX", ("*ACDE", ".XXXX"));
    Test("ABCDE", "AAXXX", ("*BCDE", ".AXXX"));

    void Test(string attempt, string target, (string, string) expected)
    {
        Assert.AreEqual(expected, EvaluateGreens(attempt, target));
    }
}

[TestMethod]
public void TestIsYellow()
{
    Test("ABCDE", "____A", 0, true);
    Test("ABCDE", "____A", 4, false);
    Test("ABCDE", "___AA", 0, true);
    Test("AACDE", "_A___", 1, true);
    Test("AACDE", "__A__", 1, true);

    void Test(string attempt, string target, int charNo, bool expected)
    {
        Assert.AreEqual(expected, IsYellow(attempt, target, charNo));
    }
}

[TestMethod]
public void TestIsAlreadyMarkedGreen()
{
    Test("AB*DE", 2, true);
    Test("AB*DE", 0, false);
    Test("AB*DE", 4, false);
    Test("*BCD*", 2, false);
    Test("*BCD*", 0, true);
    Test("*BCD*", 4, true);

    void Test(string attempt, int n, bool expected)
    {
        Assert.AreEqual(expected, IsAlreadyMarkedGreen(attempt, n));
    }
}

[TestMethod]
public void TestSetAttemptIfYellow()
{
    Test("ABCDE", "EABCD", 0, "+BCDE");
    Test("ABCDE", "EABCD", 4, "ABCD+");
    Test("ABCDE", "BAAAA", 0, "+BCDE");
    Test("AAAAB", "EABBB", 4, "AAAA+");

    void Test(string attempt, string target, int charNo, string expected)
    {
        Assert.AreEqual(expected, SetAttemptIfYellow(attempt, target, charNo));
    }
}

[TestMethod]
public void TestSetTargetIfYellow()
{
    Test("ABCDE", "EABCD", 0, "E.BCD");
    Test("ABCDE", "EABCD", 4, ".ABCD");
    Test("ABCDE", "BAAAA", 0, "B.AAA");
    Test("AAAAB", "EABEA", 4, "EA.EA");
    Test("AAAAB", "EABBB", 4, "EA.BB");
    Test("*BCDE", "*BCDA", 4, "*BCDA");

    void Test(string attempt, string target, int charNo, string expected)
    {
        Assert.AreEqual(expected, SetTargetIfYellow(attempt, target, charNo));
    }
}

[TestMethod]
public void TestEvaluateYellows()
{
    Test("ABCDE", "XAXXX", ("+____", "X.XXX"));
    Test("ABCDE", "XXXXA", ("+____", "XXXX."));
    Test("ABCDE", "XXXXE", ("____+", "XXXX."));
    Test("ABCDE", "XAAXX", ("+____", "X.AXX"));
    Test("AACDE", "XAXXX", ("+____", "X.XXX"));
    Test("ABCDE", "BCDEA", ("+++++", "....."));

    void Test(string attempt, string target, (string, string) expected)
    {
        Assert.AreEqual(expected, EvaluateYellows(attempt, target));
    }
}

[TestMethod]
public void TestMarkAttempt()
{
    Test("ABCDE", "XXXXX", "_____");
    Test("ABCDE", "BCDEA", "+++++");
    Test("ABCDE", "ABCDE", "*****");
    Test("SAINT", "LADLE", "_*___");
    Test("IDEAL", "LADLE", "_++++");
    Test("CABAL", "RECAP", "+__*_");
    Test("CABAL", "RECAP", "+__*_");
    Test("COLON", "GLORY", "_++__");

    void Test(string attempt, string target, string expected)
    {
        Assert.AreEqual(expected, MarkAttempt(attempt, target));
    }
}

[TestMethod]
public void TestPossibleAnswersAfterAttempt()
{
    var prior = new List<string> { "ABCDE", "BCDEA", "CDEAB", "DEABC", "EABCD" };
    Test(prior, "AAAAA", "*____", "ABCDE");
    Test(prior, "AXXXX", "+____", "BCDEA", "CDEAB", "DEABC", "EABCD");
    Test(prior, "AXXBX", "+__+_", "BCDEA", "CDEAB", "EABCD");

    void Test(List<string> prior, string attempt, string mark, params string[] expected)
    {
        CollectionAssert.AreEqual(expected, PossibleAnswersAfterAttempt(prior, attempt, mark).ToList());
    }
}

[TestMethod]
public void TestWordCountRemainingAfterAttempt()
{
    var prior = new List<string> { "ABCDE", "BCDEA", "CDEAB", "DEABC", "EABCD" };
    Test(prior, "AAAAA", 1);
    Test(prior, "AXXXX", 4);
    Test(prior, "XXXXX", 5);

    void Test(List<string> prior, string attempt, int expected)
    {
        Assert.AreEqual(expected, WordCountRemainingAfterAttempt(prior, attempt));
    }
}

[TestMethod]
public void TestAllRemainingWordCounts()
{
    var possAnswers = new List<string> { "AAAAA", "BBBBB", "CCCCC", "DDDDD" };
    var possAttempts = new List<string> { "ABABA", "BCBCB", "ABCBC" };
    var expected = new List<(string word, int count)> { ("ABABA", 2), ("BCBCB", 2), ("ABCBC", 1) };
    Test(possAnswers, possAttempts, expected);

    void Test(List<string> possAnswers, List<string> possAttempts, List<(string word, int count)> expected)
    {
        CollectionAssert.AreEqual(expected, AllRemainingWordCounts(possAnswers, possAttempts).ToList());
    }
}

[TestMethod]
public void TestBetterOf()
{
    var possAnswers = new List<string> { };
    Test(("A", 3), ("B", 2), possAnswers, "B");
    Test(("B", 2), ("A", 3), possAnswers, "B");
    Test(("B", 2), ("A", 2), possAnswers, "B");
    Test(("A", 2), ("B", 2), possAnswers, "A");
    possAnswers = new List<string> { "B" };
    Test(("A", 2), ("B", 2), possAnswers, "B");
    possAnswers = new List<string> { "B", "A" };
    Test(("A", 2), ("B", 2), possAnswers, "B");
    Test(("B", 2), ("A", 2), possAnswers, "A");

    void Test((string word, int count) word1, (string word, int count) word2, List<string> possAnswers, string expected)
    {
        Assert.AreEqual(expected, BetterOf(word1, word2, possAnswers).word);
    }
}

[TestMethod]
public void TestBestAttempt()
{
    var possAnswers = new List<string> { "ABCDE", "ABBBB", "EDCBA" };
    var possAttempts = new List<string> { "AAAAA", "BBBBB", "CCCCC", "DDDDD", "EEEEE", "EDCBA", "DEABC" };
    Test(possAnswers, possAttempts, "EDCBA");
    possAnswers = new List<string> { "ABCDE", "ABBBB", "BCDEA" };
    possAttempts = new List<string> { "AAAAA", "BBBBB", "CCCCC", "DDDDD", "EEEEE", "EDCBA", "DEABC" };
    Test(possAnswers, possAttempts, "BBBBB");

    void Test(List<string> possAnswers, List<string> possAttempts, string expected)
    {
        Assert.AreEqual(expected, BestAttempt(possAnswers, possAttempts));
    }
}
    }
