using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ControllerREST.Models;
using System;
using System.Linq;


public static class SeedData
{
    public static List<List<String>> readCsv(String fileName, char sep, int cols, int rows){
        List<List<String>> data = new List<List<String>>();
        using( var reader = new StreamReader(fileName)){
            int size = 0;
            while(!reader.EndOfStream && size < rows){
                
                var line = reader.ReadLine();
                string[] values = line.Split(sep);
                List<String> row = new List<String>();
                for(int i = 0; i < 10; i++){ //want to read first 10 columns only maybe change later
                    row.Add(values[i]);
                }
                data.Add(row);
                size++;
            }
        }
        return data;
    }

    public static Measure[] createMeasures(List<List<String>> data){
        Measure[] measures = new Measure[data.Count];
        foreach(List<String> row in data){
            measures[data.IndexOf(row)] = new Measure{
                apparatusId = row[0],
                apparatusVersion = row[1],
                apparatusSensorType = row[2],
                apparatusTubeType = row[3],
                temperature = row[4],
                value = row[5],
                hitsNumber = row[6],
                calibrationFunction = row[7],
                startTime = row[8],
                endTime = row[9]
            };
        }
        return measures;
    }
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ContextDb(
            serviceProvider.GetRequiredService<
                DbContextOptions<ContextDb>>()))
        {
            // Look for any movies.
            if (context.Measure.Any())
            {
                return;   // DB has been seeded
            }
            context.Measure.AddRange(
                createMeasures(
                    readCsv(@"measurements_withoutEnclosedObject.csv",
                            ';',
                            10,
                            100))
            );
            context.SaveChanges();
        }
    }
}