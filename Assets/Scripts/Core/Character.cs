using UnityEngine;

// OOP - ABSTRACTION & INHERITANCE:
// Character is the abstract base class for all game characters (Player and Enemy).
// It defines shared data (speed) and a virtual Move() method that subclasses override.
// This promotes code reuse and enforces a common interface across character types.
public class Character : MonoBehaviour
{
    public float speed = 3f;

    // OOP - POLYMORPHISM:
    // Move() is declared virtual so each subclass can provide its own movement behaviour.
    // Player moves via keyboard input; Enemy moves toward the player or a coin.
    public virtual void Move() {}
}
