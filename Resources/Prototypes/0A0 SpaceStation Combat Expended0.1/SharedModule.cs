using Robust.Shared.GameObjects;

namespace Content.Shared.Weapons.Module
{
    public abstract class SharedWeaponModuleModule : Module
    {
        public override void Initialize()
        {
            base.Initialize();
            
            // Регистрация компонента на клиенте и сервере
            RegisterComponent<WeaponModuleComponent>();
        }
    }
}