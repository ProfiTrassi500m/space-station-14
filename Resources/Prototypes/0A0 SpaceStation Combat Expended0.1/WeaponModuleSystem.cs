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
            SubscribeLocalEvent<WeaponSlotsComponent, ComponentInit>(OnInit);
            SubscribeLocalEvent<WeaponSlotsComponent, ContainerModifiedMessage>(OnSlotModified);
        }

        private void OnInit(EntityUid uid, WeaponSlotsComponent component, ComponentInit args)
        {
            InitializeSlots(uid, component);
        }

        public void InitializeSlots(EntityUid uid, WeaponSlotsComponent component)
        {
            foreach (var (slotId, _) in component.AvailableSlots)
            {
                component.InstalledModules[slotId] = null;
                _containers.EnsureContainer<ContainerSlot>(uid, $"slot_{slotId}");
            }
        }

        private void OnSlotModified(EntityUid uid, WeaponSlotsComponent component, ContainerModifiedMessage args)
        {
            UpdateWeaponStats(uid);
        }

        public bool TryInstallModule(EntityUid weapon, EntityUid module, string slotId)
        {
            if (!TryComp<WeaponSlotsComponent>(weapon, out var weaponSlots))
                return false;

            if (!weaponSlots.AvailableSlots.TryGetValue(slotId, out var slotDef))
                return false;

            if (!IsModuleCompatible(module, slotDef))
                return false;

            var container = _containers.GetContainer(weapon, $"slot_{slotId}");
            if (container.ContainedEntity != null)
                return false;

            container.Insert(module);
            weaponSlots.InstalledModules[slotId] = module;
            UpdateWeaponStats(weapon);
            return true;
        }

        private bool IsModuleCompatible(EntityUid module, SlotDefinition slotDef)
        {
            if (!TryComp<WeaponModuleComponent>(module, out var moduleComp))
                return false;

            // Проверка whitelist
            if (slotDef.Whitelist != null && !slotDef.Whitelist.Contains(moduleComp.ModuleType))
                return false;

            // Проверка blacklist
            if (slotDef.Blacklist != null && slotDef.Blacklist.Contains(moduleComp.ModuleType))
                return false;

            return true;
        }

        private void UpdateWeaponStats(EntityUid weapon)
        {
            if (!TryComp<GunComponent>(weapon, out var gun))
                return;

            // Сброс к базовым значениям
            gun.FireRate = gun.BaseFireRate;
            gun.Damage = gun.BaseDamage;
            gun.NoiseLevel = gun.BaseNoiseLevel;

            // Применение модификаторов от модулей
            foreach (var module in GetInstalledModules(weapon))
            {
                if (!TryComp<WeaponModuleComponent>(module, out var moduleComp))
                    continue;

                gun.Damage *= moduleComp.DamageModifier;
                gun.FireRate *= moduleComp.FireRateModifier;
                gun.NoiseLevel *= moduleComp.NoiseModifier;
            }
        }

        public IEnumerable<EntityUid> GetInstalledModules(EntityUid weapon)
        {
            if (!TryComp<WeaponSlotsComponent>(weapon, out var weaponSlots))
                yield break;

            foreach (var module in weaponSlots.InstalledModules.Values)
            {
                if (module != null)
                    yield return module.Value;
            }
        }
    }
}