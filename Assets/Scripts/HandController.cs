using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] GameObject _card;

    List <GameObject> _cardsInHand;
    CardSpreadingLogic _spreadingLogic;

    private void Awake()
    {
        _cardsInHand = gameObject.GetComponent<HandDataStorage>().cardsInHand;
        _spreadingLogic = gameObject.GetComponent<CardSpreadingLogic>();
    }

    private void Start()
    {
        AddNewCard(Random.Range(4, 7));
    }

    public void AddNewCard(int iterations)
    {
        for (int counter = 0; counter < iterations ; counter++)
        {
            _cardsInHand.Add(Instantiate(_card, gameObject.transform));
        }
        _spreadingLogic.CalculateCardsPositions();
    }

    public void RemoveCard(int cardIndex)
    {
        if (_cardsInHand.Count > 0)
        {
            Destroy(_cardsInHand[cardIndex]);
            _cardsInHand.Remove(_cardsInHand[cardIndex]);
            _spreadingLogic.CalculateCardsPositions();
        }
    }
}