using System;
public static class EnemyTypeExtensions
{
    public static Type GetEnemyType(this EnemyType type)
    {
        switch (type)
        {
            case EnemyType.EnemyTest:
                return typeof(EnemyTest);

            default:
                return null;
        }
    } 
}
public enum EnemyType
{
    EnemyTest
}
