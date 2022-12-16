using System;
using UnityEngine;
using UnityEngine.UI;

public class CardDataManager : MonoBehaviour
{
    [SerializeField] CardTemplate _cardTemplate;
    [SerializeField] Text[] _cardValuesText;
    [SerializeField] Image _cardArt;
    [SerializeField] Image _cardCover;
    [SerializeField] Text _cardName;
    [SerializeField] Text _cardDescription;
    [SerializeField] GameObject _counterPrefab;

    HandController _handController;
    WebCallScript _webCaller;
    int[] _cardValues;

    void Start()
    {
        _handController = gameObject.GetComponentInParent<HandController>();
        _webCaller = gameObject.AddComponent<WebCallScript>();
        _cardValues = new int[_cardTemplate.cardStats.Length];

        _cardTemplate.RandomizeStats();
        GetParamsOfTemplate();
    }

    void GetParamsOfTemplate()
    {
        _cardName.text = _cardTemplate.cardName;
        _cardDescription.text = _cardTemplate.cardDescription;

        foreach (Values ValueType in Enum.GetValues(typeof(Values)))
        {
            _cardValues[(int)ValueType] = _cardTemplate.cardStats[(int)ValueType];
            _cardValuesText[(int)ValueType].text = _cardValues[(int)ValueType].ToString();
        }

        _webCaller.GetImmage("https://picsum.photos/280/260", _cardArt);
        _cardCover.sprite = _cardTemplate.cardCover;
    }

    internal void RandomizeSomeValue(int cardIndex = 0)
    {
        Values randomValueType = SelectRandomValue();
        int randomValue = UnityEngine.Random.Range(-2, 10);

        while (randomValue == _cardValues[(int)randomValueType])
        {
            randomValue = UnityEngine.Random.Range(-2, 10);
        }

        Instantiate(CreateCounter(_cardValues[(int)randomValueType], randomValue, randomValueType),
                    transform.position+new Vector3(0,1,0),
                    transform.rotation,
                    transform.parent);

        SetValue(randomValueType, randomValue);

        CheckHP(cardIndex);
    }

    Values SelectRandomValue()
    {
        return (Values)UnityEngine.Random.Range(0, Values.GetValues(typeof(Values)).Length);
    }

    void SetValue(Values valueType,int value)
    {
        _cardValues[(int)valueType] = value;
        _cardValuesText[(int)valueType].text = value.ToString();
    }

    void CheckHP(int CardIndex = 0)
    {
        if (_cardValues[(int)Values.Health] < 1)
        {
            _handController.RemoveCard(CardIndex);
        }
    }

    CounterSpawnAnimator CreateCounter(int oldValue, int newValue, Values valueType)
    {
        GameObject counterObject = _counterPrefab;
        CounterSpawnAnimator counter = counterObject.GetComponent<CounterSpawnAnimator>();

        counter.CalculateValuesDiff(oldValue, newValue, valueType);

        return counter;
    }
}