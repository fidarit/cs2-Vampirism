namespace VampirismCS2.Models
{
    public class PermissionData
    {
        public bool Enabled { get; set; } = true;
        public bool OnHeadShotOnly { get; set; } = true;
        public bool OnKillOnly { get; set; } = true;
        public float Multiplier { get; set; } = 0.5f;

        /// <summary>
        /// if GunGame mode is active, vampirism will be disabled for gungame leader
        /// </summary>
        public bool GG_IgnoreLeader { get; set; } = false;
    }
}
