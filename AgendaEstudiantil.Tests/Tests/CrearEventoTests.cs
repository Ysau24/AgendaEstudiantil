using AgendaEstudiantil.Tests.Helpers;
using OpenQA.Selenium;

namespace AgendaEstudiantil.Tests.Tests
{
    [TestClass]
    public class CrearEventoTests : BaseTest
    {

        [TestCleanup]
        public void Cleanup()
        {
            CerrarNavegador();
        }

        [TestMethod]
        public void CrearEvento_SinFecha_DeberiaMostrarError()
        {

            IniciarSesion();

            driver.Navigate().GoToUrl("http://localhost:5134/Eventos/Create");

            wait.Until(d => d.FindElement(By.Id("Titulo")));

            driver.FindElement(By.Id("Titulo")).SendKeys("Tarea de sociales");
            driver.FindElement(By.Id("Descripcion")).SendKeys("Probando que no se guardará porque no tiene fecha");

            driver.FindElement(By.CssSelector("input[type='submit']")).Click();             

            wait.Until(d => d.FindElement(By.TagName("body")).Text.Contains("La fecha es obligatoria"));

            var bodyText = driver.FindElement(By.TagName("body")).Text;

            Assert.IsTrue(bodyText.Contains("La fecha es obligatoria"), "No se mostró el mensaje de error esperado al dejar la fecha vacía.");

            GuardarEvidencia
                (
                    "CrearEvento_PruebaNegativa",
                    "Reporte de prueba de creación de evento negativa",
                    $"Se dejó el campo 'Fecha' vacío. El sistema mostró el mensaje de error y no redirigió"
                );
        }
    }
}
