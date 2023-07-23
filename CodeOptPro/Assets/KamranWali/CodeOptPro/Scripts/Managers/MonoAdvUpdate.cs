namespace KamranWali.CodeOptPro.Managers
{
    public abstract class MonoAdvUpdate : MonoAdv, IUpdate
    {
        public abstract void UpdateObject();
        public abstract void SetActive(bool isActivate);
        public abstract bool IsActive();
    }
}