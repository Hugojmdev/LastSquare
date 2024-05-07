using System.Collections.Generic;
using System.Linq;

public static class Tag {

    public const string PLAYER = "Player";
    public const string GROUND = "Ground";
    public const string SPIKE = "Spike";
    public const string SQUARE_GROUND = "SquareGround";
    public const string SQUARE_SHIELD = "SquareShield";
    public const string SQUARE_EXPLOSIVE = "SquareExplosive";
    public const string HALF_SQUARE = "HalfSquare";
    public const string GOLDEN_SQUARE = "GoldenSquare";
    public const string ENEMY = "Enemy";
    public const string CAMERA_LIMIT = "CameraLimit";
    public const string DESTRUCTIBLE = "Destructible";

    public static readonly List<string> SQUARE_TAGS = new() {SQUARE_GROUND, SQUARE_SHIELD, SQUARE_EXPLOSIVE};
    public static readonly List<string> GROUNDED_TAGS = new() {GROUND, ENEMY, SQUARE_GROUND}; //.Concat(SQUARE_TAGS).ToList();
}