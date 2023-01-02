

public class Tests
{
    [Test]
    public void TestSet()
    {
        Assert.That(Set("ABCDE", 0, '_'), Is.EqualTo("_BCDE"));
        Assert.That(Set("ABCDE", 4, '_'), Is.EqualTo("ABCD_"));
    }

    [Test]
    public void TestIsGreen()
    {
        Assert.That(IsGreen("ABCDE", "A____", 0), Is.True);
        Assert.That(IsGreen("ABCDE", "____E", 4), Is.True);
        Assert.That(IsGreen("ABCDE", "_A___", 1), Is.False);
        Assert.That(IsGreen("BABBB", "B____", 1), Is.False);
    }

    [Test]
    public void TestIsAlreadyMarkedGreen()
    {
        Assert.That(IsAlreadyMarkedGreen("AB*D*", 2), Is.True);
        Assert.That(IsAlreadyMarkedGreen("AB*D*", 3), Is.False);
        Assert.That(IsAlreadyMarkedGreen("AB*D*", 4), Is.True);

    }

    [Test]
    public void TestIsYellow()
    {
        Assert.That(IsYellow("ABCDE", "____A", 0), Is.True);
        Assert.That(IsYellow("ABCDE", "____A", 4), Is.False);
        Assert.That(IsYellow("ABCDE", "___AA", 0), Is.True);
        Assert.That(IsYellow("AACDE", "_A___", 1), Is.True);
        Assert.That(IsYellow("AACDE", "__A__", 1), Is.True);
    }

    [Test]
    public void TestSetAttemptIfGreen()
    {
        Assert.That(SetAttemptIfGreen("ABCDE", "ABCDE", 0), Is.EqualTo("*BCDE"));
        Assert.That(SetAttemptIfGreen("ABCDE", "ABCDE", 4), Is.EqualTo("ABCD*"));
        Assert.That(SetAttemptIfGreen("BBCDE", "ABCDE", 0), Is.EqualTo("BBCDE"));
        Assert.That(SetAttemptIfGreen("ABCDE", "AACDE", 0), Is.EqualTo("*BCDE"));
    }

    [Test]
    public void TestSetTargetIfGreen()
    {
        Assert.That(SetTargetIfGreen("ABCDE", "ABCDE", 0), Is.EqualTo(".BCDE"));
        Assert.That(SetTargetIfGreen("ABCDE", "ABCDE", 4), Is.EqualTo("ABCD."));
        Assert.That(SetTargetIfGreen("BBCDE", "ABCDE", 0), Is.EqualTo("ABCDE"));
        Assert.That(SetTargetIfGreen("ABCDE", "AACDE", 0), Is.EqualTo(".ACDE"));
    }

    [Test]
    public void TestSetAttemptIfYellow()
    {
        Assert.That(SetAttemptIfYellow("ABCDE", "EABCD", 0), Is.EqualTo("+BCDE"));
        Assert.That(SetAttemptIfYellow("ABCDE", "EABCD", 4), Is.EqualTo("ABCD+"));
        Assert.That(SetAttemptIfYellow("ABCDE", "BAAAA", 0), Is.EqualTo("+BCDE"));
        Assert.That(SetAttemptIfYellow("AAAAB", "EABBB", 4), Is.EqualTo("AAAA+"));
    }

    [Test]
    public void TestSetTargetIfYellow()
    {
        Assert.That(SetTargetIfYellow("ABCDE", "EABCD", 0), Is.EqualTo("E.BCD"));
        Assert.That(SetTargetIfYellow("ABCDE", "EABCD", 4), Is.EqualTo(".ABCD"));
        Assert.That(SetTargetIfYellow("ABCDE", "BAAAA", 0), Is.EqualTo("B.AAA"));
        Assert.That(SetTargetIfYellow("AAAAB", "EABEA", 4), Is.EqualTo("EA.EA"));
        Assert.That(SetTargetIfYellow("AAAAB", "EABBB", 4), Is.EqualTo("EA.BB"));
        Assert.That(SetTargetIfYellow("*BCDE", "*BCDA", 4), Is.EqualTo("*BCDA"));
    }

    [Test]
    public void TestSetAllGreens()
    {
        Assert.That(EvaluateGreens("ABCDE", "AXXXX"), Is.EqualTo(("*BCDE", ".XXXX")));
        Assert.That(EvaluateGreens("ABCDE", "XXXXE"), Is.EqualTo(("ABCD*", "XXXX.")));
        Assert.That(EvaluateGreens("ABCDE", "ABCDE"), Is.EqualTo(("*****", ".....")));
        Assert.That(EvaluateGreens("AACDE", "AXXXX"), Is.EqualTo(("*ACDE", ".XXXX")));
        Assert.That(EvaluateGreens("ABCDE", "AAXXX"), Is.EqualTo(("*BCDE", ".AXXX")));
    }

