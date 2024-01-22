using Domain.Common;
using Domain.EFModels;

namespace Domain.Events
{
    public class ImageDeletedEvent : DomainEvent
    {
        public ImageDeletedEvent(Image image)
        {
            Image = image;
        }

        public Image Image { get; }
    }
}