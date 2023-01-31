using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomFontAtlas : MonoBehaviour
{
    [SerializeField] Sprite[] char_numbers;
    [SerializeField] Sprite[] char_uppers;
    [SerializeField] Sprite char_colon;
    [SerializeField] Sprite char_plus;
    [SerializeField] Sprite char_hyphen;
    [SerializeField] Sprite char_space;

    public Sprite GetCharSprite(char target)
    {
        if (target >= '0' && target <= '9')
        {
            // Number.
            return char_numbers[target - '0'];
        }
        else if (target >= 'A' && target <= 'Z')
        {
            // Uppercase letter.
            return char_uppers[target - 'A'];
        }
        else if (target == ':')
        {
            return char_colon;
        }
        else if (target == '+')
        {
            return char_plus;
        }
        else if (target == '-')
        {
            return char_hyphen;
        }
        else
        {
            // Fallback sprite.
            return char_space;
        }
    }
}
