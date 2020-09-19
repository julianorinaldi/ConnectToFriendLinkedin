using CoreApp;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;

namespace ConsumeGoogleSheet
{
    public class GoogleSheet
    {
        private IOutput _output;

        public GoogleSheet(IOutput output)
        {
            _output = output;
        }

        public string[] GetAddresses()
        {
            List<string> addresses = new List<string>();

            var apiKey = "AIzaSyBKWeVUp9K4B2Fv37Xf0Ry65svOJCd1sNA";
            try
            {
                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    ApiKey = apiKey,
                    ApplicationName = string.Format($"{System.Diagnostics.Process.GetCurrentProcess().ProcessName}")
                });

                String spreadsheetId = "1ota2j_ON3LAAQWzvsJEiSERcP9ixDJrHnIj5mJOFWiE";
                String range = "Contatos!C2:C";

                SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, range);

                ValueRange response = request.Execute();
                IList<IList<Object>> values = response.Values;
                if (values != null && values.Count > 0)
                {
                    foreach (var row in values)
                    {
                        if (row.Count > 0)
                        {
                            _output.WriteText($"Endereço encontrado: {row[0]}");
                            addresses.Add(row[0].ToString());
                        }
                    }
                }
                else
                {
                    _output.WriteText("Dados não encontrados");
                }
            }
            catch
            {
                throw;
            }

            return addresses.ToArray();
        }
    }
}