using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using DbConnectionHelper;
using System.Collections.Generic;

namespace DbConnectionHelperTests
{
    [TestClass]
    public class RequestManagerTests
    {
        [TestMethod]
        public void GetClientRequests_ValidClientId_ReturnsDataTable()
        {
            // Проверяет, что метод возвращает таблицу данных,
            // когда передан корректный идентификатор клиента.
            int clientId = 1;
            DataTable result = RequestManager.GetClientRequests(clientId);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DataTable));
        }

        [TestMethod]
        public void GetNextRequestID_ReturnsValidId()
        {
            // Проверяет, что метод возвращает положительный
            // идентификатор для следующего запроса.
            int nextId = RequestManager.GetNextRequestID();
            Assert.IsTrue(nextId > 0);
        }

        [TestMethod]
        public void GetLoginHistory_ReturnsDataTable()
        {
            // Проверяет, что метод возвращает
            // таблицу данных истории логинов.
            DataTable result = LoginHistoryManager.GetLoginHistory();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DataTable));
        }

        [TestMethod]
        public void FilterLoginHistory_ValidSearchText_ReturnsFilteredDataView()
        {
            // Проверяет, что метод возвращает отфильтрованные данные,
            // когда используется текст для поиска.
            DataTable loginHistoryTable = LoginHistoryManager.GetLoginHistory();
            string searchText = "user1";
            DataView result = LoginHistoryManager.FilterLoginHistory(loginHistoryTable, searchText);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateRequest_InvalidClientId_ThrowsException()
        {
            // Проверяет, что метод выбрасывает ошибку при попытке
            // создать запрос с недопустимым идентификатором клиента.
            int invalidClientId = -1;
            RequestManager.CreateRequest(1, DateTime.Now, "Проблема", 1, null, null, null, invalidClientId, "Модель", 1);
        }
    }
}
