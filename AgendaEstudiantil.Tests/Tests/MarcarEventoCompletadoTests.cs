

using AgendaEstudiantil.Tests.Helpers;
using OpenQA.Selenium;

namespace AgendaEstudiantil.Tests.Tests
{
    [TestClass]
    public class MarcarEventoCompletadoTests : BaseTest
    {

        [TestCleanup]
        public void Cleanup()
        {
            CerrarNavegador();
        }

        [TestMethod]
        public void MarcarEventoComoCompletado_CaminoFeliz()
        {

            IniciarSesion();
            NavegarA("http://localhost:5134/Eventos");

            wait.Until(d => d.FindElement(By.CssSelector("table.table")));

            var primeraFila = driver.FindElements(By.CssSelector("table.table tbody tr"))
                                    .FirstOrDefault(f => f.FindElements(By.XPath(".//button[contains(text(),'Marcar como completado')]")).Count > 0);

            Assert.IsNotNull(primeraFila, "No hay eventos pendientes para completar.");

            var btnCompletar = primeraFila.FindElement(By.XPath(".//button[contains(text(),'Marcar como completado')]"));

            btnCompletar.Click();

            wait.Until(d =>
            {
                var filaActualizada = d.FindElements(By.CssSelector("table.table tbody tr"))
                                       .FirstOrDefault(f => f.FindElements(By.XPath(".//span[contains(text(),'Completado')]")).Count > 0);
                return filaActualizada != null;
            });

            var filaFinal = driver.FindElements(By.CssSelector("table.table tbody tr"))
                                  .First(f => f.FindElements(By.XPath(".//span[contains(text(),'Completado')]")).Count > 0);

            bool badgeVisible = filaFinal.FindElement(By.XPath(".//span[contains(text(),'Completado')]")).Displayed;
            Assert.IsTrue(badgeVisible, "El evento no se marcó como completado correctamente.");

            GuardarEvidencia(
                "Evento_Completado",
                "Prueba Marcar Evento Completado",
                "Se marcó correctamente el primer evento de la tabla como completado y se aplicó el estilo esperado."
            );

        }
    }
}
