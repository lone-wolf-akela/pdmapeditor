using System.Collections.Generic;
using System.ComponentModel;

namespace PDMapEditor
{
    [TypeConverter(typeof(ShipTypeConverter))]
    public class ShipType
    {
        public static List<ShipType> ShipTypes = new List<ShipType>();

        public string Name;

        public ShipType(string name)
        {
            Name = name;

            ShipTypes.Add(this);
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
