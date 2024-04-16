using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

public class ExcelReader
{
    static char[] TRIM_CHARS = { '\"' };

    //열을 텍스트로
    public static List<Dictionary<string, object>> Read(string filePath)
    {
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            IWorkbook workbook = new XSSFWorkbook(fileStream);
            ISheet sheet = workbook.GetSheetAt(0); // 첫 번째 시트를 가져옴

            IRow headerRow = sheet.GetRow(0);
            List<string> columnNames = new List<string>();
            foreach (ICell cell in headerRow.Cells)
            {
                columnNames.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) continue;

                Dictionary<string, object> entry = new Dictionary<string, object>();
                int columnIndex = 0;
                foreach (ICell cell in row.Cells)
                {
                    if (columnIndex >= columnNames.Count) // 열 이름 수보다 많은 데이터가 있는 경우 건너뜁니다.
                        continue;

                    string columnName = columnNames[columnIndex];
                    string cellValue = GetCellValue(cell);

                    // 값 형식 변환
                    cellValue = cellValue.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                    object finalValue = cellValue;
                    int n;
                    float f;
                    if (int.TryParse(cellValue, out n))
                    {
                        finalValue = n;
                    }
                    else if (float.TryParse(cellValue, out f))
                    {
                        finalValue = f;
                    }

                    entry[columnName] = finalValue;
                    columnIndex++;
                }

                if (entry.Count > 0)
                {
                    list.Add(entry);
                }
            }

            workbook.Close();
        }

        return list;
    }
    //열을 숫자로
    public static List<Dictionary<int, object>> ReadNumericColumns(string filePath)
    {
        List<Dictionary<int, object>> list = new List<Dictionary<int, object>>();

        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            IWorkbook workbook = new XSSFWorkbook(fileStream);
            ISheet sheet = workbook.GetSheetAt(0); // 첫 번째 시트를 가져옴

            IRow headerRow = sheet.GetRow(0);
            int columnCount = headerRow.LastCellNum;

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) continue;

                Dictionary<int, object> entry = new Dictionary<int, object>();
                for (int j = 0; j < columnCount; j++)
                {
                    ICell cell = row.GetCell(j);
                    if (cell == null) continue;

                    string cellValue = GetCellValue(cell);

                    // 값 형식 변환
                    cellValue = cellValue.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                    object finalValue = cellValue;
                    int n;
                    float f;
                    if (int.TryParse(cellValue, out n))
                    {
                        finalValue = n;
                    }
                    else if (float.TryParse(cellValue, out f))
                    {
                        finalValue = f;
                    }

                    entry[j] = finalValue;
                }

                if (entry.Count > 0)
                {
                    list.Add(entry);
                }
            }

            workbook.Close();
        }

        return list;
    }

    private static string GetCellValue(ICell cell)
    {
        if (cell == null) return string.Empty;

        switch (cell.CellType)
        {
            case CellType.Numeric:
                return cell.NumericCellValue.ToString();
            case CellType.String:
                return cell.StringCellValue;
            case CellType.Boolean:
                return cell.BooleanCellValue.ToString();
            case CellType.Formula:
                return cell.CellFormula;
            default:
                return string.Empty;
        }
    }
}