using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterLoad : MonoBehaviour {
    public TextAsset file;
    public List<baseMonster> monsterList = new List<baseMonster>();
    List<string> row = new List<string>();
    List<List<string>> rowList = new List<List<string>>();
    bool isLoaded = false;
    void Awake()
    {
        Debug.Log("Load Monste Database");
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

