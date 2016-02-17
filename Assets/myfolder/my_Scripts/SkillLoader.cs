using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillLoader : MonoBehaviour {
    public TextAsset file;
    public List<baseSkill> skillList;

    public void Load()
    {
        string[][] grid = CsvParser2.Parse(file.text);
        for (int i = 1; i < grid.Length; i++)
        {
            baseSkill skill = new baseSkill();
            skill.Skill_ID = System.Convert.ToInt32(grid[i][0]);
            skill.Skill_ExtName = grid[i][1];
            skill.Skill_Name = grid[i][2];
            skill.Skill_Icon = grid[i][3];
            skill.Skill_Type = grid[i][4];
            skill.Skill_ElementNumber = System.Convert.ToInt32(grid[i][5]);
            skill.Skill_ProperLevel = System.Convert.ToInt32(grid[i][6]);
            skill.Skill_Description = grid[i][7];
            skill.Skill_Concept = grid[i][8];
            skill.Skill_Memo = grid[i][9];
            skill.Skill_Target = grid[i][10];
            skill.Skill_Range = grid[i][11];

            if (grid[i][12] != "N/A") skill.Skill_AttackDamage = System.Convert.ToSingle(grid[i][12]);
            if (grid[i][13] != "N/A") skill.Skill_AdditionalDamage = System.Convert.ToInt32(grid[i][13]);
            if (grid[i][14] != "N/A") skill.Skill_OptionalMinDamage = System.Convert.ToInt32(grid[i][14]);
            if (grid[i][15] != "N/A") skill.Skill_OptionalMaxDamage = System.Convert.ToInt32(grid[i][15]);
            if (grid[i][16] != "N/A") skill.Skill_Heal = System.Convert.ToSingle(grid[i][16]);
            if (grid[i][17] != "N/A") skill.Skill_AdditionalHeal = System.Convert.ToInt32(grid[i][17]);
            if (grid[i][18] != "N/A") skill.Skill_HealTurn = System.Convert.ToInt32(grid[i][18]);
            if (grid[i][19] != "N/A") skill.Skill_UserEffect = grid[i][19];
            if (grid[i][20] != "N/A") skill.Skill_UserEffectDescription = grid[i][20];
            if (grid[i][21] != "N/A") skill.Skill_UserEffectTiming = grid[i][21];
            if (grid[i][22] != "N/A") skill.Skill_SelfHealRate = System.Convert.ToSingle(grid[i][22]);
            if (grid[i][23] != "N/A") skill.Skill_SelfDamageRate = System.Convert.ToSingle(grid[i][23]);
            if (grid[i][24] != "N/A") skill.Skill_DebuffName = grid[i][24];
            if (grid[i][25] != "N/A") skill.Skill_DebuffRate = System.Convert.ToSingle(grid[i][25]);
            if (grid[i][26] != "N/A") skill.Skill_DebuffTurn = System.Convert.ToInt32(grid[i][26]);
            if (grid[i][27] != "N/A") skill.Skill_DebuffRateIncrease = System.Convert.ToSingle(grid[i][27]);
            if (grid[i][28] != "N/A") skill.Skill_DebuffEffect = grid[i][28];
            if (grid[i][29] != "N/A") skill.Skill_DotDamage = System.Convert.ToSingle(grid[i][29]);
            if (grid[i][30] != "N/A") skill.Skill_DotDamageTurn = System.Convert.ToInt32(grid[i][30]);
            if (grid[i][31] != "N/A") skill.Skill_BuffName = grid[i][31];
            if (grid[i][32] != "N/A") skill.Skill_BuffRate = System.Convert.ToSingle(grid[i][32]);
            if (grid[i][33] != "N/A") skill.Skill_BuffTurn = System.Convert.ToInt32(grid[i][33]);
            if (grid[i][34] != "N/A") skill.Skill_DamageResistance = System.Convert.ToSingle(grid[i][34]);
            if (grid[i][35] != "N/A") skill.Skill_BuffEffectRateIncrease = System.Convert.ToSingle(grid[i][35]);
            if (grid[i][36] != "N/A") skill.Skill_BuffEffect = grid[i][36];

            skillList.Add(skill);
        }
    }
}
