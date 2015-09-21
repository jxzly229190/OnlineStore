using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using V5.DataContract.Utility;
using V5.Library;
using V5.Portal.Backstage.Models.Utility;
using V5.Service.Utility;
using V5.Portal.Backstage.Models;

namespace V5.Portal.Backstage.Controllers.Utility
{
    public partial class UtilityController
    {
        public PartialViewResult SystemVisitor()
        {
            return PartialView("SystemVisitor");
        }
        
        public JsonResult SystemVisitorCount(string startTime, string endTime, string condition)
        {
            DateTime StartTime = DateTime.Now;
            DateTime EndTime = DateTime.Now.AddDays(1);
            if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
            {
                StartTime = DateTime.Parse(startTime);
                EndTime = DateTime.Parse(endTime);
            }
            var systemVisitorJson = new SystemVisitorChartJson();
            var vistorCountList = new List<SystemVisitorChartJson>();
            List<System_Visitor> list = null;
            int i = 0;
            systemVisitorJson.color = "red";
            systemVisitorJson.line_width = "1";
            switch (condition)
            {
                ////查询每一天
                case "day":
                    this.systemVisitorService = new SystemVisitorService();
                    list = this.systemVisitorService.Query(StartTime, EndTime, condition);
                    //下标
                    var visitorLineDay = new SystemVisitorLineDay() { labels = new int[24], value = new List<int>(), vpTitle = VpTitle(StartTime.Year, StartTime.Month, StartTime.Day) };
                    while (i < visitorLineDay.labels.Length)
                    {
                        visitorLineDay.labels[i] = i;
                        int i1 = i;
                        var visitorCount = list.Where(r => r.DepartDate == i1).Select(c => c.VisitorCount).FirstOrDefault();
                        visitorLineDay.value.Add(visitorCount);
                        i++;
                    }
                    systemVisitorJson.systemVisitorLineDay = new List<SystemVisitorLineDay> { visitorLineDay };
                    break;

                case "month":
                    this.systemVisitorService = new SystemVisitorService();
                    list = this.systemVisitorService.Query(StartTime, EndTime.AddMonths(1), condition);
                    var visitorLineMonth = new SystemVisitorLineMonth { labels = new int[DateTime.DaysInMonth(StartTime.Year, StartTime.Month)], value = new List<int>(), name = "PV", color = "#0d8ecf", line_width = "2", vpTitle = VpTitle(StartTime.Year, StartTime.Month, 0) };
                    while (i < visitorLineMonth.labels.Length)
                    {
                        visitorLineMonth.labels[i] = i + 1;
                        int i1 = i;
                        var visitorCount = list.Where(r => r.DepartDate == i1).Select(c => c.VisitorCount).FirstOrDefault();
                        visitorLineMonth.value.Add(visitorCount);
                        i++;
                    }
                    systemVisitorJson.systemVisitorLineMonth = new List<SystemVisitorLineMonth>() { visitorLineMonth };
                    break;
                case "season"://每天个季度
                    this.systemVisitorService = new SystemVisitorService();
                    list = this.systemVisitorService.Query(StartTime, StartTime.AddMonths(2), condition);
                    var visitorLineSeason = new SystemVisitorLineSeason { labels = new int[4], value = new List<int>(), vpTitle = VpTitle(StartTime.Year, StartTime.Month, 0) };
                    while (i < visitorLineSeason.labels.Length)
                    {
                        visitorLineSeason.labels[i] = i + 1;
                        int i1 = i;
                        var visitorCount = list.Where(r => r.DepartDate == i1).Select(c => c.VisitorCount).FirstOrDefault();
                        visitorLineSeason.value.Add(visitorCount);
                        i++;
                    }
                    systemVisitorJson.systemVisitorLineSeason = new List<SystemVisitorLineSeason>() { visitorLineSeason };
                    break;
                case "year"://每一年
                    this.systemVisitorService = new SystemVisitorService();
                    list = this.systemVisitorService.Query(StartTime, EndTime.AddYears(1), condition);
                    var visitorLineYear = new SystemVisitorLineYear { labels = new int[DateTime.IsLeapYear(StartTime.Year) ? 13 : 12], value = new List<int>(), vpTitle = VpTitle(StartTime.Year, 0, 0) };

                    while (i < visitorLineYear.labels.Length)
                    {
                        visitorLineYear.labels[i] = i + 1;
                        int i1 = i;
                        var visitorCount = list.Where(r => r.DepartDate == i1).Select(c => c.VisitorCount).FirstOrDefault();
                        visitorLineYear.value.Add(visitorCount);
                        i++;
                    }
                    systemVisitorJson.systemVisitorLineYear = new List<SystemVisitorLineYear>() { visitorLineYear };
                    break;
            }

            systemVisitorJson.line_width = "1";
            vistorCountList.Add(systemVisitorJson);

            return Json(vistorCountList, JsonRequestBehavior.AllowGet);
        }

