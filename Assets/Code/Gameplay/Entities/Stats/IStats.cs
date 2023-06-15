namespace Code.Gameplay.Entities.Stats
{
    public interface IStats
    {
        public float Health { get; set; }
        public float MaxHealth { get; set; }
        public float HealthRegenDelay { get; set; }
        public float HealthRegenAmount { get; set; }

        public float Shield { get; set; }
        public float MaxShield { get; set; }
        public float ShieldRegenDelay { get; set; }
        public float ShieldRegenAmount { get; set; }

        public float Armor { get; set; }
    }
}