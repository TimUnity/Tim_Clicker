using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Randomizer : MonoBehaviour
{
    #region Parameters

    public Image leveckBack;
    public Image levelIcon;
    public TextMeshProUGUI LevelName;

    [SerializeField] private Sprite[] levelBackSprites;
    [SerializeField] private Sprite[] levelIconSprites;
    #endregion

    private void Start()
    {
        leveckBack.sprite = levelBackSprites[Random.Range(0, levelBackSprites.Length)];
        levelIcon.sprite = levelIconSprites[Random.Range(0, levelIconSprites.Length)];
    }
}
