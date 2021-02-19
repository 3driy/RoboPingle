using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    GameObject bomb;
    MeshRenderer bombRend;
    [SerializeField]
    Material frenselMat;
    [SerializeField]
    GameObject explosion;
    [SerializeField]
    private float explosionForce = 1f;


    // Start is called before the first frame update
    void Start()
    {
        bomb = transform.GetChild(0).gameObject;
        bombRend = bomb.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player") {
            StartCoroutine(PreBoom(other.gameObject)); 
        }
    }


    IEnumerator PreBoom(GameObject player) {
        float timer = 0.5f;
        Color frenselColor = frenselMat.GetColor("_FrenselColor");
        bombRend.material = frenselMat;
        while (timer > 0) {
            float frensel = Mathf.Lerp(-1, 3, timer * 2);
           bombRend.material.SetFloat("_FrenselContrast",frensel);
            bombRend.material.SetColor("_FrenselColor", frenselColor * Mathf.Lerp(1, 0, timer * 2));
            timer -= Time.deltaTime;
            yield return null;
        }
        if (timer <= 0) {

            Boom(player);
            yield break;
        }
    
    }

    void Boom(GameObject player) {

        player.GetComponent<PlayerHealthBonus>().GetHit();
        player.GetComponent<Rigidbody>().AddExplosionForce(explosionForce,bomb.transform.position,3f);
        Instantiate(explosion,bomb.transform.position,Quaternion.identity);
        DestroyImmediate(this.gameObject);
    }
}
