using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataPersistanceManager : MonoBehaviour
{
    public static DataPersistanceManager Instance;
    private const string DATA_FILE_PATH = "/data.json";

    public bool inventoryUploaded = false;

    public List<ConvertedItem> convertedItemList;
    //public List<string> stringList;
    public List<Item> localItemList;


    Player player;


    public class ConvertedItem
    {
        //public Recollectable jsonItemSO;
        public string itemSO;
        public int amount;
    }
    public string village;

    public class SaveObject
    {
        public List<ConvertedItem> saveItemList = new ();
        //public List<string> stringList;
        //public string villager;
    }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //localItemList = new List<Item>();

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.O))
        {
            player = FindObjectOfType<Player>();
            List<ConvertedItem> list = SaveInventory(player.GetInventory().GetItemList());
            if (player.GetInventory().GetItemList() != null)
            {
                Debug.Log("PART 2");

                foreach (ConvertedItem jsonItem in list)
                {
                    Debug.Log("object of the saveItemList " + "Item: " + jsonItem.itemSO + ", Amount: " + jsonItem.amount);
                }
            }
            
        }
    }

    public List<ConvertedItem> SaveInventory(List<Item> listToSave)
    {
        Debug.Log("SaveInventory / DataManager");
        int i = 0;
        SaveObject saveObejct = new (); //create a new save object to store the inventory 

        foreach (Item itemToSave in listToSave) //per cada item de sa llista, feim un item amb diferent tipus
        {
            ConvertedItem convertedItem = new () { itemSO = itemToSave.itemSO.name, amount = itemToSave.amount };

            saveObejct.saveItemList.Add(convertedItem); //guardam s'item convertit a sa llista dins es save object
            Debug.Log("item" + i + saveObejct.saveItemList[i]);
            i++;
        }

        string savedObjectJson = JsonUtility.ToJson(saveObejct);

        // Ruta completa del archivo, se concatena con la ruta del directorio de datos persistente y el nombre del archivo
        string filePath = Application.persistentDataPath + DATA_FILE_PATH;
       
        // Escribe la cadena JSON en el archivo
        File.WriteAllText(filePath, savedObjectJson);

        Debug.Log("Inventory saved to: " + filePath);

        //if(saveObejct.saveItemList.Count < 0)
        //{
        //    Debug.Log("SVEITEMLIST COUNT 0");
        //}

        //foreach (ConvertedItem jsonItem in saveObejct.saveItemList)
        //{
        //    Debug.Log("object of the saveItemList " + "Item: " + jsonItem.itemSO + ", Amount: " + jsonItem.amount);
        //}

        //if (saveObejct == null)
        //{
        //    Debug.Log("SAVE OBJ NULL!!!!");
        //}
        return saveObejct.saveItemList;
    }

    public List<Item> LoadInventory()
    {
        //Debug.Log("LoadInventory() / DataManager");
        ////we make a new inventory for create a new list with all loaded elements
        //Inventory loadedInventory = new Inventory();

        //string filePath = Application.persistentDataPath + DATA_FILE_PATH;

        //if (File.Exists(filePath))
        //{
        //    string savedObjectString = File.ReadAllText(filePath);

        //    SaveObject saveObject = JsonUtility.FromJson<SaveObject>(savedObjectString);

        //    if (saveObject.saveItemList != null)
        //    {
        //        foreach (JsonItem jsonItem in saveObject.saveItemList)
        //        {
        //            Item newItem = new Item
        //            {
        //                itemSO = GameAssets.Instance.GetRecollectableFromString(jsonItem.itemSO),
        //                amount = jsonItem.amount
        //            };

        //            loadedInventory.GetItemList().Add(newItem);
        //        }
        //    }
        //    else
        //    {
        //        Debug.Log("save object. saveItemList == null");
        //    }
        //}
        //else
        //{
        //    // Aquí no tendríamos que caer nunca
        //    Debug.LogError("No save file");
        //}

        return localItemList;

    }

    public void RemoveOneItem(Item itemToRemove)
    {
        List<Item> list = LoadInventory();
        foreach(Item item in list)
        {
            if(item == itemToRemove)
            {
                if(item.amount != 1)
                {
                    item.amount--;
                }
                else
                {
                    list.Remove(itemToRemove); //en teoria és lo mateix posar aquí itemToRemove que item tot sol asi que nice
                }
            }
        }
    }

    public void SaveVillage(Village village)
    {
        //SaveObject saveObject = new SaveObject { villager = village.name };
        //string savedObjectJson = JsonUtility.ToJson(saveObject);

        //string filePath = Application.persistentDataPath + DATA_FILE_PATH;

        ////Escribe la cadena JSON en el archivo
        //File.WriteAllText(filePath, savedObjectJson);

        //Debug.Log("the villager saved is " + saveObject.villager);

        //Debug.Log("villager saved to: " + filePath);
    }

    public Village LoadVillage()
    {
        //string filePath = Application.persistentDataPath + DATA_FILE_PATH;

        //if (!File.Exists(filePath))
        //{
        //    Debug.LogWarning("Save file not found at: " + filePath);
        //    return null;
        //}

        //string savedObjectString = File.ReadAllText(filePath);

        //SaveObject saveObject = JsonUtility.FromJson<SaveObject>(savedObjectString);

        //Debug.Log(GameAssets.Instance.GetVillageFromString(saveObject.villager));

        return GameAssets.Instance.paco;
    }

}