        //根据时间区间处理纵坐标
        public List<int> LabelsSection(DateTime startTime, DateTime endTime, string condition)
        {
            var ordinate = new List<int>();
            var dateIterator = 0;
            if (condition == "day")
            {
                var dateSection = endTime.Day - startTime.Day;
                if (dateSection <= 3)
                {
                    for (int i = 0; i < 24; i++)
                    {
                        dateIterator = dateIterator + dateSection;
                        ordinate.Add(dateIterator);
                    }
                }
                if (startTime.Day - endTime.Day > 3)
                {
                    int datemonth = DateTime.DaysInMonth(startTime.Year, startTime.Month);
                    for (int i = 0; i < datemonth; i++)
                    {
                        dateIterator += 1;
                        ordinate.Add(dateIterator);
                    }
                }
            }
            if (condition == "month")
            {
                var dateSection = startTime.Month - endTime.Month;
                if (dateSection <= 1)
                {
                    var dateCount = DateTime.DaysInMonth(startTime.Year, startTime.Month);
                    for (int i = 0; i < dateCount; i++)
                    {
                        ordinate.Add(i);
                    }
                }
            }
            if (condition == "year")
            {
                var dateSection = endTime.Year - startTime.Year;
                if (dateSection == 1)
                {
                    var monthCount = DateTime.IsLeapYear(startTime.Year) ? 13 : 12;
                    for (var i = 0; i < monthCount; i++)
                    {
                        ordinate.Add(i);
                    }
                }
                if (dateSection >= 2)
                {
                    const int month = 13;
                    var monthSection = 0;
                    for (var i = 0; i < month; i++)
                    {
                        monthSection += 2;
                        ordinate.Add(monthSection);
                    }
                }
            }
            return ordinate;
        }

        public string VpTitle(int year, int month, int day)
        {
            string vptitle = string.Empty;
            if (year != 0)
            {
                vptitle += year + "年";
            }
            if (month != 0)
            {
                vptitle += month + "月";
            }
            if (day != 0)
            {
                vptitle += day + "日";
            }
            return vptitle + "访问量";
        }

        public string VpTitle(DateTime starTime, DateTime endTime)
        {
            string vpTitle = "购酒网后台";
            if (endTime.Day - starTime.Day > 1)
            {
                vpTitle += starTime.Year + "年" + starTime.Month + "月" + starTime.Day + "-" + endTime.Day + "日";
            }
            if (endTime.Month - starTime.Month > 1)
            {
                vpTitle += starTime.Year + "年" + starTime.Month + "月" + starTime.Day + "-" + endTime.Day + "日";
            }
            if (endTime.Year - starTime.Year == 0)
            {
                if (endTime.Year - starTime.Year == 0)
                {
                    vpTitle += starTime.Year + "年";
                    if (endTime.Month - starTime.Month == 0)
                    {
                        vpTitle += starTime.Month + "月";
                        if (endTime.Day - starTime.Day == 0)
                        {
                            vpTitle += starTime.Day + "日";
                        }
                        else if (endTime.Day - starTime.Day > 1)
                        {
                            vpTitle += starTime.Day + "-" + endTime.Day + "日";
                        }
                    }
                    else if (endTime.Month - starTime.Month > 1)
                    {
                        vpTitle += starTime.Month + "-" + endTime.Month + "月";
                    }
                }
                else if (endTime.Year - starTime.Year > 1)
                {
                    vpTitle += starTime.Year + "-" + endTime.Year + "年";
                }


            }
            vpTitle += "访问量";
            return vpTitle;
        }

        public JsonResult DropListMonth(string year)
        {
            if (string.IsNullOrEmpty(year))
            {
                year = DateTime.Now.Year.ToString();
            }
            var list = new List<DropListMonth>();
            if (DateTime.IsLeapYear(int.Parse(year)))
            {
                for (int i = 0; i < 13; i++)
                {
                    list.Add(new DropListMonth { Name = i + 1 + "月", Id = i + 1 });
                }
            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    list.Add(new DropListMonth { Name = i + 1 + "月", Id = i + 1 });
                }
            }
            return Json(list);
        }

    }
}