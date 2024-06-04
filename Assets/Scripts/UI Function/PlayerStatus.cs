using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private Image HP;
    [SerializeField] private Slider EXP;
    [SerializeField] private Slider wave;
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI KillCount;
    [SerializeField] private TextMeshProUGUI CoinCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHP(float min, float max)
    {
        HP.fillAmount = min / max;
    }

    public void UpdateEXP(float min, float max)
    {
        EXP.value = min /max;
    }

    public void UpdateLvlText(int lvl)
    {
        lvlText.text = lvl.ToString();
    }
}
