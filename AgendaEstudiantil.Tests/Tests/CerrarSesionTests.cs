
using AgendaEstudiantil.Tests.Helpers;
using OpenQA.Selenium;

namespace AgendaEstudiantil.Tests.Tests
{
    [TestClass]
    public class CerrarSesionTests : BaseTest
    {

        [TestCleanup]
        public void Cleanup()
        {
            CerrarNavegador();
        }

        [TestMethod]
        public void CerrarSesion_CaminoPositivo() 
        {
            IniciarSesion();

            driver.FindElement(By.CssSelector("form.form-inline button[type='submit']")).Click();

            wait.Until(d => d.FindElement(By.CssSelector("a.btn.btn-outline-light")));

            string currentUrl = driver.Url;
            Assert.IsTrue(currentUrl == "https://localhost:7127/",
                "No se redirigió correctamente a la página principal después de cerrar sesión.");

            GuardarEvidencia(
                "CerrarSesion_CaminoFeliz",
                "Reporte de Prueba Cierre de Sesión Positiva",
                $"Cierre de sesión exitoso. Redirigido a <b>{currentUrl}</b>."
            );
        }
    }
}
