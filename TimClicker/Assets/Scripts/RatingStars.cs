using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class RatingStars : MonoBehaviour
{
    #region Parameters 

    public Image[] Stars;
    public Sprite StarOff_Sprite;
    public Sprite StarOn_Sprite;
    public int StarsCount;

    #endregion 

    private void Update()
    {
        CountAchievedStars(StarsCount);
    }

    private void CountAchievedStars(int count)
    {
        if (StarsCount >= 0 && StarsCount <= 5)
        {
            foreach (var item in Stars)
            {
                item.sprite = StarOff_Sprite;
            }

            for (var i = 0; i < count; i++)
            {
                Stars[i].sprite = StarOn_Sprite;
            }
        }
    }

    private void ResetAllStars()
    {
        foreach (var item in Stars)
        {
            item.sprite = StarOff_Sprite;
        }
    }

}
