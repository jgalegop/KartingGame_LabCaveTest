using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Tooltip("Speed of rotation animation")]
    public float rotSpeed = 180f;
    [Tooltip("Audio clip of coin pick up")]
    public AudioClip coinSound = null;
    [Tooltip("Volume for audio clip of coin pick up")]
    [SerializeField] [Range(0, 1)] float coinSoundVolume = 1f;

    //Reference to coin manager script
    CoinManager coinManager = null;

    void Start()
    {
        coinManager = FindObjectOfType<CoinManager>();
        if (coinManager == null)
        {
            Debug.LogError("Coin Manager missing in scene");
        }
    }

    void Update()
    {
        transform.Rotate(0, rotSpeed * Time.deltaTime, 0, Space.World);
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.name == "Kart")
        {
            // play sound
            AudioSource.PlayClipAtPoint(coinSound, Camera.main.transform.position, coinSoundVolume);

            // score coin point
            coinManager.PickedUpCoin();

            Destroy(gameObject);
        }
    }
}
