using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SmartWaste.Web.Models;
using System.Data;

namespace SmartWaste.Web.Controllers
{
    public class PublicController : Controller
    {
        private readonly string _connectionString;

        public PublicController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("DefaultConnection is missing.");
        }

        [HttpGet("/public/map")]
        public IActionResult Map()
        {
            return Redirect("/PublicMap");
        }

        [HttpGet("/public/nearby")]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Nearby(
            double lat,
            double lng,
            int radiusMeters = 1000,
            byte? statusId = null)
        {
            radiusMeters = Math.Clamp(radiusMeters, 50, 5000);

            var bins = new List<NearbyBinDto>();

            await using var conn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand("dbo.sp_GetNearbyBins", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddRange(new[]
            {
                new SqlParameter("@Latitude", SqlDbType.Float) { Value = lat },
                new SqlParameter("@Longitude", SqlDbType.Float) { Value = lng },
                new SqlParameter("@RadiusMeters", SqlDbType.Int) { Value = radiusMeters },
                new SqlParameter("@StatusId", SqlDbType.TinyInt)
                {
                    Value = statusId ?? (object)DBNull.Value
                }
            });

            await conn.OpenAsync();

            await using var reader = await cmd.ExecuteReaderAsync();

            // Cache ordinals once
            int ordBinId = reader.GetOrdinal("BinId");
            int ordLocation = reader.GetOrdinal("Location");
            int ordLat = reader.GetOrdinal("Latitude");
            int ordLng = reader.GetOrdinal("Longitude");
            int ordCapacity = reader.GetOrdinal("CapacityLiters");
            int ordWasteType = reader.GetOrdinal("WasteType");
            int ordStatusId = reader.GetOrdinal("BinStatusId");
            int ordStatusName = reader.GetOrdinal("StatusName");
            int ordFill = reader.GetOrdinal("LatestFillLevelPercent");
            int ordLastReading = reader.GetOrdinal("LastReadingTime");
            int ordDistance = reader.GetOrdinal("DistanceMeters");

            while (await reader.ReadAsync())
            {
                bins.Add(new NearbyBinDto
                {
                    BinId = reader.GetInt32(ordBinId),
                    Location = reader.GetString(ordLocation),
                    Latitude = reader.GetDecimal(ordLat),
                    Longitude = reader.GetDecimal(ordLng),
                    CapacityLiters = reader.GetInt32(ordCapacity),
                    WasteType = reader.GetString(ordWasteType),
                    BinStatusId = reader.GetByte(ordStatusId),
                    StatusName = reader.GetString(ordStatusName),
                    LatestFillLevelPercent = reader.GetInt32(ordFill),
                    LastReadingTime = reader.IsDBNull(ordLastReading)
                        ? null
                        : reader.GetDateTime(ordLastReading),
                    DistanceMeters = reader.GetInt32(ordDistance)
                });
            }

            return Ok(bins);
        }
    }
}
