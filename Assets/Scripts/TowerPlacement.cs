using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TowerPlacement : MonoBehaviour
{
    private Camera cam;
    public static int playerMoney = 100;
    public TextMeshProUGUI moneyText;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown((0)))
        {
            Vector2 point = new Vector2();
            Event currentEvent = Event.current;
            Vector2 mousePos = new Vector2();


        }
        moneyText.text = playerMoney.ToString();
    }
}
