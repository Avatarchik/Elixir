using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumsAndClasses;

public class MonsterLoad:MonoBehaviour
{
	public TextAsset file;
	public List<baseMonster> monsterList;

	public void Initialize()
	{
        monsterList = new List<baseMonster>();
		Load(file);
		ListSetting();
		
	}

	void ListSetting()
	{
		for (int i = 0; i < rowList.Count; i++)
		{
			baseMonster monster = new baseMonster();
			monster.Mon_ID = System.Convert.ToInt32(rowList[i].ID);
			monster.Mon_ExtName = rowList[i].ExtName;
			monster.Mon_Name = rowList[i].Name;
			monster.Mon_ChemicalSeries = rowList[i].ChemicalSeries;
			monster.Mon_Type = rowList[i].Type;
			monster.Mon_Grade = System.Convert.ToInt32(rowList[i].Grade);
			monster.Mon_Level = System.Convert.ToInt32(rowList[i].Lev);
			monster.Mon_AdventField = System.Convert.ToInt32(rowList[i].AdventField);
			monster.Mon_HP = System.Convert.ToInt32(rowList[i].HP);
			monster.Mon_AttackDamage = System.Convert.ToDouble(rowList[i].AttackDamage);
			monster.Mon_Skill1_Name = rowList[i].MonsterSkill1Name;
			monster.Mon_Skill1_Rate = System.Convert.ToDouble(rowList[i].MonsterSkill1Rate);
			monster.Mon_Skill2_Name = rowList[i].MonsterSkill2Name;
			monster.Mon_Skill2_Rate = System.Convert.ToDouble(rowList[i].MonsterSkill2Rate);
            switch (rowList[i].CriticalTarget)
            {
                case "Solid":
                    monster.Mon_CriticalTarget = ChemicalStates.SOLID;
                    break;
                case "Liquid":
                    monster.Mon_CriticalTarget = ChemicalStates.LIQUID;
                    break;
                case "Gas":
                    monster.Mon_CriticalTarget = ChemicalStates.GAS;
                    break;
            }
            switch (rowList[i].RoomTemperatureStatus)
            {
                case "Solid":
                    monster.Mon_RoomTempStatus = ChemicalStates.SOLID;
                    break;
                case "Liquid":
                    monster.Mon_RoomTempStatus = ChemicalStates.LIQUID;
                    break;
                case "Gas":
                    monster.Mon_RoomTempStatus = ChemicalStates.GAS;
                    break;
            }
            monster.Mon_SolidGauge = System.Convert.ToInt32(rowList[i].SolidGauge);
			monster.Mon_LiquidGauge = System.Convert.ToInt32(rowList[i].LiquidGauge);
			monster.Mon_GasGauge = System.Convert.ToInt32(rowList[i].GasGauge);
			monster.Mon_RoomTempPos = System.Convert.ToDouble(rowList[i].RoomTemperaturePosition);
			monster.Mon_MaxCount = System.Convert.ToInt32(rowList[i].MosterMaximumCount);
			monster.Mon_NotAppartionCond = rowList[i].NotAppartionCondition;
			monster.Mon_GoldRate = System.Convert.ToInt32(rowList[i].GoldRate);
			monster.Mon_MonCard1GainRate = System.Convert.ToInt32(rowList[i].MonsterCard1GainRate);
			monster.Mon_MonCard2GainRate = System.Convert.ToInt32(rowList[i].MonsterCard2GainRate);
			
			monsterList.Add(monster);
		}
		
	}

	public class Row
	{
		public string ID;
		public string ExtName;
		public string Name;
		public string ChemicalSeries;
		public string Type;
		public string Grade;
		public string Lev;
		public string AdventField;
		public string HP;
		public string AttackDamage;
		public string MonsterSkill1Name;
		public string MonsterSkill1Rate;
		public string MonsterSkill2Name;
		public string MonsterSkill2Rate;
		public string CriticalTarget;
		public string RoomTemperatureStatus;
		public string SolidGauge;
		public string LiquidGauge;
		public string GasGauge;
		public string RoomTemperaturePosition;
		public string MosterMaximumCount;
		public string NotAppartionCondition;
		public string GoldRate;
		public string MonsterCard1GainRate;
		public string MonsterCard2GainRate;
		
	}
	
