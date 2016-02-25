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
        Dodge,
        Shield,
        ImmuneCriticalTarget,
        DebuffImmune,
        GuardStateChange,
        ImmuneHeat,
        DamageResistance,
        DotHeal,
        ImmuneSkill
    }

    public enum DebuffName
    {
        None,
        Stun,
        DoteDamage,
        Silent,
        Blind,
        ActionLimit
    }

    public enum Phase
    {
        Solid,
        Liquid,
        Gas
    }

    public enum AttackMode
    {
        Element,
        Chemist
    }
    public enum ChemicalStates
    {
        LIQUID,
        GAS,
        SOLID
    }

    public enum ChemistSkills
    {
        Cool,
        Heat,
        Analyze
    }
    [System.Serializable]
    public class Buff
    {
        public BuffName name;
        public int remainTurn;
        public int effect;
        
        public Buff (BuffName name, int remainTurn)
        {
            this.name = name;
            this.remainTurn = remainTurn;
        }
        public Buff(BuffName name, int remainTurn, int effect)
        {
            this.name = name;
            this.remainTurn = remainTurn;
            this.effect = effect;
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

        public int Effect
        {
            get { return effect; }
            set { effect = value; }
        }
    }
    [System.Serializable]
    public class Debuff
    {
        public DebuffName name;
        public int remainTurn;
        public int damagePerTurn;
        
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
		public int DamageFactor;
		public int SelfDamage;
		public int Heal;
		public int TargetTempChange;
		public string UseCondition1_1;
		public string UseCondition1_2;
		public string UseCondition2_1;
		public string UseCondition2_2;
		public string UseCondition3_1;
		public string UseCondition3_2;
		public string TargetStateChange;
		public string DebuffName;
		public int DebuffRate;
		public int DebuffTurn;
		public string DebuffEffect;
		public int DotDamage;
		public int DotDamageTurn;
		public string BuffName;
		public int BuffRate;
		public int BuffTurn;
		public string BuffEffect;
		
	}
	public class MonsterSkillConditionRow
	{
		public int no;
		public string UseCondition;
		public string Description;
		public string TargetState;
		public int TargetHpBelowN;
		public int TargetHpMoreN;
		public int SelfHpBelowN;
		public string Actionlimit;
		public int TargetNumber;
		public int RandomRate;
		public string TargetAffectedEffect;
	}
}
