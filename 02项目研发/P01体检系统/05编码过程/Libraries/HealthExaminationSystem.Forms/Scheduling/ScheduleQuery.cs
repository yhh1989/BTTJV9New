using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using gregn6Lib;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Scheduling;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Scheduling;
using Sw.Hospital.HealthExaminationSystem.Application.Scheduling.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.Scheduling
{
    public partial class ScheduleQuery : UserBaseForm
    {
        /// <summary>
        /// 公共应用服务
        /// </summary>
        private readonly ICommonAppService _commonAppService;

        /// <summary>
        /// 排期应用服务
        /// </summary>
        private readonly ISchedulingAppService _schedulingAppService;

        /// <summary>
        /// 报表名称
        /// </summary>
        private const string GRF_NAME = "每日排期.grf";

        public ScheduleQuery()
        {
            InitializeComponent();

            _schedulingAppService = new SchedulingAppService();
            _commonAppService = new CommonAppService();
        }

        private void LoadData()
        {
            AutoLoading(() =>
            {
                var input = new SearchSchedulingStartEndDto
                { Start = dateEditStart.DateTime, End = dateEditEnd.DateTime };
                var data = _schedulingAppService.GetSchedulingByStartEnd(input);
                var dates = data.Select(r => r.ScheduleDate.Date).Distinct().OrderBy(r => r.Date).ToList();
                var i = 0;
                while (input.Start.Date.AddDays(i) <= input.End.Date)
                {
                    if (!dates.Contains(input.Start.Date.AddDays(i)))
                    {
                        var schedulingEmpty = new SchedulingNewDto
                        {
                            ScheduleDate = input.Start.Date.AddDays(i),
                            IsTeam = false,
                            PersonalName = "无",
                            ItemGroups = new List<ItemGroupOfScheduleDto>()
                        };
                        data.Add(schedulingEmpty);
                    }

                    i = i + 1;
                }

                gridControlScheduling.DataSource = data;
            });
        }

        private void ScheduleQuery_Load(object sender, EventArgs e)
        {
            var date = _commonAppService.GetDateTimeNow().Now.Date;
            dateEditStart.DateTime = date;
            dateEditEnd.DateTime = date.AddDays(7);
            simpleButtonQuery.PerformClick();
        }

        private void simpleButtonQuery_Click(object sender, EventArgs e)
        {
            LoadData();
            if (radioGroupType.EditValue.Equals(2))
            {
                SetGridControlItemGroup(dateEditStart.DateTime, dateEditEnd.DateTime);
            }
        }

        private void gridViewScheduling_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (gridViewScheduling.GetRow(e.FocusedRowHandle) is SchedulingNewDto scheduling)
            {
                if (radioGroupType.EditValue.Equals(1))
                {
                    SetGridControlItemGroup(scheduling.ScheduleDate);
                }
                else if (radioGroupType.EditValue.Equals(3))
                {
                    foreach (var itemGroup in scheduling.ItemGroups)
                        itemGroup.NumberOfPeople = scheduling.TotalNumber;

                    gridControlItemGroup.DataSource = scheduling.ItemGroups;
                }
            }
        }

        private void SetGridControlItemGroup(DateTime date)
        {
            AutoLoading(() =>
            {
                var data = _schedulingAppService.GetSchedulingByDate(new SearchSchedulingNewDto { Date = date });
                foreach (var dto in data)
                {
                    foreach (var itemGroup in dto.ItemGroups)
                        itemGroup.NumberOfPeople = dto.TotalNumber;
                }

                var result = data.SelectMany(r => r.ItemGroups).GroupBy(r => r.Id).Select(r =>
                {
                    var ig = r.FirstOrDefault();
                    return new ItemGroupOfScheduleDto
                    {
                        Id = r.Key,
                        ItemGroupName = ig?.ItemGroupName,
                        ChartName = ig?.ChartName,
                        HelpChar = ig?.HelpChar,
                        NumberOfPeople = r.Sum(g => g.NumberOfPeople)
                    };
                }).ToList();
                gridControlItemGroup.DataSource = result;
            });
        }

        private void SetGridControlItemGroup(DateTime start, DateTime end)
        {
            AutoLoading(() =>
            {
                var data = _schedulingAppService.GetSchedulingByStartEnd(new SearchSchedulingStartEndDto
                { Start = start, End = end });
                foreach (var dto in data)
                {
                    foreach (var itemGroup in dto.ItemGroups)
                        itemGroup.NumberOfPeople = dto.TotalNumber;
                }

                var result = data.SelectMany(r => r.ItemGroups).GroupBy(r => r.Id).Select(r =>
                {
                    var ig = r.FirstOrDefault();
                    return new ItemGroupOfScheduleDto
                    {
                        Id = r.Key,
                        ItemGroupName = ig?.ItemGroupName,
                        ChartName = ig?.ChartName,
                        HelpChar = ig?.HelpChar,
                        NumberOfPeople = r.Sum(g => g.NumberOfPeople)
                    };
                }).ToList();
                gridControlItemGroup.DataSource = result;
            });
        }

        private void radioGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroupType.EditValue.Equals(2))
            {
                SetGridControlItemGroup(dateEditStart.DateTime, dateEditEnd.DateTime);
            }
            else
            {
                if (gridViewScheduling.GetFocusedRow() is SchedulingNewDto scheduling)
                {
                    if (radioGroupType.EditValue.Equals(1))
                    {
                        SetGridControlItemGroup(scheduling.ScheduleDate);
                    }
                    else if (radioGroupType.EditValue.Equals(3))
                    {
                        foreach (var itemGroup in scheduling.ItemGroups)
                            itemGroup.NumberOfPeople = scheduling.TotalNumber;

                        gridControlItemGroup.DataSource = scheduling.ItemGroups;
                    }
                }
            }
        }

        private void gridViewScheduling_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            if (e.Info is GridGroupRowInfo groupRow)
            {
                if (groupRow.EditValue is DateTime date)
                {
                    if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 192, 192);
                    }
                }
            }
        }

        private void simpleButtonPrint_Click(object sender, EventArgs e)
        {
            if (gridViewScheduling.GetFocusedRow() is SchedulingNewDto row)
            {
                var data = _schedulingAppService.GetSchedulingByDate(new SearchSchedulingNewDto { Date = row.ScheduleDate });
                var reportFile = GridppHelper.GetTemplate(GRF_NAME);
                var reportJson = new ReportJson
                {
                    Master = new List<Master> { new Master
                    {
                        ParameterScheduleDate = row.ScheduleDate,
                        ParameterSum = data.Sum(r=>r.TotalNumber)
                    } }
                };
                var reportJsonAm = new ReportJson { Detail = new List<Detail>() };
                var reportJsonPm = new ReportJson { Detail = new List<Detail>() };
                foreach (var dto in data)
                {
                    if (dto.TimeFrame == "下午")
                    {
                        reportJsonPm.Detail.Add(new Detail
                        {
                            FieldName = dto.Name + $"{(dto.IsTeam ? string.Empty : "（个人）")}",
                            FieldIntroducer = dto.Introducer,
                            FieldTotalNumber = dto.TotalNumber.ToString()
                        });
                    }
                    else
                    {
                        reportJsonAm.Detail.Add(new Detail
                        {
                            FieldName = dto.Name + $"{(dto.IsTeam ? string.Empty : "（个人）")}",
                            FieldIntroducer = dto.Introducer,
                            FieldTotalNumber = dto.TotalNumber.ToString()
                        });
                    }
                }

                var report = new GridppReport();
                report.LoadFromURL(reportFile);
                var reportJsonString = JsonConvert.SerializeObject(reportJson);
                report.LoadDataFromXML(reportJsonString);
                var reportAm = report.ControlByName("SubReportAm").AsSubReport.Report;
                var reportJsonAmString = JsonConvert.SerializeObject(reportJsonAm);
                reportAm.LoadDataFromXML(reportJsonAmString);
                var reportJsonPmString = JsonConvert.SerializeObject(reportJsonPm);
                var reportPm = report.ControlByName("SubReportPm").AsSubReport.Report;
                reportPm.LoadDataFromXML(reportJsonPmString);
                report.Title = $"排期打印 - {row.ScheduleDate:F}";
                report.PrintPreview(true);
            }
            else
            {
                ShowMessageBoxWarning("请先选择一条数据！");
            }
        }
    }

    public class ReportJson
    {
        public List<Master> Master { get; set; }

        public List<Detail> Detail { get; set; }
    }

    public class Master
    {
        public DateTime ParameterScheduleDate { get; set; }

        public int ParameterSum { get; set; }
    }

    public class Detail
    {
        public string FieldName { get; set; }

        public string FieldTotalNumber { get; set; }

        public string FieldIntroducer { get; set; }
    }
}