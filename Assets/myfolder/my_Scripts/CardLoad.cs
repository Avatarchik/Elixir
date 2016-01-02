
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardLoad:MonoBehaviour {
	public TextAsset file;
	public List<baseCard> cardDeck;
	
	void Awake(){
		Load (file);
		cardDeck = new List<baseCard>();
		DeckSetting ();
	}

	//여기서부터 덱만들기

	void DeckSetting(){
		Debug.Log ("DeckSetting");
		for(int i=0;i<=19;i++){
			baseCard card = new baseCard();
			card.Card_ID=System.Convert.ToInt32(rowList[i].ID);
			card.Card_ExtName=rowList[i].ExtName;
			card.Card_Name=rowList[i].Name;
			card.Card_Chemical_Series=rowList[i].Chemical_Series;
			card.Card_Type=rowList[i].Type;
			card.Card_Description=rowList[i].Description;
			card.Card_Attack_Range=rowList[i].Attack_Range;
			if(rowList[i].Target!="N/A")
			card.Card_Target=System.Convert.ToInt32(rowList[i].Target);
			if(rowList[i].Attack_Damage!="N/A")
			card.Card_Attack_Damage=System.Convert.ToDouble (rowList[i].Attack_Damage);
			if(rowList[i].HP_Damage!="N/A")
			card.Card_HP_Damage=System.Convert.ToDouble (rowList[i].HP_Damage);
			if(rowList[i].Max_Damage!="N/A")
			card.Card_Max_Damage=System.Convert.ToDouble (rowList[i].Max_Damage);
			if(rowList[i].Dot_Damage_Turn!="N/A")
			card.Card_Dot_Damage_Turn=System.Convert.ToDouble (rowList[i].Dot_Damage_Turn);
			if(rowList[i].Heal!="N/A")
			card.Card_Heal=System.Convert.ToDouble (rowList[i].Heal);
			card.Card_Condition=rowList[i].Condition;
			card.Card_Additional_Card=rowList[i].Additional_Card;
			card.Card_Effect1_Name=rowList[i].Effect1_Name;
			if(rowList[i].Effect1_Rate!="N/A")
			card.Card_Effect1_Rate=System.Convert.ToDouble (rowList[i].Effect1_Rate);
			if(rowList[i].Effect1_Turn!="N/A")
			card.Card_Effect1_Turn=System.Convert.ToInt32 (rowList[i].Effect1_Turn);
			card.Card_Effect2_Name=rowList[i].Effect2_Name;
			if(rowList[i].Effect2_Rate!="N/A")
			card.Card_Effect2_Rate=System.Convert.ToDouble (rowList[i].Effect2_Rate);
			if(rowList[i].Effect2_Turn!="N/A")
			card.Card_Effect2_Turn=System.Convert.ToInt32 (rowList[i].Effect2_Turn);

			//cardDescription 치환하여 나타내기
			card.Card_Description=card.Card_Description.Replace ("Attack_Damage",rowList[i].Attack_Damage);
			card.Card_Description=card.Card_Description.Replace("HP_Damage",rowList[i].HP_Damage);
			card.Card_Description=card.Card_Description.Replace("Max_Damage",rowList[i].Max_Damage);
			card.Card_Description=card.Card_Description.Replace("Dot_Damage_Turn",rowList[i].Dot_Damage_Turn);
			card.Card_Description=card.Card_Description.Replace("Condition",rowList[i].Condition);
			card.Card_Description=card.Card_Description.Replace("Heal",rowList[i].Heal);
			card.Card_Description=card.Card_Description.Replace("Effect1_Rate",rowList[i].Effect1_Rate);
			card.Card_Description=card.Card_Description.Replace("Effect1_Turn",rowList[i].Effect1_Turn);
			card.Card_Description=card.Card_Description.Replace("Effect2_Rate",rowList[i].Effect2_Rate);
			card.Card_Description=card.Card_Description.Replace("Effect2_Turn",rowList[i].Effect2_Turn);


			cardDeck.Add(card);
		}

	}

	//여기까지 덱만들기
	


		public class Row
		{
			public string ID;
			public string ExtName;
			public string Name;
			public string Chemical_Series;
			public string Type;
			public string Description;
			public string Attack_Range;
			public string Target;
			public string Attack_Damage;
			public string HP_Damage;
			public string Max_Damage;
			public string Dot_Damage_Turn;
			public string Heal;
			public string Condition;
			public string Additional_Card;
			public string Effect1_Name;
			public string Effect1_Rate;
			public string Effect1_Turn;
			public string Effect2_Name;
			public string Effect2_Rate;
			public string Effect2_Turn;
			
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
				row.Chemical_Series = grid[i][3];
				row.Type = grid[i][4];
				row.Description = grid[i][5];
				row.Attack_Range = grid[i][6];
				row.Target = grid[i][7];
				row.Attack_Damage = grid[i][8];
				row.HP_Damage = grid[i][9];
				row.Max_Damage = grid[i][10];
				row.Dot_Damage_Turn = grid[i][11];
				row.Heal = grid[i][12];
				row.Condition = grid[i][13];
				row.Additional_Card = grid[i][14];
				row.Effect1_Name = grid[i][15];
				row.Effect1_Rate = grid[i][16];
				row.Effect1_Turn = grid[i][17];
				row.Effect2_Name = grid[i][18];
				row.Effect2_Rate = grid[i][19];
				row.Effect2_Turn = grid[i][20];
				
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
		public Row Find_Chemical_Series(string find)
		{
			return rowList.Find(x => x.Chemical_Series == find);
		}
		public List<Row> FindAll_Chemical_Series(string find)
		{
			return rowList.FindAll(x => x.Chemical_Series == find);
		}
		public Row Find_Type(string find)
		{
			return rowList.Find(x => x.Type == find);
		}
		public List<Row> FindAll_Type(string find)
		{
			return rowList.FindAll(x => x.Type == find);
		}
		public Row Find_Description(string find)
		{
			return rowList.Find(x => x.Description == find);
		}
		public List<Row> FindAll_Description(string find)
		{
			return rowList.FindAll(x => x.Description == find);
		}
		public Row Find_Attack_Range(string find)
		{
			return rowList.Find(x => x.Attack_Range == find);
		}
		public List<Row> FindAll_Attack_Range(string find)
		{
			return rowList.FindAll(x => x.Attack_Range == find);
		}
		public Row Find_Target(string find)
		{
			return rowList.Find(x => x.Target == find);
		}
		public List<Row> FindAll_Target(string find)
		{
			return rowList.FindAll(x => x.Target == find);
		}
		public Row Find_Attack_Damage(string find)
		{
			return rowList.Find(x => x.Attack_Damage == find);
		}
		public List<Row> FindAll_Attack_Damage(string find)
		{
			return rowList.FindAll(x => x.Attack_Damage == find);
		}
		public Row Find_HP_Damage(string find)
		{
			return rowList.Find(x => x.HP_Damage == find);
		}
		public List<Row> FindAll_HP_Damage(string find)
		{
			return rowList.FindAll(x => x.HP_Damage == find);
		}
		public Row Find_Max_Damage(string find)
		{
			return rowList.Find(x => x.Max_Damage == find);
		}
		public List<Row> FindAll_Max_Damage(string find)
		{
			return rowList.FindAll(x => x.Max_Damage == find);
		}
		public Row Find_Dot_Damage_Turn(string find)
		{
			return rowList.Find(x => x.Dot_Damage_Turn == find);
		}
		public List<Row> FindAll_Dot_Damage_Turn(string find)
		{
			return rowList.FindAll(x => x.Dot_Damage_Turn == find);
		}
		public Row Find_Heal(string find)
		{
			return rowList.Find(x => x.Heal == find);
		}
		public List<Row> FindAll_Heal(string find)
		{
			return rowList.FindAll(x => x.Heal == find);
		}
		public Row Find_Condition(string find)
		{
			return rowList.Find(x => x.Condition == find);
		}
		public List<Row> FindAll_Condition(string find)
		{
			return rowList.FindAll(x => x.Condition == find);
		}
		public Row Find_Additional_Card(string find)
		{
			return rowList.Find(x => x.Additional_Card == find);
		}
		public List<Row> FindAll_Additional_Card(string find)
		{
			return rowList.FindAll(x => x.Additional_Card == find);
		}
		public Row Find_Effect1_Name(string find)
		{
			return rowList.Find(x => x.Effect1_Name == find);
		}
		public List<Row> FindAll_Effect1_Name(string find)
		{
			return rowList.FindAll(x => x.Effect1_Name == find);
		}
		public Row Find_Effect1_Rate(string find)
		{
			return rowList.Find(x => x.Effect1_Rate == find);
		}
		public List<Row> FindAll_Effect1_Rate(string find)
		{
			return rowList.FindAll(x => x.Effect1_Rate == find);
		}
		public Row Find_Effect1_Turn(string find)
		{
			return rowList.Find(x => x.Effect1_Turn == find);
		}
		public List<Row> FindAll_Effect1_Turn(string find)
		{
			return rowList.FindAll(x => x.Effect1_Turn == find);
		}
		public Row Find_Effect2_Name(string find)
		{
			return rowList.Find(x => x.Effect2_Name == find);
		}
		public List<Row> FindAll_Effect2_Name(string find)
		{
			return rowList.FindAll(x => x.Effect2_Name == find);
		}
		public Row Find_Effect2_Rate(string find)
		{
			return rowList.Find(x => x.Effect2_Rate == find);
		}
		public List<Row> FindAll_Effect2_Rate(string find)
		{
			return rowList.FindAll(x => x.Effect2_Rate == find);
		}
		public Row Find_Effect2_Turn(string find)
		{
			return rowList.Find(x => x.Effect2_Turn == find);
		}
		public List<Row> FindAll_Effect2_Turn(string find)
		{
			return rowList.FindAll(x => x.Effect2_Turn == find);
		}
		
	}
