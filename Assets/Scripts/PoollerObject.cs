using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoollerObject : MonoBehaviour
{
    public static PoollerObject poollerObjectInstance;
    public int numberToPool = 5;

    public List<GameObject> kunaiLeftList;
    public List<GameObject> kunaiRightList;
    public GameObject kunaiLeft, kunaiRight;

    void Awake()
    {
        poollerObjectInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ListKunai();
        
    }


    void ListKunai()
    {
        kunaiLeftList = new List<GameObject>();
        kunaiRightList = new List<GameObject>();
        for(int i =0;i<numberToPool;i++)
        {
            GameObject kunaiLeftCoppy = (GameObject)Instantiate(kunaiLeft);
            kunaiLeftCoppy.SetActive(false);
            kunaiLeftList.Add(kunaiLeftCoppy);
            kunaiLeftCoppy.transform.SetParent(transform);    
        }

        for(int i=0;i<numberToPool;i++)
        {
            GameObject kunaiRightCoppy = (GameObject)Instantiate(kunaiRight);
            kunaiRightCoppy.SetActive(false);
            kunaiRightList.Add(kunaiRightCoppy);
            kunaiRightCoppy.transform.SetParent(transform);

        }
    }

    
    public GameObject PoollerKunaiLeft()
    {
        for(int i=0;i<kunaiLeftList.Count;i++)
        {
            if(!kunaiLeftList[i].activeInHierarchy){
                return kunaiLeftList[i];
            }
        }
        return null;
    }

    public GameObject PoollerKunaiRight()
    {
        for(int i=0; i < kunaiRightList.Count; i++)
        {
            if(!kunaiRightList[i].activeInHierarchy)
            {
                return kunaiRightList[i];
            }
        }
        return null;
    }
    
}
