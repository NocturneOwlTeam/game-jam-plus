namespace Nocturne.Enums
{
    public enum FPSMode
    {
        Performance,
        Battery
    }

    public enum AAMode
    {
        FXAA,
        None,
        MSAA
    }

    public enum AnisotropicMode
    {
        ForcedOn,
        PerTexture,
        None
    }

    public enum VSyncMode
    {
        None,
        EveryVCount,
        EverySecondVCount
    }

    public enum PlayerState
    {
        Idle,
        Moving,
        Dashing,
        Aiming,
        Firing
    }
    public enum ItemType
    {
        Healable,
        ManaGenerator,
        Standard
    }
    public enum PlayerAnimatorLayers
    {
        Walk,
        Idle
    }
    public enum PlayerStatus
    {
        Idle,
        Walking,
        Sprinting,
        Crouching
    }

    public enum JumpState
    {
        Jumping,
        DoubleJumping,
        InfiniteJumping,
        Falling,
        None
    }

    public enum TeamIndex
    {
        None = -1,
        Neutral = 0,
        Player,
        Enemy,
        Count
    }
}