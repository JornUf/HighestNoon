using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomSettings : MonoBehaviour
{
    public Slider badgeslider;
    public Slider enemyslider;

    [SerializeField] private TextMeshProUGUI badge;
    [SerializeField] private TextMeshProUGUI badgeS;

    [SerializeField] private TextMeshProUGUI enemy;

    [SerializeField] private TextMeshProUGUI enemyS;

    public int EnemiesPerMinute = 12;

    public int BadgeHeight = 50;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void NmeChange()
    {
        int A = (int)enemyslider.value;
        EnemiesPerMinute = A;
        enemy.text = "Enemies/Minute: " + A;
        enemyS.text = "Enemies/Minute: " + A;
    }

    public void BadgeChange()
    {
        int B = (int)badgeslider.value;
        BadgeHeight = B;
        badge.text = "Badge Height: " + B;
        badgeS.text = "Badge Height: " + B;
    }
    
}
