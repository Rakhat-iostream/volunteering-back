using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models;
using Volunteer.Common.Models.DTOs.Volunteers;
using Volunteer.Common.Services;

namespace Volunteer.BL.Services
{
    public class ExcelGenerator : IExcelGenerator
    {
        public byte[] GenerateReport(PageResponse<VolunteerProfileDto> models)
        {
            using (MemoryStream ms = new MemoryStream())
            using (var workbook = new XLWorkbook())
            {
                var sheet = workbook.Worksheets
                    .Add("Attended Volunteers");
                const int stringHeight = 14;
                sheet.RowHeight = stringHeight;
                sheet.Style.Font.FontName = "Times New Roman";
                sheet.Column("A").Width = 22;
                sheet.Column("B").Width = 29;
                sheet.Column("C").Width = 19;
                sheet.Column("D").Width = 23;
                sheet.Column("E").Width = 19;
                sheet.Column("F").Width = 21;
                sheet.Column("G").Width = 18;
                sheet.Column("H").Width = 20;
                sheet.Row(3).Height = 35;

                sheet.Cells("A3").Value = "Id";
                sheet.Cells("B3").Value = "Дата рождения";
                sheet.Cells("C3").Value = "Пол";
                sheet.Cells("D3").Value = "Область";
                sheet.Cells("E3").Value = "Категория волонтерства";
                sheet.Cells("F3").Value = "Опыт";
                sheet.Cells("G3").Value = "О Себе";
                sheet.Cells("H3").Value = "Организация";
                sheet.Range("C2:F2").Merge();
                sheet.Cells("C2:F2").Value = "Отчёт по волонтерам";
                sheet.Cells("C2:F2").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                sheet.Cells("A3:H3").Style.Font.SetBold();

                sheet.Range("A3:H3").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                sheet.Range("A3:H3").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                sheet.Range("A3:H3").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                sheet.Range("A3:H3").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                sheet.Range("A3:H3").Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                sheet.RowHeight = 36;
                var row = 4;
                foreach (var report in models.Result)
                {
                    var sex = "";
                    switch (report.Sex)
                    {
                        case true:
                            sex = "Мужской";
                            break;
                        case false:
                            sex = "Женский";
                            break;
                    }
                    var region = " ";
                    switch (report.Region)
                    {
                        case Common.Models.Domain.Enum.Region.Nur_Sultan:
                            region = "Нур-Султан";
                            break;
                        case Common.Models.Domain.Enum.Region.Almaty:
                            region = "Алматы";
                            break;
                        case Common.Models.Domain.Enum.Region.Shymkent:
                            region = "Шымкент";
                            break;
                        case Common.Models.Domain.Enum.Region.Pavlodar:
                            region = "Павлодарская область";
                            break;
                        case Common.Models.Domain.Enum.Region.Almatinskaya:
                            region = "Алматинская область";
                            break;
                        case Common.Models.Domain.Enum.Region.Aktobe:
                            region = "Актюбинская область";
                            break;
                        case Common.Models.Domain.Enum.Region.Kostanay:
                            region = "Костанайская область";
                            break;
                        case Common.Models.Domain.Enum.Region.Kyzylorda:
                            region = "Кызылординская область";
                            break;
                        case Common.Models.Domain.Enum.Region.Atyrau:
                            region = "Атырауская область";
                            break;
                        case Common.Models.Domain.Enum.Region.Zapad:
                            region = "Западно-Казахстанская область";
                            break;
                        case Common.Models.Domain.Enum.Region.Akmola:
                            region = "Акмолинская область";
                            break;
                        case Common.Models.Domain.Enum.Region.Karaganda:
                            region = "Карагандинская область";
                            break;
                        case Common.Models.Domain.Enum.Region.Sever:
                            region = "Северо-Казахстанская область";
                            break;
                        case Common.Models.Domain.Enum.Region.Mangystau:
                            region = "Мангистауская область";
                            break;
                        case Common.Models.Domain.Enum.Region.Turkestan:
                            region = "Туркестанская область";
                            break;
                        case Common.Models.Domain.Enum.Region.Zhambyl:
                            region = "Жамбылская область";
                            break;
                        case Common.Models.Domain.Enum.Region.Vostok:
                            region = "Восточно-Казахстанская область";
                            break;
                    }
                    List<string> category = new() { };

                    if (report.VolunteeringCategories.Contains(Common.Models.Domain.VolunteeringCategories.Medicine))
                    {
                        category.Add("Волонтерство в медицине");
                    }
                    if (report.VolunteeringCategories.Contains(Common.Models.Domain.VolunteeringCategories.Ecology))
                    {
                        category.Add("Экологическое волонтерство");
                    }
                    if(report.VolunteeringCategories.Contains(Common.Models.Domain.VolunteeringCategories.Social))
                    {
                        category.Add("Социальное волонтерство");
                    }
                    if (report.VolunteeringCategories.Contains(Common.Models.Domain.VolunteeringCategories.Media))
                    {
                        category.Add("Медиа-волонтерство");
                    }
                    if (report.VolunteeringCategories.Contains(Common.Models.Domain.VolunteeringCategories.Event))
                    {
                        category.Add("Событийное волонтерство");
                    }
                    if (report.VolunteeringCategories.Contains(Common.Models.Domain.VolunteeringCategories.Animal))
                    {
                        category.Add("Помощь животным");
                    }
                    if (report.VolunteeringCategories.Contains(Common.Models.Domain.VolunteeringCategories.Emergency))
                    {
                        category.Add("ЧС волонтерство");
                    }
                    if (report.VolunteeringCategories.Contains(Common.Models.Domain.VolunteeringCategories.Culture))
                    {
                        category.Add("Культурное волонтерство");
                    }
                    if (report.VolunteeringCategories.Contains(Common.Models.Domain.VolunteeringCategories.Donation))
                    {
                        category.Add("Донорство");
                    }
                    if (report.VolunteeringCategories.Contains(Common.Models.Domain.VolunteeringCategories.Pro_bono))
                    {
                        category.Add("Pro bono волонтерство");
                    }
                    if (report.VolunteeringCategories.Contains(Common.Models.Domain.VolunteeringCategories.Corporate))
                    {
                        category.Add("Корпоративное");
                    }
                    if (report.VolunteeringCategories.Contains(Common.Models.Domain.VolunteeringCategories.Online))
                    {
                        category.Add("Онлайн волонтерство");
                    }
                    if (report.VolunteeringCategories.Contains(Common.Models.Domain.VolunteeringCategories.Ethno))
                    {
                        category.Add("Этно-волонтерство");
                    }
                    if (report.VolunteeringCategories.Contains(Common.Models.Domain.VolunteeringCategories.Sport))
                    {
                        category.Add("Спортивное волонтерство");
                    }
                

                    sheet.Row(row).Height = 35;

                    sheet.Range($"A{row}:H{row}").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    sheet.Range($"A{row}:H{row}").Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    sheet.Range($"A{row}:H{row}").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    sheet.Range($"A{row}:H{row}").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    sheet.Range($"A{row}:H{row}").Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    sheet.Cells($"A{row}").Value = report.VolunteerId;
                    sheet.Cells($"A{row}").DataType = XLDataType.Text;

                    sheet.Cells($"B{row}").Value = report.BirthDate.Date;
                    sheet.Cells($"B{row}").DataType = XLDataType.DateTime;

                    sheet.Cells($"C{row}").Value = sex;
                    sheet.Cells($"C{row}").DataType = XLDataType.Text;

                    sheet.Cells($"D{row}").Value = region;
                    sheet.Cells($"D{row}").DataType = XLDataType.Text;

                    sheet.Cells($"E{row}").Value = category.AsQueryable();
                    sheet.Cells($"E{row}").DataType = XLDataType.Text;

                    sheet.Cells($"F{row}").Value = report.Experience;
                    sheet.Cells($"F{row}").DataType = XLDataType.Text;

                    sheet.Cells($"G{row}").Value = report.Description;
                    sheet.Cells($"G{row}").DataType = XLDataType.Text;

                    sheet.Cells($"H{row}").Value = report.MembershipId;
                    sheet.Cells($"H{row}").DataType = XLDataType.Text;

                    row++;
                }

                workbook.SaveAs(ms);
                var byteArray = ms.ToArray();
                return byteArray;
            }
        }
    }
}
