using UnityEngine;
using System.Collections;
using EnumsAndClasses;
using System.Collections.Generic;

[System.Serializable]
public class baseCharacter{

	private string CharacterName;

    //stats
    public int level;
	public float MAX_HP=200.0f;
	public float HP;
    public float AttackDamage;
    public ChemicalStates criticalTarget;
    public ChemicalStates currentChemicalState;
    public int currentChemicalStateValue;
    public int solidStateValue;
    public int liquidStateValue;
    public int gasStateValue;
    public int dodgeRate;

    public bool criticalTargetImmuned;
    public bool debuffImmuned;
    public bool stateChangeGuarded;
    public bool heatImmuned;
    public bool skillImmuned;
    public bool damageResisted;

    public bool actionLimited;
    public bool isDead;

    public Buff dodge;
    public Buff immuneCriticalTarget;
    public Buff debuffImmune;
    public Buff guardStateChange;
    public Buff immuneHeat;
    public Buff immuneSkill;
    public Buff damageResistance;
    public List<Buff> dotHealList = new List<Buff>();

    public List<Debuff> dotDamageList = new List<Debuff>();
    public Debuff actionLimit;

    void Start()
    {
        dodge = null;
        immuneCriticalTarget = null;
        criticalTargetImmuned = false;
        debuffImmune = null;
        debuffImmuned = false;
        guardStateChange = null;
        stateChangeGuarded = false;
        immuneHeat = null;
        heatImmuned = false;
        immuneSkill = null;
        skillImmuned = false;
        damageResistance = null;
        damageResisted = false;
        this.actionLimit = null;
        actionLimited = false;
}

    public void SetHeal(int heal)
    {
        HP += heal;
        if (HP > MAX_HP)
        {
            HP = MAX_HP;
        }
        Debug.Log("Get " + heal + " heal by player");
    }
    public void SetHeal(int heal, ChemicalStates critical)
    {
        if (this.currentChemicalState == critical)
        {
            HP += (int)(heal * 1.5f);
        }
        else
        {
            HP += heal;
        }
        
        if (HP > MAX_HP)
        {
            HP = MAX_HP;
        }
        Debug.Log("Get " + heal + " heal by player");
    }
    public void SetDamage(int damage)
    {
        if (damageResisted)
        {
            HP -= damage - (damage * damageResistance.Effect / 100);
        }
        else
        {
            HP -= damage;
        }
        

        if (HP < 0)
        {
            HP = 0;
            isDead = true;
        }
        Debug.Log("Get " + damage + " damage by monster");
    }
    public void SetDamage(int damage, ChemicalStates critical)
    {
        int finalDamage = 0;
        if (this.currentChemicalState == critical)
        {
            if (criticalTargetImmuned)
            {
                Debug.Log("Critical Target Immuned");
                finalDamage = damage;
            }
            else
            {
                finalDamage = (int)(damage * 1.5f);
            }
        }
        else
        {
            finalDamage = damage;
        }

        if (damageResisted)
        {
            finalDamage -= (finalDamage * damageResistance.Effect) /100;
            HP -= finalDamage;
        }
        else
        {
            HP -= finalDamage;
        }

        if (HP < 0)
        {
            HP = 0;
            isDead = true;
        }
        Debug.Log("Get " + finalDamage + " damage by monster");
    }

