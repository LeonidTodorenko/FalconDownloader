using FalconDownloader.Contracts;
using FalconDownloader.Models;
using NetOffice.WordApi;
using System;
using System.Configuration;
using System.Linq;
using System.Text;

namespace FalconDownloader.Services
{
    public class WordService : IWordService
    {
        public IResult<bool> UnlinkFields(string path)
        {
            try
            {
                var fieldTypes = ConfigurationManager.AppSettings["notUnlinkFieldTypes"].Split(',')
                    .Select(n => n.Trim())
                    .ToList();

                using (var application = new Application())
                {
                    using (var document = application.Documents.Open(path))
                    {
                        var fields = document.Fields
                            .Where(n => fieldTypes.IndexOf(n.Type.ToString()) < 0)
                            .ToList();

                        fields.ForEach(n =>
                        {
                            try
                            {
                                n.Unlink();
                            }
                            catch (Exception)
                            {
                                // ignore
                            }
                        });

                        document.Save();
                        document.Close();
                    }

                    application.Quit();
                }
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();
                while (ex != null)
                {
                    sb.Append(ex.Message + " ");
                    ex = ex.InnerException;
                }

                return new Result<bool>(false, sb.ToString());
            }

            return new Result<bool>(true);
        }
    }
}
