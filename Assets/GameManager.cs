using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject doorGo;
    private Player player;
    int coinsNeeded = 16;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        player.onCoinCollected += ReduceCoinsNeeded;
    }

    void ReduceCoinsNeeded()
    {
        coinsNeeded--;

        if (coinsNeeded <= 0)
            GameObject.Destroy(doorGo.gameObject);

    }

}
