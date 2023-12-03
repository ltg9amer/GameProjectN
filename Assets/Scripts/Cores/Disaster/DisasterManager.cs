using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DisasterManager : MonoBehaviour
{
    static public DisasterManager instance = null;
    [SerializeField]
    private float hailDelay = 13f;
    [SerializeField]
    private float heavySnowDelay = 30f;
    [SerializeField]
    private float typhoonDelay = 5f;
    public float TyphoonDelay => typhoonDelay;
    [SerializeField]
    private float tidalWaveDelay = 3f;
    public float TidalWaveDelay => tidalWaveDelay;
    private Dictionary<string, Disaster> disasterDictionary = new Dictionary<string, Disaster>();
    public Dictionary<string, Disaster> DisasterDictionary => disasterDictionary;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        List<Disaster> disasters = GetComponentsInChildren<Disaster>().ToList();

        foreach (var disaster in disasters)
        {
            disasterDictionary.Add(disaster.name, disaster);
        }
    }

    private void Update()
    {
        if (GameManager.instance.CharacterJump.jumpCount > 0 && (GameManager.instance.CharacterJump.jumpCount %= 50) == 0)
        {
            disasterDictionary["ColdWave"]?.onPlay.Invoke();
        }

        if (GameManager.instance.currentUserData.PlayTime / hailDelay > GameManager.instance.currentUserData.HailCount)
        {
            GameManager.instance.currentUserData.HailCount++;
            disasterDictionary["Hail"]?.onPlay.Invoke();
        }

        if (GameManager.instance.currentUserData.PlayTime / heavySnowDelay > GameManager.instance.currentUserData.HeavySnowCount)
        {
            GameManager.instance.currentUserData.HeavySnowCount++;
            disasterDictionary["HeavySnow"]?.onPlay.Invoke();
        }
    }
}
