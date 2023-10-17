using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
   public static InventoryManager Instance;
   public List<InventoryItem> Items = new List<InventoryItem>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public GameObject InventoryPanel;
    private bool isInventoryActive = false;

    [Space]
    [Header("아이템 Info")]
    [SerializeField] private Text UI_itemType;
    [SerializeField] private Text UI_itemName;
    [SerializeField] private Text UI_itemDescription;
    [SerializeField] private Image UI_itemImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
       
    }


    // 아이템을 리스트에 추가하고 중복을 확인하는 메서드
    // 큰 데이터 집합에서는 HashSet을 사용하는 것이 시간 복잡도면에서 효율적!
    public bool Add(InventoryItem item)
    {
        // 리스트에 아이템이 있는지 확인
        if (!Items.Contains(item))
        {
            Items.Add(item);

            
            return true;
        }

        // 이미 존재하는 아이템이므로 추가되지 않음
        else { return false; }
    }


    public void Remove(InventoryItem item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        // 인벤토리 창을 열 때 마다 아이템 먹었던 개수만큼 다시 증가되는 현상 방지
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
    }

    public void ShowInventory()
    {
        // 상태 토글
        isInventoryActive = !isInventoryActive;
        // 오브젝트 상태에 따라 활성/비활성 설정
        InventoryPanel.SetActive(isInventoryActive);
        
    }

    public void SetInventoryInfo()
    {
        // 현재 클릭된 버튼을(아이템 정보) 얻어옵니다.
        Button clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        // 클릭된 버튼의 텍스트 컴포넌트를 가져옵니다.
        Text itemNameText = clickedButton.transform.Find("ItemName").GetComponent<Text>();

        // 클릭한 아이템의 이름을 가져옵니다.
        string itemName = itemNameText.text;

        // 아이템 이름을 기반으로 해당 아이템을 찾아옵니다.
        InventoryItem clickedItem = Items.Find(item => item.itemName == itemName);

        // 클릭한 아이템의 정보를 출력합니다.
        if (clickedItem != null)
        {
            //Debug.Log("아이템 타입: " + clickedItem.itemType);
            //Debug.Log("아이템 이름: " + clickedItem.itemName);
            //Debug.Log("아이템 설명: " + clickedItem.itemInfo);


            UI_itemType.text = clickedItem.itemType;
            UI_itemName.text = clickedItem.itemName;
            UI_itemDescription.text = clickedItem.itemInfo;
            UI_itemImage.sprite = clickedItem.icon;

        }
    }
}
