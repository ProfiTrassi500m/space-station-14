using Robust.Shared.GameObjects;
using Robust.Shared.Serialization.Manager.Attributes;
using System.Collections.Generic;

namespace Content.Shared.Weapons
{
    [RegisterComponent]
    public sealed class WeaponSlotsComponent : Component
    {
        public override string Name => "WeaponSlots";

        [DataField("availableSlots")]
        public Dictionary<string, SlotDefinition> AvailableSlots = new()
        {
            ["top_rail"] = new() { Name = "Верхняя планка", MaxItems = 1 },
            ["barrel"] = new() { Name = "Дульный слот", MaxItems = 1 },
            ["underbarrel"] = new() { Name = "Подствольный слот", MaxItems = 1 },
            ["magazine_well"] = new() { Name = "Приёмник магазина", MaxItems = 1 }
        };

        [ViewVariables]
        public Dictionary<string, EntityUid> InstalledModules = new();
    }

    [DataDefinition]
    public sealed class SlotDefinition
    {
        [DataField("name")]
        public string Name { get; } = string.Empty;

        [DataField("maxItems")]
        public int MaxItems { get; } = 1;

        [DataField("whitelist")]
        public List<string>? Whitelist { get; }  // Типы разрешённых модулей
    }
}