namespace Ships.Data
{
    internal sealed class Health : AbstractHealth
    {
        public override float ShieldRecovery { get; }
        public override float MaxHp { get; }
        public override float MaxShield { get; }

        public Health(float hp, float shield, float shieldRecovery, float shieldRecoveryInterval)
        {
            MaxHp = hp;
            ShieldRecovery = shieldRecovery;
            ShieldRecoveryInterval = shieldRecoveryInterval;
            MaxShield = shield;
            RestoreHp();
            RestoreHp();
        }
    }
}