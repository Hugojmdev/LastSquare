using UnityEngine;

public static class Layers {
    private const string DEFAULT = "Default";
    private const string PLAYER = "Player";
    private const string ENEMIES = "Enemies";
    private const string GROUND = "Ground";
    private const string SPIKES = "SpikeS";
    private const string SHIELD = "Shield";
    private const string ABILITIES = "Abilities";
    private const string EXPLOSIVE = "Explosive";
    private const string DESTRUCTIBLE = "Destructible";

    public static readonly LayerMask PLAYER_LAYER = LayerMask.GetMask(PLAYER);
    public static readonly LayerMask GROUNDED_LAYERS = LayerMask.GetMask(GROUND, SPIKES, EXPLOSIVE);
    public static readonly LayerMask ABILITY_SPAWNER_LAYERS = LayerMask.GetMask(GROUND, SPIKES, EXPLOSIVE, ABILITIES);
    public static readonly LayerMask EXPLOSION_LAYERS = LayerMask.GetMask(DESTRUCTIBLE);

}