    public void ChangeState(ChemicalStates chemicalState)
    {
        if (stateChangeGuarded)
        {
            Debug.Log("State Change Guarded");
            return;
        }
        switch(chemicalState){
            case ChemicalStates.SOLID:
                switch(this.currentChemicalState){
                    case ChemicalStates.SOLID:
                        Debug.Log("Do nothing");
                        break;
                    case ChemicalStates.LIQUID:
                        this.currentChemicalState = ChemicalStates.SOLID;
                        this.currentChemicalStateValue = this.solidStateValue;
                        break;
                    case ChemicalStates.GAS:
                        this.currentChemicalState = ChemicalStates.SOLID;
                        this.currentChemicalStateValue = this.solidStateValue;
                        break;
                }
                break;
            case ChemicalStates.LIQUID:
                switch (this.currentChemicalState)
                {
                    case ChemicalStates.SOLID:
                        this.currentChemicalState = ChemicalStates.LIQUID;
                        this.currentChemicalStateValue = 1;
                        break;
                    case ChemicalStates.LIQUID:
                        Debug.Log("Do nothing");
                        break;
                    case ChemicalStates.GAS:
                        this.currentChemicalState = ChemicalStates.LIQUID;
                        this.currentChemicalStateValue = this.liquidStateValue;
                        break;
                }
                break;
            case ChemicalStates.GAS:
                switch (this.currentChemicalState)
                {
                    case ChemicalStates.SOLID:
                        this.currentChemicalState = ChemicalStates.GAS;
                        this.currentChemicalStateValue = 1;
                        break;
                    case ChemicalStates.LIQUID:
                        this.currentChemicalState = ChemicalStates.GAS;
                        this.currentChemicalStateValue = 1;
                        break;
                    case ChemicalStates.GAS:
                        Debug.Log("Do nothing");
                        break;
                }
                break;
        }
    }

    //Chemical State
    public void IncrementCSVal()
    {
        if (heatImmuned)
        {
            Debug.Log("Heat Immuned");
            return;
        }
        switch (currentChemicalState)
        {
            case ChemicalStates.SOLID:
                currentChemicalStateValue++;
                if (currentChemicalStateValue > solidStateValue)
                {
                    currentChemicalState = ChemicalStates.LIQUID;
                    currentChemicalStateValue = 1;
                }
                break;
            case ChemicalStates.LIQUID:
                currentChemicalStateValue++;
                if (currentChemicalStateValue > liquidStateValue)
                {
                    currentChemicalState = ChemicalStates.GAS;
                    currentChemicalStateValue = 1;
                }
                break;
            case ChemicalStates.GAS:
                if (currentChemicalStateValue >= gasStateValue)
                {
                    Debug.Log("Cannot increase chemical state value");
                }
                else
                {
                    currentChemicalStateValue++;
                }

                break;
        }
    }
    public void DecrementCSVal()
    {
        switch (currentChemicalState)
        {
            case ChemicalStates.SOLID:
                if (currentChemicalStateValue == 1)
                {
                    Debug.Log("Cannot decrease chemical state value");
                }
                else
                {
                    currentChemicalStateValue--;
                }
                break;
            case ChemicalStates.LIQUID:
                currentChemicalStateValue--;
                if (currentChemicalStateValue <= 0)
                {
                    currentChemicalState = ChemicalStates.SOLID;
                    currentChemicalStateValue = solidStateValue;
                }
                break;
            case ChemicalStates.GAS:
                currentChemicalStateValue--;
                if (currentChemicalStateValue <= 0)
                {
                    currentChemicalState = ChemicalStates.LIQUID;
                    currentChemicalStateValue = liquidStateValue;
                }
                break;
        }
    }

    public bool isBuffPresent()
    {
        return (dodge != null) || (immuneCriticalTarget != null) || (debuffImmune != null) 
            || (guardStateChange != null) || (immuneHeat != null) ||
            (damageResistance != null) || (dotHealList.Count != 0);
    }
    public bool isDebuffPresent()
    {
        return (dotDamageList.Count != 0) || (actionLimit != null);
    }

