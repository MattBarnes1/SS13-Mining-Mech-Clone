using Microsoft.Xna.Framework;
using Nez.UI;
using NEZ_ENGINE_CUSTOM.UI.DragAndDropSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEZ_ENGINE_CUSTOM.Input
{
    internal static class DragAndDropSystem
    {
        public static Image myDraggedItem = new Image();
        public static IDraggableElement DraggedElement { get; private set; }
        internal static void StartDrag(Vector2 Position, Stage myStageImage, IDraggableElement myElement)
        {
            aStage?.getElements().Remove(myDraggedItem);
            aStage = myStageImage;
            DraggedElement?.OnDragReset();
            DraggedElement = myElement;
            myElement.OnDragStart();
            if (!myStageImage.getElements().Contains(myDraggedItem))
            {
                myStageImage.addElement(myDraggedItem);
            }
            myDraggedItem.setDrawable(DraggedElement.GetGraphic());
            myDraggedItem.setPosition(Position.X, Position.Y);
        }
        internal static Stage aStage;
        internal static void UpdateDrag( Vector2 Position)
        {
            if (DraggedElement == null) return;
            myDraggedItem.setDrawable(DraggedElement.GetGraphic());
            myDraggedItem.setPosition(Position.X, Position.Y);
        }

        internal static void AcceptElement(IDropTarget myResult)
        {
           if (DraggedElement == null) return;
           var myItem = DraggedElement.GetDraggedItem();
           myResult.Accept(myItem);
           DraggedElement?.OnDragEnd(true);
           DraggedElement = null;
           aStage?.getElements().Remove(myDraggedItem);
        }

        internal static void ResetDragged()
        {
            DraggedElement?.OnDragReset();
            aStage?.getElements().Remove(myDraggedItem);
            myDraggedItem.setDrawable(null);
            DraggedElement = null;
        }
    }
}
