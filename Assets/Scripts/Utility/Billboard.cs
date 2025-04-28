using UnityEngine;

public class Billboard : MonoBehaviour
{
     public bool enablePitch = false;

    // Update is called once per frame
    void Update()
    {
          Vector3 lookRot = transform.position - Camera.main.gameObject.transform.position;
          Quaternion lookRotation = Quaternion.LookRotation(lookRot);
          if(!enablePitch)
               lookRotation.eulerAngles = new Vector3(0f, lookRotation.eulerAngles.y, 0f);
          transform.rotation = lookRotation;
    }
}
