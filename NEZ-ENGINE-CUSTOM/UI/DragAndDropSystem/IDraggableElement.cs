using Nez.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEZ_ENGINE_CUSTOM.UI.DragAndDropSystem
{
    public interface IDraggableElement
    {
        object GetDraggedItem();
        void OnDragStart();
        void OnDragEnd(bool wasAccepted);
        void OnDragReset();
        SubtextureDrawable GetGraphic();
    }
}
