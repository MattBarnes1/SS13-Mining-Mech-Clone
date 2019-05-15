using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEZ_ENGINE_CUSTOM.UI.DragAndDropSystem
{
    public interface IDropTarget
    {
        bool CanAccept(IDraggableElement myOrigin);
        bool Accept(object myOrigin);
    }
}
