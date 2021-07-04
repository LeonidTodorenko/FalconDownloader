using FalconDownloader.Contracts;
using FalconDownloader.Models;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalconDownloader.Services
{
    public class ExcelService : IExcelService
    {
        public ExcelService(string pathToSettings)
        {
            _pathToSettings = pathToSettings;
        }

        public Dictionary<string, string> Stages { get; private set; }
        public Dictionary<string, string> Companies { get; private set; }

        public IResult<bool> LoadDictionaries()
        {
            var res = false;

            Stages = new Dictionary<string, string>();
            Companies = new Dictionary<string, string>();

            var info = new FileInfo(_pathToSettings);
            if(!info.Exists)
            {
                return new Result<bool>(false, "Path to Excel file with dictionaries is corrupt. Process will be stopped.");
            }
            try
            {
                using(var book = new XLWorkbook(_pathToSettings))
                {
                    Companies = book.Worksheet(1).Rows()
                        .ToDictionary(r => r.Cell(1).Value.ToString(), r => r.Cell(2).Value.ToString());
                    Stages = book.Worksheet(2).Rows()
                        .ToDictionary(r => r.Cell(1).Value.ToString(), r => r.Cell(2).Value.ToString());
                    res = true;
                }
            }
            catch(Exception ex)
            {
                var sb = new StringBuilder();
                while (ex != null)
                {
                    sb.Append(ex.Message + " ");
                    ex = ex.InnerException;
                }

                return new Result<bool>(false, sb.ToString());
            }

            return new Result<bool>(res);
        }

        private readonly string _pathToSettings;
    }
}
