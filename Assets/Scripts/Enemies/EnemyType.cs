using System;
public static class EnemyTypeExtensions
{
    public static Type GetEnemyType(this EnemyType type)
    {
        switch (type)
        {
            case EnemyType.SlideEnemy:
                return typeof(SlideEnemy);

            case EnemyType.MeleeEnemy:
                return typeof(MeleeEnemy);
            default:
                return null;
        }
    } 
}
public enum EnemyType
{
    MeleeEnemy,
    SlideEnemy
}
