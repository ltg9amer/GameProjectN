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
    private int hailCount = 1;
    private int heavySnowCount = 1;

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

        GameManager.instance.jumpCount = 48;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            disasterDictionary["Typhoon"]?.onPlay.Invoke();
        }

        if (GameManager.instance.jumpCount > 0 && (GameManager.instance.jumpCount %= 50) == 0)
        {
            disasterDictionary["ColdWave"]?.onPlay.Invoke();
        }

        if (GameManager.instance.PlayTime / hailDelay > hailCount)
        {
            hailCount++;
            disasterDictionary["Hail"]?.onPlay.Invoke();
        }

        if (GameManager.instance.PlayTime / heavySnowDelay > heavySnowCount)
        {
            heavySnowCount++;
            disasterDictionary["HeavySnow"]?.onPlay.Invoke();
        }
    }
}
