using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    public float timeOrder;
    [SerializeField]
    private Sprite[] sprite;
    [SerializeField]
    private Image[] image;
    public List<string> list1;

    public bool isChange = false;
    
    // Start is called before the first frame update
    void Start()
    {
        list1 = new List<string>();
        if (image != null && image.Length > 0)
        {
            StartCoroutine(OrderMenu());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isChange){
            StartCoroutine(OrderMenu());
        }
    }
    IEnumerator OrderMenu(){
        int toping = Random.Range(1,3);
        if(toping==3){
            image[0].sprite=sprite[0];
            image[1].sprite=sprite[1];
            image[2].sprite=sprite[2];
            
        }
        else if(toping==1){
            int random = Random.Range(0,2);
            image[0].sprite = sprite[random];
            image[1].sprite = sprite[3];
            image[2].sprite = sprite[3];
            
        }
        else{
            int random = Random.Range(0,2);
            if(random==0){
                image[0].sprite = sprite[0];
                image[1].sprite = sprite[1];
                image[2].sprite = sprite[3];
                
            }
            else if(random==1){
                image[0].sprite = sprite[1];
                image[1].sprite = sprite[2];
                image[2].sprite = sprite[3];
                
            }
            else{
                image[0].sprite = sprite[0];
                image[1].sprite = sprite[2];
                image[2].sprite = sprite[3];
                
            }
        }
        list1.Add(image[0].sprite.name);
        list1.Add(image[1].sprite.name);
        list1.Add(image[2].sprite.name);
        
        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] == "CheeseSlice")
            {
                list1[i] = "chesee_true";
            }
            if (list1[i] == "TomatoSlice")
            {
                list1[i] = "tomato_true";
            }
            if (list1[i] == "CabbageSlices")
            {
                list1[i] = "cabbage_true";
            }
        }
        isChange = false;
        yield return new WaitForSeconds(1f);
    }
   
}
