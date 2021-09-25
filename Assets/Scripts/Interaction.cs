using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Interaction : MonoBehaviour
{
    List<ChatboxBehavior> chatboxes = new List<ChatboxBehavior>();

    // Start is called before the first frame update
    void Start()
    {
        chatboxes = FindObjectsOfType<ChatboxBehavior>().ToList();
    }

    // Update is called once per frame
    void Update()
    {   
        
    }
}
