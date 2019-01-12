using System;

namespace PDMapEditor
{
    public class ActionCreate : SavedAction
    {
        private IElement createdElement;

        public ActionCreate(IElement createdElement) : base()
        {
            this.createdElement = createdElement;
            this.description = "Created " + createdElement.TypeName.ToLower();
            Program.main.labelActionStatus.Text = description;
        }

        protected override void Do(bool redo = false)
        {
            if (redo)
            {
                createdElement = (IElement)createdElement.Copy();

                Program.main.labelActionStatus.Text = "Redone \"" + description + "\"";
            }

            Selection.ClearSelection();
        }

        protected override void Undo()
        {
            if (createdElement != null)
                createdElement.Destroy();

            Program.main.labelActionStatus.Text = "Undone \"" + description + "\"";
        }
    }
}
