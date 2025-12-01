# Strategy Pattern - Effect System Documentation

## 📋 Tổng Quan

Hệ thống Effect này sử dụng **Strategy Pattern** để quản lý và thực thi các loại effect khác nhau (Damage, Shield, Buff, Scout, Storage).

## 🏗️ Cấu Trúc

```
EffectSystem/
├── IEffectStrategy.cs          # Interface định nghĩa cách thực thi effect
├── EffectStrategies.cs         # Các strategy cụ thể (Damage, Shield, Buff, etc.)
├── EffectStrategyFactory.cs    # Factory để tạo strategies
├── EffectExecutor.cs           # Executor thực thi effects
├── EffectReceiverInterfaces.cs # Interfaces nhận effect (IDamageReceiver, etc.)
└── EffectSystemExample.cs      # Ví dụ sử dụng
```

## 🔧 Cách Sử Dụng

### 1. **Thêm EffectExecutor vào GameObject**

```csharp
var effectExecutor = gameObject.AddComponent<EffectExecutor>();
```

### 2. **Thêm Receiver Interfaces vào Target**

Target object cần có các interface này để nhận effect:

```csharp
// Có thể nhận damage
targetObject.AddComponent<SimpleHealth>();  // implements IDamageReceiver

// Có thể nhận shield
targetObject.AddComponent<SimpleShield>();  // implements IShieldReceiver

// Có thể nhận buff
targetObject.AddComponent<SimpleBuff>();    // implements IBuffReceiver

// Có thể nhận scout
targetObject.AddComponent<SimpleScout>();   // implements IScoutReceiver

// Có thể nhận storage
targetObject.AddComponent<SimpleStorage>();  // implements IStorageReceiver
```

### 3. **Thực thi Effect từ GuData**

```csharp
GuData guData = GUFactory.Get("GU001");
effectExecutor.ExecuteEffectFromGU(guData, target);
```

### 4. **Thực thi Effect trực tiếp**

```csharp
var effect = new Effect
{
    damage = new DamageEffect { value = 50, scalingRatio = 0.5f },
    cooldownSec = 2f,
    check_GU = false
};

effectExecutor.ExecuteEffect(effect, target);
```

## ✨ Lợi Ích của Strategy Pattern

| Lợi ích | Giải thích |
|--------|-----------|
| **Dễ thêm effect mới** | Chỉ cần tạo class inherit `IEffectStrategy` |
| **Không cần if-else** | Logic được chia nhỏ theo strategy |
| **Dễ test** | Mỗi strategy có thể test riêng |
| **Dễ bảo trì** | Thay đổi logic effect không ảnh hưởng đến code khác |
| **Linh hoạt** | Có thể combine nhiều effects dễ dàng |

## 🎮 Thêm Effect Type Mới

### Bước 1: Tạo Strategy Class

```csharp
public class CustomEffectStrategy : IEffectStrategy
{
    public string GetEffectName() => "CustomEffect";
    
    public bool CanExecute(GameObject target)
    {
        return target != null;
    }
    
    public void Execute(GameObject target, EffectContext context)
    {
        // Logic thực thi effect
        Debug.Log($"Custom effect executed on {target.name}");
    }
}
```

### Bước 2: Thêm vào Factory

```csharp
// Trong EffectStrategyFactory.cs
case "custom":
    return new CustomEffectStrategy();
```

### Bước 3: Sử dụng

```csharp
var strategy = EffectStrategyFactory.CreateStrategy("custom");
strategy.Execute(target, context);
```

## 📝 Ví Dụ Thực Tế

```csharp
public class Player : MonoBehaviour, IDamageReceiver, IBuffReceiver
{
    public float health = 100;
    private Dictionary<string, float> buffs = new();
    
    void Start()
    {
        var executor = gameObject.AddComponent<EffectExecutor>();
        GuData gu = GUFactory.Get("GU001");
        
        // Thực thi tất cả effects từ GU lên player này
        executor.ExecuteEffectFromGU(gu, gameObject);
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Player took {damage} damage. Current health: {health}");
    }
    
    public void AddBuff(string stat, float value)
    {
        buffs[stat] = value;
        Debug.Log($"Player received {stat} buff: {value}");
    }
}
```

## 🔍 So Sánh Trước và Sau

### ❌ Trước (Structured)
```csharp
if (effect.damage != null) ExecuteDamage(effect.damage, target);
if (effect.shield != null) ExecuteShield(effect.shield, target);
if (effect.buff != null) ExecuteBuff(effect.buff, target);
// ... 20 dòng code khác ...
```

### ✅ Sau (Strategy Pattern)
```csharp
effectExecutor.ExecuteEffect(effect, target);
// Done! Strategy Pattern xử lý tất cả
```

## 📌 Ghi Chú Quan Trọng

1. **EffectContext** chứa tất cả dữ liệu cần thiết để thực thi effect
2. **Receiver Interfaces** là cách để targets chỉ định chúng có thể nhận effect gì
3. **EffectExecutor** cache strategies để tối ưu performance
4. **CanExecute** kiểm tra trước khi thực thi để tránh lỗi

## 🚀 Tiếp Theo

- Thêm animation/VFX khi effect được thực thi
- Thêm particle effects và sound effects
- Thêm effect cooldown management
- Thêm effect duration tracking
- Thêm analytics để track effect usage
