# KnightFall Code Conventions

This document contains the coding conventions for **KnightFall**.  


---

## General

- Keep code clear and easy to understand.
- Use descriptive names for classes and variables.
- A class should preferably have one main responsibility.
- Avoid unnecessarily large scripts.
- Reuse existing code when it makes sense.
- Remove test code and old comments before committing.

---

## Naming

Use the following naming style in C#:

- **Classes:** PascalCase  
  `PlayerMovement`, `EnemyHealth`, `WeaponController`

- **Methods:** PascalCase  
  `TakeDamage()`, `Attack()`, `LoadLevel()`

- **Variables:** camelCase  
  `moveSpeed`, `currentHealth`, `attackDamage`

- **Private fields:** `_camelCase`  
  `_moveSpeed`, `_currentHealth`, `_weaponData`

- **Booleans:** use clear question-style names  
  `isDead`, `isBlocking`, `canAttack`

- **File name = class name**  
  `PlayerMovement.cs` should contain `PlayerMovement`

Avoid names such as:

```text
Script1
Test123
Stuff
Thing
Manager2
NewBehaviourScript
```

---

## Unity Project Structure

Keep the Unity project structure clear and organized.

```text
Assets/
└── KnightFall/
    ├── Art/
    │   ├── Sprites/
    │   ├── Animations/
    │   └── VFX/
    │
    ├── Audio/
    │   ├── Music/
    │   └── SFX/
    │
    ├── Data/
    │   ├── Enemies/
    │   ├── Weapons/
    │   └── Levels/
    │
    ├── Prefabs/
    │   ├── Player/
    │   ├── Enemies/
    │   ├── Weapons/
    │   └── UI/
    │
    ├── Scenes/
    │
    └── Scripts/
        ├── Player/
        ├── Enemies/
        ├── Weapons/
        ├── Combat/
        ├── Levels/
        ├── UI/
        ├── Audio/
        └── Core/
```

### Examples

```text
Scripts/Player/PlayerMovement.cs
Scripts/Player/PlayerHealth.cs

Scripts/Enemies/EnemyMovement.cs
Scripts/Enemies/EnemyHealth.cs

Scripts/Weapons/Sword.cs
Scripts/Weapons/Bow.cs

Scripts/Levels/LevelManager.cs
Scripts/UI/HealthBar.cs
```

Do not create folders such as:

```text
Old
Backup
Final
Final2
TestStuff
New Folder
```

Git already keeps old versions of files.

---

## Classes

- Each class should have one main responsibility.
- Prefer several small scripts over one very large script.
- Do not put movement, health, weapons, and UI all inside one `PlayerController`.

For example:

```text
Player
├── PlayerMovement
├── PlayerHealth
├── PlayerCombat
└── WeaponController
```

For enemies:

```text
Enemy
├── EnemyMovement
├── EnemyHealth
└── EnemyAttack
```

This makes the code easier to maintain and expand later.

---

## Inspector Variables

For variables that need to be editable in the Unity Inspector, use:

```csharp
[SerializeField] private float _moveSpeed = 5f;
[SerializeField] private int _maxHealth = 100;
```

Prefer this over:

```csharp
public float moveSpeed;
public int health;
```

Use `public` only when other scripts really need direct access.

`[SerializeField]` allows a private variable to stay protected in code while still being editable in the Unity Inspector.

---

## Methods

- Keep methods short.
- Use clear and descriptive method names.
- A method should preferably do one thing.
- Use early `return` statements to avoid unnecessary nesting.

Good:

```csharp
public void TryAttack()
{
    if (isDead)
    {
        return;
    }

    if (!canAttack)
    {
        return;
    }

    Attack();
}
```

Avoid:

```csharp
public void DoStuff()
{
    // Too many unrelated actions
}
```

---

## Unity Methods

Use Unity lifecycle methods for the correct purpose.

### Awake

Use `Awake()` for getting components or setting up references.

```csharp
private void Awake()
{
    _rigidbody = GetComponent<Rigidbody2D>();
}
```

### Start

Use `Start()` for setup when the game starts.

```csharp
private void Start()
{
    _currentHealth = _maxHealth;
}
```

### Update

Use `Update()` for input and gameplay logic that needs to be checked every frame.

```csharp
private void Update()
{
    ReadInput();
}
```

### FixedUpdate

Use `FixedUpdate()` for physics and `Rigidbody2D` movement.

```csharp
private void FixedUpdate()
{
    Move();
}
```

---

## 2D Unity

KnightFall is a 2D game, so use Unity's 2D systems.

