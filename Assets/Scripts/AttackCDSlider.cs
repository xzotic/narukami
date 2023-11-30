using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackCDSlider : MonoBehaviour
{
    private Slider slider;

    [SerializeField] private PlayerAttack pa;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = pa.chargeCooldown;
    }
}