    public void AddBuff(Buff buff)
    {
        switch (buff.GetBuffname())
        {
            case BuffName.Dodge:
                dodge = buff;
                dodgeRate = buff.Effect;
                break;
            case BuffName.ImmuneCriticalTarget:
                immuneCriticalTarget = buff;
                criticalTargetImmuned = true;
                break;
            case BuffName.DebuffImmune:
                debuffImmune = buff;
                debuffImmuned = true;
                break;
            case BuffName.GuardStateChange:
                guardStateChange = buff;
                stateChangeGuarded = true;
                break;
            case BuffName.ImmuneHeat:
                immuneHeat = buff;
                heatImmuned = true;
                break;
            case BuffName.ImmuneSkill:
                immuneSkill = buff;
                skillImmuned = true;
                break;
            case BuffName.DamageResistance:
                damageResistance = buff;
                damageResisted = true;
                break;
            case BuffName.DotHeal:
                dotHealList.Add(buff);
                break;
            default:
                Debug.Log("No corresponding buff");
                break;
        }
    }
    public void AddDebuff(Debuff debuff)
    {
        if (debuffImmuned)
        {
            Debug.Log("Debuff Immuned");
            return;
        }
        switch (debuff.GetDebuffname()) {
            case DebuffName.DoteDamage:
                dotDamageList.Add(debuff);
                break;
            case DebuffName.ActionLimit:
                actionLimit = debuff;
                actionLimited = true;
                break;
            default:
                Debug.Log("No corresponding debuff");
                break;
        }
    }

    //Dodge
    public void ReduceDodgeTurn()
    {
        if(dodge != null)
        {
            dodge.RemainTurn--;
        }
        if(dodge.RemainTurn == 0)
        {
            dodge = null;
            dodgeRate = 0;
        }
    }
    public void ActivateDodge()
    {
        if (dodge != null)
        {
            Debug.Log("Maintain current dodge rate:" + this.dodgeRate);
        }
        else
        {
            dodgeRate = 0;
        }
    }
    public void RemoveDodge()
    {
        dodge = null;
        dodgeRate = 0;
    }

    //ImmuneCriticalTarget
    public void ReduceImmuneCriticalTargetTurn()
    {
        if (immuneCriticalTarget != null)
        {
            immuneCriticalTarget.RemainTurn--;
        }
        if (immuneCriticalTarget.RemainTurn == 0)
        {
            immuneCriticalTarget = null;
            criticalTargetImmuned = false;
        }
    }
    public void ActivateImmuneCriticalTarget()
    {
        if (immuneCriticalTarget != null)
        {

        }
        else
        {
            criticalTargetImmuned = false;
        }
    }
    public void RemoveImmuneCriticalTarget()
    {
        immuneCriticalTarget = null;
        criticalTargetImmuned = false;
    }

    //DebuffImmune
    public void ReduceDebuffImmuneTurn()
    {
        if (debuffImmune != null)
        {
            debuffImmune.RemainTurn--;
        }
        if (debuffImmune.RemainTurn == 0)
        {
            debuffImmune = null;
            debuffImmuned = false;
        }
    }
    public void ActivateDebuffImmune()
    {
        if (debuffImmune != null)
        {

        }
        else
        {
            debuffImmuned = false;
        }
    }
    public void RemoveDebuffImmune()
    {
        debuffImmune = null;
        debuffImmuned = false;
    }

    //GuardStateChange
    public void ReduceGuardStateChangeTurn()
    {
        if (guardStateChange != null)
        {
            guardStateChange.RemainTurn--;
        }
        if (guardStateChange.RemainTurn == 0)
        {
            guardStateChange = null;
            stateChangeGuarded = false;
        }
    }
    public void ActivateGuardStateChange()
    {
        if (guardStateChange != null)
        {

        }
        else
        {
            stateChangeGuarded = false;
        }
    }
    public void RemoveGuardStateChange()
    {
        guardStateChange = null;
        stateChangeGuarded = false;
    }

    //ImmuneHeat
    public void ReduceImmuneHeatTurn()
    {
        if (immuneHeat != null)
        {
            immuneHeat.RemainTurn--;
        }
        if (immuneHeat.RemainTurn == 0)
        {
            immuneHeat = null;
            heatImmuned = false;
        }
    }
    public void ActivateImmuneHeat()
    {
        if (immuneHeat != null)
        {

        }
        else
        {
            heatImmuned = false;
        }
    }
    public void RemoveImmuneHeat()
    {
        immuneHeat = null;
        heatImmuned = false;
    }

