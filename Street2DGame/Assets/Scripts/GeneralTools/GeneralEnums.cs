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

    public enum ShadowType
    {
        Soft,
        Hard
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
}