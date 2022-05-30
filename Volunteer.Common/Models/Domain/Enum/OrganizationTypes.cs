using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer.Common.Models.Domain.Enum
{
    public enum OrganizationTypes
    {
        [Display(Name = "-")]
        None,

        [Display(Name = "Ассоциация")]
        Association = 1,

        [Display(Name = "Гос. учреждение")]
        Gov = 2,

        [Display(Name = "Бизнес")]
        Business = 3,

        [Display(Name = "ВУЗ")]
        University = 4,

        [Display(Name = "Мед. организация")]
        Medical = 5,

        [Display(Name = "Молодеж. центр")]
        Young = 6,

        [Display(Name = "НКО")]
        NGOs = 7,

        [Display(Name = "Объединение граждан")]
        Citizens = 8,

        [Display(Name = "СМИ")]
        Media = 9,

        [Display(Name = "Учреждение соц. защиты")]
        Social = 10,

        [Display(Name = "Другое")]
        Other = 11
    }
}
