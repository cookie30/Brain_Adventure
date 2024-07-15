using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        Destroy(gameObject, 3);
    }
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward*50*Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
            Destroy(this.gameObject);
    }
}
