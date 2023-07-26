namespace KamranWali.CodeOptPro.Managers
{
    public abstract class MonoAdvUpdate : MonoAdv, IUpdate
    {
        public abstract void UpdateObject();
        public abstract void SetActive(bool isActive);
        public abstract bool IsActive();
    }
}