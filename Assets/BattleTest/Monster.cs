using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumsAndClasses;

public class Monster : MonoBehaviour {

    public new SpriteRenderer renderer;
    public int maxHp;
    public int hp;
    public int attackDamage;
    public string type;
    public bool stunned;

    public ChemicalStates currentChemicalState;
    public int currentChemicalStateValue;
    public int solidStateValue;
    public int liquidStateValue;
    public int gasStateValue;
    public ChemicalStates criticalTarget;

	public baseMonster monsterInfo;

    List<Buff> buffs = new List<Buff>();
    List<Debuff> debuffs = new List<Debuff>();

    List<Debuff> stunList = new List<Debuff>();
    List<Debuff> dotDamageList = new List<Debuff>();
    
	void Update(){
		if (hp <= 0) {
			Dead();
		}
	}

    // Apply default stats.
    public void SetStat ()
    {
        baseMonster monsterInfo = this.GetComponent<InfoMonster>().MonsterInfo;
        string imagePath = "MonsterImage/" + monsterInfo.Mon_Name;
        Debug.Log("image : " + imagePath);
        this.renderer.sprite = Resources.Load(imagePath, typeof(Sprite)) as Sprite;
        
        this.maxHp = monsterInfo.Mon_HP;
        this.hp = monsterInfo.Mon_HP;
        this.attackDamage = (int)monsterInfo.Mon_AttackDamage;
        this.type = monsterInfo.Mon_Type;
        this.stunned = false;
        this.currentChemicalState = monsterInfo.Mon_RoomTempStatus;
        this.currentChemicalStateValue = 1;
        this.solidStateValue = monsterInfo.Mon_SolidGauge;
        this.liquidStateValue = monsterInfo.Mon_LiquidGauge;
        this.gasStateValue = monsterInfo.Mon_GasGauge;
        this.criticalTarget = monsterInfo.Mon_CriticalTarget;
    }
    
    // Only apply image.
    // public void SetStat (string imagePath)
    // {
    //     this.renderer.sprite = Resources.Load(imagePath, typeof(Sprite)) as Sprite;
    //     SetStat();
    // }
    
    // Default image.
    public void SetStat (int hp, int attackDamage, string type, int boilingPoint, int meltingPoint)
    {
        this.maxHp = hp;
        this.hp = maxHp;
        this.attackDamage = attackDamage;
        this.type = type;
    }
    public void SetStat (string imagePath, int hp, int attackDamage, string type, int boilingPoint, int meltingPoint)
    {
        this.renderer.sprite = Resources.Load(imagePath, typeof(Sprite)) as Sprite;
        SetStat(hp, attackDamage, type, boilingPoint, meltingPoint);
    }
    
	public void SetHeal(int heal){
		hp += heal;
		if (hp > maxHp) {
			hp = maxHp;
		}
	}


    public void SetDamage(int damage)
    {
        hp -= damage;
        if (hp < 0)
            hp = 0;
        Debug.Log("Get " + damage + " damage by player");
    }

