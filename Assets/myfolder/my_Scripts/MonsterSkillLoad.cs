using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumsAndClasses;

public class MonsterSkillLoad:MonoBehaviour
{

	public TextAsset file;

	void Awake(){
		Load (file);
	}

	public void SetConditionList(string SkillName, List<string> Condition1List, List<string> Condition2List, List<string> Condition3List){

		Debug.Log (Find_MonsterSkillID (SkillName).UseCondition1_1);
		Condition1List.Add ((Find_MonsterSkillID (SkillName).UseCondition1_2));
		Condition1List.Add ((Find_MonsterSkillID (SkillName).UseCondition1_2));
		Condition2List.Add ((Find_MonsterSkillID (SkillName).UseCondition2_1));
		Condition2List.Add ((Find_MonsterSkillID (SkillName).UseCondition2_2));
		Condition3List.Add ((Find_MonsterSkillID (SkillName).UseCondition3_1));
		Condition3List.Add ((Find_MonsterSkillID (SkillName).UseCondition3_2));

	}


	
	List<MonsterSkillRow> rowList = new List<MonsterSkillRow>();
	bool isLoaded = false;
	
	public bool IsLoaded()
	{
		return isLoaded;
	}
	
	public List<MonsterSkillRow> GetRowList()
	{
		return rowList;
	}
	
	public void Load(TextAsset csv)
	{
		rowList.Clear();
		string[][] grid = CsvParser2.Parse(csv.text);
		for(int i = 1 ; i < grid.Length ; i++)
		{
			MonsterSkillRow row = new MonsterSkillRow();
			row.no = grid[i][0];
			row.MonsterSkillID = grid[i][1];
			row.MonsterSkillName = grid[i][2];
			row.Target = grid[i][3];
			row.Range = grid[i][4];
			row.DamageFactor = grid[i][5];
			row.Heal = grid[i][6];
			row.TargetTempChange = grid[i][7];
			row.UseCondition1_1 = grid[i][8];
			row.UseCondition1_2 = grid[i][9];
			row.UseCondition2_1 = grid[i][10];
			row.UseCondition2_2 = grid[i][11];
			row.UseCondition3_1 = grid[i][12];
			row.UseCondition3_2 = grid[i][13];
			row.TargetStateChange = grid[i][14];
			row.DebuffName = grid[i][15];
			row.DebuffRate = grid[i][16];
			row.DebuffTurn = grid[i][17];
			row.DebuffEffect = grid[i][18];
			row.DotDamage = grid[i][19];
			row.DotDamageTurn = grid[i][20];
			row.BuffName = grid[i][21];
			row.BuffRate = grid[i][22];
			row.BuffTurn = grid[i][23];
			row.BuffEffect = grid[i][24];
			
			rowList.Add(row);
		}
		isLoaded = true;
	}
	
	public int NumRows()
	{
		return rowList.Count;
	}
	
	public MonsterSkillRow GetAt(int i)
	{
		if(rowList.Count <= i)
			return null;
		return rowList[i];
	}
	
