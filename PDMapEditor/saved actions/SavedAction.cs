using System.Collections.Generic;
using System.Media;

namespace PDMapEditor
{
    //This class handles undo/redo actions
    public abstract class SavedAction
    {
        //----------------------- STATIC -----------------------//
        private static List<SavedAction> Saved = new List<SavedAction>();
        private static List<SavedAction> Undone = new List<SavedAction>();

        public static void KeyDown()
        {
            if (ActionKey.IsDown(Action.UNDO))
                UndoLast();
            else if (ActionKey.IsDown(Action.REDO))
                RedoLast();
        }

        public static void UndoLast()
        {
            if (Saved.Count > 0)
            {
                SavedAction action = Saved[Saved.Count - 1];
                Saved.RemoveAt(Saved.Count - 1);
                action.Undo();
                Undone.Add(action);
            }
            else
                SystemSounds.Beep.Play();
        }

        public static void RedoLast()
        {
            if (Undone.Count > 0)
            {
                SavedAction action = Undone[Undone.Count - 1];
                Undone.RemoveAt(Undone.Count - 1);
                action.Do(true);
                Saved.Add(action);
            }
            else
                SystemSounds.Beep.Play();
        }

        public static void ClearHistory()
        {
            Saved.Clear();
            Undone.Clear();
            Program.main.labelActionStatus.Text = "No last action.";
        }

        //---------------------- INSTANCE ----------------------//
        protected string description;

        public SavedAction()
        {
            Saved.Add(this);
        }

        protected abstract void Do(bool redo = true);
        protected abstract void Undo();
    }
}
