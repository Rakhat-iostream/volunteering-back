using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer.Common.Models.Domain.Enum
{
    public enum Region
    {
        [Display(Name = "-")]
        None,

        [Display(Name = "Нур-Султан")]
        Nur_Sultan = 1,

        [Display(Name = "Алматы")]
        Almaty = 2,

        [Display(Name = "Шымкент")]
        Shymkent = 3,

        [Display(Name = "Павлодарская область")]
        Pavlodar = 4,

        [Display(Name = "Алматинская область")]
        Almatinskaya = 5,

        [Display(Name = "Актюбинская область")]
        Aktobe = 6,

        [Display(Name = "Костанайская область")]
        Kostanay = 7,

        [Display(Name = "Кызылординская область")]
        Kyzylorda = 8,

        [Display(Name = "Атырауская область")]
        Atyrau = 9,

        [Display(Name = "Западно-Казахстанская область")]
        Zapad = 10,

        [Display(Name = "Акмолинская область")]
        Akmola = 11,

        [Display(Name = "Карагандинская область")]
        Karaganda = 12,

        [Display(Name = "Северо-Казахстанская область")]
        Sever = 13,

        [Display(Name = "Мангистауская область")]
        Mangystau = 14,

        [Display(Name = "Туркестанская область")]
        Turkestan = 15,

        [Display(Name = "Жамбылская область")]
        Zhambyl = 16,

        [Display(Name = "Восточно-Казахстанская область")]
        Vostok = 17,

    }
}
