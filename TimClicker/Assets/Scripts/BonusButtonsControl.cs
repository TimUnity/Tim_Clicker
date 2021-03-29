using UnityEngine;
using DG.Tweening; 

public class BonusButtonsControl : MonoBehaviour
{
    #region Parameters

    public GameObject bonusBTN_LongDelay;
    public GameObject bonusBTN_FruitZoom;
    public GameObject bonusBTN_DoubleScore;

    [Space] [SerializeField] private float delay = 0.25f;

    #endregion

    public void SwitchBonusStatus(string bonusName, bool bonusON)
    {
        if (bonusON)
        {  
            switch (bonusName)
            {
                case "LongDelay":
                    SwitchBTN_Position(bonusBTN_LongDelay, true);
                    break;
                case "DoubleScore":
                    SwitchBTN_Position(bonusBTN_DoubleScore, true);
                    break;
                case "BigTarget":
                    SwitchBTN_Position(bonusBTN_FruitZoom, true);
                    break;
            } 
        }
        else
        { 
            switch (bonusName)
            {
                case "LongDelay":
                    SwitchBTN_Position(bonusBTN_LongDelay, false);
                    break;
                case "DoubleScore":
                    SwitchBTN_Position(bonusBTN_DoubleScore, false);
                    break;
                case "BigTarget":
                    SwitchBTN_Position(bonusBTN_FruitZoom, false);
                    break;
            } 
        } 
    }

    private void SwitchBTN_Position(GameObject button, bool isON)
    {
        float moveOffset;
        if (isON) { moveOffset = 420; }
        else { moveOffset = 655; }
          
        //Switching buttons position
        button.transform.DOLocalMoveX(/*button.transform.localPosition.x - */moveOffset, 0.25f);
    }
}
