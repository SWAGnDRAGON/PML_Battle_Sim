using UnityEngine;

public class FloatingNumberSpawner : MonoBehaviour
{
    public static FloatingNumberSpawner Instance;

    public FloatingNumber prefab;

    void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// used to spawn floating damage/healing numbers
    /// </summary>
    /// <param name="value">integer value of damage/healing amount.</param>
    /// <param name="worldPos">transform of object spawning the number.</param>
    /// <param name="isHealing">whether or not this is a healing effect.</param>
    public static void Spawn(int value, Vector3 worldPos, bool isHealing, string dmgType = "physical", bool perfect = false)
    {
        var popup = Instantiate(Instance.prefab, worldPos, Quaternion.identity);
        popup.Initialize(value, isHealing, dmgType, perfect);
    }
}
