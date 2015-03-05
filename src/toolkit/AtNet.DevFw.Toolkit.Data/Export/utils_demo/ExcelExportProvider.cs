﻿/**
 * Copyright (C) 2007-2015 OPSoft INC,All rights reseved.
 * Get more infromation of this software,please visit site http://cms.ops.cc
 * 
 * name : LocaledExcelExportProvider.cs
 * author : newmin (new.min@msn.com)
 * date : 2012/12/01 23:00:00
 * description : 
 * history : 
 */

using System.Collections.Generic;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Ops.Toolkit.DataExport;

namespace Com.Plugin.Weixin.Utils
{
    public sealed class LocaledExcelExportProvider : IDataExportProvider
    {
        public byte[] Export(DataTable dt, IDictionary<string, string> columns)
        {
            HSSFWorkbook wb = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();

            ISheet sheet = wb.CreateSheet("Sheet1");

            //填充表头
            IRow dataRow = sheet.CreateRow(0);

            bool isReColumn = !(columns == null || columns.Count == 0);

            int tmpInt = 0;

            if (isReColumn)
            {
                foreach (string columnName in columns.Keys)
                {
                    dataRow.CreateCell(tmpInt++).SetCellValue(columns[columnName]);
                }
            }
            else
            {
                foreach (DataColumn column in dt.Columns)
                {
                    dataRow.CreateCell(tmpInt++).SetCellValue(column.ColumnName);
                }
            }

            //填充内容
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataRow = sheet.CreateRow(i + 1);


                if (isReColumn)
                {
                    tmpInt = 0;
                    foreach (string columnName in columns.Keys)
                    {
                        dataRow.CreateCell(tmpInt++).SetCellValue(dt.Rows[i][columnName].ToString());
                    }
                }
                else
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        dataRow.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }
                }
            }


            wb.Write(ms);
            //rsp.AddHeader("Content-Disposition", string.Format("attachment; filename=EmptyWorkbook.xls"));
            //rsp.BinaryWrite(ms.ToArray());

            wb = null;
            ms.Close();
            byte[] bytes = ms.ToArray();
            ms.Dispose();

            return bytes;
        }

    }
}
