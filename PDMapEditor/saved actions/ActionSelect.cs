using OpenTK;
using System;

namespace PDMapEditor
{
    public class ActionSelect : SavedAction
    {
        private IElement[] oldSelection;
        private IElement[] newSelection;

        public ActionSelect(IElement[] oldSelection, IElement[] newSelection) : base()
        {
            this.oldSelection = oldSelection;
            this.newSelection = newSelection;

            if (newSelection.Length > 0)
            {
                string elementWord = "elements";
                if (newSelection.Length == 1)
                    elementWord = newSelection[0].TypeName.ToLower();
                description = "Selected " + newSelection.Length + " " + elementWord;
            }
            else
                description = "Cleared selection";

            Program.main.labelActionStatus.Text = description;

            Do();
        }

        protected override void Do(bool redo = false)
        {
            if (!redo)
            {
                Selection.Selected.Clear();
                foreach (IElement element in newSelection)
                    Selection.Selected.Add(element);

                Program.main.labelActionStatus.Text = description;
            }
            else
            {
                Selection.Selected.Clear();
                foreach (IElement element in newSelection)
                    Selection.Selected.Add(element);

                Program.main.labelActionStatus.Text = "Redone \"" + description + "\".";
            }
        }

        protected override void Undo()
        {
            Selection.Selected.Clear();
            foreach (IElement element in oldSelection)
                Selection.Selected.Add(element);

            Program.main.labelActionStatus.Text = "Undone \"" + description + "\".";
        }
    }
}
