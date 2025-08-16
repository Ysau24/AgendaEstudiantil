

using AgendaEstudiantil.Tests.Helpers;
using OpenQA.Selenium;

namespace AgendaEstudiantil.Tests.Tests
{
    [TestClass]
    public class BuscarEventoPorTituloTests : BaseTest
    {

        [TestCleanup]
        public void Cleanup()
        {
            CerrarNavegador();
        }

        [TestMethod]
        public void BuscarEventoPorTitulo_CaminoPositivo()
        {
            IniciarSesion();

            NavegarA("https://localhost:7127/Eventos");

            wait.Until(d => d.FindElements(By.CssSelector("table.table tbody")).Count > 0);

            var inputBusqueda = driver.FindElement(By.Id("busqueda"));
            inputBusqueda.Clear();
            inputBusqueda.SendKeys("proyecto");

            wait.Until(d =>
            {
                var filas = d.FindElements(By.CssSelector("table.table tbody tr"));
                return filas.Any(f =>
                {
                    if (!f.Displayed) return false;
                    var celdas = f.FindElements(By.TagName("td"));
                    if (celdas.Count == 0) return false;
                    return celdas[0].Text.Trim().ToLower().Contains("proyecto");
                });
            });

            var filasVisibles = driver.FindElements(By.CssSelector("table.table tbody tr"))
                                      .Where(f => f.Displayed)
                                      .ToList();

            bool contieneProyectoFinal = filasVisibles.Any(f =>
            {
                var celdas = f.FindElements(By.TagName("td"));
                return celdas.Count > 0 &&
                       celdas[0].Text.Trim().Equals("Proyecto final", StringComparison.OrdinalIgnoreCase);
            });

            Assert.IsTrue(contieneProyectoFinal, "El evento 'Proyecto final' no aparece en los resultados visibles tras la búsqueda.");

            GuardarEvidencia
            (
                "BuscarEventoPorTitulo_CaminoFeliz",
                "Prueba positiva: Búsqueda por título",
                "Se encontraron resultados al escribir 'proyecto' y se visualiza 'Proyecto final'."
            );
        }

    }
}
