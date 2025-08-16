
using AgendaEstudiantil.Tests.Helpers;
using OpenQA.Selenium;

namespace AgendaEstudiantil.Tests.Tests
{
    [TestClass]
    public class VerDetallesEventoTests : BaseTest
    {

        [TestCleanup]
        public void Cleanup()
        {
            CerrarNavegador();
        }

        [TestMethod]
        public void DetallesEvento_MuestraCamposCorrectos()
        {
            IniciarSesion();

            NavegarA("http://localhost:5134/Eventos");

            wait.Until(d => d.FindElements(By.CssSelector("table tbody tr")).Count > 0);

            var primerDetalle = driver.FindElement(By.CssSelector("table tbody tr:first-child a.btn-secondary"));
            primerDetalle.Click();

            Assert.IsTrue(driver.FindElement(By.XPath("//dt[contains(text(), 'Titulo')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//dt[contains(text(), 'Fecha')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//dt[contains(text(), 'Descripcion')]")).Displayed);
            Assert.IsTrue(driver.FindElement(By.XPath("//dt[contains(text(), 'Estado')]")).Displayed);

            var linkVolver = driver.FindElement(By.LinkText("De vuelta a la lista"));
            Assert.IsTrue(linkVolver.Displayed);

            GuardarEvidencia
                (
                    "DetallesEvento_CaminoFeliz", 
                    "Reporte de prueba de validación de detalles positiva",
                    "Se muestran correctamente los campos Título, Fecha, Descripción y Estado."
                );
        }
    }
}
