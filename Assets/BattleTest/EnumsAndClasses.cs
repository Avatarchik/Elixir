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
        None,
        Dodge
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

        public int RemainTurn
        {
            get { return remainTurn; }
            set { remainTurn = value; }
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
	public class MonsterSkillRow
	{
		public string no;
		public string MonsterSkillID;
		public string MonsterSkillName;
		public string Target;
		public string Range;
		public string DamageFactor;
		public string Heal;
		public string TargetTempChange;
		public string UseCondition1_1;
		public string UseCondition1_2;
		public string UseCondition2_1;
		public string UseCondition2_2;
		public string UseCondition3_1;
		public string UseCondition3_2;
		public string TargetStateChange;
		public string DebuffName;
		public string DebuffRate;
		public string DebuffTurn;
		public string DebuffEffect;
		public string DotDamage;
		public string DotDamageTurn;
		public string BuffName;
		public string BuffRate;
		public string BuffTurn;
		public string BuffEffect;
		
	}
	public class MonsterSkillConditionRow
	{
		public int no;
		public string UseCondition;
		public string Description;
		public string TargetState;
		public int HpBelowN;
		public int HpMoreN;
		public string Actionlimit;
		public int TargetNumber;
		public int RandomRate;
		
	}
}
