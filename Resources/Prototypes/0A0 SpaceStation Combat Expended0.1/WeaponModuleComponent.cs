using Robust.Shared.GameObjects;
using Robust.Shared.Serialization.Manager.Attributes;
using Robust.Shared.ViewVariables;
using System.Collections.Generic;

namespace Content.Shared.Weapons.Module
{
    [RegisterComponent]
    public sealed class WeaponModuleComponent : Component
    {
        public override string Name => "WeaponModuleComponent";

        [DataField("moduleType")]
        public string ModuleType { get; } = "generic";

        // Модификаторы
        [DataField("accuracyModifier")]
        public float AccuracyModifier { get; } = 1.0f;

        [DataField("damageModifier")]
        public float DamageModifier { get; } = 1.0f;

        [DataField("noiseModifier")]
        public float NoiseModifier { get; } = 1.0f;

        // Совместимые слоты
        [DataField("compatibleSlots")]
        public List<string> CompatibleSlots { get; } = new();
    }
}