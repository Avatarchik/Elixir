using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnumsAndClasses;

public class MonsterSkillConditionLoad:MonoBehaviour{

	public TextAsset file;
	
	void Awake(){
		Load (file);

	}


	
	List<MonsterSkillConditionRow> rowList = new List<MonsterSkillConditionRow>();
	bool isLoaded = false;
	
	public bool IsLoaded()
	{
		return isLoaded;
	}
	
	public List<MonsterSkillConditionRow> GetRowList()
	{
		return rowList;
	}
	
	public void Load(TextAsset csv)
	{
		rowList.Clear();
		string[][] grid = CsvParser2.Parse(csv.text);
		for(int i = 1 ; i < grid.Length ; i++)
		{
			MonsterSkillConditionRow row = new MonsterSkillConditionRow();
			if(grid[i][0]!="N/A")
			row.no = System.Convert.ToInt32(grid[i][0]);
			row.UseCondition = grid[i][1];
			row.Description = grid[i][2];
			row.TargetState = grid[i][3];
			if(grid[i][4]!="N/A")
			row.HpBelowN = System.Convert.ToInt32(grid[i][4]);
			if(grid[i][5]!="N/A")
			row.HpMoreN = System.Convert.ToInt32(grid[i][5]);
			row.Actionlimit = grid[i][6];
			if(grid[i][7]!="N/A")
			row.TargetNumber = System.Convert.ToInt32(grid[i][7]);
			if(grid[i][8]!="N/A")
			row.RandomRate = System.Convert.ToInt32(grid[i][8]);
			
			rowList.Add(row);
		}
		isLoaded = true;
	}
	
	public int NumRows()
	{
		return rowList.Count;
	}
	
	public MonsterSkillConditionRow GetAt(int i)
	{
		if(rowList.Count <= i)
			return null;
		return rowList[i];
	}
	

	public MonsterSkillConditionRow Find_UseCondition(string find)
	{
		return rowList.Find(x => x.UseCondition == find);
	}
	public List<MonsterSkillConditionRow> FindAll_UseCondition(string find)
	{
		return rowList.FindAll(x => x.UseCondition == find);
	}
	
}