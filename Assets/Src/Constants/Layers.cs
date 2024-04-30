using UnityEngine;

public static class Layers {
    public const string DEFAULT = "Default";
    public const string PLAYER = "Player";
    public const string ENEMIES = "Enemies";
    public const string GROUND = "Ground";
    public const string SPIKES = "SpikeS";
    public const string SHIELD = "Shield";

    public static readonly LayerMask GROUNDED_LAYERS = LayerMask.GetMask(GROUND, SPIKES);

}