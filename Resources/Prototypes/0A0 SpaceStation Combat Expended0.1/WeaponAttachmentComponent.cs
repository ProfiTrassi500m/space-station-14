[RegisterComponent]
public sealed class WeaponAttachmentComponent : Component
{
    [DataField("slotType")]
    public string SlotType = "top_rail";

    [DataField("accuracyModifier")]
    public float AccuracyModifier = 1f;

    [DataField("damageModifier")]
    public float DamageModifier = 1f;

    // Другие модификаторы...
}