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
            ["top_rail"] = new() { 
                Name = "Picatinny Rail", 
                MaxItems = 1,
                Whitelist = new() { "sight", "optic" }
            },
            ["barrel"] = new() { 
                Name = "Barrel Attachment",
                MaxItems = 1,
                Whitelist = new() { "suppressor", "flashhider" }
            },
            ["underbarrel"] = new() { 
                Name = "Underbarrel Rail",
                MaxItems = 1,
                Whitelist = new() { "grip", "laser" }
            },
            ["magazine_well"] = new() { 
                Name = "Magazine Well",
                MaxItems = 1,
                Whitelist = new() { "magazine" }
            }
        };

        [ViewVariables]
        public Dictionary<string, EntityUid?> InstalledModules = new();
    }

    [DataDefinition]
    public sealed class SlotDefinition
    {
        [DataField("name")]
        public string Name { get; } = "Weapon Slot";
        
        [DataField("maxItems")]
        public int MaxItems { get; } = 1;
        
        [DataField("whitelist")]
        public List<string>? Whitelist { get; }
        
        [DataField("blacklist")]
        public List<string>? Blacklist { get; }
    }
}