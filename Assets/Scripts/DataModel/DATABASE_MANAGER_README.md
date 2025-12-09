# Database Manager - JSON to ScriptableObject Importer

Hệ thống toàn diện để load tất cả dữ liệu JSON thành ScriptableObject (SO) trong Unity.

## 📁 Cấu Trúc Dự Án

```
Assets/
├── Scripts/
│   └── DataModel/
│       ├── DatabaseManager.cs                 (Main database manager)
│       ├── BulkDatabaseImporter.cs             (Bulk import tool - DÙNG CÁI NÀY!)
│       ├── GU/
│       │   ├── GU_SO.cs                        (GU ScriptableObject)
│       │   ├── GU_json.cs                      (GU JSON model)
│       │   └── GU_importer.cs                  (GU importer)
│       ├── Item/
│       │   ├── Item_SO.cs                      (Item ScriptableObject)
│       │   ├── Item_json.cs                    (Item JSON model)
│       │   └── Item_importer.cs                (Item importer)
│       ├── Recipe/
│       │   ├── Recipe_SO.cs                    (Recipe ScriptableObject)
│       │   ├── Recipe_json.cs                  (Recipe JSON model)
│       │   └── Recipe_importer.cs              (Recipe importer)
│       ├── Enemy/
│       │   ├── Enemy_SO.cs                     (Enemy ScriptableObject)
│       │   ├── Enemy_json.cs                   (Enemy JSON model)
│       │   └── Enemy_importer.cs               (Enemy importer)
│       ├── GUMaster/
│       │   ├── GUMaster_SO.cs                  (GUMaster ScriptableObject)
│       │   ├── GUMaster_json.cs                (GUMaster JSON model)
│       │   └── GUMaster_importer.cs            (GUMaster importer)
│       ├── Aptitude/
│       │   ├── Aptitude_SO.cs                  (Aptitude ScriptableObject)
│       │   ├── Aptitude_json.cs                (Aptitude JSON model)
│       │   └── Aptitude_importer.cs            (Aptitude importer)
│       └── Aperture/
│           ├── Aperture_SO.cs                  (Aperture ScriptableObject)
│           ├── Aperture_json.cs                (Aperture JSON model)
│           └── Aperture_importer.cs            (Aperture importer)
└── Database/
    ├── GU_info.GU_collection.json              (GU data)
    ├── GU_info.Item_collection.json            (Item data)
    ├── GU_info.GU_recipes_collection.json      (Recipe data)
    ├── GU_info.Enermy_collection.json          (Enemy data)
    ├── GU_info.GU_master.json                  (GUMaster data)
    ├── Aptitude.json                           (Aptitude data)
    └── Aperture.json                           (Aperture/Player data)
```

## 🚀 Sử Dụng (NHANH NHẤT!)

### **Bulk Import Tất Cả Dữ Liệu (1 Click!)**

1. Vào menu: **Tools → Database → Bulk Import All Data**
2. Kéo thả 7 file JSON (hoặc chỉ những file bạn muốn):
   - GU JSON File: `GU_info.GU_collection.json`
   - Item JSON File: `GU_info.Item_collection.json`
   - Recipe JSON File: `GU_info.GU_recipes_collection.json`
   - Enemy JSON File: `GU_info.Enermy_collection.json`
   - GUMaster JSON File: `GU_info.GU_master.json`
   - Aptitude JSON File: `Aptitude.json`
   - Aperture JSON File: `Aperture.json`
3. Nhấn **Import All** → Chờ progress bar → Hoàn thành! 🎉

**Kết quả:** Tất cả ScriptableObjects sẽ được lưu vào:
- `Assets/Resources/GU/`
- `Assets/Resources/Item/`
- `Assets/Resources/Recipe/`
- `Assets/Resources/Enemy/`
- `Assets/Resources/GUMaster/`
- `Assets/Resources/Aptitude/`
- `Assets/Resources/Aperture/`

---

### **Import Từng Loại Dữ Liệu (Nếu muốn)**

#### GU:
- Vào: **Tools → GU → Import GU from JSON**
- Chọn file JSON → Nhấn Import

#### Item:
- Vào: **Tools → Item → Import Item from JSON**
- Chọn file JSON → Nhấn Import

#### Recipe:
- Vào: **Tools → Recipe → Import Recipe from JSON**
- Chọn file JSON → Nhấn Import

#### Enemy:
- Vào: **Tools → Enemy → Import Enemy from JSON**
- Chọn file JSON → Nhấn Import

## 💾 Sử Dụng DatabaseManager Trong Runtime

### Khởi Tạo:
```csharp
// DatabaseManager sẽ tự động init khi game chạy
// Nó sẽ load tất cả ScriptableObjects từ Resources/
```

### Truy Vấn Dữ Liệu:

#### GU:
```csharp
GU_SO gu = DatabaseManager.Instance.GetGU("nguyet_quang_gu");
GU_SO[] allGU = DatabaseManager.Instance.GetAllGU();
GU_SO[] damageGU = DatabaseManager.Instance.GetGUByCategory("CongPhat");
bool hasGU = DatabaseManager.Instance.HasGU("code");
```

