using System.Collections.Generic;

namespace FalconDownloader.Contracts
{
    public interface IExcelService
    {
        Dictionary<string, string> Stages { get;}
        Dictionary<string, string> Companies { get;}

        /// <summary>
        /// Load dictionaries from Excel file.
        /// </summary>
        /// <returns></returns>
        IResult<bool> LoadDictionaries();
    }
}
