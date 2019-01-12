using System;
using System.ComponentModel;

namespace PDMapEditor
{
    public class ActionPropertyChange : SavedAction
    {
        private readonly IElement[] elements;
        PropertyDescriptor property;
        readonly object oldValue;
        readonly object newValue;

        public ActionPropertyChange(IElement[] elements, PropertyDescriptor property, object oldValue, object newValue) : base()
        {
            this.elements = elements;
            this.property = property;
            this.oldValue = oldValue;
            this.newValue = newValue;

            string elementWord = "elements";
            if (elements.Length == 1)
                elementWord = "element";
            this.description = "Changed " + property.Name + " of " + elements.Length + " " + elementWord + " from " + oldValue.ToString() + " to " + newValue.ToString();
            Program.main.labelActionStatus.Text = description;
        }

        protected override void Do(bool redo = false)
        {
            if (redo)
            {
                foreach(IElement element in elements)
                    property.SetValue(element, newValue);
                Selection.InvalidateSelectionGUI();

                Program.main.labelActionStatus.Text = "Redone \"" + description + "\"";
            }
        }

        protected override void Undo()
        {
            foreach (IElement element in elements)
                property.SetValue(element, oldValue);
            Selection.InvalidateSelectionGUI();

            Program.main.labelActionStatus.Text = "Undone \"" + description + "\"";
        }
    }
}
