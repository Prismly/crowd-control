using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenExterior : MonoBehaviour
{
    [SerializeField] private List<Material> levelMats;
    [SerializeField] private List<MeshRenderer> renderers;

    public void UseMatForLvl(int levelNum)
    {
        Material matToUse = levelMats[levelNum - 1];
        foreach (MeshRenderer mr in renderers)
        {
            mr.material = matToUse;
        }
    }
}
