using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmptySlideWhenMinVal : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private Transform fill;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    // Update is called once per frame
    void Update()
    {
        if(slider.value == slider.minValue)
        {
            fill.gameObject.SetActive(false);
        }
        else
        {
            fill.gameObject.SetActive(true);
        }
    }
}
