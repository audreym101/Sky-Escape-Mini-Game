// DESIGN PATTERN - STATE:
// IEnemyState is the interface that all enemy states implement.
// OOP - ABSTRACTION: defines a contract (Execute) without exposing implementation details.
// OOP - POLYMORPHISM: each state class provides its own Execute() behaviour.
public interface IEnemyState
{
    void Execute(Enemy enemy);
}
