using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    new public string name = "New Card";
    public Sprite cardArt = null;

    // 0 = action; 1 = character; 2 = item; 3 = gear; 4 = weapon; 
    public int type_acigw;

    // which # the card is out of all the cards. Also the index it is in the entire set of cards (+1).
    public int number;

    public int cost;
    public int attack;

    public virtual void Use()
    {
        // create override for each item
        Debug.Log("Card Activated" + name);
    }
}
