using server.Application.Mappings;
using server.Domain.Models;

namespace server.Application.Helpers;
public static class CsvExtension
{
    public static Transaction ParseLine(this string line)
    {
        var values = line.Split(',');
     
        try
        {
            var model = values.TransactionModelMapping();
            return model;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error occured {ex.Message}");
        }
    }

    

}