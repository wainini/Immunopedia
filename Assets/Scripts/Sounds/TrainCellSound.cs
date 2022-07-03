using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCellSound : MonoBehaviour
{
    public void OnTrainCell()
    {
        AudioManager.instance.PlaySound("TrainCell", SoundOutput.sfx);
    }

    public void OnMinusCell()
    {
        AudioManager.instance.PlaySound("MinusCell", SoundOutput.sfx);
    }
}
