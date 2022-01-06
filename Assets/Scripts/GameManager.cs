using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] Camera camera;
    [SerializeField] PlayerController playerController;
    [SerializeField] UIManager uIManager;
    [SerializeField] OrbController orbPrefab;
    OrbController currentOrb;
    bool isSimulating = true;
    int _orbsNo;
    int orbsNo
    {
        get{return _orbsNo;}
        set{_orbsNo = value; uIManager.UpdateOrbsText(value);}
    }

    void Awake() => Instance = this;
    void Start()
    {
        SpawnOrb();
        uIManager.Play();
    }
    
    public void ResetGame()
    {
        orbsNo = 0;
        if(!currentOrb)
            SpawnOrb();
        playerController.ResetPlayer();
        uIManager.Play();
    }
    
    public void EatOrb()
    {
        orbsNo ++;
        //Spawn a new one
        SpawnOrb();
    }

    void SpawnOrb()
    {
        float zLimit = camera.orthographicSize;
        float xLimit = zLimit * camera.aspect;

        Vector3 orbPos = new Vector3(Random.Range(-xLimit * 0.8f, xLimit * 0.8f), 0, Random.Range(-zLimit * 0.8f, zLimit * 0.8f));
        currentOrb = Instantiate(orbPrefab, orbPos, Quaternion.identity);
    }

    public void Lose()
    {
        isSimulating = false;
        uIManager.Lose(orbsNo);
    }

    public void Quit() => Application.Quit();
}