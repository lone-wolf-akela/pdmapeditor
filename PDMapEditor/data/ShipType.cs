using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMapEditor
{
    public class ShipType
    {
        public static List<ShipType> ShipTypes = new List<ShipType>();

        public string Name;

        public int ComboIndex = -1;

        public ShipType(string name)
        {
            Name = name;

            ShipTypes.Add(this);

            Program.main.comboSquadronType.Items.Add(Name);
            ComboIndex = Program.main.comboSquadronType.Items.Count - 1;
        }

        public static ShipType GetTypeFromName(string name)
        {
            foreach(ShipType type in ShipTypes)
            {
                if (type.Name == name)
                    return type;
            }

            return null;
        }

        public static ShipType GetTypeFromComboIndex(int index)
        {
            foreach(ShipType type in ShipTypes)
            {
                if (type.ComboIndex == index)
                    return type;
            }

            return null;
        }
    }

    public enum AvoidanceFamily //Unused
    {
        None,
        DontAvoid,
        Strikecraft,
        Utility,
        Frigate,
        SmallRock,
        Capital,
        SuperCap,
        BattleCruiser,
        MotherShip,
        BigRock,
        SuperPriority,
    }
}
