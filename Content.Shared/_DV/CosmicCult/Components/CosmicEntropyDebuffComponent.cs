using Content.Shared.Damage;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;

namespace Content.Shared._DV.CosmicCult.Components;

/// <summary>
/// Makes the target take damage over time.
/// Meant to be used in conjunction with statusEffectSystem.
/// </summary>
[RegisterComponent]
[AutoGenerateComponentPause]
public sealed partial class CosmicEntropyDebuffComponent : Component
{
    [DataField(customTypeSerializer: typeof(TimeOffsetSerializer))]
    [AutoPausedField]
    public TimeSpan CheckTimer = default!;

    [DataField]
    public TimeSpan CheckWait = TimeSpan.FromSeconds(1);


    [DataField]
    public DamageSpecifier Pool1 = new()
    {
        DamageDict = new()
        {
            { "Cold", 0.09},
            { "Asphyxiation", 0.69},
        }
    };

    [DataField]
    public DamageSpecifier Pool2 = new()
    {
        DamageDict = new()
        {
            { "Cold", 0.1},
            { "Asphyxiation", 0.75},
        }
    };

    [DataField]
    public DamageSpecifier Pool3 = new()
    {
        DamageDict = new()
        {
            { "Cold", 0.11},
            { "Asphyxiation", 0.81},
        }
    };
}
