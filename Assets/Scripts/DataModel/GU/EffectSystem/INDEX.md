# 📚 EffectSystem - File Index & Navigation Guide

## 📂 Cấu Trúc Thư Mục

```
EffectSystem/
├── Core Files (Quan trọng)
│   ├── IEffectStrategy.cs              🔵 Interface chính
│   ├── EffectStrategies.cs             🟢 5 Strategies
│   ├── EffectStrategyFactory.cs        🟠 Factory
│   └── EffectExecutor.cs               🔴 Executor
│
├── Integration Files (Để dùng)
│   ├── EffectReceiverInterfaces.cs     🟡 Receiver interfaces
│   └── EffectSystemExample.cs          ⚪ Example usage
│
└── Documentation Files (Để đọc)
    ├── QUICK_START.md                  ✅ Bắt đầu nhanh
    ├── README.md                        📖 Tài liệu đầy đủ
    ├── REFACTORING_SUMMARY.md          📊 Tóm tắt refactoring
    ├── ARCHITECTURE_DIAGRAM.md         🏗️ Diagrams
    └── INDEX.md (file này)             📑 Navigation
```

## 🎯 Quick Navigation

### Nếu bạn muốn...

| Mục Đích | File | Phần |
|---------|------|------|
| **Bắt đầu nhanh** | `QUICK_START.md` | Toàn bộ |
| **Hiểu architecture** | `ARCHITECTURE_DIAGRAM.md` | Toàn bộ |
| **Đọc full docs** | `README.md` | Toàn bộ |
| **Hiểu refactoring** | `REFACTORING_SUMMARY.md` | Toàn bộ |
| **Biết interface** | `IEffectStrategy.cs` | Lines 1-25 |
| **Học strategies** | `EffectStrategies.cs` | Mỗi class |
| **Dùng factory** | `EffectStrategyFactory.cs` | `CreateStrategy()` |
| **Thực thi effect** | `EffectExecutor.cs` | `ExecuteEffect()` |
| **Test** | `EffectSystemExample.cs` | `TestDamageEffect()`, etc. |

## 📖 Recommended Reading Order

### Level 1: 30 phút (Cơ bản)
1. Đọc `QUICK_START.md` (5 min)
2. Xem code trong `EffectSystemExample.cs` (15 min)
3. Run example script (5 min)
4. Gọi `ExecuteEffect()` trong code của bạn (5 min)

### Level 2: 1 giờ (Hiểu sâu)
1. Đọc `README.md` (15 min)
2. Xem diagrams trong `ARCHITECTURE_DIAGRAM.md` (15 min)
3. Đọc từng strategy trong `EffectStrategies.cs` (20 min)
4. Hiểu `EffectExecutor.cs` logic (10 min)

### Level 3: 2 giờ (Expert)
1. Đọc `REFACTORING_SUMMARY.md` (20 min)
2. So sánh trước/sau (20 min)
3. Thêm effect type mới (30 min)
4. Tạo receiver interfaces riêng (30 min)
5. Test & debug (20 min)

## 🔍 File Details

### 1️⃣ IEffectStrategy.cs (Interface)

```csharp
public interface IEffectStrategy
{
    string GetEffectName();
    bool CanExecute(GameObject target);
    void Execute(GameObject target, EffectContext context);
}

public class EffectContext { /* 15+ fields */ }
```

**Khi dùng**: Khi bạn tạo effect strategy mới, phải inherit interface này

**Ví dụ**: `public class MyEffectStrategy : IEffectStrategy { }`

---

### 2️⃣ EffectStrategies.cs (5 Strategies)

```csharp
✅ DamageEffectStrategy     → Gây sát thương
✅ ShieldEffectStrategy     → Tạo khiên bảo vệ
✅ BuffEffectStrategy       → Tăng stats
✅ ScoutEffectStrategy      → Phát hiện địch
✅ StorageEffectStrategy    → Thêm chỗ chứa
```

**Khi dùng**: Khi bạn muốn thêm effect type mới, copy pattern từ đây

**Ví dụ**: Copy `DamageEffectStrategy`, rename thành `HealEffectStrategy`, sửa logic

---

### 3️⃣ EffectStrategyFactory.cs (Factory)

```csharp
public static IEffectStrategy CreateStrategy(string effectType)
{
    switch(effectType)
    {
        case "damage": return new DamageEffectStrategy();
        case "shield": return new ShieldEffectStrategy();
        // ...
    }
}
```

**Khi dùng**: Khi bạn thêm effect type mới, thêm case mới vào đây

**Ví dụ**: `case "heal": return new HealEffectStrategy();`

---

### 4️⃣ EffectExecutor.cs (Executor)

```csharp
public void ExecuteEffectFromGU(GuData guData, GameObject target)
public void ExecuteEffect(Effect effect, GameObject target)
```

**Khi dùng**: Khi bạn muốn thực thi effect, gọi 1 trong 2 methods này

**Ví dụ**:
```csharp
var executor = gameObject.AddComponent<EffectExecutor>();
executor.ExecuteEffectFromGU(guData, targetObject);
```

---

### 5️⃣ EffectReceiverInterfaces.cs (Receivers)

