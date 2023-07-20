using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [Range(3f, 7f)] public int PlayerMaxHealth;
    [Range(4f, 9f)] public float PlayerSpeed;
    [Range(7f, 15f)] public float BulletSpeed;
    [Range(0.1f, 0.8f)] public float FireCooldown;
}
