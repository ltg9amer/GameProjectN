using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointReached : MonoBehaviour
{
    [SerializeField]
    private Sprite reachedCheckpointFlag;
    [SerializeField]
    private CheckPoint checkpoint;

    public void OnReachCheckpointHandle()
    {
        GameManager.instance.currentUserData.CheckpointCount = checkpoint.CheckPointOrder;
        GetComponent<SpriteRenderer>().sprite = reachedCheckpointFlag;
    }
}
