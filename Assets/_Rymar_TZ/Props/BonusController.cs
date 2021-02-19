using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{
    GameObject bonus;
    MeshRenderer bonusRend;
    [SerializeField]
    Material frenselMat;
    [SerializeField]
    GameObject[] effects;
    [SerializeField]
    //private float explosionForce = 1f;


    // Start is called before the first frame update
    void Start()
    {
        bonus = transform.GetChild(0).gameObject;
        bonusRend = bonus.GetComponent<MeshRenderer>();
    }

    

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            StartCoroutine(PreBoom(other.gameObject));
        }
    }


    IEnumerator PreBoom(GameObject player)
    {
        float timer = 0.3f;
        Color frenselColor = frenselMat.GetColor("_FrenselColor");
        bonusRend.material = frenselMat;
        while (timer > 0)
        {
            float frensel = Mathf.Lerp(-1, 3, timer * 3);
            bonusRend.material.SetFloat("_FrenselContrast", frensel);
            bonusRend.material.SetColor("_FrenselColor", frenselColor * Mathf.Lerp(1, 0, timer * 3));
            timer -= Time.deltaTime;
            yield return null;
        }
        if (timer <= 0)
        {

            Boom(player);
            yield break;
        }

    }

    void Boom(GameObject player)
    {

        player.GetComponent<PlayerHealthBonus>().GetBonus();
        //player.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, bonus.transform.position, 3f);
        int q = Random.Range(0, effects.Length);
        Instantiate(effects[q], bonus.transform.position, Quaternion.identity);
        DestroyImmediate(this.gameObject);
    }
}
