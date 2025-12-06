# 🎯 QUICK START - Strategy Pattern Effect System

## 📁 Cấu Trúc Thư Mục Mới

```
Assets/Script/DataModel/GU/
├── GU_info_Json.cs              (Giữ nguyên)
├── GuData.cs                    (Giữ nguyên)
├── GUFactory.cs                 (Giữ nguyên)
├── GuJsonImporter.cs            (Giữ nguyên)
└── EffectSystem/                (MỚI)
    ├── IEffectStrategy.cs       (Interface chính)
    ├── EffectStrategies.cs      (5 strategies: Damage, Shield, Buff, Scout, Storage)
    ├── EffectStrategyFactory.cs (Factory để tạo strategies)
    ├── EffectExecutor.cs        (Thực thi effects)
    ├── EffectReceiverInterfaces.cs (Interfaces + simple implementations)
    ├── EffectSystemExample.cs   (Ví dụ sử dụng)
    └── README.md                (Tài liệu)
```

## 🎮 Cách Sử Dụng Đơn Giản

### Cách 1: Thực thi Effect từ GU

```csharp
using Game.GU.EffectSystem;
using Game.GU.SOModel;

public class MyGame : MonoBehaviour
{
    void Start()
    {
        // Lấy EffectExecutor
        var executor = gameObject.AddComponent<EffectExecutor>();
        
        // Lấy GU data
        GuData gu = GUFactory.Get("GU001");
        
        // Thực thi tất cả effects từ GU lên target
        executor.ExecuteEffectFromGU(gu, targetObject);
    }
}
```

### Cách 2: Tạo Effect và Thực thi

```csharp
var effect = new Effect
{
    damage = new DamageEffect { value = 50, scalingRatio = 0.5f },
    cooldownSec = 2f
};

executor.ExecuteEffect(effect, targetObject);
```

### Cách 3: Sử dụng Factory để tạo Strategy

```csharp
IEffectStrategy strategy = EffectStrategyFactory.CreateStrategy("damage");
var context = new EffectContext { value = 50, scalingRatio = 0.5f };
strategy.Execute(targetObject, context);
```

## 🔄 Flow Diagram

```
GuData (từ GUFactory)
    ↓
EffectExecutor.ExecuteEffectFromGU()
    ↓
Kiểm tra từng effect (damage, shield, buff, etc.)
    ↓
EffectStrategyFactory.CreateStrategy()
    ↓
IEffectStrategy.CanExecute() - Kiểm tra có thể thực thi không?
    ↓
IEffectStrategy.Execute() - Thực thi effect
    ↓
Target nhận effect (thông qua IDamageReceiver, IShieldReceiver, etc.)
```

## ✅ Checklist để Integrate

- [ ] Copy toàn bộ EffectSystem folder vào project
- [ ] Thêm `using Game.GU.EffectSystem;` vào file cần dùng
- [ ] Thêm các Receiver interfaces vào target object (SimpleHealth, SimpleShield, etc.)
- [ ] Tạo EffectExecutor component
- [ ] Gọi ExecuteEffectFromGU() hoặc ExecuteEffect()
- [ ] Test bằng EffectSystemExample script

## 🎯 Điểm Chính

| Điểm | Giải thích |
|------|-----------|
| **Interface** | `IEffectStrategy` định nghĩa cách thực thi |
| **Strategies** | 5 class: Damage, Shield, Buff, Scout, Storage |
| **Factory** | Tạo strategy dựa trên string name |
| **Executor** | Thực thi effect với caching |
| **Receivers** | Interfaces để target nhận effect |

## 🚀 Lợi Ích So Với Cách Cũ

| Cũ | Mới |
|----|-----|
| ❌ if-else nhiều | ✅ Strategy pattern |
| ❌ Khó thêm effect | ✅ Thêm bằng 1 class |
| ❌ Code rối | ✅ Code sạch & rõ |
| ❌ Khó test | ✅ Dễ test riêng |
| ❌ Khó maintain | ✅ Dễ maintain |

## 📚 Files Tham Khảo

1. **IEffectStrategy.cs** - Bắt đầu từ đây để hiểu interface
2. **EffectStrategies.cs** - Xem cách implement strategy
3. **EffectExecutor.cs** - Xem cách sử dụng strategies
4. **EffectReceiverInterfaces.cs** - Xem receiver examples
5. **EffectSystemExample.cs** - Copy code này để test

## 💡 Tips

- Cộng **value** và **scalingRatio** để tính final damage
- Kiểm tra **CanExecute()** trước khi thực thi
- Cache strategies để tối ưu performance
- Thêm more receiver interfaces nếu cần

## ❓ Câu Hỏi Thường Gặp

**Q: Tôi có thể thêm effect type mới không?**
A: Có! Tạo class inherit `IEffectStrategy`, thêm vào Factory, xong!

**Q: Tôi có thể combine nhiều effects không?**
A: Có! EffectExecutor thực thi tất cả effects trong object Effect

**Q: Performance có tốt không?**
A: Có! Chúng ta cache strategies để tránh tạo lại nhiều lần

**Q: Tôi cần modify Receiver classes không?**
A: Không bắt buộc. Bạn có thể implement interfaces riêng của bạn

---

**Tóm lại**: Copy EffectSystem folder, add components, call ExecuteEffect(), done! 🎉
