using OpenTK;
using System;

namespace PDMapEditor
{
    public class ActionRotate : SavedAction
    {
        private IElement[] rotatedElements;
        Vector3 diff;

        public ActionRotate(IElement[] rotatedElements, Vector3 diff) : base()
        {
            this.rotatedElements = rotatedElements;
            this.diff = diff;

            string elementWord = "elements";
            if (rotatedElements.Length == 1)
                elementWord = "element";
            description = "Rotated " + rotatedElements.Length + " " + elementWord;
            Program.main.labelActionStatus.Text = description;
        }

        protected override void Do(bool redo = false)
        {
            if (redo)
            {
                foreach (IElement element in rotatedElements)
                    if(element != null)
                        element.Rotation += diff;

                Program.main.labelActionStatus.Text = "Redone \"" + description + "\"";

                Selection.SelectElements(rotatedElements);
            }
        }

        protected override void Undo()
        {
            foreach (IElement element in rotatedElements)
                if (element != null)
                    element.Rotation -= diff;

            Selection.SelectElements(rotatedElements);
            Program.main.labelActionStatus.Text = "Undone \"" + description + "\"";
        }
    }
}
