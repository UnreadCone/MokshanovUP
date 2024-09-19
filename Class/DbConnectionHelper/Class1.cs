using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace DbConnectionHelper
{
    public static class DbConnectionHelper
    {
        public static string ConnectionString = @"Data Source=DESKTOP-B9P13UL\ADM;Initial Catalog=МокшановУП;Integrated Security=True";
    }

    public static class RequestManager
    {
        public static DataTable GetClientRequests(int clientId)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            r.startDate AS [Дата заявки],
                            r.problemDescription AS [Описание проблемы],
                            t.techTypeName AS [Тип техники],
                            r.model AS [Модель техники],
                            s.StatusName AS [Статус заявки],
                            r.completionDate AS [Дата завершения]
                        FROM [dbo].[Requests] r
                        JOIN [dbo].[TechTypes] t ON r.techTypeID = t.techTypeID
                        JOIN [dbo].[StatusRequest] s ON r.requestStatus = s.StatusId
                        WHERE r.clientID = @clientId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@clientId", clientId);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при загрузке заявок: " + ex.Message);
                }
            }
        }

        public static int GetNextRequestID()
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT ISNULL(MAX(requestID), 0) + 1 FROM Requests";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при получении следующего ID заявки: " + ex.Message);
                }
            }
        }

        public static int GetTechTypeId(string techTypeName)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT techTypeID FROM TechTypes WHERE techTypeName = @techTypeName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@techTypeName", techTypeName);
                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            return Convert.ToInt32(result);
                        }
                        else
                        {
                            string insertQuery = "INSERT INTO TechTypes (techTypeName) OUTPUT INSERTED.techTypeID VALUES (@techTypeName)";
                            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                            {
                                insertCommand.Parameters.AddWithValue("@techTypeName", techTypeName);
                                return (int)insertCommand.ExecuteScalar();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при получении ID типа техники: " + ex.Message);
                }
            }
        }

        public static void CreateRequest(int requestId, DateTime startDate, string problemDescription, int requestStatus, DateTime? completionDate, string repairParts, int? masterId, int clientId, string model, int techTypeId)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        INSERT INTO Requests 
                            (requestID, startDate, problemDescription, requestStatus, completionDate, repairParts, masterID, clientID, model, techTypeID)
                        VALUES 
                            (@requestID, @startDate, @problemDescription, @requestStatus, @completionDate, @repairParts, @masterID, @clientID, @model, @techTypeID)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@requestID", requestId);
                        command.Parameters.AddWithValue("@startDate", startDate);
                        command.Parameters.AddWithValue("@problemDescription", problemDescription);
                        command.Parameters.AddWithValue("@requestStatus", requestStatus);
                        command.Parameters.AddWithValue("@completionDate", (object)completionDate ?? DBNull.Value);
                        command.Parameters.AddWithValue("@repairParts", (object)repairParts ?? DBNull.Value);
                        command.Parameters.AddWithValue("@masterID", (object)masterId ?? DBNull.Value);
                        command.Parameters.AddWithValue("@clientID", clientId);
                        command.Parameters.AddWithValue("@model", model);
                        command.Parameters.AddWithValue("@techTypeID", techTypeId);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при создании заявки: " + ex.Message);
                }
            }
        }
    }

    public static class LoginHistoryManager
    {
        public static DataTable GetLoginHistory()
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT [Логин пользователя], [Вход подтверждён], [Дата] FROM [dbo].[История]";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    return dataTable;
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при загрузке данных: " + ex.Message);
                }
            }
        }

        public static DataView FilterLoginHistory(DataTable loginHistoryTable, string searchText)
        {
            if (loginHistoryTable == null)
            {
                return null;
            }

            string filterExpression = string.Empty;

            if (string.IsNullOrEmpty(searchText))
            {
                return new DataView(loginHistoryTable);
            }
            else
            {
                if (int.TryParse(searchText, out int numericValue))
                {
                    filterExpression = $"[Вход подтверждён] = {numericValue}";
                }
                else
                {
                    filterExpression = $"[Логин пользователя] LIKE '%{searchText}%'";
                }

                DataView dataView = new DataView(loginHistoryTable);
                dataView.RowFilter = filterExpression;
                return dataView;
            }
        }
    }

    public static class MasterRequestManager
    {
        public static DataTable LoadMasterRequests(int masterId)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                    SELECT r.requestID, r.startDate, r.problemDescription, r.requestStatus, 
                           r.repairParts, r.model, r.techTypeID, t.techTypeName, c.message
                    FROM Requests r
                    LEFT JOIN TechTypes t ON r.techTypeID = t.techTypeID
                    LEFT JOIN Comments c ON r.requestID = c.requestID
                    WHERE r.masterID = @masterID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@masterID", masterId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при загрузке заявок: " + ex.Message);
                }
            }
        }

        public static void SaveChanges(DataGridView dataGridView)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                try
                {
                    connection.Open();

                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        if (row.IsNewRow) continue;

                        int requestId = Convert.ToInt32(row.Cells["requestID"].Value);
                        int requestStatus = Convert.ToInt32(row.Cells["requestStatus"].Value);
                        string repairParts = row.Cells["repairParts"].Value?.ToString();
                        string message = row.Cells["message"].Value?.ToString();
                        DateTime? completionDate = null;

                        if (requestStatus == 3)
                        {
                            completionDate = DateTime.Now;
                        }

                        string updateRequestQuery = @"
                            UPDATE Requests
                            SET requestStatus = @requestStatus, repairParts = @repairParts, completionDate = @completionDate
                            WHERE requestID = @requestID";

                        using (SqlCommand updateRequestCommand = new SqlCommand(updateRequestQuery, connection))
                        {
                            updateRequestCommand.Parameters.AddWithValue("@requestStatus", requestStatus);
                            updateRequestCommand.Parameters.AddWithValue("@repairParts", (object)repairParts ?? DBNull.Value);
                            updateRequestCommand.Parameters.AddWithValue("@completionDate", (object)completionDate ?? DBNull.Value);
                            updateRequestCommand.Parameters.AddWithValue("@requestID", requestId);

                            updateRequestCommand.ExecuteNonQuery();
                        }

                        string updateCommentQuery = @"
                            UPDATE Comments
                            SET message = @message
                            WHERE requestID = @requestID";

                        using (SqlCommand updateCommentCommand = new SqlCommand(updateCommentQuery, connection))
                        {
                            updateCommentCommand.Parameters.AddWithValue("@message", (object)message ?? DBNull.Value);
                            updateCommentCommand.Parameters.AddWithValue("@requestID", requestId);

                            updateCommentCommand.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при сохранении изменений: " + ex.Message);
                }
            }
        }
    }

    public static class OperationManager
    {
        public static DataTable LoadRequests()
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            r.requestID,
                            c.fio AS [fio],   
                            c.phone AS [phone],  
                            r.startDate,
                            r.problemDescription,
                            r.model,
                            r.requestStatus,
                            r.masterID
                        FROM [dbo].[Requests] r
                        LEFT JOIN [dbo].[Users] c ON r.clientID = c.userID  
                        LEFT JOIN [dbo].[Users] m ON r.masterID = m.userID  
                        ";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable requestsTable = new DataTable();
                    adapter.Fill(requestsTable);

                    return requestsTable;
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при загрузке заявок: " + ex.Message);
                }
            }
        }

        public static DataTable GetStatusData()
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT StatusId, StatusName FROM [dbo].[StatusRequest]";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable statusTable = new DataTable();
                    adapter.Fill(statusTable);

                    return statusTable;
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при получении данных статусов: " + ex.Message);
                }
            }
        }

        public static DataTable GetMasterData()
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT userID, fio FROM [dbo].[Users] WHERE [type] = 'Мастер'";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable masterTable = new DataTable();
                    adapter.Fill(masterTable);

                    return masterTable;
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при получении данных мастеров: " + ex.Message);
                }
            }
        }

        public static void SaveChanges(DataTable requestsTable)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                try
                {
                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand updateCommand = new SqlCommand(@"
                        UPDATE [dbo].[Requests]
                        SET
                            requestStatus = @requestStatus,
                            masterID = @masterID
                        WHERE requestID = @requestID", connection);

                    updateCommand.Parameters.Add("@requestStatus", SqlDbType.Int, 4, "requestStatus");
                    updateCommand.Parameters.Add("@masterID", SqlDbType.Int, 4, "masterID");
                    updateCommand.Parameters.Add("@requestID", SqlDbType.Int, 4, "requestID");

                    adapter.UpdateCommand = updateCommand;
                    adapter.Update(requestsTable);
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при сохранении данных: " + ex.Message);
                }
            }
        }

        public static DataTable SearchRequests(string searchText)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            r.requestID,
                            c.fio AS [fio],   
                            c.phone AS [phone],  
                            r.startDate,
                            r.problemDescription,
                            r.model,
                            r.requestStatus,
                            r.masterID
                        FROM [dbo].[Requests] r
                        LEFT JOIN [dbo].[Users] c ON r.clientID = c.userID  
                        LEFT JOIN [dbo].[Users] m ON r.masterID = m.userID  
                        WHERE (m.fio LIKE @searchText OR c.fio LIKE @searchText OR r.requestStatus = @searchStatus)
                        ";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                    if (int.TryParse(searchText, out int status))
                    {
                        command.Parameters.AddWithValue("@searchStatus", status);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@searchStatus", DBNull.Value);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable requestsTable = new DataTable();
                    adapter.Fill(requestsTable);

                    return requestsTable;
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при поиске: " + ex.Message);
                }
            }
        }
    }

    public static class ProfileManager
    {
        public static DataTable GetUserProfile(int userID)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT fio, phone, type FROM Users WHERE userID = @userID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable profileTable = new DataTable();
                            profileTable.Load(reader);
                            return profileTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при загрузке профиля: " + ex.Message);
                }
            }
        }
    }
}