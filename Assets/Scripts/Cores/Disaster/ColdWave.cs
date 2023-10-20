using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdWave : Disaster
{
    [SerializeField]
    private float coldWaveLimitTime = 3f;
    private bool frozen = false;
    private float coldWaveTimer;

    private void Update()
    {
        coldWaveTimer -= Time.deltaTime;

        if (coldWaveTimer <= 0f && frozen)
        {
            //�ڱ� ���̱�
        }
    }

    public override void PlayDisaster()
    {
        //���� �ɱ�
        frozen = true;
        coldWaveTimer = coldWaveLimitTime;
    }
}
