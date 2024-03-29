using System.Collections.Generic;

public static class Tag{
    public const string GROUND = "Ground";
    public const string SPIKE = "Spike";
    public const string SQUARE = "Square";
    public const string HALF_SQUARE = "HalfSquare";
    public const string GOLDEN_SQUARE = "GoldenSquare";

    public static readonly List<string> JUMP_TAGS = new() { GROUND, SQUARE };
}