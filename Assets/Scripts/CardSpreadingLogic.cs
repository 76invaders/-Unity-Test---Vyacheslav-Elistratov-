using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class CardSpreadingLogic : MonoBehaviour
{
    [SerializeField] float _cardsSpreading = 1.5f;
    [SerializeField] float _spreadWidth = 100f;
    [SerializeField] float _tweenSpeed = 0.5f;
    [SerializeField] AnimationCurve _rotationCurve;
    [SerializeField] AnimationCurve _verticalPositionCurve;

    List <GameObject> _cardsInHand;
    float _spreadingLimit;
    float _handFullness;

    private void Awake()
    {
        _cardsInHand = gameObject.GetComponent<HandDataStorage>().cardsInHand;
    }

    internal void CalculateCardsPositions()
    {
        float _cardRatio = 0.5f;

        _handFullness = (float)_cardsInHand.Count;
        _spreadingLimit = _handFullness > 9.0 ? (_spreadWidth) : (_handFullness * (_spreadWidth / 10));

        if (_handFullness > 1)
        {
            foreach (GameObject Card in _cardsInHand)
            {
                _cardRatio = _cardsInHand.IndexOf(Card) / (_handFullness - 1);
                SetPositionOfACard(Card, _cardRatio);
            }
        }
        else if (_handFullness > 0)
        {
            SetPositionOfACard(_cardsInHand[0], _cardRatio);
        }

        void SetPositionOfACard(GameObject Card, float _cardRatio)
        {
                Card.transform.DOLocalMove(new Vector3(CalculateHorisontalPosition(_cardRatio) * _cardsSpreading,
                                                       CalculateVerticalPosition(_cardRatio),
                                                       0), _tweenSpeed);
                Card.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, CalculateRotation(_cardRatio)),_tweenSpeed);
        }
    }

    float CalculateHorisontalPosition(float cardRatio)
    {
        return Mathf.Lerp(-_spreadingLimit, _spreadingLimit, cardRatio) * _cardsSpreading;
    }

    float CalculateVerticalPosition(float cardRatio)
    {
        return (_handFullness * 3) * _verticalPositionCurve.Evaluate(cardRatio);
    }

    float CalculateRotation(float cardRatio)
    {
        return (_handFullness) * _rotationCurve.Evaluate(cardRatio);
    }
}