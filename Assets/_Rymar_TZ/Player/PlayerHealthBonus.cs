using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBonus : MonoBehaviour
{

    [HideInInspector]
    public int playerHealth;
    [SerializeField]
    Image healthImg;
    [SerializeField]
    Text bonusText;
    [SerializeField]
    GameObject roboModel;
    Material roboMat;
    int playerBonus;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 100;
        playerBonus = 0;
        roboMat = roboModel.GetComponent<SkinnedMeshRenderer>().materials[0];
        roboMat.SetFloat("_Damage", 1);
        //healthImg = healthBar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarColorFill();
    }

    void HealthBarColorFill() {

        healthImg.fillAmount = playerHealth / 100f;
        if (playerHealth >= 50)
        {
            healthImg.color = Color.Lerp(Color.yellow, Color.green, (playerHealth - 50) / 50);
        }
        else { 
        
            healthImg.color = Color.Lerp(Color.red, Color.yellow, playerHealth/ 50);
        }
    
    }

    public void GetHit() {
        playerHealth -= 20;
        roboMat.SetFloat("_Damage", Mathf.Lerp(0.2f, 1, playerHealth / 100f));
    
    }

    public void GetBonus() {
        playerBonus++;
        bonusText.text = "" + playerBonus;
    }
}
