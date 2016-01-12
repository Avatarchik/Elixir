using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterSkillConditionLoad:MonoBehaviour{

	public TextAsset file;
	
	void Awake(){
		Load (file);
	}

	public class Row
	{
		public string no;
		public string UseCondition;
		public string Description;
		public string TargetState;
		public string HpBelowN;
		public string HpMoreN;
		public string Actionlimit;
		public string TargetNumber;
		public string RandomRate;
		
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
			row.UseCondition = grid[i][1];
			row.Description = grid[i][2];
			row.TargetState = grid[i][3];
			row.HpBelowN = grid[i][4];
			row.HpMoreN = grid[i][5];
			row.Actionlimit = grid[i][6];
			row.TargetNumber = grid[i][7];
			row.RandomRate = grid[i][8];
			
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
	public Row Find_UseCondition(string find)
	{
		return rowList.Find(x => x.UseCondition == find);
	}
	public List<Row> FindAll_UseCondition(string find)
	{
		return rowList.FindAll(x => x.UseCondition == find);
	}
	public Row Find_Description(string find)
	{
		return rowList.Find(x => x.Description == find);
	}
	public List<Row> FindAll_Description(string find)
	{
		return rowList.FindAll(x => x.Description == find);
	}
	public Row Find_TargetState(string find)
	{
		return rowList.Find(x => x.TargetState == find);
	}
	public List<Row> FindAll_TargetState(string find)
	{
		return rowList.FindAll(x => x.TargetState == find);
	}
	public Row Find_HpBelowN(string find)
	{
		return rowList.Find(x => x.HpBelowN == find);
	}
	public List<Row> FindAll_HpBelowN(string find)
	{
		return rowList.FindAll(x => x.HpBelowN == find);
	}
	public Row Find_HpMoreN(string find)
	{
		return rowList.Find(x => x.HpMoreN == find);
	}
	public List<Row> FindAll_HpMoreN(string find)
	{
		return rowList.FindAll(x => x.HpMoreN == find);
	}
	public Row Find_Actionlimit(string find)
	{
		return rowList.Find(x => x.Actionlimit == find);
	}
	public List<Row> FindAll_Actionlimit(string find)
	{
		return rowList.FindAll(x => x.Actionlimit == find);
	}
	public Row Find_TargetNumber(string find)
	{
		return rowList.Find(x => x.TargetNumber == find);
	}
	public List<Row> FindAll_TargetNumber(string find)
	{
		return rowList.FindAll(x => x.TargetNumber == find);
	}
	public Row Find_RandomRate(string find)
	{
		return rowList.Find(x => x.RandomRate == find);
	}
	public List<Row> FindAll_RandomRate(string find)
	{
		return rowList.FindAll(x => x.RandomRate == find);
	}
	
}