namespace KamranWali.CodeOptPro.Bars
{
    public class NormalBar : BaseBar
    {
        public override void RemoveValue(int value)
            => curValue = curValue - value <= 0 ? 0 : curValue - value;
    }
}