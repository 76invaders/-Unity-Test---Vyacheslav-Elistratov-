using System.Collections.Generic;
using UnityEngine;

public class CardsStatsController : MonoBehaviour
{
    List<GameObject> _cardsInHand;

    int _handFullness;
    int _selectedCard = 0;

    private void Awake()
    {
        _cardsInHand = gameObject.GetComponent<HandDataStorage>().cardsInHand;
    }

    public void RandomizeValueIteration()
    {
        _handFullness = _cardsInHand.Count - 1;
        _selectedCard = _handFullness < _selectedCard ? (0) : (_selectedCard);

        if (_handFullness >= 0)
        {
            _cardsInHand[_selectedCard].GetComponent<CardDataManager>().RandomizeSomeValue(_selectedCard);
        }

        _selectedCard = _handFullness == _cardsInHand.Count-1 ? (_selectedCard+1):(_selectedCard);
    }
}