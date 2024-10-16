using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/Skill")]
public class Skill : ScriptableObject
{
    public string skillName;
    public int damage;
    public bool isOffensive;
}
