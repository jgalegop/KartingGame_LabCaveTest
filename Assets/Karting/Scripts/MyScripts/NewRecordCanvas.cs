using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewRecordCanvas : MonoBehaviour
{
    [Tooltip("New record text")]
    public TextMeshProUGUI newRecordText = null;
    [Tooltip("Amplitude for scaling animation")]
    public float scaleAmplitude = 0.1f;
    [Tooltip("Frequency for scaling animation")]
    public float frequency = 6f;
    [Tooltip("Time the text lasts")]
    public float totalTime = 3f;

    // controls scaling animation
    bool scaling = true;
    float initTime;
    Vector3 initScale;

    private void Start()
    {
        initScale = newRecordText.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // animation when enabled
        if (!scaling) { return; }
        newRecordText.transform.localScale = initScale +
            initScale * scaleAmplitude * Mathf.Sin(frequency * (Time.time - initTime));
    }

    private void OnEnable()
    {
        StartCoroutine(DisableAfterTime());
    }

    IEnumerator DisableAfterTime()
    {
        scaling = true;
        initTime = Time.time;
        yield return new WaitForSeconds(totalTime);
        scaling = false;
        gameObject.SetActive(false);
    }
}
