using UnityEngine;
using System.Collections;
using EnumsAndClasses;

[System.Serializable]
public class baseSkill
{
    private int skill_ID;
    private string skill_ExtName;
    private string skill_Name;
    private string skill_Icon;
    private string skill_Type;
    private int skill_ElementNumber;
    private int skill_ProperLevel;
    private string skill_Description;
    private string skill_Concept;
    private string skill_Memo;
    private string skill_Target;
    private string skill_Range;
    private float skill_AttackDamage;
    private int skill_AdditionalDamage;
    private int skill_OptionalMinDamage;
    private int skill_OptionalMaxDamage;
    private float skill_Heal;
    private int skill_AdditionalHeal;
    private int skill_HealTurn;
    private string skill_UserEffect;
    private string skill_UserEffectDescription;
    private string skill_UserEffectTiming;
    private float skill_SelfHealRate;
    private float skill_SelfDamageRate;
    private string skill_DebuffName;
    private float skill_DebuffRate;
    private int skill_DebuffTurn;
    private float skill_DebuffRateIncrease;
    private string skill_DebuffEffect;
    private float skill_DotDamage;
    private int skill_DotDamageTurn;
    private string skill_BuffName;
    private float skill_BuffRate;
    private int skill_BuffTurn;
    private float skill_DamageResistance;
    private float skill_BuffEffectRateIncrease;
    private string skill_BuffEffect;

    public int Skill_ID
    {
        get
        {
            return skill_ID;
        }

        set
        {
            skill_ID = value;
        }
    }

    public string Skill_ExtName
    {
        get
        {
            return skill_ExtName;
        }

        set
        {
            skill_ExtName = value;
        }
    }

    public string Skill_Name
    {
        get
        {
            return skill_Name;
        }

        set
        {
            skill_Name = value;
        }
    }

    public string Skill_Icon
    {
        get
        {
            return skill_Icon;
        }

        set
        {
            skill_Icon = value;
        }
    }

    public string Skill_Type
    {
        get
        {
            return skill_Type;
        }

        set
        {
            skill_Type = value;
        }
    }

    public int Skill_ElementNumber
    {
        get
        {
            return skill_ElementNumber;
        }

        set
        {
            skill_ElementNumber = value;
        }
    }

    public int Skill_ProperLevel
    {
        get
        {
            return skill_ProperLevel;
        }

        set
        {
            skill_ProperLevel = value;
        }
    }

    public string Skill_Description
    {
        get
        {
            return skill_Description;
        }

        set
        {
            skill_Description = value;
        }
    }

    public string Skill_Concept
    {
        get
        {
            return skill_Concept;
        }

        set
        {
            skill_Concept = value;
        }
    }

    public string Skill_Memo
    {
        get
        {
            return skill_Memo;
        }

        set
        {
            skill_Memo = value;
        }
    }

    public string Skill_Target
    {
        get
        {
            return skill_Target;
        }

        set
        {
            skill_Target = value;
        }
    }

    public string Skill_Range
    {
        get
        {
            return skill_Range;
        }

        set
        {
            skill_Range = value;
        }
    }

    public float Skill_AttackDamage
    {
        get
        {
            return skill_AttackDamage;
        }

        set
        {
            skill_AttackDamage = value;
        }
    }

    public int Skill_AdditionalDamage
    {
        get
        {
            return skill_AdditionalDamage;
        }

        set
        {
            skill_AdditionalDamage = value;
        }
    }

    public int Skill_OptionalMinDamage
    {
        get
        {
            return skill_OptionalMinDamage;
        }

        set
        {
            skill_OptionalMinDamage = value;
        }
    }

    public int Skill_OptionalMaxDamage
    {
        get
        {
            return skill_OptionalMaxDamage;
        }

        set
        {
            skill_OptionalMaxDamage = value;
        }
    }

    public float Skill_Heal
    {
        get
        {
            return skill_Heal;
        }

        set
        {
            skill_Heal = value;
        }
    }

    public int Skill_AdditionalHeal
    {
        get
        {
            return skill_AdditionalHeal;
        }

        set
        {
            skill_AdditionalHeal = value;
        }
    }

    public int Skill_HealTurn
    {
        get
        {
            return skill_HealTurn;
        }

        set
        {
            skill_HealTurn = value;
        }
    }

    public string Skill_UserEffect
    {
        get
        {
            return skill_UserEffect;
        }

        set
        {
            skill_UserEffect = value;
        }
    }

    public string Skill_UserEffectDescription
    {
        get
        {
            return skill_UserEffectDescription;
        }

        set
        {
            skill_UserEffectDescription = value;
        }
    }

    public string Skill_UserEffectTiming
    {
        get
        {
            return skill_UserEffectTiming;
        }

        set
        {
            skill_UserEffectTiming = value;
        }
    }

    public float Skill_SelfHealRate
    {
        get
        {
            return skill_SelfHealRate;
        }

        set
        {
            skill_SelfHealRate = value;
        }
    }

    public float Skill_SelfDamageRate
    {
        get
        {
            return skill_SelfDamageRate;
        }

        set
        {
            skill_SelfDamageRate = value;
        }
    }

    public string Skill_DebuffName
    {
        get
        {
            return skill_DebuffName;
        }

        set
        {
            skill_DebuffName = value;
        }
    }

    public float Skill_DebuffRate
    {
        get
        {
            return skill_DebuffRate;
        }

        set
        {
            skill_DebuffRate = value;
        }
    }

    public int Skill_DebuffTurn
    {
        get
        {
            return skill_DebuffTurn;
        }

        set
        {
            skill_DebuffTurn = value;
        }
    }

    public float Skill_DebuffRateIncrease
    {
        get
        {
            return skill_DebuffRateIncrease;
        }

        set
        {
            skill_DebuffRateIncrease = value;
        }
    }

    public string Skill_DebuffEffect
    {
        get
        {
            return skill_DebuffEffect;
        }

        set
        {
            skill_DebuffEffect = value;
        }
    }

    public float Skill_DotDamage
    {
        get
        {
            return skill_DotDamage;
        }

        set
        {
            skill_DotDamage = value;
        }
    }

    public int Skill_DotDamageTurn
    {
        get
        {
            return skill_DotDamageTurn;
        }

        set
        {
            skill_DotDamageTurn = value;
        }
    }

    public string Skill_BuffName
    {
        get
        {
            return skill_BuffName;
        }

        set
        {
            skill_BuffName = value;
        }
    }

    public float Skill_BuffRate
    {
        get
        {
            return skill_BuffRate;
        }

        set
        {
            skill_BuffRate = value;
        }
    }

    public int Skill_BuffTurn
    {
        get
        {
            return skill_BuffTurn;
        }

        set
        {
            skill_BuffTurn = value;
        }
    }

    public float Skill_DamageResistance
    {
        get
        {
            return skill_DamageResistance;
        }

        set
        {
            skill_DamageResistance = value;
        }
    }

    public float Skill_BuffEffectRateIncrease
    {
        get
        {
            return skill_BuffEffectRateIncrease;
        }

        set
        {
            skill_BuffEffectRateIncrease = value;
        }
    }

    public string Skill_BuffEffect
    {
        get
        {
            return skill_BuffEffect;
        }

        set
        {
            skill_BuffEffect = value;
        }
    }
}
