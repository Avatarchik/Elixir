
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
			card.Card_ChemicalSeries=rowList[i].ChemicalSeries;
			card.Card_Type=rowList[i].Type;
			card.Card_Description=rowList[i].Description;
			card.Card_Range=rowList[i].Range;
			card.Card_Target=rowList[i].Target;
			if(rowList[i].AttackDamage!="N/A")
			card.Card_AttackDamage=System.Convert.ToDouble (rowList[i].AttackDamage);
			if(rowList[i].IncreaseDamage!="N/A")
			card.Card_IncreaseDamage=System.Convert.ToDouble (rowList[i].IncreaseDamage);
			if(rowList[i].Heal!="N/A")
			card.Card_Heal=System.Convert.ToDouble (rowList[i].Heal);
			card.Card_ConditionCard=rowList[i].ConditionCard;
			if(rowList[i].ConditionState1!="N/A")
				card.Card_ConditionState1=System.Convert.ToInt32 (rowList[i].ConditionState1);
			if(rowList[i].ConditionState2!="N/A")
				card.Card_ConditionState2=System.Convert.ToInt32 (rowList[i].ConditionState2);
			card.Card_AdditionalCard=rowList[i].AdditionalCard;
			card.Card_DebuffName=rowList[i].DebuffName;
			if(rowList[i].DebuffRate!="N/A")
			card.Card_DebuffRate=System.Convert.ToDouble (rowList[i].DebuffRate);
			if(rowList[i].DebuffTurn!="N/A")
			card.Card_DebuffTurn=System.Convert.ToInt32 (rowList[i].DebuffTurn);
			if(rowList[i].DotDamage!="N/A")
			card.Card_DotDamage=System.Convert.ToDouble (rowList[i].DotDamage);
			card.Card_BuffName=rowList[i].BuffName;
			if(rowList[i].BuffRate!="N/A")
			card.Card_BuffRate=System.Convert.ToDouble (rowList[i].BuffRate);
			if(rowList[i].BuffTurn!="N/A")
			card.Card_BuffTurn=System.Convert.ToInt32 (rowList[i].BuffTurn);
			if(rowList[i].SummonedCreatureHP!="N/A")
				card.Card_SummonedCreatureHP=System.Convert.ToDouble(rowList[i].SummonedCreatureHP);
			if(rowList[i].SummonedCreatureAttackDamage!="N/A")
				card.Card_SummonedCreatureAttackDamage=System.Convert.ToDouble(rowList[i].SummonedCreatureAttackDamage);
			card.Card_EliminateBuff=rowList[i].EliminateBuff;
			card.Card_EliminateDebuff=rowList[i].EliminateDebuff;

			//cardDescription 치환하여 나타내기
			card.Card_Description=card.Card_Description.Replace ("AttackDamage",rowList[i].AttackDamage);
			card.Card_Description=card.Card_Description.Replace ("DotDamage",rowList[i].DotDamage);
			card.Card_Description=card.Card_Description.Replace("ConditionCard",rowList[i].ConditionCard);
			card.Card_Description=card.Card_Description.Replace("Heal",rowList[i].Heal);
			card.Card_Description=card.Card_Description.Replace("DebuffRate",rowList[i].DebuffRate);
			card.Card_Description=card.Card_Description.Replace("DebuffTurn",rowList[i].DebuffTurn);
			card.Card_Description=card.Card_Description.Replace("BuffRate",rowList[i].BuffRate);
			card.Card_Description=card.Card_Description.Replace("BuffTurn",rowList[i].BuffTurn);
			card.Card_Description=card.Card_Description.Replace ("SummonedCreatureHP",rowList[i].SummonedCreatureHP);
			card.Card_Description=card.Card_Description.Replace ("SummonedCreatureAttackDamage",rowList[i].SummonedCreatureAttackDamage);


			cardDeck.Add(card);
		}

	}

	//여기까지 덱만들기

		public class Row
		{
			public string ID;
			public string ExtName;
			public string Name;
			public string ChemicalSeries;
			public string Type;
			public string Description;
			public string Target;
			public string Range;
			public string AttackDamage;
			public string IncreaseDamage;
			public string AllyAdditionalDamageRate;
			public string Heal;
			public string ConditionCard;
			public string ConditionTurn;
			public string ConditionState1;
			public string ConditionState2;
			public string AdditionalCard;
			public string DebuffName;
			public string DebuffRate;
			public string DebuffTurn;
			public string DotDamage;
			public string BuffName;
			public string BuffRate;
			public string BuffTurn;
			public string SummonedCreatureHP;
			public string SummonedCreatureAttackDamage;
			public string EliminateDebuff;
			public string EliminateBuff;
			
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
				row.Description = grid[i][5];
				row.Target = grid[i][6];
				row.Range = grid[i][7];
				row.AttackDamage = grid[i][8];
				row.IncreaseDamage = grid[i][9];
				row.AllyAdditionalDamageRate = grid[i][10];
				row.Heal = grid[i][11];
				row.ConditionCard = grid[i][12];
				row.ConditionTurn = grid[i][13];
				row.ConditionState1 = grid[i][14];
				row.ConditionState2 = grid[i][15];
				row.AdditionalCard = grid[i][16];
				row.DebuffName = grid[i][17];
				row.DebuffRate = grid[i][18];
				row.DebuffTurn = grid[i][19];
				row.DotDamage = grid[i][20];
				row.BuffName = grid[i][21];
				row.BuffRate = grid[i][22];
				row.BuffTurn = grid[i][23];
				row.SummonedCreatureHP = grid[i][24];
				row.SummonedCreatureAttackDamage = grid[i][25];
				row.EliminateDebuff = grid[i][26];
				row.EliminateBuff = grid[i][27];
				
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
		public Row Find_Description(string find)
		{
			return rowList.Find(x => x.Description == find);
		}
		public List<Row> FindAll_Description(string find)
		{
			return rowList.FindAll(x => x.Description == find);
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
		public Row Find_AttackDamage(string find)
		{
			return rowList.Find(x => x.AttackDamage == find);
		}
		public List<Row> FindAll_AttackDamage(string find)
		{
			return rowList.FindAll(x => x.AttackDamage == find);
		}
		public Row Find_IncreaseDamage(string find)
		{
			return rowList.Find(x => x.IncreaseDamage == find);
		}
		public List<Row> FindAll_IncreaseDamage(string find)
		{
			return rowList.FindAll(x => x.IncreaseDamage == find);
		}
		public Row Find_AllyAdditionalDamageRate(string find)
		{
			return rowList.Find(x => x.AllyAdditionalDamageRate == find);
		}
		public List<Row> FindAll_AllyAdditionalDamageRate(string find)
		{
			return rowList.FindAll(x => x.AllyAdditionalDamageRate == find);
		}
		public Row Find_Heal(string find)
		{
			return rowList.Find(x => x.Heal == find);
		}
		public List<Row> FindAll_Heal(string find)
		{
			return rowList.FindAll(x => x.Heal == find);
		}
		public Row Find_ConditionCard(string find)
		{
			return rowList.Find(x => x.ConditionCard == find);
		}
		public List<Row> FindAll_ConditionCard(string find)
		{
			return rowList.FindAll(x => x.ConditionCard == find);
		}
		public Row Find_ConditionTurn(string find)
		{
			return rowList.Find(x => x.ConditionTurn == find);
		}
		public List<Row> FindAll_ConditionTurn(string find)
		{
			return rowList.FindAll(x => x.ConditionTurn == find);
		}
		public Row Find_ConditionState1(string find)
		{
			return rowList.Find(x => x.ConditionState1 == find);
		}
		public List<Row> FindAll_ConditionState1(string find)
		{
			return rowList.FindAll(x => x.ConditionState1 == find);
		}
		public Row Find_ConditionState2(string find)
		{
			return rowList.Find(x => x.ConditionState2 == find);
		}
		public List<Row> FindAll_ConditionState2(string find)
		{
			return rowList.FindAll(x => x.ConditionState2 == find);
		}
		public Row Find_AdditionalCard(string find)
		{
			return rowList.Find(x => x.AdditionalCard == find);
		}
		public List<Row> FindAll_AdditionalCard(string find)
		{
			return rowList.FindAll(x => x.AdditionalCard == find);
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
		public Row Find_DotDamage(string find)
		{
			return rowList.Find(x => x.DotDamage == find);
		}
		public List<Row> FindAll_DotDamage(string find)
		{
			return rowList.FindAll(x => x.DotDamage == find);
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
		public Row Find_SummonedCreatureHP(string find)
		{
			return rowList.Find(x => x.SummonedCreatureHP == find);
		}
		public List<Row> FindAll_SummonedCreatureHP(string find)
		{
			return rowList.FindAll(x => x.SummonedCreatureHP == find);
		}
		public Row Find_SummonedCreatureAttackDamage(string find)
		{
			return rowList.Find(x => x.SummonedCreatureAttackDamage == find);
		}
		public List<Row> FindAll_SummonedCreatureAttackDamage(string find)
		{
			return rowList.FindAll(x => x.SummonedCreatureAttackDamage == find);
		}
		public Row Find_EliminateDebuff(string find)
		{
			return rowList.Find(x => x.EliminateDebuff == find);
		}
		public List<Row> FindAll_EliminateDebuff(string find)
		{
			return rowList.FindAll(x => x.EliminateDebuff == find);
		}
		public Row Find_EliminateBuff(string find)
		{
			return rowList.Find(x => x.EliminateBuff == find);
		}
		public List<Row> FindAll_EliminateBuff(string find)
		{
			return rowList.FindAll(x => x.EliminateBuff == find);
		}
		
	}
