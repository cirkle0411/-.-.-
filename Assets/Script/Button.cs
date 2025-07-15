using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour
{
    public GameManager gamemanager;
    public Transform cooltimeOverlay;

    private AudioSource audioSource;

    [SerializeField] private float cooltime;
    private Vector3 CooltimeoriginalScale;
    private Vector3 CooltimeoriginalPosition;
    private float curCooltime;
    private bool isCooltime = false;

    private Vector3 originalScale;
    private bool isPressde = false;
    private float pressScale = 0.9f;
    private float pressDuration = 0.1f;

    [SerializeField] private int catNum;

    private void Awake()
    {
        catNum -= 1;

        originalScale = transform.localScale;
        CooltimeoriginalScale = cooltimeOverlay.localScale;
        CooltimeoriginalPosition = cooltimeOverlay.localPosition;
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetCooldownVisual();
    }

    // Update is called once per frame
    void Update()
    {
        if(isCooltime)
        {
            curCooltime -= Time.deltaTime;

            float ratio = Mathf.Clamp01(1f - (curCooltime / cooltime));
            UpdateCooltimeVisual(ratio);

            if (curCooltime <= 0f)
            {
                isCooltime = false;
                ResetCooldownVisual();
            }
        }
    }

    void OnMouseDown()
    {
        isPressde = true;
        transform.localScale = originalScale * pressScale;
        
        if (!isCooltime)
        {
            audioSource.Play();
            Instantiate(gamemanager.CatPrefabs[catNum], new Vector3(-8, -3, 0), Quaternion.identity);
            StartCooltime();
        }
    }

    private void OnMouseUp()
    {
        isPressde = false;
        transform.localScale = originalScale;
    }

    private void OnMouseExit()
    {
        if (isPressde)
        {
            isPressde = false;
            transform.localScale = originalScale;
        }
    }

    void StartCooltime()
    {
        curCooltime = cooltime;
        isCooltime = true;
        cooltimeOverlay.gameObject.SetActive(true);
    }

    void UpdateCooltimeVisual(float fillRatio)
    {
        Vector3 newScale = CooltimeoriginalScale;
        newScale.y = fillRatio;
        cooltimeOverlay.localScale = newScale;

        Vector3 newPos = CooltimeoriginalPosition;
        newPos.y = CooltimeoriginalPosition.y - (1 - fillRatio) * CooltimeoriginalScale.y / 2f;
        cooltimeOverlay.localPosition = newPos;
    }

    void ResetCooldownVisual()
    {
        cooltimeOverlay.gameObject.SetActive(false);
    }
}