	public MonsterSkillRow Find_no(string find)
	{
		return rowList.Find(x => x.no == find);
	}
	public List<MonsterSkillRow> FindAll_no(string find)
	{
		return rowList.FindAll(x => x.no == find);
	}
	public MonsterSkillRow Find_MonsterSkillID(string find)
	{
		return rowList.Find(x => x.MonsterSkillID == find);
	}
	public List<MonsterSkillRow> FindAll_MonsterSkillID(string find)
	{
		return rowList.FindAll(x => x.MonsterSkillID == find);
	}
	public MonsterSkillRow Find_MonsterSkillName(string find)
	{
		return rowList.Find(x => x.MonsterSkillName == find);
	}
	public List<MonsterSkillRow> FindAll_MonsterSkillName(string find)
	{
		return rowList.FindAll(x => x.MonsterSkillName == find);
	}
	public MonsterSkillRow Find_Target(string find)
	{
		return rowList.Find(x => x.Target == find);
	}
	public List<MonsterSkillRow> FindAll_Target(string find)
	{
		return rowList.FindAll(x => x.Target == find);
	}
	public MonsterSkillRow Find_Range(string find)
	{
		return rowList.Find(x => x.Range == find);
	}
	public List<MonsterSkillRow> FindAll_Range(string find)
	{
		return rowList.FindAll(x => x.Range == find);
	}
	public MonsterSkillRow Find_DamageFactor(string find)
	{
		return rowList.Find(x => x.DamageFactor == find);
	}
	public List<MonsterSkillRow> FindAll_DamageFactor(string find)
	{
		return rowList.FindAll(x => x.DamageFactor == find);
	}
	public MonsterSkillRow Find_Heal(string find)
	{
		return rowList.Find(x => x.Heal == find);
	}
	public List<MonsterSkillRow> FindAll_Heal(string find)
	{
		return rowList.FindAll(x => x.Heal == find);
	}
	public MonsterSkillRow Find_TargetTempChange(string find)
	{
		return rowList.Find(x => x.TargetTempChange == find);
	}
	public List<MonsterSkillRow> FindAll_TargetTempChange(string find)
	{
		return rowList.FindAll(x => x.TargetTempChange == find);
	}
	public MonsterSkillRow Find_UseCondition1_1(string find)
	{
		return rowList.Find(x => x.UseCondition1_1 == find);
	}
	public List<MonsterSkillRow> FindAll_UseCondition1_1(string find)
	{
		return rowList.FindAll(x => x.UseCondition1_1 == find);
	}
	public MonsterSkillRow Find_UseCondition1_2(string find)
	{
		return rowList.Find(x => x.UseCondition1_2 == find);
	}
	public List<MonsterSkillRow> FindAll_UseCondition1_2(string find)
	{
		return rowList.FindAll(x => x.UseCondition1_2 == find);
	}
	public MonsterSkillRow Find_UseCondition2_1(string find)
	{
		return rowList.Find(x => x.UseCondition2_1 == find);
	}
	public List<MonsterSkillRow> FindAll_UseCondition2_1(string find)
	{
		return rowList.FindAll(x => x.UseCondition2_1 == find);
	}
	public MonsterSkillRow Find_UseCondition2_2(string find)
	{
		return rowList.Find(x => x.UseCondition2_2 == find);
	}
	public List<MonsterSkillRow> FindAll_UseCondition2_2(string find)
	{
		return rowList.FindAll(x => x.UseCondition2_2 == find);
	}
	public MonsterSkillRow Find_UseCondition3_1(string find)
	{
		return rowList.Find(x => x.UseCondition3_1 == find);
	}
	public List<MonsterSkillRow> FindAll_UseCondition3_1(string find)
	{
		return rowList.FindAll(x => x.UseCondition3_1 == find);
	}
	public MonsterSkillRow Find_UseCondition3_2(string find)
	{
		return rowList.Find(x => x.UseCondition3_2 == find);
	}
	public List<MonsterSkillRow> FindAll_UseCondition3_2(string find)
	{
		return rowList.FindAll(x => x.UseCondition3_2 == find);
	}
	public MonsterSkillRow Find_TargetStateChange(string find)
	{
		return rowList.Find(x => x.TargetStateChange == find);
	}
	public List<MonsterSkillRow> FindAll_TargetStateChange(string find)
	{
		return rowList.FindAll(x => x.TargetStateChange == find);
	}
	public MonsterSkillRow Find_DebuffName(string find)
	{
		return rowList.Find(x => x.DebuffName == find);
	}
	public List<MonsterSkillRow> FindAll_DebuffName(string find)
	{
		return rowList.FindAll(x => x.DebuffName == find);
	}
	public MonsterSkillRow Find_DebuffRate(string find)
	{
		return rowList.Find(x => x.DebuffRate == find);
	}
	public List<MonsterSkillRow> FindAll_DebuffRate(string find)
	{
		return rowList.FindAll(x => x.DebuffRate == find);
	}
	public MonsterSkillRow Find_DebuffTurn(string find)
	{
		return rowList.Find(x => x.DebuffTurn == find);
	}
	public List<MonsterSkillRow> FindAll_DebuffTurn(string find)
	{
		return rowList.FindAll(x => x.DebuffTurn == find);
	}
	public MonsterSkillRow Find_DebuffEffect(string find)
	{
		return rowList.Find(x => x.DebuffEffect == find);
	}
	public List<MonsterSkillRow> FindAll_DebuffEffect(string find)
	{
		return rowList.FindAll(x => x.DebuffEffect == find);
	}
	public MonsterSkillRow Find_DotDamage(string find)
	{
		return rowList.Find(x => x.DotDamage == find);
	}
	public List<MonsterSkillRow> FindAll_DotDamage(string find)
	{
		return rowList.FindAll(x => x.DotDamage == find);
	}
	public MonsterSkillRow Find_DotDamageTurn(string find)
	{
		return rowList.Find(x => x.DotDamageTurn == find);
	}
	public List<MonsterSkillRow> FindAll_DotDamageTurn(string find)
	{
		return rowList.FindAll(x => x.DotDamageTurn == find);
	}
	public MonsterSkillRow Find_BuffName(string find)
	{
		return rowList.Find(x => x.BuffName == find);
	}
	public List<MonsterSkillRow> FindAll_BuffName(string find)
	{
		return rowList.FindAll(x => x.BuffName == find);
	}
	public MonsterSkillRow Find_BuffRate(string find)
	{
		return rowList.Find(x => x.BuffRate == find);
	}
	public List<MonsterSkillRow> FindAll_BuffRate(string find)
	{
		return rowList.FindAll(x => x.BuffRate == find);
	}
	public MonsterSkillRow Find_BuffTurn(string find)
	{
		return rowList.Find(x => x.BuffTurn == find);
	}
	public List<MonsterSkillRow> FindAll_BuffTurn(string find)
	{
		return rowList.FindAll(x => x.BuffTurn == find);
	}
	public MonsterSkillRow Find_BuffEffect(string find)
	{
		return rowList.Find(x => x.BuffEffect == find);
	}
	public List<MonsterSkillRow> FindAll_BuffEffect(string find)
	{
		return rowList.FindAll(x => x.BuffEffect == find);
	}
	
}