using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;

    [SerializeField]
    private GameObject cubeTwo;
    [SerializeField]
    private GameObject cubeFour;
    [SerializeField]
    private Transform startPositionCube;
    [SerializeField]
    private GameObject finshPanel;
    [SerializeField]
    private TextMeshProUGUI finshText;
    [SerializeField]
    private TextMeshProUGUI countStepsText;

    public List<Material> materialsCubes;

    [HideInInspector]
    public bool isMovingPrefab;

    private GameObject mainCube;
    private int countSteps = 0;

    public GameObject[] gameCubes;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Instantiate(cubeTwo, startPositionCube.position, Quaternion.identity);
    }

    private void Update()
    {
        CheckMoveMainCube();
        CheckPositionCubes();
    }

    private void CheckMoveMainCube()
    {
        if (isMovingPrefab)
        {
            isMovingPrefab = false;
            int index = Random.Range(0, 10);
            if (index == 0)
            {
                 mainCube = cubeFour;
            }
            else
                mainCube = cubeTwo;
            Instantiate(mainCube, startPositionCube.position, Quaternion.identity);
            countSteps++;
        }
    }
    public void CheckGameWinOrOver(string text)
    {
        finshText.text = text;
        finshPanel.SetActive(true);
        Time.timeScale = 0;
        countStepsText.text = "Count Steps: " +countSteps.ToString();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    private void CheckPositionCubes()
    {
        gameCubes = GameObject.FindGameObjectsWithTag("Cube");
        for (int i = 0; i < gameCubes.Length; i++)
        {
            if (gameCubes[i].GetComponent<Cube>().isReadyCheckPosition)
            {
                if (gameCubes[i].transform.position.z < -2 || i == 32)
                    CheckGameWinOrOver("Game Over!");
            }
        }
    }
}
