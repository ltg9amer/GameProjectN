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
    private Disaster disaster;
    public Disaster Disaster
    {
        get
        {
            return disaster;
        }

        set
        {
            disaster = value;

            disaster.onPlay?.Invoke();
        }
    }
    private int hailCount;
    private int heavySnowCount;

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
        if (GameManager.instance.JumpCount > 0 && GameManager.instance.JumpCount % 50 == 0)
        {
            Disaster = disasterDictionary["ColdWave"];
        }

        if (GameManager.instance.PlayTime / hailDelay > hailCount)
        {
            hailCount++;
            Disaster = disasterDictionary["Hail"];
        }

        if (GameManager.instance.PlayTime / heavySnowDelay > heavySnowCount)
        {
            heavySnowCount++;
            Disaster = disasterDictionary["HeavySnow"];
        }
    }
}
