namespace ThirdPersonController
{
    /// <summary>
    /// Describes a damage receiver.
    /// </summary>
    public interface IDamageReceiver
    {
        /// <summary>
        /// Applies damage to this receiver.
        /// </summary>
        /// <param name="damageInfo">The damage information.</param>
        void Damage(DamageInfo damageInfo);
    }
}