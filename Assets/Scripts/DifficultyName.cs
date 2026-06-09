using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultyName : MonoBehaviour
{
    public TMP_Text difficultyName;

    public void Easy()
    {
        difficultyName.text = "DIFFICULTY: EASY"; 
        difficultyName.color = Color.green;
    }

    public void Medium()
    {
        difficultyName.text = "DIFFICULTY: MEDIUM";
        difficultyName.color = Color.yellow;
    }

    public void Hard()
    {
        difficultyName.text = "DIFFICULTY: HARD";
        difficultyName.color = Color.red;
    }
}
