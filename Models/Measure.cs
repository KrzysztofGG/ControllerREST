using System.ComponentModel.DataAnnotations;
namespace ControllerREST.Models{
    public class Measure{
    public int Id {get; set;}
    public string apparatusId {get; set;}
    public string apparatusVersion {get; set;}
    public string apparatusSensorType {get; set;}
    public string apparatusTubeType {get; set;}
    public string temperature {get; set;}
    public string value {get; set;}
    public string hitsNumber {get; set;}
    public string calibrationFunction {get; set;}
    public string startTime {get; set;}
    public string endTime {get; set;}
    }
}