Use:

```text
Rigidbody2D
Collider2D
Physics2D
RaycastHit2D
```

Avoid accidentally using the 3D versions:

```text
Rigidbody
Collider
Physics
RaycastHit
```

---

## Components

Store components in a variable when they are used more than once.

Good:

```csharp
private Rigidbody2D _rigidbody;

private void Awake()
{
    _rigidbody = GetComponent<Rigidbody2D>();
}
```

Avoid doing this every frame:

```csharp
private void Update()
{
    GetComponent<Rigidbody2D>();
}
```

---

## Player Input

Keep input handling organized.

For KnightFall:

```text
WASD        = Move
Mouse       = Aim
Left Click  = Attack
Right Click = Block
```

Try not to check input in many different scripts.

For example:

```text
PlayerInput
    ↓
PlayerMovement
PlayerCombat
```

---

## Weapons

Weapons have different stats.

For example:

```text
Sword
- normal damage
- normal speed

Axe
- high damage
- slower speed

Bow
- ranged
- cannot be used for blocking
```

Do not place weapon values randomly throughout the code.

Use a `WeaponData` ScriptableObject, for example:

```csharp
[CreateAssetMenu(menuName = "KnightFall/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _range;
}
```

This makes weapon values easy to change later in Unity without changing the code.

---

## Enemies

Use a similar base structure for enemies.

Examples:

```text
Goblin
Skeleton
Bat
Mercenary
```

Many properties can use the same system:

```text
health
moveSpeed
damage
attackSpeed
range
```

Only create separate code when an enemy really needs unique behavior.

---

## Magic Numbers

Avoid unexplained numbers in code.

Avoid:

```csharp
health -= 25;
```

Better:

```csharp
[SerializeField] private int _attackDamage = 25;

health -= _attackDamage;
```

This makes it clear what the number is used for.

---

## Comments

Use comments only when they explain something that is not immediately obvious.

Good:

```csharp
// Prevents the player from attacking while blocking.
if (isBlocking)
{
    return;
}
```

Unnecessary:

```csharp
// Set health to 100
health = 100;
```

Do not leave large blocks of old code commented out.

---

## Error Handling

- Check important references.
- Make errors clear.
- Do not ignore errors without a reason.

Example:

```csharp
if (_weaponData == null)
{
    Debug.LogError("WeaponData is missing.", this);
    return;
}
```

---

## Git and GitHub

Use clear commit messages.

The Git history shows what was added or changed in each commit. Because of this, every commit message should briefly describe the change.

Good examples:

```text
Add player movement
Add sword attack
Add goblin enemy
Fix player blocking
Fix bow collision
Add level complete screen
```

Avoid:

```text
Update
Fix
Stuff
Test
Final
Works
```

### Git Rules

Commit:

```text
Assets/
Packages/
ProjectSettings/
.meta files
```

Normally do not commit:

```text
Library/
Temp/
Logs/
obj/
```

Do not delete `.meta` files randomly. Unity uses them to keep asset references intact.

---

## Scenes

Use clear scene names.

Good:

```text
MainMenu
Gameplay
Grasslands
TestCombat
```

Avoid:

```text
Scene1
NewScene
Test123
FinalScene2
```

---

## Prefabs

Create a prefab for GameObjects that are reused.

Examples:

```text
Player
Goblin
Skeleton
Arrow
Sword
HealthBar
Coin
```

A prefab is a reusable Unity GameObject template.  
For example, if you create a Goblin with a sprite, `Collider2D`, `Rigidbody2D`, health script, and attack script, you can save it as a prefab and reuse it in multiple levels.

Change the prefab itself when the change should apply to all instances of that object.

---

## Code Style

Always use braces.

Good:

```csharp
if (isDead)
{
    return;
}
```

Avoid:

```csharp
if (isDead) return;
```

Use 4 spaces for indentation.

Keep formatting consistent throughout the project.

---

## Example

```csharp
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;

    private int _currentHealth;

    public int CurrentHealth => _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
        {
            return;
        }

        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _currentHealth = 0;

        Debug.Log("Player died.");
    }
}
```

---

## Quick Rules

```text
Class                  PascalCase
Method                 PascalCase
Variable               camelCase
Private field          _camelCase
File name              Same as class name
Inspector variable     [SerializeField] private
Indentation             4 spaces
Braces                  Always use them
Physics                 Use 2D versions
Scripts                 Keep them focused
Git commits             Short and descriptive
```

---

## Main Rule

> Write code so another team member can quickly understand what it does.
