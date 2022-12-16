using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CounterSpawnAnimator : MonoBehaviour
{
    Text _counterText;

    private void Start()
    {
        StartCoroutine(SelfDestroy());
    }

    private void Update()
    {
        transform.position = transform.position + new Vector3(0,1,0)*Time.deltaTime;
    }

    internal void CalculateValuesDiff(int oldValue , int newValue, Values value)
    {
        _counterText = GetComponent<Text>();
        int diff = newValue - oldValue;
        _counterText.color = diff > 0 ? (Color.green) : (Color.red);
        _counterText.text = diff > 0 ? ($"+ {diff}\n{value}") :($"- {-diff}\n{value}");
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}