using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator slime;
    [SerializeField] private Material[] slimeColors;
    [SerializeField] private SkinnedMeshRenderer slimeSkin;

    private void Start()
    {
        // Randomize color of slime
        slimeSkin.material = slimeColors[Random.Range(0, 4)];
    }

    public void HideAnim()
    {
        slime.SetTrigger("Level Over");
    }

    public void ChompAnim()
    {
        slime.SetTrigger("Food Eaten");
    }
}
