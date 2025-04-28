using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class DynamicGuidebook : MonoBehaviour
{
     public static DynamicGuidebook instance;

    public Button rightbutton;
    public Button leftbutton;
    public int index;
    public Image Image;
     public TMP_Text PageTitle;
     public TMP_Text PageNumber;
     //public TMP_Text PageSubTitle;
     public TMP_Text Description;
     public GameObject ModelParent;
     public GameObject Model;
    public Canvas CanvasPage;
     public GuidebookUI GBUI;
     public Image NativeOrInvasiveStamp;

     [SerializeField]
     private Sprite NativeStamp;
     [SerializeField]
     private Sprite InvasiveStamp;

     private Vector3 baseOffet = new Vector3(-0.5f, 0, 1);
     private Vector3 offset = new Vector3();
     public float rotationSpeed = 30;

     public ObjectManager objectManager;
     private ObjectSO currentObj = null;

     private void Awake()
     {
          if (instance == null)
          {
               instance = this;
               DontDestroyOnLoad(gameObject);
          }
          else if (instance != this)
          {
               Debug.LogWarning("More than one Dynamic Guidebook started. Destroying duplicate");
               Destroy(gameObject);
          }

          
     }

     // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Start()
    {
          objectManager = ObjectManager.instance;

        index = 0;

          LoadPage(0);
    }

     private void Update()
     {
          ModelUpdate();
     }

     private void ModelUpdate()
     {
          if (!GBUI.isGuidebookOpen || Model == null) return;

          // Scaling before moved to separate canvas
/*          float referenceAspect = 1920f / 1080f;

          float currentAspect = (float)Screen.width / Screen.height;
          float aspectRatioScale = currentAspect / referenceAspect;

          float scaleWidth = Screen.width / 1920f;*/


          offset = currentObj.ModelOffset;
          ModelParent.transform.position = Camera.main.transform.position + Camera.main.transform.forward * offset.z
                                                                           + Camera.main.transform.right * offset.x
                                                                           + Camera.main.transform.up * offset.y;

 
          Model.transform.localScale = Vector3.one * currentObj.ModelScale;
          ModelParent.gameObject.transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
     }

     public void RightArrow()
    {
        if (index < objectManager.ObjectList.Count - 1)
        {
            index++;
        }

        LoadPage(index);
    }

    public void LeftArrow()
    {
        if (index > 0)
        {
            index--;
        }
          LoadPage(index);
     }

     private void LoadUndiscovered()
     {
          objectManager.BlankObject.isScanned = false;           //redundancy in case someone changes this in editor
          LoadObjectSO(objectManager.BlankObject);
          NativeOrInvasiveStamp.gameObject.SetActive(false);
     }

     public void LoadPage(int page)
     {
          index = page;
          PageNumber.text = (page + 1).ToString();
     
          if (objectManager.ObjectList[index].isScanned == false)
          {
               LoadUndiscovered();
               return;
          }

          LoadObjectSO(objectManager.ObjectList[index]);
     }

     private void LoadObjectSO(ObjectSO newObj)
     {
          currentObj = newObj;

          Image.sprite = newObj.Image;
          PageTitle.text = newObj.Name;
          //PageSubTitle.text = "\"" + objectManager.ObjectList[index].LatinName + "\"";
          Description.text = newObj.Description.text;

          NativeOrInvasiveStamp.gameObject.SetActive(true);
          NativeOrInvasiveStamp.sprite = newObj.isInvasive ? InvasiveStamp : NativeStamp;

          Destroy(Model);
          if(newObj.Model == null)
          {
               Debug.LogWarning("Model not assigned for object: " + newObj.name);
               return;
          }
          Model = Instantiate(newObj.Model);

          Model.transform.parent = ModelParent.gameObject.transform;
          Model.transform.position = ModelParent.transform.position;
          Model.transform.rotation = ModelParent.transform.rotation;
     }
}
