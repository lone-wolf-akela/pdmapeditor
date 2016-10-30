using System.Collections.Generic;
using System.Windows.Forms;

namespace PDMapEditor
{
    class ActionKey
    {
        //---------------------------------- STATIC PART ---------------------------------//
        public static Dictionary<Action, ActionKey> ActionKeys = new Dictionary<Action, ActionKey>();
        public static HashSet<Keys> pressedKeys = new HashSet<Keys>(); //Has to be a HashSet (or List) because of bugs with bitwise using of the Keys enum
        public static bool controlActive;
        public static bool altActive;

        public static void Init()
        {
            //Initialize hotkeys with default values
            new ActionKey("Toggle orthographic", Action.TOGGLE_ORTHOGRAPHIC, Keys.NumPad5);

            new ActionKey("Front view", Action.VIEW_FRONT, Keys.NumPad1);
            new ActionKey("Back view", Action.VIEW_BACK, Keys.NumPad1, true);

            new ActionKey("Left view", Action.VIEW_LEFT, Keys.NumPad3);
            new ActionKey("Right view", Action.VIEW_RIGHT, Keys.NumPad3, true);

            new ActionKey("Top view", Action.VIEW_TOP, Keys.NumPad7);
            new ActionKey("Bottom view", Action.VIEW_BOTTOM, Keys.NumPad7, true);

            new ActionKey("Reset camera", Action.CAM_RESET, Keys.R);

            new ActionKey("Move forwards", Action.PAN_FORWARDS, Keys.W);
            new ActionKey("Move left", Action.PAN_LEFT, Keys.A);
            new ActionKey("Move backwards", Action.PAN_BACKWARDS, Keys.S);
            new ActionKey("Move right", Action.PAN_RIGHT, Keys.D);
            new ActionKey("Move up", Action.PAN_UP, Keys.E);
            new ActionKey("Move down", Action.PAN_DOWN, Keys.Q);

            new ActionKey("Translation mode", Action.MODE_TRANSLATION, Keys.T);
            new ActionKey("Rotation mode", Action.MODE_ROTATION, Keys.Z);

            new ActionKey("Focus selection", Action.CAM_FOCUS_SELECTION, Keys.F);
            new ActionKey("Add to selection", Action.SELECTION_ADD, Keys.ControlKey, true);

            new ActionKey("Delete selection", Action.SELECTION_DELETE, Keys.Delete);

            new ActionKey("Copy selection", Action.SELECTION_COPY, Keys.C, true);
            new ActionKey("Paste copied", Action.SELECTION_PASTE, Keys.V, true);
        }

        public static void KeyDown(PreviewKeyDownEventArgs e)
        {
            pressedKeys.Add(e.KeyCode);

            if (e.KeyCode == Keys.Menu)
                altActive = true;

            if (e.KeyCode == Keys.ControlKey)
                controlActive = true;
        }

        public static void KeyUp(KeyEventArgs e)
        {
            pressedKeys.Remove(e.KeyCode);

            if (e.KeyCode == Keys.Menu)
                altActive = false;

            if (e.KeyCode == Keys.ControlKey)
                controlActive = false;
        }

        public static void LostFocus()
        {
            pressedKeys.Clear();
            controlActive = false;
            altActive = false;
        }

        public static bool IsDown(Action action)
        {
            if (ActionKeys[action].IsDown())
                return true;

            return false;
        }

        //---------------------------------- INSTANCE PART ---------------------------------//
        public string Name;
        public Action Action;
        public Keys Key;
        public bool Control;
        public bool Alt;

        //GUI
        public Label Label;
        public CheckBox CheckCTRL;
        public CheckBox CheckALT;
        public Button Button;

        public ActionKey(string name, Action action, Keys key, bool control = false, bool alt = false)
        {
            Name = name;
            Action = action;
            Key = key;
            Control = control;
            Alt = alt;

            ActionKeys.Add(action, this);
        }

        public bool IsDown()
        {
            if (pressedKeys.Contains(Key))
                if (Alt == altActive && Control == controlActive)
                    return true;

            return false;
        }
    }

    public enum Action
    {
        TOGGLE_ORTHOGRAPHIC,
        VIEW_FRONT,
        VIEW_LEFT,
        VIEW_TOP,
        VIEW_BACK,
        VIEW_RIGHT,
        VIEW_BOTTOM,
        CAM_RESET,
        PAN_FORWARDS,
        PAN_LEFT,
        PAN_BACKWARDS,
        PAN_RIGHT,
        PAN_UP,
        PAN_DOWN,
        MODE_TRANSLATION,
        MODE_ROTATION,
        CAM_FOCUS_SELECTION,
        SELECTION_ADD,
        SELECTION_DELETE,
        SELECTION_COPY,
        SELECTION_PASTE,
    }
}
