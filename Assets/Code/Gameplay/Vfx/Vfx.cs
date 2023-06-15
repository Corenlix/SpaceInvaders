using Code.Gameplay.Factories.Pools;

namespace Code.Gameplay.Vfx
{
    public abstract class Vfx<T> : MonoPoolBridge<T> where T : MonoPoolBridge<T>
    {
    }
}