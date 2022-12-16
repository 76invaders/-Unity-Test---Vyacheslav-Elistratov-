using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card object")]

public class CardTemplate : ScriptableObject
{
    [SerializeField] internal Sprite cardArt;
    [SerializeField] internal Sprite cardCover;
    [SerializeField] internal string cardName;
    [SerializeField] internal string cardDescription;
    [SerializeField] internal int[] cardStats = new int[3];

    internal void RandomizeStats()
    {
        cardName = "Name";
        cardDescription = "Description";
        cardStats[(int)Values.Mana] = Random.Range(1, 10);
        cardStats[(int)Values.Attack] = Random.Range(1, 10);
        cardStats[(int)Values.Health] = Random.Range(1, 10);
    }
}