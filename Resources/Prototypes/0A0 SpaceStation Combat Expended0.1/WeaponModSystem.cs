public sealed class WeaponModSystem : EntitySystem
{
    public override void Initialize()
    {
        SubscribeLocalEvent<GunComponent, InteractUsingEvent>(OnAttachMod);
    }

    private void OnAttachMod(EntityUid uid, GunComponent gun, InteractUsingEvent args)
    {
        // Логика присоединения модулей
    }
}