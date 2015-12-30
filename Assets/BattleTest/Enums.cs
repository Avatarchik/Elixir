using UnityEngine;
using System.Collections;

namespace Enums
{
    public enum MonsterType
    {
        None
    }

    public enum BuffName
    {
        None
    }

    public enum DebuffName
    {
        None,
        Stun
    }

    public enum Phase
    {
        Solid,
        Liquid,
        Gas
    }

    public class Buff
    {
        BuffName name;
        int remainTurn;
        
        public Buff (BuffName name, int remainTurn)
        {
            this.name = name;
            this.remainTurn = remainTurn;
        }
    }

    public class Debuff
    {
        BuffName name;
        int remainTurn;
        
        public Debuff (BuffName name, int remainTurn)
        {
            this.name = name;
            this.remainTurn = remainTurn;
        }
    }
}
