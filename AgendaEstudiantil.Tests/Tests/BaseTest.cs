using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AgendaEstudiantil.Tests.Helpers
{
    public class BaseTest
    {

        //Atributos y métodos para reutilizar en mis pruebas

        protected IWebDriver driver;
        protected WebDriverWait wait;
        private string url = "http://localhost:5134/Identity/Account/Login";

        public BaseTest()
        {
            var options = new ChromeOptions();
            options.AddArguments("--ignore-certificate-errors");
            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void IniciarSesion(string email = "ysmajimo@gmail.com", string password = "Quetym062403#")
        {
            driver.Navigate().GoToUrl(url);

            driver.FindElement(By.Id("Input_Email")).SendKeys(email);
            driver.FindElement(By.Id("Input_Password")).SendKeys(password);
            driver.FindElement(By.Id("login-submit")).Click();

            System.Threading.Thread.Sleep(2000);
        }

        public void NavegarA(string url)
        {
            driver.Navigate().GoToUrl(url);
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public void CerrarNavegador()
        {
            driver.Quit();
        }

        public void GuardarEvidencia(string nombreArchivoBase, string tituloReporte, string mensaje)
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

            var carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Tests\Evidencias");
            carpeta = Path.GetFullPath(carpeta);

            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            var archivoImagen = Path.Combine(carpeta, $"{nombreArchivoBase}.png");
            var archivoReporte = Path.Combine(carpeta, $"Reporte_{nombreArchivoBase}.html");

            try
            {
                screenshot.SaveAsFile(archivoImagen);

                string contenidoHtml = $@"
                <html>
                <head><title>{tituloReporte}</title><meta charset=""UTF-8""></head>
                <body>
                    <h1>{tituloReporte}</h1>
                    <p>{mensaje}</p>
                    <img src='{nombreArchivoBase}.png' alt='Captura de pantalla'/>
                </body>
                </html>";

                File.WriteAllText(archivoReporte, contenidoHtml);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error guardando archivos: " + ex.Message);
            }
        }
    }
}
