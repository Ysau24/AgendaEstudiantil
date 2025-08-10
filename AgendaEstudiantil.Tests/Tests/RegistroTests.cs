using AgendaEstudiantil.Tests.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace AgendaEstudiantil.Tests.Tests
{
    [TestClass]
    public class RegistroTests: BaseTest
    {

        [TestCleanup]
        public void Cleanup()
        {
            CerrarNavegador();
        }

        [TestMethod]
        public void Registro_Falla_ContraseñasNoCoinciden()
        {
            driver.Navigate().GoToUrl("http://localhost:5134/Identity/Account/Register");

            driver.FindElement(By.Id("Input_Email")).SendKeys("fulano@gmail.com");
            driver.FindElement(By.Id("Input_Password")).SendKeys("probando123!");
            driver.FindElement(By.Id("Input_ConfirmPassword")).SendKeys("otra123!");

            driver.FindElement(By.Id("registerSubmit")).Click();

            var bodyText = driver.FindElement(By.TagName("body")).Text;

            Assert.IsTrue(bodyText.Contains("The password and confirmation password do not match."),
                "No se mostró el mensaje de error esperado.");

            bool mensajeErrorEncontrado = bodyText.Contains("The password and confirmation password do not match.");

            if (mensajeErrorEncontrado)
            {
                GuardarEvidencia
                (
                    "Registro_PruebaNegativa",
                    "Reporte de prueba de registro de usuario negativa",
                    $"Se detectó el mensaje esperado de error."
                );

            }
        }
    }
}
