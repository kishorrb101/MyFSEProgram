using NUnit.Framework;
using FseProjectManagement.Web.Extensions.Mapper;
using NBench;


namespace FseProjectManagement.Web.Test
{
    [SetUpFixture]
    public class TestSetup
    {
         
        public void InitializeOneTimeData()
        {
            AutoMapperConfig.Initialize();
        }

        public void TearDown()
        {
            AutoMapper.Mapper.Reset();
        }
    }
}