    [Test]
    public void TestSetAllYellows()
    {
        Assert.That(EvaluateYellows("ABCDE", "XAXXX"), Is.EqualTo(("+____", "X.XXX")));
        Assert.That(EvaluateYellows("ABCDE", "XXXXA"), Is.EqualTo(("+____", "XXXX.")));
        Assert.That(EvaluateYellows("ABCDE", "XXXXE"), Is.EqualTo(("____+", "XXXX.")));
        Assert.That(EvaluateYellows("ABCDE", "XAAXX"), Is.EqualTo(("+____", "X.AXX")));
        Assert.That(EvaluateYellows("AACDE", "XAXXX"), Is.EqualTo(("+____", "X.XXX")));
        Assert.That(EvaluateYellows("ABCDE", "BCDEA"), Is.EqualTo(("+++++", ".....")));
    }

    [Test]
    public void TestMarkAttempt()
    {
        Assert.That(MarkAttempt("ABCDE", "XXXXX"), Is.EqualTo("_____"));
        Assert.That(MarkAttempt("ABCDE", "BCDEA"), Is.EqualTo("+++++"));
        Assert.That(MarkAttempt("ABCDE", "ABCDE"), Is.EqualTo("*****"));
        Assert.That(MarkAttempt("SAINT", "LADLE"), Is.EqualTo("_*___"));
        Assert.That(MarkAttempt("IDEAL", "LADLE"), Is.EqualTo("_++++"));
        Assert.That(MarkAttempt("CABAL", "RECAP"), Is.EqualTo("+__*_"));
        Assert.That(MarkAttempt("CABAL", "RECAP"), Is.EqualTo("+__*_"));
        Assert.That(MarkAttempt("COLON", "GLORY"), Is.EqualTo("_++__"));
    }

    [Test]
    public void TestPossibleAnswersAfterAttempt()
    {
        var prior = new List<string> { "SHANK", "SHAPE", "SHARD", "SHARE", "SHARK", "SHARP", "SHAVE", "SHAWL", "SHEAR", "SHEEN", "SHEEP", "SHEER" }.ToImmutableList();
        var expected = new List<string> { "SHEAR", "SHEER" }.ToImmutableList();
        Assert.That(PossibleAnswersAfterAttempt(prior, "SNIPE", "*___+"), Is.EqualTo(expected));
        expected = new List<string>().ToImmutableList();
        Assert.That(PossibleAnswersAfterAttempt(prior, "SNIPE", "_____"), Is.EqualTo(expected));
        Assert.That(PossibleAnswersAfterAttempt(prior, "SNIPE", "*****"), Is.EqualTo(expected));
        expected = new List<string>() { "SHAPE", "SHARD", "SHARE", "SHARK", "SHARP", "SHAVE", "SHEAR", "SHEEP", "SHEER" }.ToImmutableList();
        Assert.That(PossibleAnswersAfterAttempt(prior, "SHOWN", "**___"), Is.EqualTo(expected));
        expected = new List<string>() { "SHANK", "SHAPE", "SHARD", "SHARE", "SHARK", "SHARP", "SHAVE" }.ToImmutableList();
        Assert.That(PossibleAnswersAfterAttempt(prior, "SHALL", "***__"), Is.EqualTo(expected));
    }

    [Test]
    public void TestWordCountLeftByWorstOutcome()
    {
        var prior = new List<string> { "SHANK", "SHAPE", "SHARD", "SHARE", "SHARK", "SHARP", "SHAVE", "SHEAR", "SHEEN", "SHEEP", "SHEER" }.ToImmutableList();
        Assert.That(WordCountLeftByWorstOutcome(prior, "SMALL"), Is.EqualTo(7));
        Assert.That(WordCountLeftByWorstOutcome(prior, "QUILT"), Is.EqualTo(11));
        Assert.That(WordCountLeftByWorstOutcome(prior, "SHEER"), Is.EqualTo(3));
        prior = new List<string> { "SHANK", "SHAPE", "SHARD", "SHARP" }.ToImmutableList();
        Assert.That(WordCountLeftByWorstOutcome(prior, "SHARP"), Is.EqualTo(1));
    }

    [Test]
    public void TestBestAttempt()
    {
        var prior = new List<string> { "SHANK", "SHAPE", "SHARD", "SHARE", "SHARK", "SHARP", "SHAVE", "SHAWL", "SHEAR", "SHEEN", "SHEEP", "SHEER" }.ToImmutableList();
        Assert.That(BestAttempt(prior, prior), Is.EqualTo("SHEER"));
        prior = new List<string> { "SHANK", "SHAPE", "SHARD", "SHARP" }.ToImmutableList();
        Assert.That(BestAttempt(prior, prior), Is.EqualTo("SHARP"));
        prior = new List<string> { "SHAVE", "SHAWL" }.ToImmutableList();
        Assert.That(BestAttempt(prior, prior), Is.EqualTo("SHAWL"));
        prior = new List<string> { "SHAVE" }.ToImmutableList();
        Assert.That(BestAttempt(prior, prior), Is.EqualTo("SHAVE"));
    }
}