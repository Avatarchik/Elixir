using UnityEngine;
using System.Collections;

namespace EnumsAndClasses
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
        
        public BuffName GetBuffname()
        {
            return name;
        }
    }

    public class Debuff
    {
        DebuffName name;
        int remainTurn;
        
        public Debuff (DebuffName name, int remainTurn)
        {
            this.name = name;
            this.remainTurn = remainTurn;
        }
        
        public DebuffName GetDebuffname()
        {
            return name;
        }
    }
}
