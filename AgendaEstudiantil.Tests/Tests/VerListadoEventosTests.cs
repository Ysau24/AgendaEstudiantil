
using AgendaEstudiantil.Tests.Helpers;
using OpenQA.Selenium;

namespace AgendaEstudiantil.Tests.Tests
{
    [TestClass]
    public class VerListadoEventosTests : BaseTest
    {

        [TestCleanup]
        public void Cleanup()
        {
            CerrarNavegador();
        }

        [TestMethod]
        public void VerListadoEventos_CaminoPositivo()
        {
            IniciarSesion();

            driver.Navigate().GoToUrl("https://localhost:7127/Eventos");

            wait.Until(d => d.FindElement(By.CssSelector("table.table")));

            var filas = driver.FindElements(By.CssSelector("table.table tbody tr"));

            Assert.IsTrue(filas.Count > 0, "No hay eventos disponibles.");

            List<DateTime> fechas = new List<DateTime>();
            foreach (var fila in filas)
            {
                var fechaTexto = fila.FindElements(By.TagName("td"))[1].Text;
                fechas.Add(DateTime.Parse(fechaTexto));
            }

            var fechasOrdenadas = fechas.OrderBy(f => f).ToList();
            CollectionAssert.AreEqual(fechasOrdenadas, fechas, "Los eventos no están ordenados por fecha");

            GuardarEvidencia
                (
                    "ListadoEventos_CaminoPositivo",
                    "Listado de Eventos", 
                    "Se verifica que los eventos estén ordenados por fecha."
                );
        }

    }
}
