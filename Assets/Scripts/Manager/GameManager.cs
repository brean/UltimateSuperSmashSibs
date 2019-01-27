using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public List<Player> players = new List<Player>();
    public int winningTeam;

    public string previousSceneName;
    public string currentSceneName;

    // Start is called before the first frame update
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.activeSceneChanged += gettingSceneInfo;
    }

    void gettingSceneInfo(Scene previousScene, Scene newScene)
    {
        previousSceneName = currentSceneName;
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Player getPlayerForCharacter(Character character)
    {
        if (instance == null)
        {
            instance = new GameManager();
        }
        return instance.playerForCharacter(character);
    }

    public Player playerForCharacter(Character character)
    {
        if (players.Count != 4)
        {
            // quickly fake inputs for testing
            players = new List<Player>
            {
                new Player{
                    character = Character.princess,
                    inputType = InputType.Key,
                    number = 1,
                    color = Color.red,
                    team = 0,
                    active = true,
                    ready = true
                },
                new Player{
                    character = Character.jock,
                    inputType = InputType.Key,
                    number = 2,
                    color = Color.blue,
                    team = 0,
                    active = true,
                    ready = true
                },
                new Player{
                    character = Character.hipster,
                    inputType = InputType.Key,
                    number = 3,
                    color = Color.green,
                    team = 1,
                    active = true,
                    ready = true
                },
                new Player{
                    character = Character.nerd,
                    inputType = InputType.Key,
                    number = 4,
                    color = Color.yellow,
                    team = 1,
                    active = true,
                    ready = true
                }
            };
        }
        foreach (Player p in players)
        {
            if (p.character == character)
            {
                return p;
            }
        }
        return null;
    }

    public void loadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }



}