	List<Row> rowList = new List<Row>();
	bool isLoaded = false;
	
	public bool IsLoaded()
	{
		return isLoaded;
	}
	
	public List<Row> GetRowList()
	{
		return rowList;
	}
	
	public void Load(TextAsset csv)
	{
		rowList.Clear();
		string[][] grid = CsvParser2.Parse(csv.text);
		for(int i = 1 ; i < grid.Length ; i++)
		{
			Row row = new Row();
			row.ID = grid[i][0];
			row.ExtName = grid[i][1];
			row.Name = grid[i][2];
			row.ChemicalSeries = grid[i][3];
			row.Type = grid[i][4];
			row.Grade = grid[i][5];
			row.Lev = grid[i][6];
			row.AdventField = grid[i][7];
			row.HP = grid[i][8];
			row.AttackDamage = grid[i][9];
			row.MonsterSkill1Name = grid[i][10];
			row.MonsterSkill1Rate = grid[i][11];
			row.MonsterSkill2Name = grid[i][12];
			row.MonsterSkill2Rate = grid[i][13];
			row.CriticalTarget = grid[i][14];
			row.RoomTemperatureStatus = grid[i][15];
			row.SolidGauge = grid[i][16];
			row.LiquidGauge = grid[i][17];
			row.GasGauge = grid[i][18];
			row.RoomTemperaturePosition = grid[i][19];
			row.MosterMaximumCount = grid[i][20];
			row.NotAppartionCondition = grid[i][21];
			row.GoldRate = grid[i][22];
			row.MonsterCard1GainRate = grid[i][23];
			row.MonsterCard2GainRate = grid[i][24];
			
			rowList.Add(row);
		}
		isLoaded = true;
	}
	
	public int NumRows()
	{
		return rowList.Count;
	}
	
	public Row GetAt(int i)
	{
		if(rowList.Count <= i)
			return null;
		return rowList[i];
	}
	
