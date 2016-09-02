using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PDMapEditor
{
    public partial class Hotkeys : Form
    {
        ActionKey selectedActionKey;

        public Hotkeys()
        {
            InitializeComponent();
        }

        public void Init()
        {
            int i = 0;
            foreach (ActionKey actionKey in ActionKey.ActionKeys.Values)
            {
                #region ControlCreation
                actionKey.Label = new Label();
                actionKey.Label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                actionKey.Label.Location = new System.Drawing.Point(12, i * 35 + 9);
                actionKey.Label.Name = actionKey.Name;
                actionKey.Label.Size = new System.Drawing.Size(155, 26);
                actionKey.Label.Text = actionKey.Name;
                actionKey.Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.Controls.Add(actionKey.Label);

                actionKey.CheckCTRL = new CheckBox();
                actionKey.CheckCTRL.Location = new System.Drawing.Point(173, i * 35 + 9);
                actionKey.CheckCTRL.Name = actionKey.Name;
                actionKey.CheckCTRL.Size = new System.Drawing.Size(55, 26);
                actionKey.CheckCTRL.Text = "CTRL";
                actionKey.CheckCTRL.UseVisualStyleBackColor = true;
                this.Controls.Add(actionKey.CheckCTRL);

                actionKey.CheckALT = new CheckBox();
                actionKey.CheckALT.Location = new System.Drawing.Point(234, i * 35 + 9);
                actionKey.CheckALT.Name = actionKey.Name;
                actionKey.CheckALT.Size = new System.Drawing.Size(55, 26);
                actionKey.CheckALT.Text = "ALT";
                actionKey.CheckALT.UseVisualStyleBackColor = true;
                this.Controls.Add(actionKey.CheckALT);

                actionKey.Button = new Button();
                actionKey.Button.Location = new System.Drawing.Point(290, i * 35 + 9);
                actionKey.Button.Name = actionKey.Name;
                actionKey.Button.Size = new System.Drawing.Size(170, 26);
                actionKey.Button.UseVisualStyleBackColor = true;
                actionKey.Button.Text = "None";
                this.Controls.Add(actionKey.Button);
                #endregion

                actionKey.Button.Text = actionKey.Key.ToString();
                if (actionKey.Control)
                    actionKey.CheckCTRL.Checked = true;

                if (actionKey.Alt)
                    actionKey.CheckALT.Checked = true;

                //Events
                actionKey.CheckCTRL.CheckedChanged += (sender, e) => ActionKeyCheckCTRLChecked(sender, e, actionKey);
                actionKey.CheckALT.CheckedChanged += (sender, e) => ActionKeyCheckALTChecked(sender, e, actionKey);
                actionKey.Button.Click += (sender, e) => ActionKeyButtonClick(sender, e, actionKey);
                actionKey.Button.LostFocus += (sender, e) => ActionKeyButtonLostFocus(sender, e, actionKey);

                i++;
            }
        }

        private void ActionKeyCheckCTRLChecked(object sender, EventArgs e, ActionKey actionKey)
        {
            actionKey.Control = actionKey.CheckCTRL.Checked;
        }

        private void ActionKeyCheckALTChecked(object sender, EventArgs e, ActionKey actionKey)
        {
            actionKey.Alt = actionKey.CheckALT.Checked;
        }

        private void ActionKeyButtonClick(object sender, EventArgs e, ActionKey actionKey)
        {
            selectedActionKey = actionKey;
            actionKey.Button.Text = "Press any key...";
        }

        private void ActionKeyButtonLostFocus(object sender, EventArgs e, ActionKey actionKey)
        {
            actionKey.Button.Text = actionKey.Key.ToString();
            selectedActionKey = null;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (selectedActionKey != null)
            {
                selectedActionKey.Key = keyData;
                selectedActionKey.Button.Text = selectedActionKey.Key.ToString();
                this.Focus();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        //------------------------------------------ HOTKEYS SAVING ----------------------------------------//
        public static void SaveHotkeys()
        {
            XElement hotkeys = new XElement("hotkeys");

            foreach (ActionKey actionKey in ActionKey.ActionKeys.Values)
            {
                hotkeys.Add(new XElement(actionKey.Action.ToString(), actionKey.Key + "|" + actionKey.Control + "|" + actionKey.Alt));
            }

            File.WriteAllText(Path.Combine(Program.EXECUTABLE_PATH, "hotkeys.xml"), hotkeys.ToString());
        }
        public static void LoadHotkeys()
        {
            if (!File.Exists(Path.Combine(Program.EXECUTABLE_PATH, "hotkeys.xml")))
            {
                Log.WriteLine("No hotkeys.xml found, using default values.");
                return;
            }

            try
            {
                string file = File.ReadAllText(Path.Combine(Program.EXECUTABLE_PATH, "hotkeys.xml"));
                XElement hotkeys = XElement.Parse(file);

                foreach (XElement element in hotkeys.Elements())
                {
                    Action action;
                    System.Enum.TryParse(element.Name.LocalName, out action);
                    if (action == 0)
                    {
                        Log.WriteLine("Failed to parse element \"" + element.Name.LocalName + "\" from hotkeys.xml.");
                        continue;
                    }

                    string[] splitted = element.Value.Split('|');
                    if (splitted.Length < 2)
                    {
                        Log.WriteLine("Failed to parse value \"" + element.Value + "\" from hotkeys.xml.");
                        continue;
                    }

                    Keys key;
                    System.Enum.TryParse(splitted[0], out key);
                    if (key == 0)
                    {
                        Log.WriteLine("Failed to parse value \"" + element.Value + "\" from hotkeys.xml.");
                        continue;
                    }

                    bool control = false;
                    bool success = Boolean.TryParse(splitted[1], out control);
                    if (!success)
                    {
                        Log.WriteLine("Failed to parse value \"" + element.Value + "\" from hotkeys.xml.");
                        continue;
                    }

                    bool alt = false;
                    success = Boolean.TryParse(splitted[2], out alt);
                    if (!success)
                    {
                        Log.WriteLine("Failed to parse value \"" + element.Value + "\" from hotkeys.xml.");
                        continue;
                    }

                    if (!ActionKey.ActionKeys.ContainsKey(action))
                    {
                        Log.WriteLine("Action \"" + action + "\" does not exist, skipping...");
                        continue;
                    }

                    ActionKey.ActionKeys[action].Key = key;
                    ActionKey.ActionKeys[action].Control = control;
                    ActionKey.ActionKeys[action].Alt = alt;
                }
            }
            catch
            {
                Log.WriteLine("Failed to load \"" + Path.Combine(Program.EXECUTABLE_PATH, "hotkeys.xml") + "\".");
            }
        }
    }
}
