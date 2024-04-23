using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Sw.Hospital.HealthExaminationSystem.Application.DynamicColumnDirectory.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 动态列应用配置帮助器
    /// </summary>
    public class DynamicColumnConfigurationHelper
    {
        /// <summary>
        /// 加载表格视图动态列配置
        /// </summary>
        /// <param name="gridViewId"></param>
        /// <param name="gridView"></param>
        public static void LoadGridViewDynamicColumnConfiguration(string gridViewId, GridView gridView)
        {
            var data = DefinedCacheHelper.DefinedApiProxy.DynamicColumnAppService.QueryDynamicColumnConfigurationList(
                new DynamicColumnConfigurationDtoNo2 { GridViewId = gridViewId }).Result;

            if (data.Count != 0)
            {
                foreach (GridColumn gridViewColumn in gridView.Columns)
                {
                    gridViewColumn.Visible = false;
                    gridViewColumn.VisibleIndex = -1;
                    gridViewColumn.Fixed = FixedStyle.None;
                }
                foreach (var row in data)
                {
                    var column = gridView.Columns.ColumnByName(row.GridViewColumnName);
                    if (column != null && row.Visible)
                    {
                        column.Visible = true;
                        column.VisibleIndex = row.VisibleIndex + 1;
                        if (row.Fixed)
                        {
                            column.Fixed = row.IsLeft ? FixedStyle.Left : FixedStyle.Right;
                        }
                        else
                        {
                            column.Fixed = FixedStyle.None;
                        }
                    }
                }
            }
        }
    }
}