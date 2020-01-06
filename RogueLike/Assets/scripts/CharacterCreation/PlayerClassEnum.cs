using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum PlayerClassEnum : byte
{
    Warrior = 1,
    Mage = 2,
    Ranger = 3
}

//public struct PlayerClass
//{
//    public static readonly PlayerClass Warrior = new PlayerClass("Warrior" , 1, 0, 0, 0);
//    public static readonly PlayerClass Mage = new PlayerClass("Mage" , 2, 0, 0, 0);
//    public static readonly PlayerClass Ranger = new PlayerClass("Ranger" , 3, 0, 0, 0);

//    /// <summary>
//    /// The name of the Class.
//    /// </summary>
//    public string ClassName { get; private set; }
//    /// <summary>
//    /// The index where the class is at.
//    /// </summary>
//    public ushort index { get; private set; }
//    /// <summary>
//    /// The added value to the default health value.
//    /// </summary>
//    public sbyte HealthMod { get; private set; }
//    /// <summary>
//    /// The added value to the default Strength value.
//    /// </summary>
//    public sbyte StrengthMod { get; private set; }
//    /// <summary>
//    /// The added value to the default Armor Class.
//    /// </summary>
//    public sbyte ArmorClassMod { get; private set; }
//    public PlayerClass(string ClassName, ushort index, sbyte HealthMod, sbyte StrengthMod, sbyte ArmorClassMod)
//    {
//        this.ClassName = ClassName;
//        this.index = index;
//        this.HealthMod = HealthMod;
//        this.StrengthMod = StrengthMod;
//        this.ArmorClassMod = ArmorClassMod;
//    }
//}
