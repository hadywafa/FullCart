using Domain.Common;
using Domain.EFModels;

namespace Domain.Events
{
    public class ImageCreatedEvent : DomainEvent
    {
        public ImageCreatedEvent(Image image)
        {
            Image = image;
        }

        public Image Image { get; }
    }
}