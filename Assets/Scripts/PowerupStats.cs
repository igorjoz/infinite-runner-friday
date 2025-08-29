using UnityEngine;

[CreateAssetMenu(fileName = "PowerupStats", menuName = "Powerup/PowerupStats")]
public class PowerupStats : ScriptableObject
{
    [SerializeField]
    private float value;

    public float GetValue()
    {
        return value;
    }
}
