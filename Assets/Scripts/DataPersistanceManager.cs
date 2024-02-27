using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataPersistanceManager : MonoBehaviour
{
    public static DataPersistanceManager Instance;
    private const string DATA_FILE_PATH = "/data.json";

    public List<JsonItem> jsonItemList;

    public class JsonItem
    {
        //public Recollectable jsonItemSO;
        public string itemSO;
        public int amount;
    }
    public string village;

    public class SaveObject
    {
        public List<JsonItem> saveItemList;
        public string villager;
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
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SaveInventory();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadInventory();
        }
    }

    public void SaveInventory()
    {
        Debug.Log("SaveInventory / DataManager");

        jsonItemList = new List<JsonItem>();
        List<Item> itemList =  GameManager.Instance.inventory.itemList;
        foreach (Item item in itemList)
        {
            JsonItem jsonItem =  new JsonItem { itemSO = item.itemSO.name, amount = item.amount};

            jsonItemList.Add(jsonItem);
        }
        //foreach (var item in jsonItemList)
        //{
        //    Debug.Log("Item: " + item.itemSO + ", Amount: " + item.amount);
        //}
        SaveObject saveObject = new SaveObject { saveItemList = jsonItemList };

        string savedObjectJson = JsonUtility.ToJson(saveObject);

        // Ruta completa del archivo, se concatena con la ruta del directorio de datos persistente y el nombre del archivo
        string filePath = Application.persistentDataPath + DATA_FILE_PATH;
        //File.WriteAllText(Application.dataPath + DATA_FILE_PATH, savedObjectJson);

        // Escribe la cadena JSON en el archivo
        File.WriteAllText(filePath, savedObjectJson);

        Debug.Log("Inventory saved to: " + filePath);
        //Debug.Log(savedObjectJson);
    }

    public void LoadInventory()
    {
        Debug.Log("LoadInventory() / DataManager");
        if (File.Exists(Application.dataPath + DATA_FILE_PATH))
        {
            string savedObjectString = File.ReadAllText(Application.dataPath + DATA_FILE_PATH);

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(savedObjectString);

            foreach(JsonItem jsonItem in saveObject.saveItemList)
            {
                Item newItem = new Item
                {
                    itemSO = GameAssets.Instance.GetRecollectableFromString(jsonItem.itemSO),
                    amount = jsonItem.amount
                };

                GameManager.Instance.inventory.AddItem(newItem);
            }
        }
        else
        {
            // Aquí no tendríamos que caer nunca
            Debug.LogError("No save file");
        }

        foreach(Item item in GameManager.Instance.inventory.itemList)
        {
            Debug.Log(item.itemSO.name + " " + item.amount);
        }
    }

    public void SaveVillage(Village village)
    {
        SaveObject saveObject = new SaveObject { villager = village.name };
        string savedObjectJson = JsonUtility.ToJson(saveObject);

        string filePath = Application.persistentDataPath + DATA_FILE_PATH;

        // Escribe la cadena JSON en el archivo
        File.WriteAllText(filePath, savedObjectJson);

        Debug.Log("the villager saved is " + saveObject.villager);

        Debug.Log("villager saved to: " + filePath);
    }

    public Village LoadVillage()
    {
        string filePath = Application.persistentDataPath + DATA_FILE_PATH;

        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Save file not found at: " + filePath);
            return null;
        }

        string savedObjectString = File.ReadAllText(filePath);

        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(savedObjectString);

        Debug.Log(GameAssets.Instance.GetVillageFromString(saveObject.villager));

        return GameAssets.Instance.GetVillageFromString(saveObject.villager);
    }

}
