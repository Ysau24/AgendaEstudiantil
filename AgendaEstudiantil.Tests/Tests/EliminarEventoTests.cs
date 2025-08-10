using AgendaEstudiantil.Tests.Helpers;
using OpenQA.Selenium;

namespace AgendaEstudiantil.Tests.Tests
{
    [TestClass]
    public class EliminarEventoTests : BaseTest
    {
        [TestCleanup]
        public void Cleanup()
        {
            CerrarNavegador();
        }

        [TestMethod]
        public void EliminarEvento_DeberiaEliminarEventoExistente()
        {
            IniciarSesion();

            driver.Navigate().GoToUrl("http://localhost:5134/Eventos/Index");

            wait.Until(d => d.FindElement(By.CssSelector("table.table")));

            //Esto selecciona el primer botón "Eliminar" dentro de la tabla
            var botonEliminar = driver.FindElement(By.CssSelector("table.table a.btn-danger"));

            var filaEvento = botonEliminar.FindElement(By.XPath("./ancestor::tr"));
            var tituloEvento = filaEvento.FindElement(By.CssSelector("td:nth-child(1)")).Text;

            botonEliminar.Click();

            wait.Until(d => d.FindElement(By.CssSelector("input[type='submit']")));

            driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            wait.Until(d => d.Url.Contains("/Eventos"));

            var bodyText = driver.FindElement(By.TagName("body")).Text;
            Assert.IsFalse(bodyText.Contains(tituloEvento), "El evento no fue eliminado correctamente.");

            GuardarEvidencia
                (
                    "EliminarEvento_CaminoFeliz",
                    "Reporte de prueba de eliminación de evento positiva",
                    $"Se eliminó el evento con título: <b>{tituloEvento}</b>"
                );

        }
    }
}