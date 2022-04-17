public class ListSkill
{
    public ISkill Skill { get; set; }
    public float Cooldown { get; set; }

    public ListSkill(ISkill skill)
    {
        Skill = skill;
        Cooldown = skill.Cooldown;
    }
}
