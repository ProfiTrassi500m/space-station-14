using Content.Shared.Weapons;
using Robust.Shared.GameObjects;
using Robust.Shared.Containers;

namespace Content.Server.Weapons.Module
{
    public sealed class WeaponModuleSystem : EntitySystem
    {
        [Dependency] private readonly SharedContainerSystem _containers = default!;

        public override void Initialize()
        {
            SubscribeLocalEvent<WeaponSlotsComponent, MapInitEvent>(OnMapInit);
        }

        private void OnMapInit(EntityUid uid, WeaponSlotsComponent component, MapInitEvent args)
        {
            foreach (var slot in component.AvailableSlots)
            {
                component.InstalledModules[slot.Key] = EntityUid.Invalid;
                _containers.EnsureContainer<ContainerSlot>(uid, $"slot_{slot.Key}");
            }
        }

        public bool TryInstallModule(EntityUid weapon, EntityUid module, string slotId)
        {
            if (!TryComp<WeaponSlotsComponent>(weapon, out var weaponSlots))
                return false;

            if (!weaponSlots.AvailableSlots.TryGetValue(slotId, out var slotDef))
                return false;

            var container = _containers.GetContainer(weapon, $"slot_{slotId}");
            if (container.ContainedEntity != null)
                return false;

            container.Insert(module);
            weaponSlots.InstalledModules[slotId] = module;
            UpdateWeaponStats(weapon);
            return true;
        }

        private void UpdateWeaponStats(EntityUid weapon)
        {
            // Здесь будет логика пересчёта характеристик оружия
            // на основе установленных модулей
        }
    }
}