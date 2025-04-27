using Robust.Shared.GameObjects;
using Robust.Shared.IoC;

namespace Content.Server.Weapons.Attachments
{
    public sealed class WeaponAttachmentSystem : EntitySystem
    {
        [Dependency] private readonly IComponentFactory _componentFactory = default!;

        public override void Initialize()
        {
            SubscribeLocalEvent<WeaponAttachmentHolderComponent, InteractUsingEvent>(OnInteractUsing);
        }

        private void OnInteractUsing(EntityUid uid, WeaponAttachmentHolderComponent holder, InteractUsingEvent args)
        {
            if (!TryComp<WeaponAttachmentComponent>(args.Used, out var attachment))
                return;

            foreach (var slot in holder.Slots)
            {
                if (slot.SlotType == attachment.SlotType && slot.AttachedEntity == null)
                {
                    AttachToSlot(uid, slot, args.Used, attachment);
                    args.Handled = true;
                    return;
                }
            }
        }

        private void AttachToSlot(EntityUid holderUid, AttachmentSlot slot, EntityUid attachmentUid, 
            WeaponAttachmentComponent attachment)
        {
            slot.AttachedEntity = attachmentUid;
            
            // Применяем модификаторы к оружию
            if (TryComp<GunComponent>(holderUid, out var gun))
            {
                gun.FireRate *= attachment.FireRateModifier;
                // Аналогично для других параметров
            }
            
            // Визуальное обновление
            if (TryComp<SpriteComponent>(holderUid, out var sprite))
            {
                // Добавляем слой с прицелом/глушителем
            }
        }
    }
}