    //ImmuneSkill
    public void ReduceImmuneSkillTurn()
    {
        if (immuneSkill != null)
        {
            immuneSkill.RemainTurn--;
        }
        if (immuneSkill.RemainTurn == 0)
        {
            immuneSkill = null;
            skillImmuned = false;
        }
    }
    public void ActivateImmuneSkill()
    {
        if (immuneSkill != null)
        {

        }
        else
        {
            skillImmuned = false;
        }
    }
    public void RemoveImmuneSkill()
    {
        immuneSkill = null;
        skillImmuned = false;
    }

    //DamageResistance
    public void ReduceDamageResistanceTurn()
    {
        if (damageResistance != null)
        {
            damageResistance.RemainTurn--;
        }
        if (damageResistance.RemainTurn == 0)
        {
            damageResistance = null;
            damageResisted = false;
        }
    }
    public void ActivateDamageResistance()
    {
        if (immuneHeat != null)
        {

        }
        else
        {
            damageResisted = false;
        }
    }
    public void RemoveDamageResistance()
    {
        damageResistance = null;
        damageResisted = false;
    }

    // DotHeal Implementation
    public void ReduceDotHealTurn()
    {
        List<Buff> buffsToDestroy = new List<Buff>();
        foreach (Buff dotHeal in dotHealList)
        {
            dotHeal.RemainTurn--;
            if (dotHeal.RemainTurn <= 0)
            {
                buffsToDestroy.Add(dotHeal);
            }
        }
        foreach (Buff buffToDestroy in buffsToDestroy)
        {
            dotHealList.Remove(buffToDestroy);
        }
        buffsToDestroy.Clear();
        if (dotHealList.Count >= 1)
        {
            //monsterPrefs.monsterObjectList[monsterID].transform.Find("dotDamageIcon").gameObject.SetActive(true);
        }
        else
        {
            //monsterPrefs.monsterObjectList[monsterID].transform.Find("dotDamageIcon").gameObject.SetActive(false);
        }
    }
    public void ActivateDotHeal()
    {
        foreach (Buff dotHeal in dotHealList)
        {
            SetHeal(dotHeal.Effect);
        }
    }
    public void RemoveDotHeal()
    {
        dotHealList.Clear();
    }

    // DotDamage Implementation
    public void ReduceDotDamageTurn()
    {
        List<Debuff> debuffsToDestroy = new List<Debuff>();
        foreach (Debuff dotDamage in dotDamageList)
        {
            dotDamage.RemainTurn--;
            if (dotDamage.RemainTurn <= 0)
            {
                debuffsToDestroy.Add(dotDamage);
            }
        }
        foreach (Debuff debuffToDestroy in debuffsToDestroy)
        {
            dotDamageList.Remove(debuffToDestroy);
        }
        debuffsToDestroy.Clear();
        if (dotDamageList.Count >= 1)
        {
            //monsterPrefs.monsterObjectList[monsterID].transform.Find("dotDamageIcon").gameObject.SetActive(true);
        }
        else
        {
            //monsterPrefs.monsterObjectList[monsterID].transform.Find("dotDamageIcon").gameObject.SetActive(false);
        }
    }
    public void ActivateDotDamage()
    {
        foreach (Debuff dotDamage in dotDamageList)
        {
            SetDamage(dotDamage.DebuffDamage);
            if (HP <= 0)
            {
                isDead = true;
                break;
            }
        }
    }
    public void RemoveDotDamage()
    {
        dotDamageList.Clear();
    }

    // ActionLimit Implementation
    public void ReduceActionLimitTurn()
    {
        if (actionLimit != null)
        {
            actionLimit.RemainTurn--;
        }
        if (actionLimit.RemainTurn == 0)
        {
            actionLimit = null;
            actionLimited = false;
        }
    }
    public void ActivateActionLimit()
    {
        if (actionLimited)
        {
            GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().turnCount = 1;
        }
    }
    public void RemoveActionLimit()
    {
        actionLimit = null;
        actionLimited = false;
    }


}
