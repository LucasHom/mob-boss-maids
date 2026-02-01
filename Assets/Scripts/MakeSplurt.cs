using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MakeSplurt : MonoBehaviour
{

    public GameObject splurt;
    public int count = 1000;
    public Vector3 pos = new Vector3(0, .1f, 0);
    public Vector2 sizeRange = new Vector2(.05f, .3f);
    public Vector3 launchMod = new Vector3(3, 7, 3);


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GameManager.Instance.InitializeSplurt += InitializeSplurt_InitialSplurt;
    }

    private void InitializeSplurt_InitialSplurt(object sender, EventArgs e)
    {
        // call beginSplurt for each splurt point that you want to start around the map.
        beginSplurt(default(GameObject), 200, new Vector3(0.0f, 1.0f, 0.0f), default(Vector2), default(Vector3));
        beginSplurt(default(GameObject), 200, new Vector3(0.0f, 1.0f, -3.0f), default(Vector2), default(Vector3));
        beginSplurt(default(GameObject), 200, new Vector3(0.0f, 1.0f, -1.5f), default(Vector2), default(Vector3));
    }


    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) {
            beginSplurt();
        }   
    }


    public void beginSplurt(GameObject splurt = default(GameObject), int count = default(int), Vector3 pos = default(Vector3), Vector2 sizeRange = default(Vector2), Vector3 launchMod = default(Vector3)) {
        if (splurt == default(GameObject)) splurt = this.splurt;
        if (count == default(int)) count = this.count;
        if (pos == default(Vector3)) pos = this.pos;
        if (sizeRange == default(Vector2)) sizeRange = this.sizeRange;
        if (launchMod == default(Vector3)) launchMod = this.launchMod;
        
        for (int i = 0; i < count; i++) {
            GameObject newSplurt = Instantiate(splurt, pos, Quaternion.identity);
            splurtMovement sm = newSplurt.GetComponent<splurtMovement>();
            sm.sizeRange = sizeRange;
            sm.launchMod = launchMod;
            sm.camera = Camera.main.gameObject;
        }
    }
}