	public Row Find_ID(string find)
	{
		return rowList.Find(x => x.ID == find);
	}
	public List<Row> FindAll_ID(string find)
	{
		return rowList.FindAll(x => x.ID == find);
	}
	public Row Find_ExtName(string find)
	{
		return rowList.Find(x => x.ExtName == find);
	}
	public List<Row> FindAll_ExtName(string find)
	{
		return rowList.FindAll(x => x.ExtName == find);
	}
	public Row Find_Name(string find)
	{
		return rowList.Find(x => x.Name == find);
	}
	public List<Row> FindAll_Name(string find)
	{
		return rowList.FindAll(x => x.Name == find);
	}
	public Row Find_ChemicalSeries(string find)
	{
		return rowList.Find(x => x.ChemicalSeries == find);
	}
	public List<Row> FindAll_ChemicalSeries(string find)
	{
		return rowList.FindAll(x => x.ChemicalSeries == find);
	}
	public Row Find_Type(string find)
	{
		return rowList.Find(x => x.Type == find);
	}
	public List<Row> FindAll_Type(string find)
	{
		return rowList.FindAll(x => x.Type == find);
	}
	public Row Find_Grade(string find)
	{
		return rowList.Find(x => x.Grade == find);
	}
	public List<Row> FindAll_Grade(string find)
	{
		return rowList.FindAll(x => x.Grade == find);
	}
	public Row Find_Lev(string find)
	{
		return rowList.Find(x => x.Lev == find);
	}
	public List<Row> FindAll_Lev(string find)
	{
		return rowList.FindAll(x => x.Lev == find);
	}
	public Row Find_AdventField(string find)
	{
		return rowList.Find(x => x.AdventField == find);
	}
	public List<Row> FindAll_AdventField(string find)
	{
		return rowList.FindAll(x => x.AdventField == find);
	}
	public Row Find_HP(string find)
	{
		return rowList.Find(x => x.HP == find);
	}
	public List<Row> FindAll_HP(string find)
	{
		return rowList.FindAll(x => x.HP == find);
	}
	public Row Find_AttackDamage(string find)
	{
		return rowList.Find(x => x.AttackDamage == find);
	}
	public List<Row> FindAll_AttackDamage(string find)
	{
		return rowList.FindAll(x => x.AttackDamage == find);
	}
	public Row Find_MonsterSkill1Name(string find)
	{
		return rowList.Find(x => x.MonsterSkill1Name == find);
	}
	public List<Row> FindAll_MonsterSkill1Name(string find)
	{
		return rowList.FindAll(x => x.MonsterSkill1Name == find);
	}
	public Row Find_MonsterSkill1Rate(string find)
	{
		return rowList.Find(x => x.MonsterSkill1Rate == find);
	}
	public List<Row> FindAll_MonsterSkill1Rate(string find)
	{
		return rowList.FindAll(x => x.MonsterSkill1Rate == find);
	}
	public Row Find_MonsterSkill2Name(string find)
	{
		return rowList.Find(x => x.MonsterSkill2Name == find);
	}
	public List<Row> FindAll_MonsterSkill2Name(string find)
	{
		return rowList.FindAll(x => x.MonsterSkill2Name == find);
	}
	public Row Find_MonsterSkill2Rate(string find)
	{
		return rowList.Find(x => x.MonsterSkill2Rate == find);
	}
	public List<Row> FindAll_MonsterSkill2Rate(string find)
	{
		return rowList.FindAll(x => x.MonsterSkill2Rate == find);
	}
	public Row Find_CriticalTarget(string find)
	{
		return rowList.Find(x => x.CriticalTarget == find);
	}
	public List<Row> FindAll_CriticalTarget(string find)
	{
		return rowList.FindAll(x => x.CriticalTarget == find);
	}
	public Row Find_RoomTemperatureStatus(string find)
	{
		return rowList.Find(x => x.RoomTemperatureStatus == find);
	}
	public List<Row> FindAll_RoomTemperatureStatus(string find)
	{
		return rowList.FindAll(x => x.RoomTemperatureStatus == find);
	}
	public Row Find_SolidGauge(string find)
	{
		return rowList.Find(x => x.SolidGauge == find);
	}
	public List<Row> FindAll_SolidGauge(string find)
	{
		return rowList.FindAll(x => x.SolidGauge == find);
	}
	public Row Find_LiquidGauge(string find)
	{
		return rowList.Find(x => x.LiquidGauge == find);
	}
	public List<Row> FindAll_LiquidGauge(string find)
	{
		return rowList.FindAll(x => x.LiquidGauge == find);
	}
	public Row Find_GasGauge(string find)
	{
		return rowList.Find(x => x.GasGauge == find);
	}
	public List<Row> FindAll_GasGauge(string find)
	{
		return rowList.FindAll(x => x.GasGauge == find);
	}
	public Row Find_RoomTemperaturePosition(string find)
	{
		return rowList.Find(x => x.RoomTemperaturePosition == find);
	}
	public List<Row> FindAll_RoomTemperaturePosition(string find)
	{
		return rowList.FindAll(x => x.RoomTemperaturePosition == find);
	}
	public Row Find_MosterMaximumCount(string find)
	{
		return rowList.Find(x => x.MosterMaximumCount == find);
	}
	public List<Row> FindAll_MosterMaximumCount(string find)
	{
		return rowList.FindAll(x => x.MosterMaximumCount == find);
	}
	public Row Find_NotAppartionCondition(string find)
	{
		return rowList.Find(x => x.NotAppartionCondition == find);
	}
	public List<Row> FindAll_NotAppartionCondition(string find)
	{
		return rowList.FindAll(x => x.NotAppartionCondition == find);
	}
	public Row Find_GoldRate(string find)
	{
		return rowList.Find(x => x.GoldRate == find);
	}
	public List<Row> FindAll_GoldRate(string find)
	{
		return rowList.FindAll(x => x.GoldRate == find);
	}
	public Row Find_MonsterCard1GainRate(string find)
	{
		return rowList.Find(x => x.MonsterCard1GainRate == find);
	}
	public List<Row> FindAll_MonsterCard1GainRate(string find)
	{
		return rowList.FindAll(x => x.MonsterCard1GainRate == find);
	}
	public Row Find_MonsterCard2GainRate(string find)
	{
		return rowList.Find(x => x.MonsterCard2GainRate == find);
	}
	public List<Row> FindAll_MonsterCard2GainRate(string find)
	{
		return rowList.FindAll(x => x.MonsterCard2GainRate == find);
	}
	
}/*
public class MonsterLoad : MonoBehaviour {
    public TextAsset file;
    public List<baseMonster> monsterList = new List<baseMonster>();
    List<string> row = new List<string>();
    List<List<string>> rowList = new List<List<string>>();
    bool isLoaded = false;
    void Awake()
    {
        Load(file);
        monsterList = new List<baseMonster>();
        ListSetting();

    }
    public void Load(TextAsset csv)
    {
        rowList.Clear();
        string[][] grid = CsvParser2.Parse(csv.text);
        for (int i = 1; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                row.Add(grid[i][j]);
            }
            rowList.Add(row);
        }
        isLoaded = true;
    }
    void ListSetting()
    {
        for (int i = 0; i < rowList.Count; i++)
        {
            baseMonster monster = new baseMonster();
			monster.Mon_ID = System.Convert.ToInt32(rowList[i][0]);
            monster.Mon_ExtName = rowList[i][1];
			monster.Mon_Name = rowList[i][2];
			monster.Mon_ChemicalSeries = rowList[i][3];
			monster.Mon_Type = rowList[i][4];
            monster.Mon_Grade = System.Convert.ToInt32(rowList[i][5]);
            monster.Mon_Level = System.Convert.ToInt32(rowList[i][6]);
            monster.Mon_AdventField = System.Convert.ToInt32(rowList[i][7]);
            monster.Mon_HP = System.Convert.ToInt32(rowList[i][8]);
			monster.Mon_AttackDamage = System.Convert.ToDouble(rowList[i][9]);
            monster.Mon_Skill1_Name = rowList[i][10];
            monster.Mon_Skill1_Rate = System.Convert.ToDouble(rowList[i][11]);
            monster.Mon_Skill2_Name = rowList[i][12];
            monster.Mon_Skill2_Rate = System.Convert.ToDouble(rowList[i][13]);
            monster.Mon_CriticalTarget = rowList[i][14];
            monster.Mon_RoomTempStatus = rowList[i][15];
            monster.Mon_SolidGauge = System.Convert.ToInt32(rowList[i][16]);
            monster.Mon_LiquidGauge = System.Convert.ToInt32(rowList[i][17]);
            monster.Mon_GasGauge = System.Convert.ToInt32(rowList[i][18]);
            monster.Mon_RoomTempPos = System.Convert.ToDouble(rowList[i][19]);
            monster.Mon_MaxCount = System.Convert.ToInt32(rowList[i][20]);
            monster.Mon_NotAppartionCond = rowList[i][21];
            monster.Mon_GoldRate = System.Convert.ToInt32(rowList[i][22]);
            monster.Mon_MonCard1GainRate = System.Convert.ToInt32(rowList[i][23]);
            monster.Mon_MonCard2GainRate = System.Convert.ToInt32(rowList[i][24]);

            monsterList.Add(monster);
        }

    }

}
*/
