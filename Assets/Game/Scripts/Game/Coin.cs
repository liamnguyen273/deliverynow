using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip soundPickup;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0, 0, 5 * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OnCollected();
        }
    }

    void OnCollected()
    {
        InGame.Instance.AddScore(1);
        soundPickup.PlaySfx();
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        gameObject.SetActive(true);
    }
}
