using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameAnimation : MonoBehaviour
{
    [SerializeField] SlimeAnimationManager slimeAnimMan;

    private void Awake()
    {
        GameManager.gameAnimManager = this;
    }

    public void HideSlime()
    {
        slimeAnimMan.HideAnim();
    }
}
