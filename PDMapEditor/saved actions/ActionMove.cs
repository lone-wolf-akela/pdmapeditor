using OpenTK;
using System;

namespace PDMapEditor
{
    public class ActionMove : SavedAction
    {
        private IElement[] movedElements;
        Vector3 diff;

        public ActionMove(IElement[] movedElements, Vector3 diff) : base()
        {
            this.movedElements = movedElements;
            this.diff = diff;

            string elementWord = "elements";
            if (movedElements.Length == 1)
                elementWord = "element";
            description = "Moved " + movedElements.Length + " " + elementWord + ".";
            Program.main.labelActionStatus.Text = description;
        }

        protected override void Do(bool redo = false)
        {
            if (redo)
            {
                foreach (IElement element in movedElements)
                    if(element != null)
                        element.Position += diff;

                Program.main.labelActionStatus.Text = "Redone \"" + description + "\".";

                Selection.SelectElements(movedElements);
            }
        }

        protected override void Undo()
        {
            foreach (IElement element in movedElements)
                if (element != null)
                    element.Position -= diff;

            Selection.SelectElements(movedElements);
            Program.main.labelActionStatus.Text = "Undone \"" + description + "\".";
        }
    }
}
