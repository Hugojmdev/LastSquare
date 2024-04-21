using System.Collections.Generic;
using System.Linq;

public static class Tag {

    public const string PLAYER = "Player";
    public const string GROUND = "Ground";
    public const string SPIKE = "Spike";
    public const string GROUND_SQUARE = "GroundSquare";
    public const string DAMAGE_SQUARE = "DamageSquare";
    public const string EXPLOSIVE_SQUARE = "ExplosiveSquare";
    public const string HALF_SQUARE = "HalfSquare";
    public const string GOLDEN_SQUARE = "GoldenSquare";
    public const string ENEMY = "Enemy";
    public const string CAMERA_LIMIT = "CameraLimit";

    public static readonly List<string> SQUARE_TAGS = new() {GROUND_SQUARE, DAMAGE_SQUARE, EXPLOSIVE_SQUARE};
    public static readonly List<string> GROUNDED_TAGS = new() {GROUND, ENEMY, GROUND_SQUARE}; //.Concat(SQUARE_TAGS).ToList();
}