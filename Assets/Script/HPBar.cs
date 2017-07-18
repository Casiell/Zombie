using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HPBar : MonoBehaviour
{
    [SerializeField]
    private Text enemyName = null;

    private Slider slider;
    public static HPBar instance;

    private void Awake()
    {
        instance = this;
        slider = this.GetComponent<Slider>();
    }

    private void Start()
    {
        slider.normalizedValue = 1;
    }

    public void SetHP(float current, float max, string name)
    {
        slider.normalizedValue = current / max;
        enemyName.text = name;
    }
}