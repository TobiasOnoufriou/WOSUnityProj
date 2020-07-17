using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupInformation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public string Dungeon_ID;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
