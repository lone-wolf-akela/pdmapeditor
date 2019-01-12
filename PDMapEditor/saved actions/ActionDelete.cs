using System;

namespace PDMapEditor
{
    public class ActionDelete : SavedAction
    {
        private IElement[] deletedElements = new IElement[0];
        private IElement[] recreatedElements = new IElement[0];

        public ActionDelete(IElement[] elements) : base()
        {
            deletedElements = elements;

            string elementWord = "elements";

            if (deletedElements.Length == 1)
                elementWord = "element";

            description = "Deleted " + deletedElements.Length + " " + elementWord;

            Do();
        }

        protected override void Do(bool redo = false)
        {
            if (!redo)
            {
                foreach (IElement element in deletedElements)
                    if (element != null)
                        element.Destroy();

                Program.main.labelActionStatus.Text = description;
            }
            else
            {
                foreach (IElement element in recreatedElements)
                    if (element != null)
                        element.Destroy();

                recreatedElements = new IElement[0];

                Program.main.labelActionStatus.Text = "Redone \"" + description + "\"";
            }

            Selection.ClearSelection();
        }

        protected override void Undo()
        {
            recreatedElements = new IElement[deletedElements.Length];

            for (int i = 0; i < deletedElements.Length; i++)
                recreatedElements[i] = (IElement)deletedElements[i].Copy();

            Selection.SelectElements(recreatedElements);
            Program.main.labelActionStatus.Text = "Undone \"" + description + "\"";
        }
    }
}
