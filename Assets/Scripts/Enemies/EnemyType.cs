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
			case EnemyType.HammerheadShark:
                return typeof(HammerheadShark);
            case EnemyType.WorkerEnemy:
                return typeof(WorkerEnemy);
            case EnemyType.HarpoonWorker:
                return typeof(HarpoonWorker);
			case EnemyType.Captain:
                return typeof(Captain);
            default:
                return null;
        }
    } 
}
public enum EnemyType
{
    MeleeEnemy,
    SlideEnemy,
    HammerheadShark,
    WorkerEnemy,
    HarpoonWorker,
	Captain
}
