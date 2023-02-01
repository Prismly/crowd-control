using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenuScroller : MonoBehaviour
{
    private RectTransform myRect;
    [SerializeField] private float timeToScroll = 1f;
    private bool isScrolling = false;
    private float scrollSpd = 0;

    private void Start()
    {
        myRect = GetComponent<RectTransform>();
        float totalTravel = Screen.width + myRect.sizeDelta.x;
        scrollSpd = totalTravel / timeToScroll;
    }

    private void Update()
    {
        if (isScrolling)
        {
            myRect.position = myRect.position - (Vector3.right * scrollSpd * Time.deltaTime);
        }
    }

    // scrollInc is -1 if "scroll left", 1 if "scroll right"
    public void EnableScroll(int scrollInc)
    {
        isScrolling = true;
    }
}
