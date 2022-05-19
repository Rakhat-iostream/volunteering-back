using System.ComponentModel.DataAnnotations;

namespace Volunteer.Common.Models.Domain
{
    public enum VolunteeringCategories
    {
        [Display(Name = "-")]
        None,

        [Display(Name = "Волонтерство в медицине")]
        Medicine = 1,

        [Display(Name = "Экологическое волонтерство")]
        Ecology = 2,

        [Display(Name = "Социальное волонтерство")]
        Social = 3,

        [Display(Name = "Медиа-волонтерство")]
        Media = 4,

        [Display(Name = "Событийное волонтерство")]
        Event = 5,

        [Display(Name = "Помощь животным")]
        Animal = 6,

        [Display(Name = "ЧС волонтерство")]
        Emergency = 7,

        [Display(Name = "Культурное волонтерство")]
        Culture = 8,

        [Display(Name = "Донорство")]
        Donation = 9,

        [Display(Name = "Pro bono волонтерство")]
        Pro_bono = 10,

        [Display(Name = "Корпоративное")]
        Corporate = 11,

        [Display(Name = "Онлайн волонтерство")]
        Online = 12,

        [Display(Name = "Этно-волонтерство")]
        Ethno = 13,

        [Display(Name = "Спортивное волонтерство")]
        Sport = 14
    }
}