#### Item:
```csharp
Item_SO item = DatabaseManager.Instance.GetItem("canh_hoa_nguyet_lan");
Item_SO[] allItems = DatabaseManager.Instance.GetAllItems();
Item_SO[] currencyItems = DatabaseManager.Instance.GetItemsByType("currency");
bool hasItem = DatabaseManager.Instance.HasItem("code");
```

#### Recipe:
```csharp
Recipe_SO recipe = DatabaseManager.Instance.GetRecipe("recipe_code");
Recipe_SO[] allRecipes = DatabaseManager.Instance.GetAllRecipes();
bool hasRecipe = DatabaseManager.Instance.HasRecipe("code");
```

#### Enemy:
```csharp
Enemy_SO enemy = DatabaseManager.Instance.GetEnemy("enemy_code");
Enemy_SO[] allEnemies = DatabaseManager.Instance.GetAllEnemies();
Enemy_SO[] meleeEnemies = DatabaseManager.Instance.GetEnemiesByAI("melee");
bool hasEnemy = DatabaseManager.Instance.HasEnemy("code");
```

#### GUMaster:
```csharp
GUMaster_SO guMaster = DatabaseManager.Instance.GetGUMaster("code");
GUMaster_SO[] allGUMasters = DatabaseManager.Instance.GetAllGUMasters();
```

#### Aptitude:
```csharp
Aptitude_SO aptitude = DatabaseManager.Instance.GetAptitude("Giap");
Aptitude_SO[] allAptitudes = DatabaseManager.Instance.GetAllAptitudes();
Aptitude_SO[] normalAptitudes = DatabaseManager.Instance.GetAptitudesByTag("normal");
```

#### Aperture (Player Data):
```csharp
Aperture_SO aperture = DatabaseManager.Instance.GetAperture("aperture_001");
Aperture_SO playerAperture = DatabaseManager.Instance.GetApertureByPlayerId("player_001");
Aperture_SO[] allApertures = DatabaseManager.Instance.GetAllApertures();
```

### Statistics:
```csharp
DatabaseManager.Instance.PrintDatabaseStats();
// Output:
// ===== Database Statistics =====
// GU Count: 50
// Item Count: 100
// Recipe Count: 30
// Enemy Count: 20
// GUMaster Count: 15
// Aptitude Count: 10
// Aperture Count: 5
// Total Objects: 230
```

## 📝 Format JSON Yêu Cầu

Tất cả JSON files phải là **array of objects**:

```json
[
  {
    "code": "unique_code",
    "name": "Display Name",
    ...
  },
  {
    "code": "another_code",
    "name": "Another Name",
    ...
  }
]
```

**Lưu ý:** Field `code` **bắt buộc** có và phải **duy nhất**!

## 🔧 Setup DatabaseManager trong Scene

1. Tạo một empty GameObject tên `DatabaseManager`
2. Thêm component `DatabaseManager` vào nó
3. (Optional) Assign arrays trong inspector hoặc để nó tự load từ Resources
4. Đặt GameObject này trong scene chính để nó DontDestroyOnLoad
5. Hoặc thêm vào một prefab và load lúc startup

## ⚠️ Lưu Ý

- ScriptableObjects sẽ được lưu tại **Assets/Resources/{Type}/**
- DatabaseManager tự động load từ Resources khi không có assign
- Đảm bảo **không có duplicate codes** trong JSON
- JSON files phải là array, không phải object
- Nếu import lại, cũ sẽ bị xoá (backup nếu cần!)

## 🐛 Troubleshooting

### JSON Parse Error:
- Kiểm tra JSON format (phải là array)
- Xoá comments hoặc special characters
- Validate JSON tại jsonlint.com

### ScriptableObjects không load:
- Kiểm tra folder `Assets/Resources/{Type}/` tồn tại
- Chạy import lại
- Kiểm tra console log cho errors

### DatabaseManager không tìm được object:
- Chắc chắn `code` field trong JSON đúng
- Kiểm tra code không có space hoặc special characters
- Validate: `DatabaseManager.Instance.PrintDatabaseStats()`

## 📊 Ví Dụ Sử Dụng

```csharp
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        // Lấy một Cổ
        GU_SO myGU = DatabaseManager.Instance.GetGU("nguyet_quang_gu");
        if (myGU != null)
        {
            Debug.Log($"GU: {myGU.displayName}");
            Debug.Log($"Damage: {myGU.effect.damage.value}");
        }

        // Lấy thức ăn cho Cổ
        Item_SO food = DatabaseManager.Instance.GetItem("canh_hoa_nguyet_lan");
        if (food != null)
        {
            Debug.Log($"Food: {food.displayName}");
        }

        // Lấy tất cả công thức
        Recipe_SO[] recipes = DatabaseManager.Instance.GetAllRecipes();
        Debug.Log($"Total Recipes: {recipes.Length}");

        // Thống kê
        DatabaseManager.Instance.PrintDatabaseStats();
    }
}
```

---

**Tạo bởi:** GitHub Copilot
**Phiên bản:** 1.0
