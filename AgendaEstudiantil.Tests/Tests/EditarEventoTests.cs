using AgendaEstudiantil.Tests.Helpers;
using OpenQA.Selenium;
namespace AgendaEstudiantil.Tests.Tests
{
    [TestClass]
    public class EditarEventoTests : BaseTest
    {
        [TestCleanup]
        public void Cleanup()
        {
            CerrarNavegador();
        }

        [TestMethod]
        public void EditarEvento_SinFecha_DeberiaMostrarError()
        {
            IniciarSesion();

            // Esto busca el primer evento para poder editarlo
            driver.Navigate().GoToUrl("http://localhost:5134/Eventos");
            wait.Until(d => d.FindElement(By.CssSelector("table.table")));

            var botonEditar = driver.FindElement(By.CssSelector("a.btn-warning"));
            botonEditar.Click();

            wait.Until(d => d.FindElement(By.Id("Titulo")));

            var tituloInput = driver.FindElement(By.Id("Titulo"));
            tituloInput.Clear();
            tituloInput.SendKeys("Evento sin fecha");

            var fechaInput = driver.FindElement(By.Id("Fecha"));
            fechaInput.Clear();

            driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            wait.Until(d => d.FindElement(By.TagName("body")).Text.Contains("La fecha es obligatoria"));

            var bodyText = driver.FindElement(By.TagName("body")).Text;
            Assert.IsTrue(bodyText.Contains("La fecha es obligatoria"), "No se mostró el mensaje de error esperado al dejar la fecha vacía.");


            GuardarEvidencia
                (
                    "EditarEvento_PruebaNegativa",
                    "Reporte de prueba de edición de evento negativa",
                    $"Se dejó el campo 'Fecha' vacío. El sistema mostró el mensaje de error y no redirigió"
                );

        }
    }
}
