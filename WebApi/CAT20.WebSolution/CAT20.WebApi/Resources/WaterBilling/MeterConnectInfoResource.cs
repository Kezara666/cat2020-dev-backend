using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Text.Json;
using System.Globalization;
using CAT20.Core.Models.WaterBilling;

namespace CAT20.WebApi.Resources.WaterBilling
{
    public class MeterConnectInfoResource
    {

        public string? ConnectionId { get; set; } 
        
        public int? KeyPattern { get; set; } 
        public int? OfficeId { get; set; } 

        
        public string? ConnectionNo { get; set; }

        public string? MeterNo { get; set; }


        //[System.Text.Json.Serialization.JsonConverter(typeof(DateOnlyJsonConverter))]
        //public DateOnly? InstallDate { get; set; }

        public int? SubRoadId { get; set; }

        public WaterProjectSubRoadResource? WaterProjectSubRoad { get; set; }
        public bool? IsAssigned { get; set; }

        public int? OrderNo { get; set; }

        // mandatory fields
        public int? Status { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

    }
    public class DateOnlyJsonConverter : System.Text.Json.Serialization.JsonConverter<DateOnly>
    {
        private const string Format = "yyyy-MM-dd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateOnly.ParseExact(reader.GetString()!, Format,CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
        }
    }

}