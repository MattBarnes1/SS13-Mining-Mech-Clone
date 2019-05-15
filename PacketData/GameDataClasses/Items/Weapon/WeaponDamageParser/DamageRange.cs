public class DamageRange
{
    private string damageType;
    private int minDamage;
    private int maxDamage;

    public DamageRange(string damageType, int minDamage, int maxDamage)
    {
        this.maxDamage = maxDamage;
        this.damageType = damageType;
        this.minDamage = minDamage;
    }
}
