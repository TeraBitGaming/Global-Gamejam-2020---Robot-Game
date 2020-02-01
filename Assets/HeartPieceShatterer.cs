using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartPieceShatterer : MonoBehaviour
{

    [SerializeField]
    private GameObject[] heartPieces;

    [SerializeField]
    private ParticleSystem particleSystem;

    public void shatter(){
        gameObject.GetComponent<UnityEngine.UI.Image>().enabled = false;
        particleSystem.Play();
        for (int i = 0; i < heartPieces.Length; i++)
        {
            heartPieces[i].SetActive(true);
            heartPieces[i].GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // shatter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
