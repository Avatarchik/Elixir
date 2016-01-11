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
        Stun,
        DoteDamage
    }

    public enum Phase
    {
        Solid,
        Liquid,
        Gas
    }

    public enum AttackMode
    {
        Card,
        Chemist
    }
    public enum ChemicalStates
    {
        LIQUID,
        GAS,
        SOLID
    }
    public class ChemicalState
    {
        ChemicalStates chemicalState;
        int solidStateValue;
        int liquidStateValue;
        int gasStateValue;

        public ChemicalState(ChemicalStates chemicalState, int solid, int liquid, int gas)
        {
            this.chemicalState = chemicalState;
            this.solidStateValue = solid;
            this.liquidStateValue = liquid;
            this.gasStateValue = gas;
        }
        public ChemicalStates GetChemincalState()
        {
            return chemicalState;
        }
        public int GetSolidStateValue()
        {
            return solidStateValue;
        }
        public int GetLiquidStateValue()
        {
            return liquidStateValue;
        }
        public int GetGasStateValue()
        {
            return gasStateValue;
        }

    }

    public class Buff
    {
        BuffName name;
        int remainTurn;
        int damagePerTurn;
        
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
        int damagePerTurn;
        
        public Debuff (DebuffName name, int remainTurn)
        {
            this.name = name;
            this.remainTurn = remainTurn;
        }
        public Debuff(DebuffName name, int remainTurn, int damagePerTurn)
        {
            this.name = name;
            this.remainTurn = remainTurn;
            this.damagePerTurn = damagePerTurn;
        }

        public DebuffName GetDebuffname()
        {
            return name;
        }

        public int RemainTurn
        {
            get { return remainTurn; }
            set { remainTurn = value; }
        }

        public int DebuffDamage
        {
            get { return damagePerTurn; }
            set { damagePerTurn = value; }
        }
    }
}
