using Robust.Shared.GameObjects;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Shared.Weapons
{
    [RegisterComponent]
    public sealed class GunComponent : Component
    {
        public override string Name => "Gun";

        [DataField("caliber")]
        public string Caliber { get; } = string.Empty;

        [DataField("baseFireRate")]
        public float BaseFireRate { get; } = 10f;

        [DataField("fireRate")]
        public float FireRate { get; set; }

        [DataField("baseDamage")]
        public float BaseDamage { get; } = 15f;

        [DataField("damage")]
        public float Damage { get; set; }

        [DataField("baseNoiseLevel")]
        public float BaseNoiseLevel { get; } = 1f;

        [DataField("noiseLevel")]
        public float NoiseLevel { get; set; }

        [DataField("ammoType")]
        public string AmmoType { get; } = string.Empty;
    }
}