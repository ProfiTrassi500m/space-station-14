using Robust.Shared.Containers;

namespace Content.Server.Weapons;

[RegisterComponent]
public sealed class WeaponModuleHolderComponent : Component
{
    [DataField("slots")]
    public Dictionary<string, ContainerSlot> Slots = new();
}

[RegisterComponent]
public sealed class WeaponModuleComponent : Component
{
    [DataField("slotType")]
    public string SlotType = "";
}

[RegisterComponent]
public sealed class GunModifierComponent : Component
{
    [DataField("accuracy")] public float Accuracy = 1f;
    [DataField("moveSpeed")] public float MoveSpeed = 1f;
    [DataField("zoom")] public float Zoom = 1f;
    [DataField("soundVolume")] public float SoundVolume = 1f;
    [DataField("damageMultiplier")] public float DamageMultiplier = 1f;
    [DataField("recoilMultiplier")] public float RecoilMultiplier = 1f;
}

public sealed class WeaponModuleSystem : EntitySystem
{
    [Dependency] private readonly SharedContainerSystem _container = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<WeaponModuleHolderComponent, ComponentInit>(OnInit);
        SubscribeLocalEvent<WeaponModuleHolderComponent, InteractUsingEvent>(OnInteract);
        SubscribeLocalEvent<GunModifierComponent, ComponentStartup>(OnModifierAdded);
    }

    private void OnInit(EntityUid uid, WeaponModuleHolderComponent comp, ComponentInit args)
    {
        foreach (var (slotId, _) in comp.Slots)
        {
            comp.Slots[slotId] = _container.EnsureContainer<ContainerSlot>(uid, $"module_slot_{slotId}");
        }
    }

    private void OnInteract(EntityUid uid, WeaponModuleHolderComponent holder, InteractUsingEvent args)
    {
        if (!TryComp<WeaponModuleComponent>(args.Used, out var module))
            return;

        if (holder.Slots.TryGetValue(module.SlotType, out var container) && container.ContainedEntity == null)
        {
            container.Insert(args.Used);
            args.Handled = true;
        }
    }

    private void OnModifierAdded(EntityUid uid, GunModifierComponent mod, ComponentStartup args)
    {
        if (!TryComp<GunComponent>(uid, out var gun))
            return;

        ApplyModifiers(gun, mod);
    }

    private void ApplyModifiers(GunComponent gun, GunModifierComponent mod)
    {
        gun.FireRate *= mod.Accuracy;
        gun.Recoil *= mod.RecoilMultiplier;
        // Другие модификации...
    }
}

// Добавьте в WeaponModuleSystem
private void OnMagazineInsert(EntityUid uid, GunComponent gun, ContainerModifiedMessage args)
{
    if (args.Container.ID == gun.MagazineSlot)
    {
        UpdateAmmoCount(uid, gun);
    }
}

private void UpdateAmmoCount(EntityUid uid, GunComponent gun)
{
    if (!_container.TryGetContainer(uid, gun.MagazineSlot, out var container) 
        || container.ContainedEntity == null)
    {
        gun.CurrentAmmo = 0;
        return;
    }

    if (TryComp<AmmoComponent>(container.ContainedEntity, out var ammo))
    {
        gun.CurrentAmmo = ammo.Count;
        gun.MaxAmmo = ammo.Max;
    }
}