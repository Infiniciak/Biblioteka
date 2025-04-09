using Library;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Diagnostics;

namespace Biblioteka.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly LibraryDbContext _context;
        private readonly ILogger<TestController> _logger;

        public TestController(LibraryDbContext context, ILogger<TestController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("database")]
        public async Task<IActionResult> TestDatabaseConnection()
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync();
                var connection = _context.Database.GetDbConnection();

                _logger.LogInformation("Database connection test. Success: {Success}, Database: {Database}",
                    canConnect, connection.Database);

                return Ok(new
                {
                    Success = canConnect,
                    Message = canConnect ? "Database connection successful" : "Database connection failed",
                    Database = connection.Database,
                    Server = connection.DataSource,
                    ConnectionString = connection.ConnectionString // Tylko dla celów debugowania!
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database connection test failed");
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Server error",
                    Details = ex.Message,
                    StackTrace = ex.StackTrace // Tylko dla developmentu!
                });
            }
        }

        [HttpGet("tables/{tableName}")]
        public async Task<IActionResult> GetTableData(string tableName)
        {
            var allowedTables = new Dictionary<string, string>
            {
                { "members", "Czlonkowie" },
                { "books", "Ksiazki" },
                { "borrows", "Wypozyczenia" },
                { "categories", "Kategorie" }
            };

            if (!allowedTables.TryGetValue(tableName.ToLower(), out var table))
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Invalid table name",
                    AvailableTables = allowedTables.Keys
                });
            }

            try
            {
                var data = new List<Dictionary<string, object?>>();
                await using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM {table} LIMIT 100";
                    await _context.Database.OpenConnectionAsync();

                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new Dictionary<string, object?>();
                            for (var i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            }
                            data.Add(row);
                        }
                    }
                }

                return Ok(new
                {
                    Success = true,
                    Table = table,
                    Count = data.Count,
                    Data = data
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching data from table {Table}", tableName);
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "Error processing request",
                    Details = ex.Message
                });
            }
        }
        [HttpGet("debug-info")]
        public IActionResult GetDebugInfo()
        {
            if (!Debugger.IsAttached)
            {
                return BadRequest("Debug info available only when debugger is attached");
            }

            var connection = _context.Database.GetDbConnection();
            var migrations = new
            {
                Pending = _context.Database.GetPendingMigrations().ToList(),
                Applied = _context.Database.GetAppliedMigrations().ToList()
            };

            return Ok(new
            {
                Timestamp = DateTime.UtcNow,
                Environment = Environment.GetEnvironmentVariables(),
                Database = new
                {
                    State = connection.State,
                    ConnectionString = MaskConnectionString(connection.ConnectionString),
                    ServerVersion = connection.ServerVersion,
                    Timeout = connection.ConnectionTimeout
                },
                Migrations = migrations,
                Services = HttpContext.RequestServices.GetServices<object>()
                    .Select(s => s.GetType().Name)
                    .Distinct()
                    .ToList()
            });
        }

        private string MaskConnectionString(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) return "[empty]";

            var builder = new DbConnectionStringBuilder { ConnectionString = connectionString };
            if (builder.TryGetValue("Password", out var password))
            {
                builder["Password"] = "*****";
            }
            return builder.ToString();
        }
    }
}