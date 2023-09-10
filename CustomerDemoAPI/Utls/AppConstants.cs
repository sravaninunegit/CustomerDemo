using CustomerData.Models;

namespace CustomerAPi.Utls
{
    public class AppConstants
    {
        public static String  CustomerCsv= "CustomerCsv";
        public static String  OrderItemsCsv= "OrderCsv";
        public static String  OrdersCsv= "ItemCsv";


        public static List<T> ReadCsvFile<T>(string filePath)
        {
            var records = new List<T>();

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    var csvRecords = reader.ReadToEnd().Split("\n").Skip(1).ToList();

                    foreach (var csvRecord in csvRecords)
                    {
                        var values = csvRecord.Split(',');

                        if (values.Length > 0)
                        {
                            var record = Activator.CreateInstance<T>();

                            for (var i = 0; i < values.Length; i++)
                            {
                                var property = typeof(T).GetProperties()[i];
                                var value = values[i];

                                if (property.PropertyType == typeof(DateTime?))
                                {
                                 
                                    if (DateTime.TryParse(value, out DateTime parsedDate))
                                    {
                                        property.SetValue(record, parsedDate);
                                    }
                                    else
                                    {
                                        property.SetValue(record, null); 
                                    }
                                }
                                else if (property.PropertyType == typeof(int))
                                {
                                   
                                    if (int.TryParse(value, out int parsedInt))
                                    {
                                        property.SetValue(record, parsedInt);
                                    }
                                    else
                                    {
                                        property.SetValue(record, 0); 
                                    }
                                }
                                else
                                {
                                    
                                    property.SetValue(record, Convert.ChangeType(value, property.PropertyType));
                                }
                            }

                            records.Add(record);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading CSV: {ex.Message}");
                
            }

            return records;
        }

    }
}