```csharp
✅ IDamageReceiver      → Implement để nhận damage
✅ IShieldReceiver      → Implement để nhận shield
✅ IBuffReceiver        → Implement để nhận buff
✅ IScoutReceiver       → Implement để nhận scout
✅ IStorageReceiver     → Implement để nhận storage
```

**Khi dùng**: Khi bạn tạo target object, implement interface nào có thể nhận

**Ví dụ**:
```csharp
public class Enemy : MonoBehaviour, IDamageReceiver
{
    public void TakeDamage(float damage) { /* ... */ }
}
```

---

### 6️⃣ EffectSystemExample.cs (Demo)

```csharp
public void TestDamageEffect()
public void TestShieldEffect()
public void TestBuffEffect()
public void TestScoutEffect()
public void TestStorageEffect()
public void TestAllEffects()
```

**Khi dùng**: Khi bạn muốn test effect, chạy example script này

**Ví dụ**: Press `D` key → Test damage effect

---

### 📖 IEffectStrategy.cs

**Dòng 1-15**: Interface definition  
**Dòng 16-30**: EffectContext class  

**Cần đọc**: Nếu bạn tạo effect strategy mới

---

### 📖 EffectStrategies.cs

**Dòng 1-30**: DamageEffectStrategy  
**Dòng 31-60**: ShieldEffectStrategy  
**Dòng 61-90**: BuffEffectStrategy  
**Dòng 91-120**: ScoutEffectStrategy  
**Dòng 121-150**: StorageEffectStrategy  

**Cần đọc**: Khi bạn muốn hiểu cách implement strategy

---

### 📖 EffectExecutor.cs

**Dòng 1-20**: Fields & cache  
**Dòng 21-45**: ExecuteEffectFromGU()  
**Dòng 46-70**: ExecuteEffect()  
**Dòng 71-100**: Helper methods  

**Cần đọc**: Khi bạn muốn dùng executor

---

### 📖 EffectReceiverInterfaces.cs

**Dòng 1-20**: IDamageReceiver interface  
**Dòng 21-40**: IShieldReceiver interface  
**Dòng 41-60**: IBuffReceiver interface  
**Dòng 61-80**: IScoutReceiver interface  
**Dòng 81-100**: IStorageReceiver interface  
**Dòng 101-200**: Simple implementations  

**Cần đọc**: Khi bạn tạo receiver classes riêng

---

### 📖 EffectSystemExample.cs

**Dòng 1-30**: Setup trong Start()  
**Dòng 31-50**: TestDamageEffect()  
**Dòng 51-70**: TestShieldEffect()  
... và các test khác  
**Dòng 200+**: OnGUI() - test controls  

**Cần đọc**: Khi bạn muốn test hoặc learn pattern

---

## 🎬 Typical Usage Flow

```
1. Add EffectExecutor
   └─ var executor = gameObject.AddComponent<EffectExecutor>();

2. Add Receiver interfaces to target
   └─ target.AddComponent<SimpleHealth>();

3. Get GuData
   └─ var gu = GUFactory.Get("GU001");

4. Execute Effect
   └─ executor.ExecuteEffectFromGU(gu, target);

5. Done! ✅
```

## 🆘 Troubleshooting

| Masalah | Solusi |
|--------|--------|
| Target tidak nhận damage | Thêm `IDamageReceiver` interface |
| Strategy return null | Kiểm tra case dalam Factory |
| CanExecute() always false | Implement `CanExecute()` benar |
| Memory leak | Call `ClearStrategyCache()` |

## 📚 Learning Resources

1. **Understand Strategy Pattern**
   - Design Patterns: Elements of Reusable Object-Oriented Software
   - Head First Design Patterns

2. **Understand your code**
   - Read `QUICK_START.md` (5 min)
   - Read `ARCHITECTURE_DIAGRAM.md` (10 min)
   - Read `REFACTORING_SUMMARY.md` (15 min)

3. **Practice**
   - Run `EffectSystemExample.cs`
   - Add 1 effect type (heal)
   - Test dalam game logic

## ✅ Implementation Checklist

- [ ] Copy EffectSystem folder
- [ ] Read QUICK_START.md
- [ ] Run EffectSystemExample
- [ ] Add receiver interfaces to targets
- [ ] Call ExecuteEffect() dalam code
- [ ] Add custom effect type
- [ ] Test in your game
- [ ] Remove or modify as needed

## 📞 Quick Reference

| Task | Code |
|------|------|
| **Get Executor** | `var exec = gameObject.AddComponent<EffectExecutor>();` |
| **Execute Effect** | `exec.ExecuteEffect(effect, target);` |
| **Execute from GU** | `exec.ExecuteEffectFromGU(guData, target);` |
| **Create Strategy** | `var s = EffectStrategyFactory.CreateStrategy("damage");` |
| **Clear Cache** | `exec.ClearStrategyCache();` |

---

**Ghi chú**: File này là navigation guide. Nếu bạn lúng túng, hãy:
1. Read QUICK_START.md
2. Run EffectSystemExample.cs
3. Copy code pattern vào project
4. Test & modify as needed

**Happy coding!** 🚀
