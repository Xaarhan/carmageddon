using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameCommander : MonoBehaviour
{

    void Start()
    {
        _stop = true;
        _bots = new List<BaseBot>();
        spawnPoints = FindObjectsOfType<SpawnPoint>();
    }

    void Update() {
        if ( _stop ) return;

        if ( _bots.Count == 0 && _bots_spawned >= total_bots) {
             showWin();
        }


        if ( _player == null || _player.isDead  ) {
            if (_player_lifes <= player_lifes_count) {
                spawnPlayer();
            } else {
                showLoose();
            }
        }


        if ( _spawn_delay >= 0 ) {
             _spawn_delay -= Main.delta;
        }


        if ( _spawn_delay <= 0 &&
             _bots_spawned < total_bots &&
             _bots.Count < max_bots_on_arena ) {
             trySpawnBot();
        }


        for ( int i = 0; i < _bots.Count; i++ ) {
              BaseBot bot = _bots[i];
              if ( _bots[i].isDead ) {
                   onBotDead(_bots[i]);
              }
        }

        
        

    }

    private void trySpawnBot() {
        SpawnPoint spawn = getRandomFreeSpawn();
        if (spawn == null) return;
        BaseBot bot = Instantiate(defBot);
        bot.transform.position = spawn.point.position;
        bot.transform.rotation = spawn.transform.rotation;
        bot.gameObject.SetActive(true);
        bot.setTargetCar(_player);
        addBot(bot);
        _spawn_delay = 100;
    }

    private void addBot( BaseBot bot ) {
        _bots_spawned++;
        _bots.Add(bot);
    }




    private void onBotDead( BaseBot bot) {
        Main.DestroyObject(bot.gameObject);
        _bots.Remove(bot);
    }


    private SpawnPoint getRandomFreeSpawn() {
        int i = (int)(UnityEngine.Random.value * spawnPoints.Length);
        int count = 0;
        while ( count < spawnPoints.Length ) {
                SpawnPoint spawn = spawnPoints[i];
                if (spawn.isFree ) {
                    return spawn;
                }
                count++;
                i++;
                if ( i >= spawnPoints.Length) {
                     i = 0;
                }
        }
        return null;
    }



    private void spawnPlayer() {
        SpawnPoint spawn = getRandomFreeSpawn();
        if (spawn == null) return;
        _player = Instantiate(defPlayer);
        _player.transform.position = spawn.point.position;
        _player.transform.rotation = spawn.transform.rotation;
        inventory.mob = _player;
        _player.gameObject.SetActive(true);
        botsSetTarget(_player);
        _player_lifes++;
        camera.Follow = _player.transform;
        camera.LookAt =_player.transform;
    }

    private void botsSetTarget(BaseMob mob) {
        for (int i = 0; i < _bots.Count; i++) {
            _bots[i].setTargetCar(mob);
        }
    }

    private void showWin() {
        winScreen.SetActive(true);
        _stop = true;
    }

    private void showLoose() {
        looseScreen.SetActive(true);
        _stop = true;
    }

    public void restart() {
        winScreen.SetActive(false);
        looseScreen.SetActive(false);
        playBtn.SetActive(false);
        _bots_spawned = 0;
        _player_lifes = 0;
        if ( _player != null ) Main.removeObject(_player.gameObject);
        for ( int  i = 0; i < _bots.Count; i++ ) {
              Main.removeObject(_bots[i].gameObject);
        }
        _bots = new List<BaseBot>();
        _stop = false;
        spawnPlayer();
        _spawn_delay = 100;
    }



    public int player_lifes_count;

    private bool _stop;
    private int _spawn_delay;
    private int _bots_spawned;
    
    private List<BaseBot> _bots;
    private int _player_lifes;

    private BaseMob _player;
    public Inventory inventory;

    public int total_bots;
    public int max_bots_on_arena;
    public SpawnPoint[] spawnPoints;
    public BaseBot defBot;
    public BaseMob defPlayer;
    public CinemachineVirtualCamera camera;

    public GameObject winScreen;
    public GameObject looseScreen;
    public GameObject playBtn;
   
}
