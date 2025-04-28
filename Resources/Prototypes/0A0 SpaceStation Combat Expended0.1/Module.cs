using Content.Server.Weapons.Module;
using Content.Shared.Weapons.Module;
using Robust.Shared.GameObjects;

namespace Content.Server.Weapons.Module
{
    [Module]  // Указывает, что это модуль SS14
    public sealed class WeaponModuleModule : SharedWeaponModuleModule
    {
        public override void Initialize()
        {
            base.Initialize();
            
            // Регистрация компонента
            RegisterComponent<WeaponModuleComponent>();
            RegisterComponent<MagazineComponent>();  
            RegisterComponent<WeaponSlotsComponent>();
            RegisterSystem<WeaponModuleSystem>();
        }
    }
}
