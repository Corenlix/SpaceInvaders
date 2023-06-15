using System;
using Railcar.Time;
using UniRx;

namespace Code.Gameplay.Entities.Stats
{
    public abstract class BaseStatsProcessor : IStatsProcessor, IDisposable
    {
        private readonly IStats _stats;
        private readonly CompositeDisposable _subscriptions = new();

        protected BaseStatsProcessor(ITimeObservable time, IStats stats)
        {
            _stats = stats;
            time.MarkAndRepeat(() => _stats.HealthRegenDelay, _ => OnRegenHealing());
            time.MarkAndRepeat(() => _stats.ShieldRegenDelay, _ => OnRegenShielding());
        }

        public event Action<IStatsProcessor, float> ShieldChanged;
        public event Action<IStatsProcessor, float> HealthChanged;
        public event Action<IStatsProcessor> Died;

        public float Health => _stats.Health;
        public float MaxHealth => _stats.MaxHealth;
        public float HealthRegenDelay => _stats.HealthRegenDelay;
        public float HealthRegenAmount => _stats.HealthRegenAmount;
    
        public float Shield => _stats.Shield;
        public float MaxShield => _stats.MaxShield;
        public float ShieldRegenDelay => _stats.ShieldRegenDelay;
        public float ShieldRegenAmount => _stats.ShieldRegenAmount;
    
        public float Armor => _stats.Armor;

        public void TakeDamage(float incomingDamage)
        {
            var cutDamage = incomingDamage * _stats.Armor;

            if (_stats.Shield != 0)
            {
                if (_stats.Shield <= cutDamage)
                {
                    _stats.Shield = 0;
                    ShieldChanged?.Invoke(this, 0);
                }
                else
                {
                    var newShield = _stats.Shield - cutDamage;
                    _stats.Shield = newShield;
                    ShieldChanged?.Invoke(this, newShield);
                    return;
                }
            }

            if (_stats.Health != 0)
            {
                if (_stats.Health <= cutDamage)
                {
                    Kill();
                }
                else
                {
                    var newHealth = _stats.Health - cutDamage;
                    _stats.Health = newHealth;
                    HealthChanged?.Invoke(this, newHealth);
                }
            }
        }

        public void Kill()
        {
            _stats.Health = 0;
            HealthChanged?.Invoke(this, 0);
            Died?.Invoke(this);
        }

        public void Heal(float amount)
        {
            float cutHeal = amount;
        
            _stats.Health += cutHeal;
            if (_stats.Health >= _stats.MaxHealth)
            {
                cutHeal = _stats.Health - _stats.MaxHealth;
                _stats.Health = _stats.MaxHealth;
                HealthChanged?.Invoke(this, _stats.MaxHealth);
            }
            else
            {
                HealthChanged?.Invoke(this, _stats.Health);
                return;
            }

            _stats.Shield = Math.Min(_stats.Shield + cutHeal, _stats.MaxShield);
            ShieldChanged?.Invoke(this, _stats.Shield);
        }
    
        private void OnRegenHealing()
        {
            _stats.Health = Math.Min(_stats.Health + _stats.HealthRegenAmount, _stats.MaxHealth);
            HealthChanged?.Invoke(this, _stats.Health);
        }

        private void OnRegenShielding()
        {
            _stats.Shield = Math.Min(_stats.Shield + _stats.ShieldRegenAmount, _stats.MaxShield);
            ShieldChanged?.Invoke(this, _stats.Shield);
        }

        public virtual void Dispose()
        {
            _subscriptions?.Dispose();
        }
    }
}