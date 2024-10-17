using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/Skill")]
public class Skill : ScriptableObject
{
    public string skillName;
    public string skillDescription;

    public int damage;
    public bool isOffensive;
    public Sprite skillSprite;
}
