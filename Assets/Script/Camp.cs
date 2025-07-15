using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camp : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private string campName;
    private string tagName;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(campName)
        {
            case "CatCamp":
                if(tagName == "Monster")
                {
                    gameManager.Lose();
                }
                break;
            case "MonsterCamp":
                if(tagName == "Cat")
                {
                    gameManager.Win();
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tagName = "Cat";
        }

        else if (other.CompareTag("Monster"))
        {
            tagName = "Monster";
        }
    }
}
