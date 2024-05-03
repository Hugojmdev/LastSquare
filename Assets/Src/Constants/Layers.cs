using UnityEngine;

public static class Layers {
    private const string DEFAULT = "Default";
    private const string PLAYER = "Player";
    private const string ENEMIES = "Enemies";
    private const string GROUND = "Ground";
    private const string SPIKES = "SpikeS";
    private const string SHIELD = "Shield";
    private const string ABILITIES = "Abilities";

    public static readonly LayerMask PLAYER_LAYER = LayerMask.GetMask(PLAYER);
    public static readonly LayerMask GROUNDED_LAYERS = LayerMask.GetMask(GROUND, SPIKES);
    public static readonly LayerMask ABILITY_SPAWNER_LAYERS = LayerMask.GetMask(GROUND, SPIKES, ABILITIES);

}