    //Chemical State
    public void IncrementCSVal()
    {
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
                if(currentChemicalStateValue >= gasStateValue)
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
	public int DebuffListCount(){
		return debuffs.Count;
	}
	public int BuffListCount(){
		return buffs.Count;
	}

    // Stun Implementation
    public void AddStun(Debuff stun)
    {
        this.transform.Find("stun").gameObject.SetActive(true);
        stunned = true;
        stunList.Add(stun);
    }
    public void ReduceStunTurn()
    {
        List<Debuff> debuffsToDestroy = new List<Debuff>();
        foreach (Debuff stun in stunList)
        {
            stun.RemainTurn--;
            if (stun.RemainTurn <= 0)
            {
                debuffsToDestroy.Add(stun);
            }
        }
        foreach (Debuff debuffToDestroy in debuffsToDestroy)
        {
            stunList.Remove(debuffToDestroy);
        }
        debuffsToDestroy.Clear();
        if (stunList.Count >= 1)
        {
            this.transform.Find("stun").gameObject.SetActive(true);
        }
        else
        {
            this.transform.Find("stun").gameObject.SetActive(false);
        }
    }
    public void ActivateStun()
    {
        if(stunList.Count >= 1)
        {
            stunned = true;
        }
        else
        {
            stunned = false;
        }
    }
    public void RemoveStun()
    {
        stunList.Clear();
    }

    // DotDamage Implementation
    public void AddDotDamage(Debuff dotDamage)
    {
        this.transform.Find("dotDamageIcon").gameObject.SetActive(true);
        dotDamageList.Add(dotDamage);
    }
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
            this.transform.Find("dotDamageIcon").gameObject.SetActive(true);
        }
        else
        {
            this.transform.Find("dotDamageIcon").gameObject.SetActive(false);
        }
    }
    public void ActivateDotDamage()
    {
        foreach(Debuff dotDamage in dotDamageList)
        {
            SetDamage(dotDamage.DebuffDamage);
            if(hp <= 0)
            {
                Dead();
                break;
            }
        }
    }
    public void RemoveDotDamage()
    {
        stunList.Clear();
    }

    public void AddBuff(Buff buff)
    { }
    public void AddDebuff(Debuff debuff)
    { }
    public void ReduceBuffTurn()
    { }
    public void ReduceDebuffTurn()
    { }
    public void ActivateBuff()
    { }
    public void ActivateDebuff()
    { }
    public void RemoveBuff()
    { }
    public void RemoveDebuff()
    { }
        /*
        public void AddBuff(Buff buff)
        {
            buffs.Add(buff);
            Debug.Log("Add buff to monster");
            //PrintAllBuffAndDebuff();
        }

        public void AddDebuff(Debuff debuff)
        {
            debuffs.Add(debuff);
            Debug.Log("Add debuff to monster");
            //PrintAllBuffAndDebuff();
        }

        public void ReduceDebuffTurn()
        {
            foreach(Debuff debuff in debuffs)
            {
                debuff.RemainTurn--;
                Debug.Log("Debuff turn remaining: " + debuff.RemainTurn);
            }
        }

        public void ActivateDebuff()
        {
            bool tempStun= false;
            bool tempDotDamage = false;
            foreach (Debuff debuff in debuffs)
            {

                if(debuff.GetDebuffname().Equals(DebuffName.DoteDamage))//Activate Dot Damage
                {
                    Debug.Log("This Debuff is Dot Damage");
                    SetDamage(debuff.DebuffDamage);
                    tempDotDamage = true;
                    if(hp <= 0)
                    {
                        Destroy(this.gameObject);
                    }
                }

                if (debuff.GetDebuffname().Equals(DebuffName.Stun))
                {
                    //Stun Enemy
                    tempStun = true;
                }
            }
            if (tempStun)
            {
                this.transform.Find("stun").gameObject.SetActive(true);
                stunned = true;
            }
            else
            {
                this.transform.Find("stun").gameObject.SetActive(false);
                stunned = false;
            }
            if (tempDotDamage)
            {
                this.transform.Find("dotDamageIcon").gameObject.SetActive(true);
            }
            else
            {
                this.transform.Find("dotDamageIcon").gameObject.SetActive(false);
            }
        }

        public void RemoveBuff()
        {
            //NotImplemented
        }

        public void RemoveAllBuff()
        {
            buffs = new List<Buff>();
            Debug.Log("All buffs are removed");
        }

        public void RemoveDebuff()
        {
            List<Debuff> debuffsToDestroy = new List<Debuff>();
            foreach (Debuff debuff in debuffs)
            {
                if (debuff.RemainTurn == 0)
                {
                    debuffsToDestroy.Add(debuff);
                }
            }
            foreach (Debuff debuffToDestroy in debuffsToDestroy)
            {
                debuffs.Remove(debuffToDestroy);
            }
            debuffsToDestroy.Clear();
        }

        public void RemoveAllDebuff()
        {
            debuffs = new List<Debuff>();
            Debug.Log("All debuffs are removed");
        }
        */
        // using test.
        void PrintAllBuffAndDebuff()
    {
        string buffList = "Buff : ";
        foreach (var buff in buffs)
        {
            buffList += buff.GetBuffname().ToString();
            buffList += ", ";
        }
        Debug.Log(buffList);
        
        string debuffList = "Debuff : ";
        foreach (var debuff in debuffs)
        {
            debuffList += debuff.GetDebuffname().ToString();
            debuffList += ", ";
        }
        Debug.Log(debuffList);
    }

	public void Dead(){
        GameObject.Find("GameManager").GetComponent<TurnBasedCombatStateMachine>().dustCount += this.gameObject.GetComponent<InfoMonster>().MonsterInfo.Mon_GoldRate;
		Destroy (this.gameObject);
		Debug.Log (this + " is Dead.");
	}
	// Use this for initialization
	void Start () {
	
	}

}
