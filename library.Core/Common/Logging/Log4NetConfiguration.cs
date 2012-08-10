namespace library.Core.Common.Logging 
{
    public class Log4NetConfiguration 
    {
        public void Configure() 
        {
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}