using Robust.Shared.GameObjects;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Shared.Weapons
{
    [RegisterComponent]
    public sealed class MagazineComponent : Component
    {
        public override string Name => "Magazine";

        [DataField("caliber")]
        public string Caliber { get; } = string.Empty;

        [DataField("maxAmmo")]
        public int MaxAmmo { get; } = 30;

        [DataField("currentAmmo")]
        public int CurrentAmmo { get; set; } = 30;

        [DataField("compatibleWeapons")]
        public List<string> CompatibleWeapons { get; } = new();
    }
}