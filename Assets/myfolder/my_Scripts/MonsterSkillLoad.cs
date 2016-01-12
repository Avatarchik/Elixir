﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterSkillLoad:MonoBehaviour
{
	public TextAsset file;
	
	void Awake(){
		Load (file);
	}
	public class Row
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
	
	public Row GetAt(int i)
	{
		if(rowList.Count <= i)
			return null;
		return rowList[i];
	}
	
	public Row Find_no(string find)
	{
		return rowList.Find(x => x.no == find);
	}
	public List<Row> FindAll_no(string find)
	{
		return rowList.FindAll(x => x.no == find);
	}
	public Row Find_MonsterSkillID(string find)
	{
		return rowList.Find(x => x.MonsterSkillID == find);
	}
	public List<Row> FindAll_MonsterSkillID(string find)
	{
		return rowList.FindAll(x => x.MonsterSkillID == find);
	}
	public Row Find_MonsterSkillName(string find)
	{
		return rowList.Find(x => x.MonsterSkillName == find);
	}
	public List<Row> FindAll_MonsterSkillName(string find)
	{
		return rowList.FindAll(x => x.MonsterSkillName == find);
	}
	public Row Find_Target(string find)
	{
		return rowList.Find(x => x.Target == find);
	}
	public List<Row> FindAll_Target(string find)
	{
		return rowList.FindAll(x => x.Target == find);
	}
	public Row Find_Range(string find)
	{
		return rowList.Find(x => x.Range == find);
	}
	public List<Row> FindAll_Range(string find)
	{
		return rowList.FindAll(x => x.Range == find);
	}
	public Row Find_DamageFactor(string find)
	{
		return rowList.Find(x => x.DamageFactor == find);
	}
	public List<Row> FindAll_DamageFactor(string find)
	{
		return rowList.FindAll(x => x.DamageFactor == find);
	}
	public Row Find_Heal(string find)
	{
		return rowList.Find(x => x.Heal == find);
	}
	public List<Row> FindAll_Heal(string find)
	{
		return rowList.FindAll(x => x.Heal == find);
	}
	public Row Find_TargetTempChange(string find)
	{
		return rowList.Find(x => x.TargetTempChange == find);
	}
	public List<Row> FindAll_TargetTempChange(string find)
	{
		return rowList.FindAll(x => x.TargetTempChange == find);
	}
	public Row Find_UseCondition1_1(string find)
	{
		return rowList.Find(x => x.UseCondition1_1 == find);
	}
	public List<Row> FindAll_UseCondition1_1(string find)
	{
		return rowList.FindAll(x => x.UseCondition1_1 == find);
	}
	public Row Find_UseCondition1_2(string find)
	{
		return rowList.Find(x => x.UseCondition1_2 == find);
	}
	public List<Row> FindAll_UseCondition1_2(string find)
	{
		return rowList.FindAll(x => x.UseCondition1_2 == find);
	}
	public Row Find_UseCondition2_1(string find)
	{
		return rowList.Find(x => x.UseCondition2_1 == find);
	}
	public List<Row> FindAll_UseCondition2_1(string find)
	{
		return rowList.FindAll(x => x.UseCondition2_1 == find);
	}
	public Row Find_UseCondition2_2(string find)
	{
		return rowList.Find(x => x.UseCondition2_2 == find);
	}
	public List<Row> FindAll_UseCondition2_2(string find)
	{
		return rowList.FindAll(x => x.UseCondition2_2 == find);
	}
	public Row Find_UseCondition3_1(string find)
	{
		return rowList.Find(x => x.UseCondition3_1 == find);
	}
	public List<Row> FindAll_UseCondition3_1(string find)
	{
		return rowList.FindAll(x => x.UseCondition3_1 == find);
	}
	public Row Find_UseCondition3_2(string find)
	{
		return rowList.Find(x => x.UseCondition3_2 == find);
	}
	public List<Row> FindAll_UseCondition3_2(string find)
	{
		return rowList.FindAll(x => x.UseCondition3_2 == find);
	}
	public Row Find_TargetStateChange(string find)
	{
		return rowList.Find(x => x.TargetStateChange == find);
	}
	public List<Row> FindAll_TargetStateChange(string find)
	{
		return rowList.FindAll(x => x.TargetStateChange == find);
	}
	public Row Find_DebuffName(string find)
	{
		return rowList.Find(x => x.DebuffName == find);
	}
	public List<Row> FindAll_DebuffName(string find)
	{
		return rowList.FindAll(x => x.DebuffName == find);
	}
	public Row Find_DebuffRate(string find)
	{
		return rowList.Find(x => x.DebuffRate == find);
	}
	public List<Row> FindAll_DebuffRate(string find)
	{
		return rowList.FindAll(x => x.DebuffRate == find);
	}
	public Row Find_DebuffTurn(string find)
	{
		return rowList.Find(x => x.DebuffTurn == find);
	}
	public List<Row> FindAll_DebuffTurn(string find)
	{
		return rowList.FindAll(x => x.DebuffTurn == find);
	}
	public Row Find_DebuffEffect(string find)
	{
		return rowList.Find(x => x.DebuffEffect == find);
	}
	public List<Row> FindAll_DebuffEffect(string find)
	{
		return rowList.FindAll(x => x.DebuffEffect == find);
	}
	public Row Find_DotDamage(string find)
	{
		return rowList.Find(x => x.DotDamage == find);
	}
	public List<Row> FindAll_DotDamage(string find)
	{
		return rowList.FindAll(x => x.DotDamage == find);
	}
	public Row Find_DotDamageTurn(string find)
	{
		return rowList.Find(x => x.DotDamageTurn == find);
	}
	public List<Row> FindAll_DotDamageTurn(string find)
	{
		return rowList.FindAll(x => x.DotDamageTurn == find);
	}
	public Row Find_BuffName(string find)
	{
		return rowList.Find(x => x.BuffName == find);
	}
	public List<Row> FindAll_BuffName(string find)
	{
		return rowList.FindAll(x => x.BuffName == find);
	}
	public Row Find_BuffRate(string find)
	{
		return rowList.Find(x => x.BuffRate == find);
	}
	public List<Row> FindAll_BuffRate(string find)
	{
		return rowList.FindAll(x => x.BuffRate == find);
	}
	public Row Find_BuffTurn(string find)
	{
		return rowList.Find(x => x.BuffTurn == find);
	}
	public List<Row> FindAll_BuffTurn(string find)
	{
		return rowList.FindAll(x => x.BuffTurn == find);
	}
	public Row Find_BuffEffect(string find)
	{
		return rowList.Find(x => x.BuffEffect == find);
	}
	public List<Row> FindAll_BuffEffect(string find)
	{
		return rowList.FindAll(x => x.BuffEffect == find);
	}
	
}