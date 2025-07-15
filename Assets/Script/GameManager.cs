using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] MonsterPrefabs;
    public GameObject[] CatPrefabs;

    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Image trigger;

    private AudioSource audioSource;
    [SerializeField] private AudioClip winClip;
    [SerializeField] private AudioClip loseClip;

    public float intervalMin;
    public float intervalMax;

    private float interval;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = trigger.GetComponent<AudioSource>();
        resultText.gameObject.SetActive(false);
        trigger.gameObject.SetActive(false);
        StartCoroutine(SpawnMonster());
    }

    // Update is called once per frame
    void Update()
    {

    }

    System.Collections.IEnumerator SpawnMonster()
    {
        while (true)
        {
            interval= Random.Range(intervalMin, intervalMax);
            yield return new WaitForSeconds(interval);

            RandomMonster();
        }
    }

    void RandomMonster()
    {
        int index = Random.Range(0, MonsterPrefabs.Length);
        
        Instantiate(MonsterPrefabs[index], new Vector3(8, (float)-2.7, 0), Quaternion.identity);
    }

    public void Win()
    {
        resultText.text = "Win";
        resultText.gameObject.SetActive(true);
        audioSource.clip = winClip;
        trigger.gameObject.SetActive(true);
        StopAllCoroutines();
    }

    public void Lose()
    {
        resultText.text = "Defeat";
        resultText.gameObject.SetActive(true);
        audioSource.clip = loseClip;
        trigger.gameObject.SetActive(true);
        StopAllCoroutines();
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }
}
