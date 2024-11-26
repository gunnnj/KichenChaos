using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PassCounter : BaseCounter
{
    public Order order;
    private PlateKitchenObj plateKitchenObj;
    private List<string> list;

    public Image check;
    public Sprite right;
    public Sprite wrong;
    public Sprite not;

    public AudioSource SoundRight;
    public AudioSource SoundWrong;
    public AudioSource pick;

    public float Score = 0;
    void Start()
    {
        list = new List<string>();
    }
    public override void Interact(Player player)
    {
        pick.Play();
        if (!HasKitchenObject()) // There is no obj
        {
            
            if (player.HasKitchenObject()) // Player is carrying something
            {
                if(player.isPlate()){
                    plateKitchenObj = player.GetPlate();
                    Transform plateTransform = plateKitchenObj.transform;
                    
                    checkOrder(plateTransform);
                    
                    if(CompareLists(list,order.list1)){
                        check.sprite = right;
                        SoundRight.Play();
                        Score+=20f;
                    }
                    else{
                        SoundWrong.Play();
                        check.sprite = wrong;
                    }               
                    player.GetKitchenObject().DestroySelf();
                    
                }
                else{
                    SoundWrong.Play();
                    check.sprite = wrong;
                    player.GetKitchenObject().DestroySelf();
                }
                StartCoroutine(wait());
                
            }
        }
    }
    IEnumerator wait(){
        yield return new WaitForSeconds(2f);
        order.isChange = true;
        ReSetSprite();
    }
    public void ReSetSprite(){
        check.sprite = not;
    }
    public void checkOrder(Transform plate){
        Transform child = plate.GetChild(2);
        for(int j =0; j<child.childCount;j++){
            Transform CCount = child.GetChild(j);
            string str="";
            if(CCount.name.Equals("chesee")){
                if(CCount.gameObject.activeSelf){
                    str = "chesee_true";
                }
                else{
                    str = "trongsuot";
                }
                list.Add(str);
                
            }
            if(CCount.name.Equals("tomato")){
                if(CCount.gameObject.activeSelf){
                    str = "tomato_true";
                    
                }
                else{
                    str = "trongsuot";
                }
                list.Add(str);
                
            }
            if(CCount.name.Equals("cabbage")){
                if(CCount.gameObject.activeSelf){
                    str = "cabbage_true";    
                }
                else{
                    str = "trongsuot";    
                }
                list.Add(str);
                
            }
        }
    }
    public bool CompareLists(List<string> list1, List<string> list2)
    {
        // Kiểm tra độ dài trước
        if (list1.Count != list2.Count)
        {
            return false;
        }

        // Sử dụng HashSet để so sánh
        HashSet<string> set1 = new HashSet<string>(list1);
        HashSet<string> set2 = new HashSet<string>(list2);

        return set1.SetEquals(set2);
    }
    
}
