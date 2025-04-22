using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DynamicGuidebook : MonoBehaviour
{
     public static DynamicGuidebook instance;

    public Button rightbutton;
    public Button leftbutton;
    public int index;
    public Image Image;
     public TMP_Text PageTitle;
     public TMP_Text PageSubTitle;
     public TMP_Text Description;
     public GameObject ModelParent;
     public GameObject Model;
    public Canvas CanvasPage;
     public GuidebookUI GBUI;

     private Vector3 baseOffet = new Vector3(-0.5f, 0, 1);
     private Vector3 offset = new Vector3();
     public float rotationSpeed = 30;

     public ObjectManager objectManager;

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
          if (!GBUI.isGuidebookOpen) return;

          offset = objectManager.ObjectList[index].ModelOffset + baseOffet;
          ModelParent.transform.position = Camera.main.transform.position + Camera.main.transform.forward * offset.z
                                                                           + Camera.main.transform.right * offset.x
                                                                           + Camera.main.transform.up * offset.y;
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
          Image.sprite = objectManager.BlankObject.Image;
          PageTitle.text = objectManager.BlankObject.Name;
          PageSubTitle.text = "";
          Description.text = objectManager.BlankObject.Description.text;
          Destroy(Model);
     }

     public void LoadPage(int page)
     {
          index = page;

          if (objectManager.ObjectList[index].isScanned == false)
          {
               LoadUndiscovered();
               return;
          }

          Image.sprite = objectManager.ObjectList[index].Image;
          PageTitle.text = objectManager.ObjectList[index].Name;
          PageSubTitle.text = "\"" + objectManager.ObjectList[index].LatinName + "\"";
          Description.text = objectManager.ObjectList[index].Description.text;

          Destroy(Model);
          Model = Instantiate(objectManager.ObjectList[index].Model);

          Model.transform.parent = ModelParent.gameObject.transform;
          Model.transform.position = ModelParent.transform.position;
          Model.transform.rotation = ModelParent.transform.rotation;
     }
}
