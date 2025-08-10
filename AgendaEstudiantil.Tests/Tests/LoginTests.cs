using AgendaEstudiantil.Tests.Helpers;


namespace AgendaEstudiantil.Tests.Tests
{
    [TestClass]
    public class LoginTests: BaseTest
    {

        [TestCleanup]
        public void Cleanup()
        {
            CerrarNavegador();
        }

        [TestMethod]
        public void Login_CaminoPositivo_UserCorrecto()
        {
            IniciarSesion();

            string currentUrl = driver.Url;
            Assert.IsTrue(currentUrl.Contains("/Eventos"), "No se redirigió correctamente después del login.");

            GuardarEvidencia
                (
                    "Login_CaminoFeliz",
                    "Reporte de Prueba Login Positiva",
                    $"Login exitoso. Redirigido a <b>{currentUrl}</b>."
                );

        }

    }
}
