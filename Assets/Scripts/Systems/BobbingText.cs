using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BobbingText : MonoBehaviour
{
    [SerializeField] private string text = "";
    private int appendVal = 0;
    private List<Image> digits = new List<Image>();
    [SerializeField] private CustomFontAtlas spriteAtlas;
    [SerializeField] private GameObject charObj;
    [SerializeField] private float padding;
    [SerializeField] private float fontSize;
    [SerializeField] private AppendValue appendValType;
    [SerializeField] private TextAlign textAlignType;

    private enum AppendValue
    {
        NONE,
        SCORE,
        TARGET_SCORE,
        HIGH_SCORE
    }

    private enum TextAlign
    {
        LEFT,
        MIDDLE
    }

    private void Start()
    {
        RefreshSprites(true);
    }

    private void Update()
    {
        RefreshSprites(false);
    }

    public void SetText(string newText)
    {
        text = newText;
        RefreshSprites(true);
    }

    private bool UpdateAppendValue()
    {
        switch (appendValType)
        {
            case AppendValue.SCORE:
                {
                    bool changed = appendVal != GameManager.GetScore();
                    appendVal = GameManager.GetScore();
                    return changed;
                }
            case AppendValue.TARGET_SCORE:
                {
                    bool changed = appendVal != LevelManager.GetLevelTargetScore(GameManager.GetLevelID());
                    appendVal = LevelManager.GetLevelTargetScore(GameManager.GetLevelID());
                    return changed;
                }
            case AppendValue.HIGH_SCORE:
                {
                    bool changed = appendVal != LevelManager.GetLevelHighScore(GameManager.GetLevelID());
                    appendVal = LevelManager.GetLevelHighScore(GameManager.GetLevelID());
                    return changed;
                }
        }
        return false;
    }

    private void RefreshSprites(bool forceRefresh)
    {
        if (!UpdateAppendValue() && !forceRefresh)
        {
            // The append value hasn't changed, so neither should the text.
            return;
        }

        // The append value has changed! We need to refresh sprites

        // Update underlying text
        string fullText;
        if (appendValType == AppendValue.NONE)
        {
            fullText = text;
        }
        else
        {
            fullText = text + appendVal;
        }
        
        // Update number of images to fill and their positions on the canvas
        UpdateImageCount(fullText);
        UpdateImagePositions();
        // Fill images with corresponding sprites
        Debug.Log(text);
        for (int i = 0; i < fullText.Length; i++)
        {
            digits[i].sprite = spriteAtlas.GetCharSprite(fullText[i]);
        }
    }

    private void UpdateImageCount(string fullText)
    {
        while (digits.Count != fullText.Length)
        {
            if (digits.Count < fullText.Length)
            {
                // Not enough images to display text. Add a new image
                GameObject newDigit = Instantiate(charObj);
                newDigit.transform.SetParent(transform);
                RectTransform newDigitRect = newDigit.GetComponent<RectTransform>();
                newDigitRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 5 * fontSize);
                newDigitRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 9 * fontSize);
                digits.Add(newDigit.GetComponent<Image>());
            }
            else if (digits.Count > fullText.Length)
            {
                // Too many images for displaying text. Remove one
                GameObject toRemove = digits[digits.Count - 1].gameObject;
                digits.RemoveAt(digits.Count - 1);
                Destroy(toRemove);
            }
        }
    }

    private void UpdateImagePositions()
    {
        RectTransform myRect = GetComponent<RectTransform>();
        Vector2 myPos = myRect.anchoredPosition;

        if (textAlignType == TextAlign.LEFT)
        {
            for (int i = 0; i < digits.Count; i++)
            {
                RectTransform digitRect = digits[i].GetComponent<RectTransform>();
                digitRect.anchoredPosition = Vector2.right * digitRect.sizeDelta.x * i;
            }
        }
        else if (textAlignType == TextAlign.MIDDLE)
        {
            for (int i = 0; i < digits.Count; i++)
            {
                RectTransform digitRect = digits[i].GetComponent<RectTransform>();
                digitRect.anchoredPosition = Vector2.right * ((digitRect.sizeDelta.x * i) - digitRect.sizeDelta.x * 0.5f * (digits.Count - 1f));
            }
        }
    }
}
