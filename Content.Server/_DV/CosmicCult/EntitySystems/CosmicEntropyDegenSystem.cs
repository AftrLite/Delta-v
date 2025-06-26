using Content.Shared._DV.CosmicCult.Components;
using Robust.Shared.Timing;
using Content.Shared.Damage;
using Robust.Shared.Random;

namespace Content.Server._DV.CosmicCult.EntitySystems;

/// <summary>
/// Makes the person with this component take damage over time.
/// Used for status effect.
/// </summary>
public sealed partial class CosmicEntropyDegenSystem : EntitySystem
{
    [Dependency] private readonly IGameTiming _timing = default!;
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly DamageableSystem _damageable = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<CosmicEntropyDebuffComponent, ComponentStartup>(OnInit);
    }

    private void OnInit(EntityUid uid, CosmicEntropyDebuffComponent comp, ref ComponentStartup args)
    {
        var degen = new HashSet<DamageSpecifier> { comp.Pool1, comp.Pool2, comp.Pool3 };

        _damageable.TryChangeDamage(uid, _random.Pick(degen), true, false);
        comp.CheckTimer = _timing.CurTime + comp.CheckWait;
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var query = EntityQueryEnumerator<CosmicEntropyDebuffComponent>();
        while (query.MoveNext(out var uid, out var comp))
        {
            if (_timing.CurTime < comp.CheckTimer)
                continue;
            var degen = new HashSet<DamageSpecifier> { comp.Pool1, comp.Pool2, comp.Pool3 };
            comp.CheckTimer = _timing.CurTime + comp.CheckWait;
            _damageable.TryChangeDamage(uid, _random.Pick(degen), true, false);
        }
    }
}
