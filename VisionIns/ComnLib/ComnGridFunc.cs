using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using System.IO;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

class ComnGridFunc
{
    public static void SetInitGridRowColor(GridView view)
    {
        //view.Appearance.Empty.BackColor = Color.White;
        //view.Appearance.Preview.BackColor = Color.White;
        //view.Appearance.Preview.ForeColor = Color.Black;
        //view.Appearance.Row.BackColor = Color.White;
        //view.Appearance.Row.ForeColor = Color.Black;
        //view.Appearance.FocusedRow.BackColor = SystemColors.ActiveCaption;
        //view.Appearance.FocusedRow.ForeColor = Color.Black;
        //view.Appearance.FocusedCell.BackColor = SystemColors.ActiveCaption;
        //view.Appearance.FocusedCell.ForeColor = Color.Black;
        view.OptionsFind.AllowFindPanel = false;
        view.IndicatorWidth = 40;

    }

    public static DataTable DeleteAllGridViewRows(GridControl grid)
    {
        DataTable dt = (DataTable)grid.DataSource;
        dt.Rows.Clear();

        return dt;
    }

    #region[GridView StripePattern 적용]
    public static void SettingGridViewRowPatternToStripe(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
    {
        if (e.RowHandle % 2 == 0)
        {
            e.Appearance.BackColor = Color.FromArgb(239, 240, 242);
        }
    }
    #endregion[GridView StripePattern 적용]

    #region[GridView Indigator Number 적용]
    public static void SettingGridViewRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
    {
        /*
            해당 이벤트 적용 시 GridView.IndicatorWidth = 40으로 따로 적용해여야함
            현 메소드 적용 안됨
         */
        GridView L_View = sender as GridView;

        if (e.RowHandle < 0)
            e.Info.DisplayText = L_View.RowCount.ToString();
        else
            e.Info.DisplayText = (e.RowHandle + 1).ToString();

        e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

        int indiWidth = TextRenderer.MeasureText(L_View.RowCount.ToString(), null).Width;
        L_View.IndicatorWidth = 40;
    }

    #endregion[GridView Indigator Number 적용]

    #region [GridView 기본세팅 한번에 하기]

    public static void GridStyleBasicSetting(GridView view)
    {
        view.CustomDrawRowIndicator += (sender, e) =>
        {
            if (view.IsGroupRow(e.RowHandle))
            {
                e.Info.ImageIndex = -1;
            }
            else
            {
                if (e.RowHandle < 0)
                    e.Info.DisplayText = view.RowCount.ToString();
                else
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();

                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                int indiWidth = TextRenderer.MeasureText(view.RowCount.ToString(), null).Width;
            }
        };

        view.RowStyle += (sender, e) =>
        {
            if (e.RowHandle % 2 == 0)
            {
                e.Appearance.BackColor = Color.FromArgb(239, 240, 242);
            }
        };

        view.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        view.OptionsNavigation.EnterMoveNextColumn = true;
        view.OptionsView.ColumnAutoWidth = false;
        view.OptionsView.ShowGroupPanel = false;
        view.IndicatorWidth = 50;
        view.ColumnPanelRowHeight = 30;
    }

    public static void GridStyleBasicSetting_NonRowStyle(GridView view)
    {
        view.CustomDrawRowIndicator += (sender, e) =>
        {
            if (view.IsGroupRow(e.RowHandle))
            {
                e.Info.ImageIndex = -1;
            }
            else
            {
                if (e.RowHandle < 0)
                    e.Info.DisplayText = view.RowCount.ToString();
                else
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();

                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                int indiWidth = TextRenderer.MeasureText(view.RowCount.ToString(), null).Width;
            }
        };

        view.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        view.OptionsNavigation.EnterMoveNextColumn = true;
        view.OptionsView.ColumnAutoWidth = false;
        view.OptionsView.ShowGroupPanel = false;
        view.IndicatorWidth = 50;
        view.ColumnPanelRowHeight = 30;
    }

    public static void GridStyleBasicSetting(DevExpress.XtraGrid.Views.BandedGrid.BandedGridView view)
    {
        view.CustomDrawRowIndicator += (sender, e) =>
        {
            if (view.IsGroupRow(e.RowHandle))
            {
                e.Info.ImageIndex = -1;
            }
            else
            {
                if (e.RowHandle < 0)
                    e.Info.DisplayText = view.RowCount.ToString();
                else
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();

                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                int indiWidth = TextRenderer.MeasureText(view.RowCount.ToString(), null).Width;
            }
        };

        view.RowStyle += (sender, e) =>
        {
            if (e.RowHandle % 2 == 0)
            {
                e.Appearance.BackColor = Color.FromArgb(239, 240, 242);
            }
        };

        view.CustomDrawFooter += (sender, e) =>
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Center;
            var rect = e.Bounds;
            rect.X += 10;
            e.DefaultDraw();
            Font f = new Font(e.Appearance.GetFont(), FontStyle.Bold);
            e.Cache.DrawString("합계 : ", f, e.Appearance.GetForeBrush(e.Cache), rect, stringFormat);
            e.Handled = true;
        };

        for(int i = 0; i < view.Bands.Count; i++)
        {
            view.Bands[i].AppearanceHeader.Options.UseTextOptions = true;
            view.Bands[i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        view.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        view.OptionsNavigation.EnterMoveNextColumn = true;
        view.OptionsView.ColumnAutoWidth = false;
        view.OptionsView.ShowGroupPanel = false;        
        view.IndicatorWidth = 50;
        view.ColumnPanelRowHeight = 30;
    }

    /// <summary>
    /// Group 형 GridView에 적용할 Style
    /// </summary>
    /// <param name="view"></param>
    public static void GridStyleBasicSettingForGroup(GridView view)
    {
        Dictionary<int, Color> groupRowColorsCollection = null;
        Random random = null;

        view.CustomDrawRowIndicator += (sender, e) =>
        {
            if (view.IsGroupRow(e.RowHandle))
            {
                e.Info.ImageIndex = -1;
            }
            else
            {
                if (e.RowHandle < 0)
                    e.Info.DisplayText = view.RowCount.ToString();
                else
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();

                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                int indiWidth = TextRenderer.MeasureText(view.RowCount.ToString(), null).Width;
            }
        };

        view.RowStyle += (sender, e) =>
        {
            if (e.RowHandle < 0) return;
            if (groupRowColorsCollection == null) return;

            int groupRowHandle = view.GetParentRowHandle(e.RowHandle);
            if (groupRowColorsCollection.Keys.Contains(groupRowHandle))
            {
                if (e.RowHandle % 2 == 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(239, 240, 242);
                }
            }
        };

        view.CustomDrawGroupRow += (sender, e) =>
        {
            if (groupRowColorsCollection == null)
            {
                groupRowColorsCollection = new Dictionary<int, Color>();
                random = new Random();
            }

            if (!groupRowColorsCollection.Keys.Contains(e.RowHandle))
            {
                Color color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
                groupRowColorsCollection.Add(e.RowHandle, color);
            }
            // 주석 제거 시, 그룹 row 색상 무작위
            //e.Appearance.BackColor = groupRowColorsCollection[e.RowHandle];
        };

        view.GroupLevelStyle += (sender, e) =>
        {
            int i = view.GroupCount;

            if (i == 1)
            {
                if (e.Level == 0)
                {
                    e.LevelAppearance.BackColor = Color.LightCyan;
                    e.LevelAppearance.ForeColor = Color.Black;
                }
            }
            else if (i == 2)
            {
                if (e.Level == 0)
                {
                    e.LevelAppearance.BackColor = Color.LightSkyBlue;
                    e.LevelAppearance.ForeColor = Color.Black;
                }
                else if (e.Level == 1)
                {
                    e.LevelAppearance.BackColor = Color.LightCyan;
                    e.LevelAppearance.ForeColor = Color.Black;
                }
            }
            else if (i == 3)
            {
                if (e.Level == 0)
                {
                    e.LevelAppearance.BackColor = Color.FromArgb(95, 180, 255);
                    e.LevelAppearance.ForeColor = Color.Black;
                }
                else if (e.Level == 1)
                {
                    e.LevelAppearance.BackColor = Color.LightSkyBlue;
                    e.LevelAppearance.ForeColor = Color.Black;
                }
                else if (e.Level == 2)
                {
                    e.LevelAppearance.BackColor = Color.LightCyan;
                    e.LevelAppearance.ForeColor = Color.Black;
                }
            }
        };

        view.CustomDrawFooter += (sender, e) =>
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Center;
            var rect = e.Bounds;
            rect.X += 10;
            e.DefaultDraw();
            Font f = new Font(e.Appearance.GetFont(), FontStyle.Bold);
            e.Cache.DrawString("합계 : ", f, e.Appearance.GetForeBrush(e.Cache), rect, stringFormat);
            e.Handled = true;
        };

        view.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        view.Appearance.SelectedRow.BackColor = Color.LightYellow;
        view.Appearance.SelectedRow.ForeColor = Color.Black;
        //view.Appearance.FocusedRow.BackColor = Color.LightPink;
        //view.Appearance.FocusedRow.ForeColor = Color.Black;
        view.OptionsNavigation.EnterMoveNextColumn = true;
        view.OptionsView.ColumnAutoWidth = false;
        view.OptionsView.ShowGroupPanel = false;
        view.IndicatorWidth = 50;
        view.ColumnPanelRowHeight = 30;
    }

    public static void GridStyleBasicSetting_Wide(GridView view)
    {
        view.CustomDrawRowIndicator += (sender, e) =>
        {
            if (view.IsGroupRow(e.RowHandle))
            {
                e.Info.ImageIndex = -1;
            }
            else
            {
                if (e.RowHandle < 0)
                    e.Info.DisplayText = view.RowCount.ToString();
                else
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();

                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                int indiWidth = TextRenderer.MeasureText(view.RowCount.ToString(), null).Width;
            }
        };

        view.RowStyle += (sender, e) =>
        {
            if (e.RowHandle % 2 == 0)
            {
                e.Appearance.BackColor = Color.FromArgb(239, 240, 242);
            }
        };

        view.CustomDrawFooter += (sender, e) =>
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Center;
            var rect = e.Bounds;
            rect.X += 10;
            e.DefaultDraw();
            Font f = new Font(e.Appearance.GetFont(), FontStyle.Bold);
            e.Cache.DrawString("합계 : ", f, e.Appearance.GetForeBrush(e.Cache), rect, stringFormat);
            e.Handled = true;
        };

        view.OptionsNavigation.EnterMoveNextColumn = true;
        view.OptionsView.ColumnAutoWidth = false;
        view.OptionsView.ShowGroupPanel = false;
        view.IndicatorWidth = 60;
        view.ColumnPanelRowHeight = 30;
    }

