using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NodesUI : MonoBehaviour
{
    public InstrumentPlayer instrument;
    List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();
    public Color origin;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            TextMeshProUGUI text = transform.GetChild(i).GetComponent<TextMeshProUGUI>();
            texts.Add(text);
            text.color = origin;
        }
    }

    private void Update()
    {
        if (instrument.pitch > 0)
        {
            StartCoroutine(UpdateUI());
        }
    }

    IEnumerator UpdateUI()
    {
        int index = instrument.pitch - 1;
        texts[index].color = Color.white;
        yield return new WaitForSeconds(0.5f);
        texts[index].color = origin;
    }
}
