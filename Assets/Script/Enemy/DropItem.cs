using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public int BulletBagCount;


    // Start is called before the first frame update
    void Start()
    {
        BulletBagCount = GameObject.Find("GameManager").GetComponent<GameManager>().BulletBag;
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(Vector3.one* 40 * Time.deltaTime);
        Destroy(gameObject,40f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            if (this.gameObject.tag == "BulletBag")
            {
                BulletBagCount++;
                GameObject.Find("GameManager").GetComponent<GameManager>().BulletBag=BulletBagCount;

                if (BulletBagCount > 3)
                {
                    BulletBagCount = 3;
                }

            }
        }
    }
}
