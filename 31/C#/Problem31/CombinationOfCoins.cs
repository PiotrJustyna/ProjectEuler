public class CombinationOfCoins
{
    public CombinationOfCoins(
        int numberOfOnePences,
        int numberOfTwoPences,
        int numberOfFivePences,
        int numberOfTenPences,
        int numberOfTwentyPences,
        int numberOfFiftyPences,
        int numberOfOnePounds,
        int numberOfTwoPounds)
    {
        NumberOfOnePences = numberOfOnePences;
        NumberOfTwoPences = numberOfTwoPences;
        NumberOfFivePences = numberOfFivePences;
        NumberOfTenPences = numberOfTenPences;
        NumberOfTwentyPences = numberOfTwentyPences;
        NumberOfFiftyPences = numberOfFiftyPences;
        NumberOfOnePounds = numberOfOnePounds;
        NumberOfTwoPounds = numberOfTwoPounds;
    }

    public int NumberOfOnePences { get; }

    public int NumberOfTwoPences { get; }

    public int NumberOfFivePences { get; }

    public int NumberOfTenPences { get; }

    public int NumberOfTwentyPences { get; }

    public int NumberOfFiftyPences { get; }

    public int NumberOfOnePounds { get; }

    public int NumberOfTwoPounds { get; }


    public override string ToString()
    {
        return $"{NumberOfOnePences}x1p + {NumberOfTwoPences}x2p + {NumberOfFivePences}x5p + {NumberOfTenPences}x10p + {NumberOfTwentyPences}x20p + {NumberOfFiftyPences}x50p + {NumberOfOnePounds}x£1 + {NumberOfTwoPounds}x£2";
    }
}