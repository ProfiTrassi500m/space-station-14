[RegisterComponent]
public sealed class WeaponAttachmentHolderComponent : Component
{
    [DataField("slots")]
    public List<AttachmentSlot> Slots = new();
}

[DataDefinition]
public sealed class AttachmentSlot
{
    [DataField("slotType")]
    public string SlotType = "";

    [DataField("attachedEntity")]
    public EntityUid? AttachedEntity;
}