using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    public TextMeshProUGUI goalText, msgText;
    public Animator transition;
    public GameObject exitButton;
    static public bool isChk = false;

    [SerializeField]
    GameObject tile, bottonTile, startButton, tapText, gameOverPanel;

    TextMeshProUGUI scoreText;
    List<GameObject> stack;

    bool isStart, isFinish;

    List<Color32> spectrum = new List<Color32>()
    {
        new Color32 (0, 255, 33, 255),
        new Color32 (167, 255, 0, 255),
        new Color32 (230, 255, 0, 255),
        new Color32 (255, 237, 0, 255),
        new Color32 (255, 206, 0, 255),
        new Color32 (255, 185, 0, 255),
        new Color32 (255, 142, 0, 255),
        new Color32 (255, 111, 0, 255),
        new Color32 (255, 58, 0, 255),
        new Color32 (255, 0, 0, 255),
        new Color32 (255, 0, 121, 255),
        new Color32 (255, 0, 164, 255),
        new Color32 (241, 0, 255, 255),
        new Color32 (209, 0, 255, 255),
        new Color32 (178, 0, 255, 255)
    };
    int modifier;
    int colorIndex;
    int transferValue = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (gameOverPanel.transform.localScale == Vector3.one)
        {
            gameOverPanel.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
        }
        isChk = false;
        SoundManager.instance.PlayBgm("Game1");
        tapText.SetActive(true);
        exitButton.GetComponent<Button>().interactable = true;
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        stack = new List<GameObject>();
        isFinish = false;
        isStart = false;
        modifier = 1;
        colorIndex = 0;
        stack.Add(bottonTile);
        stack[0].GetComponent<Renderer>().material.color = spectrum[0];
        CreateTile();
    }

    void Update()
    {
        if (isFinish || !isStart) return;
        if (Input.GetMouseButtonDown(0))
        {
            SoundManager.instance.PlaySe("Cursor");
            if (stack.Count > 1)
                stack[stack.Count - 1].GetComponent<Tile>().ScaleTile();
            if (isFinish) return;
            StartCoroutine(MoveCamera());
            scoreText.text = (stack.Count - 1).ToString();
            tapText.SetActive(false);
            CreateTile();
        }
    }
    IEnumerator MoveCamera()
    {
        float moveLength = 1.0f;
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        while (moveLength > 0)
        {
            float stepLength = 0.1f;
            moveLength -= stepLength;
            camera.transform.Translate(0, stepLength, 0, Space.World);
            yield return new WaitForSeconds(0.05f);
        }
    }

    void CreateTile()
    {
        GameObject preTile = stack[stack.Count - 1];
        GameObject curTile;

        curTile = Instantiate(tile);
        stack.Add(curTile);

        if (stack.Count > 2)
            curTile.transform.localScale = preTile.transform.localScale;

        curTile.transform.position = new Vector3(preTile.transform.position.x,
            preTile.transform.position.y + preTile.transform.localScale.y, preTile.transform.position.z);

        colorIndex += modifier;
        if (colorIndex == spectrum.Count || colorIndex == -1)
        {
            modifier *= -1;
            colorIndex += 2 * modifier;
        }

        curTile.GetComponent<Renderer>().material.color = spectrum[colorIndex];
        curTile.GetComponent<Tile>().moveX = stack.Count % 2 == 0;
    }

    public void GameOver()
    {
        startButton.SetActive(true);
        isFinish = true;
        StartCoroutine(EndCamera());
    }

    IEnumerator EndCamera()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Vector3 temp = camera.transform.position;
        Vector3 final = new Vector3(temp.x, temp.y - stack.Count * 0.5f, temp.z);
        float cameraSizeFinal = stack.Count * 0.65f;
        while (camera.GetComponent<Camera>().orthographicSize < cameraSizeFinal)
        {
            camera.GetComponent<Camera>().orthographicSize += 0.2f;
            temp = camera.transform.position;
            temp = Vector3.Lerp(temp, final, 0.2f);
            camera.transform.position = temp;
            yield return new WaitForSeconds(0.01f);
        }
        camera.transform.position = final;
    }

    public void StartButton()
    {
        if(isFinish)
        {
            if (gameOverPanel.transform.localScale == Vector3.zero)
            {
                SoundManager.instance.PlaySe("Open");
                gameOverPanel.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack);
            }

            int scoreResult = int.Parse(scoreText.text);

            switch (scoreResult / 10)
            {
                case 0:
                    transferValue = Random.Range(5, 11);
                    break;
                case 1:
                    transferValue = Random.Range(5, 11);
                    break;
                case 2:
                    transferValue = Random.Range(10, 16);
                    break;
                case 3:
                    transferValue = Random.Range(15, 21);
                    break;
                case 4:
                    transferValue = Random.Range(20, 31);
                    break;
            }

            goalText.DOText("기록 : " + scoreResult + "층", 0.8f);
            Invoke("Msg", 1f);
        }
        else
        {
            startButton.SetActive(false);
            isStart = true;
        }
    }

    void Msg()
    {
        msgText.DOText("보상 : 전투력 +" + transferValue.ToString(), 0.8f);
    }

    public void ExitScene()
    {
        SoundManager.instance.PlaySe("Click");
        UIManager.combat += transferValue;
        PlayerPrefs.SetInt("curAmends", transferValue);
        isChk = true;

        exitButton.GetComponent<Button>().interactable = false;
        StartCoroutine(SceneLevel(SceneManager.GetActiveScene().buildIndex - 2));
    }
    public void UIHover()
    {
        SoundManager.instance.PlaySe("UIHover");
    }

    IEnumerator SceneLevel(int levelIndex)
    {
        SoundManager.instance.StopBgm();
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(levelIndex);
    }
}
