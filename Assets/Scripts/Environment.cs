using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Environment : MonoBehaviour
{
    private TextMesh scoreBoard;
    private Jumper jumperGuy;


    private void OnEnable()
    {
        scoreBoard = GameObject.Find("ScoreText").GetComponent<TextMesh>();
        //jumperGuy = transform.GetComponentInChildren<Jumper>();
        jumperGuy = GameObject.Find("JumperGuy").GetComponent<Jumper>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scoreBoard.text = jumperGuy.GetCumulativeReward().ToString("f2");
    }
}