    public static void GridStyleForSelect(GridView view)
    {
        view.CustomDrawRowIndicator += (sender, e) =>
        {
            if (view.IsGroupRow(e.RowHandle))
            {
                e.Info.ImageIndex = -1;
            }
            else
            {
                if (e.RowHandle < 0)
                    e.Info.DisplayText = view.RowCount.ToString();
                else
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();

                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                int indiWidth = TextRenderer.MeasureText(view.RowCount.ToString(), null).Width;
            }
        };

        view.RowStyle += (sender, e) =>
        {
            if (e.RowHandle % 2 == 0)
            {
                e.Appearance.BackColor = Color.FromArgb(239, 240, 242);
            }
        };

        view.OptionsView.ColumnAutoWidth = false;
        view.OptionsView.ShowGroupPanel = false;
        view.IndicatorWidth = 40;
        view.ColumnPanelRowHeight = 30;
    }

    #endregion

    #region [GridView 새 줄 추가, 삭제]


    /// <summary>
    /// GridView에 새 줄을 추가하는 메서드.
    /// 마지막 줄이 아니면 TAB키를 누른 효과를 준다.
    /// </summary>
    /// <param name="gridView1">대상 GridView</param>
    /// <param name="focusColumn">개행 후 포커스가 가야하는 Column</param>
    public static void GridAddLine(GridView gridView1, GridColumn focusColumn)
    {
        try
        {
            if (gridView1.FocusedRowHandle == gridView1.RowCount - 1
                || gridView1.RowCount == 0)
            {
                gridView1.AddNewRow();
                gridView1.UpdateCurrentRow();
                gridView1.FocusedColumn = focusColumn;
            }
            else { SendKeys.Send("{TAB}"); }
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// GridView에 새 줄을 추가하는 메서드.
    /// 마지막 줄이 아니면 TAB키를 누른 효과를 준다.
    /// </summary>
    /// <param name="gridView1">대상 GridView</param>
    /// <param name="seqColumn">순번 Column</param>
    /// <param name="focusColumn">개행 후 포커스가 가야하는 Column</param>
    public static void GridAddLine(GridView gridView1, GridColumn seqColumn, GridColumn focusColumn)
    {
        try
        {
            if (gridView1.FocusedRowHandle == gridView1.RowCount - 1
                || gridView1.RowCount == 0)
            {
                gridView1.FocusedColumn = focusColumn;
                gridView1.AddNewRow();
                gridView1.UpdateCurrentRow();
                for (int i = 0; i < gridView1.RowCount; i++) { gridView1.SetRowCellValue(i, seqColumn, i + 1); }
                gridView1.SetFocusedRowCellValue(seqColumn, gridView1.FocusedRowHandle + 1);
                gridView1.FocusedColumn = focusColumn;

            }
            else { SendKeys.Send("{TAB}"); }
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// GridView에 새 줄을 추가하는 메서드.
    /// 마지막 줄이 아니면 TAB키를 누른 효과를 준다.
    /// 개행 후 주어져야 하는 기본 값을 Dictionary로 받음.
    /// </summary>
    /// <param name="gridView1">대상 GridView</param>
    /// <param name="seqColumn">순번 Column</param>
    /// <param name="focusColumn">개행 후 포커스가 가야하는 Column</param>
    /// <param name="addTextDic">개행 후 기본으로 주어저야 하는 값과 그 컬럼 Dictionary </param>
    public static void GridAddLine(GridView gridView1, GridColumn seqColumn, GridColumn focusColumn, Dictionary<GridColumn, string> addTextDic)
    {
        try
        {
            if (gridView1.FocusedRowHandle == gridView1.RowCount - 1
                || gridView1.RowCount == 0)
            {
                gridView1.AddNewRow();
                gridView1.UpdateCurrentRow();
                for (int i = 0; i < addTextDic.Count; i++)
                {
                    gridView1.SetFocusedRowCellValue(addTextDic.Keys.ElementAt(i), addTextDic.Values.ElementAt(i));
                }
                for (int i = 0; i < gridView1.RowCount; i++) { gridView1.SetRowCellValue(i, seqColumn, i + 1); }
                gridView1.FocusedColumn = focusColumn;
            }
            else { SendKeys.Send("{TAB}"); }
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message);
        }
    }


    /// <summary>
    /// 포커싱된 GridView의 줄을 삭제하는 메서드.
    /// GridControl의 DataSource를 활용한다. (개선가능하다면 개선 필요) 
    /// </summary>
    /// <param name="gridView1">대상 GridView</param>
    /// <param name="gridControl1">대상 GridControl</param>
    /// <param name="focusColumn">Focus되어야 하는 Column</param>
    public static void GridDeleteLine(GridControl gridControl1, GridView gridView1, GridColumn focusColumn)
    {
        try
        {
            DataTable dt = gridControl1.DataSource as DataTable;
            if (dt == null) { XtraMessageBox.Show("삭제할 행이 없습니다."); return; }
            if (dt.Rows.Count == 0) { XtraMessageBox.Show("삭제할 행이 없습니다."); return; }
            if (dt.Rows.Count == 1)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    Type t = dt.Columns[i].DataType;
                    if (t.Equals(typeof(System.Decimal))) { dt.Rows[0][i] = 0; }
                    else { dt.Rows[0][i] = string.Empty; }
                }
                return;
            }
            dt.Rows.RemoveAt(gridView1.FocusedRowHandle);
            gridControl1.DataSource = dt;
            gridView1.UpdateCurrentRow();
            gridView1.FocusedColumn = focusColumn;
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// 포커싱된 GridView의 줄을 삭제하는 메서드.
    /// GridControl의 DataSource를 활용한다. (개선가능하다면 개선 필요) 
    /// </summary>
    /// <param name="gridView1">대상 GridView</param>
    /// <param name="gridControl1">대상 GridControl</param>
    /// <param name="focusColumn">Focus되어야 하는 Column</param>
    /// <param name="seqFieldName">순번이 입력되는 FieldName</param>
    public static void GridDeleteLine(GridControl gridControl1, GridView gridView1, GridColumn focusColumn, string seqFieldName)
    {
        try
        {
            DataTable dt = gridControl1.DataSource as DataTable;
            if (dt == null) { XtraMessageBox.Show("삭제할 행이 없습니다."); return; }
            if (dt.Rows.Count == 0) { XtraMessageBox.Show("삭제할 행이 없습니다."); return; }

            if (dt.Rows.Count == 1)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    Type t = dt.Columns[i].DataType;
                    if (t.Equals(typeof(System.Decimal))) { dt.Rows[0][i] = 0; }
                    else { dt.Rows[0][i] = string.Empty; }
                    dt.Rows[0][seqFieldName] = 1;
                }
                return;
            }
            dt.Rows.RemoveAt(gridView1.FocusedRowHandle);
            for (int i = 0; i < dt.Rows.Count; i++) { dt.Rows[i][seqFieldName] = i + 1; }
            gridControl1.DataSource = dt;
            gridView1.UpdateCurrentRow();
            gridView1.FocusedColumn = focusColumn;
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// 포커싱된 GridView의 줄을 삭제하는 메서드.
    /// GridControl의 DataSource를 활용한다. (개선가능하다면 개선 필요) 
    /// </summary>
    /// <param name="gridView1">대상 GridView</param>
    /// <param name="gridControl1">대상 GridControl</param>
    /// <param name="focusColumn">Focus되어야 하는 Column</param>
    /// <param name="seqFieldName">순번이 입력되는 FieldName</param>
    public static void GridDelLine(GridControl gridControl1, GridView gridView1, GridColumn focusColumn, string seqFieldName)
    {
        try
        {
            DataTable dt = gridControl1.DataSource as DataTable;
            if (dt == null) { XtraMessageBox.Show("삭제할 행이 없습니다."); return; }
            if (dt.Rows.Count == 0 || dt.Rows.Count == 1) { return; }
            dt.Rows.RemoveAt(gridView1.FocusedRowHandle);
            for (int i = 0; i < dt.Rows.Count; i++) { dt.Rows[i][seqFieldName] = i + 1; }
            gridControl1.DataSource = dt;
            gridView1.UpdateCurrentRow();
            gridView1.FocusedColumn = focusColumn;
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message);
        }
    }

    #endregion

    #region [GridView 저장 전 빈 값 체크]
    /// <summary>
    /// GridView의 특정 컬럼에 빈 값이 있는지 체크하는 메서드
    /// 빈 값이 있다면, 해당 값의 행번-컬럼Caption명에 대한 입력요청 메시지를 리턴한다.
    /// 빈 값이 없다면, string.Empty를 리턴.
    /// </summary>
    /// <param name="gridView">체크하고자 하는 GridView</param>
    /// <param name="gridColumns">체크하고자 하는 컬럼 배열</param>
    /// <returns></returns>
    public static string GridViewEmptyValueCheck(GridView gridView, GridColumn[] gridColumns)
    {
        // strMessage가 Empty라면, 빈 값이 없습니다.
        string strMessage = string.Empty;
        Dictionary<int, GridColumn> dicValue = ValidateRows(gridView, gridColumns);
        if (dicValue.Count > 0)
        {
            int key = 0;
            GridColumn value = null;
            foreach (var item in dicValue)
            {
                key = item.Key;
                value = item.Value;
            }
            strMessage = ((key + 1) + " 번째 줄 " + value.Caption.ToString() + "의 값을 입력해 주세요.");
        }
        return strMessage;
    }
    /// <summary>
    /// GridViewEmptyValueCheck를 위한 메서드 
    /// </summary>
    /// <param name="view">체크하고자 하는 GridView</param>
    /// <param name="gridColumns">체크하고자 하는 컬럼 배열</param>
    /// <returns></returns>
    public static Dictionary<int, GridColumn> ValidateRows(GridView view, GridColumn[] gridColumns)
    {
        Dictionary<int, GridColumn> dicValue = new Dictionary<int, GridColumn>();

        if (view.RowCount > 0)
        {
            string[] strArr = new string[gridColumns.Length];
            for (int i = 0; i < view.RowCount; i++)
            {
                for (int j = 0; j < gridColumns.Length; j++)
                {
                    strArr[j] = view.GetRowCellValue(i, gridColumns[j])?.ToString();
                    if (string.IsNullOrEmpty(strArr[j]))
                    {
                        dicValue.Add(i, gridColumns[j]);
                        break;
                    }
                }
            }
        }
        return dicValue;
    }

    /// <summary>
    /// Grid의 0값 체크를 위한 메서드
    /// </summary>
    /// <param name="gridView">체크할 gridview</param>
    /// <param name="gridColumns">체크할 gridColumns</param>
    /// <returns></returns>
    public static string GridViewZeroValueCheck(GridView gridView, GridColumn[] gridColumns)
    {
        // strMessage가 Empty라면, 빈 값이 없습니다.
        string strMessage = string.Empty;
        Dictionary<int, GridColumn> dicValue = ValidateRows_Zero(gridView, gridColumns);
        if (dicValue.Count > 0)
        {
            int key = 0;
            GridColumn value = null;
            foreach (var item in dicValue)
            {
                key = item.Key;
                value = item.Value;
            }
            strMessage = ((key + 1) + " 번째 줄 " + value.Caption.ToString() + "의 값을 입력해 주세요.");
        }
        return strMessage;
    }
    public static Dictionary<int, GridColumn> ValidateRows_Zero(GridView view, GridColumn[] gridColumns)
    {
        Dictionary<int, GridColumn> dicValue = new Dictionary<int, GridColumn>();
        double dCheck = 0;
        if (view.RowCount > 0)
        {
            string[] strArr = new string[gridColumns.Length];
            for (int i = 0; i < view.RowCount; i++)
            {
                for (int j = 0; j < gridColumns.Length; j++)
                {
                    strArr[j] = view.GetRowCellValue(i, gridColumns[j])?.ToString();
                    dCheck = double.TryParse(strArr[j], out dCheck) ? Convert.ToDouble(strArr[j]) : 0;
                    if (dCheck == 0)
                    {
                        dicValue.Add(i, gridColumns[j]);
                        break;
                    }
                }
            }
        }
        return dicValue;
    }
    #endregion

    #region [GridView 한글 입력 모드 지정(특정 컬럼)]
    /// <summary>
    /// 선택한 컬럼에 진입할 때 한글입력 모드로 전환하는 메서드.
    /// GridView.ShownEditor() 이벤트에 연결 시켜야 한다.
    /// </summary>
    /// <param name="view">대상이 될 GridView 객체</param>
    /// <param name="gridColumns">한글 지정할 컬림의 배열</param>
    public static void GridViewInputHangul(GridView view, GridColumn[] gridColumns)
    {
        for (int i = 0; i < gridColumns.Length; i++)
        {
            if (view.FocusedColumn == gridColumns[i])
            {
                view.ActiveEditor.ImeMode = ImeMode.Hangul;
            }
        }
    }

    /// <summary>
    /// 선택한 컬럼에 진입할 때 한글입력 모드로 전환하는 메서드.
    /// 파라미터로 주어진 GridView에 ShownEditor 이벤트를 더한다.
    /// </summary>
    /// <param name="view">대상이 될 GridView 객체</param>
    /// <param name="gridColumns">한글 지정할 컬림의 배열</param>
    public static void GridViewInputHangul_ShownEditor(GridView view, GridColumn[] gridColumns)
    {
        view.ShownEditor += (sender, e) =>
        {
            for (int i = 0; i < gridColumns.Length; i++)
            {
                if (view.FocusedColumn == gridColumns[i])
                {
                    view.ActiveEditor.ImeMode = ImeMode.Hangul;
                }
            }
        };
    }



    /// <summary>
    /// 선택한 컬럼에 진입할 때 입력모드를 지정하는 메서드.
    /// 파라미터로 주어진 GridView에 ShownEditor 이벤트를 더한다.
    /// </summary>
    /// <param name="view">대상이 될 GridView 객체</param>
    /// <param name="gridColumns">컬럼 - ImeMode Dicitonary 객체</param>
    public static void GridViewInputSet_ShownEditor(GridView view, Dictionary<GridColumn, ImeMode> columnsIme)
    {
        view.ShownEditor += (sender, e) =>
        {
            foreach (var item in columnsIme)
            {
                if (view.FocusedColumn == item.Key)
                {
                    view.ActiveEditor.ImeMode = item.Value;
                }
            }
        };
    }

    #endregion

    #region [Grid에서 FTP 업로드 - 다운로드]


    /// <summary>
    /// Grid에서 FTP 업로드할 파일을 열니다. 
    /// 1. 변수를 (Key : gridView 이름 - Value : 행번호)구성의 지정된 ComnMethod의 Dictionary 객체로 저장합니다.
    /// 2. 지정한 GridColumn에 파일목록을 표시합니다.
    /// </summary>
    /// <param name="view">대상이 되는 GridView</param>
    /// <param name="column_FileName">파일 목록 정보가 담길 GridColumn</param>
    public static void OpenFile_Grid(GridView view, GridColumn column_FileName)
    {
        //using (XtraOpenFileDialog openFileDialog = new XtraOpenFileDialog())
        //{
        //    openFileDialog.Title = "저장할 파일을 선택해주세요.";
        //    openFileDialog.InitialDirectory = "c:\\";
        //    openFileDialog.FilterIndex = 1;
        //    openFileDialog.RestoreDirectory = true;
        //    openFileDialog.Multiselect = true;

        //    if (openFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        //Get the path of specified file
        //        string[] filePath = null;
        //        filePath = new string[openFileDialog.FileNames.Length];
        //        filePath = openFileDialog.FileNames; // 경로를 포함한 파일이름

        //        string sFileNames = string.Empty;
        //        for (int i = 0; i < filePath.Length; i++)
        //        {
        //            sFileNames += Path.GetFileName(filePath[i]); // 경로를 배제한 파일이름들
        //            if (i == filePath.Length - 1)
        //                continue;
        //            sFileNames += ",";
        //        }
        //        string sKey = view.Name + view.FocusedRowHandle.ToString();
        //        ComnField.pathDic.Remove(sKey);
        //        ComnField.pathDic.Add(sKey, filePath);
        //        view.SetFocusedRowCellValue(column_FileName, sFileNames);
        //    }
        //}
    }

    /// <summary>
    /// Grid에서 지정한 파일을 FTP로 부터 다운 받습니다.
    /// </summary>
    /// <param name="view">대상이 되는 GridView</param>
    /// <param name="column_FileName">파일목록이 있는 GridColumn (쉼표로 구분) </param>
    /// <param name="dir">다운받을 FTP 경로</param>
    public static void DownFile_Grid(GridView view, GridColumn column_FileName, string dir)
    {
        try
        {
            //string files = view.GetFocusedRowCellValue(column_FileName)?.ToString();
            //if (string.IsNullOrEmpty(files))
            //{
            //    XtraMessageBox.Show("대상이 될 파일이 없습니다.");
            //    return;
            //}
            //string[] fileArr = files.Split(',');
            //ComnMethod.FTP_Download_Files(dir, fileArr);
        }
        catch (Exception ex)
        {
            XtraMessageBox.Show(ex.Message);
            return;
        }
    }

    #endregion

    #region [ 그리드에 Footer 생성 및 설정]
    /// <summary>
    /// 그리드에 Footer 생성 및 기본적인 설정 (Footer 좌측 합계 : 문자열 출력)
    /// </summary>
    /// <param name="gridView">GridView</param>
    /// <param name="gridColumns">GridColumn 배열 (디폴트 null값)</param>
    public static void GridViewFooterSetting(GridView gridView, GridColumn[] gridColumns = null)
    {
        gridView.OptionsView.ShowFooter = true;

        gridView.CustomDrawFooter += (sender, e) =>
        {
            StringFormat @string = new StringFormat();
            @string.Alignment = StringAlignment.Near;
            @string.LineAlignment = StringAlignment.Center;

            var rect = e.Bounds;
            rect.X += 10;

            e.DefaultDraw();
            e.Cache.DrawString("합계 : ", e.Appearance.GetFont(), e.Appearance.GetForeBrush(e.Cache)
                , rect, @string);
            e.Handled = true;
        };

        foreach (GridColumn Column in gridColumns)
        {
            Column.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            if (Column.FieldName.Equals("TRQTY") || Column.FieldName.Equals("BRQTY"))
                Column.SummaryItem.DisplayFormat = "{0:n0}";
            else
                Column.SummaryItem.DisplayFormat = "{0:c0}";
        }
    }
    #endregion

    // 25.03.19 이전 변수
    private static int dragRowHandle = -1;
    // 25.03.19 이후 변수
    private static int draggedRowHandle = GridControl.InvalidRowHandle;
    public static void SetGridRowDragFunction(GridControl control, GridView view)
    {
        /*
        // 25.03.19 이전 로직
        view.MouseDown += (sender, e) =>
        {
            // 드래그 시작
            GridHitInfo hitInfo = view.CalcHitInfo(e.Location);

            if (hitInfo.InRow)
            {
                dragRowHandle = hitInfo.RowHandle;
            }
        };

        view.MouseUp += (sender, e) =>
        {
            if (dragRowHandle >= 0)
            {
                // 드래그 종료
                GridHitInfo hitInfo = view.CalcHitInfo(e.Location);

                if (hitInfo.InRow)
                {
                    int targetRowHandle = hitInfo.RowHandle;
                    if (dragRowHandle == targetRowHandle) { return; }

                    RowLocationDragMove(control, view, dragRowHandle, targetRowHandle);
                }
            }
            dragRowHandle = -1;
        };
        */

        // 25.03.19 이후 로직
        view.MouseDown += (sender, e) =>
        {
            GridHitInfo hitInfo = view.CalcHitInfo(e.Location);

            // 행 클릭 시, 드래그 시작
            if (hitInfo.InRow)
            {
                draggedRowHandle = hitInfo.RowHandle;  // 드래그 시작 행의 RowHandle
            }
        };

        view.MouseUp += (sender, e) =>
        {
            draggedRowHandle = GridControl.InvalidRowHandle; // 드래그 상태 종료
        };

        view.MouseMove += (sender, e) =>
        {
            if (draggedRowHandle == GridControl.InvalidRowHandle)
                return;

            GridHitInfo hitInfo = view.CalcHitInfo(e.Location);

            if (hitInfo.InRow && hitInfo.RowHandle != draggedRowHandle)
            {
                // 드래그 중인 행을 현재 마우스 위치의 행으로 실시간으로 이동
                SwapRows(control, view, draggedRowHandle, hitInfo.RowHandle);
                draggedRowHandle = hitInfo.RowHandle;  // 현재 이동된 행의 RowHandle 업데이트
            }
        };
    }

    /// <summary>
    /// 그리드 드래그 행 위치 이동 메서드
    /// </summary>
    /// <param name="gc">대상 GridControl</param>
    /// <param name="gv">대상 GridView</param>
    /// <param name="startHandle">드래그 시작 위치</param>
    /// <param name="endHandle">드래그 종료 위치</param>
    public static void RowLocationDragMove(GridControl gc, GridView gv, int startHandle, int endHandle)
    {
        gv.UpdateCurrentRow();
        DataTable dt = (DataTable)gc.DataSource;
        DataTable dtClone = dt.Clone();

        DataRow dragDr = gv.GetDataRow(startHandle);
        for (int i = 0; i <= gv.RowCount; i++)
        {
            // 변경위치가 기존위치보다 위쪽
            if (startHandle > endHandle)
            {
                // 변경위치 도달 전 (변화없음)
                if (i < endHandle)
                {
                    DataRow dr = gv.GetDataRow(i);
                    dtClone.ImportRow(dr);
                }
                // 변경위치 도달
                else if (i == endHandle)
                    dtClone.ImportRow(dragDr);
                // 변경위치 이후 ~ 대상 행 기존위치 도달(한 행씩 뒤로 밀림)
                else if (i > endHandle && i <= startHandle)
                {
                    DataRow dr = gv.GetDataRow(i - 1);
                    dtClone.ImportRow(dr);
                }
                // 대상 행 기존위치 이후 (변화없음)
                else if (i > startHandle)
                {
                    DataRow dr = gv.GetDataRow(i);
                    dtClone.ImportRow(dr);
                }
            }
            // 변경위치가 기존위치보다 아래쪽
            else if (startHandle < endHandle)
            {
                // 기존위치 도달 전 (변화없음)
                if (i < startHandle)
                {
                    DataRow dr = gv.GetDataRow(i);
                    dtClone.ImportRow(dr);
                }
                // 기존위치 도달 및 이후 ~ 변경위치 도달 전 (한 행씩 앞으로 밀림)
                else if (i >= startHandle && i < endHandle)
                {
                    DataRow dr = gv.GetDataRow(i + 1);
                    dtClone.ImportRow(dr);
                }
                // 변경위치 도달
                else if (i == endHandle)
                    dtClone.ImportRow(dragDr);
                // 변경위치 이후 (변화없음)
                else if (i > endHandle)
                {
                    DataRow dr = gv.GetDataRow(i);
                    dtClone.ImportRow(dr);
                }
            }
        }
        gc.DataSource = dtClone;
        gv.FocusedRowHandle = endHandle;
    }

    /// <summary>
    /// 그리드 드래그 행 위치 이동 메서드 (실시간 확인)
    /// </summary>
    /// <param name="gc">대상 GridControl</param>
    /// <param name="gv">대상 GridView</param>
    /// <param name="startHandle">드래그 클릭대상 행 현재위치</param>
    /// <param name="endHandle">드래그 마우스 이동위치</param>
    private static void SwapRows(GridControl gc, GridView gv, int sourceRowHandle, int targetRowHandle)
    {
        if (sourceRowHandle == targetRowHandle)
            return;

        // 데이터 소스에서 두 행의 데이터를 교환
        DataTable dt = gc.DataSource as DataTable;
        if (dt != null && sourceRowHandle >= 0 && targetRowHandle >= 0)
        {
            // 1칸 이동이라면, 서로 스왑만 진행
            if (Math.Abs(sourceRowHandle - targetRowHandle) == 1)
            {
                DataRow sourceRow = dt.Rows[sourceRowHandle];
                DataRow targetRow = dt.Rows[targetRowHandle];

                // 임시로 저장
                var tempRow = sourceRow.ItemArray;
                sourceRow.ItemArray = targetRow.ItemArray;
                targetRow.ItemArray = tempRow;

                // 변경된 데이터를 반영
                gv.RefreshData();
            }
            // 마우스 포인터 인풋렉으로 인해, 드래그 행 차이가 2 이상인 경우
            //  - ex. 15행 Row를 클릭 및 드래그하여 처음 닿은 Row가 12행 이면, 차이는 3
            // 위 경우를 서로 스왑 진행하면, 드래그 대상 Row가 아닌 다른 Row들이 전부 섞이는 현상 발생함
            // 따라서, 차이가 나는 묶음만큼, 행 묶음을 대상으로 스왑해야 함
            else
            {
                // 대상 Row가 아래로 이동
                if (sourceRowHandle > targetRowHandle)
                {
                    int rowsCount = sourceRowHandle - targetRowHandle;
                    DataRow sourceRow = dt.Rows[sourceRowHandle];
                    DataRow targetRow = dt.Rows[targetRowHandle];
                    object[] tempRow;
                    for (int i = 0; i < rowsCount; i++)
                    {
                        targetRow = dt.Rows[targetRowHandle + i];

                        tempRow = sourceRow.ItemArray;
                        sourceRow.ItemArray = targetRow.ItemArray;
                        targetRow.ItemArray = tempRow;

                        // 변경된 데이터를 반영
                        gv.RefreshData();
                    }
                }
                // 대상 Row가 위로 이동
                else if (sourceRowHandle < targetRowHandle)
                {
                    int rowsCount = targetRowHandle - sourceRowHandle;
                    DataRow sourceRow = dt.Rows[sourceRowHandle];
                    DataRow targetRow = dt.Rows[targetRowHandle];
                    object[] tempRow;
                    for (int i = 0; i < rowsCount; i++)
                    {
                        targetRow = dt.Rows[targetRowHandle - i];

                        tempRow = sourceRow.ItemArray;
                        sourceRow.ItemArray = targetRow.ItemArray;
                        targetRow.ItemArray = tempRow;

                        // 변경된 데이터를 반영
                        gv.RefreshData();
                    }
                }
            }
        }
    }
}
